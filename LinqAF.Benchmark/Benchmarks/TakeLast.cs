﻿//using BenchmarkDotNet.Attributes;
//using System;

//namespace LinqAF.Benchmark.Benchmarks
//{
//    public class TakeLast
//    {
//        static string[] Source = new[] { "foo", "bar", "fizz" };

//        public class OneParam
//        {
//            [Benchmark]
//            public void LinqAF()
//            {
//                foreach (var item in Source.TakeLast(1))
//                {
//                    GC.KeepAlive(item);
//                }
//            }

//            [Benchmark(Baseline = true)]
//            public void LINQ2Objects()
//            {
//                foreach (var item in System.Linq.Enumerable.TakeLast(Source, 1))
//                {
//                    GC.KeepAlive(item);
//                }
//            }
//        }
//    }
//}
