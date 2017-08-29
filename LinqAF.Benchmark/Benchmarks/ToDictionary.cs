using BenchmarkDotNet.Attributes;
using System;
using System.Collections;

namespace LinqAF.Benchmark.Benchmarks
{
    public class ToDictionary
    {
        static string[] Source1 = new[] { "foo", "bar", "fizz" };
        static System.Collections.Generic.IEnumerable<string> Source2 = new UnsizedEnumerable();
        static Func<string, string> Func1 = x => x;

        class UnsizedEnumerable : System.Collections.Generic.IEnumerable<string>
        {
            class Enumerator : System.Collections.Generic.IEnumerator<string>
            {
                public string Current { get; set; }

                object IEnumerator.Current => Current;

                public int Index = 0;

                public bool MoveNext()
                {
                    if (Index >= Source1.Length) return false;

                    Current = Source1[Index];
                    Index++;
                    return true;
                }

                public void Reset()
                {
                    Index = 0;
                }

                public void Dispose() { }
            }

            public System.Collections.Generic.IEnumerator<string> GetEnumerator()
            {
                return new Enumerator();
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        public class OneParamSized
        {
            [Benchmark]
            public void LinqAF()
            {
                Source1.ToDictionary(Func1);
            }

            [Benchmark(Baseline = true)]
            public void LINQ2Objects()
            {
                System.Linq.Enumerable.ToDictionary(Source1, Func1);
            }
        }

        public class OneParamSized_Comparable
        {
            [Benchmark]
            public void LinqAF()
            {
                Source1.ToDictionary(Func1, StringComparer.InvariantCultureIgnoreCase);
            }

            [Benchmark(Baseline = true)]
            public void LINQ2Objects()
            {
                System.Linq.Enumerable.ToDictionary(Source1, Func1, StringComparer.InvariantCultureIgnoreCase);
            }
        }

        public class OneParamUnsized
        {
            [Benchmark]
            public void LinqAF()
            {
                Source2.ToDictionary(Func1);
            }

            [Benchmark(Baseline = true)]
            public void LINQ2Objects()
            {
                System.Linq.Enumerable.ToDictionary(Source2, Func1);
            }
        }

        public class OneParamUnsized_Comparable
        {
            [Benchmark]
            public void LinqAF()
            {
                Source2.ToDictionary(Func1, StringComparer.InvariantCultureIgnoreCase);
            }

            [Benchmark(Baseline = true)]
            public void LINQ2Objects()
            {
                System.Linq.Enumerable.ToDictionary(Source2, Func1, StringComparer.InvariantCultureIgnoreCase);
            }
        }

        public class TwoParamSized
        {
            [Benchmark]
            public void LinqAF()
            {
                Source1.ToDictionary(Func1, Func1);
            }

            [Benchmark(Baseline = true)]
            public void LINQ2Objects()
            {
                System.Linq.Enumerable.ToDictionary(Source1, Func1, Func1);
            }
        }

        public class TwoParamSized_Comparable
        {
            [Benchmark]
            public void LinqAF()
            {
                Source1.ToDictionary(Func1, Func1, StringComparer.InvariantCultureIgnoreCase);
            }

            [Benchmark(Baseline = true)]
            public void LINQ2Objects()
            {
                System.Linq.Enumerable.ToDictionary(Source1, Func1, Func1, StringComparer.InvariantCultureIgnoreCase);
            }
        }

        public class TwoParamUnsized
        {
            [Benchmark]
            public void LinqAF()
            {
                Source2.ToDictionary(Func1, Func1);
            }

            [Benchmark(Baseline = true)]
            public void LINQ2Objects()
            {
                System.Linq.Enumerable.ToDictionary(Source2, Func1, Func1);
            }
        }

        public class TwoParamUnsized_Comparable
        {
            [Benchmark]
            public void LinqAF()
            {
                Source2.ToDictionary(Func1, Func1, StringComparer.InvariantCultureIgnoreCase);
            }

            [Benchmark(Baseline = true)]
            public void LINQ2Objects()
            {
                System.Linq.Enumerable.ToDictionary(Source2, Func1, Func1, StringComparer.InvariantCultureIgnoreCase);
            }
        }
    }
}
