using BenchmarkDotNet.Attributes;
using System;

namespace LinqAF.Benchmark.Benchmarks
{
    public class SingleOrDefault
    {
        static int[] Source = new[] { 2 };
        static Func<int, bool> Predicate = x => x % 2 == 0;

        public class NoParams
        {
            [Benchmark]
            public int LinqAF() => Source.SingleOrDefault();
            [Benchmark(Baseline = true)]
            public int LINQ2Objects() => System.Linq.Enumerable.SingleOrDefault(Source);
        }

        public class OneParam
        {
            [Benchmark]
            public int LinqAF() => Source.SingleOrDefault(Predicate);
            [Benchmark(Baseline = true)]
            public int LINQ2Objects() => System.Linq.Enumerable.SingleOrDefault(Source, Predicate);
        }
    }
}
