using BenchmarkDotNet.Attributes;
using System;

namespace LinqAF.Benchmark.Benchmarks
{
    public class All
    {
        static int[] Source1 = new[] { 1, 2, 3 };
        static int[] Source2 = new[] { 5, 6, 7 };
        static Func<int, bool> LessThan = a => a < 4;

        public class OneParam
        {
            [Benchmark]
            public bool LinqAF() => Source1.All(LessThan) && !Source2.All(LessThan);
            [Benchmark(Baseline = true)]
            public bool LINQ2Objects() => System.Linq.Enumerable.All(Source1, LessThan) && !System.Linq.Enumerable.All(Source2, LessThan);
        }
    }
}
