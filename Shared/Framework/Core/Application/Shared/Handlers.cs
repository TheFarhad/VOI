namespace Framework.Core.Application.Shared;

using Contract.Application.Shared;

public interface IRequestHandler<in TRequest>
    where TRequest : IRequest
{
    Task HandleAsync(TRequest request, CancellationToken token = default!);
}

public interface IRequestHandler<in TRequest, TOutput>
    where TRequest : IRequest<TOutput>
{
    Task<Response<TOutput>> HandleAsync(TRequest request, CancellationToken token = default!);
}
