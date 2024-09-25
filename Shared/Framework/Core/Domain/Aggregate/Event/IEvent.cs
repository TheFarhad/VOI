namespace Framework.Core.Domain.Aggregate.Event;

using Framework.Shared;

public interface IEvent
{
    //DateTime OccurredOn{ get; }
    Guid Code { get; }
    DomainEventState State { get; }
}

public enum DomainEventState : byte { Added = 0, Edited = 1, Removed = 2 }