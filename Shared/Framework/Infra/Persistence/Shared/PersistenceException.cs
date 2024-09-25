namespace Framework.Infra.Persistence.Shared;

public abstract class PersistenceException(string message)
    : ServiceException($"Persistence: {message}");