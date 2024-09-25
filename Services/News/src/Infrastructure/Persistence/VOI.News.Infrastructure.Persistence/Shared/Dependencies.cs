namespace VOI.News.Infrastructure.Persistence.Shared;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using EF.Command;
using EF.Query;
using EF.Shared;
using Framework.Infra.Persistence.Shared;
using Framework.Infra.Persistence.Command;

public static class Dependencies
{
    private static Assembly _assembly => typeof(Dependencies).Assembly;

    public static IServiceCollection InfraPersistenceLayerDependecies(this IServiceCollection service, IConfiguration configuration)
        => service
        .FrameworkPersistenceDependencies(_assembly)
        .DbContextsDependencies<NewsCommandDbContext, NewsQueryDbContext>(configuration, NewsServiceDbContextSchema.DefaultConnectionString,
            new EventSourcingSaveChangeInterceptor())
        .RedisDependencies(configuration)
        .ServiceRegisteryDependencies()
        ;
}
