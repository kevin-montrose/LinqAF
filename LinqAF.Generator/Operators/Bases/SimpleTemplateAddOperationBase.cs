using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Linq;

namespace LinqAF.Generator
{
    abstract class SimpleTemplateAddOperationBase : IAddOperation
    {
        protected const string ENUMERABLE_TEMPLATE_VAR = "{Enumerable}";
        protected const string OUTITEM_TEMPLATE_VAR = "{OutItem}";
        protected const string ENUMERATOR_TEMPLATE_VAR = "{Enumerator}";

        public virtual void Add(Projects projects)
        {
            var template = CreateTemplate(projects.Template);
            
            foreach (var pair in projects.EnumerableDocumentsWithNames)
            {
                var enumerableName = pair.Name;
                var enumerable = pair.Document;

                if (!ShouldSpecialize(enumerableName, enumerable)) continue;

                var interfaceImpl = SpecializeTemplate(enumerableName, enumerable, template);

                var baseName = enumerableName.Substring(0, enumerableName.Length - "Enumerable".Length);

                var newFileName = GetName(baseName) + ".cs";

                projects.ModifyOutput(
                    p =>
                    {
                        var newDocument = p.AddDocument(newFileName, interfaceImpl);

                        return newDocument.Project;
                    }
                );
            }
        }

        protected virtual bool ShouldSpecialize(string enumerableName, Document enumerable) => true;

        protected virtual string SpecializeTemplate(string enumerableName, Document enumerable, string template)
        {
            var root = enumerable.GetSyntaxRootAsync().Result;
            var decl = root.DescendantNodes().OfType<StructDeclarationSyntax>().Single();

            // grab the proper names of the enumerable we're adding this op to
            string enumerableWithArgs, outItem, enumerator;
            Helper.ExtractEnumerableDetails(decl, out enumerableWithArgs, out outItem, out enumerator);

            var interfaceImpl =
                template
                    .Replace(ENUMERABLE_TEMPLATE_VAR, enumerableWithArgs)
                    .Replace(OUTITEM_TEMPLATE_VAR, outItem)
                    .Replace(ENUMERATOR_TEMPLATE_VAR, enumerator);

            return interfaceImpl;
        }

        protected abstract string CreateTemplate(Project project);
        protected abstract string GetName(string enumerableRoot);
    }
}
