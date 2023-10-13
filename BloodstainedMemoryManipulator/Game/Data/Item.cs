namespace BloodstainedMemoryManipulator.Game.Data;
public record Item
{
    public required string Id { get; init; }
    public required int Count { get; init; }
}
