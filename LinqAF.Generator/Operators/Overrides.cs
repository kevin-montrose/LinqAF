using System;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis;
using System.Linq;
using System.Collections.Generic;
using System.IO;

namespace LinqAF.Generator
{
    class Overrides : IAddOperation
    {
        const string OVERRIDES_FOLDER_NAME = "Overrides";

        public void Add(Projects projects)
        {
            var overrides = projects.Template.Documents.Where(p => p.Folders?.FirstOrDefault() == OVERRIDES_FOLDER_NAME);
            var toApply = DetermineOverrides(overrides);

            foreach (var kv in toApply)
            {
                var enumerableName = kv.Key;
                var mtds = kv.Value;

                var potentialFiles = projects.Output.Documents.Where(d => d.Name.StartsWith(enumerableName + ".")).Select(d => d.Id).ToList();

                ApplyOverrides(projects, potentialFiles, mtds);
            }
        }

        Dictionary<string, List<MethodDeclarationSyntax>> DetermineOverrides(IEnumerable<Document> overrides)
        {
            var ret = new Dictionary<string, List<MethodDeclarationSyntax>>();

            foreach (var file in overrides)
            {
                var enumerableName = file.Name;
                enumerableName = enumerableName.Substring(0, enumerableName.Length - ".cs".Length);

                var root = file.GetSyntaxRootAsync().Result;
                var decls = root.DescendantNodesAndSelf().OfType<StructDeclarationSyntax>();

                var allMtds = decls.SelectMany(d => d.DescendantNodesAndSelf().OfType<MethodDeclarationSyntax>()).ToList();

                ret[enumerableName] = allMtds;
            }

            return ret;
        }

        void ApplyOverrides(Projects projects, List<DocumentId> fileIds, List<MethodDeclarationSyntax> overrides)
        {
            foreach (var mtd in overrides)
            {
                var overrideName = mtd.Identifier.ValueText;
                var numParams = mtd.ParameterList.Parameters.Count;

                var files = projects.Output.Documents.Where(d => fileIds.Contains(d.Id));

                var inFiles =
                    files
                        .Where(
                            f =>
                            {
                                var root = f.GetSyntaxRootAsync().Result;
                                var allMtds = root.DescendantNodesAndSelf().OfType<MethodDeclarationSyntax>();

                                var sameName = allMtds.Where(m => m.Identifier.ValueText == overrideName);

                                var sameParams =
                                    sameName
                                        .Where(
                                            m =>
                                            {
                                                if (m.ParameterList.Parameters.Count != numParams) return false;

                                                for (var i = 0; i < numParams; i++)
                                                {
                                                    var o1 = mtd.ParameterList.Parameters[i];
                                                    var p1 = m.ParameterList.Parameters[i];

                                                    if (!o1.IsEquivalentTo(p1)) return false;
                                                }

                                                return true;
                                            }
                                        );

                                return sameParams.Any();
                            }
                        )
                        .ToList();

                if (inFiles.Count == 0) throw new InvalidOperationException($"Couldn't find file with method {overrideName} & {numParams} params to override");
                if (inFiles.Count > 1) throw new InvalidOperationException($"Found too many override candidates");

                var inFile = inFiles.Single();

                ReplaceInDocument(projects, inFile, mtd);
            }
        }

        void ReplaceInDocument(Projects projects, Document inFile, MethodDeclarationSyntax overrideMtd)
        {
            var root = inFile.GetSyntaxRootAsync().Result;

            var usingsInFile = root.DescendantNodesAndSelf().OfType<UsingDirectiveSyntax>().ToList();
            var usingInOverrideDoc = overrideMtd.SyntaxTree.GetRoot().DescendantNodesAndSelf().OfType<UsingDirectiveSyntax>().ToList();

            var missingUsings =
                usingInOverrideDoc.Where(
                    uo =>
                    {
                        string uoN;
                        using (var writer = new StringWriter())
                        {
                            uo.Name.WriteTo(writer);
                            uoN = writer.ToString();
                        }

                        return !usingsInFile.Any(
                            uf =>
                            {
                                string ufN;
                                using (var writer = new StringWriter())
                                {
                                    uf.Name.WriteTo(writer);
                                    ufN = writer.ToString();
                                }

                                return ufN == uoN;
                            }
                        );
                    }
                )
                .ToList();

            var allMtds = root.DescendantNodesAndSelf().OfType<MethodDeclarationSyntax>();

            var sameName = allMtds.Where(m => m.Identifier.ValueText == overrideMtd.Identifier.ValueText);

            var sameParams =
                sameName
                    .Single(
                        m =>
                        {
                            if (m.ParameterList.Parameters.Count != overrideMtd.ParameterList.Parameters.Count) return false;

                            for (var i = 0; i < overrideMtd.ParameterList.Parameters.Count; i++)
                            {
                                var o1 = overrideMtd.ParameterList.Parameters[i];
                                var p1 = m.ParameterList.Parameters[i];

                                if (!o1.IsEquivalentTo(p1)) return false;
                            }

                            return true;
                        }
                    );

            var updatedRoot = root.ReplaceNode(sameParams, overrideMtd.WithTriviaFrom(sameParams));

            if(missingUsings.Count > 0)
            {
                var insertBefore = updatedRoot.DescendantNodesAndSelf().OfType<UsingDirectiveSyntax>().First();

                updatedRoot = updatedRoot.InsertNodesBefore(insertBefore, missingUsings);
            }

            var impl = updatedRoot.DescendantNodesAndSelf().OfType<StructDeclarationSyntax>().Single();
            var potentialInternalInterfaces =
                impl.BaseList
                    ?.Types
                    .Where(t =>
                        ((SimpleNameSyntax)t.Type).Identifier.ValueText != Writer.ENUMERABLE_INTERFACE_NAME &&
                        ((SimpleNameSyntax)t.Type).Identifier.ValueText != Writer.ENUMERABLE_INTERFACE_NAME
                    )
                    .ToList();

            if ((potentialInternalInterfaces?.Count ?? 0) > 1)
            {
                throw new InvalidOperationException("Too many interfaces that might be invalidated to reason about");
            }

            if ((potentialInternalInterfaces?.Count ?? 0) > 0)
            {
                var withoutBaseList = impl.RemoveNode(impl.BaseList, SyntaxRemoveOptions.KeepNoTrivia);

                updatedRoot = updatedRoot.ReplaceNode(impl, withoutBaseList.WithTriviaFrom(impl));
            }

            projects.ModifyOutput(
                p =>
                {
                    var updatedDoc = inFile.WithSyntaxRoot(updatedRoot);

                    return updatedDoc.Project;
                }
            );
        }
    }
}