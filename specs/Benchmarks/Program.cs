﻿using BenchmarkDotNet.Running;

namespace Benchmarks;

public static class Program
{
    public static void Main(string[] args)
    {
        _ = BenchmarkRunner.Run<Parsing>();
    }
}