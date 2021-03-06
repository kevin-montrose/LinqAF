﻿using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Editing;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.CodeAnalysis.Formatting;
using Microsoft.CodeAnalysis.CSharp.Formatting;
using System.Text.RegularExpressions;
using System.Text;

namespace LinqAF.Generator
{
    static class Writer
    {
        public const string LOOKUP_EXTENSION_METHODS_FILENAME = "LookupExtensionMethods.cs";
        public const string RANGE_EXTENSION_METHODS_FILENAME = "RangeExtensionMethods.cs";
        public const string REVERSE_RANGE_EXTENSION_METHODS_FILENAME = "ReverseRangeExtensionMethods.cs";
        public const string ENUMERABLE_INTERFACE_NAME = "IStructEnumerable";
        public const string HAS_COMPARER_INTERFACE_NAME = "IHasComparer";
        const string ENUMERATOR_INTERFACE_NAME = "IStructEnumerator";
        const string PREDICATE_INTERFACE_NAME = "IStructPredicate";
        const string PROJECTION_INTERFACE_NAME = "IStructProjection";
        const string COMPARER_INTERFACE_NAME = "IStructComparer";
        const string BRIDGER_INTERFACE_NAME = "IStructBridger";
        const string COMPOUND_KEY_STRUCT_NAME = "CompoundKey";
        const string FAKE_ENUMERABLE_NAME = "FakeEnumerable";

        const string PROPERTIES_NAME = "Properties";

        public const string INTERFACE_PREABLE =
@"using System;
using LinqAF.Impl;
using System.Collections.Generic;

namespace LinqAF
{

}";
        const string ENUMERABLE_PREAMBLE =
@"using System;
using LinqAF.Impl;
using LinqAF.Config;
using System.Collections.Generic;

namespace LinqAF
{

}";
        const string ENUMERATOR_PREAMBLE =
@"using System;
using LinqAF.Impl;
using LinqAF.Config;
using System.Collections.Generic;
using MiscUtil;

namespace LinqAF
{

}";
        const string CAST_BRIDGE_EXTENSION_METHOD_CLASS_NAME = "CastBridgeExtensionMethods";
        const string OFTYPE_BRIDGE_EXTENSION_METHOD_CLASS_NAME = "OfTypeBridgeExtensionMethods";

        const string PREDICATES_FILE_NAME = "Predicates.cs";
        const string PROJECTIONS_FILE_NAME = "Projections.cs";
        const string COMPARERS_FILE_NAME = "Comparers.cs";
        const string BRIDGERS_FILE_NAME = "Bridgers.cs";
        const string COMPOUND_KEY_FILE_NAME = "CompoundKey.cs";


        struct RewriteTask
        {
            public string Name { get; private set; }

            Action<Projects> Task { get; set; }

            public RewriteTask(string name, Action<Projects> task)
            {
                Name = name;
                Task = task;
            }

            public void Run(Projects projects)
            {
                Task(projects);
            }
        }

        static RewriteTask Task(Action<Projects> act)
        {
            var name = act.Method.Name;

            return new RewriteTask(name, act);
        }

        static RewriteTask Task(IAddOperation opp)
        {
            return new RewriteTask(opp.GetType().Name, p => opp.Add(p));
        }

        public static void Rewrite(Projects projects, Action<string> log)
        {
            var ops = new List<RewriteTask>();

            // prepare basic project features
            ops.Add(Task(CopyProperties));
            ops.Add(Task(CopyPublicInterfaces));
            ops.Add(Task(CopyEnumerable));
            ops.Add(Task(CopyImplementation));
            ops.Add(Task(CopyConfiguration));
            ops.Add(Task(CopyEnumerables));
            ops.Add(Task(RemoveUnallowedMethods));
            ops.Add(Task(RemoveUnallowedInterfaces));
            ops.Add(Task(CopyEnumerableEnumerators));
            ops.Add(Task(CopyBridgeEnumerators));
            ops.Add(Task(CopyBridgeExtensionMethods));
            ops.Add(Task(CopyPredicates));
            ops.Add(Task(CopyProjections));
            ops.Add(Task(CopyComparers));
            ops.Add(Task(CopyBridgers));
            ops.Add(Task(CopyCompoundKey));
            ops.Add(Task(CopyExplicitExtensionMethods));

            // implement boxing
            ops.Add(Task(new Boxing()));

            // implement a Queryable pass-through
            ops.Add(Task(new Queryable()));

            // implement string pass-through
            ops.Add(Task(new LinqAFString()));

            // implement task pass-through
            ops.Add(Task(new LinqAFTask()));
            
            // copy a variety of convenience methods for System.Collections.Generic types
            ops.Add(Task(ConvenienceMethods));

            // simple operations
            ops.Add(Task(new Aggregate()));
            ops.Add(Task(new All()));
            ops.Add(Task(new Any()));
            ops.Add(Task(new Append()));
            ops.Add(Task(new Average()));
            ops.Add(Task(new Cast()));
            ops.Add(Task(new Contains()));
            ops.Add(Task(new Count()));
            ops.Add(Task(new DefaultIfEmpty()));
            ops.Add(Task(new ElementAt()));
            ops.Add(Task(new First()));
            ops.Add(Task(new Last()));
            ops.Add(Task(new Max()));
            ops.Add(Task(new Min()));
            ops.Add(Task(new OfType()));
            ops.Add(Task(new Prepend()));
            ops.Add(Task(new Select()));
            ops.Add(Task(new Single()));
            ops.Add(Task(new Skip()));
            ops.Add(Task(new Sum()));
            ops.Add(Task(new Take()));
            ops.Add(Task(new Where()));
            ops.Add(Task(new Distinct()));
            ops.Add(Task(new ToList()));
            ops.Add(Task(new ToArray()));
            ops.Add(Task(new ToDictionary()));
            ops.Add(Task(new ToHashSet()));
            ops.Add(Task(new ToLookup()));
            ops.Add(Task(new GroupBy()));
            ops.Add(Task(new Reverse()));
            ops.Add(Task(new OrderBy()));
            ops.Add(Task(new ThenBy()));
            ops.Add(Task(new AsEnumerable()));

            // ienumerable replacement consuming ops
            ops.Add(Task(new Concat()));
            ops.Add(Task(new SelectMany()));
            ops.Add(Task(new SequenceEqual()));
            ops.Add(Task(new Zip()));
            ops.Add(Task(new Union()));
            ops.Add(Task(new Intersect()));
            ops.Add(Task(new Join()));
            ops.Add(Task(new GroupJoin()));
            ops.Add(Task(new Except()));

            // extension methods
            ops.Add(Task(new AverageExtensionMethods()));
            ops.Add(Task(new ConcatExtensionMethods()));
            ops.Add(Task(new ExceptExtensionMethods()));
            ops.Add(Task(new IntersectExtensionMethods()));
            ops.Add(Task(new MaxExtensionMethods()));
            ops.Add(Task(new MinExtensionMethods()));
            ops.Add(Task(new SequenceEqualExtensionMethods()));
            ops.Add(Task(new SumExtensionMethods()));
            ops.Add(Task(new UnionExtensionMethods()));
            ops.Add(Task(new BuiltInExtensionMethods()));

            // apply enumerable specific overrides
            ops.Add(Task(new Overrides()));

            // apply overrides to extension methods
            ops.Add(Task(new ExtensionMethodOverrides()));
            
            // type doesn't exist post-rewrite
            ops.Add(Task(RemoveFakeEnumerable));

            // remove unused import
            ops.Add(Task(RemoveUnusedImports));

            // format!
            ops.Add(Task(FormatCode));

            // apply .NET35 #ifdefs
            //   this must be after FormatCode, because the formatting messes up
            //   the #if positioning
            ops.Add(Task(WrapNET35Declarations));

            for (var i = 0; i < ops.Count; i++)
            {
                var op = ops[i];

                var sw = new Stopwatch();
                log("Starting step #" + (i + 1) + " " + op.Name + "... ");
                sw.Start();
                op.Run(projects);
                sw.Stop();
                log("Done");
                log(" (Took: " + sw.ElapsedMilliseconds + "ms)");
                log(Environment.NewLine);
            }
        }
        
