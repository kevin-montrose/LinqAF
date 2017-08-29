using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime;
using LinqAF;
using LinqAF.Impl;
using System.Linq.Expressions;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.IO;
using Microsoft.CodeAnalysis;
using System.Text;

namespace TestHelpers
{
    public class Helper
    {
        static List<Type> AllEnumerables_NotWeird;
        static List<Type> AllEnumerables_IncludingWeird;
        static Dictionary<Type, MethodInfo[]> InterfaceMethods = new Dictionary<Type, MethodInfo[]>();
        static Dictionary<Type, MethodInfo[]> EnumerableMethods = new Dictionary<Type, MethodInfo[]>();
        static Dictionary<MethodInfo, ParameterInfo[]> Parameters = new Dictionary<MethodInfo, ParameterInfo[]>();
        static Dictionary<Type, Type[]> GenericArguments = new Dictionary<Type, Type[]>();
        static Dictionary<Type, Type> GenericTypeDefinition = new Dictionary<Type, Type>();
        static Dictionary<Type, Type> ElementType = new Dictionary<Type, Type>();

        static Dictionary<Type, MethodInfo> GetEnumeratorMethods = new Dictionary<Type, MethodInfo>();
        static Dictionary<Type, MethodInfo> GetDisposeMethods = new Dictionary<Type, MethodInfo>();
        static Dictionary<Type, MethodInfo> GetCurrentGetMethods = new Dictionary<Type, MethodInfo>();
        static Dictionary<Type, MethodInfo> GetMoveNextMethods = new Dictionary<Type, MethodInfo>();

        static MethodInfo GetEnumeratorMethod(Type t)
        {
            MethodInfo mtd;
            if (GetEnumeratorMethods.TryGetValue(t, out mtd)) return mtd;

            GetEnumeratorMethods[t] = mtd = t.GetMethod("GetEnumerator");

            return mtd;
        }

        static MethodInfo GetDisposeMethod(Type t)
        {
            MethodInfo mtd;
            if (GetDisposeMethods.TryGetValue(t, out mtd)) return mtd;

            GetDisposeMethods[t] = mtd = t.GetMethod("Dispose");

            return mtd;
        }

        static MethodInfo GetCurrentGetMethod(Type t)
        {
            MethodInfo mtd;
            if (GetCurrentGetMethods.TryGetValue(t, out mtd)) return mtd;

            GetCurrentGetMethods[t] = mtd = t.GetProperty("Current").GetMethod;

            return mtd;
        }

        static MethodInfo GetMoveNextMethod(Type t)
        {
            MethodInfo mtd;
            if (GetMoveNextMethods.TryGetValue(t, out mtd)) return mtd;

            GetMoveNextMethods[t] = mtd = t.GetMethod("MoveNext");

            return mtd;
        }

        public static IEnumerable<Type> AllEnumerables(bool includeWeirdOnes = false)
        {
            if (includeWeirdOnes && AllEnumerables_IncludingWeird != null) return AllEnumerables_IncludingWeird;
            if (!includeWeirdOnes && AllEnumerables_NotWeird != null) return AllEnumerables_NotWeird;

            var asm = typeof(IStructEnumerable<,>).Assembly;

            var impls =
                asm.GetExportedTypes()
                    .Where(a =>
                        a.GetInterfaces().Any(
                            i => i.IsGenericType && GetGenericTypeDefinition(i) == typeof(IStructEnumerable<,>))
                    )
                    .ToList();

            var ret = new List<Type>();

            foreach (var e in impls)
            {
                if (!includeWeirdOnes)
                {
                    // these are weird, skip them so long as we do so in ExtensionMethodsBase
                    if (e == typeof(LookupDefaultEnumerable<,>)) continue;
                    if (e == typeof(LookupSpecificEnumerable<,>)) continue;
                    if (e == typeof(GroupingEnumerable<,>)) continue;
                    if (e == typeof(GroupByDefaultEnumerable<,,,,>)) continue;
                    if (e == typeof(GroupBySpecificEnumerable<,,,,>)) continue;
                }

                ret.Add(e);
            }

            if (includeWeirdOnes)
            {
                AllEnumerables_IncludingWeird = ret;
            }
            else
            {
                AllEnumerables_NotWeird = ret;
            }

            return ret;
        }

        internal static ParameterInfo[] GetParameters(MethodInfo mtd)
        {
            ParameterInfo[] ps;
            if (!Parameters.TryGetValue(mtd, out ps))
            {
                Parameters[mtd] = ps = mtd.GetParameters();
            }

            return ps;
        }

        public static bool Implements(Type enumerable, Type opInterface, out List<string> missing)
        {
            missing = new List<string>();

            MethodInfo[] enumerableMtds;
            if (!EnumerableMethods.TryGetValue(enumerable, out enumerableMtds))
            {
                EnumerableMethods[enumerable] = enumerableMtds = enumerable.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
            }

            MethodInfo[] interfaceMtds;
            if (!InterfaceMethods.TryGetValue(opInterface, out interfaceMtds))
            {
                InterfaceMethods[opInterface] = interfaceMtds = opInterface.GetMethods();
            }

            var withNameCache = new Dictionary<string, List<MethodInfo>>();
            Func<string, List<MethodInfo>> withName =
                name =>
                {
                    List<MethodInfo> mtds;
                    if (!withNameCache.TryGetValue(name, out mtds))
                    {
                        withNameCache[name] = mtds = enumerableMtds.Where(n => n.Name == name).ToList();
                    }

                    return mtds;
                };

            var arityCache = new Dictionary<Tuple<List<MethodInfo>, int>, List<MethodInfo>>();
            Func<List<MethodInfo>, int, List<MethodInfo>> ofArity =
                (sameName, arity) =>
                {
                    var key = Tuple.Create(sameName, arity);
                    List<MethodInfo> mtds;
                    if (!arityCache.TryGetValue(key, out mtds))
                    {
                        arityCache[key] = mtds = sameName.Where(n => GetParameters(n).Length == arity).ToList();
                    }

                    return mtds;
                };

            foreach (var i in interfaceMtds)
            {
                var sameName = withName(i.Name);

                if (sameName.Count == 0)
                {
                    missing.Add(i.Name);
                    continue;
                }

                var iParams = GetParameters(i);

                var sameArity = ofArity(sameName, iParams.Length);
                if (sameArity.Count == 0)
                {
                    missing.Add(i.Name);
                    continue;
                }

                var foundMatch = false;
                foreach (var n in sameArity)
                {
                    var nParams = GetParameters(n);
                    var areEqual = true;

                    for (var ix = 0; ix < iParams.Length; ix++)
                    {
                        var ip = iParams[ix];
                        var np = nParams[ix];

                        if (!Equivalent(ip, np))
                        {
                            areEqual = false;
                            break;
                        }
                    }

                    if (areEqual)
                    {
                        foundMatch = true;
                        break;
                    }
                }

                if (!foundMatch)
                {
                    missing.Add(i.Name);
                }
            }

            return missing.Count == 0;
        }

        internal static Type[] GetGenericArguments(Type t)
        {
            Type[] ret;
            if (!GenericArguments.TryGetValue(t, out ret))
            {
                GenericArguments[t] = ret = t.GetGenericArguments();
            }

            return ret;
        }
        internal static Type GetGenericTypeDefinition(Type t)
        {
            Type ret;
            if (!GenericTypeDefinition.TryGetValue(t, out ret))
            {
                GenericTypeDefinition[t] = ret = t.GetGenericTypeDefinition();
            }

            return ret;
        }
        internal static Type GetElementType(Type t)
        {
            Type ret;
            if (!ElementType.TryGetValue(t, out ret))
            {
                ElementType[t] = ret = t.GetElementType();
            }

            return ret;
        }

        static bool Equivalent(ParameterInfo interfaceParam, ParameterInfo concreteParam)
        {
            var iType = interfaceParam.ParameterType;
            var cType = concreteParam.ParameterType;

            if (iType.ContainsGenericParameters && !cType.ContainsGenericParameters) return false;
            if (!iType.ContainsGenericParameters && cType.ContainsGenericParameters) return false;

            if (!iType.ContainsGenericParameters && !cType.ContainsGenericParameters)
            {
                return iType == cType;
            }

            var iGenParams = GetGenericArguments(iType);
            var cGenParams = GetGenericArguments(cType);

            if (iGenParams.Length != cGenParams.Length) return false;

            for (var i = 0; i < iGenParams.Length; i++)
            {
                var iGP = iGenParams[i];
                var cGP = cGenParams[i];

                if (iGP.IsGenericParameter) continue;

                if (iGP.IsGenericType && cGP.IsGenericType)
                {
                    var iGPG = GetGenericTypeDefinition(iGP);
                    var cGPG = GetGenericTypeDefinition(cGP);

                    if (iGPG != cGPG) return false;

                    continue;
                }

                if (iGP.IsArray && !cGP.IsArray) return false;
                if (!iGP.IsArray && cGP.IsArray) return false;

                if (iGP.IsArray && cGP.IsArray)
                {
                    var iGPE = GetElementType(iGP);
                    var cGPE = GetElementType(cGP);

                    if (iGPE.IsGenericParameter) continue;

                    if (iGPE != cGPE) return false;
                }

                if (iGP != cGP) return false;
            }

            return true;
        }

        public static IEnumerable<dynamic> GetMalformedEnumerables<T>()
        {
            yield return new BoxedEnumerable<T>();
            yield return new CastEnumerable<object, T, IdentityEnumerable<object, System.Collections.IEnumerable, IEnumerableBridger, IdentityEnumerator>, IdentityEnumerator>();
            yield return new ConcatEnumerable<T, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>>();
            yield return new DefaultIfEmptyDefaultEnumerable<T, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>>();
            yield return new DefaultIfEmptySpecificEnumerable<T, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>>();
            yield return new DistinctDefaultEnumerable<T, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>>();
            yield return new DistinctSpecificEnumerable<T, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>>();
            yield return new EmptyEnumerable<T>();
            yield return new EmptyOrderedEnumerable<T>();
            yield return new ExceptDefaultEnumerable<T, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>>();
            yield return new ExceptSpecificEnumerable<T, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>>();
            yield return new GroupByCollectionDefaultEnumerable<object, int, string, T, IdentityEnumerable<object, IEnumerable<object>, IEnumerableBridger<object>, IdentityEnumerator<object>>, IdentityEnumerator<object>>();
            yield return new GroupByCollectionSpecificEnumerable<object, int, string, T, IdentityEnumerable<object, IEnumerable<object>, IEnumerableBridger<object>, IdentityEnumerator<object>>, IdentityEnumerator<object>>();
            // skipping GroupBy*
            yield return new GroupedEnumerable<object, T>();
            yield return new GroupingEnumerable<object, T>();
            yield return new GroupJoinDefaultEnumerable<T, string, string, IdentityEnumerable<string, IEnumerable<string>, IEnumerableBridger<string>, IdentityEnumerator<string>>, IdentityEnumerator<string>, string, IdentityEnumerable<string, IEnumerable<string>, IEnumerableBridger<string>, IdentityEnumerator<string>>, IdentityEnumerator<string>>();
            yield return new GroupJoinSpecificEnumerable<T, string, string, IdentityEnumerable<string, IEnumerable<string>, IEnumerableBridger<string>, IdentityEnumerator<string>>, IdentityEnumerator<string>, string, IdentityEnumerable<string, IEnumerable<string>, IEnumerableBridger<string>, IdentityEnumerator<string>>, IdentityEnumerator<string>>();
            yield return new IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>();
            yield return new IntersectDefaultEnumerable<T, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>>();
            yield return new IntersectSpecificEnumerable<T, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>>();
            yield return new JoinDefaultEnumerable<T, string, string, IdentityEnumerable<string, IEnumerable<string>, IEnumerableBridger<string>, IdentityEnumerator<string>>, IdentityEnumerator<string>, string, IdentityEnumerable<string, IEnumerable<string>, IEnumerableBridger<string>, IdentityEnumerator<string>>, IdentityEnumerator<string>>();
            yield return new JoinSpecificEnumerable<T, string, string, IdentityEnumerable<string, IEnumerable<string>, IEnumerableBridger<string>, IdentityEnumerator<string>>, IdentityEnumerator<string>, string, IdentityEnumerable<string, IEnumerable<string>, IEnumerableBridger<string>, IdentityEnumerator<string>>, IdentityEnumerator<string>>();
            // skipping Lookup
            yield return new OfTypeEnumerable<object, T, IdentityEnumerable<object, System.Collections.IEnumerable, IEnumerableBridger, IdentityEnumerator>, IdentityEnumerator>();
            yield return new OrderByEnumerable<T, int, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>, DefaultAscending<T, int>>();
            yield return new RangeEnumerable<T>();
            yield return new RepeatEnumerable<T>();
            yield return new ReverseEnumerable<T, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>>();
            yield return new ReverseRangeEnumerable<T>();
            yield return new SelectEnumerable<object, T, IdentityEnumerable<object, IEnumerable<object>, IEnumerableBridger<object>, IdentityEnumerator<object>>, IdentityEnumerator<object>>();
            yield return new SelectIndexedEnumerable<object, T, IdentityEnumerable<object, IEnumerable<object>, IEnumerableBridger<object>, IdentityEnumerator<object>>, IdentityEnumerator<object>>();
            yield return new SelectManyEnumerable<object, T, IdentityEnumerable<object, IEnumerable<object>, IEnumerableBridger<object>, IdentityEnumerator<object>>, IdentityEnumerator<object>, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>>();
            yield return new SelectManyBridgeEnumerable<object, T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerable<object, IEnumerable<object>, IEnumerableBridger<object>, IdentityEnumerator<object>>, IdentityEnumerator<object>, IdentityEnumerator<T>>();
            yield return new SelectManyCollectionEnumerable<object, T, string, IdentityEnumerable<object, IEnumerable<object>, IEnumerableBridger<object>, IdentityEnumerator<object>>, IdentityEnumerator<object>, IdentityEnumerable<string, IEnumerable<string>, IEnumerableBridger<string>, IdentityEnumerator<string>>, IdentityEnumerator<string>>();
            yield return new SelectManyCollectionBridgeEnumerable<object, T, string, IEnumerable<string>, IEnumerableBridger<string>, IdentityEnumerable<object, IEnumerable<object>, IEnumerableBridger<object>, IdentityEnumerator<object>>, IdentityEnumerator<object>, IdentityEnumerator<string>>();
            yield return new SelectManyCollectionIndexedEnumerable<object, T, string, IdentityEnumerable<object, IEnumerable<object>, IEnumerableBridger<object>, IdentityEnumerator<object>>, IdentityEnumerator<object>, IdentityEnumerable<string, IEnumerable<string>, IEnumerableBridger<string>, IdentityEnumerator<string>>, IdentityEnumerator<string>>();
            yield return new SelectManyCollectionIndexedBridgeEnumerable<object, T, string, IEnumerable<string>, IEnumerableBridger<string>, IdentityEnumerable<object, IEnumerable<object>, IEnumerableBridger<object>, IdentityEnumerator<object>>, IdentityEnumerator<object>, IdentityEnumerator<string>>();
            yield return new SelectManyIndexedEnumerable<object, T, RepeatEnumerable<object>, RepeatEnumerator<object>, DefaultIfEmptySpecificEnumerable<T, EmptyEnumerable<T>, EmptyEnumerator<T>>, DefaultIfEmptySpecificEnumerator<T, EmptyEnumerator<T>>>();
            yield return new SelectManyIndexedBridgeEnumerable<object, T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerable<object, IEnumerable<object>, IEnumerableBridger<object>, IdentityEnumerator<object>>, IdentityEnumerator<object>, IdentityEnumerator<T>>();
            yield return new SelectSelectEnumerable<T, object, IdentityEnumerable<object, IEnumerable<object>, IEnumerableBridger<object>, IdentityEnumerator<object>>, IdentityEnumerator<object>, SingleProjection<T, object>>();
            yield return new SelectWhereEnumerable<T, object, IdentityEnumerable<object, IEnumerable<object>, IEnumerableBridger<object>, IdentityEnumerator<object>>, IdentityEnumerator<object>, SingleProjection<T, object>, SinglePredicate<T>>();
            yield return new SkipEnumerable<T, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>>();
            yield return new SkipWhileEnumerable<T, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>>();
            yield return new SkipWhileIndexedEnumerable<T, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>>();
            yield return new TakeEnumerable<T, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>>();
            yield return new TakeWhileEnumerable<T, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>>();
            yield return new TakeWhileIndexedEnumerable<T, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>>();
            yield return new UnionDefaultEnumerable<T, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>>();
            yield return new UnionSpecificEnumerable<T, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>>();
            yield return new WhereEnumerable<T, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>>();
            yield return new WhereIndexedEnumerable<T, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>>();
            yield return new WhereSelectEnumerable<T, object, IdentityEnumerable<object, IEnumerable<object>, IEnumerableBridger<object>, IdentityEnumerator<object>>, IdentityEnumerator<object>, SinglePredicate<object>, SingleProjection<T, object>>();
            yield return new WhereWhereEnumerable<T, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>, SinglePredicate<T>>();
            yield return new ZipEnumerable<T, int, int, IdentityEnumerable<int, IEnumerable<int>, IEnumerableBridger<int>, IdentityEnumerator<int>>, IdentityEnumerator<int>, IdentityEnumerable<int, IEnumerable<int>, IEnumerableBridger<int>, IdentityEnumerator<int>>, IdentityEnumerator<int>>();
        }

