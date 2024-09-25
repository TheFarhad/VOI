namespace BrowserGateway.BackgroundServices.ServiceDiscovery;

internal sealed class ServiceDisoverer(IProxyConfigProvider proxy)
    : BackgroundService
{
    private readonly DiscovererProxyProvider _proxy = (DiscovererProxyProvider)proxy;

    protected override async Task ExecuteAsync(CancellationToken token)
    {
        while (!token.IsCancellationRequested)
        {
            await _proxy.DiscoverServices();
            await Task.Delay(30000, token);
        }
    }
}