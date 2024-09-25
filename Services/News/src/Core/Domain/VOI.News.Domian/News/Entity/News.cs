namespace VOI.News.Domian.News.Entity;

using Event;
using ValueObject;
using Framework.Core.Domain.Shared.Prop;

public sealed class News : AggregateRoot<NewsId>
{
    public NewsTitle Title { get; private set; }
    public NewsDescription Description { get; private set; }
    public NewsBody Body { get; private set; }

    // back field
    private readonly List<Keyword> _keywords = [];
    public IReadOnlyCollection<Keyword> Keywords => [.. _keywords];

    private News() { }
    private News(NewsTitle title, NewsDescription description, NewsBody body, List<Keyword> keywords)
    {
        Apply(NewsCreated.New(Code.Value, title.Value, description.Value, body.Value, keywords.Select(_ => _.Code).ToList()));
        SetKeywords(keywords);
    }

    public static News New(NewsTitle title, NewsDescription description, NewsBody body, List<Keyword> keywords)
        => new(title, description, body, keywords);

    public void EditNews(NewsTitle title, NewsDescription description, NewsBody body, List<Keyword> keywords)
    {
        Apply(NewsEdited.New(Code.Value, title.Value, description.Value, body.Value, keywords.Select(_ => _.Code).ToList()));
        SetKeywords(keywords);
    }

    private void On(NewsCreated source)
     => SetProperties(source.Title, source.Description, source.Body);

    private void On(NewsEdited source)
     => SetProperties(source.Title, source.Description, source.Body);

    private void SetProperties(string title, string description, string body)
    {
        Title = title;
        Description = description;
        Body = body;
    }

    private void SetKeywords(List<Keyword> keywords)
    {
        _keywords.Clear();
        _keywords.AddRange(keywords);
    }
}