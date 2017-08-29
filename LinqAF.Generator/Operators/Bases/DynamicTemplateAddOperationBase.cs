using System.Linq;
using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.IO;

namespace LinqAF.Generator
{
    abstract class DynamicTemplateAddOperationBase: AutoTemplateAddOperationBase
    {
        protected override string SpecializeTemplate(string enumerableName, Document enumerable, string template)
        {
            var basic =  base.SpecializeTemplate(enumerableName, enumerable, template);

            var parsed = SyntaxFactory.ParseCompilationUnit(basic);
            var mtds = parsed.DescendantNodesAndSelf().OfType<MethodDeclarationSyntax>();
            var mtdReplacements = new Dictionary<MethodDeclarationSyntax, MethodDeclarationSyntax>();

            foreach(var mtd in mtds)
            {
                var retType = mtd.ReturnType as GenericNameSyntax;
                if (retType == null) continue;

                var commonImplCalls = mtd.DescendantNodesAndSelf().OfType<MemberAccessExpressionSyntax>().Where(m => (m.Expression as IdentifierNameSyntax)?.Identifier.ValueText == BuiltInExtensionMethods.COMMON_IMPLEMENTATION_NAME).ToList();
                var genArgs = retType.TypeArgumentList;

                var callReplacements = new Dictionary<MemberAccessExpressionSyntax, MemberAccessExpressionSyntax>();
                foreach(var call in commonImplCalls)
                {
                    var name = call.Name;

                    // don't replace existing generic arguments
                    if (name is GenericNameSyntax) continue;

                    // don't replace calls to Bridge, we rely on type inference there
                    if (name.Identifier.ValueText == "Bridge") continue;

                    // exception methods!
                    if (name.Identifier.ValueText == "ForbiddenCall") continue;
                    if (name.Identifier.ValueText == "UnexpectedPath") continue;
                    if (name.Identifier.ValueText == "Uninitialized") continue;
                    if (name.Identifier.ValueText == "ArgumentNull") continue;
                    if (name.Identifier.ValueText == "SequenceEmpty") continue;
                    if (name.Identifier.ValueText == "OutOfRange") continue;
                    if (name.Identifier.ValueText == "NoItemsMatched") continue;
                    if (name.Identifier.ValueText == "MultipleMatchingElements") continue;
                    if (name.Identifier.ValueText == "MultipleElements") continue;
                    if (name.Identifier.ValueText == "UninitializedProjection") continue;
                    if (name.Identifier.ValueText == "UnexpectedState") continue;
                    if (name.Identifier.ValueText == "NotImplemented") continue;
                    if (name.Identifier.ValueText == "InnerUninitialized") continue;
                    if (name.Identifier.ValueText == "InvalidOperation") continue;

                    var genName = SyntaxFactory.GenericName(name.Identifier, genArgs);

                    var updatedCall = call.WithName(genName);

                    callReplacements[call] = updatedCall;
                }

                var updatedMtd = mtd.ReplaceNodes(callReplacements.Keys, (old, _) => callReplacements[old].WithTriviaFrom(old));
                mtdReplacements[mtd] = updatedMtd;
            }

            var updatedParsed = parsed.ReplaceNodes(mtdReplacements.Keys, (old, _) => mtdReplacements[old].WithTriviaFrom(old));

            string ret;
            using (var writer = new StringWriter())
            {
                updatedParsed.WriteTo(writer);
                ret = writer.ToString();
            }

            return ret;
        }
    }
}
