using BloodstainedMemoryManipulator;
using BloodstainedMemoryManipulator.Game.DataStructs;
using BloodstainedMemoryManipulator.Process.NET.Memory;
using Process.NET;
using System.Runtime.InteropServices;
using System.Text;

var process = System.Diagnostics.Process.GetProcessesByName("BloodstainedRotN-Win64-Shipping").FirstOrDefault();
if (process == null)
{
    Console.WriteLine("Please launch Bloodstained");
    return;
}

var sharp = new ProcessSharp("BloodstainedRotN-Win64-Shipping", Process.NET.Memory.MemoryType.Remote);
var inventoriesAddr = sharp.Memory.GetAddress(sharp.ModuleFactory.MainModule.BaseAddress, OffsetConsts.Inventories) + OffsetConsts.StartOfInventories;
var inventoryArrays = sharp.Memory.Read<Vector>(inventoriesAddr, 0x11);
while (true)
{
    Console.Write("Enter {category} {item} {amt}:");
    var read = Console.ReadLine()!.Split(" ").Select(int.Parse).ToArray();
    var category = read[0];
    var itemIndex = read[1];
    var amount = read[2];
    var itemAddr = inventoryArrays[category].ArrayPointer + Marshal.SizeOf<InventoryItem>() * itemIndex;
    var item = sharp.Memory.Read<InventoryItem>(itemAddr);
    var realAmount = Math.Max(0, Math.Min(amount, item.MaxCount - item.Count));
    item.Count += realAmount;
    sharp.Memory.Write(itemAddr, item);
    Console.WriteLine($"Gave {realAmount} {sharp.Memory.Read(sharp.Memory.GetAddress(item.NamePointer, new nint[] { 0x28 }), Encoding.Unicode, 100)}, for a total of {item.Count}");
}
