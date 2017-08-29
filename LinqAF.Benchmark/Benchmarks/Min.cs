using BenchmarkDotNet.Attributes;
using System;

namespace LinqAF.Benchmark.Benchmarks
{
    public class Min
    {
        static int[] Source1 = new[] { 1, 2, 3, 4 };
        static int?[] Source2 = new[] { 1, default(int?), 2, default(int?), 3 };
        static string[] Source3 = new[] { "foo", "bar", "fizz", "buzz" };
        static Func<string, int> Func = str => str.Length;

        public class NonNull
        {
            [Benchmark]
            public double LinqAF() => Source1.Min();
            [Benchmark(Baseline = true)]
            public double LINQ2Objects() => System.Linq.Enumerable.Min(Source1);
        }

        public class Nullable
        {
            [Benchmark]
            public double? LinqAF() => Source2.Min();
            [Benchmark(Baseline = true)]
            public double? LINQ2Objects() => System.Linq.Enumerable.Min(Source2);
        }

        public class Projection
        {
            [Benchmark]
            public double LinqAF() => Source3.Min(Func);
            [Benchmark(Baseline = true)]
            public double LINQ2Objects() => System.Linq.Enumerable.Min(Source3, Func);
        }
    }
}
