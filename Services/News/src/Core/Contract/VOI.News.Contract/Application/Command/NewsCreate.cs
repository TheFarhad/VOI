namespace VOI.News.Contract.Application.Command;

using Framework.Core.Contract.Application.Shared;

public sealed record NewsCreate(string Title, string Description, string Body, List<Guid> Keywords)
    : IRequest<NewsCreatePayload>;

public sealed record NewsEdit(Guid Code, string Title, string Description, string Body, List<Guid> Keywords)
    : IRequest<NewsEditPayload>;

public sealed record NewsDelete(Guid Code)
    : IRequest<NewsDeletePayload>;
