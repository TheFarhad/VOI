namespace Tool.Compression;

using Microsoft.Extensions.DependencyInjection;

public static class Dependencies
{
    public static IServiceCollection GZipLargeObjectCompressoionDependencies(this IServiceCollection source)
        => source.AddSingleton<IBlobCompression, GZipBlobCompression>();
}
