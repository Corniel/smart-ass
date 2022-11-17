namespace Benchmarks;

public class ParseDecimal
{
    const int Zillions = 10_000;
    private string[] Strings = Array.Empty<string>();

    [GlobalSetup]
    public void Init()
    {
        var rnd = new MersenneTwister(17);
        Strings = Enumerable.Range(0, Zillions).Select(n => (1 - rnd.NextDecimal() * decimal.MaxValue).ToString()).ToArray();
    }

    [Benchmark(Baseline = true)]
    public decimal[] DotNet() => Strings.Select(DotNetParse).ToArray();

    [Benchmark]
    public decimal[] SmartAss() => Strings.Select(SmartAssParse).ToArray();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private decimal DotNetParse(string str)
    {
        decimal.TryParse(str, NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out decimal result);
        return result;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static decimal SmartAssParse(string str)
    {
        Parser.ToDecimal(str, out decimal result);
        return result;
    }
}
