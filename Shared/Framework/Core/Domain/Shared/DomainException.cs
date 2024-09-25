namespace Framework.Core.Domain.Shared;

using Framework.Shared.Exceptions;

public abstract class DomainException(string message)
    : ServiceException($"Domian: {message}");

public sealed class NullPropertyException(string property) : DomainException(String.Format(message, property))
{
    private const string message = "{0} should not be null!";

    public static void Throw(string property)
    {
        throw new NullPropertyException(property);
    }
}

public sealed class InvalidCharacterException(string property, int minChar = 3, int maxChar = 100)
    : DomainException(String.Format(message, property, minChar, maxChar))
{
    private const string message = "The number of {0} characters must be between {1} and {2}";

    public static void Throw(string property, int minChar, int maxChar)
    {
        throw new InvalidCharacterException(property, minChar, maxChar);
    }
}