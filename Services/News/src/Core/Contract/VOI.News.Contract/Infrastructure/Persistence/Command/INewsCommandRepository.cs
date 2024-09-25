namespace VOI.News.Contract.Infrastructure.Persistence.Command;

using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Domian.News.Entity;
using Domian.News.ValueObject;
using Framework.Core.Contract.Infra.Persistence.Command;

public interface INewsCommandRepository : ICommandRepository<News, NewsId>
{
    void Add(News news);

    ValueTask<EntityEntry<News>> AddAsync(News news, CancellationToken token = default!);

    ValueTask<News?> FindByIdAsync(NewsId id, CancellationToken token = default!);

    Task<News?> FirstOrDefaultAsync(Expression<Func<News, bool>> predicate, CancellationToken token = default!);

    void Remove(News source);
}