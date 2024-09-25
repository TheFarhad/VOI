namespace VOI.Keyword.Domain.Aggregates.Category.Event;

using Framework.Core.Domain.Aggregate.Event;

public sealed record CategoryCreated : IEvent
{
    public Guid Code { get; }
    public string Title { get; }
    public string Description { get; }
    public DomainEventState State { get; } = DomainEventState.Added;

    public CategoryCreated(Guid code, string title, string description)
    {
        Code = code;
        Title = title;
        Description = description;
    }
}

public sealed record CategoryTitleChanged : IEvent
{
    public Guid Code { get; }
    public string Title { get; }
    public string Description { get; }
    public DomainEventState State { get; } = DomainEventState.Edited;

    public CategoryTitleChanged(Guid code, string title, string description)
    {
        Code = code;
        Title = title;
        Description = description;
    }
}

public sealed record CategoryDescriptionChanged : IEvent
{
    public Guid Code { get; }
    public string Title { get; }
    public string Description { get; }
    public DomainEventState State { get; } = DomainEventState.Edited;

    public CategoryDescriptionChanged(Guid code, string title, string description)
    {
        Code = code;
        Title = title;
        Description = description;
    }
}

public sealed record CategoryTitleAndDescriptionChanged : IEvent
{
    public Guid Code { get; }
    public string Title { get; }
    public string Description { get; }
    public DomainEventState State { get; } = DomainEventState.Edited;

    public CategoryTitleAndDescriptionChanged(Guid code, string title, string description)
    {
        Code = code;
        Title = title;
        Description = description;
    }
}