namespace VOI.News.Domian.News.Exception;

public sealed class NewsBodyInvalidCharacterException()
    : DomainException("The number of characters in the News body must be more than 50")
{
    public static void Throw()
    {
        throw new NewsBodyInvalidCharacterException();
    }
}