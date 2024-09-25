namespace Framework.Endpoint;

using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Diagnostics;
using Core.Contract.Application.Shared;
using Framework.Core.Application.Shared;

public sealed class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        => _logger = logger;

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken token)
    {
#if DEBUG
        _logger
            .LogError(exception, "Exception occurred: {Message}", exception.Message);
#endif
        var error = GetError(exception);
        await httpContext.Response.WriteAsJsonAsync(error, token);
        return true;
    }

    private Error GetError(Exception exception)
        => exception is ServiceException ? Error.BadRequest(exception.Message) :
                                           Error.Server(exception.Message);
}
