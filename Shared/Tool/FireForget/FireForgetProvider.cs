namespace Tool.FireForget;

using Microsoft.Extensions.DependencyInjection;

public class FireForgetProvider(IServiceScopeFactory serviceScopeFactory)
{
    private readonly IServiceScopeFactory _serviceProvider = serviceScopeFactory;

    public void Execute<TService>(Func<TService, Task> func)
    {
        Task.Run(async () =>
        {
            try
            {
                using var scope = _serviceProvider.CreateScope();
                var service = scope.ServiceProvider.GetService<TService>();
                await func(service);
            }
            catch (Exception e)
            {
                // customize exception...
                Console.WriteLine(e.Message);
            }
        });
    }
}