using BenchmarkDotNet.Attributes;
using System;

namespace LinqAF.Benchmark.Benchmarks
{
    public class DefaultIfEmpty
    {
        static string[] Source1 = new[] { "foo", "bar" };
        static string[] Source2 = new string[0];
        static string Value = "fizz";

        public class NoParams
        {
            [Benchmark]
            public void LinqAF()
            {
                foreach(var str in Source1.DefaultIfEmpty())
                {
                    GC.KeepAlive(str);
                }

                foreach (var str in Source2.DefaultIfEmpty())
                {
                    GC.KeepAlive(str);
                }
            }

            [Benchmark(Baseline = true)]
            public void LINQ2Objects()
            {
                foreach (var str in System.Linq.Enumerable.DefaultIfEmpty(Source1))
                {
                    GC.KeepAlive(str);
                }

                foreach (var str in System.Linq.Enumerable.DefaultIfEmpty(Source2))
                {
                    GC.KeepAlive(str);
                }
            }
        }

        public class OneParam
        {
            [Benchmark]
            public void LinqAF()
            {
                foreach (var str in Source1.DefaultIfEmpty(Value))
                {
                    GC.KeepAlive(str);
                }

                foreach (var str in Source2.DefaultIfEmpty(Value))
                {
                    GC.KeepAlive(str);
                }
            }

            [Benchmark(Baseline = true)]
            public void LINQ2Objects()
            {
                foreach (var str in System.Linq.Enumerable.DefaultIfEmpty(Source1, Value))
                {
                    GC.KeepAlive(str);
                }

                foreach (var str in System.Linq.Enumerable.DefaultIfEmpty(Source2, Value))
                {
                    GC.KeepAlive(str);
                }
            }
        }
    }
}
