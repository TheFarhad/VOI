using Microsoft.Extensions.Hosting;

namespace Framework.Shared;

public abstract class HostedService
    : BackgroundService
{ }

public interface ILifespanHostedService : IHostedLifecycleService
{
    async Task IHostedLifecycleService.StartingAsync(CancellationToken cancellationToken)
         => await Task.CompletedTask;

    async Task IHostedService.StartAsync(CancellationToken cancellationToken)
       => await Task.CompletedTask;

    async Task IHostedLifecycleService.StartedAsync(CancellationToken cancellationToken)
         => await Task.CompletedTask;

    async Task IHostedLifecycleService.StoppingAsync(CancellationToken cancellationToken)
        => await Task.CompletedTask;

    async Task IHostedService.StopAsync(CancellationToken cancellationToken)
      => await Task.CompletedTask;

    async Task IHostedLifecycleService.StoppedAsync(CancellationToken cancellationToken)
         => await Task.CompletedTask;

}
