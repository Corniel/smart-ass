namespace Benchmarks;

public class Parsing
{
    private static readonly CultureInfo Culture = CultureInfo.InvariantCulture;
    private const int Zillions = 1_000;
    private string[] Decimals = Array.Empty<string>();
    private string[] Int32s = Array.Empty<string>();
    private string[] Int64s = Array.Empty<string>();

    [GlobalSetup]
    public void Init()
    {
        var rnd = new MersenneTwister(17);
        Decimals = Enumerable.Range(0, Zillions).Select(n => Int(rnd).ToString(Culture)).ToArray();
        Int32s = Enumerable.Range(0, Zillions).Select(n => Long(rnd).ToString(Culture)).ToArray();
        Int64s = Enumerable.Range(0, Zillions).Select(n => Decimal(rnd).ToString(Culture)).ToArray();

        static int Int(RandomSource rnd) => rnd.Next(short.MinValue, short.MaxValue) * rnd.Next(short.MaxValue);
        static long Long(RandomSource rnd) => Int(rnd) * rnd.NextInt64(int.MaxValue);
        static decimal Decimal(RandomSource rnd) => Long(rnd) * rnd.NextDecimal();
    }

    [Benchmark(Description = "decimal.Parse()")]
    public decimal[] Decimal_Parse() => Decimals.Select(DotNet.Decimal).ToArray();

    [Benchmark(Description = "double.Parse()")]
    public double[] Double_Parse() => Decimals.Select(DotNet.Double).ToArray();

    [Benchmark(Description = "int.Parse()")]
    public int[] Int_Parse() => Int32s.Select(DotNet.Int).ToArray();

    [Benchmark(Description = "long.Parse()")]
    public long[] Long_Parse() => Int64s.Select(DotNet.Long).ToArray();

    [Benchmark(Description = "Parser.ToDecimal()")]
    public decimal[] Parser_ToDecimal() => Decimals.Select(SmartAssParser.Decimal).ToArray();

    [Benchmark(Description = "Parser.ToDouble()")]
    public double[] Parser_ToDouble() => Decimals.Select(SmartAssParser.Double).ToArray();

    [Benchmark(Description = "Parser.ToInt32()")]
    public int[] Parser_ToInt32() => Int32s.Select(SmartAssParser.Int).ToArray();

    [Benchmark(Description = "Parser.ToInt64()")]
    public long[] Parser_ToInt64() => Int64s.Select(SmartAssParser.Long).ToArray();

    private static class DotNet
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static decimal Decimal(string str)
        {
            decimal.TryParse(str, NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out decimal result);
            return result;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Double(string str)
        {
            double.TryParse(str, NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out double result);
            return result;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Int(string str)
        {
            int.TryParse(str, NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture, out int result);
            return result;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static long Long(string str)
        {
            long.TryParse(str, NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture, out long result);
            return result;
        }
    }

    private static class SmartAssParser
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static decimal Decimal(string str) => Parser.ToDecimal(str).GetValueOrDefault();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Double(string str) => Parser.ToDouble(str).GetValueOrDefault();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Int(string str) => Parser.ToInt32(str).GetValueOrDefault();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static long Long(string str) => Parser.ToInt64(str).GetValueOrDefault();
    }
}
