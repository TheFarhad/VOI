namespace VOI.News.Domian.News.ValueObject;

public sealed class Keyword : ValueObject<Keyword>
{
    public Guid Code { get; }

    public Keyword(Guid code)
        => Code = code;

    public static Keyword New(Guid code)
        => new(code);

    public static explicit operator Guid(Keyword keyword)
        => keyword.Code;

    public static implicit operator Keyword(Guid code)
      => new(code);

    protected override IEnumerable<object> GetEqualityProperties()
    {
        yield return Code;
    }
}
