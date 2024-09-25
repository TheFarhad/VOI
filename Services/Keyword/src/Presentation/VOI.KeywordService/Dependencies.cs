namespace VOI.KeywordService;

using Framework.Endpoint;

public static class Dependencies
{
    public static IServiceCollection EndpointLayerDependencies(this IServiceCollection service, IConfiguration configuration)
        => service.FrameworkEndpointDependencies(configuration);
}