        public static void ForEachMalformedEnumerableExpression<T>(
            string pattern,
            params Type[] ignore
        )
        {
            var patterns =
                new[]
                {
                    ActionTemplate<BoxedEnumerable<T>>(pattern, ignore),
                    ActionTemplate<CastEnumerable<object, T, IdentityEnumerable<object, System.Collections.IEnumerable, IEnumerableBridger, IdentityEnumerator>, IdentityEnumerator>>(pattern, ignore),
                    ActionTemplate<ConcatEnumerable<T, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>>>(pattern, ignore),
                    ActionTemplate<DefaultIfEmptyDefaultEnumerable<T, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>>>(pattern, ignore),
                    ActionTemplate<DefaultIfEmptySpecificEnumerable<T, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>>>(pattern, ignore),
                    ActionTemplate<DistinctDefaultEnumerable<T, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>>>(pattern, ignore),
                    ActionTemplate<DistinctSpecificEnumerable<T, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>>>(pattern, ignore),
                    ActionTemplate<EmptyEnumerable<T>>(pattern, ignore),
                    ActionTemplate<EmptyOrderedEnumerable<T>>(pattern, ignore),
                    ActionTemplate<ExceptDefaultEnumerable<T, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>>>(pattern, ignore),
                    ActionTemplate<ExceptSpecificEnumerable<T, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>>>(pattern, ignore),
                    ActionTemplate<GroupByCollectionDefaultEnumerable<object, int, string, T, IdentityEnumerable<object, IEnumerable<object>, IEnumerableBridger<object>, IdentityEnumerator<object>>, IdentityEnumerator<object>>>(pattern, ignore),
                    ActionTemplate<GroupByCollectionSpecificEnumerable<object, int, string, T, IdentityEnumerable<object, IEnumerable<object>, IEnumerableBridger<object>, IdentityEnumerator<object>>, IdentityEnumerator<object>>>(pattern, ignore),
                    // skipping GroupBy*
                    ActionTemplate<GroupedEnumerable<object, T>>(pattern, ignore),
                    ActionTemplate<GroupingEnumerable<object, T>>(pattern, ignore),
                    ActionTemplate<GroupJoinDefaultEnumerable<T, string, string, IdentityEnumerable<string, IEnumerable<string>, IEnumerableBridger<string>, IdentityEnumerator<string>>, IdentityEnumerator<string>, string, IdentityEnumerable<string, IEnumerable<string>, IEnumerableBridger<string>, IdentityEnumerator<string>>, IdentityEnumerator<string>>>(pattern, ignore),
                    ActionTemplate<GroupJoinSpecificEnumerable<T, string, string, IdentityEnumerable<string, IEnumerable<string>, IEnumerableBridger<string>, IdentityEnumerator<string>>, IdentityEnumerator<string>, string, IdentityEnumerable<string, IEnumerable<string>, IEnumerableBridger<string>, IdentityEnumerator<string>>, IdentityEnumerator<string>>>(pattern, ignore),
                    ActionTemplate<IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>>(pattern, ignore),
                    ActionTemplate<IntersectDefaultEnumerable<T, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>>>(pattern, ignore),
                    ActionTemplate<IntersectSpecificEnumerable<T, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>>>(pattern, ignore),
                    ActionTemplate<JoinDefaultEnumerable<T, string, string, IdentityEnumerable<string, IEnumerable<string>, IEnumerableBridger<string>, IdentityEnumerator<string>>, IdentityEnumerator<string>, string, IdentityEnumerable<string, IEnumerable<string>, IEnumerableBridger<string>, IdentityEnumerator<string>>, IdentityEnumerator<string>>>(pattern, ignore),
                    ActionTemplate<JoinSpecificEnumerable<T, string, string, IdentityEnumerable<string, IEnumerable<string>, IEnumerableBridger<string>, IdentityEnumerator<string>>, IdentityEnumerator<string>, string, IdentityEnumerable<string, IEnumerable<string>, IEnumerableBridger<string>, IdentityEnumerator<string>>, IdentityEnumerator<string>>>(pattern, ignore),
                    // skipping Lookup
                    ActionTemplate<OfTypeEnumerable<object, T, IdentityEnumerable<object, System.Collections.IEnumerable, IEnumerableBridger, IdentityEnumerator>, IdentityEnumerator>>(pattern, ignore),
                    ActionTemplate<OrderByEnumerable<T, int, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>, DefaultAscending<T, int>>>(pattern, ignore),
                    ActionTemplate<RangeEnumerable<T>>(pattern, ignore),
                    ActionTemplate<RepeatEnumerable<T>>(pattern, ignore),
                    ActionTemplate<ReverseEnumerable<T, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>>>(pattern, ignore),
                    ActionTemplate<ReverseRangeEnumerable<T>>(pattern, ignore),
                    ActionTemplate<SelectEnumerable<object, T, IdentityEnumerable<object, IEnumerable<object>, IEnumerableBridger<object>, IdentityEnumerator<object>>, IdentityEnumerator<object>>>(pattern, ignore),
                    ActionTemplate<SelectIndexedEnumerable<object, T, IdentityEnumerable<object, IEnumerable<object>, IEnumerableBridger<object>, IdentityEnumerator<object>>, IdentityEnumerator<object>>>(pattern, ignore),
                    ActionTemplate<SelectManyEnumerable<object, T, IdentityEnumerable<object, IEnumerable<object>, IEnumerableBridger<object>, IdentityEnumerator<object>>, IdentityEnumerator<object>, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>>>(pattern, ignore),
                    ActionTemplate<SelectManyBridgeEnumerable<object, T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerable<object, IEnumerable<object>, IEnumerableBridger<object>, IdentityEnumerator<object>>, IdentityEnumerator<object>, IdentityEnumerator<T>>>(pattern, ignore),
                    ActionTemplate<SelectManyCollectionEnumerable<object, T, string, IdentityEnumerable<object, IEnumerable<object>, IEnumerableBridger<object>, IdentityEnumerator<object>>, IdentityEnumerator<object>, IdentityEnumerable<string, IEnumerable<string>, IEnumerableBridger<string>, IdentityEnumerator<string>>, IdentityEnumerator<string>>>(pattern, ignore),
                    ActionTemplate<SelectManyCollectionBridgeEnumerable<object, T, string, IEnumerable<string>, IEnumerableBridger<string>, IdentityEnumerable<object, IEnumerable<object>, IEnumerableBridger<object>, IdentityEnumerator<object>>, IdentityEnumerator<object>, IdentityEnumerator<string>>>(pattern, ignore),
                    ActionTemplate<SelectManyCollectionIndexedEnumerable<object, T, string, IdentityEnumerable<object, IEnumerable<object>, IEnumerableBridger<object>, IdentityEnumerator<object>>, IdentityEnumerator<object>, IdentityEnumerable<string, IEnumerable<string>, IEnumerableBridger<string>, IdentityEnumerator<string>>, IdentityEnumerator<string>>>(pattern, ignore),
                    ActionTemplate<SelectManyCollectionIndexedBridgeEnumerable<object, T, string, IEnumerable<string>, IEnumerableBridger<string>, IdentityEnumerable<object, IEnumerable<object>, IEnumerableBridger<object>, IdentityEnumerator<object>>, IdentityEnumerator<object>, IdentityEnumerator<string>>>(pattern, ignore),
                    ActionTemplate<SelectManyIndexedEnumerable<object, T, RepeatEnumerable<object>, RepeatEnumerator<object>, DefaultIfEmptySpecificEnumerable<T, EmptyEnumerable<T>, EmptyEnumerator<T>>, DefaultIfEmptySpecificEnumerator<T, EmptyEnumerator<T>>>>(pattern, ignore),
                    ActionTemplate<SelectManyIndexedBridgeEnumerable<object, T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerable<object, IEnumerable<object>, IEnumerableBridger<object>, IdentityEnumerator<object>>, IdentityEnumerator<object>, IdentityEnumerator<T>>>(pattern, ignore),
                    ActionTemplate<SelectSelectEnumerable<T, object, IdentityEnumerable<object, IEnumerable<object>, IEnumerableBridger<object>, IdentityEnumerator<object>>, IdentityEnumerator<object>, SingleProjection<T, object>>>(pattern, ignore),
                    ActionTemplate<SelectWhereEnumerable<T, object, IdentityEnumerable<object, IEnumerable<object>, IEnumerableBridger<object>, IdentityEnumerator<object>>, IdentityEnumerator<object>, SingleProjection<T, object>, SinglePredicate<T>>>(pattern, ignore),
                    ActionTemplate<SkipEnumerable<T, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>>>(pattern, ignore),
                    ActionTemplate<SkipWhileEnumerable<T, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>>>(pattern, ignore),
                    ActionTemplate<SkipWhileIndexedEnumerable<T, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>>>(pattern, ignore),
                    ActionTemplate<TakeEnumerable<T, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>>>(pattern, ignore),
                    ActionTemplate<TakeWhileEnumerable<T, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>>>(pattern, ignore),
                    ActionTemplate<TakeWhileIndexedEnumerable<T, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>>>(pattern, ignore),
                    ActionTemplate<WhereEnumerable<T, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>>>(pattern, ignore),
                    ActionTemplate<UnionDefaultEnumerable<T, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>>>(pattern, ignore),
                    ActionTemplate<UnionSpecificEnumerable<T, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>>>(pattern, ignore),
                    ActionTemplate<WhereIndexedEnumerable<T, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>>>(pattern, ignore),
                    ActionTemplate<WhereSelectEnumerable<T, object, IdentityEnumerable<object, IEnumerable<object>, IEnumerableBridger<object>, IdentityEnumerator<object>>, IdentityEnumerator<object>, SinglePredicate<object>, SingleProjection<T, object>>>(pattern, ignore),
                    ActionTemplate<WhereWhereEnumerable<T, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>, SinglePredicate<T>>>(pattern, ignore),
                    ActionTemplate<ZipEnumerable<T, int, int, IdentityEnumerable<int, IEnumerable<int>, IEnumerableBridger<int>, IdentityEnumerator<int>>, IdentityEnumerator<int>, IdentityEnumerable<int, IEnumerable<int>, IEnumerableBridger<int>, IdentityEnumerator<int>>, IdentityEnumerator<int>>>(pattern, ignore)

                    /*ActionTemplate<BoxedEnumerable<T>>(pattern, ignore),
                    ActionTemplate<CastEnumerable<object, T, IdentityEnumerable<object, System.Collections.IEnumerable, IdentityEnumerator>, IdentityEnumerator>>(pattern, ignore),
                    ActionTemplate<ConcatEnumerable<T, IdentityEnumerable<T, IEnumerable<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>, IdentityEnumerable<T, IEnumerable<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>>>(pattern, ignore),
                    ActionTemplate<DefaultIfEmptyDefaultEnumerable<T, IdentityEnumerable<T, IEnumerable<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>>>(pattern, ignore),
                    ActionTemplate<DefaultIfEmptySpecificEnumerable<T, IdentityEnumerable<T, IEnumerable<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>>>(pattern, ignore),
                    ActionTemplate<DistinctDefaultEnumerable<T, IdentityEnumerable<T, IEnumerable<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>>>(pattern, ignore),
                    ActionTemplate<DistinctSpecificEnumerable<T, IdentityEnumerable<T, IEnumerable<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>>>(pattern, ignore),
                    ActionTemplate<EmptyEnumerable<T>>(pattern, ignore),
                    ActionTemplate<EmptyOrderedEnumerable<T>>(pattern, ignore),
                    ActionTemplate<ExceptDefaultEnumerable<T, IdentityEnumerable<T, IEnumerable<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>, IdentityEnumerable<T, IEnumerable<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>>>(pattern, ignore),
                    ActionTemplate<ExceptSpecificEnumerable<T, IdentityEnumerable<T, IEnumerable<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>, IdentityEnumerable<T, IEnumerable<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>>>(pattern, ignore),
                    ActionTemplate<GroupByCollectionDefaultEnumerable<object, int, string, T, IdentityEnumerable<object, IEnumerable<object>, IdentityEnumerator<object>>, IdentityEnumerator<object>>>(pattern, ignore),
                    ActionTemplate<GroupByCollectionSpecificEnumerable<object, int, string, T, IdentityEnumerable<object, IEnumerable<object>, IdentityEnumerator<object>>, IdentityEnumerator<object>>>(pattern, ignore),
                    ActionTemplate<GroupedEnumerable<object, T>>(pattern, ignore),
                    ActionTemplate<GroupingEnumerable<object, T>>(pattern, ignore),
                    ActionTemplate<GroupJoinDefaultEnumerable<T, string, string, IdentityEnumerable<string, IEnumerable<string>, IdentityEnumerator<string>>, IdentityEnumerator<string>, string, IdentityEnumerable<string, IEnumerable<string>, IdentityEnumerator<string>>, IdentityEnumerator<string>>>(pattern, ignore),
                    ActionTemplate<GroupJoinSpecificEnumerable<T, string, string, IdentityEnumerable<string, IEnumerable<string>, IdentityEnumerator<string>>, IdentityEnumerator<string>, string, IdentityEnumerable<string, IEnumerable<string>, IdentityEnumerator<string>>, IdentityEnumerator<string>>>(pattern, ignore),
                    ActionTemplate<IdentityEnumerable<T, IEnumerable<T>, IdentityEnumerator<T>>>(pattern, ignore),
                    ActionTemplate<IntersectDefaultEnumerable<T, IdentityEnumerable<T, IEnumerable<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>, IdentityEnumerable<T, IEnumerable<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>>>(pattern, ignore),
                    ActionTemplate<IntersectSpecificEnumerable<T, IdentityEnumerable<T, IEnumerable<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>, IdentityEnumerable<T, IEnumerable<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>>>(pattern, ignore),
                    ActionTemplate<JoinDefaultEnumerable<T, string, string, IdentityEnumerable<string, IEnumerable<string>, IdentityEnumerator<string>>, IdentityEnumerator<string>, string, IdentityEnumerable<string, IEnumerable<string>, IdentityEnumerator<string>>, IdentityEnumerator<string>>>(pattern, ignore),
                    ActionTemplate<JoinSpecificEnumerable<T, string, string, IdentityEnumerable<string, IEnumerable<string>, IdentityEnumerator<string>>, IdentityEnumerator<string>, string, IdentityEnumerable<string, IEnumerable<string>, IdentityEnumerator<string>>, IdentityEnumerator<string>>>(pattern, ignore),
                    ActionTemplate<OfTypeEnumerable<object, T, IdentityEnumerable<object, System.Collections.IEnumerable, IdentityEnumerator>, IdentityEnumerator>>(pattern, ignore),
                    ActionTemplate<OrderByEnumerable<T, int, IdentityEnumerable<T, IEnumerable<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>, DefaultAscending<T, int>>>(pattern, ignore),
                    ActionTemplate<RangeEnumerable<T>>(pattern, ignore),
                    ActionTemplate<RepeatEnumerable<T>>(pattern, ignore),
                    ActionTemplate<ReverseEnumerable<T, IdentityEnumerable<T, IEnumerable<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>>>(pattern, ignore),
                    ActionTemplate<ReverseRangeEnumerable<T>>(pattern, ignore),
                    ActionTemplate<SelectEnumerable<object, T, IdentityEnumerable<object, IEnumerable<object>, IdentityEnumerator<object>>, IdentityEnumerator<object>>>(pattern, ignore),
                    ActionTemplate<SelectIndexedEnumerable<object, T, IdentityEnumerable<object, IEnumerable<object>, IdentityEnumerator<object>>, IdentityEnumerator<object>>>(pattern, ignore),
                    ActionTemplate<SelectManyBridgeEnumerable<object, T, IEnumerable<T>, IdentityEnumerable<object, IEnumerable<object>, IdentityEnumerator<object>>, IdentityEnumerator<object>, IdentityEnumerator<T>>>(pattern, ignore),
                    ActionTemplate<SelectManyIndexedBridgeEnumerable<object, T, IEnumerable<T>, IdentityEnumerable<object, IEnumerable<object>, IdentityEnumerator<object>>, IdentityEnumerator<object>, IdentityEnumerator<T>>>(pattern, ignore),
                    ActionTemplate<SelectManyEnumerable<object, T, IdentityEnumerable<object, IEnumerable<object>, IdentityEnumerator<object>>, IdentityEnumerator<object>, IdentityEnumerable<T, IEnumerable<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>>>(pattern, ignore),
                    ActionTemplate<SelectManyIndexedEnumerable<object, T, RepeatEnumerable<object>, RepeatEnumerator<object>, DefaultIfEmptySpecificEnumerable<T, EmptyEnumerable<T>, EmptyEnumerator<T>>, DefaultIfEmptySpecificEnumerator<T, EmptyEnumerator<T>>>>(pattern, ignore),
                    ActionTemplate<SelectManyCollectionBridgeEnumerable<object, T, string, IEnumerable<string>, IdentityEnumerable<object, IEnumerable<object>, IdentityEnumerator<object>>, IdentityEnumerator<object>, IdentityEnumerator<string>>>(pattern, ignore),
                    ActionTemplate<SelectManyCollectionIndexedBridgeEnumerable<object, T, string, IEnumerable<string>, IdentityEnumerable<object, IEnumerable<object>, IdentityEnumerator<object>>, IdentityEnumerator<object>, IdentityEnumerator<string>>>(pattern, ignore),
                    ActionTemplate<SelectManyCollectionEnumerable<object, T, string, IdentityEnumerable<object, IEnumerable<object>, IdentityEnumerator<object>>, IdentityEnumerator<object>, IdentityEnumerable<string, IEnumerable<string>, IdentityEnumerator<string>>, IdentityEnumerator<string>>>(pattern, ignore),
                    ActionTemplate<SelectManyCollectionIndexedEnumerable<object, T, string, IdentityEnumerable<object, IEnumerable<object>, IdentityEnumerator<object>>, IdentityEnumerator<object>, IdentityEnumerable<string, IEnumerable<string>, IdentityEnumerator<string>>, IdentityEnumerator<string>>>(pattern, ignore),
                    ActionTemplate<SelectSelectEnumerable<T, object, IdentityEnumerable<object, IEnumerable<object>, IdentityEnumerator<object>>, IdentityEnumerator<object>, SingleProjection<T, object>>>(pattern, ignore),
                    ActionTemplate<SelectWhereEnumerable<T, object, IdentityEnumerable<object, IEnumerable<object>, IdentityEnumerator<object>>, IdentityEnumerator<object>, SingleProjection<T, object>, SinglePredicate<T>>>(pattern, ignore),
                    ActionTemplate<SkipEnumerable<T, IdentityEnumerable<T, IEnumerable<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>>>(pattern, ignore),
                    ActionTemplate<SkipWhileEnumerable<T, IdentityEnumerable<T, IEnumerable<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>>>(pattern, ignore),
                    ActionTemplate<SkipWhileIndexedEnumerable<T, IdentityEnumerable<T, IEnumerable<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>>>(pattern, ignore),
                    ActionTemplate<TakeEnumerable<T, IdentityEnumerable<T, IEnumerable<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>>>(pattern, ignore),
                    ActionTemplate<TakeWhileEnumerable<T, IdentityEnumerable<T, IEnumerable<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>>>(pattern, ignore),
                    ActionTemplate<TakeWhileIndexedEnumerable<T, IdentityEnumerable<T, IEnumerable<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>>>(pattern, ignore),
                    ActionTemplate<UnionDefaultEnumerable<T, IdentityEnumerable<T, IEnumerable<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>, IdentityEnumerable<T, IEnumerable<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>>>(pattern, ignore),
                    ActionTemplate<UnionSpecificEnumerable<T, IdentityEnumerable<T, IEnumerable<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>, IdentityEnumerable<T, IEnumerable<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>>>(pattern, ignore),
                    ActionTemplate<WhereEnumerable<T, IdentityEnumerable<T, IEnumerable<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>>>(pattern, ignore),
                    ActionTemplate<WhereIndexedEnumerable<T, IdentityEnumerable<T, IEnumerable<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>>>(pattern, ignore),
                    ActionTemplate<WhereSelectEnumerable<T, object, IdentityEnumerable<object, IEnumerable<object>, IdentityEnumerator<object>>, IdentityEnumerator<object>, SinglePredicate<object>, SingleProjection<T, object>>>(pattern, ignore),
                    ActionTemplate<WhereWhereEnumerable<T, IdentityEnumerable<T, IEnumerable<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>, SinglePredicate<T>>>(pattern, ignore),
                    ActionTemplate<ZipEnumerable<T, int, int, IdentityEnumerable<int, IEnumerable<int>, IdentityEnumerator<int>>, IdentityEnumerator<int>, IdentityEnumerable<int, IEnumerable<int>, IdentityEnumerator<int>>, IdentityEnumerator<int>>>(pattern, ignore)*/
                };

            var dels = CompileDelegates(patterns);

            ForEachMalformedEnumerable<T>(
                dels[0],
                dels[1],
                dels[2],
                dels[3],
                dels[4],
                dels[5],
                dels[6],
                dels[7],
                dels[8],
                dels[9],
                dels[10],
                dels[11],
                dels[12],
                dels[13],
                dels[14],
                dels[15],
                dels[16],
                dels[17],
                dels[18],
                dels[19],
                dels[20],
                dels[21],
                dels[22],
                dels[23],
                dels[24],
                dels[25],
                dels[26],
                dels[27],
                dels[28],
                dels[29],
                dels[30],
                dels[31],
                dels[32],
                dels[33],
                dels[34],
                dels[35],
                dels[36],
                dels[37],
                dels[38],
                dels[39],
                dels[40],
                dels[41],
                dels[42],
                dels[43],
                dels[44],
                dels[45],
                dels[46],
                dels[47],
                dels[48],
                dels[49],
                dels[50],
                dels[51],
                dels[52]
            );
        }


