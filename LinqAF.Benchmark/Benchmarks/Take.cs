using BenchmarkDotNet.Attributes;
using System;

namespace LinqAF.Benchmark.Benchmarks
{
    public class Take
    {
        static string[] Source = new[] { "foo", "bar", "fizz" };

        public class OneParam
        {
            [Benchmark]
            public void LinqAF()
            {
                foreach(var item in Source.Take(1))
                {
                    GC.KeepAlive(item);
                }
            }

            [Benchmark(Baseline = true)]
            public void LINQ2Objects()
            {
                foreach (var item in System.Linq.Enumerable.Take(Source, 1))
                {
                    GC.KeepAlive(item);
                }
            }
        }
    }
}
