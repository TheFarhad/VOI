namespace VOI.News.Domian.News.Exception;

public sealed class NewsNullPropertyException : DomainException
{
    private const string message = "News {0} should not be null";

    private NewsNullPropertyException(string property)
        : base(String.Format(message, property))
    { }

    public static void Throw(string property)
    {
        throw new NewsNullPropertyException(property);
    }
}
