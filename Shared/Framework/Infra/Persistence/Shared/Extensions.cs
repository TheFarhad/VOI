namespace Framework.Infra.Persistence.Shared;

using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Metadata;
using Core.Domain.Aggregate.Entity;

public static class Extensions
{
    public static IEnumerable<IMutableEntityType> EntitiesTypes<TEntity>(this ModelBuilder source)
        where TEntity : IEntity
        => source
            .EfEntitiesTypes(_ => typeof(TEntity).IsAssignableFrom(_.ClrType));

    public static ModelBuilder SoftDeletableQueryFilter(this ModelBuilder source)
    {
        Expression<Func<SoftDeletableModel, bool>> func = _ => !_.Deleted;
        foreach (var entityType in source.EfEntitiesTypes(_ => typeof(SoftDeletableModel).IsAssignableFrom(_.ClrType)))
        {
            var parameterExpression = Expression.Parameter(entityType.ClrType);
            var body = ReplacingExpressionVisitor.Replace(func.Parameters.First(), parameterExpression, func.Body);

            entityType
                .SetQueryFilter(Expression.Lambda(body, parameterExpression));
        }
        return source;
    }

    private static List<IMutableEntityType> EfEntitiesTypes(this ModelBuilder source, Func<IMutableEntityType, bool> predicate = null!)
    {
        var result = source.Model.GetEntityTypes();
        if (predicate is not null)
        {
            result = result.Where(predicate);
        }
        return result.ToList();
    }
}
