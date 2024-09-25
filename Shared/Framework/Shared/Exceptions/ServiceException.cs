namespace Framework.Shared.Exceptions;

public interface IException { }

public abstract class ServiceException(string message)
    : Exception(message), IException
{ }
