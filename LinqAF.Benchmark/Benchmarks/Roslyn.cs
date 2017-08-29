using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Immutable;

namespace LinqAF.Benchmark.Benchmarks
{
    public class Roslyn
    {
        public class LinkedReferences
        {
            static readonly ImmutableArray<SourceReferenceTreeItem> Result = MakeResult();
            static readonly Func<SourceReferenceTreeItem, string> Func1 = r => r.DisplayText.ToLowerInvariant();
            static readonly Func<GroupingEnumerable<string, SourceReferenceTreeItem>, bool> Func2_LAF = g => g.Count() > 1;
            static readonly Func<System.Linq.IGrouping<string, SourceReferenceTreeItem>, bool> Func2_L2O = g => g.Count() > 1;
            static readonly Func<GroupingEnumerable<string, SourceReferenceTreeItem>, GroupingEnumerable<string, SourceReferenceTreeItem>> Func3_LAF = g => g;
            static readonly Func<System.Linq.IGrouping<string, SourceReferenceTreeItem>, System.Linq.IGrouping<string, SourceReferenceTreeItem>> Func3_L2O = g => g;

            [Benchmark]
            public void LinqAF()
            {
                // https://github.com/dotnet/roslyn/blob/d4dab355b96955aca5b4b0ebf6282575fad78ba8/src/VisualStudio/Core/Def/Implementation/Library/FindResults/LibraryManager_FindReferences.cs#L80
                var linkedReferences = Result.GroupBy(Func1).Where(Func2_LAF).SelectMany(Func3_LAF);
                foreach (var linkedReference in linkedReferences)
                {
                    linkedReference.AddProjectNameDisambiguator();
                }
            }

            [Benchmark(Baseline = true)]
            public void LINQ2Objects()
            {
                // https://github.com/dotnet/roslyn/blob/d4dab355b96955aca5b4b0ebf6282575fad78ba8/src/VisualStudio/Core/Def/Implementation/Library/FindResults/LibraryManager_FindReferences.cs#L80
                var linkedReferences = 
                    System.Linq.Enumerable.SelectMany(
                        System.Linq.Enumerable.Where(
                            System.Linq.Enumerable.GroupBy(
                                Result,
                                Func1
                            ),
                            Func2_L2O
                        ),
                        Func3_L2O
                    );
                foreach (var linkedReference in linkedReferences)
                {
                    linkedReference.AddProjectNameDisambiguator();
                }
            }

            static ImmutableArray<SourceReferenceTreeItem> MakeResult()
            {
                var builder = ImmutableArray.CreateBuilder<SourceReferenceTreeItem>();
                builder.Add(new SourceReferenceTreeItem { DisplayText = "BenchmarkDotNet.Attributes" });
                builder.Add(new SourceReferenceTreeItem { DisplayText = "System" });
                builder.Add(new SourceReferenceTreeItem { DisplayText = "System.Collections.Immutable" });

                return builder.ToImmutable();
            }

            class SourceReferenceTreeItem
            {
                public string DisplayText { get; set; }

                public void AddProjectNameDisambiguator() { }
            }
        }
    }
}
