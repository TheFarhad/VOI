namespace BrowserGateway;

using Tool.Serialize;
using BackgroundServices;

internal static class EntryPoint
{
    internal static void Host(string[] args)
        => WebApplication
            .CreateBuilder(args)
            .ConfigureServices()
            .Configure();

    private static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        builder
            .Services
            .SerializationDependencies()
            .DiscovererDependencies(builder.Configuration)
            .AddReverseProxy();

        return builder.Build();
    }

    private static void Configure(this WebApplication app)
    {
        app.MapReverseProxy();
        app.Run();
    }
}
