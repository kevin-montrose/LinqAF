using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.MSBuild;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqAF.Generator
{
    

    class Projects:
        IDisposable
    {
        public struct DocumentPair
        {
            public string Name { get; set; }
            public Document Document { get; set; }
        }

        public Project Template { get; private set; }
        public Project Output { get; private set; }

        public IEnumerable<Document> EnumerableDocuments
        {
            get
            {
                foreach(var pair in EnumerableDocumentsWithNames)
                {
                    yield return pair.Document;
                }
            }
        }

        public IEnumerable<DocumentPair> EnumerableDocumentsWithNames
        {
            get
            {
                foreach (var kv in EnumerableRoots)
                {
                    var name = kv.Key;
                    var id = kv.Value;
                    var doc = Output.GetDocument(id);
                    yield return new DocumentPair { Name = name, Document = doc };
                }
            }
        }

        IReadOnlyDictionary<string, DocumentId> EnumerableRoots { get; set; }
        IReadOnlyDictionary<DocumentId, string> EnumerableRootsLookup { get; set; }

        public Workspace Workspace { get; private set; }
        Solution Solution { get; set; }

        public void Save()
        {
            var templateChanges = Solution.GetProject(Template.Id).GetChanges(Template);

            var anythingChangedInTemplate =
                templateChanges.GetAddedAdditionalDocuments().Any() ||
                templateChanges.GetAddedAnalyzerReferences().Any() ||
                templateChanges.GetAddedDocuments().Any() ||
                templateChanges.GetAddedMetadataReferences().Any() ||
                templateChanges.GetAddedProjectReferences().Any() ||
                templateChanges.GetChangedAdditionalDocuments().Any() ||
                templateChanges.GetChangedDocuments().Any() ||
                templateChanges.GetRemovedAdditionalDocuments().Any() ||
                templateChanges.GetRemovedAnalyzerReferences().Any() ||
                templateChanges.GetRemovedDocuments().Any() ||
                templateChanges.GetRemovedMetadataReferences().Any() ||
                templateChanges.GetRemovedProjectReferences().Any();

            if (anythingChangedInTemplate)
            {
                throw new Exception();
            }

            if (!Workspace.TryApplyChanges(Solution))
            {
                throw new Exception();
            }
        }
        
        public void SetEnumerableRoots(IDictionary<string, DocumentId> roots)
        {
            if (EnumerableRoots != null) throw new InvalidOperationException();

            EnumerableRoots = new ReadOnlyDictionary<string, DocumentId>(roots);
            EnumerableRootsLookup = new ReadOnlyDictionary<DocumentId, string>(roots.ToDictionary(kv => kv.Value, kv => kv.Key));
        }

        public void ModifyOutput(Func<Project, Project> modifier)
        {
            var newOutput = modifier(Output);
            
            if(newOutput.Id != Output.Id)
            {
                throw new Exception();
            }

            var newSolution = newOutput.Solution;
            
            Output = newOutput;
            Solution = newSolution;
        }

        public static Projects Load(string solutionPath, string templateProjectName, string outputProjectName)
        {
            var workspace = MSBuildWorkspace.Create();
            var solution = workspace.OpenSolutionAsync(solutionPath).Result;

            var templateProject = solution.Projects.Single(p => p.Name.Equals(templateProjectName, StringComparison.OrdinalIgnoreCase));
            var outputProject = solution.Projects.Single(p => p.Name.Equals(outputProjectName, StringComparison.OrdinalIgnoreCase));
            
            while(outputProject.Documents.Count() > 0)
            {
                var toRemove = outputProject.Documents.First();
                outputProject = outputProject.RemoveDocument(toRemove.Id);
            }
            
            return
                new Projects
                {
                    Workspace = workspace,
                    Solution = solution,

                    Template = templateProject,
                    Output = outputProject,
                };
        }

        public void Dispose()
        {
            Workspace.Dispose();
            Workspace = null;
        }
    }
}
