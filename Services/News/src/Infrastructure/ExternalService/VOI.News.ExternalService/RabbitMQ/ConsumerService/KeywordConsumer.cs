namespace VOI.News.ExternalService.RabbitMQ.ConsumerService;

using Microsoft.Extensions.Options;
using Data.Event;
using Tool.Shared;
using Tool.Serialize;
using Framework.Core.Application.Event;
using Framework.Core.Domain.Aggregate.Event;

public interface IKeywordConsumer
{
    void ConsumeAsync();
}

public sealed class KeywordConsumer : IKeywordConsumer, IDisposable
{
    private readonly IModel _model;
    private readonly RabbitConfigs _rabbitConfigs;
    private readonly EventController _eventHandler;
    private readonly string _queue;
    private readonly ISerializeService _serializer;

    public KeywordConsumer(IServiceProvider serviceProvider, IOptionsMonitor<RabbitConfigs> rabbitConfigs, ISerializeService serializer)
    {
        _eventHandler = EventExecutor(serviceProvider);
        _rabbitConfigs = rabbitConfigs.CurrentValue;
        _serializer = serializer;
        _model = Model();
        _queue = QueueBind();
    }

    public void ConsumeAsync()
    {
        var consumer = new EventingBasicConsumer(_model);
        _model.BasicConsume(_queue, true, consumer);
        consumer.Received += Consumer_Received;
    }

    private EventController EventExecutor(IServiceProvider serviceProvider)
        => serviceProvider
            .CreateScope()
            .ServiceProvider
            .GetRequiredService<EventController>();

    private IModel Model()
       => new ConnectionFactory
       {
           HostName = _rabbitConfigs.HostName
       }
       .CreateConnection()
       .CreateModel();

    private void Exchange()
        => _model?.ExchangeDeclare(_rabbitConfigs.Exchange, ExchangeType.Topic);

    private string QueueBind()
    {
        var result = _model.QueueDeclare().QueueName;
        _model.QueueBind(result, _rabbitConfigs.Exchange, _rabbitConfigs.BindKey);
        return result;
    }

    private async void Consumer_Received(object? sender, BasicDeliverEventArgs e)
        => await EventHandleAsync(e);

    private async Task EventHandleAsync(BasicDeliverEventArgs source)
    {
        var state = EventState(source.RoutingKey);
        var eventData = source.Body.ToArray().TotString();
        IEvent _event = default!;

        if (state is nameof(DomainEventState.Added))
            _event = _serializer.Deserialize<KeywordCreated>(eventData)!;

        if (state is nameof(DomainEventState.Edited))
            _event = _serializer.Deserialize<KeywordEdited>(eventData)!;

        if (state is nameof(DomainEventState.Removed))
            _event = _serializer.Deserialize<KeywordRemoved>(eventData)!;

        await _eventHandler.SendAsync((dynamic)_event);
    }

    private string EventState(string routekey)
        => routekey.Split('.')[1];

    public void Dispose()
        => _model.Dispose();
}

public sealed record RabbitConfigs
{
    public string HostName { get; init; } = null!;
    public string Exchange { get; init; } = null!;
    public string BindKey { get; init; } = null!;
}