using SmartAss.Collections;

namespace SmartAss.Numerics
{
    public sealed class Fractorial : NumericSequence
    {
        public long this[int n] => n < 20 ? First[n] : this.Skip(n).First();

        public IEnumerator<long> GetEnumerator() => new Numbers();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private readonly long[] First = new[]
        {
            1,
            1,
            2,
            6,
            24,
            120,
            720,
            5040,
            40320,
            362880,
            3628800,
            39916800,
            479001600,
            6227020800,
            87178291200,
            1307674368000,
            20922789888000,
            355687428096000,
            6402373705728000,
            121645100408832000,
            2432902008176640000,
        };

        class Numbers : Iterator<long>
        {
            public long N { get; private set; } = -1;
            public long Current { get; private set; } = 1;

            public bool MoveNext()
            {
                N++;
                if (N > 0) { Current *= N; }
                return true;
            }
            
            public void Dispose() => Do.Nothing();
            public void Reset() => throw new NotSupportedException();
        }
    }
}
