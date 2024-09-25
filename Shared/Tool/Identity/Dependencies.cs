namespace Tool.Identity;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public static class Dependencies
{
    public static IServiceCollection IdentityDependencies(this IServiceCollection source, IConfiguration configuration)
    {
        var identitySection = configuration.GetSection("IdentitySection");
        source.Configure<IdentityServiceConfig>(identitySection);

#if DEBUG
        source.AddScoped<IIdentityService, FakeIdentityService>();
#else
        source.AddScoped<IIdentityService, IdentityService>();
#endif

        return source;
    }

}
