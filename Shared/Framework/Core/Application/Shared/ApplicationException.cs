namespace Framework.Core.Application.Shared;

using Framework.Shared.Exceptions;

public abstract class ApplicationException(string message)
    : ServiceException($"Application: {message}");