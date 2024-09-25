namespace VOI.News.Domian.News.Exception;

public sealed class NewsDescriptionCharacterException()
    : DomainException("The number of characters in the News description must be between 10 and 500")
{
    public static void Throw()
    {
        throw new NewsDescriptionCharacterException();
    }
}
