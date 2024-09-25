namespace Framework.Infra.ExternalService;

public abstract class ExternalServiceException(string message)
    : ServiceException($"ExternalService: {message}")
{ }
