using System.Runtime.InteropServices;

namespace BloodstainedMemoryManipulator.Game.DataStructs;

[StructLayout(LayoutKind.Explicit, Pack = 4, Size = 0x40)]
internal struct ItemDetails
{
    [FieldOffset(0x00)]
    public nint CodePointer;

    [FieldOffset(0x08)]
    public Vector IconPath;

    [FieldOffset(0x18)]
    public long Unknown1;

    /// <summary>
    /// Has format ITEM_NAME_{ItemStringId} or SHARD_NAME_{ItemStringId}
    /// </summary>
    [FieldOffset(0x20)]
    public Vector InternalNameString;

    /// <summary>
    /// Has format ITEM_EXPLAIN_{ItemStringId} or SHARD_EXPLAIN_{ItemStringId}
    /// </summary>
    [FieldOffset(0x30)]
    public Vector ItemExplainString;
}