        static void ForEachMalformedEnumerable<T>(
                Delegate f1,
                Delegate f2,
                Delegate f3,
                Delegate f4,
                Delegate f5,
                Delegate f6,
                Delegate f7,
                Delegate f8,
                Delegate f9,
                Delegate f10,
                Delegate f11,
                Delegate f12,
                Delegate f13,
                Delegate f14,
                Delegate f15,
                Delegate f16,
                Delegate f17,
                Delegate f18,
                Delegate f19,
                Delegate f20,
                Delegate f21,
                Delegate f22,
                Delegate f23,
                Delegate f24,
                Delegate f25,
                Delegate f26,
                Delegate f27,
                Delegate f28,
                Delegate f29,
                Delegate f30,
                Delegate f31,
                Delegate f32,
                Delegate f33,
                Delegate f34,
                Delegate f35,
                Delegate f36,
                Delegate f37,
                Delegate f38,
                Delegate f39,
                Delegate f40,
                Delegate f41,
                Delegate f42,
                Delegate f43,
                Delegate f44,
                Delegate f45,
                Delegate f46,
                Delegate f47,
                Delegate f48,
                Delegate f49,
                Delegate f50,
                Delegate f51,
                Delegate f52,
                Delegate f53
            )
        {
            var lookup = new Dictionary<Type, Delegate>();

            var funcs = new Delegate[] { f1, f2, f3, f4, f5, f6, f7, f8, f9, f10, f11, f12, f13, f14, f15, f16, f17, f18, f19, f20, f21, f22, f23, f24, f25, f26, f27, f28, f29, f30, f31, f32, f33, f34, f35, f36, f37, f38, f39, f40, f41, f42, f43, f44, f45, f46, f47, f48, f49, f50, f51, f52, f53 };
            foreach (var f in funcs)
            {
                var left = GetGenericArguments(f.GetType())[0];
                lookup.Add(left, f);
            }

            foreach (object e in GetMalformedEnumerables<T>())
            {
                Delegate del;
                if (!lookup.TryGetValue(e.GetType(), out del))
                {
                    throw new Exception($"No function found for {e.GetType().FullName}");
                }

                del.DynamicInvoke(e);
            }
        }

        public static readonly object NoCallValue = new object();

        static Tuple<string, string> FuncTemplate<T, V>(string pattern, Type[] ignore)
        {
            var func = (ParenthesizedLambdaExpressionSyntax)SyntaxFactory.ParseExpression(pattern);

            var firstType = typeof(T);
            var firstName = ProperTypeName(firstType);
            var firstParam = func.ParameterList.Parameters[0];
            var explicitFirstParam =
                SyntaxFactory.Parameter(
                        SyntaxFactory.List<AttributeListSyntax>(),
                        SyntaxFactory.TokenList(),
                        SyntaxFactory.ParseTypeName(firstName).WithTrailingTrivia(SyntaxFactory.Whitespace(" ")),
                        firstParam.Identifier,
                        null
                );

            var secondType = typeof(V);
            var secondName = ProperTypeName(secondType);
            var secondParam = func.ParameterList.Parameters[1];
            var explicitSecondParam =
                SyntaxFactory.Parameter(
                        SyntaxFactory.List<AttributeListSyntax>(),
                        SyntaxFactory.TokenList(),
                        SyntaxFactory.ParseTypeName(secondName).WithTrailingTrivia(SyntaxFactory.Whitespace(" ")),
                        secondParam.Identifier,
                        null
                );

            var paramList = SyntaxFactory.ParameterList(SyntaxFactory.SeparatedList(new[] { explicitFirstParam, explicitSecondParam }));

            var updatedFunc =
                SyntaxFactory.ParenthesizedLambdaExpression(
                    paramList,
                    func.Body
                );

            var genType = secondType;
            if (genType.IsGenericType && !genType.IsGenericTypeDefinition)
            {
                genType = GetGenericTypeDefinition(genType);
            }

            var makeEmpty = false;
            foreach (var i in ignore)
            {
                var iGen = i;
                if (iGen.IsGenericType && !iGen.IsGenericTypeDefinition)
                {
                    iGen = GetGenericTypeDefinition(iGen);
                }

                if (iGen == genType)
                {
                    makeEmpty = true;
                    break;
                }
            }

            if (makeEmpty)
            {
                var noCallBody = SyntaxFactory.MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression, SyntaxFactory.ParseTypeName("Helper"), (SimpleNameSyntax)SyntaxFactory.ParseName("NoCallValue"));
                updatedFunc = updatedFunc.WithBody(noCallBody);
            }

            string updatedFuncStr;
            using (var writer = new StringWriter())
            {
                updatedFunc.WriteTo(writer);
                updatedFuncStr = writer.ToString();
            }

            var funcName = $"Func<{firstName}, {secondName}, object>";

            return Tuple.Create(funcName, updatedFuncStr);
        }

        public static void ForEachEnumerableExpression<T, V>(
                V outerTerm,
                T[] initialSequence,
                Action<List<dynamic>> checker,
                string pattern,
                params Type[] ignore
        )
        {
            var patterns =
                new[]
                {
                     FuncTemplate<V, IEnumerable<T>>(pattern, ignore),
                     FuncTemplate<V, Dictionary<T, object>.KeyCollection>(pattern, ignore),
                     FuncTemplate<V, Dictionary<object, T>.ValueCollection>(pattern, ignore),
                     FuncTemplate<V, HashSet<T>>(pattern, ignore),
                     FuncTemplate<V, LinkedList<T>>(pattern, ignore),
                     FuncTemplate<V, List<T>>(pattern, ignore),
                     FuncTemplate<V, Queue<T>>(pattern, ignore),
                     FuncTemplate<V, SortedDictionary<T, object>.KeyCollection>(pattern, ignore),
                     FuncTemplate<V, SortedDictionary<object, T>.ValueCollection>(pattern, ignore),
                     FuncTemplate<V, SortedSet<T>>(pattern, ignore),
                     FuncTemplate<V, Stack<T>>(pattern, ignore),
                     FuncTemplate<V, T[]>(pattern, ignore),
                     FuncTemplate<V, BoxedEnumerable<T>>(pattern, ignore),
                     FuncTemplate<V, CastEnumerable<object, T, IdentityEnumerable<object, System.Collections.IEnumerable, IEnumerableBridger, IdentityEnumerator>, IdentityEnumerator>>(pattern, ignore),
                     FuncTemplate<V, ConcatEnumerable<T, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>>>(pattern, ignore),
                     FuncTemplate<V, DefaultIfEmptyDefaultEnumerable<T, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>>>(pattern, ignore),
                     FuncTemplate<V, DefaultIfEmptySpecificEnumerable<T, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>>>(pattern, ignore),
                     FuncTemplate<V, DistinctDefaultEnumerable<T, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>>>(pattern, ignore),
                     FuncTemplate<V, DistinctSpecificEnumerable<T, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>>>(pattern, ignore),
                     FuncTemplate<V, EmptyEnumerable<T>>(pattern, ignore),
                     FuncTemplate<V, EmptyOrderedEnumerable<T>>(pattern, ignore),
                     FuncTemplate<V, ExceptDefaultEnumerable<T, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>>>(pattern, ignore),
                     FuncTemplate<V, ExceptSpecificEnumerable<T, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>>>(pattern, ignore),
                     FuncTemplate<V, GroupByCollectionDefaultEnumerable<object, int, string, T, IdentityEnumerable<object, IEnumerable<object>,  IEnumerableBridger<object>, IdentityEnumerator<object>>, IdentityEnumerator<object>>>(pattern, ignore),
                     FuncTemplate<V, GroupByCollectionSpecificEnumerable<object, int, string, T, IdentityEnumerable<object, IEnumerable<object>, IEnumerableBridger<object>, IdentityEnumerator<object>>, IdentityEnumerator<object>>>(pattern, ignore),
                     FuncTemplate<V, GroupByDefaultEnumerable<object, object, T, IdentityEnumerable<object, IEnumerable<object>,  IEnumerableBridger<object>,IdentityEnumerator<object>>, IdentityEnumerator<object>>>(pattern, ignore),
                     FuncTemplate<V, GroupBySpecificEnumerable<object, object, T, IdentityEnumerable<object, IEnumerable<object>, IEnumerableBridger<object>, IdentityEnumerator<object>>, IdentityEnumerator<object>>>(pattern, ignore),
                     FuncTemplate<V, GroupedEnumerable<object, T>>(pattern, ignore),
                     FuncTemplate<V, GroupingEnumerable<object, T>>(pattern, ignore),
                     FuncTemplate<V, GroupJoinDefaultEnumerable<T, string, string, IdentityEnumerable<string, IEnumerable<string>, IEnumerableBridger<string>, IdentityEnumerator<string>>, IdentityEnumerator<string>, string, IdentityEnumerable<string, IEnumerable<string>, IEnumerableBridger<string>, IdentityEnumerator<string>>, IdentityEnumerator<string>>>(pattern, ignore),
                     FuncTemplate<V, GroupJoinSpecificEnumerable<T, string, string, IdentityEnumerable<string, IEnumerable<string>, IEnumerableBridger<string>, IdentityEnumerator<string>>, IdentityEnumerator<string>, string, IdentityEnumerable<string, IEnumerable<string>, IEnumerableBridger<string>, IdentityEnumerator<string>>, IdentityEnumerator<string>>>(pattern, ignore),
                     FuncTemplate<V, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>>(pattern, ignore),
                     FuncTemplate<V, IntersectDefaultEnumerable<T, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>>>(pattern, ignore),
                     FuncTemplate<V, IntersectSpecificEnumerable<T, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>>>(pattern, ignore),
                     FuncTemplate<V, JoinDefaultEnumerable<T, string, string, IdentityEnumerable<string, IEnumerable<string>, IEnumerableBridger<string>, IdentityEnumerator<string>>, IdentityEnumerator<string>, string, IdentityEnumerable<string, IEnumerable<string>, IEnumerableBridger<string>, IdentityEnumerator<string>>, IdentityEnumerator<string>>>(pattern, ignore),
                     FuncTemplate<V, JoinSpecificEnumerable<T, string, string, IdentityEnumerable<string, IEnumerable<string>, IEnumerableBridger<string>, IdentityEnumerator<string>>, IdentityEnumerator<string>, string, IdentityEnumerable<string, IEnumerable<string>, IEnumerableBridger<string>, IdentityEnumerator<string>>, IdentityEnumerator<string>>>(pattern, ignore),
                     FuncTemplate<V, LookupDefaultEnumerable<T, T>>(pattern, ignore),
                     FuncTemplate<V, LookupSpecificEnumerable<T, T>>(pattern, ignore),
                     FuncTemplate<V, OfTypeEnumerable<object, T, IdentityEnumerable<object, System.Collections.IEnumerable, IEnumerableBridger, IdentityEnumerator>, IdentityEnumerator>>(pattern, ignore),
                     FuncTemplate<V, OrderByEnumerable<T, int, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>, DefaultAscending<T, int>>>(pattern, ignore),
                     FuncTemplate<V, RangeEnumerable<T>>(pattern, ignore),
                     FuncTemplate<V, RepeatEnumerable<T>>(pattern, ignore),
                     FuncTemplate<V, ReverseEnumerable<T, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>>>(pattern, ignore),
                     FuncTemplate<V, ReverseRangeEnumerable<T>>(pattern, ignore),
                     FuncTemplate<V, SelectEnumerable<object, T, IdentityEnumerable<object, IEnumerable<object>, IEnumerableBridger<object>, IdentityEnumerator<object>>, IdentityEnumerator<object>>>(pattern, ignore),
                     FuncTemplate<V, SelectIndexedEnumerable<object, T, IdentityEnumerable<object, IEnumerable<object>, IEnumerableBridger<object>, IdentityEnumerator<object>>, IdentityEnumerator<object>>>(pattern, ignore),
                     FuncTemplate<V, SelectManyBridgeEnumerable<object, T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerable<object, IEnumerable<object>, IEnumerableBridger<object>, IdentityEnumerator<object>>, IdentityEnumerator<object>, IdentityEnumerator<T>>>(pattern, ignore),
                     FuncTemplate<V, SelectManyIndexedBridgeEnumerable<object, T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerable<object, IEnumerable<object>, IEnumerableBridger<object>, IdentityEnumerator<object>>, IdentityEnumerator<object>, IdentityEnumerator<T>>>(pattern, ignore),
                     FuncTemplate<V, SelectManyEnumerable<object, T, IdentityEnumerable<object, IEnumerable<object>, IEnumerableBridger<object>, IdentityEnumerator<object>>, IdentityEnumerator<object>, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>>>(pattern, ignore),
                     FuncTemplate<V, SelectManyIndexedEnumerable<object, T, RepeatEnumerable<object>, RepeatEnumerator<object>, DefaultIfEmptySpecificEnumerable<T, EmptyEnumerable<T>, EmptyEnumerator<T>>, DefaultIfEmptySpecificEnumerator<T, EmptyEnumerator<T>>>>(pattern, ignore),
                     FuncTemplate<V, SelectManyCollectionBridgeEnumerable<object, T, string, IEnumerable<string>, IEnumerableBridger<string>, IdentityEnumerable<object, IEnumerable<object>,  IEnumerableBridger<object>,IdentityEnumerator<object>>, IdentityEnumerator<object>, IdentityEnumerator<string>>>(pattern, ignore),
                     FuncTemplate<V, SelectManyCollectionIndexedBridgeEnumerable<object, T, string, IEnumerable<string>, IEnumerableBridger<string>, IdentityEnumerable<object, IEnumerable<object>, IEnumerableBridger<object>, IdentityEnumerator<object>>, IdentityEnumerator<object>, IdentityEnumerator<string>>>(pattern, ignore),
                     FuncTemplate<V, SelectManyCollectionEnumerable<object, T, string, IdentityEnumerable<object, IEnumerable<object>, IEnumerableBridger<object>, IdentityEnumerator<object>>, IdentityEnumerator<object>, IdentityEnumerable<string, IEnumerable<string>, IEnumerableBridger<string>, IdentityEnumerator<string>>, IdentityEnumerator<string>>>(pattern, ignore),
                     FuncTemplate<V, SelectManyCollectionIndexedEnumerable<object, T, string, IdentityEnumerable<object, IEnumerable<object>, IEnumerableBridger<object>, IdentityEnumerator<object>>, IdentityEnumerator<object>, IdentityEnumerable<string, IEnumerable<string>, IEnumerableBridger<string>, IdentityEnumerator<string>>, IdentityEnumerator<string>>>(pattern, ignore),
                     FuncTemplate<V, SelectSelectEnumerable<T, object, IdentityEnumerable<object, IEnumerable<object>,  IEnumerableBridger<object>,IdentityEnumerator<object>>, IdentityEnumerator<object>, SingleProjection<T, object>>>(pattern, ignore),
                     FuncTemplate<V, SelectWhereEnumerable<T, object, IdentityEnumerable<object, IEnumerable<object>,  IEnumerableBridger<object>,IdentityEnumerator<object>>, IdentityEnumerator<object>, SingleProjection<T, object>, SinglePredicate<T>>>(pattern, ignore),
                     FuncTemplate<V, SkipEnumerable<T, IdentityEnumerable<T, IEnumerable<T>,  IEnumerableBridger<T>,IdentityEnumerator<T>>, IdentityEnumerator<T>>>(pattern, ignore),
                     FuncTemplate<V, SkipWhileEnumerable<T, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>>>(pattern, ignore),
                     FuncTemplate<V, SkipWhileIndexedEnumerable<T, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>>>(pattern, ignore),
                     FuncTemplate<V, TakeEnumerable<T, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>>>(pattern, ignore),
                     FuncTemplate<V, TakeWhileEnumerable<T, IdentityEnumerable<T, IEnumerable<T>,  IEnumerableBridger<T>,IdentityEnumerator<T>>, IdentityEnumerator<T>>>(pattern, ignore),
                     FuncTemplate<V, TakeWhileIndexedEnumerable<T, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>>>(pattern, ignore),
                     FuncTemplate<V, UnionDefaultEnumerable<T, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>>>(pattern, ignore),
                     FuncTemplate<V, UnionSpecificEnumerable<T, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>>>(pattern, ignore),
                     FuncTemplate<V, WhereEnumerable<T, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>>>(pattern, ignore),
                     FuncTemplate<V, WhereIndexedEnumerable<T, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>>>(pattern, ignore),
                     FuncTemplate<V, WhereSelectEnumerable<T, object, IdentityEnumerable<object, IEnumerable<object>,  IEnumerableBridger<object>,IdentityEnumerator<object>>, IdentityEnumerator<object>, SinglePredicate<object>, SingleProjection<T, object>>>(pattern, ignore),
                     FuncTemplate<V, WhereWhereEnumerable<T, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>, SinglePredicate<T>>>(pattern, ignore),
                     FuncTemplate<V, ZipEnumerable<T, int, int, IdentityEnumerable<int, IEnumerable<int>, IEnumerableBridger<int>, IdentityEnumerator<int>>, IdentityEnumerator<int>, IdentityEnumerable<int, IEnumerable<int>, IEnumerableBridger<int>, IdentityEnumerator<int>>, IdentityEnumerator<int>>>(pattern, ignore)
                };

            var dels = CompileDelegates(patterns);

            ForEachEnumerable<T>(
                initialSequence,
                checker,
                inner => dels[0].DynamicInvoke(outerTerm, inner),
                inner => dels[1].DynamicInvoke(outerTerm, inner),
                inner => dels[2].DynamicInvoke(outerTerm, inner),
                inner => dels[3].DynamicInvoke(outerTerm, inner),
                inner => dels[4].DynamicInvoke(outerTerm, inner),
                inner => dels[5].DynamicInvoke(outerTerm, inner),
                inner => dels[6].DynamicInvoke(outerTerm, inner),
                inner => dels[7].DynamicInvoke(outerTerm, inner),
                inner => dels[8].DynamicInvoke(outerTerm, inner),
                inner => dels[9].DynamicInvoke(outerTerm, inner),
                inner => dels[10].DynamicInvoke(outerTerm, inner),
                inner => dels[11].DynamicInvoke(outerTerm, inner),
                inner => dels[12].DynamicInvoke(outerTerm, inner),
                inner => dels[13].DynamicInvoke(outerTerm, inner),
                inner => dels[14].DynamicInvoke(outerTerm, inner),
                inner => dels[15].DynamicInvoke(outerTerm, inner),
                inner => dels[16].DynamicInvoke(outerTerm, inner),
                inner => dels[17].DynamicInvoke(outerTerm, inner),
                inner => dels[18].DynamicInvoke(outerTerm, inner),
                inner => dels[19].DynamicInvoke(outerTerm, inner),
                inner => dels[20].DynamicInvoke(outerTerm, inner),
                inner => dels[21].DynamicInvoke(outerTerm, inner),
                inner => dels[22].DynamicInvoke(outerTerm, inner),
                inner => dels[23].DynamicInvoke(outerTerm, inner),
                inner => dels[24].DynamicInvoke(outerTerm, inner),
                inner => dels[25].DynamicInvoke(outerTerm, inner),
                inner => dels[26].DynamicInvoke(outerTerm, inner),
                inner => dels[27].DynamicInvoke(outerTerm, inner),
                inner => dels[28].DynamicInvoke(outerTerm, inner),
                inner => dels[29].DynamicInvoke(outerTerm, inner),
                inner => dels[30].DynamicInvoke(outerTerm, inner),
                inner => dels[31].DynamicInvoke(outerTerm, inner),
                inner => dels[32].DynamicInvoke(outerTerm, inner),
                inner => dels[33].DynamicInvoke(outerTerm, inner),
                inner => dels[34].DynamicInvoke(outerTerm, inner),
                inner => dels[35].DynamicInvoke(outerTerm, inner),
                inner => dels[36].DynamicInvoke(outerTerm, inner),
                inner => dels[37].DynamicInvoke(outerTerm, inner),
                inner => dels[38].DynamicInvoke(outerTerm, inner),
                inner => dels[39].DynamicInvoke(outerTerm, inner),
                inner => dels[40].DynamicInvoke(outerTerm, inner),
                inner => dels[41].DynamicInvoke(outerTerm, inner),
                inner => dels[42].DynamicInvoke(outerTerm, inner),
                inner => dels[43].DynamicInvoke(outerTerm, inner),
                inner => dels[44].DynamicInvoke(outerTerm, inner),
                inner => dels[45].DynamicInvoke(outerTerm, inner),
                inner => dels[46].DynamicInvoke(outerTerm, inner),
                inner => dels[47].DynamicInvoke(outerTerm, inner),
                inner => dels[48].DynamicInvoke(outerTerm, inner),
                inner => dels[49].DynamicInvoke(outerTerm, inner),
                inner => dels[50].DynamicInvoke(outerTerm, inner),
                inner => dels[51].DynamicInvoke(outerTerm, inner),
                inner => dels[52].DynamicInvoke(outerTerm, inner),
                inner => dels[53].DynamicInvoke(outerTerm, inner),
                inner => dels[54].DynamicInvoke(outerTerm, inner),
                inner => dels[55].DynamicInvoke(outerTerm, inner),
                inner => dels[56].DynamicInvoke(outerTerm, inner),
                inner => dels[57].DynamicInvoke(outerTerm, inner),
                inner => dels[58].DynamicInvoke(outerTerm, inner),
                inner => dels[59].DynamicInvoke(outerTerm, inner),
                inner => dels[60].DynamicInvoke(outerTerm, inner),
                inner => dels[61].DynamicInvoke(outerTerm, inner),
                inner => dels[62].DynamicInvoke(outerTerm, inner),
                inner => dels[63].DynamicInvoke(outerTerm, inner),
                inner => dels[64].DynamicInvoke(outerTerm, inner),
                inner => dels[65].DynamicInvoke(outerTerm, inner),
                inner => dels[66].DynamicInvoke(outerTerm, inner),
                inner => dels[67].DynamicInvoke(outerTerm, inner),
                inner => dels[68].DynamicInvoke(outerTerm, inner)
            );
        }

