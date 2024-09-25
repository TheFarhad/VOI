namespace Tool.FireForget;

using Microsoft.Extensions.DependencyInjection;

public static class Dependencies
{
    public static IServiceCollection FireForgetDependencies(this IServiceCollection source) =>
         source
        .AddSingleton<FireForgetProvider>();
}
