using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Editing;
using System.Linq;

namespace LinqAF.Generator
{
    class LinqAFString : IAddOperation
    {
        const string LINQAFSTRING_FILENAME_START = "LinqAFString.";
        const string REPLACEMENT_METHOD = "PassThrough";
        const string STRING_PASSTHROUGH = "System.String.";

        public void Add(Projects projects)
        {
            var docs = projects.Template.Documents.Where(d => d.Name.StartsWith(LINQAFSTRING_FILENAME_START)).ToArray();
            foreach (var doc in docs)
            {
                var root = doc.GetSyntaxRootAsync().Result;

                var editor = DocumentEditor.CreateAsync(doc).Result;

                var mtds = root.DescendantNodesAndSelf().OfType<MethodDeclarationSyntax>();
                foreach (var mtd in mtds)
                {
                    var mtdName = mtd.Identifier.ValueText;
                    if (mtdName == REPLACEMENT_METHOD)
                    {
                        editor.RemoveNode(mtd);
                        continue;
                    }

                    var parameters = mtd.ParameterList.Parameters.Select(p => p.Identifier.ValueText).ToArray();

                    var toReplace = mtd.DescendantNodesAndSelf().OfType<InvocationExpressionSyntax>().ToArray();

                    var canReplace =
                        !(
                            toReplace.Length != 1 ||
                              toReplace.Any(
                                  call => (call.Expression as IdentifierNameSyntax)?.Identifier.ValueText != REPLACEMENT_METHOD
                              )
                          );

                    // has an actual implementation, don't bother
                    if (!canReplace) continue;
                    
                    var callText = STRING_PASSTHROUGH + mtdName + "(" + string.Join(", ", parameters) + ")";

                    var replacementCall = SyntaxFactory.ParseExpression(callText);


                    editor.ReplaceNode(toReplace.Single(), replacementCall);
                }

                var updatedDoc = editor.GetChangedDocument();
                var updatedRoot = updatedDoc.GetSyntaxRootAsync().Result;

                projects.ModifyOutput(
                    p =>
                    {
                        var updated = p.AddDocument(doc.Name, updatedRoot);

                        return updated.Project;
                    }
                );
            }
        }
    }
}