        static void ForEachEnumerable<T>(
                T[] initialSequence,
                Action<List<dynamic>> checker,
                Func<IEnumerable<T>, object> b1,
                Func<Dictionary<T, object>.KeyCollection, object> b2,
                Func<Dictionary<object, T>.ValueCollection, object> b3,
                Func<HashSet<T>, object> b4,
                Func<LinkedList<T>, object> b5,
                Func<List<T>, object> b6,
                Func<Queue<T>, object> b7,
                Func<SortedDictionary<T, object>.KeyCollection, object> b8,
                Func<SortedDictionary<object, T>.ValueCollection, object> b9,
                Func<SortedSet<T>, object> b10,
                Func<Stack<T>, object> b11,
                Func<T[], object> b12,
                Func<BoxedEnumerable<T>, object> f1,
                Func<CastEnumerable<object, T, IdentityEnumerable<object, System.Collections.IEnumerable, IEnumerableBridger, IdentityEnumerator>, IdentityEnumerator>, object> f2,
                Func<ConcatEnumerable<T, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>>, object> f3,
                Func<DefaultIfEmptyDefaultEnumerable<T, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>>, object> f4,
                Func<DefaultIfEmptySpecificEnumerable<T, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>>, object> f5,
                Func<DistinctDefaultEnumerable<T, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>>, object> f6,
                Func<DistinctSpecificEnumerable<T, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>>, object> f7,
                Func<EmptyEnumerable<T>, object> f8,
                Func<EmptyOrderedEnumerable<T>, object> f9,
                Func<ExceptDefaultEnumerable<T, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>>, object> f10,
                Func<ExceptSpecificEnumerable<T, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>>, object> f11,
                Func<GroupByCollectionDefaultEnumerable<object, int, string, T, IdentityEnumerable<object, IEnumerable<object>, IEnumerableBridger<object>, IdentityEnumerator<object>>, IdentityEnumerator<object>>, object> f12,
                Func<GroupByCollectionSpecificEnumerable<object, int, string, T, IdentityEnumerable<object, IEnumerable<object>, IEnumerableBridger<object>, IdentityEnumerator<object>>, IdentityEnumerator<object>>, object> f13,
                Func<GroupByDefaultEnumerable<object, object, T, IdentityEnumerable<object, IEnumerable<object>, IEnumerableBridger<object>, IdentityEnumerator<object>>, IdentityEnumerator<object>>, object> f14,
                Func<GroupBySpecificEnumerable<object, object, T, IdentityEnumerable<object, IEnumerable<object>, IEnumerableBridger<object>, IdentityEnumerator<object>>, IdentityEnumerator<object>>, object> f15,
                Func<GroupedEnumerable<object, T>, object> f16,
                Func<GroupingEnumerable<object, T>, object> f17,
                Func<GroupJoinDefaultEnumerable<T, string, string, IdentityEnumerable<string, IEnumerable<string>, IEnumerableBridger<string>, IdentityEnumerator<string>>, IdentityEnumerator<string>, string, IdentityEnumerable<string, IEnumerable<string>, IEnumerableBridger<string>, IdentityEnumerator<string>>, IdentityEnumerator<string>>, object> f18,
                Func<GroupJoinSpecificEnumerable<T, string, string, IdentityEnumerable<string, IEnumerable<string>, IEnumerableBridger<string>, IdentityEnumerator<string>>, IdentityEnumerator<string>, string, IdentityEnumerable<string, IEnumerable<string>, IEnumerableBridger<string>, IdentityEnumerator<string>>, IdentityEnumerator<string>>, object> f19,
                Func<IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, object> f20,
                Func<IntersectDefaultEnumerable<T, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>>, object> f21,
                Func<IntersectSpecificEnumerable<T, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>>, object> f22,
                Func<JoinDefaultEnumerable<T, string, string, IdentityEnumerable<string, IEnumerable<string>, IEnumerableBridger<string>, IdentityEnumerator<string>>, IdentityEnumerator<string>, string, IdentityEnumerable<string, IEnumerable<string>, IEnumerableBridger<string>,  IdentityEnumerator<string>>, IdentityEnumerator<string>>, object> f23,
                Func<JoinSpecificEnumerable<T, string, string, IdentityEnumerable<string, IEnumerable<string>, IEnumerableBridger<string>, IdentityEnumerator<string>>, IdentityEnumerator<string>, string, IdentityEnumerable<string, IEnumerable<string>, IEnumerableBridger<string>, IdentityEnumerator<string>>, IdentityEnumerator<string>>, object> f24,
                Func<LookupDefaultEnumerable<T, T>, object> f25,
                Func<LookupSpecificEnumerable<T, T>, object> f26,
                Func<OfTypeEnumerable<object, T, IdentityEnumerable<object, System.Collections.IEnumerable, IEnumerableBridger, IdentityEnumerator>, IdentityEnumerator>, object> f27,
                Func<OrderByEnumerable<T, int, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>, DefaultAscending<T, int>>, object> f28,
                Func<RangeEnumerable<T>, object> f29,
                Func<RepeatEnumerable<T>, object> f30,
                Func<ReverseEnumerable<T, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>>, object> f31,
                Func<ReverseRangeEnumerable<T>, object> f32,
                Func<SelectEnumerable<object, T, IdentityEnumerable<object, IEnumerable<object>, IEnumerableBridger<object>, IdentityEnumerator<object>>, IdentityEnumerator<object>>, object> f33,
                Func<SelectIndexedEnumerable<object, T, IdentityEnumerable<object, IEnumerable<object>, IEnumerableBridger<object>, IdentityEnumerator<object>>, IdentityEnumerator<object>>, object> f34,
                Func<SelectManyBridgeEnumerable<object, T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerable<object, IEnumerable<object>, IEnumerableBridger<object>, IdentityEnumerator<object>>, IdentityEnumerator<object>, IdentityEnumerator<T>>, object> f35,
                Func<SelectManyIndexedBridgeEnumerable<object, T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerable<object, IEnumerable<object>, IEnumerableBridger<object>, IdentityEnumerator<object>>, IdentityEnumerator<object>, IdentityEnumerator<T>>, object> f36,
                Func<SelectManyEnumerable<object, T, IdentityEnumerable<object, IEnumerable<object>, IEnumerableBridger<object>, IdentityEnumerator<object>>, IdentityEnumerator<object>, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>>, object> f37,
                Func<SelectManyIndexedEnumerable<object, T, RepeatEnumerable<object>, RepeatEnumerator<object>, DefaultIfEmptySpecificEnumerable<T, EmptyEnumerable<T>, EmptyEnumerator<T>>, DefaultIfEmptySpecificEnumerator<T, EmptyEnumerator<T>>>, object> f38,
                Func<SelectManyCollectionBridgeEnumerable<object, T, string, IEnumerable<string>, IEnumerableBridger<string>, IdentityEnumerable<object, IEnumerable<object>, IEnumerableBridger<object>, IdentityEnumerator<object>>, IdentityEnumerator<object>, IdentityEnumerator<string>>, object> f39,
                Func<SelectManyCollectionIndexedBridgeEnumerable<object, T, string, IEnumerable<string>, IEnumerableBridger<string>, IdentityEnumerable<object, IEnumerable<object>, IEnumerableBridger<object>, IdentityEnumerator<object>>, IdentityEnumerator<object>, IdentityEnumerator<string>>, object> f40,
                Func<SelectManyCollectionEnumerable<object, T, string, IdentityEnumerable<object, IEnumerable<object>, IEnumerableBridger<object>, IdentityEnumerator<object>>, IdentityEnumerator<object>, IdentityEnumerable<string, IEnumerable<string>, IEnumerableBridger<string>, IdentityEnumerator<string>>, IdentityEnumerator<string>>, object> f41,
                Func<SelectManyCollectionIndexedEnumerable<object, T, string, IdentityEnumerable<object, IEnumerable<object>, IEnumerableBridger<object>, IdentityEnumerator<object>>, IdentityEnumerator<object>, IdentityEnumerable<string, IEnumerable<string>, IEnumerableBridger<string>, IdentityEnumerator<string>>, IdentityEnumerator<string>>, object> f42,
                Func<SelectSelectEnumerable<T, object, IdentityEnumerable<object, IEnumerable<object>, IEnumerableBridger<object>, IdentityEnumerator<object>>, IdentityEnumerator<object>, SingleProjection<T, object>>, object> f43,
                Func<SelectWhereEnumerable<T, object, IdentityEnumerable<object, IEnumerable<object>, IEnumerableBridger<object>, IdentityEnumerator<object>>, IdentityEnumerator<object>, SingleProjection<T, object>, SinglePredicate<T>>, object> f44,
                Func<SkipEnumerable<T, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>>, object> f45,
                Func<SkipWhileEnumerable<T, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>>, object> f46,
                Func<SkipWhileIndexedEnumerable<T, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>>, object> f47,
                Func<TakeEnumerable<T, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>>, object> f48,
                Func<TakeWhileEnumerable<T, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>>, object> f49,
                Func<TakeWhileIndexedEnumerable<T, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>>, object> f50,
                Func<UnionDefaultEnumerable<T, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>>, object> f51,
                Func<UnionSpecificEnumerable<T, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>>, object> f52,
                Func<WhereEnumerable<T, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>>, object> f53,
                Func<WhereIndexedEnumerable<T, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>>, object> f54,
                Func<WhereSelectEnumerable<T, object, IdentityEnumerable<object, IEnumerable<object>, IEnumerableBridger<object>, IdentityEnumerator<object>>, IdentityEnumerator<object>, SinglePredicate<object>, SingleProjection<T, object>>, object> f55,
                Func<WhereWhereEnumerable<T, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>, SinglePredicate<T>>, object> f56,
                Func<ZipEnumerable<T, int, int, IdentityEnumerable<int, IEnumerable<int>, IEnumerableBridger<int>, IdentityEnumerator<int>>, IdentityEnumerator<int>, IdentityEnumerable<int, IEnumerable<int>, IEnumerableBridger<int>, IdentityEnumerator<int>>, IdentityEnumerator<int>>, object> f57
            )
        {
            // the interop types
            {
                var bridgeLookup = new Dictionary<Type, Delegate>();
                var bridgeFuncs = new Delegate[] { b2, b3, b4, b5, b6, b7, b8, b9, b10, b11, b12 };
                foreach (var f in bridgeFuncs)
                {
                    var left = GetGenericArguments(f.GetType())[0];
                    bridgeLookup.Add(left, f);
                }

                foreach (object e in GetBridgedEnumerables(initialSequence))
                {
                    Delegate del;
                    object right;
                    if (!bridgeLookup.TryGetValue(e.GetType(), out del))
                    {
                        if (b1 != null)
                        {
                            right = b1.DynamicInvoke(e);
                            b1 = null;
                        }
                        else
                        {
                            throw new Exception($"No function found for {e.GetType().FullName}");
                        }
                    }
                    else
                    {
                        right = del.DynamicInvoke(e);
                    }

                    if (object.ReferenceEquals(NoCallValue, right)) continue;

                    if (!right.GetType().IsValueType)
                    {
                        throw new Exception($"A non-value type enumerable was returned for {e}");
                    }

                    var mtd = GetEnumeratorMethod(right.GetType());
                    var i = mtd.Invoke(right, new object[0]);

                    if (!i.GetType().IsValueType)
                    {
                        throw new Exception($"A non-value type enumerator was returned for {right}");
                    }

                    var iType = i.GetType();

                    var dispose = GetDisposeMethod(iType);
                    var moveNext = GetMoveNextMethod(iType);
                    var current = GetCurrentGetMethod(iType);

                    var res = new List<object>();
                    try
                    {
                        while (true)
                        {
                            var moveNextRes = (bool)moveNext.Invoke(i, null);
                            if (!moveNextRes) break;

                            var currentRes = current.Invoke(i, null);
                            res.Add(currentRes);
                        }
                    }
                    finally
                    {
                        dispose.Invoke(i, null);
                    }

                    checker(res);
                }
            }

            // the free types
            {
                var lookup = new Dictionary<Type, Delegate>();

                var funcs = new Delegate[] { f1, f2, f3, f4, f5, f6, f7, f8, f9, f10, f11, f12, f13, f14, f15, f16, f17, f18, f19, f20, f21, f22, f23, f24, f25, f26, f27, f28, f29, f30, f31, f32, f33, f34, f35, f36, f37, f38, f39, f40, f41, f42, f43, f44, f45, f46, f47, f48, f49, f50, f51, f52, f53, f54, f55, f56, f57 };
                foreach (var f in funcs)
                {
                    var left = GetGenericArguments(f.GetType())[0];
                    lookup.Add(left, f);
                }

                foreach (object e in GetEnumerables(initialSequence))
                {
                    Delegate del;
                    if (!lookup.TryGetValue(e.GetType(), out del))
                    {
                        throw new Exception($"No function found for {e.GetType().FullName}");
                    }

                    object right = del.DynamicInvoke(e);
                    if (object.ReferenceEquals(NoCallValue, right)) continue;

                    if (!right.GetType().IsValueType)
                    {
                        throw new Exception($"A non-value type enumerable was returned for {e}");
                    }

                    var mtd = GetEnumeratorMethod(right.GetType());
                    var i = mtd.Invoke(right, new object[0]);

                    if (!i.GetType().IsValueType)
                    {
                        throw new Exception($"A non-value type enumerator was returned for {right}");
                    }

                    var iType = i.GetType();
                    var dispose = GetDisposeMethod(iType);
                    var moveNext = GetMoveNextMethod(iType);
                    var current = GetCurrentGetMethod(iType);

                    var res = new List<object>();
                    try
                    {
                        while (true)
                        {
                            var moveNextRes = (bool)moveNext.Invoke(i, null);
                            if (!moveNextRes) break;

                            var currentRes = current.Invoke(i, null);
                            res.Add(currentRes);
                        }
                    }
                    finally
                    {
                        dispose.Invoke(i, null);
                    }

                    checker(res);
                }
            }
        }

        static string ProperTypeName(Type type)
        {
            var name = type.FullName;
            if (type.IsArray)
            {
                if (type.GetArrayRank() != 1) throw new Exception("NOPE!");

                var elem = type.GetElementType();
                var elemName = ProperTypeName(elem);

                name = elemName + "[]";
            }
            else
            {
                if (type.IsGenericType)
                {
                    if (type.FullName.Contains('+'))
                    {
                        // just the key & value collections now
                        name = type.FullName;

                        var end = name.IndexOf('[');
                        name = name.Substring(0, end);

                        var ix = name.IndexOf('`');
                        var ij = ix + 1;
                        while (ij < name.Length && char.IsDigit(name[ij])) ij++;

                        var inner = "<";
                        foreach (var arg in GetGenericArguments(type))
                        {
                            inner += ProperTypeName(arg) + ",";
                        }
                        inner = inner.Substring(0, inner.Length - 1);
                        inner += ">";

                        name = name.Substring(0, ix) + inner + name.Substring(ij);
                    }
                    else
                    {
                        var ix = name.IndexOf('`');
                        name = name.Substring(0, ix);
                        name += "<";

                        foreach (var arg in GetGenericArguments(type))
                        {
                            name += ProperTypeName(arg) + ",";
                        }
                        name = name.Substring(0, name.Length - 1);
                        name += ">";
                    }
                }
            }

            name = name.Replace('+', '.');

            return name;
        }

        static Tuple<string, string> ActionTemplate<T>(string pattern, Type[] ignore)
        {
            var act = (SimpleLambdaExpressionSyntax)SyntaxFactory.ParseExpression(pattern);

            var type = typeof(T);
            var name = ProperTypeName(type);

            var firstParam = act.Parameter;
            var explicitParam =
                SyntaxFactory.Parameter(
                        SyntaxFactory.List<AttributeListSyntax>(),
                        SyntaxFactory.TokenList(),
                        SyntaxFactory.ParseTypeName(name).WithTrailingTrivia(SyntaxFactory.Whitespace(" ")),
                        firstParam.Identifier,
                        null
                );
            var paramList = SyntaxFactory.ParameterList(SyntaxFactory.SeparatedList(new[] { explicitParam }));

            var updatedAct =
                SyntaxFactory.ParenthesizedLambdaExpression(
                    paramList,
                    act.Body
                );

            var genType = type;
            if (genType.IsGenericType && !genType.IsGenericTypeDefinition)
            {
                genType = GetGenericTypeDefinition(genType);
            }

            var makeEmpty = false;
            foreach (var i in ignore)
            {
                var iGen = i;
                if (iGen.IsGenericType && !iGen.IsGenericTypeDefinition)
                {
                    iGen = GetGenericTypeDefinition(iGen);
                }

                if (iGen == genType)
                {
                    makeEmpty = true;
                    break;
                }
            }

            if (makeEmpty)
            {
                updatedAct = updatedAct.WithBody(SyntaxFactory.Block());
            }

            string updatedActStr;
            using (var writer = new StringWriter())
            {
                updatedAct.WriteTo(writer);
                updatedActStr = writer.ToString();
            }

            var actionDecl = $"Action<{name}>";

            return Tuple.Create(actionDecl, updatedActStr);
        }

