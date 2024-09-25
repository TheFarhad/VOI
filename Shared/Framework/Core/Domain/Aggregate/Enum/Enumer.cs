namespace Framework.Core.Domain.Aggregate.Enum;

public abstract class Enumer : IComparer
{
    public readonly string Value;
    public abstract string Display { get; protected set; }

    protected Enumer(string source)
        => Value = string.IsNullOrWhiteSpace(source) ? String.Empty : source;

    public List<T>? GetItems<T>() where T : Enumer
        => typeof(T)
                .GetProperties(BindingFlags.Static | BindingFlags.Public)
                .First((_) => _.Name == "Items")
                .GetValue(null)?
                .As<IList>()
                .Cast<T>()
                .ToList();

    public static bool operator ==(Enumer left, Enumer right)
    {
        var result = false;
        if (left is null && right is null) result = true;
        else if (left is not null && right is not null) result = left.Equals(right);
        return result;
    }

    public static bool operator !=(Enumer left, Enumer right)
        => !(left == right);

    public override bool Equals(object? obj)
        => obj is Enumer enumer && Value == enumer.Value;

    public override int GetHashCode()
        => base.GetHashCode();

    public int Compare(object? obj1, object? obj2)
    {
        var result = 0;
        if (obj1 is Enumer e1 && obj2 is Enumer e2)
        {
            result = e1.Value.CompareTo(e2.Value);
        }
        return result;
    }
}