namespace Framework.Core.Domain.Aggregate.Entity;

using Event;

public interface IAggregateRoot : IEntity
{
    int Version { get; }

    IReadOnlyCollection<IEvent> Events { get; }
    public void ClearEvents();
}
