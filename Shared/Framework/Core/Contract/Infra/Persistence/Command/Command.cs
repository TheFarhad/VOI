namespace Framework.Core.Contract.Infra.Persistence.Command;

using Shared;
using Domain.Aggregate.Entity;

public interface ICommandRepository<TAggregateRoot, TId> : IRepository
    where TAggregateRoot : AggregateRoot<TId>
    where TId : Identity
{ }