        static int NextHolderId = 0;

        static Delegate[] CompileDelegates(Tuple<string, string>[] patterns)
        {
            var className = "DelegateHolder_" + (NextHolderId++);

            var fields = new StringBuilder();

            var actionNameLookup = new Dictionary<int, string>();

            for (var i = 0; i < patterns.Length; i++)
            {
                var t = patterns[i];

                var fieldName = $"Delegate_{(i)}";
                var fieldStr = $"public static {t.Item1} {fieldName} = {t.Item2};";

                actionNameLookup[i] = fieldName;

                fields.AppendLine(fieldStr);
            }

            var compilationUnit = $@"
using System;
using LinqAF;
using LinqAF.Tests;
using TestHelpers;
using System.Collections.Generic;
using System.Collections;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestHelpers.Generated
{{
    public static class {className}
    {{
        {fields.ToString()}
    }}
}}";
            var compileUnit = CSharpSyntaxTree.ParseText(compilationUnit);
            var compile = CSharpCompilation.Create(
                "GeneratedAssembly." + className,
                new[] { compileUnit },
                new[]
                {
                    MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
                    MetadataReference.CreateFromFile(typeof(Helper).Assembly.Location),
                    MetadataReference.CreateFromFile(typeof(LinqAF.Enumerable).Assembly.Location),
                    MetadataReference.CreateFromFile(typeof(System.Collections.IEnumerable).Assembly.Location),
                    MetadataReference.CreateFromFile(typeof(List<>).Assembly.Location),
                    MetadataReference.CreateFromFile(typeof(LinkedList<>).Assembly.Location),
                    MetadataReference.CreateFromFile(typeof(Microsoft.VisualStudio.TestTools.UnitTesting.Assert).Assembly.Location),
                    MetadataReference.CreateFromFile(typeof(Microsoft.CSharp.RuntimeBinder.RuntimeBinderException).Assembly.Location),
                    MetadataReference.CreateFromFile(typeof(Microsoft.CSharp.RuntimeBinder.Binder).Assembly.Location),
                    MetadataReference.CreateFromFile(typeof(ExpressionType).Assembly.Location),
                    MetadataReference.CreateFromFile(typeof(FileInfo).Assembly.Location)
                },
                new CSharpCompilationOptions(
                    OutputKind.DynamicallyLinkedLibrary
                )
            );

            Assembly dynAssembly;
            using (var ms = new MemoryStream())
            {
                var result = compile.Emit(ms);

                if (!result.Success)
                {
                    var failures =
                        string.Join("\n",
                            result.Diagnostics.Where(
                                diagnostic =>
                                diagnostic.IsWarningAsError ||
                                diagnostic.Severity == DiagnosticSeverity.Error
                            )
                            .Select(
                                d => $"{d.Id}: {d.GetMessage()} @{d.Location}"
                            ).AsEnumerable()
                        );

                    throw new Exception(failures);
                }
                else
                {
                    ms.Seek(0, SeekOrigin.Begin);
                    dynAssembly = Assembly.Load(ms.ToArray());
                }
            }

            var @class = dynAssembly.GetType("TestHelpers.Generated." + className);

            var ret = new Delegate[patterns.Length];
            foreach (var kv in actionNameLookup)
            {
                var index = kv.Key;
                var name = kv.Value;

                var field = @class.GetField(name, BindingFlags.Public | BindingFlags.Static);
                var action = (Delegate)field.GetValue(null);

                ret[index] = action;
            }

            return ret;
        }

        public static void ForEachEnumerableNoRetExpression<T>(
            T[] initialialSequence,
            string pattern,
            params Type[] ignore
        )
        {
            var patterns =
                new[]
                {
                    ActionTemplate<IEnumerable<T>>(pattern, ignore),
                    ActionTemplate<Dictionary<T, object>.KeyCollection>(pattern, ignore),
                    ActionTemplate<Dictionary<object, T>.ValueCollection>(pattern, ignore),
                    ActionTemplate<HashSet<T>>(pattern, ignore),
                    ActionTemplate<LinkedList<T>>(pattern, ignore),
                    ActionTemplate<List<T>>(pattern, ignore),
                    ActionTemplate<Queue<T>>(pattern, ignore),
                    ActionTemplate<SortedDictionary<T, object>.KeyCollection>(pattern, ignore),
                    ActionTemplate<SortedDictionary<object, T>.ValueCollection>(pattern, ignore),
                    ActionTemplate<SortedSet<T>>(pattern, ignore),
                    ActionTemplate<Stack<T>>(pattern, ignore),
                    ActionTemplate<T[]>(pattern, ignore),
                    ActionTemplate<BoxedEnumerable<T>>(pattern, ignore),
                    ActionTemplate<CastEnumerable<object, T, IdentityEnumerable<object, System.Collections.IEnumerable, IEnumerableBridger, IdentityEnumerator>, IdentityEnumerator>>(pattern, ignore),
                    ActionTemplate<ConcatEnumerable<T, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>>>(pattern, ignore),
                    ActionTemplate<DefaultIfEmptyDefaultEnumerable<T, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>>>(pattern, ignore),
                    ActionTemplate<DefaultIfEmptySpecificEnumerable<T, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>>>(pattern, ignore),
                    ActionTemplate<DistinctDefaultEnumerable<T, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>>>(pattern, ignore),
                    ActionTemplate<DistinctSpecificEnumerable<T, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>>>(pattern, ignore),
                    ActionTemplate<EmptyEnumerable<T>>(pattern, ignore),
                    ActionTemplate<EmptyOrderedEnumerable<T>>(pattern, ignore),
                    ActionTemplate<ExceptDefaultEnumerable<T, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>>>(pattern, ignore),
                    ActionTemplate<ExceptSpecificEnumerable<T, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>>>(pattern, ignore),
                    ActionTemplate<GroupByCollectionDefaultEnumerable<object, int, string, T, IdentityEnumerable<object, IEnumerable<object>, IEnumerableBridger<object>, IdentityEnumerator<object>>, IdentityEnumerator<object>>>(pattern, ignore),
                    ActionTemplate<GroupByCollectionSpecificEnumerable<object, int, string, T, IdentityEnumerable<object, IEnumerable<object>,  IEnumerableBridger<object>,IdentityEnumerator<object>>, IdentityEnumerator<object>>>(pattern, ignore),
                    ActionTemplate<GroupByDefaultEnumerable<object, object, T, IdentityEnumerable<object, IEnumerable<object>, IEnumerableBridger<object>, IdentityEnumerator<object>>, IdentityEnumerator<object>>>(pattern, ignore),
                    ActionTemplate<GroupBySpecificEnumerable<object, object, T, IdentityEnumerable<object, IEnumerable<object>, IEnumerableBridger<object>, IdentityEnumerator<object>>, IdentityEnumerator<object>>>(pattern, ignore),
                    ActionTemplate<GroupedEnumerable<object, T>>(pattern, ignore),
                    ActionTemplate<GroupingEnumerable<object, T>>(pattern, ignore),
                    ActionTemplate<GroupJoinDefaultEnumerable<T, string, string, IdentityEnumerable<string, IEnumerable<string>, IEnumerableBridger<string>, IdentityEnumerator<string>>, IdentityEnumerator<string>, string, IdentityEnumerable<string, IEnumerable<string>,IEnumerableBridger<string>, IdentityEnumerator<string>>, IdentityEnumerator<string>>>(pattern, ignore),
                    ActionTemplate<GroupJoinSpecificEnumerable<T, string, string, IdentityEnumerable<string, IEnumerable<string>, IEnumerableBridger<string>, IdentityEnumerator<string>>, IdentityEnumerator<string>, string, IdentityEnumerable<string, IEnumerable<string>,IEnumerableBridger<string>, IdentityEnumerator<string>>, IdentityEnumerator<string>>>(pattern, ignore),
                    ActionTemplate<IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>>(pattern, ignore),
                    ActionTemplate<IntersectDefaultEnumerable<T, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>>>(pattern, ignore),
                    ActionTemplate<IntersectSpecificEnumerable<T, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>>>(pattern, ignore),
                    ActionTemplate<JoinDefaultEnumerable<T, string, string, IdentityEnumerable<string, IEnumerable<string>, IEnumerableBridger<string>, IdentityEnumerator<string>>, IdentityEnumerator<string>, string, IdentityEnumerable<string, IEnumerable<string>,IEnumerableBridger<string>, IdentityEnumerator<string>>, IdentityEnumerator<string>>>(pattern, ignore),
                    ActionTemplate<JoinSpecificEnumerable<T, string, string, IdentityEnumerable<string, IEnumerable<string>, IEnumerableBridger<string>, IdentityEnumerator<string>>, IdentityEnumerator<string>, string, IdentityEnumerable<string, IEnumerable<string>, IEnumerableBridger<string>,IdentityEnumerator<string>>, IdentityEnumerator<string>>>(pattern, ignore),
                    ActionTemplate<LookupDefaultEnumerable<T, T>>(pattern, ignore),
                    ActionTemplate<LookupSpecificEnumerable<T, T>>(pattern, ignore),
                    ActionTemplate<OfTypeEnumerable<object, T, IdentityEnumerable<object, System.Collections.IEnumerable, IEnumerableBridger,IdentityEnumerator>, IdentityEnumerator>>(pattern, ignore),
                    ActionTemplate<OrderByEnumerable<T, int, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>, DefaultAscending<T, int>>>(pattern, ignore),
                    ActionTemplate<RangeEnumerable<T>>(pattern, ignore),
                    ActionTemplate<RepeatEnumerable<T>>(pattern, ignore),
                    ActionTemplate<ReverseEnumerable<T, IdentityEnumerable<T, IEnumerable<T>,  IEnumerableBridger<T>,IdentityEnumerator<T>>, IdentityEnumerator<T>>>(pattern, ignore),
                    ActionTemplate<ReverseRangeEnumerable<T>>(pattern, ignore),
                    ActionTemplate<SelectEnumerable<object, T, IdentityEnumerable<object, IEnumerable<object>, IEnumerableBridger<object>, IdentityEnumerator<object>>, IdentityEnumerator<object>>>(pattern, ignore),
                    ActionTemplate<SelectIndexedEnumerable<object, T, IdentityEnumerable<object, IEnumerable<object>,  IEnumerableBridger<object>,IdentityEnumerator<object>>, IdentityEnumerator<object>>>(pattern, ignore),
                    ActionTemplate<SelectManyBridgeEnumerable<object, T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerable<object, IEnumerable<object>, IEnumerableBridger<object>, IdentityEnumerator<object>>, IdentityEnumerator<object>, IdentityEnumerator<T>>>(pattern, ignore),
                    ActionTemplate<SelectManyIndexedBridgeEnumerable<object, T, IEnumerable<T>,  IEnumerableBridger<T>,IdentityEnumerable<object, IEnumerable<object>, IEnumerableBridger<object>, IdentityEnumerator<object>>, IdentityEnumerator<object>, IdentityEnumerator<T>>>(pattern, ignore),
                    ActionTemplate<SelectManyEnumerable<object, T, IdentityEnumerable<object, IEnumerable<object>,  IEnumerableBridger<object>,IdentityEnumerator<object>>, IdentityEnumerator<object>, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>>>(pattern, ignore),
                    ActionTemplate<SelectManyIndexedEnumerable<object, T, RepeatEnumerable<object>, RepeatEnumerator<object>, DefaultIfEmptySpecificEnumerable<T, EmptyEnumerable<T>, EmptyEnumerator<T>>, DefaultIfEmptySpecificEnumerator<T, EmptyEnumerator<T>>>>(pattern, ignore),
                    ActionTemplate<SelectManyCollectionBridgeEnumerable<object, T, string, IEnumerable<string>, IEnumerableBridger<string>, IdentityEnumerable<object, IEnumerable<object>, IEnumerableBridger<object>, IdentityEnumerator<object>>, IdentityEnumerator<object>, IdentityEnumerator<string>>>(pattern, ignore),
                    ActionTemplate<SelectManyCollectionIndexedBridgeEnumerable<object, T, string, IEnumerable<string>, IEnumerableBridger<string>, IdentityEnumerable<object, IEnumerable<object>, IEnumerableBridger<object>, IdentityEnumerator<object>>, IdentityEnumerator<object>, IdentityEnumerator<string>>>(pattern, ignore),
                    ActionTemplate<SelectManyCollectionEnumerable<object, T, string, IdentityEnumerable<object, IEnumerable<object>,  IEnumerableBridger<object>,IdentityEnumerator<object>>, IdentityEnumerator<object>, IdentityEnumerable<string, IEnumerable<string>,IEnumerableBridger<string>, IdentityEnumerator<string>>, IdentityEnumerator<string>>>(pattern, ignore),
                    ActionTemplate<SelectManyCollectionIndexedEnumerable<object, T, string, IdentityEnumerable<object, IEnumerable<object>,IEnumerableBridger<object>,  IdentityEnumerator<object>>, IdentityEnumerator<object>, IdentityEnumerable<string, IEnumerable<string>,IEnumerableBridger<string>, IdentityEnumerator<string>>, IdentityEnumerator<string>>>(pattern, ignore),
                    ActionTemplate<SelectSelectEnumerable<T, object, IdentityEnumerable<object, IEnumerable<object>, IEnumerableBridger<object>, IdentityEnumerator<object>>, IdentityEnumerator<object>, SingleProjection<T, object>>>(pattern, ignore),
                    ActionTemplate<SelectWhereEnumerable<T, object, IdentityEnumerable<object, IEnumerable<object>,  IEnumerableBridger<object>,IdentityEnumerator<object>>, IdentityEnumerator<object>, SingleProjection<T, object>, SinglePredicate<T>>>(pattern, ignore),
                    ActionTemplate<SkipEnumerable<T, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>>>(pattern, ignore),
                    ActionTemplate<SkipWhileEnumerable<T, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>>>(pattern, ignore),
                    ActionTemplate<SkipWhileIndexedEnumerable<T, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>>>(pattern, ignore),
                    ActionTemplate<TakeEnumerable<T, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>>>(pattern, ignore),
                    ActionTemplate<TakeWhileEnumerable<T, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>>>(pattern, ignore),
                    ActionTemplate<TakeWhileIndexedEnumerable<T, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>>>(pattern, ignore),
                    ActionTemplate<UnionDefaultEnumerable<T, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>>>(pattern, ignore),
                    ActionTemplate<UnionSpecificEnumerable<T, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>>>(pattern, ignore),
                    ActionTemplate<WhereEnumerable<T, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>>>(pattern, ignore),
                    ActionTemplate<WhereIndexedEnumerable<T, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>>>(pattern, ignore),
                    ActionTemplate<WhereSelectEnumerable<T, object, IdentityEnumerable<object, IEnumerable<object>,  IEnumerableBridger<object>,IdentityEnumerator<object>>, IdentityEnumerator<object>, SinglePredicate<object>, SingleProjection<T, object>>>(pattern, ignore),
                    ActionTemplate<WhereWhereEnumerable<T, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>, SinglePredicate<T>>>(pattern, ignore),
                    ActionTemplate<ZipEnumerable<T, int, int, IdentityEnumerable<int, IEnumerable<int>,IEnumerableBridger<int>,  IdentityEnumerator<int>>, IdentityEnumerator<int>, IdentityEnumerable<int, IEnumerable<int>,IEnumerableBridger<int>, IdentityEnumerator<int>>, IdentityEnumerator<int>>>(pattern, ignore)
                };

            var dels = CompileDelegates(patterns);

            ForEachEnumerableNoRet<T>(
                initialialSequence,
                dels[0],
                dels[1],
                dels[2],
                dels[3],
                dels[4],
                dels[5],
                dels[6],
                dels[7],
                dels[8],
                dels[9],
                dels[10],
                dels[11],
                dels[12],
                dels[13],
                dels[14],
                dels[15],
                dels[16],
                dels[17],
                dels[18],
                dels[19],
                dels[20],
                dels[21],
                dels[22],
                dels[23],
                dels[24],
                dels[25],
                dels[26],
                dels[27],
                dels[28],
                dels[29],
                dels[30],
                dels[31],
                dels[32],
                dels[33],
                dels[34],
                dels[35],
                dels[36],
                dels[37],
                dels[38],
                dels[39],
                dels[40],
                dels[41],
                dels[42],
                dels[43],
                dels[44],
                dels[45],
                dels[46],
                dels[47],
                dels[48],
                dels[49],
                dels[50],
                dels[51],
                dels[52],
                dels[53],
                dels[54],
                dels[55],
                dels[56],
                dels[57],
                dels[58],
                dels[59],
                dels[60],
                dels[61],
                dels[62],
                dels[63],
                dels[64],
                dels[65],
                dels[66],
                dels[67],
                dels[68]
            );
        }

        static void ForEachEnumerableNoRet<T>(
                T[] initialSequence,
                Delegate b1,
                Delegate b2,
                Delegate b3,
                Delegate b4,
                Delegate b5,
                Delegate b6,
                Delegate b7,
                Delegate b8,
                Delegate b9,
                Delegate b10,
                Delegate b11,
                Delegate b12,
                Delegate f1,
                Delegate f2,
                Delegate f3,
                Delegate f4,
                Delegate f5,
                Delegate f6,
                Delegate f7,
                Delegate f8,
                Delegate f9,
                Delegate f10,
                Delegate f11,
                Delegate f12,
                Delegate f13,
                Delegate f14,
                Delegate f15,
                Delegate f16,
                Delegate f17,
                Delegate f18,
                Delegate f19,
                Delegate f20,
                Delegate f21,
                Delegate f22,
                Delegate f23,
                Delegate f24,
                Delegate f25,
                Delegate f26,
                Delegate f27,
                Delegate f28,
                Delegate f29,
                Delegate f30,
                Delegate f31,
                Delegate f32,
                Delegate f33,
                Delegate f34,
                Delegate f35,
                Delegate f36,
                Delegate f37,
                Delegate f38,
                Delegate f39,
                Delegate f40,
                Delegate f41,
                Delegate f42,
                Delegate f43,
                Delegate f44,
                Delegate f45,
                Delegate f46,
                Delegate f47,
                Delegate f48,
                Delegate f49,
                Delegate f50,
                Delegate f51,
                Delegate f52,
                Delegate f53,
                Delegate f54,
                Delegate f55,
                Delegate f56,
                Delegate f57
            )
        {
            // the interop types
            {
                var bridgeLookup = new Dictionary<Type, Delegate>();
                var bridgeFuncs = new Delegate[] { b2, b3, b4, b5, b6, b7, b8, b9, b10, b11, b12 };
                foreach (var f in bridgeFuncs)
                {
                    var left = GetGenericArguments(f.GetType())[0];
                    bridgeLookup.Add(left, f);
                }

                foreach (object e in GetBridgedEnumerables(initialSequence))
                {
                    Delegate del;
                    if (!bridgeLookup.TryGetValue(e.GetType(), out del))
                    {
                        if (b1 != null)
                        {
                            b1.DynamicInvoke(e);
                            b1 = null;
                        }
                        else
                        {
                            throw new Exception($"No function found for {e.GetType().FullName}");
                        }
                    }
                    else
                    {
                        del.DynamicInvoke(e);
                    }
                }
            }

            // the free types
            {
                var lookup = new Dictionary<Type, Delegate>();
                var funcs = new Delegate[] { f1, f2, f3, f4, f5, f6, f7, f8, f9, f10, f11, f12, f13, f14, f15, f16, f17, f18, f19, f20, f21, f22, f23, f24, f25, f26, f27, f28, f29, f30, f31, f32, f33, f34, f35, f36, f37, f38, f39, f40, f41, f42, f43, f44, f45, f46, f47, f48, f49, f50, f51, f52, f53, f54, f55, f56, f57 };
                foreach (var f in funcs)
                {
                    var left = GetGenericArguments(f.GetType())[0];
                    lookup.Add(left, f);
                }

                foreach (object e in GetEnumerables(initialSequence))
                {
                    Delegate del;
                    if (!lookup.TryGetValue(e.GetType(), out del))
                    {
                        throw new Exception($"No function found for {e.GetType().FullName}");
                    }

                    del.DynamicInvoke(e);
                }

            }
        }

