using BenchmarkDotNet.Attributes;
using System;

namespace LinqAF.Benchmark.Benchmarks
{
    public class Zip
    {
        static string[] Source1 = new[] { "foo", "bar", "world" };
        static string[] Source2 = new[] { "fizz", "buzz", "bazz" };
        static Func<string, string, string> ResultSelector = (a, b) => a;

        public class ThreeParams
        {
            [Benchmark]
            public void LinqAF()
            {
                foreach(var item in Source1.Zip(Source2, ResultSelector))
                {
                    GC.KeepAlive(item);
                }
            }

            [Benchmark(Baseline = true)]
            public void LINQ2Objects()
            {
                foreach (var item in System.Linq.Enumerable.Zip(Source1, Source2, ResultSelector))
                {
                    GC.KeepAlive(item);
                }
            }
        }
    }
}
