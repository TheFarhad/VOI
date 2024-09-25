namespace VOI.Keyword.Infrastructure.Persistence.Shared;

using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using EF.Shared;
using EF.Query.Context;
using Framework.Infra.Persistence.Shared;

public static class Dependencies
{
    private static readonly Assembly _assembly = typeof(Dependencies).Assembly;

    public static IServiceCollection InfraPersistencenLayerDependencies(this IServiceCollection service, IConfiguration configuration)
        => service
              .FrameworkPersistenceDependencies(_assembly)
              .DbContextsDependencies<KeywordCommandDbContext, KeywordQueryDbContext>(configuration, KeywordServiceDbContextSchema.DefaultConnectionString,
                new EventSourcingSaveChangeInterceptor())
              .RedisDependencies(configuration)
              .ServiceRegisteryDependencies()
              ;
}

