namespace VOI.News.Contract.Application.Query;

using Framework.Core.Contract.Application.Shared;

public sealed record NewsSearchByTitle(string Title)
    : PageRequest<NewsSearchByTitlePayload>;


