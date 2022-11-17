namespace Benchmarks;

public class ParseDouble
{
    const int Zillions = 10_000;
    private string[] Strings = Array.Empty<string>();

    [GlobalSetup]
    public void Init()
    {
        var rnd = new MersenneTwister(17);
        Strings = Enumerable.Range(0, Zillions).Select(n => (1 - rnd.NextDouble() * double.MaxValue).ToString()).ToArray();
    }

    [Benchmark(Baseline = true)]
    public double[] DotNet() => Strings.Select(DotNetParse).ToArray();

    [Benchmark]
    public double[] SmartAss() => Strings.Select(SmartAssParse).ToArray();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private double DotNetParse(string str)
    {
        double.TryParse(str, NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out double result);
        return result;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static double SmartAssParse(string str)
    {
        Parser.ToDouble(str, out double result);
        return result;
    }
}
