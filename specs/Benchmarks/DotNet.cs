namespace Benchmarks;

public class DotNet
{
    public class Maths
    {
        public class Sign
        {
            public static class Imp
            {
                public static int Current(int value) => Math.Sign(value);
            }
        }

        public class Abs
        {
            private const int Count = 1_000_000;
            private readonly int[] I = new int[Count];
            private readonly int[] O = new int[Count];

            public Abs()
            {
                var rnd = new Random(42);
                for (int i = 0; i < Count; i++)
                {
                    I[i] = rnd.Next(-8000, 8001) * rnd.Next(8000);
                }
            }

            [Benchmark(Baseline = true)]
            public void Math_Abs()
            {
                for (var i = 0; i < I.Length; i++)
                {
                    O[i] = Imp.Current(I[i]);
                }
            }

            [Benchmark]
            public void LT0()
            {
                for (var i = 0; i < Count; i++)
                {
                    O[i] = Imp.LT0(I[i]);
                }
            }

            [Benchmark]
            public void Not_Int_MinValue()
            {
                for (var i = 0; i < Count; i++)
                {
                    O[i] = Imp.NotIntMinValue(I[i]);
                }
            }

            [Benchmark]
            public void Unchecked()
            {
                for (var i = 0; i < Count; i++)
                {
                    O[i] = Imp.Unchecked(I[i]);
                }
            }

            [Benchmark]
            public void NoInlining()
            {
                for (var i = 0; i < Count; i++)
                {
                    O[i] = Imp.NoInlining(I[i]);
                }
            }

            public static class Imp
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static int LT0(int value)
                {
                    value = (value + (value >>= 31)) ^ value;

                    if (value < 0)
                    {
                        throw new OverflowException("Some message");
                    }
                    return value;
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static int NotIntMinValue(int value)
                {
                    value = (value + (value >>= 31)) ^ value;

                    if (value == int.MinValue)
                    {
                        throw new OverflowException("Some message");
                    }
                    return value;
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static int Unchecked(int value)
                {
                    value = unchecked((value + (value >>= 31)) ^ value);

                    if (value == int.MinValue)
                    {
                        throw new OverflowException("Some message");
                    }
                    return value;
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static int Current(int value)
                {
                    if (value < 0)
                    {
                        value = -value;
                        if (value < 0)
                        {
                            throw new OverflowException("Some message");
                        }
                    }
                    return value;
                }

                [MethodImpl(MethodImplOptions.NoInlining)]
                public static int NoInlining(int value)
                {
                    value = value < 0 ? -value : value;

                    if (value == int.MinValue)
                    {
                        throw new OverflowException("Some message");
                    }

                    return value;
                }
            }
        }
    }
}
