namespace VOI.Keyword.Domain.Aggregates.Keyword.Exception;

public sealed class KeywordTitleNullException()
    : DomainException("Keyword title should not be null");

public sealed class KeywordTitleCharacterException()
    : DomainException("The number of characters in the keyword title must be between 2 and 100");

public sealed class KeywordDescriptionNullException()
    : DomainException("Keyword description should not be null");

public sealed class KeywordDescriptionCharacterException()
    : DomainException("The number of characters in the Keyword description must be between 10 and 500");