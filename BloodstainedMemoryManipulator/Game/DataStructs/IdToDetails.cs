using System.Runtime.InteropServices;

namespace BloodstainedMemoryManipulator.Game.DataStructs;

[StructLayout(LayoutKind.Explicit, Pack = 4, Size = 0x18)]
internal struct IdToDetails
{
    [FieldOffset(0x00)]
    public long Id;

    [FieldOffset(0x08)]
    public nint ItemDetailsPointer;

    [FieldOffset(0x10)]
    public int Unknown1;

    [FieldOffset(0x14)]
    public int Unknown2;
}
