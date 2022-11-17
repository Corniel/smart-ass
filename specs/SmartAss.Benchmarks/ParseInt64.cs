namespace Benchmarks;

public class ParseInt64
{
    const int Zillions = 10_000;
    private string[] Strings = Array.Empty<string>();

    [GlobalSetup]
    public void Init()
    {
        var rnd = new MersenneTwister(17);
        Strings = Enumerable.Range(0, Zillions).Select(n => rnd.NextInt64(long.MinValue, long.MaxValue).ToString()).ToArray();
    }

    [Benchmark(Baseline = true)]
    public long[] DotNet() => Strings.Select(DotNetParse).ToArray();

    [Benchmark]
    public long[] SmartAss() => Strings.Select(SmartAssParse).ToArray();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static long DotNetParse(string str)
    {
        long.TryParse(str, NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture, out long result);
        return result;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static long SmartAssParse(string str)
    {
        Parser.ToInt64(str, out long result);
        return result;
    }
}
