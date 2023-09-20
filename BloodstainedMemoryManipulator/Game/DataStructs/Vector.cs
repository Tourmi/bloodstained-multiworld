using System.Runtime.InteropServices;

namespace BloodstainedMemoryManipulator.Game.DataStructs;

[StructLayout(LayoutKind.Explicit, Pack = 4, Size = 0x10)]
internal struct Vector
{
    [FieldOffset(0x0)]
    public nint ArrayPointer;
    [FieldOffset(0x8)]
    public uint ArrayCount;
    [FieldOffset(0xC)]
    public uint ArrayCapacity;
}
