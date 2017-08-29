using BenchmarkDotNet.Attributes;
using System;

namespace LinqAF.Benchmark.Benchmarks
{
    public class LongCount
    {
        static int[] Source = new[] { 1, 2, 3 };
        static Func<int, bool> Predicate = x => x % 2 == 0;

        public class NoParams
        {
            [Benchmark]
            public long LinqAF() => Source.LongCount();

            [Benchmark(Baseline = true)]
            public long LINQ2Objects() => System.Linq.Enumerable.LongCount(Source);
        }

        public class OneParam
        {
            [Benchmark]
            public long LinqAF() => Source.LongCount(Predicate);

            [Benchmark(Baseline = true)]
            public long LINQ2Objects() => System.Linq.Enumerable.LongCount(Source, Predicate);
        }
    }
}
