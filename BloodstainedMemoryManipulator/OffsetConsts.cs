namespace BloodstainedMemoryManipulator;
internal static class OffsetConsts
{
    public static readonly nint[] PlayerData1 = new nint[] { 0x0AAB30B0, 0x30, 0x508 };
    public static readonly nint[] PlayerData2 = new nint[] { 0x0AAB30B0, 0x30, 0x4B8 };
    public static readonly nint[] PlayerData3 = new nint[] { 0x0AAB30B0, 0x30, 0x4C8 };
    public static readonly nint[] PlayerData4 = new nint[] { 0x0AABEC88, 0x00, 0x20 };
    public static readonly nint[] PlayerData5 = new nint[] { 0x0AABEC88, 0x00, 0x220 };

    public static readonly nint[] Inventories = PlayerData1.Concat(new nint[] { 0xD28, 0x968 }).ToArray();

    public static readonly nint[] IdToDetailsVector = new nint[] { 0x0A81CEE0, 0x280, 0xF0, 0x270, 0xC18, 0x30 };
}
