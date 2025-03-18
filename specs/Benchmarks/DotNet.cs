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
    public void Bits()
    {
        for (var i = 0; i < Count; i++)
        {
            O[i] = Imp.Bits(I[i]);
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
        public static int Bits(int value)
        {
            var abs = (value + (value >>= 31)) ^ value;
            return (abs & 0x8000_0000) == 0
                ? abs
                : throw new ArgumentOutOfRangeException(nameof(value));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Current(int value)
        {
            if (value < 0)
            {
                value = -value;
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value));
                }
            }
            return value;
        }
    }
}
    }
}
