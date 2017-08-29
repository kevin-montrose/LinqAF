using BenchmarkDotNet.Attributes;
using System;

namespace LinqAF.Benchmark.Benchmarks
{
    public class Empty
    {
        [Benchmark]
        public void LinqAF()
        {
            foreach(var item in Enumerable.Empty<string>())
            {
                GC.KeepAlive(item);
            }
        }

        [Benchmark(Baseline = true)]
        public void LINQ2Objects()
        {
            foreach (var item in System.Linq.Enumerable.Empty<string>())
            {
                GC.KeepAlive(item);
            }
        }
    }
}
