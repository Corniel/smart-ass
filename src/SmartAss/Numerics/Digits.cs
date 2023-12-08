namespace SmartAss.Numerics;

public static class Digits
{
    public static long ToInt64(IEnumerable<int> digits)
    {
        var n = 0L;
        foreach(var digit in digits) 
        {
            n = n * 10 + digit;
        }
        return n;
    }

    public static int ToInt32(IEnumerable<int> digits)
    {
        var n = 0;
        foreach (var digit in digits)
        {
            n = n * 10 + digit;
        }
        return n;
    }

    public static IEnumerable<int> Parse(string str) => new DigitsParser(str);

    private struct DigitsParser(string s) : IEnumerator<int>, IEnumerable<int>
    {
        private readonly string str = s;
        private int pos = -1;

        public int Current { get; private set; } = 0;

        object IEnumerator.Current => Current;

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
        public IEnumerator<int> GetEnumerator() => this;
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public void Dispose() => Do.Nothing();
    }
}
