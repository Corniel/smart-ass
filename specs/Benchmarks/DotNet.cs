using System.Diagnostics.CodeAnalysis;
using System.Diagnostics;

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
            private readonly int[][] I =
            [
                new int[Count],
                new int[Count],
                new int[Count],
            ];
            private readonly int[] O = new int[Count];

            public Abs()
            {
                var rnd = new Random(42);

                for (int i = 0; i < Count; i++)
                {
                    var factor = rnd.NextDouble() > 0.99 ? -1 : +1;

                    I[0][i] = rnd.Next(-8000, 8001) * rnd.Next(8000);
                    I[1][i] = +factor * rnd.Next(0, 8001) * rnd.Next(8000);
                    I[2][i] = -factor * rnd.Next(0, 8001) * rnd.Next(8000);
                }
            }

            [Params(0)]
            public int Set { get; set; }

            [Benchmark(Baseline = true)]
            public void Math_Abs()
            {
                for (var i = 0; i < I.Length; i++)
                {
                    O[i] = Imp.Current(I[Set][i]);
                }
            }

            [Benchmark]
            public void Flatten()
            {
                for (var i = 0; i < I.Length; i++)
                {
                    O[i] = Imp.Flatten(I[Set][i]);
                }
            }

            [Benchmark]
            public void Alternative()
            {
                for (var i = 0; i < I.Length; i++)
                {
                    O[i] = Imp.Alternative(I[Set][i]);
                }
            }

            public static class Imp
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static int Current(int value)
                {
                    if (value < 0)
                    {
                        value = -value;
                        if (value < 0)
                        {
                            ThrowNegateTwosCompOverflow();
                        }
                    }
                    return value;
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static int Flatten(int value)
                {
                    if (value < 0)
                    {
                        value = -value;
                    }
                    if (value < 0)
                    {
                        ThrowNegateTwosCompOverflow();
                    }
                    return value;
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static int Alternative(int value)
                {
                    value = unchecked((value + (value >>= 31)) ^ value);

                    if (value < 0)
                    {
                        ThrowNegateTwosCompOverflow();
                    }
                    return value;
                }

                [DoesNotReturn]
                [StackTraceHidden]
                internal static void ThrowNegateTwosCompOverflow()
                {
                    throw new OverflowException("Message");
                }
            }
        }
    }
}
