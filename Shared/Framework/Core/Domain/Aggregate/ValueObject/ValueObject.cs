namespace Framework.Core.Domain.Aggregate.ValueObject;

public abstract class ValueObject<T> : IEquatable<T>
    where T : ValueObject<T>
{
    public bool Equals(T? other)
       => this == other;

    public override bool Equals(object? obj)
        => obj is not null &&
        obj is T other &&
        obj.GetType() == GetType() &&
        GetEqualityProperties().SequenceEqual(other.GetEqualityProperties());

    public static bool operator ==(ValueObject<T> left, ValueObject<T> right)
        =>
        left is null && right is null ? true
            :
        left is not null && right is not null ? left.Equals(right)
        :
        false;

    public static bool operator !=(ValueObject<T> left, ValueObject<T> right)
        => !(left == right);

    public override int GetHashCode()
        => GetEqualityProperties()
                .Select(_ => _?.GetHashCode() ?? 0)
                .Aggregate((a, b) => a ^ b);

    protected abstract IEnumerable<object> GetEqualityProperties();
}

public abstract record ValueObject();
