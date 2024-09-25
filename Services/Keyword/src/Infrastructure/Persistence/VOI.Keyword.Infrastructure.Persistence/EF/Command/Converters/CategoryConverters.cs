namespace VOI.Keyword.Infrastructure.Persistence.EF.Command.Converters;

using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Domain.Aggregates.Category.ValueObject;

internal sealed class CategoryIdConverter()
    : ValueConverter<CategoryId, int>(_ => _.Value, _ => CategoryId.New(_))
{ }

internal sealed class CategoryTitleConverter()
    : ValueConverter<CategoryTitle, string>(_ => _.Value, _ => CategoryTitle.New(_))
{ }

internal sealed class CategoryDescriptionConverter()
    : ValueConverter<CategoryDescription, string>(_ => _.Value, _ => CategoryDescription.New(_))
{ }
