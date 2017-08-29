using BenchmarkDotNet.Attributes;
using System;

namespace LinqAF.Benchmark.Benchmarks
{
    public class Where
    {
        static string[] Source = new[] { "foo", "bar", "fizz", "buzz", "hello", "world" };
        static Func<string, bool> Predicate = str => str.Length % 2 == 0;
        static Func<string, int, bool> PredicateIndexed = (str, ix) => str.Length % 2 == 0;

        public class OneParamNoIndex
        {
            [Benchmark]
            public void LinqAF()
            {
                foreach(var item in Source.Where(Predicate))
                {
                    GC.KeepAlive(item);
                }
            }

            [Benchmark(Baseline = true)]
            public void LINQ2Objects()
            {
                foreach (var item in System.Linq.Enumerable.Where(Source, Predicate))
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
                foreach (var item in Source.Where(PredicateIndexed))
                {
                    GC.KeepAlive(item);
                }
            }

            [Benchmark(Baseline = true)]
            public void LINQ2Objects()
            {
                foreach (var item in System.Linq.Enumerable.Where(Source, PredicateIndexed))
                {
                    GC.KeepAlive(item);
                }
            }
        }
    }
}
