namespace VOI.Keyword.Domain.Aggregates.Keyword.Entity;

using Enum;
using Event;
using ValueObject;

public sealed class Keyword : AggregateRoot<KeywordId>
{
    public KeywordTitle Title { get; private set; }
    public KeywordDescription Description { get; private set; }
    public KeywordStatus Status { get; private set; }

    private Keyword() { }
    public Keyword(KeywordTitle title, KeywordDescription description)
    {
        Status = KeywordStatus.Preview;
        Apply(new KeywordCreated(Code.Value, title.Value, description.Value));
    }

    public void ChangeTitleAndDescription(KeywordTitle title)
       => Apply(new KeywordTitleAndDescriptionChanged(Code.Value, title.Value, Description.Value, Status.Value));

    public void ChangeStatus(string status)
       => Apply(new KeywordStatusChanged(Code.Value, Title.Value, Description.Value, status));

    #region utilities

    private void On(KeywordCreated source)
    {
        Title = source.Title;
        Description = source.Description;
    }

    private void On(KeywordTitleAndDescriptionChanged source)
    {
        Title = source.Title;
        Description = source.Description;
    }

    private void On(KeywordStatusChanged source)
        => Status = KeywordStatus.New(source.Status);

    #endregion
}
