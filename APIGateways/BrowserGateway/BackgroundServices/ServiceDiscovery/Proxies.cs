namespace BrowserGateway.BackgroundServices.ServiceDiscovery;

using Framework.Infra.Persistence.ServiceRegistration;

internal sealed class DiscovererProxyProvider
    : IProxyConfigProvider
{
    private DiscovererProxyConfig _proxyConfig = new(default!, default!);
    private readonly RedisServiceRegistry _registry;

    public DiscovererProxyProvider(RedisServiceRegistry registry)
        => _registry = registry;

    public IProxyConfig GetConfig()
        => _proxyConfig;

    public async Task DiscoverServices()
    {
        List<RouteConfig> routes = [];
        List<ClusterConfig> clusters = [];

        var redisKey = _registry.RedisKey();
        if (!String.IsNullOrWhiteSpace(redisKey) && _registry.KeyExists(redisKey))
        {
            var servicesInfo = await _registry.GetServices<Services>(redisKey);

            servicesInfo?
                .ServicesData
                .ForEach(_ =>
                {
                    routes.Add(ConfigRoute(_));
                    clusters.Add(ConfigClusters(_));
                });

            var temp = _proxyConfig;
            _proxyConfig = new(routes, clusters);
            temp?.Signal();
        }
    }

    private RouteConfig ConfigRoute(Service source)
    => new RouteConfig
    {
        ClusterId = source.ClusterId,
        RouteId = source.RouteId,
        Match = new RouteMatch { Path = source.RouteMatch },
        Transforms = new List<Dictionary<string, string>>
                    {
                        new Dictionary<string, string>
                        {
                            { "PathPattern", source.PathPattern }
                        }
                    }
    };

    private ClusterConfig ConfigClusters(Service source)
        => new ClusterConfig
        {
            LoadBalancingPolicy = LoadBalancingPolicy.RoundRobin,
            ClusterId = source.ClusterId,
            Destinations = source.Instances.Select(_ => new
            {
                Id = _.InstanceId,
                config = new DestinationConfig
                {
                    Address = $"https://{_.Host}:{_.Port}"
                }
            }).ToDictionary(e => e.Id, e => e.config)
        };
}

internal sealed class DiscovererProxyConfig : IProxyConfig
{
    public IReadOnlyList<RouteConfig> Routes { get; }
    public IReadOnlyList<ClusterConfig> Clusters { get; }
    public IChangeToken ChangeToken { get; }
    private CancellationTokenSource _tokenSource = new();

    public DiscovererProxyConfig(IReadOnlyList<RouteConfig> routes, IReadOnlyList<ClusterConfig> clusters)
    {
        Routes = routes;
        Clusters = clusters;
        ChangeToken = new CancellationChangeToken(_tokenSource.Token);
    }

    public void Signal()
        => _tokenSource.Cancel();
}

internal static class LoadBalancingPolicy
{
    internal const string RoundRobin = "RoundRobin";
    internal const string Last = "Last";
}
