using BenchmarkDotNet.Attributes;
using System;

namespace LinqAF.Benchmark.Benchmarks
{
    public class Repeat
    {
        [Benchmark]
        public void LinqAF()
        {
            foreach(var item in Enumerable.Repeat("foo", 15))
            {
                GC.KeepAlive(item);
            }
        }

        [Benchmark(Baseline = true)]
        public void LINQ2Objects()
        {
            foreach (var item in System.Linq.Enumerable.Range(0, 10))
            {
                GC.KeepAlive(item);
            }
        }
    }
}
