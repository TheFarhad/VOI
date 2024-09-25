namespace VOI.News.ExternalService.RabbitMQ.Data.Event;

using Framework.Core.Domain.Aggregate.Event;

public sealed record KeywordCreated : IEvent
{
    public Guid Code { get; }
    public string Title { get; }
    public string Description { get; }
    public string Status { get; }
    public DomainEventState State { get; } = DomainEventState.Added;

    public KeywordCreated(Guid code, string title, string description, string status)
    {
        Code = code;
        Title = title;
        Description = description;
        Status = status;
    }
}

public sealed record KeywordEdited : IEvent
{
    public Guid Code { get; }
    public string Title { get; }
    public string Description { get; }
    public string Status { get; }
    public DomainEventState State { get; } = DomainEventState.Edited;

    public KeywordEdited(Guid code, string title, string description, string status)
    {
        Code = code;
        Title = title;
        Description = description;
        Status = status;
    }
}

public sealed record KeywordRemoved : IEvent
{
    public Guid Code { get; }
    public string Title { get; }
    public string Description { get; }
    public string Status { get; }
    public DomainEventState State { get; } = DomainEventState.Removed;

    public KeywordRemoved(Guid code, string title, string description, string status)
    {
        Code = code;
        Title = title;
        Description = description;
        Status = status;
    }
}
