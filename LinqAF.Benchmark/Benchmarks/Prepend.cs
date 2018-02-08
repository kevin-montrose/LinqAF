using LinqAF;
using BenchmarkDotNet.Attributes;
using System;

namespace LinqAF.Benchmark.Benchmarks
{
    public class Prepend
    {
        static string[] Source = new[] { "foo", "bar", "fizz" };
        static string Item = "buzz";

        public class NoParams
        {
            [Benchmark]
            public void LinqAF()
            {
                var e = Source.Prepend(Item);
                foreach(var i in e)
                {
                    GC.KeepAlive(i);
                }
            }

            [Benchmark(Baseline = true)]
            public void LINQ2Objects()
            {
                var e = System.Linq.Enumerable.Prepend(Source, Item);
                foreach (var i in e)
                {
                    GC.KeepAlive(i);
                }
            }
        }
    }
}
