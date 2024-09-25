namespace Framework.Shared;

using Scrutor;

public static class RegisterDynamically
{
    public static IServiceCollection Dependencies(this IServiceCollection
services, Assembly assembly, Type assignableTo, ServiceLifetime lifetime)
   => services.Dependencies([assembly], [assignableTo], lifetime);

    public static IServiceCollection Dependencies(this IServiceCollection
services, List<Assembly> assemblies, List<Type> assignableTo, ServiceLifetime lifetime)
   => services.Register(assemblies, assignableTo, lifetime);

    private static IServiceCollection Register(this IServiceCollection source, List<Assembly> assemblies, List<Type> assignableTo, ServiceLifetime lifeTime)
        => source
             .Scan(typeSelector =>
                 typeSelector.FromAssemblies(assemblies)
                 .AddClasses(typeFilter => typeFilter.AssignableToAny(assignableTo))
                 .AsImplementedInterfaces()
                 .SetLifeTime(lifeTime)
             );

    private static IImplementationTypeSelector SetLifeTime(this ILifetimeSelector source, ServiceLifetime lifeTime)
        => lifeTime switch
        {
            ServiceLifetime.Transient => source.WithTransientLifetime(),
            ServiceLifetime.Scoped => source.WithScopedLifetime(),
            ServiceLifetime.Singleton => source.WithSingletonLifetime(),
            _ => source
        };
}
