namespace VOI.News.ExternalService;

using System.Reflection;
using RabbitMQ.ConsumerService;
using Framework.Shared;
using Framework.Core.Application.Event;

public static class Dependencies
{
    private static readonly Assembly _assembly = typeof(Dependencies).Assembly;

    public static IServiceCollection InfraExternalServiceLayerDependencies(this IServiceCollection service, IConfiguration configuration)
     => service
        .Dependencies(_assembly, typeof(IEventHandler<>), ServiceLifetime.Transient)
        .Configure<RabbitConfigs>(configuration.GetSection(nameof(RabbitConfigs)))
        .AddSingleton<IKeywordConsumer, KeywordConsumer>()
        .AddHostedService<KeywordConsumerBackgroundService>()
        ;
}
