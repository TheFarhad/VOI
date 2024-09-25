namespace VOI.Keyword.Application.Query.Keyword;

using Contract.Application.DTO;
using Contract.Application.Query;
using Framework.Core.Application.Shared;
using Contract.Infrastructure.Persistence.Query;

public sealed class KeywordSearchHandler(IKeywordQueryRepository keywordRepository)
    : IRequestHandler<KeywordSearchByTitleAndStatus, KeywordSearchByTitleAndStatusPayload>
{
    private readonly IKeywordQueryRepository _keywordRepository = keywordRepository;

    public async Task<Response<KeywordSearchByTitleAndStatusPayload>> HandleAsync(KeywordSearchByTitleAndStatus query, CancellationToken token = default)
    {
        var payload = await _keywordRepository.ListAsync(query, token);
        return payload?.Items?.Count > 0 ? payload : Error.NotFound("");
    }
}
