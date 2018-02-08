
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Loggers;
using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Running;
using LinqAF.Benchmark.Benchmarks;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace LinqAF.Benchmark
{
    class Program
    {
        static void Main(string[] args)
        {

            var outputDir = args.Length > 0 ? args[0] : null;

            if (outputDir != null)
            {
                if (!Directory.Exists(outputDir))
                {
                    Directory.CreateDirectory(outputDir);
                }
            }


            var logFile = outputDir != null ? Path.Combine(outputDir, "log.txt") : null;
            using (var fs = logFile != null ? File.Create(logFile) : null)
            using (var writer = fs != null ? new StreamWriter(fs) : null)
            {
                Console.SetOut(new ForkWriter(Console.Out, writer));

                var slower = new List<string>();
                var allocatedMore = new List<string>();

                Run(outputDir, slower, allocatedMore, new[] { typeof(Aggregate.OneParam), typeof(Aggregate.TwoParams), typeof(Aggregate.ThreeParams) });
                Run(outputDir, slower, allocatedMore, new[] { typeof(All.OneParam) });
                Run(outputDir, slower, allocatedMore, new[] { typeof(Any.NoParams), typeof(Any.OneParam) });
                Run(outputDir, slower, allocatedMore, new[] { typeof(Append.NoParams) });
                Run(outputDir, slower, allocatedMore, new[] { typeof(Average.NonNull), typeof(Average.Nullable), typeof(Average.ProjectionNonNull), typeof(Average.ProjectionNullable) });
                Run(outputDir, slower, allocatedMore, new[] { typeof(Cast.UnderlyingTyped), typeof(Cast.UnderlyingUntyped) });
                Run(outputDir, slower, allocatedMore, new[] { typeof(Concat.NoParams) });
                Run(outputDir, slower, allocatedMore, new[] { typeof(Contains.OneParam), typeof(Contains.TwoParams) });
                Run(outputDir, slower, allocatedMore, new[] { typeof(Count.NoParams), typeof(Count.OneParam) });
                Run(outputDir, slower, allocatedMore, new[] { typeof(DefaultIfEmpty.NoParams), typeof(DefaultIfEmpty.OneParam) });
                Run(outputDir, slower, allocatedMore, new[] { typeof(Distinct.NoParams), typeof(Distinct.OneParam) });
                Run(outputDir, slower, allocatedMore, new[] { typeof(ElementAt.OneParam) });
                Run(outputDir, slower, allocatedMore, new[] { typeof(ElementAtOrDefault.OneParam) });
                Run(outputDir, slower, allocatedMore, new[] { typeof(Empty) });
                Run(outputDir, slower, allocatedMore, new[] { typeof(Except.OneParam), typeof(Except.TwoParams) });
                Run(outputDir, slower, allocatedMore, new[] { typeof(First.NoParams), typeof(First.OneParam) });
                Run(outputDir, slower, allocatedMore, new[] { typeof(FirstOrDefault.NoParams), typeof(FirstOrDefault.OneParam) });
                Run(outputDir, slower, allocatedMore, new[] { typeof(GroupBy.KeySelector), typeof(GroupBy.KeySelector_Comparer), typeof(GroupBy.KeySelector_ElementSelector), typeof(GroupBy.KeySelector_ElementSelector_Comparer), typeof(GroupBy.KeySelector_ElementSelector_ResultSelector), typeof(GroupBy.KeySelector_ElementSelector_ResultSelector_Comparer), typeof(GroupBy.KeySelector_ResultSelector), typeof(GroupBy.KeySelector_ResultSelector_Comparer) });
                Run(outputDir, slower, allocatedMore, new[] { typeof(GroupJoin.FourParams), typeof(GroupJoin.FiveParams) });
                Run(outputDir, slower, allocatedMore, new[] { typeof(Intersect.OneParam), typeof(Intersect.TwoParams) });
                Run(outputDir, slower, allocatedMore, new[] { typeof(Join.FourParams), typeof(Join.FiveParams) });
                Run(outputDir, slower, allocatedMore, new[] { typeof(Last.NoParams), typeof(Last.OneParam) });
                Run(outputDir, slower, allocatedMore, new[] { typeof(LastOrDefault.NoParams), typeof(LastOrDefault.OneParam) });
                Run(outputDir, slower, allocatedMore, new[] { typeof(LongCount.NoParams), typeof(LongCount.OneParam) });
                Run(outputDir, slower, allocatedMore, new[] { typeof(Max.NonNull), typeof(Max.Nullable), typeof(Max.Projection) });
                Run(outputDir, slower, allocatedMore, new[] { typeof(Min.NonNull), typeof(Min.Nullable), typeof(Min.Projection) });
                Run(outputDir, slower, allocatedMore, new[] { typeof(OfType.NoParams) });
                Run(outputDir, slower, allocatedMore, new[] { typeof(OrderBy.OneParam), typeof(OrderBy.TwoParams) });
                Run(outputDir, slower, allocatedMore, new[] { typeof(OrderByDescending.OneParam), typeof(OrderByDescending.TwoParams) });
                Run(outputDir, slower, allocatedMore, new[] { typeof(Prepend.NoParams) });
                Run(outputDir, slower, allocatedMore, new[] { typeof(Range) });
                Run(outputDir, slower, allocatedMore, new[] { typeof(Repeat) });
                Run(outputDir, slower, allocatedMore, new[] { typeof(Reverse.UnderlyingSized), typeof(Reverse.UnderlyingUnsized) });
                Run(outputDir, slower, allocatedMore, new[] { typeof(Select.OneParamNoIndex), typeof(Select.OneParamIndexed) });
                Run(outputDir, slower, allocatedMore, new[] { typeof(SelectMany.OneParamNoIndex), typeof(SelectMany.OneParamIndexed), typeof(SelectMany.TwoParamsNoIndex), typeof(SelectMany.TwoParamsIndexed) });
                Run(outputDir, slower, allocatedMore, new[] { typeof(SequenceEqual.OneParam), typeof(SequenceEqual.TwoParams) });
                Run(outputDir, slower, allocatedMore, new[] { typeof(Benchmarks.Single.NoParams), typeof(Benchmarks.Single.OneParam) });
                Run(outputDir, slower, allocatedMore, new[] { typeof(SingleOrDefault.NoParams), typeof(SingleOrDefault.OneParam) });
                Run(outputDir, slower, allocatedMore, new[] { typeof(Skip.OneParam) });
                // No released .NET Framework has SkipLast yet
                Run(outputDir, slower, allocatedMore, new[] { typeof(SkipWhile.OneParamNoIndex), typeof(SkipWhile.OneParamIndexed) });
                Run(outputDir, slower, allocatedMore, new[] { typeof(Sum.NonNull), typeof(Sum.Nullable), typeof(Sum.Projection) });
                Run(outputDir, slower, allocatedMore, new[] { typeof(Take.OneParam) });
                // No released .NET Framework has TakeLast yet
                Run(outputDir, slower, allocatedMore, new[] { typeof(TakeWhile.OneParamNoIndex), typeof(TakeWhile.OneParamIndexed) });
                Run(outputDir, slower, allocatedMore, new[] { typeof(ThenBy.OneParam), typeof(ThenBy.TwoParams) });
                Run(outputDir, slower, allocatedMore, new[] { typeof(ThenByDescending.OneParam), typeof(ThenByDescending.TwoParams) });
                Run(outputDir, slower, allocatedMore, new[] { typeof(ToArray.UnderlyingSized), typeof(ToArray.UnderlyingUnsized) });
                Run(outputDir, slower, allocatedMore, new[] { typeof(ToDictionary.OneParamSized), typeof(ToDictionary.OneParamSized_Comparable), typeof(ToDictionary.OneParamUnsized), typeof(ToDictionary.OneParamUnsized_Comparable), typeof(ToDictionary.TwoParamSized), typeof(ToDictionary.TwoParamSized_Comparable), typeof(ToDictionary.TwoParamUnsized), typeof(ToDictionary.TwoParamUnsized_Comparable) });
                // No released .NET Framework has ToHashSet yet
                Run(outputDir, slower, allocatedMore, new[] { typeof(ToList.UnderlyingSized), typeof(ToList.UnderlyingUnsized) });
                Run(outputDir, slower, allocatedMore, new[] { typeof(ToLookup.KeySelector), typeof(ToLookup.KeySelector_Comparer), typeof(ToLookup.KeySelector_ElementSelector), typeof(ToLookup.KeySelector_ElementSelector_Comparer) });
                Run(outputDir, slower, allocatedMore, new[] { typeof(Union.OneParam), typeof(Union.TwoParams) });
                Run(outputDir, slower, allocatedMore, new[] { typeof(Where.OneParamNoIndex), typeof(Where.OneParamIndexed) });
                Run(outputDir, slower, allocatedMore, new[] { typeof(Zip.ThreeParams) });
                Run(outputDir, slower, allocatedMore, new[] { typeof(StackOverflow.QuestionShow), typeof(StackOverflow.Interesting) });
                Run(outputDir, slower, allocatedMore, new[] { typeof(Jil.IsAnonymouseClass) });
                Run(outputDir, slower, allocatedMore, new[] { typeof(Benchmarks.Roslyn.LinkedReferences) });
                Run(outputDir, slower, allocatedMore, new[] { typeof(Crazy.Giant) });

                Console.WriteLine();
                Console.WriteLine("====");
                Console.WriteLine();

                Console.WriteLine("Were slower");
                Console.WriteLine("-----------");
                foreach (var line in slower.OrderBy(x => x))
                {
                    Console.WriteLine("\t" + line);
                }

                Console.WriteLine();
                Console.WriteLine("Allocated more");
                Console.WriteLine("-----------");
                foreach (var line in allocatedMore.OrderBy(x => x))
                {
                    Console.WriteLine("\t" + line);
                }
            }
        }

        class ForkWriter : TextWriter
        {
            public override Encoding Encoding => A?.Encoding ?? B?.Encoding;

            TextWriter A;
            TextWriter B;
            public ForkWriter(TextWriter a, TextWriter b)
            {
                A = a;
                B = b;
            }

            public override void Write(char value)
            {
                A?.Write(value);
                B?.Write(value);
            }
        }

        //static void Run(string outputDir, List<string> slower, List<string> allocatedMore, params Type[] benchmarks)
        //{
        //    foreach (var bench in benchmarks)
        //    {
        //        var benchInstance = Activator.CreateInstance(bench);

        //        var action = typeof(Action<>).MakeGenericType(bench);

        //        var linqAF = Delegate.CreateDelegate(action, bench.GetMethod("LinqAF"));
        //        //var linq2Objects = Delegate.CreateDelegate(action, bench.GetMethod("LINQ2Objects"));

        //        linqAF.DynamicInvoke(benchInstance);
        //        //linq2Objects.DynamicInvoke(benchInstance);

        //        GC.Collect(3, GCCollectionMode.Forced);
        //        for (var i = 0; i < 10; i++)
        //        {
        //            linqAF.DynamicInvoke(benchInstance);
        //        }
        //        /*GC.Collect(3, GCCollectionMode.Forced);
        //        for (var i = 0; i < 10; i++)
        //        {
        //            linq2Objects.DynamicInvoke(benchInstance);
        //        }*/
        //    }
        //}

        static Dictionary<Type, Summary> Run(string outputDir, List<string> slower, List<string> allocatedMore, params Type[] benchmarks)
        {
            var config = ManualConfig.CreateEmpty().With(new MemoryDiagnoser()).With(DefaultConfig.Instance.GetColumnProviders().ToArray()).With(DefaultConfig.Instance.GetExporters().ToArray());
            config = config.With(Job.RyuJitX64.WithGcConcurrent(true).WithGcServer(true));
            config = config.With(Job.LegacyJitX86.WithGcConcurrent(true).WithGcServer(true));
            config = config.With(DefaultConfig.Instance.GetLoggers().ToArray());
            
            var ret = new Dictionary<Type, Summary>();

            foreach (var bench in benchmarks)
            {
                var perTestConfig = config;

                IDisposable dispose = null;
                if (outputDir != null)
                {
                    var typeName = bench.DeclaringType != null ? $"{bench.DeclaringType.Name}.{bench.Name}" : bench.Name;
                    var path = Path.Combine(outputDir, typeName + ".log");
                    var stream = File.Create(path);
                    var writer = new StreamWriter(stream);
                    perTestConfig = config.With(new StreamLogger(writer));

                    dispose = writer;
                }

                var summary = BenchmarkRunner.Run(bench, perTestConfig);

                ret[bench] = summary;

                RecordOutliers(summary, slower, allocatedMore);

                dispose?.Dispose();
            }

            return ret;
        }

        static void RecordOutliers(Summary summary, List<string> slower, List<string> allocatedMore)
        {
            var displayInfos = summary.Benchmarks.Select(b => b.Job.DisplayInfo).Distinct().ToList();

            foreach(var info in displayInfos)
            {
                var benches = summary.Benchmarks.Where(b => b.Job.DisplayInfo == info).ToList();
                var linqAF = benches.Single(b => b.Target.Method.Name == "LinqAF");
                var linq2Objects = benches.Single(b => b.Target.Method.Name == "LINQ2Objects");

                var benchmarkClass = linqAF.Target.Method.DeclaringType;

                string benchmarkName;
                if(benchmarkClass.DeclaringType != null)
                {
                    benchmarkName = $"{benchmarkClass.DeclaringType.Name}.{benchmarkClass.Name}";
                }
                else
                {
                    benchmarkName = benchmarkClass.Name;
                }

                var linqAFReport = summary.Reports.Single(r => r.Benchmark == linqAF);
                var linq2ObjectsReport = summary.Reports.Single(r => r.Benchmark == linq2Objects);

                var linqAFStats = linqAFReport.ResultStatistics;
                var linq2ObjectsStats = linq2ObjectsReport.ResultStatistics;

                var linqAFGc = linqAFReport.GcStats;
                var linq2ObjectsGc = linq2ObjectsReport.GcStats;

                var linqAFWasFaster = linqAFStats.ConfidenceInterval.Upper < linq2ObjectsStats.ConfidenceInterval.Lower;
                var linqAFWasSlower = linqAFStats.ConfidenceInterval.Lower > linq2ObjectsStats.ConfidenceInterval.Upper;
                var linqAFAllocatedLess = linqAFGc.BytesAllocatedPerOperation <= linq2ObjectsGc.BytesAllocatedPerOperation;
                var linqAFAllocatedNothing = linqAFGc.BytesAllocatedPerOperation == 0;

                // basically, if the confidence intervals overlap let's just say it's "about the same"
                var linqAFIsSlower = !linqAFWasFaster && linqAFWasSlower;

                if (linqAFIsSlower)
                {
                    double speedDiff = linqAFStats.ConfidenceInterval.Mean - linq2ObjectsStats.ConfidenceInterval.Mean;
                    double speedDiffScaled = speedDiff / linq2ObjectsStats.ConfidenceInterval.Mean;
                    double speedDiffPerc = speedDiffScaled * 100.0;

                    Console.WriteLine($"{benchmarkName} - {info} - LinqAF was **SLOWER** than LINQ2Objects ({speedDiffPerc:N1}%)");
                    slower.Add($"{benchmarkName} - {info} - ({speedDiffPerc:N1}%)");
                }

                var linqAFAllocatedMore = !linqAFAllocatedLess && !linqAFAllocatedNothing;

                if (linqAFAllocatedMore)
                {
                    var allocDiff = linqAFGc.BytesAllocatedPerOperation - linq2ObjectsGc.BytesAllocatedPerOperation;

                    Console.WriteLine($"{benchmarkName} - LinqAF allocated **MORE** than LINQ2Objects ({allocDiff:N0} bytes)");
                    allocatedMore.Add($"{benchmarkName} - {info} - ({allocDiff:N0} bytes)");
                }
            }
        }
    }
}
