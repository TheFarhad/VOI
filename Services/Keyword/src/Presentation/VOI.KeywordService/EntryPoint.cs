namespace VOI.KeywordService;

using Keyword.ExternalServices;
using Keyword.Application.Shared;
using Keyword.Infrastructure.Persistence.Shared;
using Keyword.Infrastructure.Persistence.EF.Command.Context;

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
            .ApplicationLayerDependencies()
            .InfraPersistencenLayerDependencies(configuration)
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

        //app.DbContextAutoMigrator<ServiceCommandDbContext>();

        app.Run();
    }
}
