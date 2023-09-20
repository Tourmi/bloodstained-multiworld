using System.Runtime.InteropServices;

namespace BloodstainedMemoryManipulator.Game.DataStructs;

[StructLayout(LayoutKind.Explicit, Pack = 4, Size = 0x78)]
internal struct InventoryItem
{
    /// <summary>
    /// Unique ID for the item
    /// </summary>
    [FieldOffset(0x00)]
    public long Id;

    /// <summary>
    /// Vector for the Icon Number of the item
    /// </summary>
    [FieldOffset(0x08)]
    public Vector IconNumber;

    /// <summary>
    /// Actual string value is stored in a string vector at offset 0x28 or 0x38 from here
    /// </summary>
    [FieldOffset(0x18)]
    public nint NamePointer;

    /// <summary>
    /// No information
    /// </summary>
    [FieldOffset(0x20)]
    public nint UnknownPointer1;

    /// <summary>
    /// 0x12 value for everything?
    /// </summary>
    [FieldOffset(0x28)]
    public int Unknown2;

    /// <summary>
    /// No information
    /// </summary>
    [FieldOffset(0x2C)]
    public int Unknown3;

    /// <summary>
    /// Actual string value is stored in a string vector at offset 0x28 or 0x38 from here
    /// </summary>
    [FieldOffset(0x30)]
    public nint DescriptionPointer;

    /// <summary>
    /// No information
    /// </summary>
    [FieldOffset(0x38)]
    public nint UnknownPointer2;

    /// <summary>
    /// 0x12 value for everything?
    /// </summary>
    [FieldOffset(0x40)]
    public int Unknown4;

    /// <summary>
    /// No information
    /// </summary>
    [FieldOffset(0x44)]
    public int Unknown5;

    /// <summary>
    /// How many of the item is present in the inventory
    /// </summary>
    [FieldOffset(0x48)]
    public int Count;

    /// <summary>
    /// The maximum amount of the item that can be carried
    /// </summary>
    [FieldOffset(0x4C)]
    public int MaxCount;

    /// <summary>
    /// No information
    /// </summary>
    [FieldOffset(0x50)]
    public int Unknown6;

    /// <summary>
    /// Index within the current inventory
    /// </summary>
    [FieldOffset(0x54)]
    public int InventoryIndex;

    /// <summary>
    /// No information
    /// </summary>
    [FieldOffset(0x58)]
    public int Unknown7;

    /// <summary>
    /// 0x000358C2 value for seemingly all weapons
    /// </summary>
    [FieldOffset(0x5C)]
    public int Unknown8;

    /// <summary>
    /// No information
    /// </summary>
    [FieldOffset(0x60)]
    public int Unknown9;

    /// <summary>
    /// 0x07 for weapons, 0x05 for books
    /// </summary>
    [FieldOffset(0x64)]
    public int Category;

    /// <summary>
    /// Knife is 0x258, empty for books
    /// </summary>
    [FieldOffset(0x68)]
    public int Unknown10;

    /// <summary>
    /// Knife is 0x3c, empty for books
    /// </summary>
    [FieldOffset(0x6C)]
    public int Unknown11;

    /// <summary>
    /// No information
    /// </summary>
    [FieldOffset(0x70)]
    public long Unknown12;
}
