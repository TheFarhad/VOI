namespace Framework.Endpoint;

using Shared.Exceptions;

public abstract class EndpointException(string message)
    : ServiceException($"Endpoint: {message}");