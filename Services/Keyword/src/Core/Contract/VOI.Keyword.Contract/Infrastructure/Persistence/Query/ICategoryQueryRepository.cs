namespace VOI.Keyword.Contract.Infrastructure.Persistence.Query;

public interface ICategoryQueryRepository : IQueryRepository
{
    Task<CategorySearchByTitlePayload> ListAsync(CategorySearchByTitle query, CancellationToken token = default!);
}