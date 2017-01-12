using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqAF.Generator
{
    static class Helper
    {
        public static void ExtractEnumerableDetails(StructDeclarationSyntax @struct, out string enumerable, out string outType, out string enumerator)
        {
            var enumerableBase = @struct.BaseList.Types.OfType<SimpleBaseTypeSyntax>().Single(b => (b.Type as GenericNameSyntax)?.Identifier.ValueText == Writer.ENUMERABLE_INTERFACE_NAME).Type;

            var genericArgs = enumerableBase.DescendantNodes().OfType<TypeArgumentListSyntax>().First();

            enumerable = @struct.Identifier.ValueText + @struct.TypeParameterList.GetText().ToString();
            outType = genericArgs.Arguments[0].GetText().ToString();
            enumerator = genericArgs.Arguments[1].GetText().ToString();
        }
    }
}
