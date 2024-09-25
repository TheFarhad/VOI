namespace VOI.Keyword.Infrastructure.Persistence.EF.Command.Repositories;

using Domain.Aggregates.Category.Entity;
using Domain.Aggregates.Category.ValueObject;
using Contract.Infrastructure.Persistence.Command;

internal sealed class CategoryRepository(KeywordCommandDbContext context)
    : EfCommandRepository<Category, CategoryId, KeywordCommandDbContext>(context), ICategoryCommandRepository
{
    //public async Task<List<OutboxEvent>> Get(int maxCount = 100)
    //    => await Context
    //                    .OutboxEvents
    //                    .Take(maxCount)
    //                    .ToListAsync();

    //public async Task MarkAsRead(List<OutboxEvent> outBoxEventItems)
    //{
    //    await Task.CompletedTask;
    //}
    //public ValueTask<Category?> FindByIdAsync(CategoryId id, CancellationToken token = default)
    //{
    //    throw new NotImplementedException();
    //}
}
