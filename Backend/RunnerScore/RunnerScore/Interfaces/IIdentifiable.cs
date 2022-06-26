namespace RunnerScore.Interfaces;

public interface IIdentifiable<TypeId>
{
    public TypeId Id { get; set; }
}
