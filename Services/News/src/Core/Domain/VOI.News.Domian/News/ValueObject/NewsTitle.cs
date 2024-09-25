namespace VOI.News.Domian.News.ValueObject;

public sealed class NewsTitle : ValueObject<NewsTitle>
{
    public string Value { get; }

    private NewsTitle(string title)
    {
        if (String.IsNullOrWhiteSpace(title))
        {
            NewsNullPropertyException.Throw("title");
        }
        if (title.Length < 3 && title.Length > 200)
        {
            NewsTitleInvalidCharacterException.Throw();
        }
        Value = title;
    }

    public static NewsTitle New(string title)
       => new(title);

    public static explicit operator string(NewsTitle title)
      => title.Value;

    public static implicit operator NewsTitle(string title)
        => new(title);

    protected override IEnumerable<object> GetEqualityProperties()
    {
        yield return Value;
    }
}
