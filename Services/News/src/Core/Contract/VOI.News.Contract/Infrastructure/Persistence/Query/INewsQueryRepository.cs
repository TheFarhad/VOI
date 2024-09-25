namespace VOI.News.Contract.Infrastructure.Persistence.Query;

using Application.Query;
using Framework.Core.Contract.Infra.Persistence.Query;

public interface INewsQueryRepository : IQueryRepository
{
    Task<NewsSearchByTitlePayload> ListAsync(NewsSearchByTitle query, CancellationToken token = default!);
}