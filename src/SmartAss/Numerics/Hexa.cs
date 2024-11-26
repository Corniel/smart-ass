namespace SmartAss.Numerics;

public static class Hexa
{
    [Pure]
    public static ulong UInt64(string str) => uint.Parse(str, NumberStyles.HexNumber, CultureInfo.InvariantCulture);

    [Pure]
    public static char Char(string str) => (char)UInt64(str);
}
