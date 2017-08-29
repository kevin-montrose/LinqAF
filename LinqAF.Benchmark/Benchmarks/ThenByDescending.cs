using BenchmarkDotNet.Attributes;
using LinqAF.Benchmark.Helpers;
using System;

namespace LinqAF.Benchmark.Benchmarks
{
    public class ThenByDescending
    {
        static string[] Source = new[] { "foo", "world", "fuzz" };
        static Func<string, int> KeySelector1 = str => str.Length;
        static Func<string, char> KeySelector2 = str => str[0];
        static CharComparer Comparer = new CharComparer();

        public class OneParam
        {
            [Benchmark]
            public void LinqAF()
            {
                foreach(var item in Source.OrderBy(KeySelector1).ThenByDescending(KeySelector2))
                {
                    GC.KeepAlive(item);
                }
            }

            [Benchmark(Baseline = true)]
            public void LINQ2Objects()
            {
                foreach (var item in System.Linq.Enumerable.ThenByDescending(System.Linq.Enumerable.OrderBy(Source, KeySelector1), KeySelector2))
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
                foreach (var item in Source.OrderBy(KeySelector1).ThenByDescending(KeySelector2, Comparer))
                {
                    GC.KeepAlive(item);
                }
            }

            [Benchmark(Baseline = true)]
            public void LINQ2Objects()
            {
                foreach (var item in System.Linq.Enumerable.ThenByDescending(System.Linq.Enumerable.OrderBy(Source, KeySelector1), KeySelector2, Comparer))
                {
                    GC.KeepAlive(item);
                }
            }
        }
    }
}
