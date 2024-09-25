namespace VOI.Keyword.Contract.Application.DTO;

using Framework.Core.Contract.Application.Shared;

public sealed record CategorySearchByTitlePayload
    : RequestOutput<CategoryItem>;

public sealed record CategoryItem(int Id, Guid Code, string Title);


