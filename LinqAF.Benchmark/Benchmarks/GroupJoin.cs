using BenchmarkDotNet.Attributes;
using LinqAF.Benchmark.Helpers;
using System;

namespace LinqAF.Benchmark.Benchmarks
{
    public class GroupJoin
    {
        static string[] Source1 = new[] { "foo", "fizz", "hello" };
        static string[] Source2 = new[] { "bar", "fuzz", "world" };
        static Func<string, int> KeySelector = str => str.Length;
        static IntComparer Comparer = new IntComparer();

        public class FourParams
        {
            [Benchmark]
            public void LinqAF()
            {
                foreach(var grp in Source1.GroupJoin(Source2, KeySelector, KeySelector, (x, grp) => x))
                {
                }
            }

            [Benchmark(Baseline = true)]
            public void LINQ2Objects()
            {
                foreach (var grp in System.Linq.Enumerable.GroupJoin(Source1, Source2, KeySelector, KeySelector, (x, grp) => x))
                {
                }
            }
        }

        public class FiveParams
        {
            [Benchmark]
            public void LinqAF()
            {
                foreach (var grp in Source1.GroupJoin(Source2, KeySelector, KeySelector, (x, grp) => x, Comparer))
                {
                }
            }

            [Benchmark(Baseline = true)]
            public void LINQ2Objects()
            {
                foreach (var grp in System.Linq.Enumerable.GroupJoin(Source1, Source2, KeySelector, KeySelector, (x, grp) => x, Comparer))
                {
                }
            }
        }
    }
}
