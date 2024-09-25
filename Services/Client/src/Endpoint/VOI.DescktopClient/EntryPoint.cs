namespace VOI.DescktopClient;

using Tool.Identity;
using ClientServices;
using Tool.Serialize;

internal static class EntryPoint
{
    public static void Host(string[] args)
        => WebApplication
                .CreateBuilder(args)
                    .ConfigureServices()
                    .Configure();

    private static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        var services = builder.Services;
        var configuration = builder.Configuration;

        services
            .SerializationDependencies()
            .IdentityDependencies(configuration)
            .Configure<GzipCompressionProviderOptions>(options =>
            {
                options.Level = CompressionLevel.Optimal;
            })
           .Configure<BrotliCompressionProviderOptions>(options =>
           {
               options.Level = CompressionLevel.Optimal;
           })
           .AddResponseCompression(_ =>
           {
               _.EnableForHttps = true;
               _.Providers.Add<GzipCompressionProvider>();
               _.Providers.Add<BrotliCompressionProvider>();
           })
           .AddCarter()
           .AddEndpointsApiExplorer()
           .AddSwaggerGen();

        services.AddHttpClient<KeywordClient>();
        services.AddHttpClient<NewsClient>();

        return builder.Build();
    }

    private static void Configure(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.MapCarter();
        app.UseHttpsRedirection();
        app.Run();
    }
}
