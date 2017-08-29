using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis;
using System;
using System.Linq;
using System.Collections.Generic;

namespace LinqAF.Generator
{
    class BuiltInExtensionMethods :
        ExtensionMethodsBase
    {
        const string BUILTIN_ENUMERABLE_NAME = "BuiltInEnumerable";
        const string BUILTIN_ENUMERATOR_NAME = "BuiltInEnumerator";

        const string CONSTRAINED_BUILTIN_ENUMERABLE_NAME = "ConstrainedBuiltInEnumerable";
        const string CONSTRAINED_BUILTIN_ENUMERATOR_NAME = "ConstrainedBuiltInEnumerator";

        public const string COMMON_IMPLEMENTATION_NAME = "CommonImplementation";
        static readonly ExpressionSyntax COMMON_IMPLEMENTATION = SyntaxFactory.ParseExpression(COMMON_IMPLEMENTATION_NAME);

        const string BRIDGE_NAME = "Bridge";
        static readonly MemberAccessExpressionSyntax BRIDGE = (MemberAccessExpressionSyntax)SyntaxFactory.ParseExpression("CommonImplementation.Bridge");

        const string OUT_TYPE_ANNOTATION = nameof(OUT_TYPE_ANNOTATION);

        public override string GetExtensionMethodBaseClassName()
        {
            throw new InvalidOperationException();
        }

        public override void Add(Projects projects)
        {
            var builtIns = GetEnumerables(projects).Where(e => e.IsBridgeType).ToList();
            var enumerables = GetEnumerables(projects).Where(e => !e.IsBridgeType).ToList();

            foreach (var builtIn in builtIns)
            {
                WriteExtensionsMethodClassFor(projects, builtIn, builtIns, enumerables);
            }
        }

        void WriteExtensionsMethodClassFor(
            Projects projects,
            EnumerableDetails builtIn,
            IEnumerable<EnumerableDetails> allBuiltIns,
            IEnumerable<EnumerableDetails> allEnumerables
        )
        {
            string name;
            if (builtIn.Enumerable is GenericNameSyntax)
            {
                name = ((GenericNameSyntax)builtIn.Enumerable).Identifier.ValueText;
            }
            else
            {
                if (builtIn.Enumerable is QualifiedNameSyntax)
                {
                    var asQualified = (QualifiedNameSyntax)builtIn.Enumerable;

                    var left = asQualified.Left;
                    var right = asQualified.Right;

                    if (left is GenericNameSyntax)
                    {
                        name = ((GenericNameSyntax)left).Identifier.ValueText;
                    }
                    else
                    {
                        name = ((SimpleNameSyntax)left).Identifier.ValueText;
                    }

                    if (right is GenericNameSyntax)
                    {
                        name += ((GenericNameSyntax)right).Identifier.ValueText;
                    }
                    else
                    {
                        name += right.Identifier.ValueText;
                    }
                }
                else
                {
                    if (builtIn.Enumerable is ArrayTypeSyntax)
                    {
                        name = "Array";
                    }
                    else
                    {
                        throw new InvalidOperationException();
                    }
                }
            }

            var templateFileNameStarts = "BuiltInExtensionMethodsBase.";
            var templateFileNameEnd = ".cs";

            var templateDocs =
                projects.Template.Documents
                    .Where(d => d.Name.StartsWith(templateFileNameStarts) && d.Name.EndsWith(templateFileNameEnd))
                    .ToList();

            var parsedRoots = templateDocs.Select(d => d.GetSyntaxRootAsync().Result).ToList();
            var templateMtds = parsedRoots.SelectMany(r => r.DescendantNodesAndSelf().OfType<MethodDeclarationSyntax>()).ToList();

            // create the initial template
            CompilationUnitSyntax root;
            ClassDeclarationSyntax extensionDecl;
            CreateInitialTemplate(name, out root, out extensionDecl);

            // create the methods for each enumerable
            var updatedDecl = extensionDecl;
            foreach (var mtd in templateMtds)
            {
                // figure out what the enumerable and enumerator should _really_ be bound to
                var mentionsOfPlaceholderEnumerable =
                    mtd
                        .DescendantNodesAndSelf()
                        .OfType<SimpleNameSyntax>()
                        .Where(
                            i => i.Identifier.ValueText == BUILTIN_ENUMERABLE_NAME ||
                                 i.Identifier.ValueText == PLACEHOLDER_ENUMERABLE_NAME ||
                                 i.Identifier.ValueText == CONSTRAINED_BUILTIN_ENUMERABLE_NAME
                        )
                        .ToList();
                var mentionsOfPlaceholderEnumerator =
                    mtd
                        .DescendantNodesAndSelf()
                        .OfType<SimpleNameSyntax>()
                        .Where(
                            i => i.Identifier.ValueText == BUILTIN_ENUMERATOR_NAME ||
                                 i.Identifier.ValueText == PLACEHOLDER_ENUMERATOR_NAME ||
                                 i.Identifier.ValueText == CONSTRAINED_BUILTIN_ENUMERATOR_NAME
                        )
                        .ToList();

                var outTypes =
                    mentionsOfPlaceholderEnumerable
                        .OfType<GenericNameSyntax>()
                        .Concat(mentionsOfPlaceholderEnumerator.OfType<GenericNameSyntax>())
                        .Select(g => g.TypeArgumentList.Arguments.ElementAt(0))
                        .OfType<TypeSyntax>()
                        .Select(t => t.ToString())
                        .Distinct()
                        .ToList();

                var outTypeStr = outTypes.ToArray();

                var enumerableParamCount =
                    mtd.ParameterList.Parameters
                        .Count(
                            c =>
                                c.DescendantNodesAndSelf()
                                    .OfType<SimpleNameSyntax>()
                                    .Any(
                                        x =>
                                            x.Identifier.ValueText == BUILTIN_ENUMERABLE_NAME ||
                                            x.Identifier.ValueText == PLACEHOLDER_ENUMERABLE_NAME ||
                                            x.Identifier.ValueText == CONSTRAINED_BUILTIN_ENUMERABLE_NAME
                                    )
                        );


                var updatedMtd = mtd;

                if (enumerableParamCount <= 1)
                {
                    HandleSinglePlaceholderTemplateMethod(ref updatedDecl, updatedMtd, builtIn, outTypeStr.Single());
                }
                else
                {
                    HandleDoublePlaceholderTemplateMethod(ref updatedDecl, updatedMtd, builtIn, allBuiltIns, allEnumerables, outTypeStr);
                }
            }

            // write the extension methods document
            var newFileName = name + "ExtensionMethods.cs";
            var updatdRoot = root.ReplaceNode(extensionDecl, updatedDecl.WithTriviaFrom(extensionDecl));
            var updatedCode = updatdRoot.ToFullString();

            projects.ModifyOutput(
                p =>
                {
                    var updatedDoc = p.AddDocument(newFileName, updatedCode);

                    return updatedDoc.Project;
                }
            );
        }

        static void HandleSinglePlaceholderTemplateMethod(ref ClassDeclarationSyntax updatedDecl, MethodDeclarationSyntax mtd, EnumerableDetails builtIn, string outTypeStr)
        {
            var expanded = ExpandMethodFromPlaceholders(mtd, new[] { builtIn }, BUILTIN_ENUMERABLE_NAME, BUILTIN_ENUMERATOR_NAME, false);

            var constrainedExpansions = new List<MethodDeclarationSyntax>();
            foreach (var expandedMtd in expanded)
            {
                var updated = ExpandConstrainedBuiltIns(expandedMtd, builtIn, outTypeStr);
                constrainedExpansions.Add(updated);
            }

            foreach (var expandedMtd in constrainedExpansions)
            {
                // slap the type on so we can find it when rewritting later
                var withOutTypeAnnotation = expandedMtd.WithAdditionalAnnotations(new SyntaxAnnotation(OUT_TYPE_ANNOTATION, outTypeStr));
                updatedDecl = updatedDecl.AddMembers(new[] { withOutTypeAnnotation });
            }

            // change methods to be extension methods, and replace placeholders
            var replaces = new Dictionary<SyntaxNode, SyntaxNode>();
            foreach(var workingMtd in updatedDecl.DescendantNodesAndSelf().OfType<MethodDeclarationSyntax>().Where(m => !m.HasAnnotation(METHOD_REWRITTEN)))
            {
                // grab the type we need for the enumerable and enumerator
                var outTypeAnnotation = workingMtd.GetAnnotations(OUT_TYPE_ANNOTATION).SingleOrDefault();

                // did we already handle this elsewhere?
                if (outTypeAnnotation == null)
                {
                    var skipped = workingMtd.WithAdditionalAnnotations(METHOD_REWRITTEN);

                    replaces[workingMtd] = skipped.WithTriviaFrom(workingMtd);
                    continue;
                }

                var workingOutTypeStr = outTypeAnnotation.Data;
                var outType = SyntaxFactory.ParseTypeName(workingOutTypeStr);

                TypeSyntax enumerable, enumerator;
                BindEnumerableAndEnumerator(builtIn, outType, out enumerable, out enumerator);

                // make the method an extension method
                var extension = MakeExtensionMethod(workingMtd);

                // make the dynamic bridge calls call CommonImplementation.Bridge
                var bridged = ReplaceBridgeCalls(extension);

                // slap the appropriate enumerable and enumerator into the various CommonImplementation calls

                MethodDeclarationSyntax parameterized;
                if (!bridged.HasAnnotation(DO_NOT_PARAMETERIZE))
                {
                    parameterized = InjectIdentityAndEnumerator(bridged, enumerable, enumerator);
                }
                else
                {
                    parameterized = bridged;
                }

                parameterized = parameterized.WithAdditionalAnnotations(METHOD_REWRITTEN);

                replaces[workingMtd] = parameterized.WithTriviaFrom(workingMtd);
            }

            updatedDecl = updatedDecl.ReplaceNodes(replaces.Keys, (old, _) => replaces[old]);

            // change methods to be extension methods, and replace placeholders
            //while (true)
            //{
            //    var workingMtd = updatedDecl.DescendantNodesAndSelf().OfType<MethodDeclarationSyntax>().Where(m => !m.HasAnnotation(METHOD_REWRITTEN)).FirstOrDefault();
            //    if (workingMtd == null) break;

            //    // grab the type we need for the enumerable and enumerator
            //    var outTypeAnnotation = workingMtd.GetAnnotations(OUT_TYPE_ANNOTATION).SingleOrDefault();

            //    // did we already handle this elsewhere?
            //    if (outTypeAnnotation == null)
            //    {
            //        var skipped = workingMtd.WithAdditionalAnnotations(METHOD_REWRITTEN);

            //        updatedDecl = updatedDecl.ReplaceNode(workingMtd, skipped.WithTriviaFrom(workingMtd));
            //        continue;
            //    }

            //    var workingOutTypeStr = outTypeAnnotation.Data;
            //    var outType = SyntaxFactory.ParseTypeName(workingOutTypeStr);
                
            //    TypeSyntax enumerable, enumerator;
            //    BindEnumerableAndEnumerator(builtIn, outType, out enumerable, out enumerator);
                
            //    // make the method an extension method
            //    var extension = MakeExtensionMethod(workingMtd);

            //    // make the dynamic bridge calls call CommonImplementation.Bridge
            //    var bridged = ReplaceBridgeCalls(extension);

            //    // slap the appropriate enumerable and enumerator into the various CommonImplementation calls

            //    MethodDeclarationSyntax parameterized;
            //    if (!bridged.HasAnnotation(DO_NOT_PARAMETERIZE))
            //    {
            //        parameterized = InjectIdentityAndEnumerator(bridged, enumerable, enumerator);
            //    }
            //    else
            //    {
            //        parameterized = bridged;
            //    }

            //    parameterized = parameterized.WithAdditionalAnnotations(METHOD_REWRITTEN);

            //    updatedDecl = updatedDecl.ReplaceNode(workingMtd, parameterized.WithTriviaFrom(workingMtd));
            //}
        }

        static MethodDeclarationSyntax ExpandConstrainedBuiltIns(MethodDeclarationSyntax mtd, EnumerableDetails builtIn, string outTypeStr)
        {

            var constraintEnumerablesMentions =
                mtd
                    .DescendantNodesAndSelf()
                    .OfType<SimpleNameSyntax>()
                    .Where(w => w.Identifier.ValueText == CONSTRAINED_BUILTIN_ENUMERABLE_NAME)
                    .ToList();

            var constraintEnumeratorsMentions =
                mtd
                    .DescendantNodesAndSelf()
                    .OfType<SimpleNameSyntax>()
                    .Where(w => w.Identifier.ValueText == CONSTRAINED_BUILTIN_ENUMERATOR_NAME)
                    .ToList();

            if (constraintEnumerablesMentions.Count == 0 || constraintEnumeratorsMentions.Count == 0)
            {
                return mtd;
            }

            var enumerable = builtIn.BridgeEnumerable;
            var enumerator = builtIn.BridgeEnumerator;

            var outType = SyntaxFactory.ParseTypeName(outTypeStr);

            var enumerableOutMentions = enumerable.DescendantNodesAndSelf().OfType<SimpleNameSyntax>().Where(i => i.Identifier.ValueText == builtIn.OutItem).ToList();
            var enumeratorOutMentions = enumerator.DescendantNodesAndSelf().OfType<SimpleNameSyntax>().Where(i => i.Identifier.ValueText == builtIn.OutItem).ToList();

            var boundEnumerable = enumerable.ReplaceNodes(enumerableOutMentions, (old, _) => outType.WithTriviaFrom(old));
            var boundEnumerator = enumerator.ReplaceNodes(enumeratorOutMentions, (old, _) => outType.WithTriviaFrom(old));

            var replacements = new Dictionary<SyntaxNode, SyntaxNode>();

            constraintEnumerablesMentions.ForEach(e => replacements[e] = boundEnumerable);
            constraintEnumeratorsMentions.ForEach(e => replacements[e] = boundEnumerator);

            var updatedMtd = mtd.ReplaceNodes(replacements.Keys, (old, _) => replacements[old].WithTriviaFrom(old));
            updatedMtd = updatedMtd.WithAdditionalAnnotations(DO_NOT_PARAMETERIZE);

            return updatedMtd;
        }

        static void HandleDoublePlaceholderTemplateMethod(
            ref ClassDeclarationSyntax updatedDecl,
            MethodDeclarationSyntax mtd,
            EnumerableDetails builtIn,
            IEnumerable<EnumerableDetails> allBuiltIns,
            IEnumerable<EnumerableDetails> allEnumerables,
            string[] outTypeStrs
        )
        {
            var outTypes = outTypeStrs.Select(t => SyntaxFactory.ParseTypeName(t)).ToArray();
            var expanded = ExpandDoubleParameterizedFromPlaceholders(mtd, outTypes, builtIn, allBuiltIns, allEnumerables);

            var extensions = new List<MethodDeclarationSyntax>();
            foreach (var bound in expanded)
            {
                var firstParam = bound.ParameterList.Parameters.First();

                var withThisStr = THIS.ToFullString() + firstParam.ToFullString();
                var withThis = SyntaxFactory.ParseParameterList(withThisStr).Parameters.ElementAt(0);

                var updated = bound.ReplaceNode(firstParam, withThis);

                var modifiers =
                    new SyntaxToken[]
                    {
                        SyntaxFactory.Token(SyntaxKind.PublicKeyword).WithTriviaFrom(updated.Modifiers.First()),
                        SyntaxFactory.Token(SyntaxKind.StaticKeyword).WithTrailingTrivia(SyntaxFactory.Whitespace(" "))
                    };

                var modifiersList = SyntaxFactory.TokenList(modifiers);
                updated = updated.WithModifiers(modifiersList);

                extensions.Add(updated);
            }

            var bridgesReplaced = new List<MethodDeclarationSyntax>();
            foreach (var extension in extensions)
            {
                var updated = ReplaceBridgeCalls(extension);
                bridgesReplaced.Add(updated);
            }

            var refLocalsReplaced = new List<MethodDeclarationSyntax>();
            foreach (var bridged in bridgesReplaced)
            {
                var updated = bridged;
                ReplaceRefLocalCalls(ref updated);
                refLocalsReplaced.Add(updated);
            }

            var refParamsReplaced = new List<MethodDeclarationSyntax>();
            foreach (var deLocaled in refLocalsReplaced)
            {
                var updated = deLocaled;
                ReplaceRefParamCalls(ref updated);
                refParamsReplaced.Add(updated);
            }

            updatedDecl = updatedDecl.AddMembers(refParamsReplaced.ToArray());
        }

        static void BindEnumerableAndEnumerator(EnumerableDetails details, TypeSyntax newOutType, out TypeSyntax updatedEnumerable, out TypeSyntax updatedEnumerator)
        {
            var baseEnumerable = details.BridgeEnumerable;
            var baseEnumerator = details.BridgeEnumerator;

            var enumerableMentionsOfOutType = baseEnumerable.DescendantNodesAndSelf().OfType<SimpleNameSyntax>().Where(i => i.Identifier.ValueText == details.OutItem).ToList();
            var enumeratorMentionsOfOutType = baseEnumerator.DescendantNodesAndSelf().OfType<SimpleNameSyntax>().Where(i => i.Identifier.ValueText == details.OutItem).ToList();

            updatedEnumerable = baseEnumerable.ReplaceNodes(enumerableMentionsOfOutType, (old, _) => newOutType.WithTriviaFrom(old));
            updatedEnumerator = baseEnumerator.ReplaceNodes(enumeratorMentionsOfOutType, (old, _) => newOutType.WithTriviaFrom(old));
        }

        static IEnumerable<MethodDeclarationSyntax> ExpandDoubleParameterizedFromPlaceholders(
            MethodDeclarationSyntax mtd,
            TypeSyntax[] outTypes,
            EnumerableDetails builtIn,
            IEnumerable<EnumerableDetails> allBuiltIns,
            IEnumerable<EnumerableDetails> allEnumerables
        )
        {
            List<SyntaxNode> firstEnumerables, firstEnumerators, secondEnumerables, secondEnumerators, firstParameterEnumerables, secondParameterEnumerables;
            ExtractEnumerablesAndEnumeratorsInFirstAndSecondPosition(
                mtd,
                out firstEnumerables,
                out secondEnumerables,
                out firstEnumerators,
                out secondEnumerators,
                out firstParameterEnumerables,
                out secondParameterEnumerables
            );

            Func<SyntaxNode, bool, EnumerableDetails, SyntaxNode> makeReplacementEnumerable =
                (node, isFirst, e) =>
                {
                    TypeSyntax outType;
                    if (outTypes.Length == 1)
                    {
                        outType = outTypes[0];
                    }
                    else
                    {
                        outType = outTypes[isFirst ? 0 : 1];
                    }

                    var bridgeEnumerable = e.BridgeEnumerable ?? e.Enumerable;
                    var defaultOutType = e.OutItem;
                    var uses = bridgeEnumerable.DescendantNodesAndSelf().OfType<SimpleNameSyntax>().Where(i => i.Identifier.ValueText == defaultOutType).ToList();
                    var updatedEnumerable = bridgeEnumerable.ReplaceNodes(uses, (old, _) => outType.WithTriviaFrom(old));

                    return updatedEnumerable.WithTriviaFrom(node);
                };
            Func<SyntaxNode, bool, EnumerableDetails, SyntaxNode> makeReplacementEnumerator =
                (node, isFirst, e) =>
                {
                    TypeSyntax outType;
                    if (outTypes.Length == 1)
                    {
                        outType = outTypes[0];
                    }
                    else
                    {
                        outType = outTypes[isFirst ? 0 : 1];
                    }

                    var bridgeEnumerator = e.BridgeEnumerator ?? e.Enumerator;
                    var defaultOutType = e.OutItem;
                    var uses = bridgeEnumerator.DescendantNodesAndSelf().OfType<SimpleNameSyntax>().Where(i => i.Identifier.ValueText == defaultOutType).ToList();
                    var updatedEnumerator = bridgeEnumerator.ReplaceNodes(uses, (old, _) => outType.WithTriviaFrom(old));

                    return updatedEnumerator.WithTriviaFrom(node);
                };
            Func<SyntaxNode, bool, EnumerableDetails, SyntaxNode> makeReplacementSystemEnumerable =
                (node, isFirst, e) =>
                {
                    TypeSyntax outType;
                    if (outTypes.Length == 1)
                    {
                        outType = outTypes[0];
                    }
                    else
                    {
                        outType = outTypes[isFirst ? 0 : 1];
                    }

                    var enumerable = e.Enumerable;
                    var defaultOutType = e.OutItem;
                    var uses = enumerable.DescendantNodesAndSelf().OfType<SimpleNameSyntax>().Where(i => i.Identifier.ValueText == defaultOutType).ToList();
                    var updatedEnumerable = enumerable.ReplaceNodes(uses, (old, _) => outType.WithTriviaFrom(old));

                    return updatedEnumerable.WithTriviaFrom(node);
                };

            var firstReplacements = new Dictionary<SyntaxNode, SyntaxNode>();
            foreach (var enumerable in firstEnumerables)
            {
                firstReplacements[enumerable] = makeReplacementEnumerable(enumerable, true, builtIn);
            }
            foreach (var enumerator in firstEnumerators)
            {
                firstReplacements[enumerator] = makeReplacementEnumerator(enumerator, true, builtIn);
            }
            foreach (var enumerable in firstParameterEnumerables)
            {
                firstReplacements[enumerable] = makeReplacementSystemEnumerable(enumerable, true, builtIn);
            }

            var ret = new List<MethodDeclarationSyntax>();

            var overIntroducedEnumerables = secondEnumerables.All(t => (t as SimpleNameSyntax)?.Identifier.ValueText == PLACEHOLDER_ENUMERABLE_NAME);

            var toExpandTo = overIntroducedEnumerables ? allEnumerables : allBuiltIns;

            foreach (var otherBuiltInUnmodified in toExpandTo)
            {
                var otherBuiltIn = otherBuiltInUnmodified;

                var replacements = new Dictionary<SyntaxNode, SyntaxNode>(firstReplacements);
                var additionalGenericTerms = new List<string>(builtIn.GenericArgs);

                var genericArgsInConflict = otherBuiltIn.GenericArgs.Where(t => additionalGenericTerms.Contains(t)).ToList();

                var oldGenArgtoNew = new Dictionary<string, string>();

                var outType = outTypes.Length == 1 ? outTypes[0] : outTypes[1];
                oldGenArgtoNew[otherBuiltIn.OutItem] = outType.ToFullString().Trim();

                foreach (var inConflict in genericArgsInConflict)
                {
                    var i = 1;
                    var updated = inConflict + i;
                    while (otherBuiltIn.GenericArgs.Contains(updated) || oldGenArgtoNew.ContainsValue(updated))
                    {
                        i++;
                        updated = inConflict + i;
                    }

                    oldGenArgtoNew[inConflict] = updated;
                }

                otherBuiltIn = otherBuiltIn.MapGenericTypeParams(oldGenArgtoNew);

                additionalGenericTerms.AddRange(otherBuiltIn.GenericArgs);

                foreach (var enumerable in secondEnumerables)
                {
                    replacements[enumerable] = makeReplacementEnumerable(enumerable, false, otherBuiltIn);
                }
                foreach (var enumerator in secondEnumerators)
                {
                    replacements[enumerator] = makeReplacementEnumerator(enumerator, false, otherBuiltIn);
                }
                foreach (var enumerable in secondParameterEnumerables)
                {
                    replacements[enumerable] = makeReplacementSystemEnumerable(enumerable, false, otherBuiltIn);
                }

                var pairUpdated = mtd.ReplaceNodes(replacements.Keys, (old, _) => replacements[old]);

                if (additionalGenericTerms.Count > 0)
                {
                    var additionalTypeParams = additionalGenericTerms.Select(t => SyntaxFactory.TypeParameter(t)).ToArray();
                    pairUpdated = pairUpdated.AddTypeParameterListParameters(additionalTypeParams);
                }

                if (otherBuiltIn.Constraints.Count > 0)
                {
                    var constraintList = SyntaxFactory.List(otherBuiltIn.Constraints);
                    pairUpdated = pairUpdated.WithConstraintClauses(constraintList);
                }

                ret.Add(pairUpdated);
            }

            return ret;
        }

        static void ExtractEnumerablesAndEnumeratorsInFirstAndSecondPosition(
            MethodDeclarationSyntax mtd,
            out List<SyntaxNode> firstMentionsEnumerables,
            out List<SyntaxNode> secondMentionsEnumerables,
            out List<SyntaxNode> firstMentionsEnumerators,
            out List<SyntaxNode> secondMentionsEnumerators,
            out List<SyntaxNode> firstParameterEnumerables,
            out List<SyntaxNode> secondParameterEnumerables
        )
        {
            var typeArgLists = mtd.DescendantNodesAndSelf().OfType<TypeArgumentListSyntax>().ToList();
            var relevantTypeArgLists =
                typeArgLists
                    .Where(t => t.Parent is GenericNameSyntax)
                    .Where(t => ((GenericNameSyntax)t.Parent).Identifier.ValueText != "Func")   // technically we should have a proper whitelist, but ehhh
                    .ToList();

            var typeArgsMentioningBuiltIn =
                relevantTypeArgLists
                    .Where(l =>
                        l.Arguments
                            .Any(
                                a => (a as SimpleNameSyntax)?.Identifier.ValueText == BUILTIN_ENUMERABLE_NAME ||
                                     (a as SimpleNameSyntax)?.Identifier.ValueText == PLACEHOLDER_ENUMERABLE_NAME ||
                                     (a as SimpleNameSyntax)?.Identifier.ValueText == CONSTRAINED_BUILTIN_ENUMERABLE_NAME
                            )
                    )
                    .ToList();

            var parameterTypesMentioningBuiltIn =
                mtd.ParameterList.Parameters
                    .Select(p => p.Type)
                    .Where(
                        p =>
                            p.DescendantNodesAndSelf()
                                .OfType<SimpleNameSyntax>()
                                .Any(
                                    x =>
                                        x.Identifier.ValueText == BUILTIN_ENUMERABLE_NAME ||
                                        x.Identifier.ValueText == PLACEHOLDER_ENUMERABLE_NAME ||
                                        x.Identifier.ValueText == CONSTRAINED_BUILTIN_ENUMERABLE_NAME
                                )
                    )
                    .ToList();

            firstMentionsEnumerables = new List<SyntaxNode>();
            firstMentionsEnumerators = new List<SyntaxNode>();

            secondMentionsEnumerables = new List<SyntaxNode>();
            secondMentionsEnumerators = new List<SyntaxNode>();

            firstParameterEnumerables = new List<SyntaxNode>();
            secondParameterEnumerables = new List<SyntaxNode>();

            foreach (var argList in typeArgsMentioningBuiltIn)
            {
                var isFirstEnumerable = true;
                var isFirstEnumerator = true;

                foreach (var arg in argList.Arguments)
                {
                    var name = (arg as SimpleNameSyntax)?.Identifier.ValueText;

                    if (name == BUILTIN_ENUMERABLE_NAME || name == PLACEHOLDER_ENUMERABLE_NAME || name == CONSTRAINED_BUILTIN_ENUMERABLE_NAME)
                    {
                        if (isFirstEnumerable)
                        {
                            firstMentionsEnumerables.Add(arg);
                            isFirstEnumerable = false;
                        }
                        else
                        {
                            secondMentionsEnumerables.Add(arg);
                        }
                        continue;
                    }

                    if (name == BUILTIN_ENUMERATOR_NAME || name == PLACEHOLDER_ENUMERATOR_NAME || name == CONSTRAINED_BUILTIN_ENUMERATOR_NAME)
                    {
                        if (isFirstEnumerator)
                        {
                            firstMentionsEnumerators.Add(arg);
                            isFirstEnumerator = false;
                        }
                        else
                        {
                            secondMentionsEnumerators.Add(arg);
                        }
                        continue;
                    }
                }
            }

            {

                var isFirstEnumerable = true;

                foreach (var pType in parameterTypesMentioningBuiltIn)
                {
                    var name = (pType as SimpleNameSyntax)?.Identifier.ValueText;

                    // should be more general than this, but meh
                    if (name == "Func")
                    {
                        foreach (var p in ((GenericNameSyntax)pType).TypeArgumentList.Arguments)
                        {
                            var funcParamName = (p as SimpleNameSyntax)?.Identifier.ValueText;
                            if (funcParamName == null) continue;

                            if (funcParamName == BUILTIN_ENUMERABLE_NAME || funcParamName == PLACEHOLDER_ENUMERABLE_NAME || funcParamName == CONSTRAINED_BUILTIN_ENUMERABLE_NAME)
                            {
                                secondParameterEnumerables.Add(p);
                            }
                        }

                        continue;
                    }

                    if (name == BUILTIN_ENUMERABLE_NAME || name == PLACEHOLDER_ENUMERABLE_NAME || name == CONSTRAINED_BUILTIN_ENUMERABLE_NAME)
                    {
                        if (isFirstEnumerable)
                        {
                            firstParameterEnumerables.Add(pType);
                            isFirstEnumerable = false;
                        }
                        else
                        {
                            secondParameterEnumerables.Add(pType);
                        }

                        continue;
                    }
                }
            }
        }

        static MethodDeclarationSyntax ReplaceBridgeCalls(MethodDeclarationSyntax mtd)
        {
            var updated = mtd;

            var invocations =
                (mtd.Body?.DescendantNodesAndSelf() ??
                 mtd.ExpressionBody?.DescendantNodesAndSelf())
                    .OfType<InvocationExpressionSyntax>()
                    .ToList();

            var replacements = new Dictionary<InvocationExpressionSyntax, InvocationExpressionSyntax>();

            foreach (var invocation in invocations)
            {
                var exp = invocation.Expression;
                if (!(exp is IdentifierNameSyntax)) continue;

                var ident = (IdentifierNameSyntax)exp;
                if (ident.Identifier.ValueText != BRIDGE_NAME) continue;

                var newInvoke = invocation.WithExpression(BRIDGE);

                replacements[invocation] = newInvoke;
            }

            updated = updated.ReplaceNodes(replacements.Keys, (old, _) => replacements[old].WithTriviaFrom(old));

            return updated;
        }

        const string FORBIDDEN_CALL_NAME = "ForbiddenCall";
        const string UNEXPECTED_PATH_NAME = "UnexpectedPath";
        const string UNINITIALIZED_NAME = "Uninitialized";
        const string ARGUMENT_NULL_NAME = "ArgumentNull";
        const string SEQUENCE_EMPTY_NAME = "SequenceEmpty";
        const string OUT_OF_RANGE_NAME = "OutOfRange";
        const string NO_ITEMS_MATCHED_NAME = "NoItemsMatched";
        const string MULTIPLE_MATCHING_ELEMENTS_NAME = "MultipleMatchingElements";
        const string MULTIPLE_ELEMENTS_NAME = "MultipleElements";
        const string UNINITIALIZED_PROJECTION_NAME = "UninitializedProjection";
        const string UNEXPECTED_STATE_NAME = "UnexpectedState";
        const string NOT_IMPLEMENTED_NAME = "NotImplemented";
        const string INNER_UNINITIALIZED_NAME = "InnerUninitialized";
        const string INVALID_OPERATION_NAME = "InvalidOperation";

        static MethodDeclarationSyntax InjectIdentityAndEnumerator(MethodDeclarationSyntax mtd, TypeSyntax enumerable, TypeSyntax enumerator)
        {
            var updated = mtd;
            
            // replace calls to CommonImplementation methods so that the appropriate enumerable and enumerators
            //   are used
            var commonNonBridge =
                (updated.Body?.DescendantNodesAndSelf() ??
                 updated.ExpressionBody?.DescendantNodesAndSelf()
                )
                .OfType<InvocationExpressionSyntax>()
                .Where(o => o.Expression is MemberAccessExpressionSyntax)
                .Where(o => ((MemberAccessExpressionSyntax)o.Expression).Expression is IdentifierNameSyntax)
                .Where(o => ((IdentifierNameSyntax)((MemberAccessExpressionSyntax)o.Expression).Expression).Identifier.ValueText == COMMON_IMPLEMENTATION_NAME)
                .Where(o => ((MemberAccessExpressionSyntax)o.Expression).Name.Identifier.ValueText != BRIDGE_NAME)
                .Where(o => ((MemberAccessExpressionSyntax)o.Expression).Name.Identifier.ValueText != FORBIDDEN_CALL_NAME)
                .Where(o => ((MemberAccessExpressionSyntax)o.Expression).Name.Identifier.ValueText != UNEXPECTED_PATH_NAME)
                .Where(o => ((MemberAccessExpressionSyntax)o.Expression).Name.Identifier.ValueText != UNINITIALIZED_NAME)
                .Where(o => ((MemberAccessExpressionSyntax)o.Expression).Name.Identifier.ValueText != ARGUMENT_NULL_NAME)
                .Where(o => ((MemberAccessExpressionSyntax)o.Expression).Name.Identifier.ValueText != SEQUENCE_EMPTY_NAME)
                .Where(o => ((MemberAccessExpressionSyntax)o.Expression).Name.Identifier.ValueText != OUT_OF_RANGE_NAME)
                .Where(o => ((MemberAccessExpressionSyntax)o.Expression).Name.Identifier.ValueText != NO_ITEMS_MATCHED_NAME)
                .Where(o => ((MemberAccessExpressionSyntax)o.Expression).Name.Identifier.ValueText != MULTIPLE_MATCHING_ELEMENTS_NAME)
                .Where(o => ((MemberAccessExpressionSyntax)o.Expression).Name.Identifier.ValueText != MULTIPLE_ELEMENTS_NAME)
                .Where(o => ((MemberAccessExpressionSyntax)o.Expression).Name.Identifier.ValueText != UNINITIALIZED_PROJECTION_NAME)
                .Where(o => ((MemberAccessExpressionSyntax)o.Expression).Name.Identifier.ValueText != UNEXPECTED_STATE_NAME)
                .Where(o => ((MemberAccessExpressionSyntax)o.Expression).Name.Identifier.ValueText != NOT_IMPLEMENTED_NAME)
                .Where(o => ((MemberAccessExpressionSyntax)o.Expression).Name.Identifier.ValueText != INNER_UNINITIALIZED_NAME)
                .Where(o => ((MemberAccessExpressionSyntax)o.Expression).Name.Identifier.ValueText != INVALID_OPERATION_NAME)
                .ToList();

            var replacements = new Dictionary<InvocationExpressionSyntax, InvocationExpressionSyntax>();

            foreach (var invocation in commonNonBridge)
            {
                var method = ((MemberAccessExpressionSyntax)invocation.Expression).Name;
                GenericNameSyntax genericMethod;
                if (method is GenericNameSyntax)
                {
                    genericMethod = (GenericNameSyntax)method;
                }
                else
                {
                    genericMethod = SyntaxFactory.GenericName(method.Identifier);
                }

                var withMoreTerms = genericMethod.AddTypeArgumentListArguments(enumerable, enumerator);
                var commonAccess = ((MemberAccessExpressionSyntax)invocation.Expression).WithName(withMoreTerms);
                var updatedInvocation = invocation.WithExpression(commonAccess);

                replacements[invocation] = updatedInvocation;
            }

            updated = updated.ReplaceNodes(replacements.Keys, (old, _) => replacements[old].WithTriviaFrom(old));

            var retVal = updated.ReturnType;

            if (retVal != null)
            {
                var updatedRetVal = retVal;

                var mentionsOfEnumerable = updatedRetVal.DescendantNodesAndSelf().OfType<SimpleNameSyntax>().Where(t => t.Identifier.ValueText == BUILTIN_ENUMERABLE_NAME).ToList();
                updatedRetVal = updatedRetVal.ReplaceNodes(mentionsOfEnumerable, (old, _) => enumerable.WithTriviaFrom(old));

                var mentionsOfEnumerator = updatedRetVal.DescendantNodesAndSelf().OfType<SimpleNameSyntax>().Where(t => t.Identifier.ValueText == BUILTIN_ENUMERATOR_NAME).ToList();
                updatedRetVal = updatedRetVal.ReplaceNodes(mentionsOfEnumerator, (old, _) => enumerator.WithTriviaFrom(old));

                updated = updated.WithReturnType(updatedRetVal.WithTriviaFrom(retVal));
            }

            return updated;
        }
    }
}