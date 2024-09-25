namespace VOI.Keyword.Contract.Application.Command.Category;

using Framework.Core.Contract.Application.Shared;

public sealed record CategoryCreate(string Title, string Description)
    : IRequest<CategoryCreatePayload>;

public sealed record CategoryChangeTitleAndDescription(Guid Code, string Title, string Description)
    : IRequest<CategoryChangeTitleAndDescriptionPayload>;

public sealed record CategoryDelete(Guid Code)
    : IRequest<CategoryDeletePayload>;
