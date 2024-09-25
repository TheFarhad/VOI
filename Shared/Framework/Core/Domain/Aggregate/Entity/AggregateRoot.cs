namespace Framework.Core.Domain.Aggregate.Entity;

public abstract class AggregateRoot<TId> : Entity<TId>, IAggregateRoot
    where TId : Identity
{
    public int Version { get; protected set; }

    private readonly List<IEvent> _events = [];
    public IReadOnlyCollection<IEvent> Events => [.. _events];

    protected AggregateRoot() { }
    protected AggregateRoot(IEnumerable<IEvent> source)
        => EventsToFinalState(source);

    public void ClearEvents()
        => _events.Clear();

    public void Apply<TDomainEvent>(TDomainEvent source) where TDomainEvent : IEvent
    {
        Mutate(source);
        SetEvent(source);
    }

    protected void EventsToFinalState(IEnumerable<IEvent> source)
    {
        if (source?.Any() is true)
        {
            ClearEvents(); // Is this true?
            foreach (var domainEvent in source)
            {
                Mutate(domainEvent);
                Version++;
            }
            _events.AddRange(source.ToList());
        }
    }

    private void SetEvent<TDomainEvent>(TDomainEvent source) where TDomainEvent : IEvent
        => _events.Add(source);

    private void Mutate<TDomainEvent>(TDomainEvent source) where TDomainEvent : IEvent
        => CallMethod("On", source.GetType(), [source]);
}
