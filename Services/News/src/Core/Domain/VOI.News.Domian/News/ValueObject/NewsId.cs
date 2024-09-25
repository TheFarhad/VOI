namespace VOI.News.Domian.News.ValueObject;

public sealed class NewsId : Identity
{
    public long Value { get; }

    private NewsId(long id)
        => Value = id;

    public static NewsId New(long id)
        => new(id);

    protected override IEnumerable<object> GetEqualityProperties()
    {
        yield return Value;
    }
}
