namespace Framework.Core.Contract.Infra.Persistence.Command;

using Microsoft.EntityFrameworkCore.Storage;

public interface IUnitOfWork : IDisposable
{
    Task<bool> SaveAsync(CancellationToken token = default!);

    Task OnTransactionAsync(Action<IDbContextTransaction> onCommit, Action<IDbContextTransaction> onFailure = default!, CancellationToken token = default!);
}
