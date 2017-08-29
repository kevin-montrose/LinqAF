using BenchmarkDotNet.Attributes;
using System;

namespace LinqAF.Benchmark.Benchmarks
{
    public class Count
    {
        static int[] Source = new[] { 1, 2, 3 };
        static Func<int, bool> Predicate = x => x % 2 == 0;

        public class NoParams
        {
            [Benchmark]
            public int LinqAF() => Source.Count();

            [Benchmark(Baseline = true)]
            public int LINQ2Objects() => System.Linq.Enumerable.Count(Source);
        }

        public class OneParam
        {
            [Benchmark]
            public int LinqAF() => Source.Count(Predicate);

            [Benchmark(Baseline = true)]
            public int LINQ2Objects() => System.Linq.Enumerable.Count(Source, Predicate);
        }
    }
}
