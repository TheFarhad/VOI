namespace VOI.News.Application.Query;

using Contract.Application.Query;
using Contract.Infrastructure.Persistence.Query;
using Framework.Core.Application.Shared;

public sealed class NewsSearchByTitleQueryHandler(INewsQueryRepository newsRepository) : IRequestHandler<NewsSearchByTitle, NewsSearchByTitlePayload>
{
    private readonly INewsQueryRepository _newsRepository = newsRepository;

    public async Task<Response<NewsSearchByTitlePayload>> HandleAsync(NewsSearchByTitle query, CancellationToken token = default!)
    {
        var payload = await _newsRepository.ListAsync(query, token);
        return payload?.Items?.Count > 0 ? payload : Error.NotFound("");
    }
}
