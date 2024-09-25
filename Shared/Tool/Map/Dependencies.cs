namespace Tool.Map;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared;

public static class Dependencies
{
    public static IServiceCollection AutoMappingDependencies(this IServiceCollection source, IConfiguration configuration, string autoMapperConfigSection)
    {
        source.Configure<AutoMapperConfig>(configuration.GetSection(autoMapperConfigSection));
        var configs = configuration.Get<AutoMapperConfig>();
        var assembly = Assemblies.Get(configs.AssmblyNamesForLoadProfiles);

        return source
            .AddAutoMapper(assembly)
            .AddSingleton<IMap, Mapper>();
    }
}
