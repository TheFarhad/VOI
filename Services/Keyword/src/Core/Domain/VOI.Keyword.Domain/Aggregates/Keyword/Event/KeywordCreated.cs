namespace VOI.Keyword.Domain.Aggregates.Keyword.Event;

using Enum;
using Framework.Core.Domain.Aggregate.Event;

public sealed record KeywordCreated : IEvent
{
    public Guid Code { get; }
    public string Title { get; }
    public string Description { get; }
    public string Status { get; }
    public DomainEventState State { get; } = DomainEventState.Added;

    public KeywordCreated(Guid code, string title, string description)
    {
        Code = code;
        Title = title;
        Description = description;
        Status = KeywordStatus.Preview.Value;
    }
}

public sealed record KeywordTitleAndDescriptionChanged : IEvent
{
    public Guid Code { get; }
    public string Title { get; }
    public string Description { get; }
    public string Status { get; }
    public DomainEventState State { get; } = DomainEventState.Edited;

    public KeywordTitleAndDescriptionChanged(Guid code, string title, string description, string status)
    {
        Code = code;
        Title = title;
        Description = description;
        Status = status;
    }
}

public sealed record KeywordStatusChanged : IEvent
{
    public Guid Code { get; }
    public string Title { get; }
    public string Description { get; }
    public string Status { get; }
    public DomainEventState State { get; } = DomainEventState.Edited;

    public KeywordStatusChanged(Guid code, string title, string description, string status)
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
