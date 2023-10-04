using BloodstainedMemoryManipulator.Game.DataStructs;
using Process.NET.Memory;
using System.Text;

namespace BloodstainedMemoryManipulator.Process.NET.Memory;
internal static class MemoryExtensions
{
    public static nint GetAddress(this IMemory memory, nint baseAddress, IEnumerable<nint> offsets)
    {
        var currAddress = baseAddress;
        foreach (var offset in offsets.SkipLast(1))
        {
            currAddress = memory.Read<nint>(currAddress + offset);
        }

        if (offsets.Any())
        {
            currAddress += offsets.Last();
        }

        return currAddress;
    }

    public static T Read<T>(this IMemory memory, nint baseAddress, IEnumerable<nint> offsets)
        where T : struct
        => memory.Read<T>(memory.GetAddress(baseAddress, offsets));

    public static string ReadUnicodeString(this IMemory memory, Vector toRead) => memory.Read(toRead.ArrayPointer, Encoding.Unicode, (int)toRead.ArrayCount * 2);

    public static void Write<T>(this IMemory memory, nint baseAddress, IEnumerable<nint> offsets, T value)
        where T : struct
        => memory.Write(memory.GetAddress(baseAddress, offsets), value);
}
