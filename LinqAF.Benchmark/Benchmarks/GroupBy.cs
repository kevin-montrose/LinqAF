using BenchmarkDotNet.Attributes;
using LinqAF.Benchmark.Helpers;
using System;

namespace LinqAF.Benchmark.Benchmarks
{
    public class GroupBy
    {
        static string[] Source = new[] { "foo", "bar", "fizz", "buzz", "hello", "world" };
        static Func<string, int> _KeySelector = str => str.Length;
        static Func<string, string> ElementSelector = str => str;
        static IntComparer Comparer = new IntComparer();

        public class KeySelector
        {
            [Benchmark]
            public void LinqAF()
            {
                foreach(var grp in Source.GroupBy(_KeySelector))
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
                foreach (var grp in System.Linq.Enumerable.GroupBy(Source, _KeySelector))
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
                foreach (var grp in Source.GroupBy(_KeySelector, Comparer))
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
                foreach (var grp in System.Linq.Enumerable.GroupBy(Source, _KeySelector, Comparer))
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
                foreach (var grp in Source.GroupBy(_KeySelector, ElementSelector))
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
                foreach (var grp in System.Linq.Enumerable.GroupBy(Source, _KeySelector, ElementSelector))
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
                foreach (var grp in Source.GroupBy(_KeySelector, ElementSelector, Comparer))
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
                foreach (var grp in System.Linq.Enumerable.GroupBy(Source, _KeySelector, ElementSelector, Comparer))
                {
                    foreach (var item in grp)
                    {
                        GC.KeepAlive(item);
                    }
                }
            }
        }

        public class KeySelector_ResultSelector
        {
            [Benchmark]
            public void LinqAF()
            {
                foreach (var grp in Source.GroupBy(_KeySelector, (x, g) => x))
                {
                }
            }

            [Benchmark(Baseline = true)]
            public void LINQ2Objects()
            {
                foreach (var grp in System.Linq.Enumerable.GroupBy(Source, _KeySelector, (x, g) => x))
                {
                }
            }
        }

        public class KeySelector_ResultSelector_Comparer
        {
            [Benchmark]
            public void LinqAF()
            {
                foreach (var grp in Source.GroupBy(_KeySelector, (x, g) => x, Comparer))
                {
                }
            }

            [Benchmark(Baseline = true)]
            public void LINQ2Objects()
            {
                foreach (var grp in System.Linq.Enumerable.GroupBy(Source, _KeySelector, (x, g) => x, Comparer))
                {
                }
            }
        }

        public class KeySelector_ElementSelector_ResultSelector
        {
            [Benchmark]
            public void LinqAF()
            {
                foreach (var grp in Source.GroupBy(_KeySelector, ElementSelector, (x, g) => x))
                {
                }
            }

            [Benchmark(Baseline = true)]
            public void LINQ2Objects()
            {
                foreach (var grp in System.Linq.Enumerable.GroupBy(Source, _KeySelector, ElementSelector, (x, g) => x))
                {
                }
            }
        }

        public class KeySelector_ElementSelector_ResultSelector_Comparer
        {
            [Benchmark]
            public void LinqAF()
            {
                foreach (var grp in Source.GroupBy(_KeySelector, ElementSelector, (x, g) => x, Comparer))
                {
                }
            }

            [Benchmark(Baseline = true)]
            public void LINQ2Objects()
            {
                foreach (var grp in System.Linq.Enumerable.GroupBy(Source, _KeySelector, ElementSelector, (x, g) => x, Comparer))
                {
                }
            }
        }
    }
}
