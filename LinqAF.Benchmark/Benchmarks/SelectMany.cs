using BenchmarkDotNet.Attributes;
using System;

namespace LinqAF.Benchmark.Benchmarks
{
    public class SelectMany
    {
        static string[][] Source = new[] { new[] { "foo", "bar" }, new[] { "fizz", "buzz" }, new[] { "hello", "world" } };
        static Func<string[], string> CollectionSelector = arr => arr[0];
        static Func<string[], int, string> CollectionSelectorIndexed = (arr, ix) => arr[0];
        static Func<string[], char, string> ResultSelector = (a, b) => a[0];

        public class OneParamNoIndex
        {
            [Benchmark]
            public void LinqAF()
            {
                foreach (var item in Source.SelectMany(CollectionSelector))
                {
                }
            }

            [Benchmark(Baseline = true)]
            public void LINQ2Objects()
            {
                foreach (var item in System.Linq.Enumerable.SelectMany(Source, CollectionSelector))
                {
                }
            }
        }

        public class OneParamIndexed
        {
            [Benchmark]
            public void LinqAF()
            {
                foreach (var item in Source.SelectMany(CollectionSelectorIndexed))
                {
                }
            }

            [Benchmark(Baseline = true)]
            public void LINQ2Objects()
            {
                foreach (var item in System.Linq.Enumerable.SelectMany(Source, CollectionSelectorIndexed))
                {
                }
            }
        }

        public class TwoParamsNoIndex
        {
            [Benchmark]
            public void LinqAF()
            {
                foreach (var item in Source.SelectMany(CollectionSelector, ResultSelector))
                {
                }
            }

            [Benchmark(Baseline = true)]
            public void LINQ2Objects()
            {
                foreach (var item in System.Linq.Enumerable.SelectMany(Source, CollectionSelector, ResultSelector))
                {
                }
            }
        }

        public class TwoParamsIndexed
        {
            [Benchmark]
            public void LinqAF()
            {
                foreach (var item in Source.SelectMany(CollectionSelectorIndexed, ResultSelector))
                {
                }
            }

            [Benchmark(Baseline = true)]
            public void LINQ2Objects()
            {
                foreach (var item in System.Linq.Enumerable.SelectMany(Source, CollectionSelectorIndexed, ResultSelector))
                {
                }
            }
        }
    }
}
