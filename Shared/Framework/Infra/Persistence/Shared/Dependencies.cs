namespace Framework.Infra.Persistence.Shared;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;
using Framework.Shared;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Framework.Infra.Persistence.Query;
using Framework.Infra.Persistence.Command;
using Core.Contract.Infra.Persistence.Query;
using Core.Contract.Infra.Persistence.Command;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Framework.Infra.Persistence.ServiceRegistration;

public static class Dependencies
{
    private static readonly ServiceLifetime _transient = ServiceLifetime.Transient;

    public static IServiceCollection FrameworkPersistenceDependencies(this IServiceCollection service, Assembly assembly) => service
        .Repositories(assembly)
        .UnitOfWork(assembly);

    private static IServiceCollection Repositories(this IServiceCollection source, Assembly assemby)
      => source
          .Dependencies(assemby, typeof(ICommandRepository<,>), _transient)
          .Dependencies(assemby, typeof(IQueryRepository), _transient);

    private static IServiceCollection UnitOfWork(this IServiceCollection source, Assembly assembly) => source
        .Dependencies(assembly, typeof(IUnitOfWork), ServiceLifetime.Scoped);

    public static IServiceCollection DbContextsDependencies<TCommandDbContext, TQueryDbContext>(this IServiceCollection service, IConfiguration configuration, string connectionStringSection, params IInterceptor[] interceptors)
        where TCommandDbContext : EFCommandDbContext<TCommandDbContext>
        where TQueryDbContext : EFQueryDbContext<TQueryDbContext>
    {
        var connstr = configuration.GetConnectionString(connectionStringSection);

        return service
                .CommandDbContext<TCommandDbContext>(connstr, interceptors)
                .QueryDbContext<TQueryDbContext>(connstr);
    }

    private static IServiceCollection CommandDbContext<TCommandDbContext>(this IServiceCollection service, string connectionString, params IInterceptor[] interceptors)
        where TCommandDbContext : EFCommandDbContext<TCommandDbContext>
      => service
          .AddDbContext<TCommandDbContext>(_ =>
          {
              _
               .UseSqlServer(connectionString)
               .AddInterceptors(interceptors)
               .LogOptions();
          });

    private static IServiceCollection QueryDbContext<TQueryDbContext>(this IServiceCollection service, string connectionString)
         where TQueryDbContext : EFQueryDbContext<TQueryDbContext>
        => service
            .AddDbContext<TQueryDbContext>(_ =>
            {
                _
                .UseSqlServer(connectionString)
                .LogOptions();
            });

    public static IServiceCollection RedisDependencies(this IServiceCollection service, IConfiguration configuration)
        => service
            .AddSingleton<IConnectionMultiplexer>(_ =>
            {
                var host = configuration.GetValue<String>("RedisConfigs:Host:Name");
                var port = configuration.GetValue<int>("RedisConfigs:Host:Port");
                var password = configuration.GetValue<String>("RedisConfigs:Password");

                return ConnectionMultiplexer.Connect(new ConfigurationOptions
                {
                    EndPoints = { $"{host}:{port}" },
                    Password = password
                });
            });

    public static IServiceCollection ServiceRegisteryDependencies(this IServiceCollection service)
        => service.AddSingleton<RedisServiceRegistry>();

    private static void LogOptions(this DbContextOptionsBuilder source)
    {
#if DEBUG
        source
            .LogTo(Console.WriteLine, LogLevel.Information)
            .EnableDetailedErrors()
            .EnableSensitiveDataLogging();
#endif
    }
}
