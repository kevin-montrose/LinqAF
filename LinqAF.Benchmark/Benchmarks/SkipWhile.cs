using BenchmarkDotNet.Attributes;
using System;

namespace LinqAF.Benchmark.Benchmarks
{
    public class SkipWhile
    {
        static string[] Source = new[] { "foo", "bar", "fizz" };
        static Func<string, bool> Predicate = str => str.Length == 3;
        static Func<string, int, bool> PredicateIndexed = (str, ix) => str.Length == 3;

        public class OneParamNoIndex
        {
            [Benchmark]
            public void LinqAF()
            {
                foreach(var item in Source.SkipWhile(Predicate))
                {
                    GC.KeepAlive(item);
                }
            }

            [Benchmark(Baseline = true)]
            public void LINQ2Objects()
            {
                foreach (var item in System.Linq.Enumerable.SkipWhile(Source, Predicate))
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
                foreach (var item in Source.SkipWhile(PredicateIndexed))
                {
                    GC.KeepAlive(item);
                }
            }

            [Benchmark(Baseline = true)]
            public void LINQ2Objects()
            {
                foreach (var item in System.Linq.Enumerable.SkipWhile(Source, PredicateIndexed))
                {
                    GC.KeepAlive(item);
                }
            }
        }
    }
}
