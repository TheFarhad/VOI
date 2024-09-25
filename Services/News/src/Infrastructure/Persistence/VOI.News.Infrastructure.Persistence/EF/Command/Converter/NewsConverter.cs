namespace VOI.News.Infrastructure.Persistence.EF.Command.Converter;

using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Domian.News.ValueObject;

internal sealed class NewsIdConverter()
    : ValueConverter<NewsId, long>(_ => _.Value, _ => NewsId.New(_))
{ }

internal sealed class NewsTitleConverter()
    : ValueConverter<NewsTitle, string>(_ => _.Value, _ => NewsTitle.New(_))
{ }

internal sealed class NewsDescriptionConverter()
    : ValueConverter<NewsDescription, string>(_ => _.Value, _ => NewsDescription.New(_))
{ }

internal sealed class NewsBodyConverter()
    : ValueConverter<NewsBody, string>(_ => _.Value, _ => NewsBody.New(_))
{ }