        static void ConvenienceMethods(Projects projects)
        {
            // convenience extension methods
            {
                var docs = projects.Template.Documents.Where(d => d.Name.StartsWith("ConvenienceExtensionMethods")).ToList();
                foreach (var doc in docs)
                {
                    Copier.Copy(doc, "", projects);
                }
            }

            // convenience new methods
            {
                var docs = projects.Template.Documents.Where(d => d.Name.StartsWith("LinqAFNew")).ToList();
                foreach (var doc in docs)
                {
                    Copier.Copy(doc, "", projects);
                }
            }
        }

        static void FormatCode(Projects projects)
        {
            var allDocIds = projects.Output.Documents.Where(d => d.Name.EndsWith(".cs")).Select(d => d.Id).ToList();
            
            var compilation = projects.Output.GetCompilationAsync().Result;
            compilation =
                compilation.AddReferences(
                    MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
                    MetadataReference.CreateFromFile(typeof(System.Collections.IEnumerable).Assembly.Location),
                    MetadataReference.CreateFromFile(typeof(List<>).Assembly.Location),
                    MetadataReference.CreateFromFile(typeof(LinkedList<>).Assembly.Location),
                    MetadataReference.CreateFromFile(typeof(Func<>).Assembly.Location),
                    MetadataReference.CreateFromFile(typeof(IEqualityComparer<>).Assembly.Location),
                    MetadataReference.CreateFromFile(typeof(Expression).Assembly.Location),
                    MetadataReference.CreateFromFile(typeof(System.Reflection.AssemblyTitleAttribute).Assembly.Location),
                    MetadataReference.CreateFromFile(typeof(System.Runtime.InteropServices.GuidAttribute).Assembly.Location),
                    MetadataReference.CreateFromFile(typeof(System.Runtime.CompilerServices.InternalsVisibleToAttribute).Assembly.Location)
                );

            var opts = projects.Workspace.Options;
            opts = opts.WithChangedOption(CSharpFormattingOptions.IndentBlock, true);
            opts = opts.WithChangedOption(CSharpFormattingOptions.IndentBraces, false);
            opts = opts.WithChangedOption(CSharpFormattingOptions.IndentSwitchCaseSection, true);
            opts = opts.WithChangedOption(CSharpFormattingOptions.IndentSwitchSection, true);
            opts = opts.WithChangedOption(CSharpFormattingOptions.LabelPositioning, LabelPositionOptions.OneLess);
            opts = opts.WithChangedOption(CSharpFormattingOptions.NewLineForCatch, true);
            opts = opts.WithChangedOption(CSharpFormattingOptions.NewLineForClausesInQuery, true);
            opts = opts.WithChangedOption(CSharpFormattingOptions.NewLineForElse, true);
            opts = opts.WithChangedOption(CSharpFormattingOptions.NewLineForFinally, true);
            opts = opts.WithChangedOption(CSharpFormattingOptions.NewLineForMembersInAnonymousTypes, true);
            opts = opts.WithChangedOption(CSharpFormattingOptions.NewLineForMembersInObjectInit, true);
            opts = opts.WithChangedOption(CSharpFormattingOptions.NewLinesForBracesInAccessors, true);
            opts = opts.WithChangedOption(CSharpFormattingOptions.NewLinesForBracesInAnonymousMethods, true);
            opts = opts.WithChangedOption(CSharpFormattingOptions.NewLinesForBracesInControlBlocks, true);
            opts = opts.WithChangedOption(CSharpFormattingOptions.NewLinesForBracesInLambdaExpressionBody, true);
            opts = opts.WithChangedOption(CSharpFormattingOptions.NewLinesForBracesInMethods, true);
            opts = opts.WithChangedOption(CSharpFormattingOptions.NewLinesForBracesInObjectCollectionArrayInitializers, true);
            opts = opts.WithChangedOption(CSharpFormattingOptions.NewLinesForBracesInProperties, true);
            opts = opts.WithChangedOption(CSharpFormattingOptions.NewLinesForBracesInTypes, true);
            opts = opts.WithChangedOption(CSharpFormattingOptions.SpaceAfterCast, false);
            opts = opts.WithChangedOption(CSharpFormattingOptions.SpaceAfterColonInBaseTypeDeclaration, true);
            opts = opts.WithChangedOption(CSharpFormattingOptions.SpaceAfterComma, true);
            opts = opts.WithChangedOption(CSharpFormattingOptions.SpaceAfterControlFlowStatementKeyword, true);
            opts = opts.WithChangedOption(CSharpFormattingOptions.SpaceAfterDot, false);
            opts = opts.WithChangedOption(CSharpFormattingOptions.SpaceAfterMethodCallName, false);
            opts = opts.WithChangedOption(CSharpFormattingOptions.SpaceAfterSemicolonsInForStatement, true);
            opts = opts.WithChangedOption(CSharpFormattingOptions.SpaceBeforeColonInBaseTypeDeclaration, false);
            opts = opts.WithChangedOption(CSharpFormattingOptions.SpaceBeforeComma, false);
            opts = opts.WithChangedOption(CSharpFormattingOptions.SpaceBeforeDot, false);
            opts = opts.WithChangedOption(CSharpFormattingOptions.SpaceBeforeOpenSquareBracket, true);
            opts = opts.WithChangedOption(CSharpFormattingOptions.SpaceBeforeSemicolonsInForStatement, false);
            opts = opts.WithChangedOption(CSharpFormattingOptions.SpaceBetweenEmptyMethodCallParentheses, false);
            opts = opts.WithChangedOption(CSharpFormattingOptions.SpaceBetweenEmptyMethodDeclarationParentheses, false);
            opts = opts.WithChangedOption(CSharpFormattingOptions.SpaceBetweenEmptySquareBrackets, false);
            opts = opts.WithChangedOption(CSharpFormattingOptions.SpacesIgnoreAroundVariableDeclaration, false);
            opts = opts.WithChangedOption(CSharpFormattingOptions.SpaceWithinCastParentheses, false);
            opts = opts.WithChangedOption(CSharpFormattingOptions.SpaceWithinExpressionParentheses, false);
            opts = opts.WithChangedOption(CSharpFormattingOptions.SpaceWithinMethodCallParentheses, false);
            opts = opts.WithChangedOption(CSharpFormattingOptions.SpaceWithinMethodDeclarationParenthesis, false);
            opts = opts.WithChangedOption(CSharpFormattingOptions.SpaceWithinOtherParentheses, false);
            opts = opts.WithChangedOption(CSharpFormattingOptions.SpaceWithinSquareBrackets, false);
            opts = opts.WithChangedOption(CSharpFormattingOptions.SpacingAfterMethodDeclarationName, false);
            opts = opts.WithChangedOption(CSharpFormattingOptions.SpacingAroundBinaryOperator, BinaryOperatorSpacingOptions.Single);
            opts = opts.WithChangedOption(CSharpFormattingOptions.WrappingKeepStatementsOnSingleLine, true);
            opts = opts.WithChangedOption(CSharpFormattingOptions.WrappingPreserveSingleLine, true);

            foreach (var docId in allDocIds)
            {
                projects.ModifyOutput(
                    p =>
                    {
                        var doc = p.GetDocument(docId);

                        // don't bother here
                        if (doc.Name == "AssemblyInfo.cs") return p;

                        var root = doc.GetSyntaxRootAsync().Result;

                        var updatedRoot = Formatter.Format(root, projects.Workspace, opts);

                        var updatedRootText = updatedRoot.GetText().ToString();
                        var updatedRootTextLines = updatedRootText.Split(new[] { "\r\n" }, StringSplitOptions.None);
                        var collaspedUpdatedRootText = new StringBuilder();

                        for(var i = 0; i < updatedRootTextLines.Length; i++)
                        {
                            var line = updatedRootTextLines[i];
                            if (string.IsNullOrWhiteSpace(line))
                            {
                                var prevLine = i - 1;
                                if (prevLine >= 0 && string.IsNullOrWhiteSpace(updatedRootTextLines[prevLine])) continue;

                                var nextLine = i + 1;
                                if (nextLine < updatedRootTextLines.Length && Regex.IsMatch(updatedRootTextLines[nextLine], @"\s*}\s*")) continue;
                            }

                            collaspedUpdatedRootText.AppendLine(line);
                        }

                        var collapsedUpdatedRootString = collaspedUpdatedRootText.ToString();
                        var replacementRoot = SyntaxFactory.ParseCompilationUnit(collapsedUpdatedRootString);
                        
                        var editor = DocumentEditor.CreateAsync(doc).Result;
                        editor.ReplaceNode(root, replacementRoot);

                        var updated = editor.GetChangedDocument();

                        return updated.Project;
                    }
                );
            }
        }

