using BenchmarkDotNet.Attributes;
using System;

namespace LinqAF.Benchmark.Benchmarks
{
    public class LastOrDefault
    {
        static int[] Source = new[] { 1, 2, 3 };
        static Func<int, bool> Predicate = x => x % 2 == 0;

        public class NoParams
        {
            [Benchmark]
            public int LinqAF() => Source.LastOrDefault();
            [Benchmark(Baseline = true)]
            public int LINQ2Objects() => System.Linq.Enumerable.LastOrDefault(Source);
        }

        public class OneParam
        {
            [Benchmark]
            public int LinqAF() => Source.LastOrDefault(Predicate);
            [Benchmark(Baseline = true)]
            public int LINQ2Objects() => System.Linq.Enumerable.LastOrDefault(Source, Predicate);
        }
    }
}
