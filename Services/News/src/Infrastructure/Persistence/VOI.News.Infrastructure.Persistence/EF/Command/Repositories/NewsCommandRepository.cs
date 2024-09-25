namespace VOI.News.Infrastructure.Persistence.EF.Command.Repositories;

using Domian.News.Entity;
using Domian.News.ValueObject;
using Framework.Infra.Persistence.Command;
using Contract.Infrastructure.Persistence.Command;

internal sealed class NewsCommandRepository(NewsCommandDbContext context)
    : EfCommandRepository<News, NewsId, NewsCommandDbContext>(context),
    INewsCommandRepository
{

}