        static void RemoveUnusedImports(Projects projects)
        {
            var allDocIds = projects.Output.Documents.Where(d => d.Name.EndsWith(".cs")).Select(d => d.Id).ToList();

            var compilation = projects.Output.GetCompilationAsync().Result;
            compilation =
                compilation.AddReferences(
                    MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
                    MetadataReference.CreateFromFile(typeof(System.Collections.IEnumerable).Assembly.Location),
                    MetadataReference.CreateFromFile(typeof(List<>).Assembly.Location),
                    MetadataReference.CreateFromFile(typeof(LinkedList<>).Assembly.Location),
                    MetadataReference.CreateFromFile(typeof(Func<>).Assembly.Location),
                    MetadataReference.CreateFromFile(typeof(IEqualityComparer<>).Assembly.Location),
                    MetadataReference.CreateFromFile(typeof(Expression).Assembly.Location),
                    MetadataReference.CreateFromFile(typeof(System.Reflection.AssemblyTitleAttribute).Assembly.Location),
                    MetadataReference.CreateFromFile(typeof(System.Runtime.InteropServices.GuidAttribute).Assembly.Location),
                    MetadataReference.CreateFromFile(typeof(System.Runtime.CompilerServices.InternalsVisibleToAttribute).Assembly.Location)
                );

            var remove = new Dictionary<DocumentId, List<UsingDirectiveSyntax>>();

            foreach (var docId in allDocIds)
            {
                var doc = projects.Output.GetDocument(docId);

                // don't bother here
                if (doc.Name == "AssemblyInfo.cs") continue;

                var root = doc.GetSyntaxRootAsync().Result;

                var model = compilation.GetSemanticModel(root.SyntaxTree);
                
                var usings = root.DescendantNodesAndSelf().OfType<UsingDirectiveSyntax>().ToList();
                
                var nodesToNamespaces = 
                    root
                        .DescendantNodes()
                        .Select(
                            node =>
                            {
                                var info = model.GetSymbolInfo(node);
                                var symbol = info.Symbol;

                                if (symbol == null) return null;

                                return Tuple.Create(node, info, symbol, symbol?.ContainingNamespace, symbol?.ContainingNamespace?.ToString());
                            }
                        )
                        .Where(n => n != null)
                        .ToList();

                var namespaces = nodesToNamespaces.Select(n => n.Item5).Where(n => n != null).Distinct().ToList();

                var needRemoval =
                    usings
                        .Where(
                            u =>
                            {
                                var name = "";

                                var stack = new Stack<NameSyntax>();

                                stack.Push(u.Name);

                                while (stack.Count > 0)
                                {
                                    var nextLeft = stack.Pop();

                                    var qualified = nextLeft as QualifiedNameSyntax;
                                    if (qualified != null)
                                    {
                                        stack.Push(qualified.Right);
                                        stack.Push(qualified.Left);
                                        continue;
                                    }

                                    var asName = nextLeft as IdentifierNameSyntax;
                                    if (asName != null)
                                    {
                                        name += "." + asName.Identifier.ValueText;
                                        continue;
                                    }
                                }

                                var properName = name.Trim('.');

                                return !namespaces.Contains(properName);
                            }
                         )
                         .ToList();

                if (needRemoval.Count > 0)
                {
                    remove[docId] = needRemoval;
                }
            }

            projects.ModifyOutput(
                p =>
                {
                    var curP = p;

                    foreach(var kv in remove)
                    {
                        var doc = curP.GetDocument(kv.Key);

                        var editor = DocumentEditor.CreateAsync(doc).Result;

                        foreach(var node in kv.Value)
                        {
                            editor.RemoveNode(node);
                        }

                        var updatedDoc = editor.GetChangedDocument();

                        curP = updatedDoc.Project;
                    }

                    return curP;
                }
            );
        }

