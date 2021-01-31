using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Explicit)]
internal struct UIntFloat
{
    [FieldOffset(0)]
    public float floatValue;
    [FieldOffset(0)]
    public uint uintValue;
}

public static class ValueConversion
{
    public static int GetIntFromFloat(this float value)
    {
        UIntFloat union = new UIntFloat()
        {
            floatValue = value
        };

        return (int)union.uintValue;
    }

    public static float GetFloatFromInt(this int value)
    {
        UIntFloat union = new UIntFloat()
        {
            uintValue = (uint)value
        };

        return (int)union.floatValue;
    }

    public static uint GetUIntFromFloat(this float value)
    {
        UIntFloat union = new UIntFloat()
        {
            floatValue = value
        };

        return union.uintValue;
    }

    public static float GetFloatFromInt(this uint value)
    {
        UIntFloat union = new UIntFloat()
        {
            uintValue = (uint)value
        };

        return (int)union.floatValue;
    }
}