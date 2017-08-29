using BenchmarkDotNet.Attributes;

namespace LinqAF.Benchmark.Benchmarks
{
    public class Cast
    {
        static object[] Source1 = new[] { "foo", "bar", "fizz" };
        static System.Collections.IEnumerable Source2 = new UntypedEnumerable();

        class UntypedEnumerable : System.Collections.IEnumerable
        {
            class Enumerator : System.Collections.IEnumerator
            {
                public object Current { get; set; }

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
            }

            public System.Collections.IEnumerator GetEnumerator()
            {
                return new Enumerator();
            }
        }

        public class UnderlyingTyped
        {
            [Benchmark]
            public void LinqAF()
            {
                foreach(var str in Source1.Cast<string>())
                {
                    System.GC.KeepAlive(str);
                }
            }

            [Benchmark(Baseline = true)]
            public void LINQ2Objects()
            {
                foreach (var str in System.Linq.Enumerable.Cast<string>(Source1))
                {
                    System.GC.KeepAlive(str);
                }
            }
        }

        public class UnderlyingUntyped
        {
            [Benchmark]
            public void LinqAF()
            {
                foreach (var str in Source2.Cast<string>())
                {
                    System.GC.KeepAlive(str);
                }
            }

            [Benchmark(Baseline = true)]
            public void LINQ2Objects()
            {
                foreach (var str in System.Linq.Enumerable.Cast<string>(Source2))
                {
                    System.GC.KeepAlive(str);
                }
            }
        }
    }
}