        /// <summary>
        /// Looks for a bunch of stuff that's added in version > .NET 3.5,
        /// and wraps it in #if !NET35 blocks
        /// </summary>
        static void WrapNET35Declarations(Projects projects)
        {
            foreach(var doc in projects.Output.Documents)
            {
                var root = doc.GetSyntaxRootAsync().Result;

                var replacements = new Dictionary<SyntaxNode, SyntaxNode>();

                if (doc.Name.StartsWith("LinqAFTask."))
                {
                    replacements[root] = WrapInIfDef("!NET35", root);
                    goto doReplacements;
                }

                // structs
                var structs = root.DescendantNodes().OfType<StructDeclarationSyntax>();
                foreach(var @struct in structs)
                {
                    var ident = @struct.Identifier.ValueText;
                    var needsWrap = ident == "SortedSetBridger" || ident == "SortedSetEnumerator";

                    if (needsWrap)
                    {
                        var replacement = WrapInIfDef("!NET35", @struct);
                        replacements[@struct] = replacement;
                    }
                }

                // methods
                var mtds = root.DescendantNodes().OfType<MethodDeclarationSyntax>();
                foreach(var mtd in mtds)
                {
                    // args
                    foreach(var p in mtd.ParameterList.Parameters)
                    {
                        var allTypes = p.DescendantNodesAndSelf().OfType<TypeSyntax>();
                        var anyForbidden = allTypes.Any(x => NeedsWrap(x));

                        if (anyForbidden)
                        {
                            var replacement = WrapInIfDef("!NET35", mtd);
                            replacements[mtd] = replacement;
                            goto nextMethod;
                        }
                    }

                    // return
                    var ret = mtd.ReturnType;
                    var types = ret.DescendantNodesAndSelf().OfType<TypeSyntax>();
                    foreach(var type in types)
                    {
                        if (NeedsWrap(type))
                        {
                            var replacement = WrapInIfDef("!NET35", mtd);
                            replacements[mtd] = replacement;
                            goto nextMethod;
                        }
                    }

                    // calls
                    var calls = mtd.DescendantNodesAndSelf().OfType<InvocationExpressionSyntax>();
                    foreach(var call in calls)
                    {
                        if (NeedsWrap(call))
                        {
                            var replacement = WrapInIfDef("!NET35", mtd);
                            replacements[mtd] = replacement;
                            goto nextMethod;
                        }
                    }

                    // no-op
                    nextMethod: GC.KeepAlive(projects);
                }

                // attributes
                var attrs = root.DescendantNodes().OfType<AttributeSyntax>();
                foreach(var attr in attrs)
                {
                    var attrList = attr.Parent;
                    
                    foreach(var a in attr.ArgumentList.Arguments)
                    {
                        if(NeedsWrap(a.Expression))
                        {
                            var replacement = WrapInIfDef("!NET35", attrList);
                            replacements[attrList] = replacement;
                            goto nextAttribute;
                        }
                    }

                    // no-op
                    nextAttribute: GC.KeepAlive(projects);
                }

                doReplacements:

                if (replacements.Count == 0) continue;

                var updated = root.ReplaceNodes(replacements.Keys, (old, _) => replacements[old]);

                projects.ModifyOutput(
                    p =>
                    {
                        var updatedDoc = p.GetDocument(doc.Id).WithSyntaxRoot(updated);

                        return updatedDoc.Project;
                    }
                );
            }
        }

        static bool NeedsWrap(InvocationExpressionSyntax call)
        {
            var ident = call.Expression as MemberAccessExpressionSyntax;
            if (ident == null) return false;

            var rightHand = ident.Expression?.ToString();
            var methodName = ident.Name?.Identifier.ValueText;

            var methodIsZippy = methodName == "Zip";
            var rightHandIsQueryabley = rightHand?.EndsWith("Queryable") ?? false;

            var isZipCall = methodIsZippy && rightHandIsQueryabley;
            if (isZipCall) return true;

            var methodIsIsNullOrWhiteSpace = methodName == "IsNullOrWhiteSpace";
            var rightHandIsStringy = rightHand?.EndsWith("String") ?? false;

            var isIsNullOrWhiteSpaceCall = methodIsIsNullOrWhiteSpace && rightHandIsStringy;
            if (isIsNullOrWhiteSpaceCall) return true;

            return false;
        }

        static bool NeedsWrap(TypeSyntax type)
        {
            if (!(type is GenericNameSyntax)) return false;

            var asGenericName = type as GenericNameSyntax;
            var val = asGenericName.Identifier.ValueText;

            // this doesn't exist in .NET 3.5
            return val == "SortedSet" || val == "ISet";
        }

        static bool NeedsWrap(ExpressionSyntax exp)
        {
            if (!(exp is MemberAccessExpressionSyntax)) return false;

            var literal = exp.ToString();

            // these don't exist in .NET 3.5
            return literal == "MethodImplOptions.AggressiveInlining" || literal == "System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining";
        }

