using System.Numerics;

namespace SmartAss.Numerics;

public static class Digits
{
    /// <summary>Gets the number of digits for base 10.</summary>
    [Pure]
    public static int DigitCount(this int number) => DigitCount(number, 10);

    /// <summary>Gets the number of digits for base 10.</summary>
    [Pure]
    public static int DigitCount(this long number) => DigitCount(number, 10);

    /// <summary>Gets the number of digits for the specified base.</summary>
    [Pure]
    public static int DigitCount<TNumber>(this TNumber number, TNumber @base) where TNumber : struct, INumber<TNumber>
    {
        var digits = 0;
        do
        {
            digits++;
            number /= @base;
        }
        while (number > TNumber.Zero);
        return digits;
    }

    [Pure]
    public static long ToInt64(IEnumerable<int> digits)
    {
        var n = 0L;
        foreach (var digit in digits)
        {
            n = n * 10 + digit;
        }
        return n;
    }

    [Pure]
    public static int ToInt32(IEnumerable<int> digits)
    {
        var n = 0;
        foreach (var digit in digits)
        {
            n = n * 10 + digit;
        }
        return n;
    }

    [Pure]
    public static IEnumerable<int> Parse(string str) => new DigitsParser(str);

    private struct DigitsParser(string s) : IEnumerator<int>, IEnumerable<int>
    {
        private readonly string str = s;
        private int pos = -1;

        public int Current { get; private set; } = 0;

        object IEnumerator.Current => Current;

        [Impure]
        public bool MoveNext()
        {
            while (++pos < str.Length)
            {
                Current = str[pos] - '0';
                if (Current >= 0 && Current <= 9) return true;
            }
            return false;
        }

        public void Reset() => Do.Nothing();

        [Pure]
        public IEnumerator<int> GetEnumerator() => this;

        [Pure]
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public void Dispose() => Do.Nothing();
    }
}
