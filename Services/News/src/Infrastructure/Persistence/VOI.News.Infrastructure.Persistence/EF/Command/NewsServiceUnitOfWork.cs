namespace VOI.News.Infrastructure.Persistence.EF.Command;

using Framework.Infra.Persistence.Command;
using Contract.Infrastructure.Persistence.Command;

internal sealed class NewsServiceUnitOfWork(NewsCommandDbContext context)
    : EfUnitOfWork<NewsCommandDbContext>(context), INewsServiceUnitOfWork
{ }
