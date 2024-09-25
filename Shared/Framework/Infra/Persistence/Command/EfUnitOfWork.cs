namespace Framework.Infra.Persistence.Command;

using Core.Contract.Infra.Persistence.Command;

public class EfUnitOfWork<TEfCommandDbContext> : IUnitOfWork where TEfCommandDbContext : EFCommandDbContext<TEfCommandDbContext>
{
    private readonly TEfCommandDbContext _context;

    public EfUnitOfWork(TEfCommandDbContext context)
        => _context = context;

    public async Task<bool> SaveAsync(CancellationToken cancellationToken = default!)
        => await
                _context
               .SaveChangesAsync(cancellationToken) > 0;

    public async Task OnTransactionAsync(Action<IDbContextTransaction> onCommit, Action<IDbContextTransaction> onException = default!, CancellationToken cancellationToken = default!)
    {
        using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            if (onCommit is { })
            {
                onCommit.Invoke(transaction);
                await _context.Database.CommitTransactionAsync(cancellationToken);
                // or ??
                //await transaction.CommitAsync();
            }
        }
        catch (Exception)
        {
            if (onException is { })
            {
                onException.Invoke(transaction);
            }
            else
            {
                await _context.Database.RollbackTransactionAsync(cancellationToken);
                // or ??
                // await transaction.RollbackAsync();
            }
        }
    }

    public void Dispose()
        => _context.Dispose();
}
