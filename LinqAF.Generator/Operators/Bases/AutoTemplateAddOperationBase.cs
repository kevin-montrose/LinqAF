using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqAF.Generator
{
    abstract class AutoTemplateAddOperationBase : SimpleTemplateAddOperationBase
    {
        const string TEMPLATE_FOLDER = "Templates";
        const string TEMPLATE_CLASS_BASE = "TemplateBase";

        const string THIS_PLACEHOLDER = "This";
        static readonly ThisExpressionSyntax THIS = SyntaxFactory.ThisExpression();

        const string REF_THIS_PLACEHOLDER = "RefThis";
        static readonly RefExpressionSyntax REF_THIS = SyntaxFactory.RefExpression(SyntaxFactory.ParseToken("ref").WithTrailingTrivia(SyntaxFactory.Whitespace(" ")), SyntaxFactory.ThisExpression());

        const string IS_DEFAULT_VALUE_PLACEHOLDER = "IsDefaultValue";
        static readonly InvocationExpressionSyntax IS_DEFAULT_VALUE = (InvocationExpressionSyntax)SyntaxFactory.ParseExpression("this.IsDefaultValue()");

        static readonly SyntaxAnnotation METHOD_GENERIC_PARAMS_PREFIXED = new SyntaxAnnotation(nameof(AutoTemplateAddOperationBase) + "." + nameof(METHOD_GENERIC_PARAMS_PREFIXED));

        protected abstract string GetOperationName();

        protected override string GetName(string enumerableRoot)
        {
            return enumerableRoot + "." + GetOperationName();
        }

        protected override string CreateTemplate(Project templateProject)
        {
            var baseClassName = GetOperationName() + "Base";

            var inTemplateFolder = templateProject.Documents.Where(d => d.Folders?.FirstOrDefault() == TEMPLATE_FOLDER).ToList();
            var implementingTemplateBase =
                inTemplateFolder
                    .Where(
                        d =>
                        {
                            var root = d.GetSyntaxRootAsync().Result;

                            var classes = root.DescendantNodesAndSelf().OfType<ClassDeclarationSyntax>().ToList();

                            var extendingTemplateBase = classes.Where(c => c.BaseList != null && c.BaseList.Types.Any(t => (t.Type as SimpleNameSyntax)?.Identifier.ValueText == TEMPLATE_CLASS_BASE)).ToList();

                            return extendingTemplateBase.Any();
                        }
                    ).ToList();

            var docWithTemplate =
                implementingTemplateBase
                    .Single(d =>
                    {
                        var root = d.GetSyntaxRootAsync().Result;

                        var classes = root.DescendantNodesAndSelf().OfType<ClassDeclarationSyntax>().ToList();

                        return classes.Any(t => t.Identifier.ValueText == baseClassName);
                    }
                    );

            // extract the basic template, which we'll rewrite in the doc
            var template =
                docWithTemplate
                    .GetSyntaxRootAsync().Result
                    .DescendantNodesAndSelf()
                    .OfType<ClassDeclarationSyntax>()
                    .Single(t => t.Identifier.ValueText == baseClassName);

            // remove just the Template base constraint
            var typesSansTemplateBase =
                template.BaseList.Types
                    .Select(t => t.Type)
                    .Where(t => (t as SimpleNameSyntax)?.Identifier.ValueText != TEMPLATE_CLASS_BASE)
                    .Select(t => (BaseTypeSyntax)SyntaxFactory.SimpleBaseType(t))
                    .ToList();
            var newBaseTypes = SyntaxFactory.SeparatedList(typesSansTemplateBase);
            var baseListWithoutTemplateBase = SyntaxFactory.BaseList(newBaseTypes);
            var templateWithoutBaseList = template.WithBaseList(baseListWithoutTemplateBase.WithTriviaFrom(template.BaseList));

            // remove all type constraints
            var templateWithoutTypeContraints = templateWithoutBaseList.WithConstraintClauses(SyntaxFactory.List<TypeParameterConstraintClauseSyntax>());

            // read these nodes out now, because we're about to replace them
            var genericParameterList = templateWithoutTypeContraints.DescendantNodesAndSelf().OfType<TypeParameterListSyntax>().First();
            var oldOutItem = genericParameterList.Parameters.ElementAt(0);
            var oldEnumerable = genericParameterList.Parameters.ElementAt(1);
            var oldEnumerator = genericParameterList.Parameters.ElementAt(2);

            var newOutItem = SyntaxFactory.IdentifierName(OUTITEM_TEMPLATE_VAR);
            var newEnumerable = SyntaxFactory.IdentifierName(ENUMERABLE_TEMPLATE_VAR);
            var newEnumerator = SyntaxFactory.IdentifierName(ENUMERATOR_TEMPLATE_VAR);

            // turn into a partial struct
            var templateAsStruct =
                SyntaxFactory.ParseSyntaxTree(
                    templateWithoutTypeContraints.ToString().Replace("abstract class", "public partial struct")
                )
                .GetRoot()
                .DescendantNodesAndSelf()
                .OfType<StructDeclarationSyntax>().Single();
            
            // rewrite all method declarations so that generic parameters are prefixed to avoid collisions
            var withRewrittenMethods = templateAsStruct;
            var genericParamPrefix = "T" + GetOperationName() + "_";

            while (true)
            {
                var needRewrite = withRewrittenMethods.DescendantNodesAndSelf().OfType<MethodDeclarationSyntax>().Where(t => !t.HasAnnotation(METHOD_GENERIC_PARAMS_PREFIXED));
                if (!needRewrite.Any()) break;

                var mtd = needRewrite.First();
                var genArgs = mtd.TypeParameterList;
                if (genArgs == null)
                {
                    var withAnnotation = mtd.WithAdditionalAnnotations(METHOD_GENERIC_PARAMS_PREFIXED);
                    withRewrittenMethods = withRewrittenMethods.ReplaceNode(mtd, withAnnotation);
                    continue;
                }

                var newMtd = mtd;

                var oldPs = genArgs.Parameters.Select(p => p.Identifier.ValueText).ToList();
                var newPs = oldPs.Select(p => genericParamPrefix + p.Substring(1)).ToList();

                for (var i = 0; i < oldPs.Count; i++)
                {
                    var oldP = oldPs[i];
                    var newP = newPs[i];

                    var replacementName = SyntaxFactory.IdentifierName(newP);
                    var replacementType = SyntaxFactory.TypeParameter(newP);

                    var oldBody = newMtd.Body;
                    var oldBodyExpression = newMtd.ExpressionBody;
                    var oldReturn = newMtd.ReturnType;
                    var oldTypeParams = newMtd.TypeParameterList;
                    var oldParams = newMtd.ParameterList;
                    var oldConstraints = newMtd.ConstraintClauses;

                    if (oldBody != null)
                    {
                        var oldNodes = oldBody.DescendantNodesAndSelf().OfType<IdentifierNameSyntax>().Where(t => t.Identifier.ValueText == oldP).ToList();
                        var newBody = oldBody.ReplaceNodes(oldNodes, (old, _) => replacementName.WithTriviaFrom(old));
                        newMtd = newMtd.WithBody(newBody.WithTriviaFrom(oldBody));
                    }
                    else
                    {
                        var oldNodes = oldBodyExpression.DescendantNodesAndSelf().OfType<IdentifierNameSyntax>().Where(t => t.Identifier.ValueText == oldP).ToList();
                        var newBodyExpression = oldBodyExpression.ReplaceNodes(oldNodes, (old, _) => replacementName.WithTriviaFrom(old));
                        newMtd = newMtd.WithExpressionBody(newBodyExpression.WithTriviaFrom(oldBodyExpression));
                    }

                    if (oldReturn != null)
                    {
                        var oldNodes = oldReturn.DescendantNodesAndSelf().OfType<IdentifierNameSyntax>().Where(t => t.Identifier.ValueText == oldP).ToList();
                        var newReturn = oldReturn.ReplaceNodes(oldNodes, (old, _) => replacementName.WithTriviaFrom(old));
                        newMtd = newMtd.WithReturnType(newReturn.WithTriviaFrom(oldReturn));
                    }

                    if (oldTypeParams != null)
                    {
                        var oldNodes = oldTypeParams.DescendantNodesAndSelf().OfType<TypeParameterSyntax>().Where(t => t.Identifier.ValueText == oldP).ToList();
                        var newTypeParams = oldTypeParams.ReplaceNodes(oldNodes, (old, _) => replacementType.WithTriviaFrom(old));
                        newMtd = newMtd.WithTypeParameterList(newTypeParams.WithTriviaFrom(oldTypeParams));
                    }

                    if (oldParams != null)
                    {
                        var oldNodes = oldParams.DescendantNodesAndSelf().OfType<IdentifierNameSyntax>().Where(t => t.Identifier.ValueText == oldP).ToList();
                        var newParams = oldParams.ReplaceNodes(oldNodes, (old, _) => replacementName.WithTriviaFrom(old));
                        newMtd = newMtd.WithParameterList(newParams.WithTriviaFrom(oldParams));
                    }

                    if (oldConstraints != null)
                    {
                        var newSyntaxList = SyntaxFactory.List<TypeParameterConstraintClauseSyntax>();
                        foreach (var oldConstraint in oldConstraints)
                        {
                            var oldNodes = oldConstraint.DescendantNodesAndSelf().OfType<IdentifierNameSyntax>().Where(t => t.Identifier.ValueText == oldP).ToList();
                            var newConstraint = oldConstraint.ReplaceNodes(oldNodes, (old, _) => replacementName.WithTriviaFrom(old));

                            newSyntaxList = newSyntaxList.Add(newConstraint.WithTriviaFrom(oldConstraint));
                        }

                        newMtd = newMtd.WithConstraintClauses(newSyntaxList);
                    }
                }
                
                newMtd = newMtd.WithAdditionalAnnotations(METHOD_GENERIC_PARAMS_PREFIXED);

                withRewrittenMethods = withRewrittenMethods.ReplaceNode(mtd, newMtd);
            }
            
            // change the type name
            var withoutGenericParameterList = withRewrittenMethods.WithTypeParameterList(null);
            var withEnumerableTypeName = withoutGenericParameterList.WithIdentifier(newEnumerable.Identifier);

            // replace all the outitem references
            var oldOutItemNodes = withEnumerableTypeName.DescendantNodesAndSelf().Where(d => (d as IdentifierNameSyntax)?.Identifier.ValueText == oldOutItem.Identifier.ValueText).ToList();
            var withNewOutItem = withEnumerableTypeName.ReplaceNodes(oldOutItemNodes, (old, _) => newOutItem.WithTriviaFrom(old));

            // replace all the enumerable references
            var oldEnumerableNodes = withNewOutItem.DescendantNodesAndSelf().Where(d => (d as IdentifierNameSyntax)?.Identifier.ValueText == oldEnumerable.Identifier.ValueText).ToList();
            var withNewEnumerable = withNewOutItem.ReplaceNodes(oldEnumerableNodes, (old, _) => newEnumerable.WithTriviaFrom(old));

            // replace all the enumerator references
            var oldEnumeratorNodes = withNewEnumerable.DescendantNodesAndSelf().Where(d => (d as IdentifierNameSyntax)?.Identifier.ValueText == oldEnumerator.Identifier.ValueText).ToList();
            var withNewEnumerator = withNewEnumerable.ReplaceNodes(oldEnumeratorNodes, (old, _) => newEnumerator.WithTriviaFrom(old));

            // replace This() invocations
            var oldThisNodes = withNewEnumerator.DescendantNodesAndSelf().OfType<InvocationExpressionSyntax>().Where(e => (e.Expression as IdentifierNameSyntax)?.Identifier.ValueText == THIS_PLACEHOLDER).ToList();
            var withNewThisNodes = withNewEnumerator.ReplaceNodes(oldThisNodes, (old, _) => THIS.WithTriviaFrom(old));

            // replace RefThis() invocations
            var oldRefThisNodes = withNewThisNodes.DescendantNodesAndSelf().OfType<InvocationExpressionSyntax>().Where(e => (e.Expression as IdentifierNameSyntax)?.Identifier.ValueText == REF_THIS_PLACEHOLDER).ToList();
            var withNewRefThisNodes = withNewThisNodes.ReplaceNodes(oldRefThisNodes, (old, _) => REF_THIS.WithTriviaFrom(old));

            // replace IsDefaultValue() invocations
            var oldIsDefaultValueNodes = withNewRefThisNodes.DescendantNodesAndSelf().OfType<InvocationExpressionSyntax>().Where(e => (e.Expression as IdentifierNameSyntax)?.Identifier.ValueText == IS_DEFAULT_VALUE_PLACEHOLDER).ToList();
            var withNewIsDefaultValueNodes = withNewRefThisNodes.ReplaceNodes(oldIsDefaultValueNodes, (old, _) => IS_DEFAULT_VALUE.WithTriviaFrom(old));
            
            // update the document root
            var oldRoot = docWithTemplate.GetSyntaxRootAsync().Result;
            var updatedRoot = oldRoot.ReplaceNode(template, withNewIsDefaultValueNodes.WithTriviaFrom(template));

            using (var txt = new StringWriter())
            {
                updatedRoot.WriteTo(txt);

                var ret = txt.ToString();
                return ret;
            }
        }
    }
}