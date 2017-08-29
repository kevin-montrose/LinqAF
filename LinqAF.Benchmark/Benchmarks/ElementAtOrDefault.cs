using BenchmarkDotNet.Attributes;

namespace LinqAF.Benchmark.Benchmarks
{
    public class ElementAtOrDefault
    {
        static int[] Source = new[] { 1, 2, 3 };

        public class OneParam
        {
            [Benchmark]
            public int LinqAF() => Source.ElementAtOrDefault(1);
            [Benchmark(Baseline = true)]
            public int LINQ2Objects() => System.Linq.Enumerable.ElementAtOrDefault(Source, 1);
        }
    }
}
