namespace VOI.Keyword.Domain.Aggregates.Keyword.ValueObject;

public sealed class KeywordId : Identity
{
    public int Value { get; }

    private KeywordId(int id)
        => Value = id;

    public static KeywordId New(int id)
        => new(id);

    protected override IEnumerable<object> GetEqualityProperties()
    {
        yield return Value;
    }
}

public sealed class KeywordIdOld : Identity
{
    public int Value { get; }

    private KeywordIdOld(int id)
        => Value = id;


    public static KeywordIdOld New(int id)
        => new KeywordIdOld(id);

    protected override IEnumerable<object> GetEqualityProperties()
    {
        yield return Value;
    }
}