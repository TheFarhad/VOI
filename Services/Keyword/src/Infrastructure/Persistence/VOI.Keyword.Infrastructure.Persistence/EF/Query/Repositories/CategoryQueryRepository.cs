namespace VOI.Keyword.Infrastructure.Persistence.EF.Query.Repositories;

using Tool.Shared;
using Persistence.EF.Query.Context;
using Keyword.Contract.Application.DTO;
using Framework.Infra.Persistence.Query;
using Keyword.Contract.Application.Query;
using Keyword.Contract.Infrastructure.Persistence.Query;

internal sealed class CategoryQueryRepository(KeywordQueryDbContext dbContext) : EfQueryRepository<KeywordQueryDbContext>(dbContext), ICategoryQueryRepository
{
    public async Task<CategorySearchByTitlePayload> ListAsync(CategorySearchByTitle query, CancellationToken token = default)
    {
        var categories = Context.Categories.AsQueryable();
        var totalCount = query.NeededTotalCount ? await categories.CountAsync(token) : 0;

        // هر دو شرط زیر در پایپلاین بررسی می شود و دیگر نیازی نیست در اینجا چک شود
        if (!String.IsNullOrWhiteSpace(query.Title))
        {
            categories = categories.Where(_ => _.Title.Contains(query.Title));
        }

        var categoryItems = await categories
        .OrderBy(query.SortBy, query.SortAscending)
        .Skip(query.Skip)
        .Take(query.Size)
        .Select(_ => new CategoryItem(_.Id, _.Code, _.Title))
        .ToListAsync(token);

        var result = new CategorySearchByTitlePayload
        {
            Items = categoryItems,
            Total = totalCount
        };

        return result;
    }
}
