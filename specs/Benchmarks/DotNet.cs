namespace Benchmarks;

public class DotNet
{
    public class Maths
    {
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
                    O[i] = Imp.LT0(I[i]);
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

            [Benchmark]
            public void CMov()
            {
                for (var i = 0; i < Count; i++)
                {
                    O[i] = Imp.CMov(I[i]);
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

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static int CMov(int value)
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
