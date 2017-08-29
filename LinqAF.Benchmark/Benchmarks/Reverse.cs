using BenchmarkDotNet.Attributes;
using System;

namespace LinqAF.Benchmark.Benchmarks
{
    public class Reverse
    {
        static string[] Source1 = new[] { "foo", "bar", "hello", "world" };
        static System.Collections.Generic.IEnumerable<string> Source2 = new UnsizedEnumerable();

        class UnsizedEnumerable : System.Collections.Generic.IEnumerable<string>
        {
            class Enumerator : System.Collections.Generic.IEnumerator<string>
            {
                public string Current { get; set; }

                object System.Collections.IEnumerator.Current => Current;

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

            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
        }

        public class UnderlyingSized
        {
            [Benchmark]
            public void LinqAF()
            {
                foreach(var item in Source1.Reverse())
                {
                    GC.KeepAlive(item);
                }
            }

            [Benchmark(Baseline = true)]
            public void LINQ2Objects()
            {
                foreach (var item in System.Linq.Enumerable.Reverse(Source1))
                {
                    GC.KeepAlive(item);
                }
            }
        }

        public class UnderlyingUnsized
        {
            [Benchmark]
            public void LinqAF()
            {
                foreach (var item in Source2.Reverse())
                {
                    GC.KeepAlive(item);
                }
            }

            [Benchmark(Baseline = true)]
            public void LINQ2Objects()
            {
                foreach (var item in System.Linq.Enumerable.Reverse(Source2))
                {
                    GC.KeepAlive(item);
                }
            }
        }
    }
}
