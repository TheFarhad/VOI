namespace VOI.NewsService;

public static class Dependencies
{
    public static IServiceCollection EndpointLayerDependencies(this IServiceCollection service, IConfiguration configuration)
        => service.FrameworkEndpointDependencies(configuration);
}
