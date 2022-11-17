namespace Benchmarks;

public class ParseInt32
{
    const int Zillions = 10_000;
    private string[] Strings = Array.Empty<string>();

    [GlobalSetup]
    public void Init()
    {
        var rnd = new MersenneTwister(17);
        Strings = Enumerable.Range(0, Zillions).Select(n => rnd.Next(int.MinValue, int.MaxValue).ToString()).ToArray();
    }

    [Benchmark(Baseline = true)]
    public int[] DotNet() => Strings.Select(DotNetParse).ToArray();

    [Benchmark]
    public int[] SmartAss() => Strings.Select(SmartAssParse).ToArray();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static int DotNetParse(string str)
    {
        int.TryParse(str, NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture, out int result);
        return result;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static int SmartAssParse(string str)
    {
        Parser.ToInt32(str, out int result);
        return result;
    }
}
