using BloodstainedMemoryManipulator.Game.Data;
using BloodstainedMemoryManipulator.Game.DataStructs;
using BloodstainedMemoryManipulator.Process.NET.Memory;
using Process.NET;
using Process.NET.Memory;
using System.Runtime.InteropServices;

namespace BloodstainedMemoryManipulator.Game;
internal class ProcessNetGame : IGame
{
    private readonly System.Diagnostics.Process _process;
    private readonly ProcessSharp _sharp;

    private readonly Vector[] _inventoryArrays;
    private readonly Dictionary<long, string> _runtimeToStringMap;
    private readonly Dictionary<string, long> _stringToRuntimeMap;
    private readonly Dictionary<long, int> _runtimeItemCounts;
    private readonly CancellationTokenSource _cancellationTokenSource;

    public event Action<Item>? ItemFound;
    public event Action<PlayerData>? PlayerDataChanged;
    public event Action? GameShutdown;

    /// <summary>
    /// Expects Bloodstained to be open and a save loaded
    /// </summary>
    /// <exception cref="InvalidOperationException"></exception>
    public ProcessNetGame()
    {
        var process = System.Diagnostics.Process.GetProcessesByName("BloodstainedRotN-Win64-Shipping").FirstOrDefault();
        if (process == null)
        {
            throw new InvalidOperationException("Bloodstained is not running");
        }
        this._process = process;

        //TODO: Find way to check if a save was loaded
        var isSaveLoaded = true;
        if (!isSaveLoaded)
        {
            throw new InvalidOperationException("Save was not loaded");
        }

        _sharp = new ProcessSharp(process, MemoryType.Remote);
        var inventoriesAddr = _sharp.Memory.GetAddress(_sharp.ModuleFactory.MainModule.BaseAddress, OffsetConsts.Inventories);
        _inventoryArrays = _sharp.Memory.Read<Vector>(inventoriesAddr, 0x11);
        var idToDetailsVector = _sharp.Memory.Read<Vector>(_sharp.ModuleFactory.MainModule.BaseAddress, OffsetConsts.IdToDetailsVector);
        var idDetailsStructs = _sharp.Memory.Read<IdToDetails>(idToDetailsVector.ArrayPointer, (int)idToDetailsVector.ArrayCount);
        var idDetails = idDetailsStructs.Select(x => (x.Id, Details: _sharp.Memory.Read<ItemDetails>(x.ItemDetailsPointer))).ToArray();
        var idDetailsStrings = idDetails.Select(x => (
                x.Id,
                IconPath: _sharp.Memory.ReadUnicodeString(x.Details.IconPath),
                ItemName: _sharp.Memory.ReadUnicodeString(x.Details.InternalNameString).Replace("ITEM_NAME_", "").Replace("SHARD_NAME_", ""),
                ItemExplain: _sharp.Memory.ReadUnicodeString(x.Details.ItemExplainString)
            )).ToArray();

        _runtimeToStringMap = idDetailsStrings.GroupBy(q => q.Id).ToDictionary(g => g.Key, g => g.First().ItemName);
        _stringToRuntimeMap = idDetailsStrings.GroupBy(q => q.ItemName).ToDictionary(q => q.Key, q => q.First().Id);

        _runtimeItemCounts = new Dictionary<long, int>();
        _ = UpdateItemCounts();
        _cancellationTokenSource = new CancellationTokenSource();
        _ = UpdateLoop(_cancellationTokenSource.Token);
    }

    public Item GetItem(string id) => new() { Id = id, Count = FindItem(_stringToRuntimeMap[id]).Item.Count };

    /// <summary>
    /// For now, only increments the amount in the player's inventory, meaning this will only work for items in a prepared save-file
    /// </summary>
    public void GiveItem(Item item)
    {
        var itemId = _stringToRuntimeMap[item.Id];
        var found = FindItem(itemId);
        found.Item.Count = Math.Max(0, Math.Min(found.Item.Count + item.Count, found.Item.MaxCount));
        _sharp.Memory.Write(found.Address, found.Item);
    }

    public void UpdatePlayerData(PlayerData playerData) => throw new NotImplementedException();

    public void Dispose()
    {
        _cancellationTokenSource.Cancel();
        Thread.Sleep(100);
        _sharp.Dispose();
        _process.Dispose();
    }

    private (nint Address, InventoryItem Item) FindItem(long itemId)
    {
        for (int i = 0; i < _inventoryArrays.Length; i++)
        {
            //Borrowed books are special items and not part of the regular inventories
            if (i == IndexingConsts.BorrowedBookInventory) continue;
            var items = _sharp.Memory.Read<InventoryItem>(_inventoryArrays[i].ArrayPointer, (int)_inventoryArrays[i].ArrayCount);

            for (int j = 0; j < items.Length; j++)
            {
                InventoryItem item = items[j];
                if (item.Id == itemId)
                {
                    return (_inventoryArrays[i].ArrayPointer + j * Marshal.SizeOf<InventoryItem>(), item);
                }
            }
        }

        throw new Exception("Could not find given item id");
    }

    private IEnumerable<(long Id, int Delta)> UpdateItemCounts()
    {
        for (int i = 0; i < _inventoryArrays.Length; i++)
        {
            //Borrowed books are special items and not part of the regular inventories
            if (i == IndexingConsts.BorrowedBookInventory) continue;
            var items = _sharp.Memory.Read<InventoryItem>(_inventoryArrays[i].ArrayPointer, (int)_inventoryArrays[i].ArrayCount);

            foreach (var item in items)
            {
                if (!_runtimeItemCounts.ContainsKey(item.Id))
                {
                    _runtimeItemCounts[item.Id] = 0;
                }

                if (_runtimeItemCounts[item.Id] != item.Count)
                {
                    yield return (item.Id, item.Count - _runtimeItemCounts[item.Id]);
                }

                _runtimeItemCounts[item.Id] = item.Count;
            }
        }
    }

    private async Task UpdateLoop(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            var updatedItems = UpdateItemCounts();

            foreach (var (id, delta) in updatedItems)
            {
                ItemFound?.Invoke(new Item { Id = _runtimeToStringMap[id], Count = delta });
            }

            await Task.Delay(1000, cancellationToken);
        }
    }
}
