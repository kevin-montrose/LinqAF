using BenchmarkDotNet.Attributes;
using LinqAF.Benchmark.Helpers;
using System;

namespace LinqAF.Benchmark.Benchmarks
{
    public class Join
    {
        static string[] Source1 = new[] { "foo", "fizz", "hello" };
        static string[] Source2 = new[] { "bar", "fuzz", "world" };
        static Func<string, int> KeySelector = str => str.Length;
        static Func<string, string, string> ResultSelector = (a, b) => a;
        static IntComparer Comparer = new IntComparer();

        public class FourParams
        {
            [Benchmark]
            public void LinqAF()
            {
                foreach(var item in Source1.Join(Source2, KeySelector, KeySelector, ResultSelector))
                {
                    GC.KeepAlive(item);
                }
            }

            [Benchmark(Baseline = true)]
            public void LINQ2Objects()
            {
                foreach (var item in System.Linq.Enumerable.Join(Source1, Source2, KeySelector, KeySelector, ResultSelector))
                {
                    GC.KeepAlive(item);
                }
            }
        }

        public class FiveParams
        {
            [Benchmark]
            public void LinqAF()
            {
                foreach (var item in Source1.Join(Source2, KeySelector, KeySelector, ResultSelector, Comparer))
                {
                    GC.KeepAlive(item);
                }
            }

            [Benchmark(Baseline = true)]
            public void LINQ2Objects()
            {
                foreach (var item in System.Linq.Enumerable.Join(Source1, Source2, KeySelector, KeySelector, ResultSelector, Comparer))
                {
                    GC.KeepAlive(item);
                }
            }
        }
    }
}
