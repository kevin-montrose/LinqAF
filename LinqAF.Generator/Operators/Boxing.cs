using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Editing;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LinqAF.Generator
{
    class Boxing: IAddOperation
    {
        const string BoxTemplate =
@"using System;
using LinqAF.Impl;
using LinqAF.Config;

namespace LinqAF
{
    public partial struct {Enumerable}
    {
        class Boxed: BoxedBase<{OutItem}, {Enumerable}, {Enumerator}>
        {
            public Boxed(ref {Enumerable} outer) : base(ref outer) { }
        }

        public static explicit operator BoxedEnumerable<{OutItem}>({Enumerable} e)
        {
            if (e.IsDefaultValue())
            {
                return EmptyCache<{OutItem}>.BadBoxedEmpty;
            }

            Allocator.Current.EnumerableBoxed<{Enumerable}>();
            
            var box = new Boxed(ref e);
            return new BoxedEnumerable<{OutItem}>(box);
        }

        public BoxedEnumerable<{OutItem}> Box()
        {
            if (IsDefaultValue())
            {
                throw CommonImplementation.InvalidOperation(""Enumerable uninitialized"");
            }

            Allocator.Current.EnumerableBoxed<{Enumerable}>();
            
            var box = new Boxed(ref this);
            return new BoxedEnumerable<{OutItem}>(box);
        }
    }
}
";

        public void Add(Projects projects)
        {
            foreach (var pair in projects.EnumerableDocumentsWithNames)
            {
                var enumerableName = pair.Name;

                // can't box self
                if (enumerableName == "BoxedEnumerable") continue;

                var enumerable = pair.Document;

                var root = enumerable.GetSyntaxRootAsync().Result;
                var decl = root.DescendantNodes().OfType<StructDeclarationSyntax>().Single();

                string enumerableWithArgs, outItem, enumerator;
                Helper.ExtractEnumerableDetails(decl, out enumerableWithArgs, out outItem, out enumerator);

                var boxedImpl = BoxTemplate.Replace("{Enumerable}", enumerableWithArgs).Replace("{OutItem}", outItem).Replace("{Enumerator}", enumerator);

                var baseName = enumerableName.Substring(0, enumerableName.Length - "Enumerable".Length);

                var newFileName = baseName + ".Boxing.cs";

                projects.ModifyOutput(
                    p =>
                    {
                        var newDocument = p.AddDocument(newFileName, boxedImpl);
                        
                        return newDocument.Project;
                    }
                );
            }
        }
    }
}
