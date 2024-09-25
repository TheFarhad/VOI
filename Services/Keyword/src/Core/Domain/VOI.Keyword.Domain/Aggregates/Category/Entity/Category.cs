namespace VOI.Keyword.Domain.Aggregates.Category.Entity;

using Event;
using ValueObject;

public sealed class Category : AggregateRoot<CategoryId>
{
    public CategoryTitle Title { get; private set; }
    public CategoryDescription Description { get; private set; }

    private Category() { }
    private Category(CategoryTitle title, CategoryDescription description)
        => Apply(new CategoryCreated(Code.Value, title.Value, description.Value));

    public static Category New(CategoryTitle title, CategoryDescription description)
        => new(title, description);

    public void ChangeTitle(CategoryTitle title)
        => Apply(new CategoryTitleChanged(Code.Value, title.Value, Description.Value));

    public void ChangeDescription(CategoryDescription description)
     => Apply(new CategoryDescriptionChanged(Code.Value, Title.Value, description.Value));

    public void ChangeTitleAndDescription(CategoryTitle title, CategoryDescription description)
      => Apply(new CategoryTitleAndDescriptionChanged(Code.Value, title.Value, description.Value));

    #region utilities

    private void On(CategoryCreated source)
        => SetProperties(source.Title, source.Description);

    private void On(CategoryTitleChanged source)
       => Title = source.Title;

    private void On(CategoryDescriptionChanged source)
       => Description = source.Description;

    private void On(CategoryTitleAndDescriptionChanged source)
       => SetProperties(source.Title, source.Description);

    private void SetProperties(string title, string description)
    {
        Title = title;
        Description = description;
    }

    #endregion
}
