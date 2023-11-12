using SmartAss.Numerics;
using System.Text;

namespace System;

public static class RadixExtensions
{
    public static string ToString(this long n, Radix radix)
        => radix.Size switch
        {
            1 => new('/', (int)n),
            60 => Sexagesimal(n),
            _ => ToString(n, "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ", radix.Size),
        };

    private static string Sexagesimal(long number)
    {
        if (number == 0) return "0";

        var chars = new StringBuilder();
        while (number != 0)
        {
            chars.Insert(0, $"[{number % 60:00}]");
            number /= 60;
        }
        return chars.ToString();
    }

    private static string ToString(long number, string digits, int radix)
    {
        if (number == 0) return "0";

        var chars = new char[2];
        var length = 0;

        while (number != 0)
        {
            Ensure(ref chars, length);
            chars[^++length] = digits[(int)(number % radix)];
            number /= radix;
        }
        return new(chars[^length..]);
    }

    private static void Ensure(ref char[] buffer, int length)
    {
        if (length == buffer.Length)
        {
            var copy = new char[length * 2];
            Array.Copy(buffer, 0, copy, length, length);
            buffer = copy;
        }
    }
}
