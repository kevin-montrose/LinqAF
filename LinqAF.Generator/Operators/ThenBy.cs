using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Linq;

namespace LinqAF.Generator
{
    class ThenBy : AutoTemplateAddOperationBase
    {
        protected override string GetOperationName() => nameof(ThenBy);

        protected override bool ShouldSpecialize(string enumerableName, Document enumerable)
        {
            var interfaces =
                enumerable
                    .GetSyntaxRootAsync().Result
                    .DescendantNodesAndSelf()
                    .OfType<StructDeclarationSyntax>()
                    .Single()
                    .BaseList?.Types
                    .Select(t => t.Type)
                    .ToList();

            return interfaces.Any(i => ((SimpleNameSyntax)i).Identifier.ValueText == Writer.HAS_COMPARER_INTERFACE_NAME);
        }
    }
}
