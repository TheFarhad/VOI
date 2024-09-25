namespace Framework.Core.Application.Shared.Pipeline;

using FluentValidation;
using Shared;

public sealed class RequestValidator(ILogger<RequestValidator> logger, IServiceProvider service)
    : RequestController
{
    private readonly IServiceProvider _serviceProvider = service;
    private readonly ILogger<RequestValidator> _logger = logger;

    public override async Task SendAsync<TRequest>(TRequest request, CancellationToken token = default)
    {
        var type = RequestType(request);

        //_logger.LogDebug("Validating request of type {RequestType} With value {request}  start at :{StartDateTime}", type, request, DateTime.Now);

        var errors = Validate(request);
        if (errors is { Count: > 0 })
        {
            //StringBuilder messages = new();
            //errors.ForEach(_ =>
            //{
            //    messages.AppendLine(_.Message);
            //});

            //_logger.LogInformation("Validating Request of type {RequestType} With value {Request} failed. Validation errors are: {ValidationErrors}", request, request, messages);
            return;
        }
        //_logger.LogDebug("Validating request of type {RequestType} With value {request}  finished at :{EndDateTime}", type, request, DateTime.Now);
        await InvokeChainAsync(request);
    }

    public override async Task<Response<TOutput>> SendAsync<TRequest, TOutput>(TRequest command, CancellationToken token = default!)
    {
        //LogStart(command, type);

        Response<TOutput> result = default!;
        var type = RequestType(command);
        var errors = Validate(command);

        if (errors is { Count: > 0 })
        {
            //StringBuilder messages = new();
            //errors.ForEach(_ =>
            //{
            //    messages.AppendLine(_.Message);
            //});
            //LogError(command, type, messages);
            result = errors;
        }
        else
        {
            //LogSuccess(command, type);
            result = await InvokeChainAsync<TRequest, TOutput>(command, token);
        }
        return result;
    }

    private Type RequestType<TRequest>(TRequest request)
        => request!.Type();

    private void LogStart<TRequest>(TRequest request, Type requestType)
        => _logger
             .LogDebug("Validating request of type {RequestType} With value {Request}  start at :{StartDateTime}", requestType, request, DateTime.Now);

    private void LogError<TRequest>(TRequest request, Type requestType, params string[] errors)
        => _logger.LogInformation("Validating request of type {RequestType} With value {Request}  failed. Validation errors are: {ValidationErrors}", requestType, request, errors);

    private void LogSuccess<TRequest>(TRequest request, Type requestType)
        => _logger
                .LogDebug("Validating request of type {RequestType} With value {Request} finished at :{EndDateTime}", requestType, request, DateTime.Now);

    private List<Error> Validate<TRequest>(TRequest request)
    {
        List<Error> result = [];

        var validator = _serviceProvider.GetService<IValidator<TRequest>>();
        if (validator is { })
        {
            var validationResult = validator.Validate(request);
            if (!validationResult.IsValid)
            {
                validationResult
                    .Errors
                    .ForEach(_ =>
                    {
                        result.Add(Error.BadRequest(_.ErrorMessage));
                    });
            }
        }
        else
        {
            //_logger.LogInformation("There is not any validator for {RequestType}", RequestType(request));
        }
        return result;
    }
}