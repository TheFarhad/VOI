namespace VOI.Keyword.Infrastructure.Persistence.EF.Command.Converters;

using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Domain.Aggregates.Keyword.Enum;
using Domain.Aggregates.Keyword.ValueObject;

internal sealed class KeywordIdConverter()
    : ValueConverter<KeywordId, int>(_ => _.Value, _ => KeywordId.New(_))
{ }

internal sealed class KeywordTitleConverter()
    : ValueConverter<KeywordTitle, string>(_ => _.Value, _ => KeywordTitle.New(_))
{ }

internal sealed class KeywordDescriptionConverter()
    : ValueConverter<KeywordDescription, string>(_ => _.Value, _ => KeywordDescription.New(_))
{ }

internal sealed class KeywordStatusConverter()
    : ValueConverter<KeywordStatus, string>(_ => _.Value, _ => KeywordStatus.New(_))
{ }

