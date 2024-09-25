namespace Framework.Core.Domain.Aggregate.Entity;

public abstract class SoftDeletableSource<TId> : AggregateRoot<TId>, ISoftDeletable
    where TId : Identity
{
    public bool Deleted { get; private set; } = false;

    protected SoftDeletableSource() : base() { }
    protected SoftDeletableSource(IEnumerable<IEvent> source) : base(source) { }

    public void Remove() => Deleted = true;
    public void Active() => Deleted = false;
}

public interface ISoftDeletable
{
    bool Deleted { get; }
}
