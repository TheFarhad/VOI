namespace VOI.Keyword.Infrastructure.Persistence.EF.Command.UnitOfWork;

using Context;
using Framework.Infra.Persistence.Command;
using Contract.Infrastructure.Persistence.Command;

internal sealed class KeywordServiceUnitOfWork(KeywordCommandDbContext dbContext) : EfUnitOfWork<KeywordCommandDbContext>(dbContext), IKeywordServiceUnitOfWork
{ }
