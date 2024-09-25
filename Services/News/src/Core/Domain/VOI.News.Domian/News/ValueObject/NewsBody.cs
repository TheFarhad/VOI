namespace VOI.News.Domian.News.ValueObject;

public sealed class NewsBody : ValueObject<NewsBody>
{
    public string Value { get; }

    private NewsBody(string body)
    {
        if (String.IsNullOrWhiteSpace(body))
        {
            NewsNullPropertyException.Throw("body");
        }
        if (body.Length < 50)
        {
            NewsBodyInvalidCharacterException.Throw();
        }
        Value = body;
    }

    public static NewsBody New(string body)
       => new(body);

    public static explicit operator string(NewsBody body)
      => body.Value;

    public static implicit operator NewsBody(string body)
        => new(body);

    protected override IEnumerable<object> GetEqualityProperties()
    {
        yield return Value;
    }
}
