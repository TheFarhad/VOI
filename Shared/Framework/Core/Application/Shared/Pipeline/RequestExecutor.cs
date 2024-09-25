namespace Framework.Core.Application.Shared.Pipeline;

using System.Diagnostics;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Shared;

public sealed class RequestExecutor : RequestController
{
    private readonly IServiceProvider _provider;
    //private readonly ILogger<Executor> _logger;
    //private readonly Stopwatch _timer;

    public RequestExecutor(IServiceProvider provider/*, ILogger<Executor> logger*/)
    {
        _provider = provider;
        //_logger = logger;
        //_timer = new Stopwatch();
    }

    public override async Task SendAsync<TRequest>(TRequest request, CancellationToken token = default!)
    {
        var type = request.Type();
        //_timer.Start();
        try
        {
            //LogStart(command, type);
            List<Task> tasks = [];
            _provider
               .GetServices<IRequestHandler<TRequest>>()
               .ToList()
               .ForEach(_ => tasks.Add(_.HandleAsync(request, token)));

            await Task.WhenAll(tasks);
        }
        catch (Exception e)
        {
            //LogError(e, type);
            throw;
        }
        finally
        {
            //_timer.Stop();
            //LogFinal(type);
        }
    }

    public override async Task<Response<TOutput>> SendAsync<TRequest, TOutput>(TRequest request, CancellationToken token = default!)
    {
        var type = request.Type();
        //_timer.Start();
        try
        {
            //LogStart(request, type);
            return await _provider
                            .GetRequiredService<IRequestHandler<TRequest, TOutput>>()!
                            .HandleAsync(request, token);
        }
        catch (Exception e)
        {
            //LogError(e, type);
            throw e;
        }
        finally
        {
            //LogFinal(type);
            //_timer.Stop();
        }
    }

    //private void LogStart<TRequest>(TRequest request, Type requestType)
    //    => _logger.LogDebug("Routing request of type {RequestType} With value {Request}  Start at {StartDateTime}", requestType, request, DateTime.Now);

    //private void LogError(Exception exception, Type requestType)
    //    => _logger.LogError(exception, "There is not suitable handler for {RequestType} Routing failed at {StartDateTime}.", requestType, DateTime.Now);

    //private void LogFinal(Type requestType)
    //    => _logger.LogInformation("Processing the {RequestType} request tooks {Millisecconds} Millisecconds", requestType, _timer.ElapsedMilliseconds);
}
