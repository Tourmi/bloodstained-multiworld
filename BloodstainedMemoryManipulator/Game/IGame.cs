using BloodstainedMemoryManipulator.Game.Data;

namespace BloodstainedMemoryManipulator.Game;
public interface IGame : IDisposable
{
    public event Action<Item>? ItemFound;
    public event Action<PlayerData>? PlayerDataChanged;
    public event Action? GameShutdown;

    public Item GetItem(string id);
    public void GiveItem(Item item);
    public void UpdatePlayerData(PlayerData playerData);
}
