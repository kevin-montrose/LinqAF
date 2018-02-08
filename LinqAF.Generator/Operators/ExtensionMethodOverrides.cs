using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LinqAF.Generator
{
    class ExtensionMethodOverrides : IAddOperation
    {
        const string OVERRIDES_FOLDER_NAME = "Overrides";
        const string EXTENSION_METHODS_FOLDER_NAME = "ExtensionMethods";

        public void Add(Projects projects)
        {
            var overrides = projects.Template.Documents.Where(p => p.Folders?.ElementAtOrDefault(0) == OVERRIDES_FOLDER_NAME && p.Folders?.ElementAtOrDefault(1) == EXTENSION_METHODS_FOLDER_NAME);
            var toApply = DetermineOverrides(overrides);

            foreach (var kv in toApply)
            {
                var enumerableName = kv.Key;
                var mtds = kv.Value;

                var file = projects.Output.Documents.Where(d => d.Name == kv.Key).Select(d => d.Id).Single();

                ApplyOverrides(projects, file, mtds);
            }
        }

        Dictionary<string, List<MethodDeclarationSyntax>> DetermineOverrides(IEnumerable<Document> overrides)
        {
            var ret = new Dictionary<string, List<MethodDeclarationSyntax>>();

            foreach (var file in overrides)
            {
                var className = file.Name;

                var root = file.GetSyntaxRootAsync().Result;
                var decls = root.DescendantNodesAndSelf().OfType<ClassDeclarationSyntax>();

                var allMtds = decls.SelectMany(d => d.DescendantNodesAndSelf().OfType<MethodDeclarationSyntax>()).ToList();

                ret[className] = allMtds;
            }

            return ret;
        }

        void ApplyOverrides(Projects projects, DocumentId docId, List<MethodDeclarationSyntax> mtds)
        {
            var toUpdate = projects.Output.Documents.Single(d => d.Id == docId);
            var root = toUpdate.GetSyntaxRootAsync().Result;

            var needsReplacement = new Dictionary<SyntaxNode, SyntaxNode>();

            foreach (var mtd in mtds)
            {
                var mtdStr = mtd.ToString();

                var oldMtds = root.DescendantNodesAndSelf().OfType<MethodDeclarationSyntax>();

                MethodDeclarationSyntax toReplace = null;

                foreach (var oldMtd in oldMtds)
                {
                    if (oldMtd.Identifier.ValueText != mtd.Identifier.ValueText) continue;
                    if (oldMtd.ParameterList.Parameters.Count != mtd.ParameterList.Parameters.Count) continue;
                    //if (oldMtd.ReturnType.ToString() != mtd.ReturnType.ToString()) continue;

                    var allParamsMatch = true;

                    for(var i = 0; i < oldMtd.ParameterList.Parameters.Count; i++)
                    {
                        var oldParam = oldMtd.ParameterList.Parameters.ElementAt(i);
                        var newParam = mtd.ParameterList.Parameters.ElementAt(i);

                        if(oldParam.ToString() != newParam.ToString())
                        {
                            allParamsMatch = false;
                            break;
                        }
                    }

                    if (!allParamsMatch) continue;

                    toReplace = oldMtd;
                    break;
                }

                if (toReplace == null) throw new InvalidOperationException($"Couldn't find a method to replace for {toUpdate.Name} -> {mtd.Identifier.ValueText}");

                // will explode if we try to replace the same method twice
                needsReplacement.Add(toReplace, mtd);
            }

            var newRoot = root.ReplaceNodes(needsReplacement.Keys, (old, _) => needsReplacement[old].WithTriviaFrom(old));

            projects.ModifyOutput(
                p =>
                {
                    var toModify = p.Documents.Single(d => d.Id == docId);
                    var updatedDoc = toModify.WithSyntaxRoot(newRoot);

                    return updatedDoc.Project;
                }
            );
        }
    }
}
