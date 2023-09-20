using System.Runtime.InteropServices;

namespace BloodstainedMemoryManipulator.Game.DataStructs;

/// <summary>
/// Only the Borrowed Book inventory category uses this struct
/// </summary>
[StructLayout(LayoutKind.Explicit, Pack = 4, Size = 0x58)]
internal struct BorrowedBookItem
{
    /// <summary>
    /// Unique ID for the item
    /// </summary>
    [FieldOffset(0x00)]
    public long Id;

    /// <summary>
    /// No information
    /// </summary>
    [FieldOffset(0x08)]
    public nint UnknownPointer1;

    /// <summary>
    /// No information
    /// </summary>
    [FieldOffset(0x10)]
    public int Unknown1;

    /// <summary>
    /// No information
    /// </summary>
    [FieldOffset(0x14)]
    public int Unknown2;

    /// <summary>
    /// No information
    /// </summary>
    [FieldOffset(0x18)]
    public nint UnknownPointer2;

    /// <summary>
    /// No information
    /// </summary>
    [FieldOffset(0x20)]
    public nint UnknownPointer3;

    /// <summary>
    /// 0x12 for some reason
    /// </summary>
    [FieldOffset(0x28)]
    public int Unknown3;

    /// <summary>
    /// No information
    /// </summary>
    [FieldOffset(0x2C)]
    public int Unknown4;

    /// <summary>
    /// No information
    /// </summary>
    [FieldOffset(0x30)]
    public nint UnknownPointer4;

    /// <summary>
    /// No information
    /// </summary>
    [FieldOffset(0x38)]
    public nint UnknownPointer5;

    /// <summary>
    /// 0x12 for some reason
    /// </summary>
    [FieldOffset(0x40)]
    public int Unknown5;

    /// <summary>
    /// No information
    /// </summary>
    [FieldOffset(0x44)]
    public int Unknown6;

    /// <summary>
    /// 0x00010001 for some reason. Might be bools, or bytes. Unsure
    /// </summary>
    [FieldOffset(0x48)]
    public int Unknown7;

    /// <summary>
    /// No information
    /// </summary>
    [FieldOffset(0x4C)]
    public int Unknown8;

    /// <summary>
    /// No information
    /// </summary>
    [FieldOffset(0x50)]
    public nint UnknownPointer6;
}