        // surrounds the given node in an #if ... #endif directive block
        static SyntaxNode WrapInIfDef(string variableName, SyntaxNode node)
        {
            var notVariable = SyntaxFactory.ParseExpression(variableName).WithLeadingTrivia(SyntaxFactory.Whitespace(" "));
            var linebreak = SyntaxFactory.Whitespace("\n");

            var @if = SyntaxFactory.IfDirectiveTrivia(notVariable, true, true, true);

            var @endif = SyntaxFactory.EndIfDirectiveTrivia(true);
            
            var triviaIf = SyntaxFactory.Trivia(@if);
            var triviaEndIf = SyntaxFactory.Trivia(@endif);

            var nodeOldLeadingTrivia = node.GetLeadingTrivia();
            var nodeOldTrailingTrivia = node.GetTrailingTrivia();

            var newLeadingTrivia = nodeOldLeadingTrivia.Add(linebreak).Add(triviaIf).Add(linebreak);
            var newTrailingTrivia = nodeOldTrailingTrivia.Add(linebreak).Add(triviaEndIf).Add(linebreak);

            var wrapped = node.WithLeadingTrivia(newLeadingTrivia).WithTrailingTrivia(newTrailingTrivia);

            return wrapped;
        }

        /// <summary>
        /// Removes all casts to FakeEnumerable from all files
        /// </summary>
        static void RemoveFakeEnumerable(Projects projects)
        {
            var allCasts = 
                projects
                    .Output
                    .Documents
                    .Select(
                        d =>
                        {
                            var casts = d.GetSyntaxRootAsync().Result.DescendantNodesAndSelf().OfType<CastExpressionSyntax>();

                            return
                                new
                                {
                                    Casts = casts.ToList(),
                                    Document = d
                                };
                        }
                    )
                    .Where(t => t.Casts.Count > 0)
                    .ToList();

            foreach(var kv in allCasts)
            {
                var needRemoval = kv.Casts.Where(c => (c.Type as GenericNameSyntax)?.Identifier.ValueText == FAKE_ENUMERABLE_NAME).ToList();
                if (needRemoval.Count == 0) continue;

                projects.ModifyOutput(
                    p =>
                    {
                        var pDoc = p.GetDocument(kv.Document.Id);

                        var editor = DocumentEditor.CreateAsync(pDoc).Result;
                        foreach (var node in needRemoval)
                        {
                            var rawExp = node.Expression;

                            editor.ReplaceNode(node, rawExp);
                        }

                        return editor.GetChangedDocument().Project;
                    }
                );
            }
        }

        /// <summary>
        /// Get IStructEnumerable and IStructEnumerator into the project, they're the root of everything
        /// </summary>
        static void CopyPublicInterfaces(Projects projects)
        {
            var allInterfaces = projects.Template.Documents.SelectMany(d => d.GetSyntaxRootAsync().Result.DescendantNodes().OfType<InterfaceDeclarationSyntax>());

            var ienumerable = allInterfaces.Single(i => i.Identifier.ValueText == ENUMERABLE_INTERFACE_NAME);
            var ienumerator = allInterfaces.Single(i => i.Identifier.ValueText == ENUMERATOR_INTERFACE_NAME);
            var ipredicate = allInterfaces.Single(i => i.Identifier.ValueText == PREDICATE_INTERFACE_NAME);
            var iprojection = allInterfaces.Single(i => i.Identifier.ValueText == PROJECTION_INTERFACE_NAME);
            var icomparer = allInterfaces.Single(i => i.Identifier.ValueText == COMPARER_INTERFACE_NAME);
            var ibridger = allInterfaces.Single(i => i.Identifier.ValueText == BRIDGER_INTERFACE_NAME);

            {
                var ienumerableDoc = projects.Output.AddDocument(ENUMERABLE_INTERFACE_NAME + ".cs", INTERFACE_PREABLE);
                var ns = ienumerableDoc.GetSyntaxRootAsync().Result.DescendantNodes().OfType<NamespaceDeclarationSyntax>().Single();
                var newNs = ns.AddMembers(ienumerable);
                var editor = DocumentEditor.CreateAsync(ienumerableDoc).Result;
                editor.ReplaceNode(ns, newNs);

                projects.ModifyOutput(p => editor.GetChangedDocument().Project);
            }

            {
                var ienumeratorDoc = projects.Output.AddDocument(ENUMERATOR_INTERFACE_NAME + ".cs", INTERFACE_PREABLE);

                var ns = ienumeratorDoc.GetSyntaxRootAsync().Result.DescendantNodes().OfType<NamespaceDeclarationSyntax>().Single();
                var newNs = ns.AddMembers(ienumerator);
                var editor = DocumentEditor.CreateAsync(ienumeratorDoc).Result;
                editor.ReplaceNode(ns, newNs);

                projects.ModifyOutput(p => editor.GetChangedDocument().Project);
            }

            {
                var ipredicateDoc = projects.Output.AddDocument(PREDICATE_INTERFACE_NAME + ".cs", INTERFACE_PREABLE);

                var ns = ipredicateDoc.GetSyntaxRootAsync().Result.DescendantNodes().OfType<NamespaceDeclarationSyntax>().Single();
                var newNs = ns.AddMembers(ipredicate);
                var editor = DocumentEditor.CreateAsync(ipredicateDoc).Result;
                editor.ReplaceNode(ns, newNs);

                projects.ModifyOutput(p => editor.GetChangedDocument().Project);
            }

            {
                var iprojectionDoc = projects.Output.AddDocument(PROJECTION_INTERFACE_NAME + ".cs", INTERFACE_PREABLE);

                var ns = iprojectionDoc.GetSyntaxRootAsync().Result.DescendantNodes().OfType<NamespaceDeclarationSyntax>().Single();
                var newNs = ns.AddMembers(iprojection);
                var editor = DocumentEditor.CreateAsync(iprojectionDoc).Result;
                editor.ReplaceNode(ns, newNs);

                projects.ModifyOutput(p => editor.GetChangedDocument().Project);
            }

            {
                var icomparerDoc = projects.Output.AddDocument(COMPARER_INTERFACE_NAME + ".cs", INTERFACE_PREABLE);

                var ns = icomparerDoc.GetSyntaxRootAsync().Result.DescendantNodes().OfType<NamespaceDeclarationSyntax>().Single();
                var newNs = ns.AddMembers(icomparer);
                var editor = DocumentEditor.CreateAsync(icomparerDoc).Result;
                editor.ReplaceNode(ns, newNs);

                projects.ModifyOutput(p => editor.GetChangedDocument().Project);
            }

            {
                var ibridgerDoc = projects.Output.AddDocument(BRIDGER_INTERFACE_NAME + ".cs", INTERFACE_PREABLE);

                var ns = ibridgerDoc.GetSyntaxRootAsync().Result.DescendantNodes().OfType<NamespaceDeclarationSyntax>().Single();
                var newNs = ns.AddMembers(ibridger);
                var editor = DocumentEditor.CreateAsync(ibridgerDoc).Result;
                editor.ReplaceNode(ns, newNs);

                projects.ModifyOutput(p => editor.GetChangedDocument().Project);
            }
        }

