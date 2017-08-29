using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis;
using System.Linq;
using System.Collections.Generic;
using System;

namespace LinqAF.Generator
{
    abstract class ExtensionMethodsBase : IAddOperation
    {
        const string NAME_TEMPLATE_VAR = "{Name}";
        const string EXTENSION_METHODS_CLASS_TEMPLATE =
@"using System.Collections.Generic;
using LinqAF.Impl;
using System;
using LinqAF.Config;

namespace LinqAF
{
    public static class {Name}ExtensionMethods
    {
    }
}";
        protected const string PLACEHOLDER_ENUMERABLE_NAME = "PlaceholderEnumerable";
        protected const string PLACEHOLDER_ENUMERATOR_NAME = "PlaceholderEnumerator";
        protected static readonly ThisExpressionSyntax THIS = SyntaxFactory.ThisExpression().WithTrailingTrivia(SyntaxFactory.SyntaxTrivia(SyntaxKind.WhitespaceTrivia, " "));
        const string REF_LOCAL_PLACEHOLDER = "RefLocal";

        const string REF_PARAM_PLACEHOLDER = "RefParam";

        static protected readonly SyntaxAnnotation METHOD_REWRITTEN = new SyntaxAnnotation(nameof(ExtensionMethodsBase) + "." + nameof(METHOD_REWRITTEN));
        static protected readonly SyntaxAnnotation METHOD_ON_BRIDGE_TYPE = new SyntaxAnnotation(nameof(ExtensionMethodsBase) + "." + nameof(METHOD_ON_BRIDGE_TYPE));
        static protected readonly SyntaxAnnotation DO_NOT_PARAMETERIZE = new SyntaxAnnotation(nameof(ExtensionMethodsBase) + "." + nameof(DO_NOT_PARAMETERIZE));

        protected class EnumerableDetails
        {
            public TypeSyntax Enumerable { get; set; }
            public NameSyntax Enumerator { get; set; }
            public string OutItem { get; set; }
            public List<string> GenericArgs { get; set; }
            public List<TypeParameterConstraintClauseSyntax> Constraints { get; set; }

            public bool IsBridgeType { get; set; }
            public TypeSyntax BridgeEnumerable { get; set; }
            public TypeSyntax BridgeEnumerator { get; set; }

            static TSyntaxNode ReplaceNodes<TSyntaxNode>(TSyntaxNode node, string name, TypeSyntax replacement)
                where TSyntaxNode: SyntaxNode
            {
                var uses = node.DescendantNodesAndSelf().OfType<SimpleNameSyntax>().Where(i => i.Identifier.ValueText == name).ToList();

                return node.ReplaceNodes(uses, (old, _) => replacement.WithTriviaFrom(old));
            }

            static TSyntaxNode ReplaceNodes<TSyntaxNode>(TSyntaxNode node, IEnumerable<KeyValuePair<string, TypeSyntax>> replacements)
                where TSyntaxNode: SyntaxNode
            {
                if (node == null) return null;

                var ret = node;

                foreach(var replacement in replacements)
                {
                    ret = ReplaceNodes(ret, replacement.Key, replacement.Value);
                }

                return ret;
            }

