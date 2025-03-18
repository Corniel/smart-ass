using BenchmarkDotNet.Running;

namespace Benchmarks;

public static class Program
{
    public static void Main(string[] args)
    {
        _ = BenchmarkRunner.Run<DotNet.Maths.Abs>();
    }

    public static void All()
    {
        _ = BenchmarkRunner.Run<Choose>();
    }
}
