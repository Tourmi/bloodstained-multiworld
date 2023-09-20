using Process.NET.Memory;

namespace BloodstainedMemoryManipulator.Process.NET.Memory;
internal static class MemoryExtensions
{
    public static nint GetAddress(this IMemory memory, nint baseAddress, IEnumerable<nint> offsets)
    {
        var currAddress = baseAddress;
        foreach (var offset in offsets)
        {
            currAddress = memory.Read<nint>(currAddress + offset);
        }

        return currAddress;
    }

    public static T Read<T>(this IMemory memory, nint baseAddress, IEnumerable<nint> offsets)
        where T : struct
        => memory.Read<T>(memory.GetAddress(baseAddress, offsets));

    public static void Write<T>(this IMemory memory, nint baseAddress, IEnumerable<nint> offsets, T value)
        where T : struct
        => memory.Write(memory.GetAddress(baseAddress, offsets), value);
}
