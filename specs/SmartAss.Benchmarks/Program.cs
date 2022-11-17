using BenchmarkDotNet.Running;

namespace Benchmarks;

public static class Program
{
    public static void Main(string[] args)
    {
        _ = BenchmarkRunner.Run<ParseDecimal>();
        _ = BenchmarkRunner.Run<ParseDouble>();
        _ = BenchmarkRunner.Run<ParseInt32>();
        _ = BenchmarkRunner.Run<ParseInt64>();
    }
}
