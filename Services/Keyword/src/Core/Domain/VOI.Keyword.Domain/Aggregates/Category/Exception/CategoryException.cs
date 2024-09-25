namespace VOI.Keyword.Domain.Aggregates.Category.Exception;

using Framework.Core.Domain.Aggregate.Exception;

public sealed class CategoryTitleNullException()
    : DomainException("Category title should not be null");

public sealed class CategoryTitleCharacterException() :
    DomainException("The number of characters in the category title must be between 2 and 100");

public sealed class CategoryDescriptionNullException()
    : DomainException("Category Description should not be null");

public sealed class CategoryDescriptionCharacterException() :
    DomainException("The number of characters in the category description must be between 20 and 500");