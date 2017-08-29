using BenchmarkDotNet.Attributes;
using System;
using System.Collections;

namespace LinqAF.Benchmark.Benchmarks
{
    public class ToArray
    {
        static string[] Source1 = new[] { "foo", "bar", "fizz" };
        static System.Collections.Generic.IEnumerable<string> Source2 = new UnsizedEnumerable();

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

        public class UnderlyingSized
        {
            [Benchmark]
            public void LinqAF()
            {
                foreach (var str in Source1.ToArray())
                {
                    System.GC.KeepAlive(str);
                }
            }

            [Benchmark(Baseline = true)]
            public void LINQ2Objects()
            {
                foreach (var str in System.Linq.Enumerable.ToArray(Source1))
                {
                    System.GC.KeepAlive(str);
                }
            }
        }

        public class UnderlyingUnsized
        {
            [Benchmark]
            public void LinqAF()
            {
                foreach (var str in Source2.ToArray())
                {
                    System.GC.KeepAlive(str);
                }
            }

            [Benchmark(Baseline = true)]
            public void LINQ2Objects()
            {
                foreach (var str in System.Linq.Enumerable.ToArray(Source2))
                {
                    System.GC.KeepAlive(str);
                }
            }
        }
    }
}
