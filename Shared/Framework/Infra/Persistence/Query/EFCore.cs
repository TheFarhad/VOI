namespace Framework.Infra.Persistence.Query;

using Microsoft.EntityFrameworkCore;
using Shared;

public abstract class EFQueryDbContext<TQueryDbContext> : EFDbContext
    where TQueryDbContext : EFQueryDbContext<TQueryDbContext>
{
    protected EFQueryDbContext() : base() { }
    public EFQueryDbContext(DbContextOptions<TQueryDbContext> options)
        : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.SoftDeletableQueryFilter();

        /* ##
         از لحاظ دیتابیسی چنین کاری غلط است و سربار ایجاد میکند
         چون به تمام کوئری ها روی این جداول، شرط دیلیتد برابر با فالس را تحمیل میکند
        این فیلد هم ایندکس نیست

         مگر اینکه دو کار مهم انجام شود

         1
        اگر ستونهایی را به عنوان نانکلاستر ایندکس در نظر گرفتیم
        ستون دیلیتد را هم به به آنها اضافه منیم
        (نانکلاستر ترکیبی از چند ستون)

         2
         اگر ستونی داریم که مقدار آن یونیک است، آن ستون را نیز شرطی کنیم
         یعنی اینکه به شزطی یونیک باشد که دیلیتد حتما فالس باشد
         */
    }

    public override int SaveChanges()
        => 0;

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
        => 0;

    public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken token = default!)
        => await Task.FromResult(0);

    public override async Task<int> SaveChangesAsync(CancellationToken token = default!)
        => await Task.FromResult(0);
}

public abstract class EfQueryRepository<TQueryDbContext>(TQueryDbContext context)
    where TQueryDbContext : EFQueryDbContext<TQueryDbContext>
{
    protected readonly TQueryDbContext Context = context;
}

public interface IQueryModel { }