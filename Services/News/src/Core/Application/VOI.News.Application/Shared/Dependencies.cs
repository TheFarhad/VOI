namespace VOI.News.Application.Shared;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public static class Dependencies
{
    private static readonly Assembly _assembly = typeof(Dependencies).Assembly;

    public static IServiceCollection ApplicationLayerDependencies(this IServiceCollection service, IConfiguration configuration)
      => service.FrameworkApplicationDependencies(_assembly);
}
