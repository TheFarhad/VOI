namespace VOI.News.Domian.News.Exception;

public sealed class NewsTitleInvalidCharacterException()
    : DomainException("The number of characters in the News title must be between 3 and 200")
{
    public static void Throw()
    {
        throw new NewsTitleInvalidCharacterException();
    }
}
