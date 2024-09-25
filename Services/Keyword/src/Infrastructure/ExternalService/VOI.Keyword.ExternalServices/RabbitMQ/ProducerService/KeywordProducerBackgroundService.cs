namespace VOI.Keyword.ExternalServices.RabbitMQ.ProducerService;

using Framework.Shared;

public sealed class KeywordProducerBackgroundService(IKeywordProducer keywordProducer)
    : HostedService
{
    private readonly IKeywordProducer _keywordProducer = keywordProducer;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await _keywordProducer.PublishAsync(stoppingToken);
            await Task.Delay(TimeSpan.FromSeconds(20), stoppingToken);
        }
    }
}