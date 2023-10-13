namespace BloodstainedMemoryManipulator.Game.Data;
public record PlayerData
{
    public required int Health { get; init; }
    public required int MaxHealth { get; init; }
    public required int MP { get; init; }
    public required int MaxMP { get; init; }
    public required int Cash { get; init; }
}
