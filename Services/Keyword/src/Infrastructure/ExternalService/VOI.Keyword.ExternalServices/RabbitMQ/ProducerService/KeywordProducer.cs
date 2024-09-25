namespace VOI.Keyword.ExternalServices.RabbitMQ.ProducerService;

using Tool.Shared;
using Framework.Core.Domain.Aggregate.Event;
using Infrastructure.Persistence.EF.Command.Context;

public interface IKeywordProducer
{
    Task PublishAsync(CancellationToken token = default);
}

public sealed class KeywordProducer
    : IKeywordProducer, IDisposable
{
    private readonly IModel _model;
    private readonly RabbitConfigs _rabbitConfigs;
    private readonly KeywordCommandDbContext _dbcontext;

    public KeywordProducer(IServiceProvider serviceProvider, IOptionsMonitor<RabbitConfigs> rabbitConfigs)
    {
        _dbcontext = DbContext(serviceProvider);

        _rabbitConfigs = rabbitConfigs.CurrentValue;
        _model = Model();
        Exchange();
    }

    public async Task PublishAsync(CancellationToken token = default)
    {
        var query = _dbcontext
                        .OutboxEvents
                        .Where(_ => _.OwnerAggregate == "Keyword"
                                    && !_.IsProccessd)
                        .Take(100)
                        .AsQueryable();

        var keywordEvents = await query.ToListAsync(token);
        if (keywordEvents?.Count > 0)
        {
            keywordEvents
                .ForEach(_ =>
                {
                    _model
                        .BasicPublish(_rabbitConfigs.Exchange,
                                      RouteKey(_.State),
                                      null,
                                      _.Payload.ToBytes());
                });

            await query
                     .ExecuteUpdateAsync(_ =>
                                        _.SetProperty(p => p.IsProccessd, true));
        }
    }

    private KeywordCommandDbContext DbContext(IServiceProvider serviceProvider)
        => serviceProvider
            .CreateScope()
            .ServiceProvider
            .GetRequiredService<KeywordCommandDbContext>();

    private IModel Model()
        => new ConnectionFactory
        {
            HostName = _rabbitConfigs.HostName
        }
        .CreateConnection()
        .CreateModel();

    private void Exchange()
        => _model?.ExchangeDeclare(_rabbitConfigs.Exchange, ExchangeType.Topic);

    private string RouteKey(DomainEventState state)
        => state switch
        {
            DomainEventState.Added =>
            Format(nameof(DomainEventState.Added)),
            DomainEventState.Edited =>
            Format(nameof(DomainEventState.Edited)),
            _ =>
            Format(nameof(DomainEventState.Removed))
        };

    private string Format(object? arg)
        => String.Format(_rabbitConfigs.RouteKey, arg);

    public void Dispose()
        => _model.Dispose();
}

public sealed record RabbitConfigs
{
    public string HostName { get; init; } = null!;
    public string Exchange { get; init; } = null!;
    public string RouteKey { get; init; } = null!;
}
