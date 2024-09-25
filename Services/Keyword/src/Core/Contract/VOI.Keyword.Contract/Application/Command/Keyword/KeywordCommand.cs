namespace VOI.Keyword.Contract.Application.Command.Keyword;

using Framework.Core.Contract.Application.Shared;

public sealed record KeywordCreate(string Title, string Description)
    : IRequest<KeywordCreatePayload>;

public sealed record KeywordChangeTitle(Guid Code, string Title)
    : IRequest<KeywordChangeTitlePayload>;

public sealed record KeywordChangeStatus(Guid Code, string Status)
    : IRequest<KeywordChangeStatusPayload>;

public sealed record KeywordDelete(Guid Code)
    : IRequest<KeywordDeletePayload>;