            public EnumerableDetails MapGenericTypeParams(IReadOnlyDictionary<string, string> map)
            {
                var syntaxMap = map.ToDictionary(kv => kv.Key, kv => SyntaxFactory.ParseTypeName(kv.Value));

                var updatedEnumerable = ReplaceNodes(Enumerable, syntaxMap);
                var updatedEnumerator = ReplaceNodes(Enumerator, syntaxMap);
                var updatedOutItem = map.ContainsKey(OutItem) ? map[OutItem] : OutItem;
                var updatedGenericArgs =
                    GenericArgs
                        .Where(g => !map.ContainsKey(g))
                        .Concat(
                            GenericArgs
                                .Where(g => map.ContainsKey(g))
                                .Select(g => map[g])
                        )
                        .ToList();
                var updatedConstraints = Constraints.Select(c => ReplaceNodes(c, syntaxMap)).ToList();
                var updatedBridgeEnumerable = ReplaceNodes(BridgeEnumerable, syntaxMap);
                var updatedBridgeEnumerator = ReplaceNodes(BridgeEnumerator, syntaxMap);

                return
                    new EnumerableDetails
                    {
                        BridgeEnumerable = updatedBridgeEnumerable,
                        BridgeEnumerator = updatedBridgeEnumerator,
                        Constraints = updatedConstraints,
                        Enumerable = updatedEnumerable,
                        Enumerator = updatedEnumerator,
                        GenericArgs = updatedGenericArgs,
                        IsBridgeType = IsBridgeType,
                        OutItem = updatedOutItem
                    };
            }
        }

        public abstract string GetExtensionMethodBaseClassName();

