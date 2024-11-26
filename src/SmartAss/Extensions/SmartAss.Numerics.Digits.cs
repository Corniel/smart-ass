namespace System;

public static class DigitExtensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsDigit(this char c) => c >= '0' && c <= '9';

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int Digit(this char c) => c - '0';

    public static int? TryDigit(this char c) => c.IsDigit() ? c.Digit() : null;

    /// <summary>Gets the <see cref="int"/> values of all digit characters in the <see cref="string"/>.</summary>
    public static IEnumerable<int> Digits(this string str) => SmartAss.Numerics.Digits.Parse(str);

    public static IReadOnlyList<int> Digits(this int number)
    {
        if (number == 0) return [0];

        var digits = new int[10];
        var pos = 10;
        var remainder = number;

        while (remainder != 0)
        {
            digits[--pos] = (remainder % 10);
            remainder = remainder / 10;
        }
        return digits[pos..];
    }
}
