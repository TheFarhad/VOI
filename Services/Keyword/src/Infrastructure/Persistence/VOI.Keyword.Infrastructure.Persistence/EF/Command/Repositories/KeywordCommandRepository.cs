namespace VOI.Keyword.Infrastructure.Persistence.EF.Command.Repositories;

using Domain.Aggregates.Keyword.Entity;
using Domain.Aggregates.Keyword.ValueObject;
using Contract.Infrastructure.Persistence.Command;

internal sealed class KeywordCommandRepository(KeywordCommandDbContext context)
    : EfCommandRepository<Keyword, KeywordId, KeywordCommandDbContext>(context), IKeywordCommandRepository
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
}
