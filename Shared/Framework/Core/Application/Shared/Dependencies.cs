namespace Framework.Core.Application.Shared;

using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Event;
using Shared.Pipeline;
using Framework.Shared;

public static class Dependencies
{
    private static readonly ServiceLifetime _transient = ServiceLifetime.Transient;

    public static IServiceCollection FrameworkApplicationDependencies(this IServiceCollection service, Assembly assembly)
        => service
            .Handlers(assembly)
            .Controllers();

    private static IServiceCollection Handlers(this IServiceCollection service, Assembly assembly)
        => service
            .Dependencies(
            [assembly],
            [
             typeof(IEventHandler<>),
             typeof(IRequestHandler<>),
             typeof(IRequestHandler<,>)
            ],
            _transient);

    private static IServiceCollection Controllers(this IServiceCollection service)
        => service
            .EventController()
            .RequestController();

    private static IServiceCollection EventController(this IServiceCollection service)
       => service
           .AddScoped<EventController>();

    private static IServiceCollection RequestController(this IServiceCollection service)
        => service
            .AddScoped<RequestValidator>()
            .AddScoped<RequestExecutor>()
            .AddScoped<RequestController>(_ =>
            {
                var validator = _.GetRequiredService<RequestValidator>();
                var executor = _.GetRequiredService<RequestExecutor>();

                validator.Pipe(executor);
                return validator;
            });
}
