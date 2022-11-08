using System.Globalization;

namespace SmartAss.Numerics;

public static class Hexa
{
    public static ulong UInt64(string str) => uint.Parse(str, NumberStyles.HexNumber, CultureInfo.InvariantCulture);
    public static char Char(string str) => (char)UInt64(str);
}
