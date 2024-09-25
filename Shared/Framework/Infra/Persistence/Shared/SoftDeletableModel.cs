namespace Framework.Infra.Persistence.Shared;

public abstract class SoftDeletableModel
{
    public abstract bool Deleted { get; set; }
}
