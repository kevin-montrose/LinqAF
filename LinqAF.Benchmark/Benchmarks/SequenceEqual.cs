using BenchmarkDotNet.Attributes;
using System;

namespace LinqAF.Benchmark.Benchmarks
{
    public class SequenceEqual
    {
        static string[] Source1 = new[] { "foo", "bar", "bazz" };
        static string[] Source2 = new[] { "foo", "bar", "fizz" };
        static StringComparer Comparer = StringComparer.InvariantCulture;

        public class OneParam
        {
            [Benchmark]
            public bool LinqAF() => Source1.SequenceEqual(Source2);
            [Benchmark(Baseline = true)]
            public bool LINQ2Objects() => System.Linq.Enumerable.SequenceEqual(Source1, Source2);
        }

        public class TwoParams
        {
            [Benchmark]
            public bool LinqAF() => Source1.SequenceEqual(Source2, Comparer);
            [Benchmark(Baseline = true)]
            public bool LINQ2Objects() => System.Linq.Enumerable.SequenceEqual(Source1, Source2, Comparer);
        }
    }
}
