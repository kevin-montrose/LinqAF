using BenchmarkDotNet.Attributes;
using System;


namespace LinqAF.Benchmark.Benchmarks
{
    public class Crazy
    {
        public class Giant
        {
            [Benchmark]
            public void LinqAF()
            {
                var giant =
                    Enumerable.Range(0, 1000)
                        .Where(x => x % 2 == 0)
                        .Select(x => new[] { x, x * x })
                        .Concat(
                            Enumerable.Repeat(new int[] { 3, 4, 5 }, 6)
                        )
                        .SelectMany(y => y)
                        .Union(
                            Enumerable.Empty<int>().DefaultIfEmpty(999)
                        )
                        .Intersect(
                            Enumerable.Range(500, 10000)
                        )
                        .Except(new[] { 1001, 1002, 1003, 2001 })
                        .Join(Enumerable.Range(1, 10000), x => x, y => y, (a, b) => (a + b) / 2)
                        .GroupBy(x => x % 5)
                        .Select(x => new[] { x.First(), x.FirstOrDefault(), x.Last(), x.LastOrDefault() })
                        .SelectMany(x => x)
                        .GroupJoin(Enumerable.Range(1, 10000), x => x, y => y, (a, b) => b.FirstOrDefault(z => true))
                        .Take(90000)
                        .Skip(1)
                        .TakeWhile(x => true)
                        .SkipWhile(x => false)
                        .Reverse()
                        .OrderBy(x => x)
                        .ThenBy(y => y)
                        .Zip(Enumerable.Range(1, 100), (a, b) => a + b)
                        .Distinct();
                
                foreach (var i in giant)
                {
                    GC.KeepAlive(i);
                }

                foreach (var i in giant.AsEnumerable())
                {
                    GC.KeepAlive(i);
                }

                foreach (var i in giant.ToArray())
                {
                    GC.KeepAlive(i);
                }

                foreach (var i in giant.ToList())
                {
                    GC.KeepAlive(i);
                }

                foreach (var i in giant.ToDictionary(x => x, x => x.ToString()))
                {
                    GC.KeepAlive(i);
                }

                foreach (var i in giant.ToLookup(x => x, x => x.ToString()))
                {
                    GC.KeepAlive(i);
                }

                GC.KeepAlive(giant.Contains(5));
            }

            [Benchmark(Baseline = true)]
            public void LINQ2Objects()
            {
                var giant =
                    System.Linq.Enumerable.Distinct(
                        System.Linq.Enumerable.Zip(
                            System.Linq.Enumerable.OrderBy(
                                System.Linq.Enumerable.Reverse(
                                    System.Linq.Enumerable.SkipWhile(
                                        System.Linq.Enumerable.TakeWhile(
                                            System.Linq.Enumerable.Skip(
                                                System.Linq.Enumerable.Take(
                                                    System.Linq.Enumerable.GroupJoin(
                                                        System.Linq.Enumerable.SelectMany(
                                                            System.Linq.Enumerable.Select(
                                                                System.Linq.Enumerable.GroupBy(
                                                                    System.Linq.Enumerable.Join(
                                                                        System.Linq.Enumerable.Except(
                                                                            System.Linq.Enumerable.Intersect(
                                                                                System.Linq.Enumerable.Union(
                                                                                    System.Linq.Enumerable.SelectMany(
                                                                                        System.Linq.Enumerable.Concat(
                                                                                            System.Linq.Enumerable.Select(
                                                                                                System.Linq.Enumerable.Where(
                                                                                                    System.Linq.Enumerable.Range(0, 1000),
                                                                                                    x => x % 2 == 0
                                                                                                ),
                                                                                                x => new[] { x, x * x }
                                                                                            ),
                                                                                            System.Linq.Enumerable.Repeat(new int[] { 3, 4, 5 }, 6)
                                                                                        ),
                                                                                        y => y
                                                                                    ),
                                                                                    System.Linq.Enumerable.DefaultIfEmpty(
                                                                                        System.Linq.Enumerable.Empty<int>(),
                                                                                        999
                                                                                    )
                                                                                ),
                                                                                System.Linq.Enumerable.Range(500, 10000)
                                                                            ),
                                                                            new[] { 1001, 1002, 1003, 2001 }
                                                                        ),
                                                                        System.Linq.Enumerable.Range(1, 10000),
                                                                        x => x,
                                                                        y => y,
                                                                        (a, b) => (a + b) / 2
                                                                    ),
                                                                    x => x % 5
                                                                ),
                                                                x => new[] { System.Linq.Enumerable.First(x), System.Linq.Enumerable.FirstOrDefault(x), System.Linq.Enumerable.Last(x), System.Linq.Enumerable.LastOrDefault(x) }
                                                            ),
                                                            x => x
                                                        ),
                                                        System.Linq.Enumerable.Range(1, 10000),
                                                        x => x,
                                                        y => y,
                                                        (a, b) => System.Linq.Enumerable.FirstOrDefault(b, z => true)
                                                    ),
                                                    90000
                                                ),
                                                1
                                            ),
                                            x => true
                                        ),
                                        x => false
                                    )
                                ),
                                x => x
                            )
                            .CreateOrderedEnumerable(
                                y => y,
                                null,
                                false
                            ),
                            System.Linq.Enumerable.Range(1, 100),
                            (a, b) => a + b
                        )
                    );
                
                foreach (var i in giant)
                {
                    GC.KeepAlive(i);
                }

                foreach (var i in System.Linq.Enumerable.AsEnumerable(giant))
                {
                    GC.KeepAlive(i);
                }

                foreach (var i in System.Linq.Enumerable.ToArray(giant))
                {
                    GC.KeepAlive(i);
                }

                foreach (var i in System.Linq.Enumerable.ToList(giant))
                {
                    GC.KeepAlive(i);
                }

                foreach (var i in System.Linq.Enumerable.ToDictionary(giant, x => x, x => x.ToString()))
                {
                    GC.KeepAlive(i);
                }

                foreach (var i in System.Linq.Enumerable.ToLookup(giant, x => x, x => x.ToString()))
                {
                    GC.KeepAlive(i);
                }

                GC.KeepAlive(System.Linq.Enumerable.Contains(giant, 5));
            }
        }
    }
}