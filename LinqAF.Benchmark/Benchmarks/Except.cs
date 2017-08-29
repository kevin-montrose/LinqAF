using BenchmarkDotNet.Attributes;
using System;

namespace LinqAF.Benchmark.Benchmarks
{
    public class Except
    {
        static string[] Source1 = new[] { "foo", "bar", "fizz" };
        static string[] Source2 = new[] { "bar", "bazz" };
        static StringComparer Comparer = StringComparer.InvariantCulture;

        public class OneParam
        {
            [Benchmark]
            public void LinqAF()
            {
                foreach(var item in Source1.Except(Source2))
                {
                    GC.KeepAlive(item);
                }
            }

            [Benchmark(Baseline = true)]
            public void LINQ2Objects()
            {
                foreach (var item in System.Linq.Enumerable.Except(Source1, Source2))
                {
                    GC.KeepAlive(item);
                }
            }
        }

        public class TwoParams
        {
            [Benchmark]
            public void LinqAF()
            {
                foreach (var item in Source1.Except(Source2, Comparer))
                {
                    GC.KeepAlive(item);
                }
            }

            [Benchmark(Baseline = true)]
            public void LINQ2Objects()
            {
                foreach (var item in System.Linq.Enumerable.Except(Source1, Source2, Comparer))
                {
                    GC.KeepAlive(item);
                }
            }
        }
    }
}