        class GetBridgedEnumerables_SameOrder<T> : IComparer<T>
        {
            private T[] Order;
            public GetBridgedEnumerables_SameOrder(T[] order)
            {
                Order = order;
            }

            public int Compare(T x, T y)
            {
                var xIx = Array.IndexOf(Order, x);
                var yIx = Array.IndexOf(Order, y);

                return Comparer<int>.Default.Compare(xIx, yIx);
            }
        }

        static IEnumerable<T> WrapperEnumerable<T>(T[] toYield)
        {
            for (var i = 0; i < toYield.Length; i++)
            {
                yield return toYield[i];
            }
        }

        public static IEnumerable<IEnumerable<T>> GetBridgedEnumerables<T>(T[] toYield)
        {
            yield return WrapperEnumerable(toYield);

            yield return toYield;

            {
                var dictKeys = new Dictionary<T, object>();
                foreach (var t in toYield)
                {
                    try
                    {
                        dictKeys.Add(t, new object());
                    }
                    catch (Exception) { }
                }
                yield return dictKeys.Keys;
            }

            {
                var dictValues = new Dictionary<object, T>();
                foreach (var t in toYield)
                {
                    dictValues.Add(new object(), t);
                }
                yield return dictValues.Values;
            }

            yield return new HashSet<T>(toYield);
            yield return new LinkedList<T>(toYield);
            yield return new List<T>(toYield);
            yield return new Queue<T>(toYield);

            {
                var sortedDictKeys = new SortedDictionary<T, object>(new GetBridgedEnumerables_SameOrder<T>(toYield));
                foreach (var t in toYield)
                {
                    try
                    {
                        sortedDictKeys.Add(t, new object());
                    }
                    catch (Exception) { }
                }
                yield return sortedDictKeys.Keys;
            }

            {
                var objects = new object[toYield.Length];
                for (int i = 0; i < toYield.Length; i++)
                {
                    objects[i] = new object();
                }

                var sortedDictValues = new SortedDictionary<object, T>(new GetBridgedEnumerables_SameOrder<object>(objects));
                for (var i = 0; i < toYield.Length; i++)
                {
                    var key = objects[i];
                    var val = toYield[i];
                    sortedDictValues.Add(key, val);
                }
                yield return sortedDictValues.Values;
            }

            yield return new SortedSet<T>(toYield, new GetBridgedEnumerables_SameOrder<T>(toYield));

            {
                var toYieldRev = new T[toYield.Length];
                Array.Copy(toYield, toYieldRev, toYield.Length);
                Array.Reverse(toYieldRev);
                yield return new Stack<T>(toYieldRev);
            }
        }

        public static IEnumerable<dynamic> GetEnumerables<T>(T[] toYield)
        {
            var box =
                GetYielding<
                    T,
                    BoxedEnumerable<T>,
                    BoxedEnumerator<T>
                >(toYield, null);
            yield return box;

            var cast =
                GetYielding<
                    T,
                    CastEnumerable<object, T, IdentityEnumerable<object, System.Collections.IEnumerable, IEnumerableBridger, IdentityEnumerator>, IdentityEnumerator>,
                    CastEnumerator<object, T, IdentityEnumerator>
                >(toYield, null);
            yield return cast;

            var concat =
                GetYielding<
                    T,
                    ConcatEnumerable<T, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>>,
                    ConcatEnumerator<T, IdentityEnumerator<T>, IdentityEnumerator<T>>
                >(toYield, null);
            yield return concat;

            var defaultIfEmptyDefault_NotEmpty =
                GetYielding<
                    T,
                    DefaultIfEmptyDefaultEnumerable<T, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>>,
                    DefaultIfEmptyDefaultEnumerator<T, IdentityEnumerator<T>>
                >(toYield, null);
            yield return defaultIfEmptyDefault_NotEmpty;

            var defaultIfEmptySpecific_NotEmpty =
                GetYielding<
                    T,
                    DefaultIfEmptySpecificEnumerable<T, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>>,
                    DefaultIfEmptySpecificEnumerator<T, IdentityEnumerator<T>>
                >(toYield, new object[] { default(T) });
            yield return defaultIfEmptySpecific_NotEmpty;

            var distinctDefault =
                GetYielding<
                    T,
                    DistinctDefaultEnumerable<T, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>>,
                    DistinctDefaultEnumerator<T, IdentityEnumerator<T>>
                >(toYield, null);
            yield return distinctDefault;

            var distinctSpecific =
                GetYielding<
                    T,
                    DistinctSpecificEnumerable<T, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>>,
                    DistinctSpecificEnumerator<T, IdentityEnumerator<T>>
                >(toYield, null);
            yield return distinctSpecific;

            // skipping Empty & EmptyOrdered

            var exceptDefault =
                GetYielding<
                    T,
                    ExceptDefaultEnumerable<T, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>>,
                    ExceptDefaultEnumerator<T, IdentityEnumerator<T>, IdentityEnumerator<T>>
                >(toYield, null);
            yield return exceptDefault;

            var exceptSpecific =
                GetYielding<
                    T,
                    ExceptSpecificEnumerable<T, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>>,
                    ExceptSpecificEnumerator<T, IdentityEnumerator<T>, IdentityEnumerator<T>>
                >(toYield, null);
            yield return exceptSpecific;

            var groupByCollectionDefault =
                GetYielding<
                    T,
                    GroupByCollectionDefaultEnumerable<object, int, string, T, IdentityEnumerable<object, IEnumerable<object>, IEnumerableBridger<object>, IdentityEnumerator<object>>, IdentityEnumerator<object>>,
                    GroupByCollectionDefaultEnumerator<object, int, string, T, IdentityEnumerator<object>>
                >(toYield, null);
            yield return groupByCollectionDefault;

            var groupByCollectionSpecific =
                GetYielding<
                    T,
                    GroupByCollectionSpecificEnumerable<object, int, string, T, IdentityEnumerable<object, IEnumerable<object>, IEnumerableBridger<object>, IdentityEnumerator<object>>, IdentityEnumerator<object>>,
                    GroupByCollectionSpecificEnumerator<object, int, string, T, IdentityEnumerator<object>>
                >(toYield, null);
            yield return groupByCollectionSpecific;

            // skipping GroupBy* intentionally

            var grouped =
                GetYielding<
                    T,
                    GroupedEnumerable<object, T>,
                    GroupedEnumerator<T>
                 >(toYield, null);
            yield return grouped;

            var grouping =
                GetYielding<
                    T,
                    GroupingEnumerable<object, T>,
                    GroupingEnumerator<T>
                 >(toYield, null);
            yield return grouping;

            var groupJoinDefault =
                GetYielding<
                    T,
                    GroupJoinDefaultEnumerable<T, string, string, IdentityEnumerable<string, IEnumerable<string>, IEnumerableBridger<string>, IdentityEnumerator<string>>, IdentityEnumerator<string>, string, IdentityEnumerable<string, IEnumerable<string>, IEnumerableBridger<string>, IdentityEnumerator<string>>, IdentityEnumerator<string>>,
                    GroupJoinDefaultEnumerator<T, string, string, IdentityEnumerator<string>, string, IdentityEnumerator<string>>
                >(toYield, null);
            yield return groupJoinDefault;

            var groupJoinSpecific =
                GetYielding<
                    T,
                    GroupJoinSpecificEnumerable<T, string, string, IdentityEnumerable<string, IEnumerable<string>, IEnumerableBridger<string>, IdentityEnumerator<string>>, IdentityEnumerator<string>, string, IdentityEnumerable<string, IEnumerable<string>, IEnumerableBridger<string>, IdentityEnumerator<string>>, IdentityEnumerator<string>>,
                    GroupJoinSpecificEnumerator<T, string, string, IdentityEnumerator<string>, string, IdentityEnumerator<string>>
                >(toYield, null);
            yield return groupJoinSpecific;

            var identity =
                GetYielding<
                    T,
                    IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>,
                    IdentityEnumerator<T>
                >(toYield, null);
            yield return identity;

            var intersectDefault =
                GetYielding<
                    T,
                    IntersectDefaultEnumerable<T, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>>,
                    IntersectDefaultEnumerator<T, IdentityEnumerator<T>, IdentityEnumerator<T>>
                >(toYield, null);
            yield return intersectDefault;

            var intersectSpecific =
                GetYielding<
                    T,
                    IntersectSpecificEnumerable<T, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>>,
                    IntersectSpecificEnumerator<T, IdentityEnumerator<T>, IdentityEnumerator<T>>
                >(toYield, null);
            yield return intersectSpecific;

            var joinDefault =
                GetYielding<
                    T,
                    JoinDefaultEnumerable<T, string, string, IdentityEnumerable<string, IEnumerable<string>, IEnumerableBridger<string>, IdentityEnumerator<string>>, IdentityEnumerator<string>, string, IdentityEnumerable<string, IEnumerable<string>, IEnumerableBridger<string>, IdentityEnumerator<string>>, IdentityEnumerator<string>>,
                    JoinDefaultEnumerator<T, string, string, IdentityEnumerator<string>, string, IdentityEnumerator<string>>
                >(toYield, null);
            yield return joinDefault;

            var joinSpecific =
                GetYielding<
                    T,
                    JoinSpecificEnumerable<T, string, string, IdentityEnumerable<string, IEnumerable<string>, IEnumerableBridger<string>, IdentityEnumerator<string>>, IdentityEnumerator<string>, string, IdentityEnumerable<string, IEnumerable<string>, IEnumerableBridger<string>, IdentityEnumerator<string>>, IdentityEnumerator<string>>,
                    JoinSpecificEnumerator<T, string, string, IdentityEnumerator<string>, string, IdentityEnumerator<string>>
                >(toYield, null);
            yield return joinSpecific;

            // skipping Lookup

            var ofType =
                GetYielding<
                    T,
                    OfTypeEnumerable<object, T, IdentityEnumerable<object, System.Collections.IEnumerable, IEnumerableBridger, IdentityEnumerator>, IdentityEnumerator>,
                    OfTypeEnumerator<object, T, IdentityEnumerator>
                >(toYield, null);
            yield return ofType;

            var orderBy =
                GetYielding<
                    T,
                    OrderByEnumerable<T, int, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>, DefaultAscending<T, int>>,
                    OrderByEnumerator<T, int, IdentityEnumerator<T>, DefaultAscending<T, int>>
                >(toYield, null);
            yield return orderBy;

            // skipping Range & Repeat

            var reverse =
                GetYielding<
                    T,
                    ReverseEnumerable<T, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>>,
                    ReverseEnumerator<T>
                >(toYield, null);
            yield return reverse;

            // skipping ReverseRange

            var select =
                GetYielding<
                    T,
                    SelectEnumerable<object, T, IdentityEnumerable<object, IEnumerable<object>, IEnumerableBridger<object>, IdentityEnumerator<object>>, IdentityEnumerator<object>>,
                    SelectEnumerator<object, T, IdentityEnumerator<object>>
                >(toYield, null);
            yield return select;

            var selectIndexed =
                GetYielding<
                    T,
                    SelectIndexedEnumerable<object, T, IdentityEnumerable<object, IEnumerable<object>, IEnumerableBridger<object>, IdentityEnumerator<object>>, IdentityEnumerator<object>>,
                    SelectIndexedEnumerator<object, T, IdentityEnumerator<object>>
                >(toYield, null);
            yield return selectIndexed;

            var selectManyBridged =
                GetYielding<
                    T,
                    SelectManyBridgeEnumerable<object, T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerable<object, IEnumerable<object>, IEnumerableBridger<object>, IdentityEnumerator<object>>, IdentityEnumerator<object>, IdentityEnumerator<T>>,
                    SelectManyBridgeEnumerator<object, T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<object>, IdentityEnumerator<T>>
                >(toYield, null);
            yield return selectManyBridged;

            var selectMany =
                GetYielding<
                    T,
                    SelectManyEnumerable<object, T, IdentityEnumerable<object, IEnumerable<object>, IEnumerableBridger<object>, IdentityEnumerator<object>>, IdentityEnumerator<object>, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>>,
                    SelectManyEnumerator<object, T, IdentityEnumerator<object>, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>>
                >(toYield, null);
            yield return selectMany;

            var selectManyCollection =
                GetYielding<
                    T,
                    SelectManyCollectionEnumerable<object, T, string, IdentityEnumerable<object, IEnumerable<object>, IEnumerableBridger<object>, IdentityEnumerator<object>>, IdentityEnumerator<object>, IdentityEnumerable<string, IEnumerable<string>, IEnumerableBridger<string>, IdentityEnumerator<string>>, IdentityEnumerator<string>>,
                    SelectManyCollectionEnumerator<object, T, string, IdentityEnumerator<object>, IdentityEnumerable<string, IEnumerable<string>, IEnumerableBridger<string>, IdentityEnumerator<string>>, IdentityEnumerator<string>>
                >(toYield, null);
            yield return selectManyCollection;

            var selectManyCollectionBridge =
                GetYielding<
                    T,
                    SelectManyCollectionBridgeEnumerable<object, T, string, IEnumerable<string>, IEnumerableBridger<string>, IdentityEnumerable<object, IEnumerable<object>, IEnumerableBridger<object>, IdentityEnumerator<object>>, IdentityEnumerator<object>, IdentityEnumerator<string>>,
                    SelectManyCollectionBridgeEnumerator<object, T, string, IEnumerable<string>, IEnumerableBridger<string>, IdentityEnumerator<object>, IdentityEnumerator<string>>
                >(toYield, null);
            yield return selectManyCollectionBridge;

            var selectManyCollectionIndexed =
                GetYielding<
                    T,
                    SelectManyCollectionIndexedEnumerable<object, T, string, IdentityEnumerable<object, IEnumerable<object>, IEnumerableBridger<object>, IdentityEnumerator<object>>, IdentityEnumerator<object>, IdentityEnumerable<string, IEnumerable<string>, IEnumerableBridger<string>, IdentityEnumerator<string>>, IdentityEnumerator<string>>,
                    SelectManyCollectionIndexedEnumerator<object, T, string, IdentityEnumerator<object>, IdentityEnumerable<string, IEnumerable<string>, IEnumerableBridger<string>, IdentityEnumerator<string>>, IdentityEnumerator<string>>
                >(toYield, null);
            yield return selectManyCollectionIndexed;

            var selectManyCollectionIndexedBridge =
                GetYielding<
                    T,
                    SelectManyCollectionIndexedBridgeEnumerable<object, T, string, IEnumerable<string>, IEnumerableBridger<string>, IdentityEnumerable<object, IEnumerable<object>, IEnumerableBridger<object>, IdentityEnumerator<object>>, IdentityEnumerator<object>, IdentityEnumerator<string>>,
                    SelectManyCollectionIndexedBridgeEnumerator<object, T, string, IEnumerable<string>, IEnumerableBridger<string>, IdentityEnumerator<object>, IdentityEnumerator<string>>
                >(toYield, null);
            yield return selectManyCollectionIndexedBridge;

            var selectManyIndexed =
                GetYielding<
                    T,
                    SelectManyIndexedEnumerable<object, T, RepeatEnumerable<object>, RepeatEnumerator<object>, DefaultIfEmptySpecificEnumerable<T, EmptyEnumerable<T>, EmptyEnumerator<T>>, DefaultIfEmptySpecificEnumerator<T, EmptyEnumerator<T>>>,
                    SelectManyIndexedEnumerator<object, T, RepeatEnumerator<object>, DefaultIfEmptySpecificEnumerable<T, EmptyEnumerable<T>, EmptyEnumerator<T>>, DefaultIfEmptySpecificEnumerator<T, EmptyEnumerator<T>>>
                >(toYield, null);
            yield return selectManyIndexed;

            var selectManyIndexedBridge =
                GetYielding<
                    T,
                    SelectManyIndexedBridgeEnumerable<object, T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerable<object, IEnumerable<object>, IEnumerableBridger<object>, IdentityEnumerator<object>>, IdentityEnumerator<object>, IdentityEnumerator<T>>,
                    SelectManyIndexedBridgeEnumerator<object, T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<object>, IdentityEnumerator<T>>
                >(toYield, null);
            yield return selectManyIndexedBridge;

            var selectSelect =
                GetYielding<
                    T,
                    SelectSelectEnumerable<T, object, IdentityEnumerable<object, IEnumerable<object>, IEnumerableBridger<object>, IdentityEnumerator<object>>, IdentityEnumerator<object>, SingleProjection<T, object>>,
                    SelectSelectEnumerator<T, object, IdentityEnumerator<object>, SingleProjection<T, object>>
                >(toYield, null);
            yield return selectSelect;

            var selectWhere =
                GetYielding<
                    T,
                    SelectWhereEnumerable<T, object, IdentityEnumerable<object, IEnumerable<object>, IEnumerableBridger<object>, IdentityEnumerator<object>>, IdentityEnumerator<object>, SingleProjection<T, object>, SinglePredicate<T>>,
                    SelectWhereEnumerator<T, object, IdentityEnumerator<object>, SingleProjection<T, object>, SinglePredicate<T>>
                >(toYield, null);
            yield return selectWhere;

            var skip =
                GetYielding<
                    T,
                    SkipEnumerable<T, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>>,
                    SkipEnumerator<T, IdentityEnumerator<T>>
                >(toYield, null);
            yield return skip;

            var skipWhile =
                GetYielding<
                    T,
                    SkipWhileEnumerable<T, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>>,
                    SkipWhileEnumerator<T, IdentityEnumerator<T>>
                >(toYield, null);
            yield return skipWhile;

            var skipWhileIndexed =
                GetYielding<
                    T,
                    SkipWhileIndexedEnumerable<T, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>>,
                    SkipWhileIndexedEnumerator<T, IdentityEnumerator<T>>
                >(toYield, null);
            yield return skipWhileIndexed;

            var take =
                GetYielding<
                    T,
                    TakeEnumerable<T, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>>,
                    TakeEnumerator<T, IdentityEnumerator<T>>
                >(toYield, null);
            yield return take;

            var takeWhile =
                GetYielding<
                    T,
                    TakeWhileEnumerable<T, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>>,
                    TakeWhileEnumerator<T, IdentityEnumerator<T>>
                >(toYield, null);
            yield return takeWhile;

            var takeWhileIndexed =
                GetYielding<
                    T,
                    TakeWhileIndexedEnumerable<T, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>>,
                    TakeWhileIndexedEnumerator<T, IdentityEnumerator<T>>
                >(toYield, null);
            yield return takeWhileIndexed;

            var unionDefault =
                GetYielding<
                    T,
                    UnionDefaultEnumerable<T, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>>,
                    UnionDefaultEnumerator<T, IdentityEnumerator<T>, IdentityEnumerator<T>>
                >(toYield, null);
            yield return unionDefault;

            var unionSpecific =
                GetYielding<
                    T,
                    UnionSpecificEnumerable<T, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>>,
                    UnionSpecificEnumerator<T, IdentityEnumerator<T>, IdentityEnumerator<T>>
                >(toYield, null);
            yield return unionSpecific;

            var where =
                GetYielding<
                    T,
                    WhereEnumerable<T, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>>,
                    WhereEnumerator<T, IdentityEnumerator<T>>
                >(toYield, null);
            yield return where;

            var whereIndexed =
                GetYielding<
                    T,
                    WhereIndexedEnumerable<T, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>>,
                    WhereIndexedEnumerator<T, IdentityEnumerator<T>>
                >(toYield, null);
            yield return whereIndexed;

            var whereSelect =
                GetYielding<
                    T,
                    WhereSelectEnumerable<T, object, IdentityEnumerable<object, IEnumerable<object>, IEnumerableBridger<object>, IdentityEnumerator<object>>, IdentityEnumerator<object>, SinglePredicate<object>, SingleProjection<T, object>>,
                    WhereSelectEnumerator<T, object, IdentityEnumerator<object>, SinglePredicate<object>, SingleProjection<T, object>>
                >(toYield, null);
            yield return whereSelect;

            var whereWhere =
                GetYielding<
                    T,
                    WhereWhereEnumerable<T, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>, SinglePredicate<T>>,
                    WhereWhereEnumerator<T, IdentityEnumerator<T>, SinglePredicate<T>>
                >(toYield, null);
            yield return whereWhere;

            var zip =
               GetYielding<
                   T,
                   ZipEnumerable<T, int, int, IdentityEnumerable<int, IEnumerable<int>, IEnumerableBridger<int>, IdentityEnumerator<int>>, IdentityEnumerator<int>, IdentityEnumerable<int, IEnumerable<int>, IEnumerableBridger<int>, IdentityEnumerator<int>>, IdentityEnumerator<int>>,
                   ZipEnumerator<T, int, int, IdentityEnumerator<int>, IdentityEnumerator<int>>
               >(toYield, null);
            yield return zip;
        }

