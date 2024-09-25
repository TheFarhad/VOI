namespace VOI.Keyword.Infrastructure.Persistence.EF.Command.Configurations;

using Microsoft.EntityFrameworkCore.Metadata;
using Shared;
using Converters;
using Domain.Aggregates.Keyword.Entity;

internal sealed class KeywordConfigurator : IEntityTypeConfiguration<Keyword>
{
    public void Configure(EntityTypeBuilder<Keyword> builder)
    {
        builder
            .ToTable(KeywordServiceDbContextSchema.KeywordTableSchema.TableName);

        builder
            .Property(_ => _.Id)
            .HasConversion<KeywordIdConverter>()
            .ValueGeneratedOnAdd()
            .Metadata.SetBeforeSaveBehavior(PropertySaveBehavior.Ignore);

        builder
            .Property(_ => _.Title)
            .IsUnicode(true)
            .HasMaxLength(100)
            .HasConversion<KeywordTitleConverter>();

        builder
            .Property(_ => _.Description)
            .IsUnicode(true)
            .HasMaxLength(500)
            .HasConversion<KeywordDescriptionConverter>();

        builder
         .Property(_ => _.Status)
         .HasMaxLength(20)
         .HasConversion<KeywordStatusConverter>();
    }
}