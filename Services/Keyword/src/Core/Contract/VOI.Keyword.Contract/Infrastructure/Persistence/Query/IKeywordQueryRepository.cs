namespace VOI.Keyword.Contract.Infrastructure.Persistence.Query;


public interface IKeywordQueryRepository : IQueryRepository
{
    Task<KeywordSearchByTitleAndStatusPayload> ListAsync(KeywordSearchByTitleAndStatus query, CancellationToken token = default!);
}