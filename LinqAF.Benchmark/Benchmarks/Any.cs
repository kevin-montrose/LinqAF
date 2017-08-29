using BenchmarkDotNet.Attributes;
using System;

namespace LinqAF.Benchmark.Benchmarks
{
    public class Any
    {
        static int[] Source1 = new[] { 1, 2, 3 };
        static int[] Source2 = new[] { 5, 6, 7 };
        static int[] Source3 = new int[0];
        static Func<int, bool> LessThan = a => a < 4;

        public class NoParams
        {
            [Benchmark]
            public bool LinqAF() => Source1.Any() && !Source3.Any();
            [Benchmark(Baseline = true)]
            public bool LINQ2Objects() => System.Linq.Enumerable.Any(Source1) && !System.Linq.Enumerable.Any(Source3);
        }

        public class OneParam
        {
            [Benchmark]
            public bool LinqAF() => Source1.Any(LessThan) && !Source2.Any(LessThan);
            [Benchmark(Baseline = true)]
            public bool LINQ2Objects() => System.Linq.Enumerable.Any(Source1, LessThan) && !System.Linq.Enumerable.Any(Source2, LessThan);
        }
    }
}
