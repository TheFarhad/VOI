namespace VOI.News.Infrastructure.Persistence.EF.Command.Configuration;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared;
using Converter;
using Domian.News.Entity;

internal sealed class NewsConfigurator : IEntityTypeConfiguration<News>
{
    public void Configure(EntityTypeBuilder<News> builder)
    {
        builder
            .ToTable(NewsServiceDbContextSchema.NewsTableSchema.TableName);

        builder
            .Property(x => x.Id)
            .HasConversion<NewsIdConverter>()
            .ValueGeneratedOnAdd()
            .Metadata
            .SetBeforeSaveBehavior(PropertySaveBehavior.Ignore);

        builder
            .Property(_ => _.Title)
            .HasConversion<NewsTitleConverter>()
            .IsRequired()
            .IsUnicode(true)
            .HasMaxLength(200);

        builder
            .Property(_ => _.Description)
            .HasConversion<NewsDescriptionConverter>()
            .IsRequired()
            .IsUnicode(true)
            .HasMaxLength(500);

        builder
           .Property(_ => _.Body)
           .HasConversion<NewsBodyConverter>()
           .IsRequired()
           .IsUnicode(true);

        builder
            .OwnsMany(_ => _.Keywords, owned =>
            {
                owned
                .ToTable(NewsServiceDbContextSchema.NewsTableSchema.KeywordTableName);

                owned
                .Property(p => p.Code)
                .IsRequired()
                .HasColumnName(NewsServiceDbContextSchema.NewsTableSchema.KeywordColumnName);
            })
            .UsePropertyAccessMode(PropertyAccessMode.Field);

        builder
            .Navigation(_ => _.Keywords)
            .Metadata
            .SetField(NewsServiceDbContextSchema.NewsTableSchema.NewsKeywordColumnBackField);

    }
}
