namespace VOI.News.Infrastructure.Persistence.EF.Query.Repositories;

using Tool.Shared;
using Contract.Application.Dto;
using Contract.Application.Query;
using Framework.Infra.Persistence.Query;
using Contract.Infrastructure.Persistence.Query;

internal sealed class NewsQueryRepository(NewsQueryDbContext context)
    : EfQueryRepository<NewsQueryDbContext>(context), INewsQueryRepository
{
    public async Task<NewsSearchByTitlePayload> ListAsync(NewsSearchByTitle query, CancellationToken token = default!)
    {
        var news = Context.News.AsQueryable();
        var totalCount = query.NeededTotalCount ? await news.CountAsync() : 0;

        if (!String.IsNullOrWhiteSpace(query.Title))
        {
            news = news.Where(_ => _.Title.Contains(query.Title));
        }

        var items = await news
            .Include(_ => _.Keywords)
            .ThenInclude(_ => _.Detail)
            .Skip(query.Skip)
            .Take(query.Size)
            .OrderBy(query.SortBy, query.SortAscending)
            .Select(_ => new NewsItem(_.Code, _.Title, _.Body))
            .ToListAsync(token);

        return new NewsSearchByTitlePayload
        {
            Items = items,
            Total = totalCount
        };
    }
}
