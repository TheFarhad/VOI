namespace Framework.Core.Application.Shared.Pipeline;

using Shared;
using Contract.Application.Shared;

public abstract class Pipeline<TChain>
    where TChain : Pipeline<TChain>
{
    protected TChain? Chain { get; private set; }

    public void Pipe(TChain chain)
        => Chain = chain;
}

public abstract class RequestController : Pipeline<RequestController>
{
    public abstract Task SendAsync<TRequest>(TRequest request, CancellationToken token = default!)
       where TRequest : IRequest;

    public abstract Task<Response<TOutput>> SendAsync<TRequest, TOutput>(TRequest request, CancellationToken token = default!)
         where TRequest : IRequest<TOutput>;

    protected async Task InvokeChainAsync<TRequest>(TRequest command, CancellationToken token = default!)
        where TRequest : IRequest
    {
        if (Chain is { })
            await Chain.SendAsync(command, token);
    }

    protected async Task<Response<TOutput>> InvokeChainAsync<TRequest, TOutput>(TRequest command, CancellationToken token = default!)
        where TRequest : IRequest<TOutput>
    {
        Response<TOutput> result = default!;
        if (Chain is { })
            result = await Chain.SendAsync<TRequest, TOutput>(command, token);
        return result;
    }
}