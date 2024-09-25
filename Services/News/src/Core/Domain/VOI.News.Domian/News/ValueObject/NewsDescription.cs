namespace VOI.News.Domian.News.ValueObject;

public sealed class NewsDescription : ValueObject<NewsDescription>
{
    public string Value { get; }

    private NewsDescription(string description)
    {
        if (String.IsNullOrWhiteSpace(description))
        {
            NewsNullPropertyException.Throw("description");
        }
        if (description.Length < 10 && description.Length > 500)
        {
            NewsDescriptionCharacterException.Throw();
        }
        Value = description;
    }

    public static NewsDescription New(string description)
       => new(description);

    public static explicit operator string(NewsDescription description)
      => description.Value;

    public static implicit operator NewsDescription(string description)
        => new(description);

    protected override IEnumerable<object> GetEqualityProperties()
    {
        yield return Value;
    }
}
