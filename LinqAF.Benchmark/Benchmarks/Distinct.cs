using BenchmarkDotNet.Attributes;
using System;

namespace LinqAF.Benchmark.Benchmarks
{
    public class Distinct
    {
        static string[] Source = new[] { "foo", "bar", "foo", "fizz", "buzz"  };
        static StringComparer Comparer = StringComparer.InvariantCulture;

        public class NoParams
        {
            [Benchmark]
            public void LinqAF()
            {
                foreach(var item in Source.Distinct())
                {
                    GC.KeepAlive(item);
                }
            }

            [Benchmark(Baseline = true)]
            public void LINQ2Objects()
            {
                foreach (var item in System.Linq.Enumerable.Distinct(Source))
                {
                    GC.KeepAlive(item);
                }
            }
        }

        public class OneParam
        {
            [Benchmark]
            public void LinqAF()
            {
                foreach (var item in Source.Distinct(Comparer))
                {
                    GC.KeepAlive(item);
                }
            }

            [Benchmark(Baseline = true)]
            public void LINQ2Objects()
            {
                foreach (var item in System.Linq.Enumerable.Distinct(Source, Comparer))
                {
                    GC.KeepAlive(item);
                }
            }
        }
    }
}
