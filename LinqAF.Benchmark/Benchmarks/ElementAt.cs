using BenchmarkDotNet.Attributes;

namespace LinqAF.Benchmark.Benchmarks
{
    public class ElementAt
    {
        static int[] Source = new[] { 1, 2, 3 };

        public class OneParam
        {
            [Benchmark]
            public int LinqAF() => Source.ElementAt(1);
            [Benchmark(Baseline = true)]
            public int LINQ2Objects() => System.Linq.Enumerable.ElementAt(Source, 1);
        }
    }
}
