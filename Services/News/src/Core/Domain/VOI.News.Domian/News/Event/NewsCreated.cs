namespace VOI.News.Domian.News.Event;

public sealed record NewsCreated : IEvent
{
    public Guid Code { get; }
    public string Title { get; }
    public string Description { get; }
    public string Body { get; }
    public List<Guid> Keywords { get; }
    public DomainEventState State { get; } = DomainEventState.Added;

    private NewsCreated(Guid code, string title, string description, string body, List<Guid> keywords)
    {
        Code = code;
        Title = title;
        Description = description;
        Body = body;
        Keywords = keywords;
    }

    public static NewsCreated New(Guid code, string title, string description, string body, List<Guid> keywords)
        => new(code, title, description, body, keywords);
}