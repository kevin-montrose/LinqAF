using BenchmarkDotNet.Attributes;

namespace LinqAF.Benchmark.Benchmarks
{
    public class OfType
    {
        static object[] Source = new object[] { "foo", 4, "bar", -22.2, "fizz" };

        public class NoParams
        {
            [Benchmark]
            public void LinqAF()
            {
                foreach(var str in Source.OfType<string>())
                {
                    System.GC.KeepAlive(str);
                }
            }

            [Benchmark(Baseline = true)]
            public void LINQ2Objects()
            {
                foreach (var str in System.Linq.Enumerable.OfType<string>(Source))
                {
                    System.GC.KeepAlive(str);
                }
            }
        }
    }
}
