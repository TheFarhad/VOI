namespace VOI.Keyword.Application.Shared;

using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Framework.Core.Application.Shared;

public static class Dependencies
{
    private static readonly Assembly _assembly = typeof(Dependencies).Assembly;

    public static IServiceCollection ApplicationLayerDependencies(this IServiceCollection service)
        => service.FrameworkApplicationDependencies(_assembly);
}
