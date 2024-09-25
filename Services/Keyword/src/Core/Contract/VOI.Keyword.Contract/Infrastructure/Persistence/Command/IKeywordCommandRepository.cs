namespace VOI.Keyword.Contract.Infrastructure.Persistence.Command;

using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Domain.Aggregates.Keyword.Entity;
using Domain.Aggregates.Keyword.ValueObject;

public interface IKeywordCommandRepository : ICommandRepository<Keyword, KeywordId>, IEventSorcingRepository
{
    void Add(Keyword source);

    ValueTask<EntityEntry<Keyword>> AddAsync(Keyword source, CancellationToken token = default!);

    ValueTask<Keyword?> FindByIdAsync(KeywordId id, CancellationToken token = default!);

    Task<Keyword?> FirstOrDefaultAsync(Expression<Func<Keyword, bool>> expression, CancellationToken token = default!);

    void Remove(Keyword source);
}