using Microsoft.Extensions.DependencyInjection;

namespace Tool.File;

public static class Dependencies
{
    public static IServiceCollection FilingDependencies(this IServiceCollection source)
        => source
            .AddSingleton<IFileService, FileService>()
            .AddSingleton<IUploadService, UploadService>();
}
