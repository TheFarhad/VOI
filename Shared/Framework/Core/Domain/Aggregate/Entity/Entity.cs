namespace Framework.Core.Domain.Aggregate.Entity;

public interface IEntity { }

public abstract class Entity<TId> : IEntity, IEquatable<Entity<TId>>
    where TId : Identity
{
    public TId Id { get; private set; }
    public Code Code { get; private set; } = Code.New(Guid.NewGuid());

    protected Entity() { }

    public static bool operator ==(Entity<TId> left, Entity<TId> right)
    {
        var result = false;
        if (left is null && right is null) result = true;
        else if (left is { } && right is { }) result = left.Equals(right);
        return result;
    }

    public static bool operator !=(Entity<TId> left, Entity<TId> right)
        => !(left == right);

    public bool Equals(Entity<TId>? other)
        => Id == other?.Id;

    public override bool Equals(object? obj)
        => obj is Entity<TId> other && Id == other.Id;

    public override int GetHashCode()
        => HashCode.Combine(GetType(), Id);

    public object? CallMethod(string methodName, Type type, params object[] parameters)
        => GetType()
           .GetMethod(methodName, BindingFlags.Instance | BindingFlags.NonPublic, [type])
           ?.Invoke(this, parameters);
}