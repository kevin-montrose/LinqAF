using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LinqAF.Generator
{
    class Count : AutoTemplateAddOperationBase
    {
        protected override string GetOperationName() => nameof(Count);

        protected override string SpecializeTemplate(string enumerableName, Document enumerable, string template)
        {
            var basic = base.SpecializeTemplate(enumerableName, enumerable, template);

            // LookupEnumerable implicitly implements ILookup, which itself has a Count property
            if (enumerableName != "LookupEnumerable") return basic;

            var parsedTemplate = SyntaxFactory.ParseCompilationUnit(basic);
            var structImpl = parsedTemplate.DescendantNodesAndSelf().OfType<StructDeclarationSyntax>().Single(s => s.Identifier.ValueText == enumerableName);
            var countDecl = (GenericNameSyntax)structImpl.BaseList.Types.Single(t => ((SimpleNameSyntax)t.Type).Identifier.ValueText == "ICount").Type;
            var countMtds = structImpl.DescendantNodesAndSelf().OfType<MethodDeclarationSyntax>().Where(m => m.Identifier.ValueText == "Count").ToList();
            

            var replaceMtds = new Dictionary<MethodDeclarationSyntax, MethodDeclarationSyntax>();
            foreach(var mtd in countMtds)
            {
                var updatedMtd = mtd;
                updatedMtd = mtd.WithModifiers(SyntaxFactory.TokenList());

                var explicitImpl = SyntaxFactory.ExplicitInterfaceSpecifier(countDecl.WithoutTrivia()).WithLeadingTrivia(SyntaxFactory.Whitespace(" "));

                updatedMtd = updatedMtd.WithExplicitInterfaceSpecifier(explicitImpl);

                replaceMtds[mtd] = updatedMtd;
            }

            var rewritten = parsedTemplate.ReplaceNodes(replaceMtds.Keys, (old, _) => replaceMtds[old].WithTriviaFrom(old));

            using (var writer = new StringWriter())
            {
                rewritten.WriteTo(writer);
                
                return writer.ToString();
            }
        }
    }
}