        public virtual void Add(Projects projects)
        {
            var name = GetExtensionMethodBaseClassName();
            var fileName = name + "ExtensionMethodsBase.cs";

            var templateDoc = projects.Template.Documents.Single(d => d.Name == fileName);
            var parsed = templateDoc.GetSyntaxRootAsync().Result;

            // create the initial template
            CompilationUnitSyntax root;
            ClassDeclarationSyntax extensionDecl;
            CreateInitialTemplate(name, out root, out extensionDecl);

            // find all the enumerables we need to have extension methods on
            var allEnumerables = GetEnumerables(projects);

            // let's only care about the one's we can treat uniformly
            var enumerables = allEnumerables.Where(e => !e.IsBridgeType).ToList();

            // create the methods for each enumerable
            var updatedDecl = extensionDecl;
            foreach (var mtd in parsed.DescendantNodesAndSelf().OfType<MethodDeclarationSyntax>())
            {
                var expanded = ExpandMethodFromPlaceholders(mtd, enumerables, PLACEHOLDER_ENUMERABLE_NAME, PLACEHOLDER_ENUMERATOR_NAME, true);
                updatedDecl = updatedDecl.AddMembers(expanded.ToArray());
            }

            // change methods to be extension methods, and replace placeholders
            var replacements = new Dictionary<SyntaxNode, SyntaxNode>();

            foreach(var mtd in updatedDecl.DescendantNodesAndSelf().OfType<MethodDeclarationSyntax>().Where(m => !m.HasAnnotation(METHOD_REWRITTEN)))
            {
                var rewritten = MakeExtensionMethod(mtd);
                rewritten = rewritten.WithAdditionalAnnotations(METHOD_REWRITTEN);

                replacements[mtd] = rewritten.WithTriviaFrom(mtd);
            }

            updatedDecl = updatedDecl.ReplaceNodes(replacements.Keys, (old, _) => replacements[old]);

            //while (true)
            //{
            //    var mtd = updatedDecl.DescendantNodesAndSelf().OfType<MethodDeclarationSyntax>().Where(m => !m.HasAnnotation(METHOD_REWRITTEN)).FirstOrDefault();
            //    if (mtd == null) break;

            //    var rewritten = MakeExtensionMethod(mtd);
            //    rewritten = rewritten.WithAdditionalAnnotations(METHOD_REWRITTEN);

            //    updatedDecl = updatedDecl.ReplaceNode(mtd, rewritten.WithTriviaFrom(mtd));
            //}

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

        protected static void CreateInitialTemplate(string name, out CompilationUnitSyntax root, out ClassDeclarationSyntax extensionDecl)
        {
            // create the initial template
            var initialText = EXTENSION_METHODS_CLASS_TEMPLATE.Replace(NAME_TEMPLATE_VAR, name);
            root = SyntaxFactory.ParseCompilationUnit(initialText);
            extensionDecl = root.DescendantNodesAndSelf().OfType<ClassDeclarationSyntax>().Single();
        }
        
        static EnumerableDetails BridgeType(string bridgeType, string bridgeOutItem, string enumerableType, string enumeratorType, params string[] genericArgs)
        {
            return
                new EnumerableDetails
                {
                    Constraints = new List<TypeParameterConstraintClauseSyntax>(),
                    Enumerable = SyntaxFactory.ParseTypeName(bridgeType),
                    Enumerator = null,
                    GenericArgs = genericArgs.ToList(),
                    OutItem = bridgeOutItem,

                    IsBridgeType = true,

                    BridgeEnumerable = SyntaxFactory.ParseTypeName(enumerableType),
                    BridgeEnumerator = SyntaxFactory.ParseTypeName(enumeratorType)
                };
        }

        protected static IEnumerable<EnumerableDetails> GetEnumerables(Projects projects)
        {
            var ret = new List<EnumerableDetails>();

            // shove the bridge types in up front, so we always remember to parameterize over them
            ret.Add(
                BridgeType(
                    "TItem[]",
                    "TItem",
                    "IdentityEnumerable<TItem, TItem[], ArrayBridger<TItem>, ArrayEnumerator<TItem>>",
                    "ArrayEnumerator<TItem>"
                )
            );
            ret.Add(
                BridgeType(
                    "IEnumerable<TItem>",
                    "TItem",
                    "IdentityEnumerable<TItem, IEnumerable<TItem>, IEnumerableBridger<TItem>, IdentityEnumerator<TItem>>",
                    "IdentityEnumerator<TItem>"
                )
            );
            ret.Add(
                BridgeType(
                    "Dictionary<TOutItem, TDictionaryValue>.KeyCollection", 
                    "TOutItem",
                    "IdentityEnumerable<TOutItem, Dictionary<TOutItem, TDictionaryValue>.KeyCollection, DictionaryKeysBridger<TOutItem, TDictionaryValue>, DictionaryKeysEnumerator<TOutItem, TDictionaryValue>>",
                    "DictionaryKeysEnumerator<TOutItem, TDictionaryValue>",
                    "TDictionaryValue")
                );
            ret.Add(
                BridgeType(
                    "Dictionary<TDictionaryKey, TOutItem>.ValueCollection", 
                    "TOutItem",
                    "IdentityEnumerable<TOutItem, Dictionary<TDictionaryKey, TOutItem>.ValueCollection, DictionaryValuesBridger<TDictionaryKey, TOutItem>, DictionaryValuesEnumerator<TDictionaryKey, TOutItem>>",
                    "DictionaryValuesEnumerator<TDictionaryKey, TOutItem>",
                    "TDictionaryKey"
                )
            );
            ret.Add(
                BridgeType(
                    "HashSet<TItem>", 
                    "TItem",
                    "IdentityEnumerable<TItem, HashSet<TItem>, HashSetBridger<TItem>, HashSetEnumerator<TItem>>",
                    "HashSetEnumerator<TItem>"
                )
            );
            ret.Add(
                BridgeType(
                    "LinkedList<TItem>", 
                    "TItem",
                    "IdentityEnumerable<TItem, LinkedList<TItem>, LinkedListBridger<TItem>, LinkedListEnumerator<TItem>>",
                    "LinkedListEnumerator<TItem>"
                )
            );
            ret.Add(
                BridgeType(
                    "List<TItem>", 
                    "TItem",
                    "IdentityEnumerable<TItem, List<TItem>, ListBridger<TItem>, ListEnumerator<TItem>>",
                    "ListEnumerator<TItem>"
                )
            );
            ret.Add(
                BridgeType(
                    "Queue<TItem>", 
                    "TItem",
                    "IdentityEnumerable<TItem, Queue<TItem>, QueueBridger<TItem>, QueueEnumerator<TItem>>",
                    "QueueEnumerator<TItem>"
                )
            );
            ret.Add(
                BridgeType(
                    "SortedDictionary<TOutItem, TDictionaryValue>.KeyCollection", 
                    "TOutItem",
                    "IdentityEnumerable<TOutItem, SortedDictionary<TOutItem, TDictionaryValue>.KeyCollection, SortedDictionaryKeysBridger<TOutItem, TDictionaryValue>, SortedDictionaryKeysEnumerator<TOutItem, TDictionaryValue>>",
                    "SortedDictionaryKeysEnumerator<TOutItem, TDictionaryValue>",
                    "TDictionaryValue"
                )
            );
            ret.Add(
                BridgeType(
                    "SortedDictionary<TDictionaryKey, TOutItem>.ValueCollection", 
                    "TOutItem",
                    "IdentityEnumerable<TOutItem, SortedDictionary<TDictionaryKey, TOutItem>.ValueCollection, SortedDictionaryValuesBridger<TDictionaryKey, TOutItem>, SortedDictionaryValuesEnumerator<TDictionaryKey, TOutItem>>",
                    "SortedDictionaryValuesEnumerator<TDictionaryKey, TOutItem>",
                    "TDictionaryKey"
                )
            );
            ret.Add(
                BridgeType(
                    "SortedSet<TItem>", 
                    "TItem",
                    "IdentityEnumerable<TItem, SortedSet<TItem>, SortedSetBridger<TItem>, SortedSetEnumerator<TItem>>",
                    "SortedSetEnumerator<TItem>"
                )
            );
            ret.Add(
                BridgeType(
                    "Stack<TItem>", 
                    "TItem",
                    "IdentityEnumerable<TItem, Stack<TItem>, StackBridger<TItem>, StackEnumerator<TItem>>",
                    "StackEnumerator<TItem>"
                )
            );

            foreach (var pair in projects.EnumerableDocumentsWithNames)
            {
                var enumerableName = pair.Name;

                if (enumerableName == "LookupDefaultEnumerable") continue;
                if (enumerableName == "LookupSpecificEnumerable") continue;
                if (enumerableName == "GroupByDefaultEnumerable") continue;
                if (enumerableName == "GroupBySpecificEnumerable") continue;

                var root = pair.Document.GetSyntaxRootAsync().Result;
                var enumeratorName = pair.Name.Substring(0, pair.Name.Length - "Enumerable".Length) + "Enumerator";
                var enumerableStruct = root.DescendantNodesAndSelf().OfType<StructDeclarationSyntax>().Single(s => s.Identifier.ValueText == enumerableName);
                var enumerableStr = enumerableStruct.Identifier.ToFullString() + enumerableStruct.TypeParameterList.ToFullString();
                var enumerable = (GenericNameSyntax)SyntaxFactory.ParseTypeName(enumerableStr);

                var baseTypes = enumerableStruct.BaseList.Types.Select(t => t.Type).ToList();
                var baseTypeNames = baseTypes.OfType<GenericNameSyntax>().ToList();
                var ienumerable = baseTypeNames.Single(s => s.Identifier.ValueText == Writer.ENUMERABLE_INTERFACE_NAME);
                var outItem = ((SimpleNameSyntax)ienumerable.TypeArgumentList.Arguments.ElementAt(0)).Identifier.ValueText;
                var enumerator = (NameSyntax)ienumerable.TypeArgumentList.Arguments.ElementAt(1);

                var genericTypes = enumerable.TypeArgumentList.Arguments.OfType<SimpleNameSyntax>().Select(t => t.Identifier.ValueText).Where(t => t != outItem).ToList();
                var genericConstraints =
                    enumerableStruct.ConstraintClauses.OfType<TypeParameterConstraintClauseSyntax>().Where(t => genericTypes.Contains(t.Name.Identifier.ValueText)).ToList();

                ret.Add(
                    new EnumerableDetails
                    {
                        Enumerable = enumerable,
                        Enumerator = enumerator,
                        OutItem = outItem,
                        GenericArgs = genericTypes,
                        Constraints = genericConstraints,
                        IsBridgeType = false
                    }
                );
            }

            return ret;
        }

        static protected IEnumerable<MethodDeclarationSyntax> ExpandMethodFromPlaceholders(
            MethodDeclarationSyntax template, 
            IEnumerable<EnumerableDetails> enumerables,
            string placeHolderEnumerableName,
            string placeHolderEnumeratorName,
            bool includeReturnTypes
        )
        {
            var ret = new List<MethodDeclarationSyntax>();

            Func<SyntaxNode, bool> inReturn = null;
            inReturn =
                node =>
                {
                    if (node.Parent == null) return false;

                    var isPartOfMethod = node.Parent is MethodDeclarationSyntax;
                    if (!isPartOfMethod) return inReturn(node.Parent);

                    var parentMethod = (MethodDeclarationSyntax)node.Parent;

                    if (parentMethod.ReturnType == null) return false;

                        // hit the containing method, so it's make or break time
                        return node == parentMethod.ReturnType;
                };

            var mentionsOfPlaceholderEnumerable = 
                template.DescendantNodesAndSelf().OfType<SimpleNameSyntax>().Where(s => s.Identifier.ValueText == placeHolderEnumerableName).ToList();
            var mentionsOfPlaceholderEnumerator = 
                template.DescendantNodesAndSelf().OfType<SimpleNameSyntax>().Where(s => s.Identifier.ValueText == placeHolderEnumeratorName).ToList();
            
            if (!includeReturnTypes)
            {
                var inReturnEnumerables = mentionsOfPlaceholderEnumerable.Where(n => inReturn(n)).ToList();
                var inReturnEnumerators = mentionsOfPlaceholderEnumerator.Where(n => inReturn(n)).ToList();

                mentionsOfPlaceholderEnumerable = mentionsOfPlaceholderEnumerable.Except(inReturnEnumerables).ToList();
                mentionsOfPlaceholderEnumerator = mentionsOfPlaceholderEnumerator.Except(inReturnEnumerators).ToList();
            }

            // no changes to be made, leave it alone
            if (mentionsOfPlaceholderEnumerable.Count == 0 && mentionsOfPlaceholderEnumerator.Count == 0)
            {
                ret.Add(template);
                return ret;
            }

            var outTypes =
                mentionsOfPlaceholderEnumerable
                    .OfType<GenericNameSyntax>()
                    .Concat(mentionsOfPlaceholderEnumerator.OfType<GenericNameSyntax>())
                    .Select(g => g.TypeArgumentList.Arguments.ElementAt(0))
                    .OfType<TypeSyntax>()
                    .Select(t => t.ToString())
                    .Distinct()
                    .ToList();

            if (outTypes.Count > 1)
            {
                throw new Exception("Expected only a single out type in extension method placeholder usage");
            }

            var outTypeStr = outTypes.Single();
            var outType = SyntaxFactory.ParseTypeName(outTypeStr);

            foreach (var pair in enumerables)
            {
                var updatedMtd = template;

                // replace all the uses of the out item with whatever is bound in the template
                var enumerableOutTypeUses = pair.Enumerable.DescendantNodesAndSelf().OfType<SimpleNameSyntax>().Where(t => t.Identifier.ValueText == pair.OutItem).ToList();
                var enumeratorOutTypeUses =
                    pair.Enumerator != null ?
                        pair.Enumerator.DescendantNodesAndSelf().OfType<SimpleNameSyntax>().Where(t => t.Identifier.ValueText == pair.OutItem).ToList() :
                        new List<SimpleNameSyntax>();

                // rework enumerable and enumerator to bind to the appropriate type
                var boundEnumerable = pair.Enumerable.ReplaceNodes(enumerableOutTypeUses, (old, _) => outType.WithTriviaFrom(old));
                var boundEnumerator = pair.Enumerator?.ReplaceNodes(enumeratorOutTypeUses, (old, _) => outType.WithTriviaFrom(old));

                var dontInjectIntoCommon = false;
                var attrs = updatedMtd.AttributeLists.SelectMany(a => a.Attributes).ToList();
                var dnpAttrs = attrs.Where(a => (a.Name as IdentifierNameSyntax)?.Identifier.ValueText == "DoNotInject").ToList();
                if (dnpAttrs.Any())
                {
                    var attrKeeps = new List<AttributeSyntax>(attrs);
                    foreach (var attr in dnpAttrs)
                    {
                        attrKeeps.Remove(attr);
                    }

                    if (attrKeeps.Count == 0)
                    {
                        updatedMtd = updatedMtd.RemoveNodes(updatedMtd.AttributeLists, SyntaxRemoveOptions.KeepLeadingTrivia);
                    }
                    else
                    {
                        var attrListSyntax = SyntaxFactory.AttributeList().AddAttributes(attrKeeps.ToArray());
                        var list = SyntaxFactory.List(new[] { attrListSyntax });

                        updatedMtd = updatedMtd.WithAttributeLists(list);
                    }

                    dontInjectIntoCommon = true;
                }

                if (dontInjectIntoCommon)
                {
                    Func<SimpleNameSyntax, bool> inParameterList =
                        p =>
                        {
                            var pList = updatedMtd.ParameterList;

                            return pList.Parameters.Any(x => x.Type.Equals(p));
                        };

                    var replace = new Dictionary<SimpleNameSyntax, SyntaxNode>();

                    var bridingeEnumerableOutTypeUses = pair.BridgeEnumerable.DescendantNodesAndSelf().OfType<SimpleNameSyntax>().Where(t => t.Identifier.ValueText == pair.OutItem).ToList();
                    var bridingeEnumeratorOutTypeUses = pair.BridgeEnumerator.DescendantNodesAndSelf().OfType<SimpleNameSyntax>().Where(t => t.Identifier.ValueText == pair.OutItem).ToList();

                    var bridgingEnumerable = pair.BridgeEnumerable.ReplaceNodes(bridingeEnumerableOutTypeUses, (old, _) => outType.WithTriviaFrom(old));
                    var bridgingEnumerator = pair.BridgeEnumerator.ReplaceNodes(bridingeEnumeratorOutTypeUses, (old, _) => outType.WithTriviaFrom(old));

                    var toReplaceEnumerables = updatedMtd.DescendantNodesAndSelf().OfType<SimpleNameSyntax>().Where(s => s.Identifier.ValueText == placeHolderEnumerableName).ToList();
                    var toReplaceEnumerators = updatedMtd.DescendantNodesAndSelf().OfType<SimpleNameSyntax>().Where(s => s.Identifier.ValueText == placeHolderEnumeratorName).ToList();

                    foreach(var e in toReplaceEnumerables)
                    {
                        replace[e] = bridgingEnumerable;
                    }

                    foreach(var e in toReplaceEnumerators)
                    {
                        replace[e] = bridgingEnumerator;
                    }

                    var inParams = new List<SimpleNameSyntax>();
                    foreach(var kv in replace)
                    {
                        if (inParameterList(kv.Key))
                        {
                            inParams.Add(kv.Key);
                        }
                    }

                    foreach(var p in inParams)
                    {
                        replace[p] = boundEnumerable;
                    }

                    updatedMtd = updatedMtd.ReplaceNodes(replace.Keys, (old, _) => replace[old].WithTriviaFrom(old));
                    updatedMtd = updatedMtd.WithAdditionalAnnotations(DO_NOT_PARAMETERIZE);
                }
                else
                {
                    // replace the old enumerable and enumerator references
                    updatedMtd = updatedMtd.ReplaceNodes(mentionsOfPlaceholderEnumerable, (old, _) => boundEnumerable.WithTriviaFrom(old));

                    var updatedMentionsOfPlaceholderEnumerator = updatedMtd.DescendantNodesAndSelf().OfType<SimpleNameSyntax>().Where(s => s.Identifier.ValueText == placeHolderEnumeratorName).ToList();

                    if (!includeReturnTypes)
                    {
                        var enumeratorsInReturn = updatedMentionsOfPlaceholderEnumerator.Where(e => inReturn(e)).ToList();
                        updatedMentionsOfPlaceholderEnumerator = updatedMentionsOfPlaceholderEnumerator.Except(enumeratorsInReturn).ToList();
                    }

                    if (boundEnumerator != null)
                    {
                        updatedMtd = updatedMtd.ReplaceNodes(updatedMentionsOfPlaceholderEnumerator, (old, _) => boundEnumerator.WithTriviaFrom(old));
                    }
                    else
                    {
                        updatedMtd = updatedMtd.RemoveNodes(updatedMentionsOfPlaceholderEnumerator, SyntaxRemoveOptions.KeepNoTrivia);
                    }
                }

                // rewrite any type constraints so that they refer to the new out item too
                var updatedConstraints = new List<TypeParameterConstraintClauseSyntax>();
                updatedConstraints.AddRange(template.ConstraintClauses);

                foreach (var constraint in pair.Constraints)
                {
                    var constraitOutTypeUses = constraint.DescendantNodesAndSelf().OfType<SimpleNameSyntax>().Where(t => t.Identifier.ValueText == pair.OutItem).ToList();
                    var updatedConstraint = constraint.ReplaceNodes(constraitOutTypeUses, (old, _) => outType.WithTriviaFrom(old));
                    updatedConstraints.Add(updatedConstraint);
                }

                updatedMtd = updatedMtd.WithConstraintClauses(SyntaxFactory.List(updatedConstraints));

                // slam all the generic types that need to be known into place
                var typeList = new List<TypeParameterSyntax>();
                foreach (var param in pair.GenericArgs)
                {
                    typeList.Add(SyntaxFactory.TypeParameter(param));
                }

                if (typeList.Count > 0)
                {
                    updatedMtd = updatedMtd.AddTypeParameterListParameters(typeList.ToArray());
                }

                if (pair.IsBridgeType && !dontInjectIntoCommon)
                {
                    // bridge types are handled with lots of specificly parameterized methods in CommonImplementation, so remove
                    //   any of the direct mentions in type argument lists in the body
                    updatedMtd = updatedMtd.WithAdditionalAnnotations(METHOD_ON_BRIDGE_TYPE);

                    var bodyGenericTypeArgs = 
                        (updatedMtd.Body?.DescendantNodesAndSelf() ?? updatedMtd.ExpressionBody?.DescendantNodesAndSelf())
                            .OfType<TypeArgumentListSyntax>()
                            .SelectMany(t => t.Arguments)
                            .ToList();

                    var needRemoval = bodyGenericTypeArgs.Where(b => b.IsEquivalentTo(boundEnumerable)).ToList();

                    updatedMtd = updatedMtd.RemoveNodes(needRemoval, SyntaxRemoveOptions.KeepNoTrivia);

                    var replacements = new Dictionary<SyntaxNode, SyntaxNode>();

                    foreach(var withEmptyTypeArgs in (updatedMtd.Body?.DescendantNodesAndSelf() ?? updatedMtd.ExpressionBody?.DescendantNodesAndSelf()).OfType<TypeArgumentListSyntax>().Where(t => t.Arguments.Count == 0))
                    {
                        var parent = (GenericNameSyntax)withEmptyTypeArgs.Parent;
                        var simpleName = SyntaxFactory.IdentifierName(parent.Identifier);

                        replacements[parent] = simpleName.WithTriviaFrom(parent);
                    }

                    updatedMtd = updatedMtd.ReplaceNodes(replacements.Keys, (old, _) => replacements[old]);

                    //while (true)
                    //{
                    //    var withEmptyTypeArgs =
                    //        (updatedMtd.Body?.DescendantNodesAndSelf() ?? updatedMtd.ExpressionBody?.DescendantNodesAndSelf())
                    //            .OfType<TypeArgumentListSyntax>()
                    //            .FirstOrDefault(t => t.Arguments.Count == 0);

                    //    if (withEmptyTypeArgs == null) break;

                    //    var parent = (GenericNameSyntax)withEmptyTypeArgs.Parent;
                    //    var simpleName = SyntaxFactory.IdentifierName(parent.Identifier);

                    //    updatedMtd = updatedMtd.ReplaceNode(parent, simpleName.WithTriviaFrom(parent));
                    //}
                }

                ret.Add(updatedMtd.WithTriviaFrom(template));
            }

            return ret;
        }

        static protected MethodDeclarationSyntax MakeExtensionMethod(MethodDeclarationSyntax mtd)
        {
            var updatedMtd = mtd;
            
            // make it static
            var staticKeyword = SyntaxFactory.Token(SyntaxKind.StaticKeyword);
            updatedMtd = updatedMtd.AddModifiers(staticKeyword.WithTrailingTrivia(SyntaxFactory.Whitespace(" ")));

            // slap the "this " in front of the first parameter
            var firstParam = updatedMtd.ParameterList.Parameters.ElementAt(0);
            var withThisStr = THIS.ToFullString() + firstParam.ToFullString();
            var withThis = SyntaxFactory.ParseParameterList(withThisStr).Parameters.ElementAt(0);

            updatedMtd = updatedMtd.ReplaceNode(firstParam, withThis.WithTriviaFrom(firstParam));

            // replace all uses of RefLocal(x) with ref x
            ReplaceRefLocalCalls(ref updatedMtd);

            // replace all uses of RefParam(x) with ref x
            ReplaceRefParamCalls(ref updatedMtd);

            return updatedMtd;
        }

        protected static void ReplaceRefLocalCalls(ref MethodDeclarationSyntax updatedMtd)
        {
            var refLocalUses =
                updatedMtd
                    .DescendantNodesAndSelf()
                    .OfType<InvocationExpressionSyntax>()
                    .Where(e => (e.Expression as IdentifierNameSyntax)?.Identifier.ValueText == REF_LOCAL_PLACEHOLDER)
                    .ToList();

            var replacements = new Dictionary<ExpressionSyntax, RefExpressionSyntax>();

            foreach (var l in refLocalUses)
            {
                var local = l.ArgumentList.Arguments.ElementAt(0).Expression;

                var withRef = SyntaxFactory.RefExpression(local);
                var refKeyword = withRef.RefKeyword.WithTrailingTrivia(SyntaxFactory.Whitespace(" "));
                withRef = withRef.WithRefKeyword(refKeyword);

                replacements[l] = withRef.WithTriviaFrom(l);
            }

            updatedMtd = updatedMtd.ReplaceNodes(replacements.Keys, (old, _) => replacements[old]);
        }

        protected static void ReplaceRefParamCalls(ref MethodDeclarationSyntax updatedMtd)
        {
            var refParamUses =
                updatedMtd
                    .DescendantNodesAndSelf()
                    .OfType<InvocationExpressionSyntax>()
                    .Where(e => (e.Expression as IdentifierNameSyntax)?.Identifier.ValueText == REF_PARAM_PLACEHOLDER)
                    .ToList();

            foreach (var p in refParamUses)
            {
                var parameter = p.ArgumentList.Arguments.ElementAt(0).Expression;

                var withRef = SyntaxFactory.RefExpression(parameter);
                var refKeyword = withRef.RefKeyword.WithTrailingTrivia(SyntaxFactory.Whitespace(" "));
                withRef = withRef.WithRefKeyword(refKeyword);

                updatedMtd = updatedMtd.ReplaceNode(p, withRef.WithTriviaFrom(p));
            }
        }
    }
}