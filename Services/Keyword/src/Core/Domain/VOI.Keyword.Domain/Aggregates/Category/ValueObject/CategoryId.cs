namespace VOI.Keyword.Domain.Aggregates.Category.ValueObject;

public sealed class CategoryId : Identity
{
    public int Value { get; }

    private CategoryId(int id)
        => Value = id;

    public static CategoryId New(int id)
       => new(id);

    protected override IEnumerable<object> GetEqualityProperties()
    {
        yield return Value;
    }
}