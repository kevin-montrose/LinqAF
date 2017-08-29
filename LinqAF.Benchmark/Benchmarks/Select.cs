using BenchmarkDotNet.Attributes;
using System;

namespace LinqAF.Benchmark.Benchmarks
{
    public class Select
    {
        static string[] Source = new[] { "foo", "bar", "fizz" };
        static Func<string, string> Selector = str => str;
        static Func<string, int, string> SelectorIndexes = (str, ix) => str;

        public class OneParamNoIndex
        {
            [Benchmark]
            public void LinqAF()
            {
                foreach(var item in Source.Select(Selector))
                {
                    GC.KeepAlive(item);
                }
            }

            [Benchmark(Baseline = true)]
            public void LINQ2Objects()
            {
                foreach (var item in System.Linq.Enumerable.Select(Source, Selector))
                {
                    GC.KeepAlive(item);
                }
            }
        }

        public class OneParamIndexed
        {
            [Benchmark]
            public void LinqAF()
            {
                foreach (var item in Source.Select(SelectorIndexes))
                {
                    GC.KeepAlive(item);
                }
            }

            [Benchmark(Baseline = true)]
            public void LINQ2Objects()
            {
                foreach (var item in System.Linq.Enumerable.Select(Source, SelectorIndexes))
                {
                    GC.KeepAlive(item);
                }
            }
        }
    }
}
