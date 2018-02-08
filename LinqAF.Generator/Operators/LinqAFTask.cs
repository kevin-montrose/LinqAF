using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Editing;
using System.Linq;

namespace LinqAF.Generator
{
    class LinqAFTask : IAddOperation
    {
        const string LINQAFTASK_FILENAME_START = "LinqAFTask.";
        const string REPLACEMENT_METHOD = "PassThrough";
        const string TASK_PASSTHROUGH = "System.Threading.Tasks.Task.";
                   

        public void Add(Projects projects)
        {
            var docs = projects.Template.Documents.Where(d => d.Name.StartsWith(LINQAFTASK_FILENAME_START)).ToArray();
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

                    var toReplaceArr = mtd.DescendantNodesAndSelf().OfType<InvocationExpressionSyntax>().ToArray();

                    var canReplace =
                        !(
                            toReplaceArr.Length != 1 ||
                              toReplaceArr.Any(
                                  call => (call.Expression as SimpleNameSyntax)?.Identifier.ValueText != REPLACEMENT_METHOD
                              )
                          );

                    // has an actual implementation, don't bother
                    if (!canReplace) continue;

                    var toReplace = toReplaceArr.Single();

                    var gen = toReplace.Expression as GenericNameSyntax;

                    string typeArgs;
                    if (gen != null)
                    {
                        typeArgs = "<" + string.Join(", ", gen.TypeArgumentList.Arguments.Select(t => t.ToFullString())) + ">";
                    }
                    else
                    {
                        typeArgs = "";
                    }

                    var callText = TASK_PASSTHROUGH + mtdName + typeArgs + "(" + string.Join(", ", parameters) + ")";

                    var replacementCall = SyntaxFactory.ParseExpression(callText);

                    editor.ReplaceNode(toReplace, replacementCall);
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
