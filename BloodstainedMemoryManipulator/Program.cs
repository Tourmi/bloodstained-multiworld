using BloodstainedMemoryManipulator.Game;
using BloodstainedMemoryManipulator.Game.Data;

IGame game;
try
{
    game = new ProcessNetGame();
} catch (Exception ex)
{
    Console.WriteLine(ex.ToString());
    return;
}

game.ItemFound += item => Console.WriteLine($"Player got {item.Count} {item.Id} for a total of {game.GetItem(item.Id).Count}");
game.PlayerDataChanged += playerData => Console.WriteLine($"Player data changed: {playerData}");
game.GameShutdown += () => Console.WriteLine("Game has shutdown");

while (true)
{
    try
    {
        Console.WriteLine("Enter {item} {amount}:");
        var read = Console.ReadLine()!.Split(" ").ToArray();
        var id = read[0];
        var count = read.Length >= 2 ? int.Parse(read[1]) : 1;
        Console.WriteLine($"Giving {count} {id}");
        game.GiveItem(new Item { Id = id, Count = count });
    } catch (Exception ex)
    {
        Console.WriteLine(ex.ToString());
    }
}
