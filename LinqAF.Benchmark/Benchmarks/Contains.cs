using BenchmarkDotNet.Attributes;
using LinqAF.Benchmark.Helpers;

namespace LinqAF.Benchmark.Benchmarks
{
    public class Contains
    {
        static int[] Source = new[] { 1, 2, 3 };
        static int Value1 = 2;
        static int Value2 = 4;
        static IntComparer Comparer = new IntComparer();

        public class OneParam
        {
            [Benchmark]
            public bool LinqAF() => Source.Contains(Value1) && !Source.Contains(Value2);

            [Benchmark(Baseline = true)]
            public bool LINQ2Objects() => System.Linq.Enumerable.Contains(Source, Value1) && !System.Linq.Enumerable.Contains(Source, Value2);
        }

        public class TwoParams
        {
            [Benchmark]
            public bool LinqAF() => Source.Contains(Value1, Comparer) && !Source.Contains(Value2, Comparer);

            [Benchmark(Baseline = true)]
            public bool LINQ2Objects() => System.Linq.Enumerable.Contains(Source, Value1, Comparer) && !System.Linq.Enumerable.Contains(Source, Value2, Comparer);
        }
    }
}
