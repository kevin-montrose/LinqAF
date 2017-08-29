using BenchmarkDotNet.Attributes;
using System;

namespace LinqAF.Benchmark.Benchmarks
{
    public class Aggregate
    {
        static int Seed = 5;
        static int[] Source = new[] { 1, 2, 3, 4 };
        static Func<int, int, int> Func = (a, b) => a + b;
        static Func<int, double> ResultSelector = a => (a * 2.0) / 3.0;


        public class OneParam
        {
            [Benchmark]
            public int LinqAF() => Source.Aggregate(Func);

            [Benchmark(Baseline = true)]
            public int LINQ2Objects() => System.Linq.Enumerable.Aggregate(Source, Func);
        }

        public class TwoParams
        {
            [Benchmark]
            public int LinqAF() => Source.Aggregate(Seed, Func);

            [Benchmark(Baseline = true)]
            public int LINQ2Objects() => System.Linq.Enumerable.Aggregate(Source, Seed, Func);
        }

        public class ThreeParams
        {
            [Benchmark]
            public double LinqAF() => Source.Aggregate(Seed, Func, ResultSelector);

            [Benchmark(Baseline = true)]
            public double LINQ2Objects() => System.Linq.Enumerable.Aggregate(Source, Seed, Func, ResultSelector);
        }
    }
}