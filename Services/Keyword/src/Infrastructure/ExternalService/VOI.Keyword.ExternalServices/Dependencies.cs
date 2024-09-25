namespace VOI.Keyword.ExternalServices;

using RabbitMQ.ProducerService;

public static class Dependencies
{
    public static IServiceCollection InfraExternalServiceLayerDependencies(this IServiceCollection service, IConfiguration configuration)
     => service
        .Configure<RabbitConfigs>(configuration.GetSection(nameof(RabbitConfigs)))
        .AddSingleton<IKeywordProducer, KeywordProducer>()
        .AddHostedService<KeywordProducerBackgroundService>()
        ;
}
