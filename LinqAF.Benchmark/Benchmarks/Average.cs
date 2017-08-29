using BenchmarkDotNet.Attributes;
using System;

namespace LinqAF.Benchmark.Benchmarks
{
    public class Average
    {
        static int[] Source1 = new[] { 1, 2, 3, 4 };
        static int?[] Source2 = new[] { 1, default(int?), 2, default(int?), 3 };
        static string[] Source3 = new[] { "foo", "bar", "fizz", "buzz" };
        static Func<string, int> Func1 = str => str.Length;
        static Func<string, int?> Func2 = str => str.Length % 2 == 0 ? (int?)null : str.Length;

        public class NonNull
        {
            [Benchmark]
            public double LinqAF() => Source1.Average();
            [Benchmark(Baseline = true)]
            public double LINQ2Objects() => System.Linq.Enumerable.Average(Source1);
        }

        public class Nullable
        {
            [Benchmark]
            public double? LinqAF() => Source2.Average();
            [Benchmark(Baseline = true)]
            public double? LINQ2Objects() => System.Linq.Enumerable.Average(Source2);
        }

        public class ProjectionNonNull
        {
            [Benchmark]
            public double LinqAF() => Source3.Average(Func1);
            [Benchmark(Baseline = true)]
            public double LINQ2Objects() => System.Linq.Enumerable.Average(Source3, Func1);
        }

        public class ProjectionNullable
        {
            [Benchmark]
            public double? LinqAF() => Source3.Average(Func2);
            [Benchmark(Baseline = true)]
            public double? LINQ2Objects() => System.Linq.Enumerable.Average(Source3, Func2);
        }
    }
}