        /// <summary>
        /// Copy Enumerable.cs, as a freak one-off file
        /// </summary>
        static void CopyEnumerable(Projects projects)
        {
            var enumerable = projects.Template.Documents.Single(d => d.Name == "Enumerable.cs");

            Copier.Copy(enumerable, null, projects);
        }

        /// <summary>
        /// Copy properties in Properties folder
        /// </summary>
        static void CopyProperties(Projects projects)
        {
            var propsToCopy = projects.Template.Documents.Where(d => d.Folders.LastOrDefault() == PROPERTIES_NAME).ToList();

            foreach (var prop in propsToCopy)
            {
                Copier.Copy(prop, PROPERTIES_NAME, projects);
            }
        }

        /// <summary>
        /// Copy everything in the Impl folder over, that's the "actual" code
        /// </summary>
        static void CopyImplementation(Projects projects)
        {
            var implsToCopy = projects.Template.Documents.Where(d => d.Folders.LastOrDefault() == "Impl").ToList();

            foreach (var impl in implsToCopy)
            {
                Copier.Copy(impl, "Impl", projects);
            }
        }

        static void CopyConfiguration(Projects projects)
        {
            var implsToCopy = projects.Template.Documents.Where(d => d.Folders.LastOrDefault() == "Config").ToList();

            foreach (var impl in implsToCopy)
            {
                Copier.Copy(impl, "Config", projects);
            }
        }

        /// <summary>
        /// Find all the declarations of structs that implement IStructEnumerable, and copy them over.
        /// </summary>
        static void CopyEnumerables(Projects projects)
        {
            var allStructs =
                projects.Template.Documents
                    .Where(d => (d.Folders?.Count ?? 0) == 0)
                    .SelectMany(d => d.GetSyntaxRootAsync().Result.DescendantNodesAndSelf().OfType<StructDeclarationSyntax>())
                    .ToList();

            var structsWithInterfaces = allStructs.Where(s => s.BaseList?.Types.Any() ?? false).ToList();

            var enumerables =
                structsWithInterfaces
                    .Where(
                        i =>
                            i.BaseList.Types
                                .OfType<SimpleBaseTypeSyntax>()
                                .Select(x => x.Type)
                                .OfType<GenericNameSyntax>()
                                .Any(g => g.Identifier.ValueText == ENUMERABLE_INTERFACE_NAME)
                    ).ToList();

            var enumerableNamesToRootDocument = new Dictionary<string, DocumentId>();

            foreach (var enumerable in enumerables)
            {
                Document newDocument = null;
                projects.ModifyOutput(
                    p =>
                    {
                        var full = enumerable.Identifier.ValueText;
                        var lessEnumerable = full.Substring(0, full.Length - "Enumerable".Length);
                        var fileName = lessEnumerable + ".cs";

                        newDocument = p.AddDocument(fileName, ENUMERABLE_PREAMBLE);

                        return newDocument.Project;
                    }
                );

                var syntax = newDocument.GetSyntaxRootAsync().Result;

                var oldNS = syntax.DescendantNodesAndSelf().OfType<NamespaceDeclarationSyntax>().Single();
                var newNS = oldNS.AddMembers(enumerable);

                projects.ModifyOutput(
                   p =>
                   {
                       var tree = newDocument.GetSyntaxTreeAsync().Result;
                       var root = tree.GetRoot();

                       var editor = DocumentEditor.CreateAsync(newDocument).Result;

                       editor.ReplaceNode(oldNS, newNS);

                       newDocument = editor.GetChangedDocument();

                       return newDocument.Project;
                   }
                );

                enumerableNamesToRootDocument[enumerable.Identifier.ValueText] = newDocument.Id;
            }

            projects.SetEnumerableRoots(enumerableNamesToRootDocument);
        }

        /// <summary>
        /// Remove methods from the root enumerables that _isn't_ a member of IStructEnumerable or IHasComparer.
        /// 
        /// Needed because template might be a little messy.
        /// </summary>
        static void RemoveUnallowedMethods(Projects projects)
        {
            var enumerableInterface =
                projects.Template.Documents
                    .SelectMany(d => d.GetSyntaxRootAsync().Result.DescendantNodes().OfType<InterfaceDeclarationSyntax>())
                    .Single(i => i.Identifier.ValueText == ENUMERABLE_INTERFACE_NAME);

            var hasComparerInterface =
                projects.Template.Documents
                    .SelectMany(d => d.GetSyntaxRootAsync().Result.DescendantNodes().OfType<InterfaceDeclarationSyntax>())
                    .Single(i => i.Identifier.ValueText == HAS_COMPARER_INTERFACE_NAME);

            var methodNames = new HashSet<string>(
                enumerableInterface.Members
                    .OfType<MethodDeclarationSyntax>()
                    .Select(m => m.Identifier.ValueText)
                    .Concat(
                        hasComparerInterface.Members.OfType<MethodDeclarationSyntax>()
                        .Select(m => m.Identifier.ValueText)
                    )
                );

            // equals and gethashcode can be overridden if need be
            methodNames.Add("Equals");
            methodNames.Add("GetHashCode");

            foreach (var doc in projects.EnumerableDocuments)
            {
                var isLookup = doc.Name == "LookupSpecific.cs" || doc.Name == "LookupDefault.cs";

                var editor = DocumentEditor.CreateAsync(doc).Result;

                var root = doc.GetSyntaxRootAsync().Result;

                var enumerable = root.DescendantNodes().OfType<StructDeclarationSyntax>().Single();

                foreach (var mtd in enumerable.Members.OfType<MethodDeclarationSyntax>())
                {
                    var isContains = mtd.Identifier.ValueText == "Contains";

                    var isLookupContains = isLookup && isContains;

                    var mtdName = mtd.Identifier.ValueText;
                    if (!methodNames.Contains(mtdName) && !isLookupContains)
                    {
                        editor.RemoveNode(mtd);
                    }
                }

                projects.ModifyOutput(
                    p =>
                    {
                        var updatedDoc = editor.GetChangedDocument();

                        return updatedDoc.Project;
                    }
                );

            }
        }

