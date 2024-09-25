namespace VOI.NewsService;

using News.ExternalService;
using News.Application.Shared;
using News.Infrastructure.Persistence.Shared;
using News.Infrastructure.Persistence.EF.Command;

internal static class EntryPoint
{
    internal static void Host(string[] args)
        => WebApplication
            .CreateBuilder(args)
            .ConfigureServices()
            .Configure();

    private static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        var services = builder.Services;
        var configuration = builder.Configuration;

        services
            .ApplicationLayerDependencies(configuration)
            .InfraPersistenceLayerDependecies(configuration)
            .InfraExternalServiceLayerDependencies(configuration)
            .EndpointLayerDependencies(configuration);

        return builder.Build();
    }

    private static void Configure(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.UseResponseCompression();
        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();

        //app.DbContextAutoMigrator<NewsCommandDbContext>();
        //Task.Run(async () =>
        //{
        //    await app.DbContextAutoMigrator<NewsCommandDbContext>();
        //});

        app.Run();
    }
}
