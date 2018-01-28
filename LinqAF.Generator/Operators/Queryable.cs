using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Editing;
using System.Linq;

namespace LinqAF.Generator
{
    class Queryable : IAddOperation
    {
        const string QUERYABLE_FILENAME = "Queryable.cs";
        const string REPLACEMENT_METHOD = "PassThrough";
        const string QUERYABLE_PASSTHROUGH = "System.Linq.Queryable.";

        public void Add(Projects projects)
        {
            var queryable = projects.Template.Documents.Single(d => d.Name == QUERYABLE_FILENAME);

            var root = queryable.GetSyntaxRootAsync().Result;

            var editor = DocumentEditor.CreateAsync(queryable).Result;

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

                var toReplace = mtd.DescendantNodesAndSelf().OfType<InvocationExpressionSyntax>().Single();

                var gen = toReplace.Expression as GenericNameSyntax;

                string typeArgs;
                if(gen != null)
                {
                    typeArgs = "<" + string.Join(", ", gen.TypeArgumentList.Arguments.Select(t => t.ToFullString())) + ">";
                }
                else
                {
                    typeArgs = "";
                }

                var callText = QUERYABLE_PASSTHROUGH + mtdName + typeArgs + "(" + string.Join(", ", parameters) + ")";

                var replacementCall = SyntaxFactory.ParseExpression(callText);
                

                editor.ReplaceNode(toReplace, replacementCall);
            }

            var updatedDoc = editor.GetChangedDocument();
            var updatedRoot = updatedDoc.GetSyntaxRootAsync().Result;

            projects.ModifyOutput(
                p =>
                {
                    var updated = p.AddDocument(queryable.Name, updatedRoot);

                    return updated.Project;
                }
            );
        }
    }
}
