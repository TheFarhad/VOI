namespace VOI.Keyword.Contract.Infrastructure.Persistence.Command;

using Domain.Aggregates.Category.Entity;
using Domain.Aggregates.Category.ValueObject;
using Microsoft.EntityFrameworkCore.ChangeTracking;

public interface ICategoryCommandRepository :
    ICommandRepository<Category, CategoryId>, IEventSorcingRepository
{
    void Add(Category source);

    ValueTask<EntityEntry<Category>> AddAsync(Category source, CancellationToken token = default);

    ValueTask<Category?> FindByIdAsync(CategoryId id, CancellationToken token = default);

    Task<Category?> FirstOrDefaultAsync(Expression<Func<Category, bool>> expression, CancellationToken token = default!);

    void Remove(Category source);
}