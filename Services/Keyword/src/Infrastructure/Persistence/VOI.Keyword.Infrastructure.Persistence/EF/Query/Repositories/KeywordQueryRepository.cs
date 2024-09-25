namespace VOI.Keyword.Infrastructure.Persistence.EF.Query.Repositories;

using Tool.Shared;
using Persistence.EF.Query.Context;
using Keyword.Contract.Application.DTO;
using Framework.Infra.Persistence.Query;
using Keyword.Contract.Application.Query;
using Keyword.Contract.Infrastructure.Persistence.Query;

internal sealed class KeywordQueryRepository(KeywordQueryDbContext dbContext) : EfQueryRepository<KeywordQueryDbContext>(dbContext), IKeywordQueryRepository
{
    public async Task<KeywordSearchByTitleAndStatusPayload> ListAsync(KeywordSearchByTitleAndStatus query, CancellationToken token = default)
    {
        var keywords = Context.Keywords.AsQueryable();

        var totalCount = query.NeededTotalCount ? await keywords.CountAsync(token) : 0;

        // هر دو شرط زیر در پایپلاین بررسی می شود و دیگر نیازی نیست در اینجا چک شود
        if (!String.IsNullOrWhiteSpace(query.Title))
        {
            keywords = keywords.Where(_ => _.Title.Contains(query.Title));
        }
        if (!String.IsNullOrWhiteSpace(query.Status))
        {
            keywords = keywords.Where(_ => _.Status == query.Status);
        }

        var keywordItems = await keywords
            .Skip(query.Skip)
            .Take(query.Size)
            .OrderBy(query.SortBy, query.SortAscending)
            .Select(_ => new KeywordItem(_.Id, _.Code, _.Title, _.Status))
            .ToListAsync(token);

        var result = new KeywordSearchByTitleAndStatusPayload
        {
            Items = keywordItems,
            Total = totalCount
        };
        return result;
    }
}
