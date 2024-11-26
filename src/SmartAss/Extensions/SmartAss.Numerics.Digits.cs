namespace System;

public static class DigitExtensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [Pure]
    public static bool IsDigit(this char c) => c >= '0' && c <= '9';

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [Pure]
    public static int Digit(this char c) => c - '0';

    [Pure]
    public static int? TryDigit(this char c) => c.IsDigit() ? c.Digit() : null;

    /// <summary>Gets the <see cref="int"/> values of all digit characters in the <see cref="string"/>.</summary>
    [Pure]
    public static IEnumerable<int> Digits(this string str) => SmartAss.Numerics.Digits.Parse(str);

    [Pure]
    public static IReadOnlyList<int> Digits(this int number)
    {
        if (number == 0) return [0];

        var digits = new int[10];
        var pos = 10;
        var remainder = number;

        while (remainder != 0)
        {
            digits[--pos] = remainder % 10;
            remainder = remainder / 10;
        }
        return digits[pos..];
    }
}