        /// <summary>
        /// Remove interface implementations from the root enumerables that _aren't_ IStructEnumerable.
        /// 
        /// Needed because template might be a little messy.
        /// </summary>
        static void RemoveUnallowedInterfaces(Projects projects)
        {
            foreach (var doc in projects.EnumerableDocuments)
            {
                var editor = DocumentEditor.CreateAsync(doc).Result;

                var root = doc.GetSyntaxRootAsync().Result;

                var enumerable = root.DescendantNodes().OfType<StructDeclarationSyntax>().Single();

                foreach (var intfs in enumerable.BaseList.Types)
                {
                    var iname = ((intfs as SimpleBaseTypeSyntax)?.Type as SimpleNameSyntax)?.Identifier.ValueText;

                    var needsRemove = iname != ENUMERABLE_INTERFACE_NAME && iname != HAS_COMPARER_INTERFACE_NAME;

                    if (needsRemove)
                    {
                        editor.RemoveNode(intfs);
                    }
                }

                projects.ModifyOutput(
                    p =>
                    {
                        var updatedDoc = editor.GetChangedDocument();

                        return updatedDoc.Project;
                    }
                );
            }
        }

        /// <summary>
        /// Move enumerators paired to enumerables from the template project to the output project.
        /// 
        /// Expects each enumerable to have a corresponding enumerator.
        /// </summary>
        static void CopyEnumerableEnumerators(Projects projects)
        {
            foreach (var pair in projects.EnumerableDocumentsWithNames)
            {
                var enumerableName = pair.Name;
                var baseName = enumerableName.Substring(0, enumerableName.Length - "Enumerable".Length);

                var enumeratorName = baseName + "Enumerator";
                var newFileName = baseName + ".Enumerator.cs";

                var templateEnumerators =
                    projects.Template.Documents
                        .SelectMany(
                            d =>
                            {
                                var root = d.GetSyntaxRootAsync().Result;

                                return root.DescendantNodes().OfType<StructDeclarationSyntax>();
                            }
                        )
                        .Where(s => s.Identifier.ValueText == enumeratorName)
                        .ToArray();

                projects.ModifyOutput(
                    p =>
                    {
                        var newDocument = p.AddDocument(newFileName, ENUMERATOR_PREAMBLE);
                        var editor = DocumentEditor.CreateAsync(newDocument).Result;

                        var ns = newDocument.GetSyntaxRootAsync().Result.DescendantNodes().OfType<NamespaceDeclarationSyntax>().Single();

                        var updatedNs = ns.AddMembers(templateEnumerators);

                        editor.ReplaceNode(ns, updatedNs);

                        newDocument = editor.GetChangedDocument();

                        return newDocument.Project;
                    }
                );
            }
        }

        /// <summary>
        /// Move structs that implement IStructPredicate.
        /// </summary>
        static void CopyPredicates(Projects projects)
        {
            var allStructs = projects.Template.Documents.SelectMany(d => d.GetSyntaxRootAsync().Result.DescendantNodesAndSelf().OfType<StructDeclarationSyntax>());

            var predicateImpls = allStructs.Where(s => s.BaseList != null && s.BaseList.Types.Any(t => (t.Type as GenericNameSyntax)?.Identifier.ValueText == PREDICATE_INTERFACE_NAME)).ToArray();

            projects.ModifyOutput(
                p =>
                {
                    var newDoc = p.AddDocument(PREDICATES_FILE_NAME, ENUMERABLE_PREAMBLE);
                    var editor = DocumentEditor.CreateAsync(newDoc).Result;
                    
                    var root = newDoc.GetSyntaxRootAsync().Result;
                    var ns = root.DescendantNodesAndSelf().OfType<NamespaceDeclarationSyntax>().Single();
                    var newNs = ns.AddMembers(predicateImpls);


                    editor.ReplaceNode(ns, newNs);
                    var updatedDoc = editor.GetChangedDocument();

                    return updatedDoc.Project;
                }
            );
        }

        /// <summary>
        /// Move structs that implement IStructProjection.
        /// </summary>
        static void CopyProjections(Projects projects)
        {
            var allStructs = projects.Template.Documents.SelectMany(d => d.GetSyntaxRootAsync().Result.DescendantNodesAndSelf().OfType<StructDeclarationSyntax>());

            var projectionImpls = allStructs.Where(s => s.BaseList != null && s.BaseList.Types.Any(t => (t.Type as GenericNameSyntax)?.Identifier.ValueText == PROJECTION_INTERFACE_NAME)).ToArray();

            projects.ModifyOutput(
                p =>
                {
                    var newDoc = p.AddDocument(PROJECTIONS_FILE_NAME, ENUMERABLE_PREAMBLE);
                    var editor = DocumentEditor.CreateAsync(newDoc).Result;

                    var root = newDoc.GetSyntaxRootAsync().Result;
                    var ns = root.DescendantNodesAndSelf().OfType<NamespaceDeclarationSyntax>().Single();
                    var newNs = ns.AddMembers(projectionImpls);


                    editor.ReplaceNode(ns, newNs);
                    var updatedDoc = editor.GetChangedDocument();

                    return updatedDoc.Project;
                }
            );
        }

        static void CopyExplicitExtensionMethods(Projects projects)
        {
            Action<string> copy =
                filename =>
                {
                    var toCopy = projects.Template.Documents.Single(d => d.Name == filename);
                    var toCopyText = toCopy.GetTextAsync().Result;

                    projects.ModifyOutput(
                        p =>
                        {
                            var newDoc = p.AddDocument(filename, toCopyText);

                            return newDoc.Project;
                        }
                    );
                };

            copy(LOOKUP_EXTENSION_METHODS_FILENAME);
            copy(RANGE_EXTENSION_METHODS_FILENAME);
            copy(REVERSE_RANGE_EXTENSION_METHODS_FILENAME);
        }

