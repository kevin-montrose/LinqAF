using BenchmarkDotNet.Attributes;

namespace LinqAF.Benchmark.Benchmarks
{
    public class Concat
    {
        static string[] Source1 = new[] { "foo", "bar", "fizz" };
        static string[] Source2 = new[] { "buzz", "bazz" };

        public class NoParams
        {
            [Benchmark]
            public void LinqAF()
            {
                foreach(var str in Source1.Concat(Source2))
                {
                    System.GC.KeepAlive(str);
                }
            }

            [Benchmark(Baseline = true)]
            public void LINQ2Objects()
            {
                foreach (var str in System.Linq.Enumerable.Concat(Source1, Source2))
                {
                    System.GC.KeepAlive(str);
                }
            }
        }
    }
}
