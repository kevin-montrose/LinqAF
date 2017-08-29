using BenchmarkDotNet.Attributes;
using LinqAF.Benchmark.Helpers;
using System;

namespace LinqAF.Benchmark.Benchmarks
{
    public class OrderByDescending
    {
        static string[] Source = new[] { "foo", "world", "fuzz" };
        static Func<string, int> KeySelector = str => str.Length;
        static IntComparer Comparer = new IntComparer();

        public class OneParam
        {
            [Benchmark]
            public void LinqAF()
            {
                foreach(var item in Source.OrderByDescending(KeySelector))
                {
                    GC.KeepAlive(item);
                }
            }

            [Benchmark(Baseline = true)]
            public void LINQ2Objects()
            {
                foreach (var item in System.Linq.Enumerable.OrderByDescending(Source, KeySelector))
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
                foreach (var item in Source.OrderByDescending(KeySelector, Comparer))
                {
                    GC.KeepAlive(item);
                }
            }

            [Benchmark(Baseline = true)]
            public void LINQ2Objects()
            {
                foreach (var item in System.Linq.Enumerable.OrderByDescending(Source, KeySelector, Comparer))
                {
                    GC.KeepAlive(item);
                }
            }
        }
    }
}
