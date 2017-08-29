using BenchmarkDotNet.Attributes;
using System;

namespace LinqAF.Benchmark.Benchmarks
{
    public class Single
    {
        static int[] Source = new[] { 2 };
        static Func<int, bool> Predicate = x => x % 2 == 0;

        public class NoParams
        {
            [Benchmark]
            public int LinqAF() => Source.Single();
            [Benchmark(Baseline = true)]
            public int LINQ2Objects() => System.Linq.Enumerable.Single(Source);
        }

        public class OneParam
        {
            [Benchmark]
            public int LinqAF() => Source.Single(Predicate);
            [Benchmark(Baseline = true)]
            public int LINQ2Objects() => System.Linq.Enumerable.Single(Source, Predicate);
        }
    }
}
