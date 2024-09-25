namespace VOI.Keyword.Contract.Application.Query;

using Framework.Core.Contract.Application.Shared;

public sealed record CategorySearchByTitle(string Title)
    : PageRequest<CategorySearchByTitlePayload>;
