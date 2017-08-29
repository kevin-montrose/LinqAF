using BenchmarkDotNet.Attributes;
using LinqAF.Benchmark.Helpers;
using System;

namespace LinqAF.Benchmark.Benchmarks
{
    public class ToLookup
    {
        static string[] Source = 
            new[] {
                "foo",
                "bar",
                "fizz",
                "buzz",
                "hello",
                "world",
                "make it so",
                "engage",
                "indeed",
                "equines",
                "",
                "escape",
                "enter",
                "c#",
                ".net",
                "correct battery horse staple",
                "a8df1971-c7da-4b26-940d-272030e75768",
                "a8df1971-c7da-4b26-940d-272030e75768 a8df1971-c7da-4b26-940d-272030e75768",
                "a8df1971-c7da-4b26-940d-272030e75768 a8df1971-c7da-4b26-940d-272030e75768 a8df1971-c7da-4b26-940d-272030e75768",
                "a8df1971-c7da-4b26-940d-272030e75768 a8df1971-c7da-4b26-940d-272030e75768 a8df1971-c7da-4b26-940d-272030e75768 a8df1971-c7da-4b26-940d-272030e75768"
            }; // make it long enough that LinqAF will re-allocate at least once
        static Func<string, int> _KeySelector = str => str.Length;
        static Func<string, string> ElementSelector = str => str;
        static IntComparer Comparer = new IntComparer();

        public class KeySelector
        {
            [Benchmark]
            public void LinqAF()
            {
                var e = Source.ToLookup(_KeySelector);
                foreach(var grp in e)
                {
                    foreach(var item in grp)
                    {
                        GC.KeepAlive(item);
                    }
                }
            }

            [Benchmark(Baseline = true)]
            public void LINQ2Objects()
            {
                var e = System.Linq.Enumerable.ToLookup(Source, _KeySelector);
                foreach (var grp in e)
                {
                    foreach (var item in grp)
                    {
                        GC.KeepAlive(item);
                    }
                }
            }
        }

        public class KeySelector_Comparer
        {
            [Benchmark]
            public void LinqAF()
            {
                var e = Source.ToLookup(_KeySelector, Comparer);
                foreach (var grp in e)
                {
                    foreach (var item in grp)
                    {
                        GC.KeepAlive(item);
                    }
                }
            }

            [Benchmark(Baseline = true)]
            public void LINQ2Objects()
            {
                var e = System.Linq.Enumerable.ToLookup(Source, _KeySelector, Comparer);
                foreach (var grp in e)
                {
                    foreach (var item in grp)
                    {
                        GC.KeepAlive(item);
                    }
                }
            }
        }

        public class KeySelector_ElementSelector
        {
            [Benchmark]
            public void LinqAF()
            {
                var e = Source.ToLookup(_KeySelector, ElementSelector);
                foreach (var grp in e)
                {
                    foreach (var item in grp)
                    {
                        GC.KeepAlive(item);
                    }
                }
            }

            [Benchmark(Baseline = true)]
            public void LINQ2Objects()
            {
                var e = System.Linq.Enumerable.ToLookup(Source, _KeySelector, ElementSelector);
                foreach (var grp in e)
                {
                    foreach (var item in grp)
                    {
                        GC.KeepAlive(item);
                    }
                }
            }
        }

        public class KeySelector_ElementSelector_Comparer
        {
            [Benchmark]
            public void LinqAF()
            {
                var e = Source.ToLookup(_KeySelector, ElementSelector, Comparer);
                foreach (var grp in e)
                {
                    foreach (var item in grp)
                    {
                        GC.KeepAlive(item);
                    }
                }
            }

            [Benchmark(Baseline = true)]
            public void LINQ2Objects()
            {
                var e = System.Linq.Enumerable.ToLookup(Source, _KeySelector, ElementSelector, Comparer);
                foreach (var grp in e)
                {
                    foreach (var item in grp)
                    {
                        GC.KeepAlive(item);
                    }
                }
            }
        }
    }
}
