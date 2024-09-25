namespace VOI.News.ExternalService.RabbitMQ.ConsumerService;

using Framework.Shared;

public sealed class KeywordConsumerBackgroundService
    : HostedService
{
    public KeywordConsumerBackgroundService(IKeywordConsumer consumer)
     => consumer.ConsumeAsync();

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Delay(TimeSpan.FromSeconds(30), stoppingToken);
        }
    }
}