        static TEnumerable GetYielding<T, TEnumerable, TEnumerator>(T[] toYield, object[] extra)
            where TEnumerable : struct, IStructEnumerable<T, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<T>
        {
            var gen = GetGenericTypeDefinition(typeof(TEnumerable));

            if (gen == typeof(BoxedEnumerable<>))
            {
                var inner = new IdentityEnumerable<T, T[], ArrayBridger<T>, ArrayEnumerator<T>>(toYield);

                return (TEnumerable)(object)(BoxedEnumerable<T>)inner;
            }

            if (gen == typeof(CastEnumerable<,,,>))
            {
                var ident = new IdentityEnumerable<object, System.Collections.IEnumerable, IEnumerableBridger, IdentityEnumerator>(toYield);

                return (TEnumerable)(object)new CastEnumerable<object, T, IdentityEnumerable<object, System.Collections.IEnumerable, IEnumerableBridger, IdentityEnumerator>, IdentityEnumerator>(ref ident);
            }

            if (gen == typeof(ConcatEnumerable<,,,,>))
            {
                return (TEnumerable)(object)IEnumerableExtensionMethods.Concat(new T[0], (IEnumerable<T>)toYield);
            }

            if (gen == typeof(DefaultIfEmptyDefaultEnumerable<,,>))
            {
                return (TEnumerable)(object)IEnumerableExtensionMethods.DefaultIfEmpty(toYield);
            }

            if (gen == typeof(DefaultIfEmptySpecificEnumerable<,,>))
            {
                return (TEnumerable)(object)IEnumerableExtensionMethods.DefaultIfEmpty(toYield, (T)extra[0]);
            }

            if (gen == typeof(DistinctDefaultEnumerable<,,>))
            {
                // DistinctDefaultEnumerable<T, IdentityEnumerable<T, IEnumerable<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>>

                var inner = new IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>(toYield);
                var distinct = inner.Distinct();

                return (TEnumerable)(object)distinct;
            }

            if (gen == typeof(DistinctSpecificEnumerable<,,>))
            {
                // DistinctSpecificEnumerable<T, IdentityEnumerable<T, IEnumerable<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>>

                var inner = new IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>(toYield);
                var distinct = inner.Distinct(EqualityComparer<T>.Default);

                return (TEnumerable)(object)distinct;
            }

            if (gen == typeof(EmptyEnumerable<>))
            {
                return (TEnumerable)(object)Enumerable.Empty<T>();
            }

            if (gen == typeof(EmptyOrderedEnumerator<>))
            {
                return (TEnumerable)(object)EmptyCache<T>.EmptyOrdered;
            }

            if (gen == typeof(ExceptDefaultEnumerable<,,,,>))
            {
                // ExceptDefaultEnumerable<T, IdentityEnumerable<T, IEnumerable<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>, IdentityEnumerable<T, IEnumerable<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>>
                var left = new IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>(toYield);
                var right = new IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>(new T[0]);
                var except = left.Except(right);
                return (TEnumerable)(object)except;
            }

            if (gen == typeof(ExceptSpecificEnumerable<,,,,>))
            {
                // ExceptSpecificEnumerable<T, IdentityEnumerable<T, IEnumerable<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>, IdentityEnumerable<T, IEnumerable<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>>
                var left = new IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>((IEnumerable<T>)toYield);
                var right = new IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>(new T[0]);
                var except = left.Except(right, EqualityComparer<T>.Default);
                return (TEnumerable)(object)except;
            }

            if (gen == typeof(GroupByCollectionDefaultEnumerable<,,,,,>))
            {
                // GroupByCollectionDefaultEnumerable<object, int, string, T, IdentityEnumerable<object, IEnumerable<object>, IdentityEnumerator<object>>, IdentityEnumerator<object>>
                var os = new object[toYield.Length * 2];
                for (var i = 0; i < toYield.Length; i++)
                {
                    var ix = i * 2;
                    var ij = ix + 1;

                    var key = new object();

                    os[ix] = os[ij] = key;
                }

                var left = new IdentityEnumerable<object, IEnumerable<object>, IEnumerableBridger<object>, IdentityEnumerator<object>>(os);
                var groupBy =
                    left.GroupBy(
                        x => Array.IndexOf(os, x),
                        o => o.ToString(),
                        (key, group) =>
                        {
                            if (group.Count() != 2) throw new Exception();

                            return toYield[key / 2];
                        }
                    );

                return (TEnumerable)(object)groupBy;
            }

            if (gen == typeof(GroupByCollectionSpecificEnumerable<,,,,,>))
            {
                // GroupByCollectionSpecificEnumerable<object, int, string, T, IdentityEnumerable<object, IEnumerable<object>, IdentityEnumerator<object>>, IdentityEnumerator<object>>
                var os = new object[toYield.Length * 2];
                for (var i = 0; i < toYield.Length; i++)
                {
                    var ix = i * 2;
                    var ij = ix + 1;

                    var key = new object();

                    os[ix] = os[ij] = key;
                }

                var left = new IdentityEnumerable<object, IEnumerable<object>, IEnumerableBridger<object>, IdentityEnumerator<object>>(os);
                var groupBy =
                    left.GroupBy(
                        x => Array.IndexOf(os, x),
                        o => o.ToString(),
                        (key, group) =>
                        {
                            if (group.Count() != 2) throw new Exception();

                            return toYield[key / 2];
                        },
                        EqualityComparer<int>.Default
                    );

                return (TEnumerable)(object)groupBy;
            }

            // skippping GroupBy*

            if (gen == typeof(GroupedEnumerable<,>))
            {
                // GroupedEnumerable<object, T>

                var ixes = Enumerable.Range(0, toYield.Length).ToArray();
                var counter = new IndexedItemContainer<T>();
                counter.Items = toYield;
                counter.UsedItems = toYield.Length;
                var grouping = new GroupingEnumerable<object, T>(new object(), (uint)toYield.Length, ixes, ref counter);

                var grouped = new GroupedEnumerable<object, T>(ref grouping);

                return (TEnumerable)(object)grouped;
            }

            if (gen == typeof(GroupingEnumerable<,>))
            {
                // GroupingEnumerable<object, T>

                var ixes = Enumerable.Range(0, toYield.Length).ToArray();
                var counter = new IndexedItemContainer<T>();
                counter.Items = toYield;
                counter.UsedItems = toYield.Length;
                var grouping = new GroupingEnumerable<object, T>(new object(), (uint)toYield.Length, ixes, ref counter);

                return (TEnumerable)(object)grouping;
            }

            if (gen == typeof(GroupJoinDefaultEnumerable<,,,,,,,>))
            {
                // GroupJoinDefaultEnumerable<T, string, string, IdentityEnumerable<string, IEnumerable<string>, IdentityEnumerator<string>>, IdentityEnumerator<string>, string, IdentityEnumerable<string, IEnumerable<string>, IdentityEnumerator<string>>, IdentityEnumerator<string>>
                var os = new string[toYield.Length];
                var @is = new string[toYield.Length * 2];

                for (var i = 0; i < toYield.Length; i++)
                {
                    os[i] = string.Join("", Enumerable.Repeat("a", i).AsEnumerable());
                    @is[i * 2] = i.ToString();
                    @is[i * 2 + 1] = i.ToString();
                }

                var outer = new IdentityEnumerable<string, IEnumerable<string>, IEnumerableBridger<string>, IdentityEnumerator<string>>(os);
                var inner = new IdentityEnumerable<string, IEnumerable<string>, IEnumerableBridger<string>, IdentityEnumerator<string>>(@is);

                var groupJoin =
                    outer.GroupJoin(
                        inner,
                        i => Array.IndexOf(os, i).ToString(),
                        i => int.Parse(i).ToString(),
                        (o, grp) =>
                        {
                            if (grp.Count() != 2) throw new Exception();

                            var keys = new HashSet<int>();
                            foreach (var x in grp)
                            {
                                keys.Add(int.Parse(x));
                            }

                            if (keys.Count != 1) throw new Exception();

                            return toYield[keys.Single()];
                        }
                    );

                return (TEnumerable)(object)groupJoin;
            }

            if (gen == typeof(GroupJoinSpecificEnumerable<,,,,,,,>))
            {
                // GroupJoinDefaultEnumerable<T, string, string, IdentityEnumerable<string, IEnumerable<string>, IdentityEnumerator<string>>, IdentityEnumerator<string>, string, IdentityEnumerable<string, IEnumerable<string>, IdentityEnumerator<string>>, IdentityEnumerator<string>>
                var os = new string[toYield.Length];
                var @is = new string[toYield.Length * 2];

                for (var i = 0; i < toYield.Length; i++)
                {
                    os[i] = string.Join("", Enumerable.Repeat("a", i).AsEnumerable());
                    @is[i * 2] = i.ToString();
                    @is[i * 2 + 1] = i.ToString();
                }

                var outer = new IdentityEnumerable<string, IEnumerable<string>, IEnumerableBridger<string>, IdentityEnumerator<string>>(os);
                var inner = new IdentityEnumerable<string, IEnumerable<string>, IEnumerableBridger<string>, IdentityEnumerator<string>>(@is);

                var groupJoin =
                    outer.GroupJoin(
                        inner,
                        i => Array.IndexOf(os, i).ToString(),
                        i => int.Parse(i).ToString(),
                        (o, grp) =>
                        {
                            if (grp.Count() != 2) throw new Exception();

                            var keys = new HashSet<int>();
                            foreach (var x in grp)
                            {
                                keys.Add(int.Parse(x));
                            }

                            if (keys.Count != 1) throw new Exception();

                            return toYield[keys.Single()];
                        },
                        StringComparer.InvariantCultureIgnoreCase
                    );

                return (TEnumerable)(object)groupJoin;
            }

            if (gen == typeof(IdentityEnumerable<,,,>))
            {
                var inner = (IEnumerable<T>)toYield;
                return (TEnumerable)(object)new IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>(inner);
            }

            if (gen == typeof(IntersectDefaultEnumerable<,,,,>))
            {
                // IntersectDefaultEnumerable<T, IdentityEnumerable<T, IEnumerable<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>, IdentityEnumerable<T, IEnumerable<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>>
                var doubled = new T[toYield.Length * 2];
                for (var i = 0; i < toYield.Length; i++)
                {
                    var j = i + toYield.Length;

                    doubled[i] = doubled[j] = toYield[i];
                }

                var left = new IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>(doubled);
                var right = new IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>(doubled);
                var intersect = left.Intersect(right);

                return (TEnumerable)(object)intersect;
            }

            if (gen == typeof(IntersectSpecificEnumerable<,,,,>))
            {
                // IntersectSpecificEnumerable<T, IdentityEnumerable<T, IEnumerable<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>, IdentityEnumerable<T, IEnumerable<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>>
                var doubled = new T[toYield.Length * 2];
                for (var i = 0; i < toYield.Length; i++)
                {
                    var j = i + toYield.Length;

                    doubled[i] = doubled[j] = toYield[i];
                }

                var left = new IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>(doubled);
                var right = new IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>(doubled);
                var intersect = left.Intersect(right, EqualityComparer<T>.Default);

                return (TEnumerable)(object)intersect;
            }

            if (gen == typeof(JoinDefaultEnumerable<,,,,,,,>))
            {
                // JoinDefaultEnumerable<T, string, string, IdentityEnumerable<string, IEnumerable<string>, IdentityEnumerator<string>>, IdentityEnumerator<string>, string, IdentityEnumerable<string, IEnumerable<string>, IdentityEnumerator<string>>, IdentityEnumerator<string>>
                var ls = new string[toYield.Length * 2];
                var rs = new string[toYield.Length * 2];

                for (var i = 0; i < toYield.Length; i++)
                {
                    ls[i] = i.ToString();
                    rs[i] = (i * 2).ToString();
                }

                for (var i = toYield.Length; i < ls.Length; i++)
                {
                    var val = 100 + i;
                    ls[i] = val.ToString();
                    rs[i] = (val * 20).ToString();
                }

                var left = new IdentityEnumerable<string, IEnumerable<string>, IEnumerableBridger<string>, IdentityEnumerator<string>>(ls);
                var right = new IdentityEnumerable<string, IEnumerable<string>, IEnumerableBridger<string>, IdentityEnumerator<string>>(rs);

                var join =
                    left.Join(
                        right,
                        o => o,
                        i => (int.Parse(i) / 2).ToString(),
                        (o, i) =>
                        {
                            var oix = int.Parse(o);
                            var iix = int.Parse(i) / 2;

                            if (oix != iix) throw new Exception();

                            return toYield[oix];
                        }
                    );

                return (TEnumerable)(object)join;
            }

            if (gen == typeof(JoinSpecificEnumerable<,,,,,,,>))
            {
                // JoinSpecificEnumerable<T, string, string, IdentityEnumerable<string, IEnumerable<string>, IdentityEnumerator<string>>, IdentityEnumerator<string>, string, IdentityEnumerable<string, IEnumerable<string>, IdentityEnumerator<string>>, IdentityEnumerator<string>>
                var ls = new string[toYield.Length * 2];
                var rs = new string[toYield.Length * 2];

                for (var i = 0; i < toYield.Length; i++)
                {
                    ls[i] = i.ToString();
                    rs[i] = (i * 2).ToString();
                }

                for (var i = toYield.Length; i < ls.Length; i++)
                {
                    var val = 100 + i;
                    ls[i] = val.ToString();
                    rs[i] = (val * 20).ToString();
                }

                var left = new IdentityEnumerable<string, IEnumerable<string>, IEnumerableBridger<string>, IdentityEnumerator<string>>(ls);
                var right = new IdentityEnumerable<string, IEnumerable<string>, IEnumerableBridger<string>, IdentityEnumerator<string>>(rs);

                var join =
                    left.Join(
                        right,
                        o => o,
                        i => (int.Parse(i) / 2).ToString(),
                        (o, i) =>
                        {
                            var oix = int.Parse(o);
                            var iix = int.Parse(i) / 2;

                            if (oix != iix) throw new Exception();

                            return toYield[oix];
                        },
                        StringComparer.OrdinalIgnoreCase
                    );

                return (TEnumerable)(object)join;
            }

            // skipping lookup

            if (gen == typeof(OfTypeEnumerable<,,,>))
            {
                var fin = new object[toYield.Length + 2];
                if (typeof(T) == typeof(string))
                {
                    fin[0] = 123.0;
                    fin[fin.Length - 1] = 456.0;
                }
                else
                {
                    fin[0] = "foo";
                    fin[fin.Length - 1] = "bar";
                }

                Array.Copy(toYield, 0, fin, 1, toYield.Length);

                return (TEnumerable)(object)OfTypeBridgeExtensionMethods.OfType<T>(fin);
            }

            if (gen == typeof(OrderByEnumerable<,,,,>))
            {
                // OrderByEnumerable<T, int, IdentityEnumerable<T, IEnumerable<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>, DefaultAscending<T, int>>

                var ixes = new int[toYield.Length];
                var ix = 0;
                for (var i = 0; i < ixes.Length; i++)
                {
                    ixes[i] = ix;
                    ix++;
                }

                var inner = new IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>(toYield);
                var orderBy =
                    inner.OrderBy(t => ixes[Array.IndexOf(toYield, t)]);

                return (TEnumerable)(object)orderBy;
            }

            if (gen == typeof(RangeEnumerable<>))
            {
                if ((toYield?.Length ?? 0) != 0) throw new InvalidOperationException("Range cannot yield a particular sequence");
                if ((extra?.Length ?? 0) != 2) throw new InvalidOperationException("Range requires a start and count value be placed in extra");

                var start = (int)extra[0];
                var count = (int)extra[1];

                return (TEnumerable)(object)Enumerable.Range(start, count);
            }

            if (gen == typeof(RepeatEnumerable<>))
            {
                if ((toYield?.Length ?? 0) != 1) throw new InvalidOperationException("Repeat can only yield a single value");
                if ((extra?.Length ?? 0) != 1) throw new InvalidOperationException("Repeat requires a count value be placed in extra");

                var value = toYield[0];
                var count = (int)extra[0];

                return (TEnumerable)(object)Enumerable.Repeat(value, count);
            }

            if (gen == typeof(ReverseEnumerable<,,>))
            {
                // ReverseEnumerable<T, IdentityEnumerable<T, IEnumerable<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>>
                var revToYield = new T[toYield.Length];
                Array.Copy(toYield, revToYield, toYield.Length);
                Array.Reverse(revToYield);

                var inner = new IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>(revToYield);
                var reverse = inner.Reverse();

                return (TEnumerable)(object)reverse;
            }

            if (gen == typeof(ReverseRangeEnumerable<>))
            {
                if ((toYield?.Length ?? 0) != 0) throw new InvalidOperationException("ReverseRange cannot yield a particular sequence");
                if ((extra?.Length ?? 0) != 2) throw new InvalidOperationException("ReverseRange requires a start and count value be placed in extra");

                var start = (int)extra[0];
                var count = (int)extra[1];

                return (TEnumerable)(object)new ReverseRangeEnumerable<int>(Enumerable.ReverseRangeSigil, start, count);
            }

            if (gen == typeof(SelectEnumerable<,,,>))
            {
                var vals = new object[toYield.Length];
                var lookup = new Dictionary<object, T>();
                for (var i = 0; i < vals.Length; i++)
                {
                    var key = new object();
                    vals[i] = key;
                    lookup[key] = toYield[i];
                }

                Func<object, T> selector = key => lookup[key];

                return (TEnumerable)(object)IEnumerableExtensionMethods.Select(vals, selector);
            }

            if (gen == typeof(SelectIndexedEnumerable<,,,>))
            {
                var vals = new object[toYield.Length];
                Func<object, int, T> selector = (_, ix) => toYield[ix];

                return (TEnumerable)(object)IEnumerableExtensionMethods.Select(vals, selector);
            }

            if (gen == typeof(SelectManyEnumerable<,,,,,>))
            {
                var keys = new object[toYield.Length];
                var lookup = new Dictionary<object, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>>();
                for (var i = 0; i < toYield.Length; i++)
                {
                    var key = new object();
                    var val = new[] { toYield[i] };
                    keys[i] = key;
                    lookup[key] = new IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>(val);
                }

                Func<object, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>> selector = key => lookup[key];

                var ident = new IdentityEnumerable<object, IEnumerable<object>, IEnumerableBridger<object>, IdentityEnumerator<object>>(keys);
                return (TEnumerable)(object)CommonImplementation.SelectMany<object, T, IdentityEnumerable<object, IEnumerable<object>, IEnumerableBridger<object>, IdentityEnumerator<object>>, IdentityEnumerator<object>, IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>>(ref ident, selector);
            }

            if (gen == typeof(SelectManyBridgeEnumerable<,,,,,,>))
            {
                var keys = new object[toYield.Length];
                var lookup = new Dictionary<object, T[]>();
                for (var i = 0; i < toYield.Length; i++)
                {
                    var key = new object();
                    var val = new[] { toYield[i] };
                    keys[i] = key;
                    lookup[key] = val;
                }

                Func<object, IEnumerable<T>> selector = key => lookup[key];

                return (TEnumerable)(object)IEnumerableExtensionMethods.SelectMany(keys, selector);
            }

            if (gen == typeof(SelectManyCollectionEnumerable<,,,,,,>))
            {
                var os = new object[toYield.Length];
                var ss = new IEnumerable<string>[toYield.Length];

                for (var i = 0; i < toYield.Length; i++)
                {
                    os[i] = new object();
                    ss[i] = new string[] { i.ToString() };
                }

                Func<object, IdentityEnumerable<string, IEnumerable<string>, IEnumerableBridger<string>, IdentityEnumerator<string>>> collectionSelector = o =>
                {
                    var a = Array.IndexOf(os, o);

                    var ret = ss[a];

                    return new IdentityEnumerable<string, IEnumerable<string>, IEnumerableBridger<string>, IdentityEnumerator<string>>(ret);
                };

                Func<object, string, T> resultSelector = (o, s) =>
                {
                    var b = int.Parse(s);

                    return toYield[b];
                };

                var osE = new IdentityEnumerable<object, IEnumerable<object>, IEnumerableBridger<object>, IdentityEnumerator<object>>((IEnumerable<object>)os);

                return (TEnumerable)(object)osE.SelectMany(collectionSelector, resultSelector);
            }

            if (gen == typeof(SelectManyCollectionBridgeEnumerable<,,,,,,,>))
            {
                var os = new object[toYield.Length];
                var ss = new IEnumerable<string>[toYield.Length];

                for (var i = 0; i < toYield.Length; i++)
                {
                    os[i] = new object();
                    ss[i] = new string[] { i.ToString() };
                }

                Func<object, IEnumerable<string>> collectionSelector = o => ss[Array.IndexOf(os, o)];
                Func<object, string, T> resultSelector = (o, s) =>
                {
                    var a = Array.IndexOf(os, o);
                    var b = int.Parse(s);

                    if (a != b) throw new Exception();

                    return toYield[b];
                };

                var x = IEnumerableExtensionMethods.SelectMany(os, collectionSelector, resultSelector);

                return (TEnumerable)(object)x;
            }

            if (gen == typeof(SelectManyCollectionIndexedEnumerable<,,,,,,>))
            {
                var os = new object[toYield.Length];
                var ss = new IEnumerable<string>[toYield.Length];

                for (var i = 0; i < toYield.Length; i++)
                {
                    os[i] = new object();
                    ss[i] = new string[] { i.ToString() };
                }

                Func<object, int, IdentityEnumerable<string, IEnumerable<string>, IEnumerableBridger<string>, IdentityEnumerator<string>>> collectionSelector = (o, ix) =>
                {
                    var a = Array.IndexOf(os, o);

                    if (a != ix) throw new Exception();

                    var ret = ss[a];

                    return new IdentityEnumerable<string, IEnumerable<string>, IEnumerableBridger<string>, IdentityEnumerator<string>>(ret);
                };

                Func<object, string, T> resultSelector = (o, s) =>
                {
                    var a = Array.IndexOf(os, o);
                    var b = int.Parse(s);

                    if (a != b) throw new Exception();

                    return toYield[b];
                };

                var osE = new IdentityEnumerable<object, IEnumerable<object>, IEnumerableBridger<object>, IdentityEnumerator<object>>((IEnumerable<object>)os);

                return (TEnumerable)(object)osE.SelectMany(collectionSelector, resultSelector);
            }

            if (gen == typeof(SelectManyCollectionIndexedBridgeEnumerable<,,,,,,,>))
            {
                var os = new object[toYield.Length];
                var ss = new IEnumerable<string>[toYield.Length];

                for (var i = 0; i < toYield.Length; i++)
                {
                    os[i] = new object();
                    ss[i] = new string[] { i.ToString() };
                }

                Func<object, int, IEnumerable<string>> collectionSelector = (o, ix) =>
                {
                    var a = Array.IndexOf(os, o);

                    if (a != ix) throw new Exception();

                    return ss[ix];
                };

                Func<object, string, T> resultSelector = (o, s) =>
                {
                    var a = Array.IndexOf(os, o);
                    var b = int.Parse(s);

                    if (a != b) throw new Exception();

                    return toYield[b];
                };

                return (TEnumerable)(object)IEnumerableExtensionMethods.SelectMany(os, collectionSelector, resultSelector);
            }

            if (gen == typeof(SelectManyIndexedEnumerable<,,,,,>))
            {
                var vals = new DefaultIfEmptySpecificEnumerable<T, EmptyEnumerable<T>, EmptyEnumerator<T>>[toYield.Length];
                for (var i = 0; i < toYield.Length; i++)
                {
                    var empty = Enumerable.Empty<T>();
                    var val = CommonImplementation.DefaultIfEmpty<T, EmptyEnumerable<T>, EmptyEnumerator<T>>(ref empty, toYield[i]);
                    vals[i] = val;
                }

                Func<object, int, DefaultIfEmptySpecificEnumerable<T, EmptyEnumerable<T>, EmptyEnumerator<T>>> selector = (_, ix) => vals[ix];

                var ixs = Enumerable.Repeat(new object(), toYield.Length);
                var ret = ixs.SelectMany(selector);

                return (TEnumerable)(object)ret;
            }

            if (gen == typeof(SelectManyIndexedBridgeEnumerable<,,,,,,>))
            {
                var keys = new object[toYield.Length];
                var vals = new IEnumerable<T>[toYield.Length];
                for (var i = 0; i < toYield.Length; i++)
                {
                    keys[i] = new object();
                    var val = new[] { toYield[i] };
                    vals[i] = val;
                }

                Func<object, int, IEnumerable<T>> selector = (_, ix) => vals[ix];

                return (TEnumerable)(object)IEnumerableExtensionMethods.SelectMany<object, T>(keys, selector);
            }

            if (gen == typeof(SelectSelectEnumerable<,,,,>))
            {
                // SelectSelectEnumerable<T, object, IdentityEnumerable<object, IEnumerable<object>, IdentityEnumerator<object>>, IdentityEnumerator<object>, SingleProjection<T, object>>
                var inner = new object[toYield.Length];
                for (var i = 0; i < toYield.Length; i++)
                {
                    inner[i] = new object();
                }

                var id = new IdentityEnumerable<object, IEnumerable<object>, IEnumerableBridger<object>, IdentityEnumerator<object>>(inner);
                var proj = new SingleProjection<T, object>(o => toYield[Array.IndexOf(inner, o)]);
                var selectSelect =
                    new SelectSelectEnumerable<
                        T,
                        object,
                        IdentityEnumerable<object, IEnumerable<object>, IEnumerableBridger<object>, IdentityEnumerator<object>>,
                        IdentityEnumerator<object>,
                        SingleProjection<T, object>
                    >(ref id, ref proj);

                return (TEnumerable)(object)selectSelect;
            }

            if (gen == typeof(SelectWhereEnumerable<,,,,,>))
            {
                // SelectWhereEnumerable<T, object, IdentityEnumerable<object, IEnumerable<object>, IdentityEnumerator<object>>, IdentityEnumerator<object>, SingleProjection<T, object>, SinglePredicate<T>>

                var inner = new object[toYield.Length];
                for (var i = 0; i < toYield.Length; i++)
                {
                    inner[i] = new object();
                }

                var id = new IdentityEnumerable<object, IEnumerable<object>, IEnumerableBridger<object>, IdentityEnumerator<object>>(inner);
                var proj = new SingleProjection<T, object>(o => toYield[Array.IndexOf(inner, o)]);
                var pred = new SinglePredicate<T>(f => true);
                var selectWhere =
                    new SelectWhereEnumerable<
                        T,
                        object,
                        IdentityEnumerable<object, IEnumerable<object>, IEnumerableBridger<object>, IdentityEnumerator<object>>,
                        IdentityEnumerator<object>,
                        SingleProjection<T, object>,
                        SinglePredicate<T>
                    >(ref id, ref proj, ref pred);

                return (TEnumerable)(object)selectWhere;
            }

            if (gen == typeof(SkipEnumerable<,,>))
            {
                var vals = new T[toYield.Length + 3];
                Array.Copy(toYield, 0, vals, 3, toYield.Length);

                return (TEnumerable)(object)IEnumerableExtensionMethods.Skip(vals, 3);
            }

            if (gen == typeof(SkipWhileEnumerable<,,>))
            {
                var vals = new T[toYield.Length + 3];
                Array.Copy(toYield, 0, vals, 3, toYield.Length);

                var defaultValue = default(T);

                Func<T, bool> isDefault = item => object.ReferenceEquals(item, defaultValue) || item.Equals(defaultValue);

                return (TEnumerable)(object)IEnumerableExtensionMethods.SkipWhile(vals, isDefault);
            }

            if (gen == typeof(SkipWhileIndexedEnumerable<,,>))
            {
                var vals = new T[toYield.Length + 3];
                Array.Copy(toYield, 0, vals, 3, toYield.Length);

                Func<T, int, bool> isDefault = (_, ix) => ix < 3;

                return (TEnumerable)(object)IEnumerableExtensionMethods.SkipWhile(vals, isDefault);
            }

            if (gen == typeof(TakeEnumerable<,,>))
            {
                var vals = new T[toYield.Length + 3];
                Array.Copy(toYield, 0, vals, 0, toYield.Length);

                return (TEnumerable)(object)IEnumerableExtensionMethods.Take(vals, toYield.Length);
            }

            if (gen == typeof(TakeWhileEnumerable<,,>))
            {
                var vals = new T[toYield.Length + 3];
                Array.Copy(toYield, 0, vals, 0, toYield.Length);

                var defaultValue = default(T);
                Func<T, bool> isNotDefault = item => !(object.ReferenceEquals(item, defaultValue) || item.Equals(defaultValue));

                return (TEnumerable)(object)IEnumerableExtensionMethods.TakeWhile(vals, isNotDefault);
            }

            if (gen == typeof(TakeWhileIndexedEnumerable<,,>))
            {
                var vals = new T[toYield.Length + 3];
                Array.Copy(toYield, 0, vals, 0, toYield.Length);

                Func<T, int, bool> isNotDefault = (_, ix) => ix < toYield.Length;

                return (TEnumerable)(object)IEnumerableExtensionMethods.TakeWhile(vals, isNotDefault);
            }

            if (gen == typeof(UnionDefaultEnumerable<,,,,>))
            {
                // UnionDefaultEnumerable<T, IdentityEnumerable<T, IEnumerable<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>, IdentityEnumerable<T, IEnumerable<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>>

                var firstHalf = toYield.Length / 2;
                var secondHalf = toYield.Length - firstHalf;

                T[] left, right;

                if (toYield.Length > 0)
                {
                    left = new T[firstHalf + 1];
                    right = new T[secondHalf + 1];

                    for (var i = 0; i < firstHalf; i++)
                    {
                        left[i] = toYield[i];
                    }
                    left[left.Length - 1] = toYield[0];

                    for (var i = 0; i < secondHalf; i++)
                    {
                        right[i] = toYield[firstHalf + i];
                    }
                    right[right.Length - 1] = right[right.Length - 2];
                }
                else
                {
                    left = right = new T[0];
                }

                var leftE = new IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>(left);
                var rightE = new IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>(right);

                var union = leftE.Union(rightE);

                return (TEnumerable)(object)union;
            }

            if (gen == typeof(UnionSpecificEnumerable<,,,,>))
            {
                // UnionSpecificEnumerable<T, IdentityEnumerable<T, IEnumerable<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>, IdentityEnumerable<T, IEnumerable<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>>

                var firstHalf = toYield.Length / 2;
                var secondHalf = toYield.Length - firstHalf;

                T[] left, right;

                if (toYield.Length > 0)
                {
                    left = new T[firstHalf + 1];
                    right = new T[secondHalf + 1];

                    for (var i = 0; i < firstHalf; i++)
                    {
                        left[i] = toYield[i];
                    }
                    left[left.Length - 1] = toYield[0];

                    for (var i = 0; i < secondHalf; i++)
                    {
                        right[i] = toYield[firstHalf + i];
                    }
                    right[right.Length - 1] = right[right.Length - 2];
                }
                else
                {
                    left = right = new T[0];
                }

                var leftE = new IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>(left);
                var rightE = new IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>(right);

                var union = leftE.Union(rightE, EqualityComparer<T>.Default);

                return (TEnumerable)(object)union;
            }

            if (gen == typeof(WhereEnumerable<,,>))
            {
                var all = new List<T>();

                for (var i = 0; i < 10; i++)
                {
                    all.Add(default(T));
                }

                all.AddRange(toYield);

                return (TEnumerable)(object)IEnumerableExtensionMethods.Where(all, i => toYield.Contains(i));
            }

            if (gen == typeof(WhereIndexedEnumerable<,,>))
            {
                var all = new List<T>();

                for (var i = 0; i < 10; i++)
                {
                    all.Add(default(T));
                }

                all.AddRange(toYield);

                for (var i = 0; i < 10; i++)
                {
                    all.Add(default(T));
                }

                return (TEnumerable)(object)IEnumerableExtensionMethods.Where(all, (_, ix) => ix >= 10 && ix < all.Count - 10);
            }

            if (gen == typeof(WhereSelectEnumerable<,,,,,>))
            {
                // WhereSelectEnumerable<T, object, IdentityEnumerable<object, IEnumerable<object>, IdentityEnumerator<object>>, IdentityEnumerator<object>, SinglePredicate<object>, SingleProjection<T, object>>

                var os = new object[toYield.Length * 2];
                for (var i = 0; i < os.Length; i += 2)
                {
                    os[i] = new object();
                    os[i + 1] = new object();
                }

                var inner = new IdentityEnumerable<object, IEnumerable<object>, IEnumerableBridger<object>, IdentityEnumerator<object>>(os);
                var pred = new SinglePredicate<object>(o => Array.IndexOf(os, o) % 2 == 0);
                var proj = new SingleProjection<T, object>(o => toYield[Array.IndexOf(os, o) / 2]);
                var whereSelect =
                    new WhereSelectEnumerable<
                        T,
                        object,
                        IdentityEnumerable<object, IEnumerable<object>, IEnumerableBridger<object>, IdentityEnumerator<object>>,
                        IdentityEnumerator<object>,
                        SinglePredicate<object>,
                        SingleProjection<T, object>
                    >(ref inner, ref pred, ref proj);

                return (TEnumerable)(object)whereSelect;
            }

            if (gen == typeof(WhereWhereEnumerable<,,,>))
            {
                // WhereWhereEnumerable<T, IdentityEnumerable<T, IEnumerable<T>, IdentityEnumerator<T>>, IdentityEnumerator<T>, SinglePredicate<T>>

                var inner = new T[toYield.Length * 2];
                for (var i = 0; i < toYield.Length; i++)
                {
                    var ix = i * 2;
                    inner[ix] = toYield[i];
                }

                var ident = new IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>(inner);
                var pred = new SinglePredicate<T>(i => Array.IndexOf(inner, i) % 2 == 0);
                var whereWhere =
                    new WhereWhereEnumerable<
                        T,
                        IdentityEnumerable<T, IEnumerable<T>, IEnumerableBridger<T>, IdentityEnumerator<T>>,
                        IdentityEnumerator<T>,
                        SinglePredicate<T>
                    >(ref ident, ref pred);

                return (TEnumerable)(object)whereWhere;
            }

            if (gen == typeof(ZipEnumerable<,,,,,,>))
            {
                var first = new List<int>();
                var second = new List<int>();

                for (var i = 0; i < toYield.Length; i++)
                {
                    if (i % 2 == 0)
                    {
                        first.Add(i);
                        second.Add(-1);
                    }
                    else
                    {
                        first.Add(-1);
                        second.Add(i);
                    }
                }

                Func<int, int, T> selector =
                    (a, b) =>
                    {
                        if (a != -1) return toYield[a];

                        return toYield[b];
                    };

                return (TEnumerable)(object)IEnumerableExtensionMethods.Zip(first, (IEnumerable<int>)second, selector);
            }

            throw new NotImplementedException();
        }

        public static void Throws<TException>(Action act)
            where TException : Exception
        {
            try
            {
                act();
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.Fail($"Expected {typeof(TException).Name} to be thrown");
            }
            catch (TException)
            {

            }
        }

        public static void IsNaN(float f)
        {
            if (!float.IsNaN(f))
            {
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.Fail($"Expected NaN, found {f}");
            }
        }

        public static void IsNaN(float? f)
        {
            if (f == null || !float.IsNaN(f.Value))
            {
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.Fail($"Expected NaN, found {f}");
            }
        }

        public static void IsNaN(double d)
        {
            if (!double.IsNaN(d))
            {
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.Fail($"Expected NaN, found {d}");
            }
        }

        public static void IsNaN(double? d)
        {
            if (d == null || !double.IsNaN(d.Value))
            {
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.Fail($"Expected NaN, found {d}");
            }
        }
    }
}