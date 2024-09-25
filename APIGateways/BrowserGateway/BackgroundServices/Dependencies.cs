namespace BrowserGateway.BackgroundServices;

using Yarp.ReverseProxy.Configuration;
using ServiceDiscovery;
using Framework.Infra.Persistence.Shared;

internal static class Dependencies
{
    internal static IServiceCollection DiscovererDependencies(this IServiceCollection service, IConfiguration configuration)
        => service
            .RedisDependencies(configuration)
            .ServiceRegisteryDependencies()
            .AddSingleton<IProxyConfigProvider, DiscovererProxyProvider>()
            .AddHostedService<ServiceDisoverer>();
}
