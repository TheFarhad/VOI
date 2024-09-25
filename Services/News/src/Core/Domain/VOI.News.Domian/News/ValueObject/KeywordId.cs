namespace VOI.News.Domian.News.ValueObject;

using System.Collections.Generic;

public sealed class KeywordId : Identity
{
    public int Value { get; }

    private KeywordId(int id)
        => Value = id;

    public static KeywordId New(int id)
        => new(id);

    protected override IEnumerable<object> GetEqualityProperties()
    {
        throw new NotImplementedException();
    }
}