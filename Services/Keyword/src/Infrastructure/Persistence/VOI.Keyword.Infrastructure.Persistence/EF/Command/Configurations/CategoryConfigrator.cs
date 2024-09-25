namespace VOI.Keyword.Infrastructure.Persistence.EF.Command.Configurations;

using Microsoft.EntityFrameworkCore.Metadata;
using Shared;
using Converters;
using Domain.Aggregates.Category.Entity;

internal sealed class CategoryConfigrator : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder
            .ToTable(KeywordServiceDbContextSchema.CategoryTableSchema.TableName);

        builder
            .Property(_ => _.Id)
            .HasConversion<CategoryIdConverter>()
            .ValueGeneratedOnAdd()
            .Metadata.SetBeforeSaveBehavior(PropertySaveBehavior.Ignore);

        builder
            .Property(_ => _.Title)
            .IsUnicode(true)
            .HasMaxLength(100)
            .HasConversion<CategoryTitleConverter>();

        builder
            .Property(_ => _.Description)
            .IsUnicode(true)
            .HasMaxLength(500)
            .HasConversion<CategoryDescriptionConverter>();
    }
}