       static void CopyCompoundKey(Projects projects)
        {
            var allStructs = projects.Template.Documents.SelectMany(d => d.GetSyntaxRootAsync().Result.DescendantNodesAndSelf().OfType<StructDeclarationSyntax>());

            var compoundKey = allStructs.Single(s => s.Identifier.ValueText == COMPOUND_KEY_STRUCT_NAME);

            projects.ModifyOutput(
                p =>
                {
                    var newDoc = p.AddDocument(COMPOUND_KEY_FILE_NAME, ENUMERABLE_PREAMBLE);
                    var editor = DocumentEditor.CreateAsync(newDoc).Result;

                    var root = newDoc.GetSyntaxRootAsync().Result;
                    var ns = root.DescendantNodesAndSelf().OfType<NamespaceDeclarationSyntax>().Single();
                    var newNs = ns.AddMembers(compoundKey);

                    editor.ReplaceNode(ns, newNs);
                    var updatedDoc = editor.GetChangedDocument();

                    return updatedDoc.Project;
                }
            );
        }

        /// <summary>
        /// Move structs that implement IStructComparer.
        /// </summary>
        static void CopyComparers(Projects projects)
        {
            var allStructs = projects.Template.Documents.SelectMany(d => d.GetSyntaxRootAsync().Result.DescendantNodesAndSelf().OfType<StructDeclarationSyntax>());

            var comparerImpls = allStructs.Where(s => s.BaseList != null && s.BaseList.Types.Any(t => (t.Type as GenericNameSyntax)?.Identifier.ValueText == COMPARER_INTERFACE_NAME)).ToArray();

            projects.ModifyOutput(
                p =>
                {
                    var newDoc = p.AddDocument(COMPARERS_FILE_NAME, ENUMERABLE_PREAMBLE);
                    var editor = DocumentEditor.CreateAsync(newDoc).Result;

                    var root = newDoc.GetSyntaxRootAsync().Result;
                    var ns = root.DescendantNodesAndSelf().OfType<NamespaceDeclarationSyntax>().Single();
                    var newNs = ns.AddMembers(comparerImpls);
                    
                    editor.ReplaceNode(ns, newNs);
                    var updatedDoc = editor.GetChangedDocument();

                    return updatedDoc.Project;
                }
            );
        }

        /// <summary>
        /// Move structs that implement IStructBridger.
        /// </summary>
        static void CopyBridgers(Projects projects)
        {
            var allStructs = projects.Template.Documents.SelectMany(d => d.GetSyntaxRootAsync().Result.DescendantNodesAndSelf().OfType<StructDeclarationSyntax>());

            var comparerImpls = allStructs.Where(s => s.BaseList != null && s.BaseList.Types.Any(t => (t.Type as GenericNameSyntax)?.Identifier.ValueText == BRIDGER_INTERFACE_NAME)).ToArray();

            projects.ModifyOutput(
                p =>
                {
                    var newDoc = p.AddDocument(BRIDGERS_FILE_NAME, ENUMERABLE_PREAMBLE);
                    var editor = DocumentEditor.CreateAsync(newDoc).Result;

                    var root = newDoc.GetSyntaxRootAsync().Result;
                    var ns = root.DescendantNodesAndSelf().OfType<NamespaceDeclarationSyntax>().Single();
                    var newNs = ns.AddMembers(comparerImpls);

                    editor.ReplaceNode(ns, newNs);
                    var updatedDoc = editor.GetChangedDocument();

                    return updatedDoc.Project;
                }
            );
        }

        /// <summary>
        /// Move OfType and Cast extension methods, because generating them is a lot of trouble
        /// and they're super simple.
        /// </summary>
        static void CopyBridgeExtensionMethods(Projects projects)
        {
            var allClasses = projects.Template.Documents.SelectMany(d => d.GetSyntaxRootAsync().Result.DescendantNodesAndSelf().OfType<ClassDeclarationSyntax>());

            var castClass = allClasses.Single(c => c.Identifier.ValueText == CAST_BRIDGE_EXTENSION_METHOD_CLASS_NAME);
            var ofTypeClass = allClasses.Single(c => c.Identifier.ValueText == OFTYPE_BRIDGE_EXTENSION_METHOD_CLASS_NAME);

            var castRoot = castClass.SyntaxTree.GetRoot();
            var ofTypeRoot = ofTypeClass.SyntaxTree.GetRoot();

            projects.ModifyOutput(
                p =>
                {
                    var castDoc = p.AddDocument(CAST_BRIDGE_EXTENSION_METHOD_CLASS_NAME + ".cs", castRoot.GetText());
                    var p2 = castDoc.Project;

                    var ofTypeDoc = p2.AddDocument(OFTYPE_BRIDGE_EXTENSION_METHOD_CLASS_NAME + ".cs", ofTypeRoot.GetText());
                    var p3 = ofTypeDoc.Project;

                    return p3;
                }
            );
        }

        /// <summary>
        /// Move enumerators that are bridging System.Collections.Generic classes over.
        /// </summary>
        static void CopyBridgeEnumerators(Projects projects)
        {
            var allStructs =
                projects.Template.Documents
                    .Where(d => (d.Folders?.Count ?? 0) == 0)
                    .SelectMany(d => d.GetSyntaxRootAsync().Result.DescendantNodesAndSelf().OfType<StructDeclarationSyntax>())
                    .ToList();

            var structsWithInterfaces = allStructs.Where(s => s.BaseList?.Types.Any() ?? false).ToList();

            var enumerators =
                structsWithInterfaces
                    .Where(
                        i =>
                            i.BaseList.Types
                                .OfType<SimpleBaseTypeSyntax>()
                                .Select(x => x.Type)
                                .OfType<GenericNameSyntax>()
                                .Any(g => g.Identifier.ValueText == ENUMERATOR_INTERFACE_NAME)
                    ).ToList();

            var unpaired = new List<StructDeclarationSyntax>();

            foreach (var e in enumerators)
            {
                var name = e.Identifier.ValueText;
                var baseFileName = name.Substring(0, name.Length - "Enumerator".Length) + "Enumerable";

                if (projects.EnumerableDocumentsWithNames.Any(p => p.Name == baseFileName)) continue;

                unpaired.Add(e);
            }

            projects.ModifyOutput(
                p =>
                {
                    var newDoc = p.AddDocument("Bridges.cs", ENUMERATOR_PREAMBLE);

                    var root = newDoc.GetSyntaxRootAsync().Result;

                    var ns = root.DescendantNodes().OfType<NamespaceDeclarationSyntax>().Single();

                    var newNs = ns.AddMembers(unpaired.ToArray());

                    var editor = DocumentEditor.CreateAsync(newDoc).Result;

                    editor.ReplaceNode(ns, newNs);

                    var updatedDoc = editor.GetChangedDocument();

                    return updatedDoc.Project;
                }
            );
        }
    }
}