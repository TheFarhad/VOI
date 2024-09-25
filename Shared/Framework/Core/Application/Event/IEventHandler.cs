namespace Framework.Core.Application.Event;

public interface IEventHandler<TEvent>
    where TEvent : IEvent
{
    Task HandleAsync(TEvent source, CancellationToken token = default!);
}

public sealed class EventController(IServiceProvider serviceProvider)
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;

    public async Task SendAsync<TEvent>(TEvent source, CancellationToken token = default!)
        where TEvent : IEvent
    {
        List<Task> tasks = [];
        _serviceProvider
            .GetServices<IEventHandler<TEvent>>()
            .ToList()
            .ForEach(_ => tasks.Add(_.HandleAsync(source, token)));

        await Task.WhenAll(tasks);
    }
}
