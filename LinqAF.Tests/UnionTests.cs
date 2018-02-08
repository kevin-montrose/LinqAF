using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using TestHelpers;
using System.Reflection;
using System.Text;

namespace LinqAF.Tests
{
    [TestClass]
    public class UnionTests
    {
        static void _InstanceExtensionNoOverlapImpl(int spread, int take)
        {
            Dictionary<MethodInfo, List<MethodInfo>> instOverlaps, extOverlaps;
            Helper.GetOverlappingMethods(typeof(Impl.IUnion<,,>), out instOverlaps, out extOverlaps, spread, take);

            if (instOverlaps.Count > 0)
            {
                var failure = new StringBuilder();
                foreach (var kv in instOverlaps)
                {
                    failure.AppendLine("For " + kv.Key);
                    failure.AppendLine(
                        LinqAFString.Join("\t -", kv.Value.Select(x => x.ToString() + "\n"))
                    );

                    Assert.Fail(failure.ToString());
                }
            }

            if (extOverlaps.Count > 0)
            {
                var failure = new StringBuilder();
                foreach (var kv in extOverlaps)
                {
                    failure.AppendLine("For " + kv.Key);
                    failure.AppendLine(
                        LinqAFString.Join("\t -", kv.Value.Select(x => x.ToString() + "\n"))
                    );

                    Assert.Fail(failure.ToString());
                }
            }
        }

        [TestMethod]
        public void InstanceExtensionNoOverlap1()
        => _InstanceExtensionNoOverlapImpl(5, 0);

        [TestMethod]
        public void InstanceExtensionNoOverlap2()
        => _InstanceExtensionNoOverlapImpl(5, 1);

        [TestMethod]
        public void InstanceExtensionNoOverlap3()
        => _InstanceExtensionNoOverlapImpl(5, 2);

        [TestMethod]
        public void InstanceExtensionNoOverlap4()
        => _InstanceExtensionNoOverlapImpl(5, 3);

        [TestMethod]
        public void InstanceExtensionNoOverlap5()
        => _InstanceExtensionNoOverlapImpl(5, 4);

        [TestMethod]
        public void Universal()
        {
            var enums = Helper.AllEnumerables();

            foreach (var e in enums)
            {
                System.Collections.Generic.List<string> missing;
                if (!Helper.Implements(e, typeof(LinqAF.Impl.IUnion<,,>), out missing))
                {
                    Assert.Fail($"{e.Name} does not implement IUnion ({string.Join(", ", missing)})");
                }
            }
        }

        [TestMethod]
        public void AcceptsAllEnumerables()
        {
            var missingSimple = new List<string>();
            var missingComparer = new List<string>();

            var iunion = typeof(Impl.IUnion<,,>);
            var enums = Helper.AllEnumerables(includeWeirdOnes: false);
            foreach (var e in enums)
            {
                var simple =
                    iunion
                        .GetMethods()
                        .Where(
                            m =>
                            {
                                var ps = Helper.GetParameters(m);
                                if (ps.Length != 1) return false;

                                var p = ps[0].ParameterType;

                                if (p.IsGenericType && !p.IsGenericTypeDefinition)
                                {
                                    p = Helper.GetGenericTypeDefinition(p);
                                }
                                return p == e;
                            }
                        )
                        .SingleOrDefault();
                if (simple == null) missingSimple.Add(e.Name);

                var comparer =
                    iunion
                        .GetMethods()
                        .Where(
                            m =>
                            {
                                var ps = Helper.GetParameters(m);
                                if (ps.Length != 2) return false;

                                var p = ps[0].ParameterType;

                                if (p.IsGenericType && !p.IsGenericTypeDefinition)
                                {
                                    p = Helper.GetGenericTypeDefinition(p);
                                }
                                return p == e;
                            }
                        )
                        .SingleOrDefault();
                if (comparer == null) missingComparer.Add(e.Name);
            }

            if (missingSimple.Any())
            {
                Assert.Fail("Missing simple for: \r\n" + string.Join("\r\n", missingSimple));
            }

            if (missingComparer.Any())
            {
                Assert.Fail("Missing comparer for: \r\n" + string.Join("\r\n", missingComparer));
            }
        }

        [TestMethod]
        public void Simple()
        {
            var left = Enumerable.Range(1, 3);
            var right = new[] { 3, 4, 5 };
            var asUnion = left.Union(right);

            Assert.IsTrue(asUnion.GetType().IsValueType);

            var res = new List<int>();
            foreach (var item in asUnion)
            {
                res.Add(item);
            }

            Assert.AreEqual(5, res.Count);
            Assert.AreEqual(1, res[0]);
            Assert.AreEqual(2, res[1]);
            Assert.AreEqual(3, res[2]);
            Assert.AreEqual(4, res[3]);
            Assert.AreEqual(5, res[4]);
        }

        public class _Comparer : IEqualityComparer<string>
        {
            public bool Equals(string x, string y)
            {
                return
                    (x?.Length ?? 0) == (y?.Length ?? 0);
            }

            public int GetHashCode(string obj)
            {
                return (obj?.Length ?? 0).GetHashCode();
            }
        }

        [TestMethod]
        public void Comparer()
        {
            var left = new[] { "one", "two", "three" }.Select(i => i);
            var right = Enumerable.Repeat("six", 3);
            var asUnion = left.Union(right, new _Comparer());

            Assert.IsTrue(asUnion.GetType().IsValueType);

            var res = new List<string>();
            foreach (var item in asUnion)
            {
                res.Add(item);
            }

            Assert.AreEqual(2, res.Count);
            Assert.AreEqual("one", res[0]);
            Assert.AreEqual("three", res[1]);
        }

        [TestMethod]
        public void Chaining()
        {
            // default
            {
                Helper.ForEachEnumerableNoRetExpression(
                    new[] { 1, 1, 3, 5 },
                    @"a =>
                        Helper.ForEachEnumerableExpression(
                            a,
                            new [] { 3, 6 },
                            res =>
                            {
                                Assert.AreEqual(4, res.Count);
                                Assert.AreEqual(1, res[0]);
                                Assert.AreEqual(3, res[1]);
                                Assert.AreEqual(5, res[2]);
                                Assert.AreEqual(6, res[3]);
                            },
                            ""(a, b) => a.Union(b)"",
                            typeof(Dictionary<,>.KeyCollection),
                            typeof(DistinctDefaultEnumerable<,,>),
                            typeof(DistinctSpecificEnumerable<,,>),
                            typeof(EmptyEnumerable<>),
                            typeof(EmptyOrderedEnumerable<>),
                            typeof(GroupByDefaultEnumerable<,,,,>),
                            typeof(GroupBySpecificEnumerable<,,,,>),
                            typeof(HashSet<>),
                            typeof(LookupDefaultEnumerable<,>),
                            typeof(LookupSpecificEnumerable<,>),
                            typeof(SortedDictionary<,>.KeyCollection),
                            typeof(SortedSet<>),
                            typeof(UnionDefaultEnumerable<,,,,>),
                            typeof(UnionSpecificEnumerable<,,,,>)
                        )",
                    typeof(Dictionary<,>.KeyCollection),
                    typeof(DistinctDefaultEnumerable<,,>),
                    typeof(DistinctSpecificEnumerable<,,>),
                    typeof(EmptyEnumerable<>),
                    typeof(EmptyOrderedEnumerable<>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(HashSet<>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>),
                    typeof(SortedDictionary<,>.KeyCollection),
                    typeof(SortedSet<>),
                    typeof(UnionDefaultEnumerable<,,,,>),
                    typeof(UnionSpecificEnumerable<,,,,>)
                );
            }

            // specific
            {
                Helper.ForEachEnumerableNoRetExpression(
                    new[] { "foo", "bar", "hello", "fizz" },
                    @"a =>
                        Helper.ForEachEnumerableExpression(
                            a,
                            new [] { ""world"", ""buzz"", ""a"" },
                            res =>
                            {
                                Assert.AreEqual(4, res.Count);
                                Assert.AreEqual(""foo"", res[0]);
                                Assert.AreEqual(""hello"", res[1]);
                                Assert.AreEqual(""fizz"", res[2]);
                                Assert.AreEqual(""a"", res[3]);
                            },
                            ""(a, b) => a.Union(b, new UnionTests._Comparer())"",
                            typeof(Dictionary<,>.KeyCollection),
                            typeof(DistinctDefaultEnumerable<,,>),
                            typeof(DistinctSpecificEnumerable<,,>),
                            typeof(EmptyEnumerable<>),
                            typeof(EmptyOrderedEnumerable<>),
                            typeof(GroupByDefaultEnumerable<,,,,>),
                            typeof(GroupBySpecificEnumerable<,,,,>),
                            typeof(HashSet<>),
                            typeof(LookupDefaultEnumerable<,>),
                            typeof(LookupSpecificEnumerable<,>),
                            typeof(SortedDictionary<,>.KeyCollection),
                            typeof(SortedSet<>),
                            typeof(UnionDefaultEnumerable<,,,,>),
                            typeof(UnionSpecificEnumerable<,,,,>)
                        )",
                    typeof(Dictionary<,>.KeyCollection),
                    typeof(DistinctDefaultEnumerable<,,>),
                    typeof(DistinctSpecificEnumerable<,,>),
                    typeof(EmptyEnumerable<>),
                    typeof(EmptyOrderedEnumerable<>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(HashSet<>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>),
                    typeof(SortedDictionary<,>.KeyCollection),
                    typeof(SortedSet<>),
                    typeof(UnionDefaultEnumerable<,,,,>),
                    typeof(UnionSpecificEnumerable<,,,,>)
                );
            }
        }

        public class _IntComparer : IEqualityComparer<int>
        {
            public bool Equals(int x, int y) => x == y;

            public int GetHashCode(int obj) => obj;
        }

        public class _GroupingComparer<T> : IEqualityComparer<GroupingEnumerable<T, T>>
        {
            public bool Equals(GroupingEnumerable<T, T> x, GroupingEnumerable<T, T> y)
            {
                if (!x.Key.Equals(y.Key)) return false;

                var xList = x.ToList();
                var yList = y.ToList();

                if (xList.Count != yList.Count) return false;

                for (var i = 0; i < xList.Count; i++)
                {
                    if (!xList[i].Equals(yList[i])) return false;
                }

                return true;
            }

            public int GetHashCode(GroupingEnumerable<T, T> obj)
            {
                var ret = obj.Key.GetHashCode();
                foreach (var item in obj)
                {
                    ret *= 17;
                    ret += item.GetHashCode();
                }

                return ret;
            }
        }

        [TestMethod]
        public void Chaining_Weird()
        {
            var empty = Enumerable.Empty<int>();
            var emptyOrdered = empty.OrderBy(x => x);
            var groupByDefault = new[] { 1, 1, 2, 2, 3, 3 }.GroupBy(x => x);
            var groupBySpecific = new[] { "hello", "HELLO", "world", "WORLD", "foo", "FOO" }.GroupBy(x => x, StringComparer.OrdinalIgnoreCase);
            var lookup = new int[] { 1, 1, 2, 2, 3, 3 }.ToLookup(x => x);
            var range = Enumerable.Range(1, 5);
            var repeat = Enumerable.Repeat("foo", 5);
            var reverseRange = Enumerable.Range(1, 5).Reverse();
            var oneItemDefault = Enumerable.Empty<int>().DefaultIfEmpty();
            var oneItemSpecific = Enumerable.Empty<int>().DefaultIfEmpty(4);
            var oneItemDefaultOrdered = oneItemDefault.OrderBy(x => x);
            var oneItemSpecificOrdered = oneItemSpecific.OrderBy(x => x);

            // empty
            {
                Assert.IsFalse(empty.Union(empty).Any());
                Assert.IsFalse(empty.Union(empty, new _IntComparer()).Any());
                Assert.IsFalse(empty.Union(emptyOrdered).Any());
                Assert.IsFalse(empty.Union(emptyOrdered, new _IntComparer()).Any());
                var emptyGroupingInt = Enumerable.Empty<GroupingEnumerable<int, int>>();
                Assert.IsTrue(emptyGroupingInt.Union(groupByDefault).SequenceEqual(groupByDefault, new _GroupingComparer<int>()));
                Assert.IsTrue(emptyGroupingInt.Union(groupByDefault, new _GroupingComparer<int>()).SequenceEqual(groupByDefault, new _GroupingComparer<int>()));
                var emptyGroupingString = Enumerable.Empty<GroupingEnumerable<string, string>>();
                Assert.IsTrue(emptyGroupingString.Union(groupBySpecific).SequenceEqual(groupBySpecific, new _GroupingComparer<string>()));
                Assert.IsTrue(emptyGroupingString.Union(groupBySpecific, new _GroupingComparer<string>()).SequenceEqual(groupBySpecific, new _GroupingComparer<string>()));
                Assert.IsTrue(emptyGroupingInt.Union(lookup).SequenceEqual(lookup, new _GroupingComparer<int>()));
                Assert.IsTrue(emptyGroupingInt.Union(lookup, new _GroupingComparer<int>()).SequenceEqual(lookup, new _GroupingComparer<int>()));
                Assert.IsTrue(empty.Union(range).SequenceEqual(range));
                Assert.IsTrue(empty.Union(range, new _IntComparer()).SequenceEqual(range));
                var emptyString = Enumerable.Empty<string>();
                Assert.IsTrue(emptyString.Union(repeat).SequenceEqual(new[] { repeat.First() }));
                Assert.IsTrue(emptyString.Union(repeat, StringComparer.InvariantCultureIgnoreCase).SequenceEqual(new[] { repeat.First() }));
                Assert.IsTrue(empty.Union(reverseRange).SequenceEqual(reverseRange));
                Assert.IsTrue(empty.Union(reverseRange, new _IntComparer()).SequenceEqual(reverseRange));

                var defaultIfEmptyDefault = new[] { 1 }.DefaultIfEmpty();
                Assert.IsTrue(empty.Union(defaultIfEmptyDefault).SequenceEqual(defaultIfEmptyDefault));
                Assert.IsTrue(empty.Union(defaultIfEmptyDefault, new _IntComparer()).SequenceEqual(defaultIfEmptyDefault));

                var defaultIfEmptySpecific = new[] { 1 }.DefaultIfEmpty(4);
                Assert.IsTrue(empty.Union(defaultIfEmptySpecific).SequenceEqual(defaultIfEmptySpecific));
                Assert.IsTrue(empty.Union(defaultIfEmptySpecific, new _IntComparer()).SequenceEqual(defaultIfEmptySpecific));

                Assert.IsTrue(empty.Union(oneItemDefault).SequenceEqual(oneItemDefault));
                Assert.IsTrue(empty.Union(oneItemDefault, new _IntComparer()).SequenceEqual(oneItemDefault));
                Assert.IsTrue(empty.Union(oneItemSpecific).SequenceEqual(oneItemSpecific));
                Assert.IsTrue(empty.Union(oneItemSpecific, new _IntComparer()).SequenceEqual(oneItemSpecific));
                Assert.IsTrue(empty.Union(oneItemDefaultOrdered).SequenceEqual(oneItemDefaultOrdered));
                Assert.IsTrue(empty.Union(oneItemDefaultOrdered, new _IntComparer()).SequenceEqual(oneItemDefaultOrdered));
                Assert.IsTrue(empty.Union(oneItemSpecificOrdered).SequenceEqual(oneItemSpecificOrdered));
                Assert.IsTrue(empty.Union(oneItemSpecificOrdered, new _IntComparer()).SequenceEqual(oneItemSpecificOrdered));

                Helper.ForEachEnumerableExpression(
                    empty,
                    new int[0],
                    res => { },
                    @"(a, b) =>
                      {
                        Assert.IsFalse(a.Union(b).Any());
                        Assert.IsFalse(a.Union(b, new UnionTests._IntComparer()).Any());

                        return Helper.NoCallValue;
                      }",
                    typeof(DefaultIfEmptyDefaultEnumerable<,,>),
                    typeof(DefaultIfEmptySpecificEnumerable<,,>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );
            }

            // emptyOrdered
            {
                Assert.IsFalse(emptyOrdered.Union(empty).Any());
                Assert.IsFalse(emptyOrdered.Union(empty, new _IntComparer()).Any());
                Assert.IsFalse(emptyOrdered.Union(emptyOrdered).Any());
                Assert.IsFalse(emptyOrdered.Union(emptyOrdered, new _IntComparer()).Any());
                var emptyGroupingIntOrdered = Enumerable.Empty<GroupingEnumerable<int, int>>().OrderBy(x => x);
                Assert.IsTrue(emptyGroupingIntOrdered.Union(groupByDefault).SequenceEqual(groupByDefault, new _GroupingComparer<int>()));
                Assert.IsTrue(emptyGroupingIntOrdered.Union(groupByDefault, new _GroupingComparer<int>()).SequenceEqual(groupByDefault, new _GroupingComparer<int>()));
                var emptyGroupingStringOrdered = Enumerable.Empty<GroupingEnumerable<string, string>>().OrderBy(x => x);
                Assert.IsTrue(emptyGroupingStringOrdered.Union(groupBySpecific).SequenceEqual(groupBySpecific, new _GroupingComparer<string>()));
                Assert.IsTrue(emptyGroupingStringOrdered.Union(groupBySpecific, new _GroupingComparer<string>()).SequenceEqual(groupBySpecific, new _GroupingComparer<string>()));
                Assert.IsTrue(emptyGroupingIntOrdered.Union(lookup).SequenceEqual(lookup, new _GroupingComparer<int>()));
                Assert.IsTrue(emptyGroupingIntOrdered.Union(lookup, new _GroupingComparer<int>()).SequenceEqual(lookup, new _GroupingComparer<int>()));
                Assert.IsTrue(emptyOrdered.Union(range).SequenceEqual(range));
                Assert.IsTrue(emptyOrdered.Union(range, new _IntComparer()).SequenceEqual(range));
                var emptyStringOrdered = Enumerable.Empty<string>().OrderBy(x => x);
                Assert.IsTrue(emptyStringOrdered.Union(repeat).SequenceEqual(new[] { repeat.First() }));
                Assert.IsTrue(emptyStringOrdered.Union(repeat, StringComparer.InvariantCultureIgnoreCase).SequenceEqual(new[] { repeat.First() }));
                Assert.IsTrue(emptyOrdered.Union(reverseRange).SequenceEqual(reverseRange));
                Assert.IsTrue(emptyOrdered.Union(reverseRange, new _IntComparer()).SequenceEqual(reverseRange));

                var defaultIfEmptyDefault = new[] { 1 }.DefaultIfEmpty();
                Assert.IsTrue(emptyOrdered.Union(defaultIfEmptyDefault).SequenceEqual(defaultIfEmptyDefault));
                Assert.IsTrue(emptyOrdered.Union(defaultIfEmptyDefault, new _IntComparer()).SequenceEqual(defaultIfEmptyDefault));

                var defaultIfEmptySpecific = new[] { 1 }.DefaultIfEmpty(4);
                Assert.IsTrue(emptyOrdered.Union(defaultIfEmptySpecific).SequenceEqual(defaultIfEmptySpecific));
                Assert.IsTrue(emptyOrdered.Union(defaultIfEmptySpecific, new _IntComparer()).SequenceEqual(defaultIfEmptySpecific));

                Assert.IsTrue(emptyOrdered.Union(oneItemDefault).SequenceEqual(oneItemDefault));
                Assert.IsTrue(emptyOrdered.Union(oneItemDefault, new _IntComparer()).SequenceEqual(oneItemDefault));
                Assert.IsTrue(emptyOrdered.Union(oneItemSpecific).SequenceEqual(oneItemSpecific));
                Assert.IsTrue(emptyOrdered.Union(oneItemSpecific, new _IntComparer()).SequenceEqual(oneItemSpecific));
                Assert.IsTrue(emptyOrdered.Union(oneItemDefaultOrdered).SequenceEqual(oneItemDefaultOrdered));
                Assert.IsTrue(emptyOrdered.Union(oneItemDefaultOrdered, new _IntComparer()).SequenceEqual(oneItemDefaultOrdered));
                Assert.IsTrue(emptyOrdered.Union(oneItemSpecificOrdered).SequenceEqual(oneItemSpecificOrdered));
                Assert.IsTrue(emptyOrdered.Union(oneItemSpecificOrdered, new _IntComparer()).SequenceEqual(oneItemSpecificOrdered));

                Helper.ForEachEnumerableExpression(
                    emptyOrdered,
                    new int[0],
                    res => { },
                    @"(a, b) =>
                      {
                        Assert.IsFalse(a.Union(b).Any());
                        Assert.IsFalse(a.Union(b, new UnionTests._IntComparer()).Any());

                        return Helper.NoCallValue;
                      }",
                    typeof(DefaultIfEmptyDefaultEnumerable<,,>),
                    typeof(DefaultIfEmptySpecificEnumerable<,,>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );
            }

            // groupByDefault
            {
                var emptyGroupingInt = Enumerable.Empty<GroupingEnumerable<int, int>>();
                Assert.IsTrue(groupByDefault.Union(emptyGroupingInt).SequenceEqual(groupByDefault, new _GroupingComparer<int>()));
                Assert.IsTrue(groupByDefault.Union(emptyGroupingInt, new _GroupingComparer<int>()).SequenceEqual(groupByDefault, new _GroupingComparer<int>()));
                var emptyGroupingIntOrdered = Enumerable.Empty<GroupingEnumerable<int, int>>().OrderBy(x => x);
                Assert.IsTrue(groupByDefault.Union(emptyGroupingIntOrdered).SequenceEqual(groupByDefault, new _GroupingComparer<int>()));
                Assert.IsTrue(groupByDefault.Union(emptyGroupingIntOrdered, new _GroupingComparer<int>()).SequenceEqual(groupByDefault, new _GroupingComparer<int>()));
                Assert.IsTrue(groupByDefault.Union(groupByDefault).SequenceEqual(groupByDefault.Concat(groupByDefault), new _GroupingComparer<int>()));
                Assert.IsTrue(groupByDefault.Union(groupByDefault, new _GroupingComparer<int>()).SequenceEqual(groupByDefault, new _GroupingComparer<int>()));
                var groupByIntSpecific = new[] { 1, 1, 2, 2, 3, 3 }.GroupBy(x => x, new _IntComparer());
                Assert.IsTrue(groupByDefault.Union(groupByIntSpecific).SequenceEqual(groupByDefault.Concat(groupByIntSpecific), new _GroupingComparer<int>()));
                Assert.IsTrue(groupByDefault.Union(groupByIntSpecific, new _GroupingComparer<int>()).SequenceEqual(groupByDefault, new _GroupingComparer<int>()));
                Assert.IsTrue(groupByDefault.Union(lookup).SequenceEqual(groupByDefault.Concat(lookup), new _GroupingComparer<int>()));
                Assert.IsTrue(groupByDefault.Union(lookup, new _GroupingComparer<int>()).SequenceEqual(groupByDefault, new _GroupingComparer<int>()));
                // range is non-sensical
                var repeatGrouping = Enumerable.Repeat(groupByDefault.First(), 1);
                Assert.IsTrue(groupByDefault.Union(repeatGrouping).SequenceEqual(groupByDefault.Concat(repeatGrouping), new _GroupingComparer<int>()));
                Assert.IsTrue(groupByDefault.Union(repeatGrouping, new _GroupingComparer<int>()).SequenceEqual(groupByDefault, new _GroupingComparer<int>()));
                // reverseRange is non-sensical

                var skipWhile = groupByDefault.SkipWhile(x => false);
                Assert.IsTrue(groupByDefault.Union(skipWhile).SequenceEqual(groupByDefault.Concat(groupByDefault), new _GroupingComparer<int>()));
                Assert.IsTrue(groupByDefault.Union(skipWhile, new _GroupingComparer<int>()).SequenceEqual(groupByDefault, new _GroupingComparer<int>()));
                var takeWhile = groupByDefault.TakeWhile(x => true);
                Assert.IsTrue(groupByDefault.Union(takeWhile).SequenceEqual(groupByDefault.Concat(groupByDefault), new _GroupingComparer<int>()));
                Assert.IsTrue(groupByDefault.Union(takeWhile, new _GroupingComparer<int>()).SequenceEqual(groupByDefault, new _GroupingComparer<int>()));
                var whereWhere = groupByDefault.Where(x => true).Where(x => true);
                Assert.IsTrue(groupByDefault.Union(whereWhere).SequenceEqual(groupByDefault.Concat(groupByDefault), new _GroupingComparer<int>()));
                Assert.IsTrue(groupByDefault.Union(whereWhere, new _GroupingComparer<int>()).SequenceEqual(groupByDefault, new _GroupingComparer<int>()));

                var orderBy = new[] { groupByDefault.First() }.OrderBy(x => x);
                var unionAB1 = groupByDefault.Union(orderBy).ToList();
                Assert.AreEqual(4, unionAB1.Count);
                var unionAB2 = groupByDefault.Union(orderBy, new _GroupingComparer<int>()).ToList();
                Assert.AreEqual(3, unionAB2.Count);

                Helper.ForEachEnumerableExpression(
                    groupByDefault,
                    new[] { groupByDefault.First() },
                    res => { },
                    @"(a, b) =>
                      {
                        Assert.IsTrue(a.Union(b).SequenceEqual(a.Concat(b), new UnionTests._GroupingComparer<int>()));
                        Assert.IsTrue(a.Union(b, new UnionTests._GroupingComparer<int>()).SequenceEqual(a, new UnionTests._GroupingComparer<int>()));

                        return Helper.NoCallValue;
                      }",
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>),
                    typeof(SkipWhileEnumerable<,,>),
                    typeof(TakeWhileEnumerable<,,>),
                    typeof(WhereWhereEnumerable<,,,>),
                    typeof(OrderByEnumerable<,,,,>)
                );
            }

            // groupBySpecific
            {
                var emptyGroupingString = Enumerable.Empty<GroupingEnumerable<string, string>>();
                Assert.IsTrue(groupBySpecific.Union(emptyGroupingString).SequenceEqual(groupBySpecific, new _GroupingComparer<string>()));
                Assert.IsTrue(groupBySpecific.Union(emptyGroupingString, new _GroupingComparer<string>()).SequenceEqual(groupBySpecific, new _GroupingComparer<string>()));
                var emptyGroupingStringOrdered = Enumerable.Empty<GroupingEnumerable<string, string>>().OrderBy(x => x);
                Assert.IsTrue(groupBySpecific.Union(emptyGroupingStringOrdered).SequenceEqual(groupBySpecific, new _GroupingComparer<string>()));
                Assert.IsTrue(groupBySpecific.Union(emptyGroupingStringOrdered, new _GroupingComparer<string>()).SequenceEqual(groupBySpecific, new _GroupingComparer<string>()));
                var groupByStringDefault = new[] { "hello", "HELLO", "world", "WORLD", "foo", "FOO" }.GroupBy(x => x);
                Assert.IsTrue(groupBySpecific.Union(groupByStringDefault).SequenceEqual(groupBySpecific.Concat(groupByStringDefault), new _GroupingComparer<string>()));
                Assert.IsTrue(groupBySpecific.Union(groupByStringDefault, new _GroupingComparer<string>()).SequenceEqual(groupBySpecific.Concat(groupByStringDefault), new _GroupingComparer<string>()));
                Assert.IsTrue(groupBySpecific.Union(groupBySpecific).SequenceEqual(groupBySpecific.Concat(groupBySpecific), new _GroupingComparer<string>()));
                Assert.IsTrue(groupBySpecific.Union(groupBySpecific, new _GroupingComparer<string>()).SequenceEqual(groupBySpecific, new _GroupingComparer<string>()));
                var lookupString = new[] { "hello", "HELLO", "world", "WORLD", "foo", "FOO" }.ToLookup(x => x, StringComparer.OrdinalIgnoreCase);
                Assert.IsTrue(groupBySpecific.Union(lookupString).SequenceEqual(groupBySpecific.Concat(lookupString), new _GroupingComparer<string>()));
                Assert.IsTrue(groupBySpecific.Union(lookupString, new _GroupingComparer<string>()).SequenceEqual(groupBySpecific, new _GroupingComparer<string>()));
                // range is non-sensical
                var repeatGrouping = Enumerable.Repeat(groupBySpecific.First(), 1);
                Assert.IsTrue(groupBySpecific.Union(repeatGrouping).SequenceEqual(groupBySpecific.Concat(repeatGrouping), new _GroupingComparer<string>()));
                Assert.IsTrue(groupBySpecific.Union(repeatGrouping, new _GroupingComparer<string>()).SequenceEqual(groupBySpecific, new _GroupingComparer<string>()));
                // reverseRange is non-sensical

                var skipWhile = groupBySpecific.SkipWhile(x => false);
                Assert.IsTrue(groupBySpecific.Union(skipWhile).SequenceEqual(groupBySpecific.Concat(groupBySpecific), new _GroupingComparer<string>()));
                Assert.IsTrue(groupBySpecific.Union(skipWhile, new _GroupingComparer<string>()).SequenceEqual(groupBySpecific, new _GroupingComparer<string>()));
                var takeWhile = groupBySpecific.TakeWhile(x => true);
                Assert.IsTrue(groupBySpecific.Union(takeWhile).SequenceEqual(groupBySpecific.Concat(groupBySpecific), new _GroupingComparer<string>()));
                Assert.IsTrue(groupBySpecific.Union(takeWhile, new _GroupingComparer<string>()).SequenceEqual(groupBySpecific, new _GroupingComparer<string>()));
                var whereWhere = groupBySpecific.Where(x => true).Where(x => true);
                Assert.IsTrue(groupBySpecific.Union(whereWhere).SequenceEqual(groupBySpecific.Concat(groupBySpecific), new _GroupingComparer<string>()));
                Assert.IsTrue(groupBySpecific.Union(whereWhere, new _GroupingComparer<string>()).SequenceEqual(groupBySpecific, new _GroupingComparer<string>()));

                var orderBy = new[] { groupBySpecific.First() }.OrderBy(x => x);
                var unionAB1 = groupBySpecific.Union(orderBy).ToList();
                Assert.AreEqual(4, unionAB1.Count);
                var unionAB2 = groupBySpecific.Union(orderBy, new _GroupingComparer<string>()).ToList();
                Assert.AreEqual(3, unionAB2.Count);

                Helper.ForEachEnumerableExpression(
                    groupBySpecific,
                    new[] { groupBySpecific.First() },
                    res => { },
                    @"(a, b) =>
                      {
                        Assert.IsTrue(a.Union(b).SequenceEqual(a.Concat(b), new UnionTests._GroupingComparer<string>()));
                        Assert.IsTrue(a.Union(b, new UnionTests._GroupingComparer<string>()).SequenceEqual(a, new UnionTests._GroupingComparer<string>()));

                        return Helper.NoCallValue;
                      }",
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>),
                    typeof(SkipWhileEnumerable<,,>),
                    typeof(TakeWhileEnumerable<,,>),
                    typeof(WhereWhereEnumerable<,,,>),
                    typeof(OrderByEnumerable<,,,,>)
                );
            }

            // lookup
            {
                var emptyGroupingInt = Enumerable.Empty<GroupingEnumerable<int, int>>();
                Assert.IsTrue(lookup.Union(emptyGroupingInt).SequenceEqual(lookup, new _GroupingComparer<int>()));
                Assert.IsTrue(lookup.Union(emptyGroupingInt, new _GroupingComparer<int>()).SequenceEqual(lookup, new _GroupingComparer<int>()));
                var emptyGroupingIntOrdered = Enumerable.Empty<GroupingEnumerable<int, int>>().OrderBy(x => x);
                Assert.IsTrue(lookup.Union(emptyGroupingIntOrdered).SequenceEqual(lookup, new _GroupingComparer<int>()));
                Assert.IsTrue(lookup.Union(emptyGroupingIntOrdered, new _GroupingComparer<int>()).SequenceEqual(lookup, new _GroupingComparer<int>()));
                Assert.IsTrue(lookup.Union(groupByDefault).SequenceEqual(lookup.Concat(groupByDefault), new _GroupingComparer<int>()));
                Assert.IsTrue(lookup.Union(groupByDefault, new _GroupingComparer<int>()).SequenceEqual(lookup, new _GroupingComparer<int>()));
                var groupByIntSpecific = new[] { 1, 1, 2, 2, 3, 3 }.GroupBy(x => x, new _IntComparer());
                Assert.IsTrue(lookup.Union(groupByIntSpecific).SequenceEqual(lookup.Concat(groupByIntSpecific), new _GroupingComparer<int>()));
                Assert.IsTrue(lookup.Union(groupByIntSpecific, new _GroupingComparer<int>()).SequenceEqual(lookup, new _GroupingComparer<int>()));
                Assert.IsTrue(lookup.Union(lookup).SequenceEqual(lookup.Concat(lookup), new _GroupingComparer<int>()));
                Assert.IsTrue(lookup.Union(lookup, new _GroupingComparer<int>()).SequenceEqual(lookup, new _GroupingComparer<int>()));
                // range is non-sensical
                var repeatGrouping = Enumerable.Repeat(lookup.First(), 1);
                Assert.IsTrue(lookup.Union(repeatGrouping).SequenceEqual(lookup.Concat(repeatGrouping), new _GroupingComparer<int>()));
                Assert.IsTrue(lookup.Union(repeatGrouping, new _GroupingComparer<int>()).SequenceEqual(lookup, new _GroupingComparer<int>()));
                // reverseRange is non-sensical

                var skipWhile = lookup.SkipWhile(x => false);
                Assert.IsTrue(lookup.Union(skipWhile).SequenceEqual(lookup.Concat(lookup), new _GroupingComparer<int>()));
                Assert.IsTrue(lookup.Union(skipWhile, new _GroupingComparer<int>()).SequenceEqual(lookup, new _GroupingComparer<int>()));
                var takeWhile = lookup.TakeWhile(x => true);
                Assert.IsTrue(lookup.Union(takeWhile).SequenceEqual(lookup.Concat(lookup), new _GroupingComparer<int>()));
                Assert.IsTrue(lookup.Union(takeWhile, new _GroupingComparer<int>()).SequenceEqual(lookup, new _GroupingComparer<int>()));
                var whereWhere = lookup.Where(x => true).Where(x => true);
                Assert.IsTrue(lookup.Union(whereWhere).SequenceEqual(lookup.Concat(lookup), new _GroupingComparer<int>()));
                Assert.IsTrue(lookup.Union(whereWhere, new _GroupingComparer<int>()).SequenceEqual(lookup, new _GroupingComparer<int>()));

                var orderBy = new[] { lookup.First() }.OrderBy(x => x);
                var unionAB1 = lookup.Union(orderBy).ToList();
                Assert.AreEqual(4, unionAB1.Count);
                var unionAB2 = lookup.Union(orderBy, new _GroupingComparer<int>()).ToList();
                Assert.AreEqual(3, unionAB2.Count);

                Helper.ForEachEnumerableExpression(
                    lookup,
                    new[] { lookup.First() },
                    res => { },
                    @"(a, b) =>
                      {
                        Assert.IsTrue(a.Union(b).SequenceEqual(a.Concat(b), new UnionTests._GroupingComparer<int>()));
                        Assert.IsTrue(a.Union(b, new UnionTests._GroupingComparer<int>()).SequenceEqual(a, new UnionTests._GroupingComparer<int>()));

                        return Helper.NoCallValue;
                      }",
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>),
                    typeof(SkipWhileEnumerable<,,>),
                    typeof(TakeWhileEnumerable<,,>),
                    typeof(WhereWhereEnumerable<,,,>),
                    typeof(OrderByEnumerable<,,,,>)
                );
            }

            // range
            {
                Assert.IsTrue(range.Union(empty).SequenceEqual(range));
                Assert.IsTrue(range.Union(empty, new _IntComparer()).SequenceEqual(range));
                Assert.IsTrue(range.Union(emptyOrdered).SequenceEqual(range));
                Assert.IsTrue(range.Union(emptyOrdered, new _IntComparer()).SequenceEqual(range));
                // groupby & lookup are non-sensical
                Assert.IsTrue(range.Union(range).SequenceEqual(range));
                Assert.IsTrue(range.Union(range, new _IntComparer()).SequenceEqual(range));
                var repeatInt = Enumerable.Repeat(6, 6);
                Assert.IsTrue(range.Union(repeatInt).SequenceEqual(new[] { 1, 2, 3, 4, 5, 6 }));
                Assert.IsTrue(range.Union(repeatInt, new _IntComparer()).SequenceEqual(new[] { 1, 2, 3, 4, 5, 6 }));
                Assert.IsTrue(range.Union(reverseRange).SequenceEqual(range));
                Assert.IsTrue(range.Union(reverseRange, new _IntComparer()).SequenceEqual(range));

                Assert.IsTrue(range.Union(oneItemDefault).SequenceEqual(new[] { 1, 2, 3, 4, 5, 0 }));
                Assert.IsTrue(range.Union(oneItemDefault, new _IntComparer()).SequenceEqual(new[] { 1, 2, 3, 4, 5, 0 }));
                Assert.IsTrue(range.Union(oneItemSpecific).SequenceEqual(new[] { 1, 2, 3, 4, 5 }));
                Assert.IsTrue(range.Union(oneItemSpecific, new _IntComparer()).SequenceEqual(new[] { 1, 2, 3, 4, 5 }));
                Assert.IsTrue(range.Union(oneItemDefaultOrdered).SequenceEqual(new[] { 1, 2, 3, 4, 5, 0 }));
                Assert.IsTrue(range.Union(oneItemDefaultOrdered, new _IntComparer()).SequenceEqual(new[] { 1, 2, 3, 4, 5, 0 }));
                Assert.IsTrue(range.Union(oneItemSpecificOrdered).SequenceEqual(new[] { 1, 2, 3, 4, 5 }));
                Assert.IsTrue(range.Union(oneItemSpecificOrdered, new _IntComparer()).SequenceEqual(new[] { 1, 2, 3, 4, 5 }));

                Helper.ForEachEnumerableExpression(
                    range,
                    new[] { 6 },
                    res => { },
                    @"(a, b) =>
                      {
                        Assert.IsTrue(a.Union(b).SequenceEqual(new [] { 1, 2, 3, 4, 5, 6 }));
                        Assert.IsTrue(a.Union(b, new UnionTests._IntComparer()).SequenceEqual(new [] { 1, 2, 3, 4, 5, 6 }));

                        return Helper.NoCallValue;
                      }",
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );
            }

            // repeat
            {
                var emptyString = Enumerable.Empty<string>();
                Assert.IsTrue(repeat.Union(emptyString).SequenceEqual(new[] { repeat.First() }));
                Assert.IsTrue(repeat.Union(emptyString, StringComparer.InvariantCultureIgnoreCase).SequenceEqual(new[] { repeat.First() }));
                var emptyStringOrdered = emptyString.OrderBy(x => x);
                Assert.IsTrue(repeat.Union(emptyStringOrdered).SequenceEqual(new[] { repeat.First() }));
                Assert.IsTrue(repeat.Union(emptyStringOrdered, StringComparer.InvariantCultureIgnoreCase).SequenceEqual(new[] { repeat.First() }));
                // groupby, looking, & range are non-sensical
                Assert.IsTrue(repeat.Union(repeat).SequenceEqual(new[] { repeat.First() }));
                Assert.IsTrue(repeat.Union(repeat, StringComparer.InvariantCultureIgnoreCase).SequenceEqual(new[] { repeat.First() }));

                var repeatInt = Enumerable.Repeat(1, 1);
                Assert.IsTrue(repeatInt.Union(oneItemDefault).SequenceEqual(new[] { 1, 0 }));
                Assert.IsTrue(repeatInt.Union(oneItemDefault, new _IntComparer()).SequenceEqual(new[] { 1, 0 }));
                Assert.IsTrue(repeatInt.Union(oneItemSpecific).SequenceEqual(new[] { 1, 4 }));
                Assert.IsTrue(repeatInt.Union(oneItemSpecific, new _IntComparer()).SequenceEqual(new[] { 1, 4 }));
                Assert.IsTrue(repeatInt.Union(oneItemDefaultOrdered).SequenceEqual(new[] { 1, 0 }));
                Assert.IsTrue(repeatInt.Union(oneItemDefaultOrdered, new _IntComparer()).SequenceEqual(new[] { 1, 0 }));
                Assert.IsTrue(repeatInt.Union(oneItemSpecificOrdered).SequenceEqual(new[] { 1, 4 }));
                Assert.IsTrue(repeatInt.Union(oneItemSpecificOrdered, new _IntComparer()).SequenceEqual(new[] { 1, 4 }));

                Helper.ForEachEnumerableExpression(
                    repeat,
                    new[] { "bar" },
                    res => { },
                    @"(a, b) =>
                      {
                        Assert.IsTrue(a.Union(b).SequenceEqual(new[] { ""foo"", ""bar"" }));
                        Assert.IsTrue(a.Union(b, StringComparer.InvariantCultureIgnoreCase).SequenceEqual(new[] { ""foo"", ""bar"" }));

                        return Helper.NoCallValue;
                      }",
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );
            }

            // reverseRange
            {
                Assert.IsTrue(reverseRange.Union(empty).SequenceEqual(reverseRange));
                Assert.IsTrue(reverseRange.Union(empty, new _IntComparer()).SequenceEqual(reverseRange));
                Assert.IsTrue(reverseRange.Union(emptyOrdered).SequenceEqual(reverseRange));
                Assert.IsTrue(reverseRange.Union(emptyOrdered, new _IntComparer()).SequenceEqual(reverseRange));
                // groupby & lookup are non-sensical
                Assert.IsTrue(reverseRange.Union(range).SequenceEqual(reverseRange));
                Assert.IsTrue(reverseRange.Union(range, new _IntComparer()).SequenceEqual(reverseRange));
                var repeatInt = Enumerable.Repeat(6, 6);
                Assert.IsTrue(reverseRange.Union(repeatInt).SequenceEqual(new[] { 5, 4, 3, 2, 1, 6 }));
                Assert.IsTrue(reverseRange.Union(repeatInt, new _IntComparer()).SequenceEqual(new[] { 5, 4, 3, 2, 1, 6 }));
                Assert.IsTrue(reverseRange.Union(reverseRange).SequenceEqual(reverseRange));
                Assert.IsTrue(reverseRange.Union(reverseRange, new _IntComparer()).SequenceEqual(reverseRange));

                Assert.IsTrue(reverseRange.Union(oneItemDefault).SequenceEqual(new[] { 5, 4, 3, 2, 1, 0 }));
                Assert.IsTrue(reverseRange.Union(oneItemDefault, new _IntComparer()).SequenceEqual(new[] { 5, 4, 3, 2, 1, 0 }));
                Assert.IsTrue(reverseRange.Union(oneItemSpecific).SequenceEqual(new[] { 5, 4, 3, 2, 1 }));
                Assert.IsTrue(reverseRange.Union(oneItemSpecific, new _IntComparer()).SequenceEqual(new[] { 5, 4, 3, 2, 1 }));
                Assert.IsTrue(reverseRange.Union(oneItemDefaultOrdered).SequenceEqual(new[] { 5, 4, 3, 2, 1, 0 }));
                Assert.IsTrue(reverseRange.Union(oneItemDefaultOrdered, new _IntComparer()).SequenceEqual(new[] { 5, 4, 3, 2, 1, 0 }));
                Assert.IsTrue(reverseRange.Union(oneItemSpecificOrdered).SequenceEqual(new[] { 5, 4, 3, 2, 1 }));
                Assert.IsTrue(reverseRange.Union(oneItemSpecificOrdered, new _IntComparer()).SequenceEqual(new[] { 5, 4, 3, 2, 1 }));

                Helper.ForEachEnumerableExpression(
                    reverseRange,
                    new[] { 6 },
                    res => { },
                    @"(a, b) =>
                      {
                        Assert.IsTrue(a.Union(b).SequenceEqual(new[] { 5, 4, 3, 2, 1, 6 }));
                        Assert.IsTrue(a.Union(b, new UnionTests._IntComparer()).SequenceEqual(new[] { 5, 4, 3, 2, 1, 6 }));

                        return Helper.NoCallValue;
                      }",
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );
            }

            // oneItemDefault
            {
                Assert.IsTrue(oneItemDefault.Union(empty).SequenceEqual(oneItemDefault));
                Assert.IsTrue(oneItemDefault.Union(empty, new _IntComparer()).SequenceEqual(oneItemDefault));
                Assert.IsTrue(oneItemDefault.Union(emptyOrdered).SequenceEqual(oneItemDefault));
                Assert.IsTrue(oneItemDefault.Union(emptyOrdered, new _IntComparer()).SequenceEqual(oneItemDefault));
                // default grouping is non-sensical
                Assert.IsTrue(oneItemDefault.Union(range).SequenceEqual(new[] { 0, 1, 2, 3, 4, 5 }));
                Assert.IsTrue(oneItemDefault.Union(range, new _IntComparer()).SequenceEqual(new[] { 0, 1, 2, 3, 4, 5 }));
                var repeatInt = Enumerable.Repeat(1, 1);
                Assert.IsTrue(oneItemDefault.Union(repeatInt).SequenceEqual(new[] { 0, 1 }));
                Assert.IsTrue(oneItemDefault.Union(repeatInt, new _IntComparer()).SequenceEqual(new[] { 0, 1 }));
                Assert.IsTrue(oneItemDefault.Union(reverseRange).SequenceEqual(new[] { 0, 5, 4, 3, 2, 1 }));
                Assert.IsTrue(oneItemDefault.Union(reverseRange, new _IntComparer()).SequenceEqual(new[] { 0, 5, 4, 3, 2, 1 }));

                var defaultIfEmptyDefault = new[] { 1 }.DefaultIfEmpty();
                Assert.IsTrue(oneItemDefault.Union(defaultIfEmptyDefault).SequenceEqual(new[] { 0, 1 }));
                Assert.IsTrue(oneItemDefault.Union(defaultIfEmptyDefault, new _IntComparer()).SequenceEqual(new[] { 0, 1 }));

                var defaultIfEmptySpecific = new[] { 1 }.DefaultIfEmpty(4);
                Assert.IsTrue(oneItemDefault.Union(defaultIfEmptySpecific).SequenceEqual(new[] { 0, 1 }));
                Assert.IsTrue(oneItemDefault.Union(defaultIfEmptySpecific, new _IntComparer()).SequenceEqual(new[] { 0, 1 }));

                Assert.IsTrue(oneItemDefault.Union(oneItemDefault).SequenceEqual(oneItemDefault));
                Assert.IsTrue(oneItemDefault.Union(oneItemDefault, new _IntComparer()).SequenceEqual(oneItemDefault));
                Assert.IsTrue(oneItemDefault.Union(oneItemSpecific).SequenceEqual(new[] { 0, 4 }));
                Assert.IsTrue(oneItemDefault.Union(oneItemSpecific, new _IntComparer()).SequenceEqual(new[] { 0, 4 }));
                Assert.IsTrue(oneItemDefault.Union(oneItemDefaultOrdered).SequenceEqual(oneItemDefaultOrdered));
                Assert.IsTrue(oneItemDefault.Union(oneItemDefaultOrdered, new _IntComparer()).SequenceEqual(oneItemDefaultOrdered));
                Assert.IsTrue(oneItemDefault.Union(oneItemSpecificOrdered).SequenceEqual(new[] { 0, 4 }));
                Assert.IsTrue(oneItemDefault.Union(oneItemSpecificOrdered, new _IntComparer()).SequenceEqual(new[] { 0, 4 }));

                Helper.ForEachEnumerableExpression(
                    oneItemDefault,
                    new int[0],
                    res => { },
                    @"(a, b) =>
                      {
                        Assert.IsTrue(a.Union(b).SequenceEqual(a));
                        Assert.IsTrue(a.Union(b, new UnionTests._IntComparer()).SequenceEqual(a));

                        return Helper.NoCallValue;
                      }",
                    typeof(DefaultIfEmptyDefaultEnumerable<,,>),
                    typeof(DefaultIfEmptySpecificEnumerable<,,>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );
            }

            // oneItemSpecific
            {
                Assert.IsTrue(oneItemSpecific.Union(empty).SequenceEqual(oneItemSpecific));
                Assert.IsTrue(oneItemSpecific.Union(empty, new _IntComparer()).SequenceEqual(oneItemSpecific));
                Assert.IsTrue(oneItemSpecific.Union(emptyOrdered).SequenceEqual(oneItemSpecific));
                Assert.IsTrue(oneItemSpecific.Union(emptyOrdered, new _IntComparer()).SequenceEqual(oneItemSpecific));

                var oneItemSpecificGroupingInt = Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty(groupByDefault.First());
                Assert.IsTrue(oneItemSpecificGroupingInt.Union(groupByDefault).SequenceEqual(new[] { groupByDefault.ElementAt(0), groupByDefault.ElementAt(0), groupByDefault.ElementAt(1), groupByDefault.ElementAt(2) }, new _GroupingComparer<int>()));
                Assert.IsTrue(oneItemSpecificGroupingInt.Union(groupByDefault, new _GroupingComparer<int>()).SequenceEqual(groupByDefault, new _GroupingComparer<int>()));

                var oneItemSpecificGroupingString = Enumerable.Empty<GroupingEnumerable<string, string>>().DefaultIfEmpty(groupBySpecific.First());
                Assert.IsTrue(oneItemSpecificGroupingString.Union(groupBySpecific).SequenceEqual(new[] { groupBySpecific.ElementAt(0), groupBySpecific.ElementAt(0), groupBySpecific.ElementAt(1), groupBySpecific.ElementAt(2) }, new _GroupingComparer<string>()));
                Assert.IsTrue(oneItemSpecificGroupingString.Union(groupBySpecific, new _GroupingComparer<string>()).SequenceEqual(groupBySpecific, new _GroupingComparer<string>()));

                Assert.IsTrue(oneItemSpecificGroupingInt.Union(lookup).SequenceEqual(new[] { lookup.ElementAt(0), lookup.ElementAt(0), lookup.ElementAt(1), lookup.ElementAt(2) }, new _GroupingComparer<int>()));
                Assert.IsTrue(oneItemSpecificGroupingInt.Union(lookup, new _GroupingComparer<int>()).SequenceEqual(lookup, new _GroupingComparer<int>()));

                Assert.IsTrue(oneItemSpecific.Union(range).SequenceEqual(new[] { 4, 1, 2, 3, 5 }));
                Assert.IsTrue(oneItemSpecific.Union(range, new _IntComparer()).SequenceEqual(new[] { 4, 1, 2, 3, 5 }));
                var repeatInt = Enumerable.Repeat(1, 1);
                Assert.IsTrue(oneItemSpecific.Union(repeatInt).SequenceEqual(new[] { 4, 1 }));
                Assert.IsTrue(oneItemSpecific.Union(repeatInt, new _IntComparer()).SequenceEqual(new[] { 4, 1 }));
                Assert.IsTrue(oneItemSpecific.Union(reverseRange).SequenceEqual(new[] { 4, 5, 3, 2, 1 }));
                Assert.IsTrue(oneItemSpecific.Union(reverseRange, new _IntComparer()).SequenceEqual(new[] { 4, 5, 3, 2, 1 }));

                var defaultIfEmptyDefault = new[] { 1 }.DefaultIfEmpty();
                Assert.IsTrue(oneItemSpecific.Union(defaultIfEmptyDefault).SequenceEqual(new[] { 4, 1 }));
                Assert.IsTrue(oneItemSpecific.Union(defaultIfEmptyDefault, new _IntComparer()).SequenceEqual(new[] { 4, 1 }));

                var defaultIfEmptySpecific = new[] { 1 }.DefaultIfEmpty(4);
                Assert.IsTrue(oneItemSpecific.Union(defaultIfEmptySpecific).SequenceEqual(new[] { 4, 1 }));
                Assert.IsTrue(oneItemSpecific.Union(defaultIfEmptySpecific, new _IntComparer()).SequenceEqual(new[] { 4, 1 }));

                Assert.IsTrue(oneItemSpecific.Union(oneItemDefault).SequenceEqual(new[] { 4, 0 }));
                Assert.IsTrue(oneItemSpecific.Union(oneItemDefault, new _IntComparer()).SequenceEqual(new[] { 4, 0 }));
                Assert.IsTrue(oneItemSpecific.Union(oneItemSpecific).SequenceEqual(oneItemSpecific));
                Assert.IsTrue(oneItemSpecific.Union(oneItemSpecific, new _IntComparer()).SequenceEqual(oneItemSpecific));
                Assert.IsTrue(oneItemSpecific.Union(oneItemDefaultOrdered).SequenceEqual(new[] { 4, 0 }));
                Assert.IsTrue(oneItemSpecific.Union(oneItemDefaultOrdered, new _IntComparer()).SequenceEqual(new[] { 4, 0 }));
                Assert.IsTrue(oneItemSpecific.Union(oneItemSpecificOrdered).SequenceEqual(oneItemSpecificOrdered));
                Assert.IsTrue(oneItemSpecific.Union(oneItemSpecificOrdered, new _IntComparer()).SequenceEqual(oneItemSpecificOrdered));

                Helper.ForEachEnumerableExpression(
                    oneItemSpecific,
                    new int[0],
                    res => { },
                    @"(a, b) =>
                      {
                        Assert.IsTrue(a.Union(b).SequenceEqual(a));
                        Assert.IsTrue(a.Union(b, new UnionTests._IntComparer()).SequenceEqual(a));

                        return Helper.NoCallValue;
                      }",
                    typeof(DefaultIfEmptyDefaultEnumerable<,,>),
                    typeof(DefaultIfEmptySpecificEnumerable<,,>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );
            }

            // oneItemDefaultOrdered
            {
                Assert.IsTrue(oneItemDefaultOrdered.Union(empty).SequenceEqual(oneItemDefault));
                Assert.IsTrue(oneItemDefaultOrdered.Union(empty, new _IntComparer()).SequenceEqual(oneItemDefault));
                Assert.IsTrue(oneItemDefaultOrdered.Union(emptyOrdered).SequenceEqual(oneItemDefault));
                Assert.IsTrue(oneItemDefaultOrdered.Union(emptyOrdered, new _IntComparer()).SequenceEqual(oneItemDefault));
                // default grouping is non-sensical
                Assert.IsTrue(oneItemDefaultOrdered.Union(range).SequenceEqual(new[] { 0, 1, 2, 3, 4, 5 }));
                Assert.IsTrue(oneItemDefaultOrdered.Union(range, new _IntComparer()).SequenceEqual(new[] { 0, 1, 2, 3, 4, 5 }));
                var repeatInt = Enumerable.Repeat(1, 1);
                Assert.IsTrue(oneItemDefaultOrdered.Union(repeatInt).SequenceEqual(new[] { 0, 1 }));
                Assert.IsTrue(oneItemDefaultOrdered.Union(repeatInt, new _IntComparer()).SequenceEqual(new[] { 0, 1 }));
                Assert.IsTrue(oneItemDefaultOrdered.Union(reverseRange).SequenceEqual(new[] { 0, 5, 4, 3, 2, 1 }));
                Assert.IsTrue(oneItemDefaultOrdered.Union(reverseRange, new _IntComparer()).SequenceEqual(new[] { 0, 5, 4, 3, 2, 1 }));

                var defaultIfEmptyDefault = new[] { 1 }.DefaultIfEmpty();
                Assert.IsTrue(oneItemDefaultOrdered.Union(defaultIfEmptyDefault).SequenceEqual(new[] { 0, 1 }));
                Assert.IsTrue(oneItemDefaultOrdered.Union(defaultIfEmptyDefault, new _IntComparer()).SequenceEqual(new[] { 0, 1 }));

                var defaultIfEmptySpecific = new[] { 1 }.DefaultIfEmpty(4);
                Assert.IsTrue(oneItemDefaultOrdered.Union(defaultIfEmptySpecific).SequenceEqual(new[] { 0, 1 }));
                Assert.IsTrue(oneItemDefaultOrdered.Union(defaultIfEmptySpecific, new _IntComparer()).SequenceEqual(new[] { 0, 1 }));

                Assert.IsTrue(oneItemDefaultOrdered.Union(oneItemDefault).SequenceEqual(oneItemDefault));
                Assert.IsTrue(oneItemDefaultOrdered.Union(oneItemDefault, new _IntComparer()).SequenceEqual(oneItemDefault));
                Assert.IsTrue(oneItemDefaultOrdered.Union(oneItemSpecific).SequenceEqual(new[] { 0, 4 }));
                Assert.IsTrue(oneItemDefaultOrdered.Union(oneItemSpecific, new _IntComparer()).SequenceEqual(new[] { 0, 4 }));
                Assert.IsTrue(oneItemDefaultOrdered.Union(oneItemDefaultOrdered).SequenceEqual(oneItemDefaultOrdered));
                Assert.IsTrue(oneItemDefaultOrdered.Union(oneItemDefaultOrdered, new _IntComparer()).SequenceEqual(oneItemDefaultOrdered));
                Assert.IsTrue(oneItemDefaultOrdered.Union(oneItemSpecificOrdered).SequenceEqual(new[] { 0, 4 }));
                Assert.IsTrue(oneItemDefaultOrdered.Union(oneItemSpecificOrdered, new _IntComparer()).SequenceEqual(new[] { 0, 4 }));

                Helper.ForEachEnumerableExpression(
                    oneItemDefaultOrdered,
                    new int[0],
                    res => { },
                    @"(a, b) =>
                      {
                        Assert.IsTrue(a.Union(b).SequenceEqual(a));
                        Assert.IsTrue(a.Union(b, new UnionTests._IntComparer()).SequenceEqual(a));

                        return Helper.NoCallValue;
                      }",
                    typeof(DefaultIfEmptyDefaultEnumerable<,,>),
                    typeof(DefaultIfEmptySpecificEnumerable<,,>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );
            }

            // oneItemSpecificOrdered
            {
                Assert.IsTrue(oneItemSpecificOrdered.Union(empty).SequenceEqual(oneItemSpecific));
                Assert.IsTrue(oneItemSpecificOrdered.Union(empty, new _IntComparer()).SequenceEqual(oneItemSpecific));
                Assert.IsTrue(oneItemSpecificOrdered.Union(emptyOrdered).SequenceEqual(oneItemSpecific));
                Assert.IsTrue(oneItemSpecificOrdered.Union(emptyOrdered, new _IntComparer()).SequenceEqual(oneItemSpecific));

                var oneItemSpecificOrderedGroupingInt = Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty(groupByDefault.First()).OrderBy(x => x);
                Assert.IsTrue(oneItemSpecificOrderedGroupingInt.Union(groupByDefault).SequenceEqual(new[] { groupByDefault.ElementAt(0), groupByDefault.ElementAt(0), groupByDefault.ElementAt(1), groupByDefault.ElementAt(2) }, new _GroupingComparer<int>()));
                Assert.IsTrue(oneItemSpecificOrderedGroupingInt.Union(groupByDefault, new _GroupingComparer<int>()).SequenceEqual(groupByDefault, new _GroupingComparer<int>()));

                var oneItemSpecificOrderedGroupingString = Enumerable.Empty<GroupingEnumerable<string, string>>().DefaultIfEmpty(groupBySpecific.First()).OrderBy(x => x);
                Assert.IsTrue(oneItemSpecificOrderedGroupingString.Union(groupBySpecific).SequenceEqual(new[] { groupBySpecific.ElementAt(0), groupBySpecific.ElementAt(0), groupBySpecific.ElementAt(1), groupBySpecific.ElementAt(2) }, new _GroupingComparer<string>()));
                Assert.IsTrue(oneItemSpecificOrderedGroupingString.Union(groupBySpecific, new _GroupingComparer<string>()).SequenceEqual(groupBySpecific, new _GroupingComparer<string>()));

                Assert.IsTrue(oneItemSpecificOrderedGroupingInt.Union(lookup).SequenceEqual(new[] { lookup.ElementAt(0), lookup.ElementAt(0), lookup.ElementAt(1), lookup.ElementAt(2) }, new _GroupingComparer<int>()));
                Assert.IsTrue(oneItemSpecificOrderedGroupingInt.Union(lookup, new _GroupingComparer<int>()).SequenceEqual(lookup, new _GroupingComparer<int>()));

                Assert.IsTrue(oneItemSpecificOrdered.Union(range).SequenceEqual(new[] { 4, 1, 2, 3, 5 }));
                Assert.IsTrue(oneItemSpecificOrdered.Union(range, new _IntComparer()).SequenceEqual(new[] { 4, 1, 2, 3, 5 }));
                var repeatInt = Enumerable.Repeat(1, 1);
                Assert.IsTrue(oneItemSpecificOrdered.Union(repeatInt).SequenceEqual(new[] { 4, 1 }));
                Assert.IsTrue(oneItemSpecificOrdered.Union(repeatInt, new _IntComparer()).SequenceEqual(new[] { 4, 1 }));
                Assert.IsTrue(oneItemSpecificOrdered.Union(reverseRange).SequenceEqual(new[] { 4, 5, 3, 2, 1 }));
                Assert.IsTrue(oneItemSpecificOrdered.Union(reverseRange, new _IntComparer()).SequenceEqual(new[] { 4, 5, 3, 2, 1 }));

                var defaultIfEmptyDefault = new[] { 1 }.DefaultIfEmpty();
                Assert.IsTrue(oneItemSpecificOrdered.Union(defaultIfEmptyDefault).SequenceEqual(new[] { 4, 1 }));
                Assert.IsTrue(oneItemSpecificOrdered.Union(defaultIfEmptyDefault, new _IntComparer()).SequenceEqual(new[] { 4, 1 }));

                var defaultIfEmptySpecific = new[] { 1 }.DefaultIfEmpty(4);
                Assert.IsTrue(oneItemSpecificOrdered.Union(defaultIfEmptySpecific).SequenceEqual(new[] { 4, 1 }));
                Assert.IsTrue(oneItemSpecificOrdered.Union(defaultIfEmptySpecific, new _IntComparer()).SequenceEqual(new[] { 4, 1 }));

                Assert.IsTrue(oneItemSpecificOrdered.Union(oneItemDefault).SequenceEqual(new[] { 4, 0 }));
                Assert.IsTrue(oneItemSpecificOrdered.Union(oneItemDefault, new _IntComparer()).SequenceEqual(new[] { 4, 0 }));
                Assert.IsTrue(oneItemSpecificOrdered.Union(oneItemSpecific).SequenceEqual(oneItemSpecific));
                Assert.IsTrue(oneItemSpecificOrdered.Union(oneItemSpecific, new _IntComparer()).SequenceEqual(oneItemSpecific));
                Assert.IsTrue(oneItemSpecificOrdered.Union(oneItemDefaultOrdered).SequenceEqual(new[] { 4, 0 }));
                Assert.IsTrue(oneItemSpecificOrdered.Union(oneItemDefaultOrdered, new _IntComparer()).SequenceEqual(new[] { 4, 0 }));
                Assert.IsTrue(oneItemSpecificOrdered.Union(oneItemSpecificOrdered).SequenceEqual(oneItemSpecificOrdered));
                Assert.IsTrue(oneItemSpecificOrdered.Union(oneItemSpecificOrdered, new _IntComparer()).SequenceEqual(oneItemSpecificOrdered));

                Helper.ForEachEnumerableExpression(
                    oneItemSpecificOrdered,
                    new int[0],
                    res => { },
                    @"(a, b) =>
                      {
                        Assert.IsTrue(a.Union(b).SequenceEqual(a));
                        Assert.IsTrue(a.Union(b, new UnionTests._IntComparer()).SequenceEqual(a));

                        return Helper.NoCallValue;
                      }",
                    typeof(DefaultIfEmptyDefaultEnumerable<,,>),
                    typeof(DefaultIfEmptySpecificEnumerable<,,>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );
            }
        }

        [TestMethod]
        public void Malformed_Default()
        {
            // default
            {
                Helper.ForEachMalformedEnumerableExpression<string>(
                    @"a =>
                      Helper.ForEachEnumerableExpression(
                        a,
                        new string[0],
                        res => { },
                        @""(a, b) =>
                           {
                            try
                            {
                                a.Union(b);
                                Assert.Fail();
                            }
                            catch(ArgumentException exc)
                            {
                                Assert.AreEqual(""""first"""", exc.ParamName);
                            }

                            try
                            {
                                b.Union(a);
                                Assert.Fail();
                            }
                            catch(ArgumentException exc)
                            {
                                Assert.AreEqual(""""second"""", exc.ParamName);
                            }

                            return Helper.NoCallValue;
                           }"",
                        typeof(GroupByDefaultEnumerable<,,,,>),
                        typeof(GroupBySpecificEnumerable<,,,,>),
                        typeof(LookupDefaultEnumerable<,>),
                        typeof(LookupSpecificEnumerable<,>)
                      )",
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );
            }
        }

        [TestMethod]
        public void Malformed_Specific()
        {
            // default
            {
                Helper.ForEachMalformedEnumerableExpression<string>(
                    @"a =>
                      Helper.ForEachEnumerableExpression(
                        a,
                        new string[0],
                        res => { },
                        @""(a, b) =>
                           {
                            try
                            {
                                a.Union(b, StringComparer.InvariantCulture);
                                Assert.Fail();
                            }
                            catch(ArgumentException exc)
                            {
                                Assert.AreEqual(""""first"""", exc.ParamName);
                            }

                            try
                            {
                                b.Union(a, StringComparer.InvariantCulture);
                                Assert.Fail();
                            }
                            catch(ArgumentException exc)
                            {
                                Assert.AreEqual(""""second"""", exc.ParamName);
                            }

                            return Helper.NoCallValue;
                           }"",
                        typeof(GroupByDefaultEnumerable<,,,,>),
                        typeof(GroupBySpecificEnumerable<,,,,>),
                        typeof(LookupDefaultEnumerable<,>),
                        typeof(LookupSpecificEnumerable<,>)
                      )",
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );
            }
        }

        [TestMethod]
        public void Malformed_Weird()
        {
            var empty = new EmptyEnumerable<int>();
            var emptyOrdered = new EmptyOrderedEnumerable<int>();
            var groupByDefault = new GroupByDefaultEnumerable<int, int, int, EmptyEnumerable<int>, EmptyEnumerator<int>>();
            var groupBySpecific = new GroupBySpecificEnumerable<int, int, int, EmptyEnumerable<int>, EmptyEnumerator<int>>();
            var lookup = new LookupDefaultEnumerable<int, int>();
            var range = new RangeEnumerable();
            var repeat = new RepeatEnumerable<int>();
            var reverseRange = new ReverseRangeEnumerable();
            var oneItemDefault = new OneItemDefaultEnumerable<int>();
            var oneItemSpecific = new OneItemSpecificEnumerable<int>();
            var oneItemDefaultOrdered = new OneItemDefaultOrderedEnumerable<int>();
            var oneItemSpecificOrdered = new OneItemSpecificOrderedEnumerable<int>();

            // empty
            {
                var emptyGood = Enumerable.Empty<int>();
                try { empty.Union(emptyGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { empty.Union(emptyGood, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { emptyGood.Union(empty); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { emptyGood.Union(empty, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                var emptyOrderedGood = Enumerable.Empty<int>().OrderBy(x => x);
                try { empty.Union(emptyOrderedGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { empty.Union(emptyOrderedGood, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { emptyOrderedGood.Union(empty); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { emptyOrderedGood.Union(empty, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }


                var emptyGroupingBad = new EmptyEnumerable<GroupingEnumerable<int, int>>();

                var groupByDefaultGood = new[] { 1 }.GroupBy(x => x);
                try { emptyGroupingBad.Union(groupByDefaultGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { emptyGroupingBad.Union(groupByDefaultGood, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { groupByDefaultGood.Union(emptyGroupingBad); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { groupByDefaultGood.Union(emptyGroupingBad, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                var groupBySpecificGood = new[] { 1 }.GroupBy(x => x, new _IntComparer());
                try { emptyGroupingBad.Union(groupBySpecificGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { emptyGroupingBad.Union(groupBySpecificGood, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { groupBySpecificGood.Union(emptyGroupingBad); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { groupBySpecificGood.Union(emptyGroupingBad, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                var lookupGood = new[] { 1 }.ToLookup(x => x);
                try { emptyGroupingBad.Union(lookupGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { emptyGroupingBad.Union(lookupGood, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { lookupGood.Union(emptyGroupingBad); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { lookupGood.Union(emptyGroupingBad, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                var rangeGood = Enumerable.Range(1, 1);
                try { empty.Union(rangeGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { empty.Union(rangeGood, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { rangeGood.Union(empty); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { rangeGood.Union(empty, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                var repeatGood = Enumerable.Repeat(1, 1);
                try { empty.Union(repeatGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { empty.Union(repeatGood, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { repeatGood.Union(empty); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { repeatGood.Union(empty, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                var reverseRangeGood = Enumerable.Range(1, 1).Reverse();
                try { empty.Union(reverseRangeGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { empty.Union(reverseRangeGood, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { reverseRangeGood.Union(empty); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { reverseRangeGood.Union(empty, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                var oneItemDefaultGood = Enumerable.Empty<int>().DefaultIfEmpty();
                try { empty.Union(oneItemDefaultGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { empty.Union(oneItemDefaultGood, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemDefaultGood.Union(empty); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemDefaultGood.Union(empty, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                var oneItemSpecificGood = Enumerable.Empty<int>().DefaultIfEmpty(4);
                try { empty.Union(oneItemSpecificGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { empty.Union(oneItemSpecificGood, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemSpecificGood.Union(empty); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemSpecificGood.Union(empty, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                var oneItemDefaultOrderedGood = Enumerable.Empty<int>().DefaultIfEmpty().OrderBy(x => x);
                try { empty.Union(oneItemDefaultOrderedGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { empty.Union(oneItemDefaultOrderedGood, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemDefaultOrderedGood.Union(empty); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemDefaultOrderedGood.Union(empty, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                var oneItemSpecificOrderedGood = Enumerable.Empty<int>().DefaultIfEmpty(4).OrderBy(x => x);
                try { empty.Union(oneItemSpecificOrderedGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { empty.Union(oneItemSpecificOrderedGood, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemSpecificOrderedGood.Union(empty); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemSpecificOrderedGood.Union(empty, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                Helper.ForEachEnumerableExpression(
                    empty,
                    new int[0],
                    res => { },
                    @"(bad, good) =>
                      {
                        try { bad.Union(good); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""first"", exc.ParamName); }
                        try { bad.Union(good, new UnionTests._IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""first"", exc.ParamName); }
                        try { good.Union(bad); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""second"", exc.ParamName); }
                        try { good.Union(bad, new UnionTests._IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""second"", exc.ParamName); }

                        return Helper.NoCallValue;
                      }",
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );
            }

            // emptyOrdered
            {
                var emptyGood = Enumerable.Empty<int>();
                try { emptyOrdered.Union(emptyGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { emptyOrdered.Union(emptyGood, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { emptyGood.Union(emptyOrdered); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { emptyGood.Union(emptyOrdered, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                var emptyOrderedGood = Enumerable.Empty<int>().OrderBy(x => x);
                try { emptyOrdered.Union(emptyOrderedGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { emptyOrdered.Union(emptyOrderedGood, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { emptyOrderedGood.Union(emptyOrdered); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { emptyOrderedGood.Union(emptyOrdered, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }


                var emptyGroupingOrdredBad = new EmptyOrderedEnumerable<GroupingEnumerable<int, int>>();

                var groupByDefaultGood = new[] { 1 }.GroupBy(x => x);
                try { emptyGroupingOrdredBad.Union(groupByDefaultGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { emptyGroupingOrdredBad.Union(groupByDefaultGood, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { groupByDefaultGood.Union(emptyGroupingOrdredBad); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { groupByDefaultGood.Union(emptyGroupingOrdredBad, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                var groupBySpecificGood = new[] { 1 }.GroupBy(x => x, new _IntComparer());
                try { emptyGroupingOrdredBad.Union(groupBySpecificGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { emptyGroupingOrdredBad.Union(groupBySpecificGood, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { groupBySpecificGood.Union(emptyGroupingOrdredBad); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { groupBySpecificGood.Union(emptyGroupingOrdredBad, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                var lookupGood = new[] { 1 }.ToLookup(x => x);
                try { emptyGroupingOrdredBad.Union(lookupGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { emptyGroupingOrdredBad.Union(lookupGood, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { lookupGood.Union(emptyGroupingOrdredBad); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { lookupGood.Union(emptyGroupingOrdredBad, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                var rangeGood = Enumerable.Range(1, 1);
                try { emptyOrdered.Union(rangeGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { emptyOrdered.Union(rangeGood, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { rangeGood.Union(emptyOrdered); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { rangeGood.Union(emptyOrdered, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                var repeatGood = Enumerable.Repeat(1, 1);
                try { emptyOrdered.Union(repeatGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { emptyOrdered.Union(repeatGood, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { repeatGood.Union(emptyOrdered); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { repeatGood.Union(emptyOrdered, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                var reverseRangeGood = Enumerable.Range(1, 1).Reverse();
                try { emptyOrdered.Union(reverseRangeGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { emptyOrdered.Union(reverseRangeGood, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { reverseRangeGood.Union(emptyOrdered); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { reverseRangeGood.Union(emptyOrdered, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                var oneItemDefaultGood = Enumerable.Empty<int>().DefaultIfEmpty();
                try { emptyOrdered.Union(oneItemDefaultGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { emptyOrdered.Union(oneItemDefaultGood, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemDefaultGood.Union(emptyOrdered); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemDefaultGood.Union(emptyOrdered, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                var oneItemSpecificGood = Enumerable.Empty<int>().DefaultIfEmpty(4);
                try { emptyOrdered.Union(oneItemSpecificGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { emptyOrdered.Union(oneItemSpecificGood, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemSpecificGood.Union(emptyOrdered); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemSpecificGood.Union(emptyOrdered, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                var oneItemDefaultOrderedGood = Enumerable.Empty<int>().DefaultIfEmpty().OrderBy(x => x);
                try { emptyOrdered.Union(oneItemDefaultOrderedGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { emptyOrdered.Union(oneItemDefaultOrderedGood, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemDefaultOrderedGood.Union(emptyOrdered); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemDefaultOrderedGood.Union(emptyOrdered, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                var oneItemSpecificOrderedGood = Enumerable.Empty<int>().DefaultIfEmpty(4).OrderBy(x => x);
                try { emptyOrdered.Union(oneItemSpecificOrderedGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { emptyOrdered.Union(oneItemSpecificOrderedGood, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemSpecificOrderedGood.Union(emptyOrdered); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemSpecificOrderedGood.Union(emptyOrdered, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                Helper.ForEachEnumerableExpression(
                    emptyOrdered,
                    new int[0],
                    res => { },
                    @"(bad, good) =>
                      {
                        try { bad.Union(good); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""first"", exc.ParamName); }
                        try { bad.Union(good, new UnionTests._IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""first"", exc.ParamName); }
                        try { good.Union(bad); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""second"", exc.ParamName); }
                        try { good.Union(bad, new UnionTests._IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""second"", exc.ParamName); }

                        return Helper.NoCallValue;
                      }",
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );
            }

            // groupByDefault
            {
                var emptyGood = Enumerable.Empty<GroupingEnumerable<int, int>>();
                try { groupByDefault.Union(emptyGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { groupByDefault.Union(emptyGood, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { emptyGood.Union(groupByDefault); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { emptyGood.Union(groupByDefault, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                var emptyOrderedGood = Enumerable.Empty<GroupingEnumerable<int, int>>().OrderBy(x => x);
                try { groupByDefault.Union(emptyOrderedGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { groupByDefault.Union(emptyOrderedGood, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { emptyOrderedGood.Union(groupByDefault); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { emptyOrderedGood.Union(groupByDefault, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }


                var groupByDefaultGood = new[] { 1 }.GroupBy(x => x);
                try { groupByDefault.Union(groupByDefaultGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { groupByDefault.Union(groupByDefaultGood, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { groupByDefaultGood.Union(groupByDefault); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { groupByDefaultGood.Union(groupByDefault, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                var groupBySpecificGood = new[] { 1 }.GroupBy(x => x, new _IntComparer());
                try { groupByDefault.Union(groupBySpecificGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { groupByDefault.Union(groupBySpecificGood, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { groupBySpecificGood.Union(groupByDefault); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { groupBySpecificGood.Union(groupByDefault, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                var lookupGood = new[] { 1 }.ToLookup(x => x);
                try { groupByDefault.Union(lookupGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { groupByDefault.Union(lookupGood, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { lookupGood.Union(groupByDefault); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { lookupGood.Union(groupByDefault, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                // range is non-sensical

                var repeatGood = Enumerable.Repeat(groupByDefaultGood.First(), 1);
                try { groupByDefault.Union(repeatGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { groupByDefault.Union(repeatGood, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { repeatGood.Union(groupByDefault); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { repeatGood.Union(groupByDefault, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                // reverseRange is non-sensical

                Helper.ForEachEnumerableExpression(
                    groupByDefault,
                    new GroupingEnumerable<int, int>[0],
                    res => { },
                    @"(bad, good) =>
                      {
                        try { bad.Union(good); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""first"", exc.ParamName); }
                        try { bad.Union(good, new UnionTests._GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""first"", exc.ParamName); }
                        try { good.Union(bad); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""second"", exc.ParamName); }
                        try { good.Union(bad, new UnionTests._GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""second"", exc.ParamName); }

                        return Helper.NoCallValue;
                      }",
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );
            }

            // groupBySpecific
            {
                var emptyGood = Enumerable.Empty<GroupingEnumerable<int, int>>();
                try { groupBySpecific.Union(emptyGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { groupBySpecific.Union(emptyGood, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { emptyGood.Union(groupBySpecific); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { emptyGood.Union(groupBySpecific, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                var emptyOrderedGood = Enumerable.Empty<GroupingEnumerable<int, int>>().OrderBy(x => x);
                try { groupBySpecific.Union(emptyOrderedGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { groupBySpecific.Union(emptyOrderedGood, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { emptyOrderedGood.Union(groupBySpecific); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { emptyOrderedGood.Union(groupBySpecific, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }


                var groupByDefaultGood = new[] { 1 }.GroupBy(x => x);
                try { groupBySpecific.Union(groupByDefaultGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { groupBySpecific.Union(groupByDefaultGood, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { groupByDefaultGood.Union(groupBySpecific); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { groupByDefaultGood.Union(groupBySpecific, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                var groupBySpecificGood = new[] { 1 }.GroupBy(x => x, new _IntComparer());
                try { groupBySpecific.Union(groupBySpecificGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { groupBySpecific.Union(groupBySpecificGood, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { groupBySpecificGood.Union(groupBySpecific); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { groupBySpecificGood.Union(groupBySpecific, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                var lookupGood = new[] { 1 }.ToLookup(x => x);
                try { groupBySpecific.Union(lookupGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { groupBySpecific.Union(lookupGood, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { lookupGood.Union(groupBySpecific); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { lookupGood.Union(groupBySpecific, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                // range is non-sensical

                var repeatGood = Enumerable.Repeat(groupByDefaultGood.First(), 1);
                try { groupBySpecific.Union(repeatGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { groupBySpecific.Union(repeatGood, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { repeatGood.Union(groupBySpecific); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { repeatGood.Union(groupBySpecific, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                // reverseRange is non-sensical

                Helper.ForEachEnumerableExpression(
                    groupBySpecific,
                    new GroupingEnumerable<int, int>[0],
                    res => { },
                    @"(bad, good) =>
                      {
                        try { bad.Union(good); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""first"", exc.ParamName); }
                        try { bad.Union(good, new UnionTests._GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""first"", exc.ParamName); }
                        try { good.Union(bad); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""second"", exc.ParamName); }
                        try { good.Union(bad, new UnionTests._GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""second"", exc.ParamName); }

                        return Helper.NoCallValue;
                      }",
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );
            }

            // lookup
            {
                var emptyGood = Enumerable.Empty<GroupingEnumerable<int, int>>();
                try { lookup.Union(emptyGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { lookup.Union(emptyGood, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { emptyGood.Union(lookup); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { emptyGood.Union(lookup, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                var emptyOrderedGood = Enumerable.Empty<GroupingEnumerable<int, int>>().OrderBy(x => x);
                try { lookup.Union(emptyOrderedGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { lookup.Union(emptyOrderedGood, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { emptyOrderedGood.Union(lookup); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { emptyOrderedGood.Union(lookup, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }


                var groupByDefaultGood = new[] { 1 }.GroupBy(x => x);
                try { lookup.Union(groupByDefaultGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { lookup.Union(groupByDefaultGood, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { groupByDefaultGood.Union(lookup); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { groupByDefaultGood.Union(lookup, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                var groupBySpecificGood = new[] { 1 }.GroupBy(x => x, new _IntComparer());
                try { lookup.Union(groupBySpecificGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { lookup.Union(groupBySpecificGood, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { groupBySpecificGood.Union(lookup); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { groupBySpecificGood.Union(lookup, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                var lookupGood = new[] { 1 }.ToLookup(x => x);
                try { lookup.Union(lookupGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { lookup.Union(lookupGood, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { lookupGood.Union(lookup); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { lookupGood.Union(lookup, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                // range is non-sensical

                var repeatGood = Enumerable.Repeat(groupByDefaultGood.First(), 1);
                try { lookup.Union(repeatGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { lookup.Union(repeatGood, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { repeatGood.Union(lookup); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { repeatGood.Union(lookup, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                // reverseRange is non-sensical

                Helper.ForEachEnumerableExpression(
                    lookup,
                    new GroupingEnumerable<int, int>[0],
                    res => { },
                    @"(bad, good) =>
                      {
                        try { bad.Union(good); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""first"", exc.ParamName); }
                        try { bad.Union(good, new UnionTests._GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""first"", exc.ParamName); }
                        try { good.Union(bad); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""second"", exc.ParamName); }
                        try { good.Union(bad, new UnionTests._GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""second"", exc.ParamName); }

                        return Helper.NoCallValue;
                      }",
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );
            }

            // range
            {
                var emptyGood = Enumerable.Empty<int>();
                try { range.Union(emptyGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { range.Union(emptyGood, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { emptyGood.Union(range); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { emptyGood.Union(range, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                var emptyOrderedGood = Enumerable.Empty<int>().OrderBy(x => x);
                try { range.Union(emptyOrderedGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { range.Union(emptyOrderedGood, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { emptyOrderedGood.Union(range); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { emptyOrderedGood.Union(range, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                // groupby & lookup are non-sensical

                var rangeGood = Enumerable.Range(1, 1);
                try { range.Union(rangeGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { range.Union(rangeGood, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { rangeGood.Union(range); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { rangeGood.Union(range, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                var repeatGood = Enumerable.Repeat(1, 1);
                try { range.Union(repeatGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { range.Union(repeatGood, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { repeatGood.Union(range); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { repeatGood.Union(range, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                var reverseRangeGood = Enumerable.Range(1, 1).Reverse();
                try { range.Union(reverseRangeGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { range.Union(reverseRangeGood, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { reverseRangeGood.Union(range); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { reverseRangeGood.Union(range, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                var oneItemDefaultGood = Enumerable.Empty<int>().DefaultIfEmpty();
                try { range.Union(oneItemDefaultGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { range.Union(oneItemDefaultGood, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemDefaultGood.Union(range); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemDefaultGood.Union(range, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                var oneItemSpecificGood = Enumerable.Empty<int>().DefaultIfEmpty(4);
                try { range.Union(oneItemSpecificGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { range.Union(oneItemSpecificGood, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemSpecificGood.Union(range); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemSpecificGood.Union(range, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                var oneItemDefaultOrderedGood = Enumerable.Empty<int>().DefaultIfEmpty().OrderBy(x => x);
                try { range.Union(oneItemDefaultOrderedGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { range.Union(oneItemDefaultOrderedGood, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemDefaultOrderedGood.Union(range); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemDefaultOrderedGood.Union(range, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                var oneItemSpecificOrderedGood = Enumerable.Empty<int>().DefaultIfEmpty(4).OrderBy(x => x);
                try { range.Union(oneItemSpecificOrderedGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { range.Union(oneItemSpecificOrderedGood, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemSpecificOrderedGood.Union(range); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemSpecificOrderedGood.Union(range, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                Helper.ForEachEnumerableExpression(
                    range,
                    new int[0],
                    res => { },
                    @"(bad, good) =>
                      {
                        try { bad.Union(good); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""first"", exc.ParamName); }
                        try { bad.Union(good, new UnionTests._IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""first"", exc.ParamName); }
                        try { good.Union(bad); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""second"", exc.ParamName); }
                        try { good.Union(bad, new UnionTests._IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""second"", exc.ParamName); }

                        return Helper.NoCallValue;
                      }",
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );
            }

            // repeat
            {
                var emptyGood = Enumerable.Empty<int>();
                try { repeat.Union(emptyGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { repeat.Union(emptyGood, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { emptyGood.Union(repeat); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { emptyGood.Union(repeat, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                var emptyOrderedGood = Enumerable.Empty<int>().OrderBy(x => x);
                try { repeat.Union(emptyOrderedGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { repeat.Union(emptyOrderedGood, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { emptyOrderedGood.Union(repeat); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { emptyOrderedGood.Union(repeat, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                // groupby & lookup are non-sensical

                var rangeGood = Enumerable.Range(1, 1);
                try { repeat.Union(rangeGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { repeat.Union(rangeGood, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { rangeGood.Union(repeat); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { rangeGood.Union(repeat, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                var repeatGood = Enumerable.Repeat(1, 1);
                try { repeat.Union(repeatGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { repeat.Union(repeatGood, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { repeatGood.Union(repeat); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { repeatGood.Union(repeat, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                var reverseRangeGood = Enumerable.Range(1, 1).Reverse();
                try { repeat.Union(reverseRangeGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { repeat.Union(reverseRangeGood, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { reverseRangeGood.Union(repeat); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { reverseRangeGood.Union(repeat, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                var oneItemDefaultGood = Enumerable.Empty<int>().DefaultIfEmpty();
                try { repeat.Union(oneItemDefaultGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { repeat.Union(oneItemDefaultGood, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemDefaultGood.Union(repeat); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemDefaultGood.Union(repeat, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                var oneItemSpecificGood = Enumerable.Empty<int>().DefaultIfEmpty(4);
                try { repeat.Union(oneItemSpecificGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { repeat.Union(oneItemSpecificGood, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemSpecificGood.Union(repeat); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemSpecificGood.Union(repeat, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                var oneItemDefaultOrderedGood = Enumerable.Empty<int>().DefaultIfEmpty().OrderBy(x => x);
                try { repeat.Union(oneItemDefaultOrderedGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { repeat.Union(oneItemDefaultOrderedGood, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemDefaultOrderedGood.Union(repeat); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemDefaultOrderedGood.Union(repeat, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                var oneItemSpecificOrderedGood = Enumerable.Empty<int>().DefaultIfEmpty(4).OrderBy(x => x);
                try { repeat.Union(oneItemSpecificOrderedGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { repeat.Union(oneItemSpecificOrderedGood, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemSpecificOrderedGood.Union(repeat); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemSpecificOrderedGood.Union(repeat, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                Helper.ForEachEnumerableExpression(
                    repeat,
                    new int[0],
                    res => { },
                    @"(bad, good) =>
                      {
                        try { bad.Union(good); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""first"", exc.ParamName); }
                        try { bad.Union(good, new UnionTests._IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""first"", exc.ParamName); }
                        try { good.Union(bad); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""second"", exc.ParamName); }
                        try { good.Union(bad, new UnionTests._IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""second"", exc.ParamName); }

                        return Helper.NoCallValue;
                      }",
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );
            }

            // reverseRange
            {
                var emptyGood = Enumerable.Empty<int>();
                try { reverseRange.Union(emptyGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { reverseRange.Union(emptyGood, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { emptyGood.Union(reverseRange); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { emptyGood.Union(reverseRange, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                var emptyOrderedGood = Enumerable.Empty<int>().OrderBy(x => x);
                try { reverseRange.Union(emptyOrderedGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { reverseRange.Union(emptyOrderedGood, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { emptyOrderedGood.Union(reverseRange); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { emptyOrderedGood.Union(reverseRange, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                // groupby & lookup are non-sensical

                var rangeGood = Enumerable.Range(1, 1);
                try { reverseRange.Union(rangeGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { reverseRange.Union(rangeGood, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { rangeGood.Union(reverseRange); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { rangeGood.Union(reverseRange, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                var repeatGood = Enumerable.Repeat(1, 1);
                try { reverseRange.Union(repeatGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { reverseRange.Union(repeatGood, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { repeatGood.Union(reverseRange); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { repeatGood.Union(reverseRange, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                var reverseRangeGood = Enumerable.Range(1, 1).Reverse();
                try { reverseRange.Union(reverseRangeGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { reverseRange.Union(reverseRangeGood, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { reverseRangeGood.Union(reverseRange); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { reverseRangeGood.Union(reverseRange, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                var oneItemDefaultGood = Enumerable.Empty<int>().DefaultIfEmpty();
                try { reverseRange.Union(oneItemDefaultGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { reverseRange.Union(oneItemDefaultGood, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemDefaultGood.Union(reverseRange); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemDefaultGood.Union(reverseRange, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                var oneItemSpecificGood = Enumerable.Empty<int>().DefaultIfEmpty(4);
                try { reverseRange.Union(oneItemSpecificGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { reverseRange.Union(oneItemSpecificGood, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemSpecificGood.Union(reverseRange); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemSpecificGood.Union(reverseRange, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                var oneItemDefaultOrderedGood = Enumerable.Empty<int>().DefaultIfEmpty().OrderBy(x => x);
                try { reverseRange.Union(oneItemDefaultOrderedGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { reverseRange.Union(oneItemDefaultOrderedGood, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemDefaultOrderedGood.Union(reverseRange); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemDefaultOrderedGood.Union(reverseRange, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                var oneItemSpecificOrderedGood = Enumerable.Empty<int>().DefaultIfEmpty(4).OrderBy(x => x);
                try { reverseRange.Union(oneItemSpecificOrderedGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { reverseRange.Union(oneItemSpecificOrderedGood, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemSpecificOrderedGood.Union(reverseRange); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemSpecificOrderedGood.Union(reverseRange, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                Helper.ForEachEnumerableExpression(
                    reverseRange,
                    new int[0],
                    res => { },
                    @"(bad, good) =>
                      {
                        try { bad.Union(good); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""first"", exc.ParamName); }
                        try { bad.Union(good, new UnionTests._IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""first"", exc.ParamName); }
                        try { good.Union(bad); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""second"", exc.ParamName); }
                        try { good.Union(bad, new UnionTests._IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""second"", exc.ParamName); }

                        return Helper.NoCallValue;
                      }",
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );
            }

            // oneItemDefault
            {
                var emptyGood = Enumerable.Empty<int>();
                try { oneItemDefault.Union(emptyGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemDefault.Union(emptyGood, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { emptyGood.Union(oneItemDefault); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { emptyGood.Union(oneItemDefault, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                var emptyOrderedGood = Enumerable.Empty<int>().OrderBy(x => x);
                try { oneItemDefault.Union(emptyOrderedGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemDefault.Union(emptyOrderedGood, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { emptyOrderedGood.Union(oneItemDefault); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { emptyOrderedGood.Union(oneItemDefault, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                var oneItemDefaultGroupingBad = new OneItemDefaultEnumerable<GroupingEnumerable<int, int>>();

                var groupByDefaultGood = new[] { 1 }.GroupBy(x => x);
                try { oneItemDefaultGroupingBad.Union(groupByDefaultGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemDefaultGroupingBad.Union(groupByDefaultGood, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { groupByDefaultGood.Union(oneItemDefaultGroupingBad); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { groupByDefaultGood.Union(oneItemDefaultGroupingBad, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                var groupBySpecificGood = new[] { 1 }.GroupBy(x => x, new _IntComparer());
                try { oneItemDefaultGroupingBad.Union(groupBySpecificGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemDefaultGroupingBad.Union(groupBySpecificGood, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { groupBySpecificGood.Union(oneItemDefaultGroupingBad); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { groupBySpecificGood.Union(oneItemDefaultGroupingBad, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                var lookupGood = new[] { 1 }.ToLookup(x => x);
                try { oneItemDefaultGroupingBad.Union(lookupGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemDefaultGroupingBad.Union(lookupGood, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { lookupGood.Union(oneItemDefaultGroupingBad); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { lookupGood.Union(oneItemDefaultGroupingBad, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                var rangeGood = Enumerable.Range(1, 1);
                try { oneItemDefault.Union(rangeGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemDefault.Union(rangeGood, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { rangeGood.Union(oneItemDefault); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { rangeGood.Union(oneItemDefault, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                var repeatGood = Enumerable.Repeat(1, 1);
                try { oneItemDefault.Union(repeatGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemDefault.Union(repeatGood, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { repeatGood.Union(oneItemDefault); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { repeatGood.Union(oneItemDefault, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                var reverseRangeGood = Enumerable.Range(1, 1).Reverse();
                try { oneItemDefault.Union(reverseRangeGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemDefault.Union(reverseRangeGood, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { reverseRangeGood.Union(oneItemDefault); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { reverseRangeGood.Union(oneItemDefault, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                var oneItemDefaultGood = Enumerable.Empty<int>().DefaultIfEmpty();
                try { oneItemDefault.Union(oneItemDefaultGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemDefault.Union(oneItemDefaultGood, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemDefaultGood.Union(oneItemDefault); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemDefaultGood.Union(oneItemDefault, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                var oneItemSpecificGood = Enumerable.Empty<int>().DefaultIfEmpty(4);
                try { oneItemDefault.Union(oneItemSpecificGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemDefault.Union(oneItemSpecificGood, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemSpecificGood.Union(oneItemDefault); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemSpecificGood.Union(oneItemDefault, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                var oneItemDefaultOrderedGood = Enumerable.Empty<int>().DefaultIfEmpty().OrderBy(x => x);
                try { oneItemDefault.Union(oneItemDefaultOrderedGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemDefault.Union(oneItemDefaultOrderedGood, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemDefaultOrderedGood.Union(oneItemDefault); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemDefaultOrderedGood.Union(oneItemDefault, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                var oneItemSpecificOrderedGood = Enumerable.Empty<int>().DefaultIfEmpty(4).OrderBy(x => x);
                try { oneItemDefault.Union(oneItemSpecificOrderedGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemDefault.Union(oneItemSpecificOrderedGood, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemSpecificOrderedGood.Union(oneItemDefault); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemSpecificOrderedGood.Union(oneItemDefault, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                Helper.ForEachEnumerableExpression(
                    oneItemDefault,
                    new int[0],
                    res => { },
                    @"(bad, good) =>
                      {
                        try { bad.Union(good); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""first"", exc.ParamName); }
                        try { bad.Union(good, new UnionTests._IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""first"", exc.ParamName); }
                        try { good.Union(bad); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""second"", exc.ParamName); }
                        try { good.Union(bad, new UnionTests._IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""second"", exc.ParamName); }

                        return Helper.NoCallValue;
                      }",
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );
            }

            // oneItemSpecific
            {
                var emptyGood = Enumerable.Empty<int>();
                try { oneItemSpecific.Union(emptyGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemSpecific.Union(emptyGood, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { emptyGood.Union(oneItemSpecific); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { emptyGood.Union(oneItemSpecific, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                var emptyOrderedGood = Enumerable.Empty<int>().OrderBy(x => x);
                try { oneItemSpecific.Union(emptyOrderedGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemSpecific.Union(emptyOrderedGood, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { emptyOrderedGood.Union(oneItemSpecific); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { emptyOrderedGood.Union(oneItemSpecific, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                var oneItemSpecificGroupingBad = new OneItemSpecificEnumerable<GroupingEnumerable<int, int>>();

                var groupByDefaultGood = new[] { 1 }.GroupBy(x => x);
                try { oneItemSpecificGroupingBad.Union(groupByDefaultGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemSpecificGroupingBad.Union(groupByDefaultGood, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { groupByDefaultGood.Union(oneItemSpecificGroupingBad); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { groupByDefaultGood.Union(oneItemSpecificGroupingBad, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                var groupBySpecificGood = new[] { 1 }.GroupBy(x => x, new _IntComparer());
                try { oneItemSpecificGroupingBad.Union(groupBySpecificGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemSpecificGroupingBad.Union(groupBySpecificGood, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { groupBySpecificGood.Union(oneItemSpecificGroupingBad); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { groupBySpecificGood.Union(oneItemSpecificGroupingBad, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                var lookupGood = new[] { 1 }.ToLookup(x => x);
                try { oneItemSpecificGroupingBad.Union(lookupGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemSpecificGroupingBad.Union(lookupGood, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { lookupGood.Union(oneItemSpecificGroupingBad); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { lookupGood.Union(oneItemSpecificGroupingBad, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                var rangeGood = Enumerable.Range(1, 1);
                try { oneItemSpecific.Union(rangeGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemSpecific.Union(rangeGood, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { rangeGood.Union(oneItemSpecific); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { rangeGood.Union(oneItemSpecific, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                var repeatGood = Enumerable.Repeat(1, 1);
                try { oneItemSpecific.Union(repeatGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemSpecific.Union(repeatGood, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { repeatGood.Union(oneItemSpecific); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { repeatGood.Union(oneItemSpecific, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                var reverseRangeGood = Enumerable.Range(1, 1).Reverse();
                try { oneItemSpecific.Union(reverseRangeGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemSpecific.Union(reverseRangeGood, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { reverseRangeGood.Union(oneItemSpecific); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { reverseRangeGood.Union(oneItemSpecific, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                var oneItemDefaultGood = Enumerable.Empty<int>().DefaultIfEmpty();
                try { oneItemSpecific.Union(oneItemDefaultGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemSpecific.Union(oneItemDefaultGood, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemDefaultGood.Union(oneItemSpecific); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemDefaultGood.Union(oneItemSpecific, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                var oneItemSpecificGood = Enumerable.Empty<int>().DefaultIfEmpty(4);
                try { oneItemSpecific.Union(oneItemSpecificGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemSpecific.Union(oneItemSpecificGood, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemSpecificGood.Union(oneItemSpecific); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemSpecificGood.Union(oneItemSpecific, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                var oneItemDefaultOrderedGood = Enumerable.Empty<int>().DefaultIfEmpty().OrderBy(x => x);
                try { oneItemSpecific.Union(oneItemDefaultOrderedGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemSpecific.Union(oneItemDefaultOrderedGood, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemDefaultOrderedGood.Union(oneItemSpecific); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemDefaultOrderedGood.Union(oneItemSpecific, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                var oneItemSpecificOrderedGood = Enumerable.Empty<int>().DefaultIfEmpty(4).OrderBy(x => x);
                try { oneItemSpecific.Union(oneItemSpecificOrderedGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemSpecific.Union(oneItemSpecificOrderedGood, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemSpecificOrderedGood.Union(oneItemSpecific); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemSpecificOrderedGood.Union(oneItemSpecific, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                Helper.ForEachEnumerableExpression(
                    oneItemSpecific,
                    new int[0],
                    res => { },
                    @"(bad, good) =>
                      {
                        try { bad.Union(good); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""first"", exc.ParamName); }
                        try { bad.Union(good, new UnionTests._IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""first"", exc.ParamName); }
                        try { good.Union(bad); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""second"", exc.ParamName); }
                        try { good.Union(bad, new UnionTests._IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""second"", exc.ParamName); }

                        return Helper.NoCallValue;
                      }",
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );
            }

            // oneItemDefaultOrdered
            {
                var emptyGood = Enumerable.Empty<int>();
                try { oneItemDefaultOrdered.Union(emptyGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemDefaultOrdered.Union(emptyGood, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { emptyGood.Union(oneItemDefaultOrdered); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { emptyGood.Union(oneItemDefaultOrdered, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                var emptyOrderedGood = Enumerable.Empty<int>().OrderBy(x => x);
                try { oneItemDefaultOrdered.Union(emptyOrderedGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemDefaultOrdered.Union(emptyOrderedGood, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { emptyOrderedGood.Union(oneItemDefaultOrdered); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { emptyOrderedGood.Union(oneItemDefaultOrdered, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                var oneItemDefaultOrderedGroupingBad = new OneItemDefaultOrderedEnumerable<GroupingEnumerable<int, int>>();

                var groupByDefaultGood = new[] { 1 }.GroupBy(x => x);
                try { oneItemDefaultOrderedGroupingBad.Union(groupByDefaultGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemDefaultOrderedGroupingBad.Union(groupByDefaultGood, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { groupByDefaultGood.Union(oneItemDefaultOrderedGroupingBad); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { groupByDefaultGood.Union(oneItemDefaultOrderedGroupingBad, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                var groupBySpecificGood = new[] { 1 }.GroupBy(x => x, new _IntComparer());
                try { oneItemDefaultOrderedGroupingBad.Union(groupBySpecificGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemDefaultOrderedGroupingBad.Union(groupBySpecificGood, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { groupBySpecificGood.Union(oneItemDefaultOrderedGroupingBad); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { groupBySpecificGood.Union(oneItemDefaultOrderedGroupingBad, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                var lookupGood = new[] { 1 }.ToLookup(x => x);
                try { oneItemDefaultOrderedGroupingBad.Union(lookupGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemDefaultOrderedGroupingBad.Union(lookupGood, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { lookupGood.Union(oneItemDefaultOrderedGroupingBad); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { lookupGood.Union(oneItemDefaultOrderedGroupingBad, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                var rangeGood = Enumerable.Range(1, 1);
                try { oneItemDefaultOrdered.Union(rangeGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemDefaultOrdered.Union(rangeGood, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { rangeGood.Union(oneItemDefaultOrdered); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { rangeGood.Union(oneItemDefaultOrdered, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                var repeatGood = Enumerable.Repeat(1, 1);
                try { oneItemDefaultOrdered.Union(repeatGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemDefaultOrdered.Union(repeatGood, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { repeatGood.Union(oneItemDefaultOrdered); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { repeatGood.Union(oneItemDefaultOrdered, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                var reverseRangeGood = Enumerable.Range(1, 1).Reverse();
                try { oneItemDefaultOrdered.Union(reverseRangeGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemDefaultOrdered.Union(reverseRangeGood, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { reverseRangeGood.Union(oneItemDefaultOrdered); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { reverseRangeGood.Union(oneItemDefaultOrdered, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                var oneItemDefaultGood = Enumerable.Empty<int>().DefaultIfEmpty();
                try { oneItemDefaultOrdered.Union(oneItemDefaultGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemDefaultOrdered.Union(oneItemDefaultGood, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemDefaultGood.Union(oneItemDefaultOrdered); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemDefaultGood.Union(oneItemDefaultOrdered, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                var oneItemSpecificGood = Enumerable.Empty<int>().DefaultIfEmpty(4);
                try { oneItemDefaultOrdered.Union(oneItemSpecificGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemDefaultOrdered.Union(oneItemSpecificGood, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemSpecificGood.Union(oneItemDefaultOrdered); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemSpecificGood.Union(oneItemDefaultOrdered, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                var oneItemDefaultOrderedGood = Enumerable.Empty<int>().DefaultIfEmpty().OrderBy(x => x);
                try { oneItemDefaultOrdered.Union(oneItemDefaultOrderedGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemDefaultOrdered.Union(oneItemDefaultOrderedGood, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemDefaultOrderedGood.Union(oneItemDefaultOrdered); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemDefaultOrderedGood.Union(oneItemDefaultOrdered, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                var oneItemSpecificOrderedGood = Enumerable.Empty<int>().DefaultIfEmpty(4).OrderBy(x => x);
                try { oneItemDefaultOrdered.Union(oneItemSpecificOrderedGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemDefaultOrdered.Union(oneItemSpecificOrderedGood, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemSpecificOrderedGood.Union(oneItemDefaultOrdered); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemSpecificOrderedGood.Union(oneItemDefaultOrdered, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                Helper.ForEachEnumerableExpression(
                    oneItemDefaultOrdered,
                    new int[0],
                    res => { },
                    @"(bad, good) =>
                      {
                        try { bad.Union(good); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""first"", exc.ParamName); }
                        try { bad.Union(good, new UnionTests._IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""first"", exc.ParamName); }
                        try { good.Union(bad); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""second"", exc.ParamName); }
                        try { good.Union(bad, new UnionTests._IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""second"", exc.ParamName); }

                        return Helper.NoCallValue;
                      }",
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );
            }

            // oneItemSpecificOrdered
            {
                var emptyGood = Enumerable.Empty<int>();
                try { oneItemSpecificOrdered.Union(emptyGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemSpecificOrdered.Union(emptyGood, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { emptyGood.Union(oneItemSpecificOrdered); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { emptyGood.Union(oneItemSpecificOrdered, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                var emptyOrderedGood = Enumerable.Empty<int>().OrderBy(x => x);
                try { oneItemSpecificOrdered.Union(emptyOrderedGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemSpecificOrdered.Union(emptyOrderedGood, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { emptyOrderedGood.Union(oneItemSpecificOrdered); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { emptyOrderedGood.Union(oneItemSpecificOrdered, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                var oneItemSpecificOrderedGroupingBad = new OneItemSpecificOrderedEnumerable<GroupingEnumerable<int, int>>();

                var groupByDefaultGood = new[] { 1 }.GroupBy(x => x);
                try { oneItemSpecificOrderedGroupingBad.Union(groupByDefaultGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemSpecificOrderedGroupingBad.Union(groupByDefaultGood, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { groupByDefaultGood.Union(oneItemSpecificOrderedGroupingBad); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { groupByDefaultGood.Union(oneItemSpecificOrderedGroupingBad, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                var groupBySpecificGood = new[] { 1 }.GroupBy(x => x, new _IntComparer());
                try { oneItemSpecificOrderedGroupingBad.Union(groupBySpecificGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemSpecificOrderedGroupingBad.Union(groupBySpecificGood, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { groupBySpecificGood.Union(oneItemSpecificOrderedGroupingBad); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { groupBySpecificGood.Union(oneItemSpecificOrderedGroupingBad, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                var lookupGood = new[] { 1 }.ToLookup(x => x);
                try { oneItemSpecificOrderedGroupingBad.Union(lookupGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemSpecificOrderedGroupingBad.Union(lookupGood, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { lookupGood.Union(oneItemSpecificOrderedGroupingBad); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { lookupGood.Union(oneItemSpecificOrderedGroupingBad, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                var rangeGood = Enumerable.Range(1, 1);
                try { oneItemSpecificOrdered.Union(rangeGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemSpecificOrdered.Union(rangeGood, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { rangeGood.Union(oneItemSpecificOrdered); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { rangeGood.Union(oneItemSpecificOrdered, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                var repeatGood = Enumerable.Repeat(1, 1);
                try { oneItemSpecificOrdered.Union(repeatGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemSpecificOrdered.Union(repeatGood, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { repeatGood.Union(oneItemSpecificOrdered); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { repeatGood.Union(oneItemSpecificOrdered, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                var reverseRangeGood = Enumerable.Range(1, 1).Reverse();
                try { oneItemSpecificOrdered.Union(reverseRangeGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemSpecificOrdered.Union(reverseRangeGood, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { reverseRangeGood.Union(oneItemSpecificOrdered); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { reverseRangeGood.Union(oneItemSpecificOrdered, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                var oneItemDefaultGood = Enumerable.Empty<int>().DefaultIfEmpty();
                try { oneItemSpecificOrdered.Union(oneItemDefaultGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemSpecificOrdered.Union(oneItemDefaultGood, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemDefaultGood.Union(oneItemSpecificOrdered); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemDefaultGood.Union(oneItemSpecificOrdered, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                var oneItemSpecificGood = Enumerable.Empty<int>().DefaultIfEmpty(4);
                try { oneItemSpecificOrdered.Union(oneItemSpecificGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemSpecificOrdered.Union(oneItemSpecificGood, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemSpecificGood.Union(oneItemSpecificOrdered); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemSpecificGood.Union(oneItemSpecificOrdered, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                var oneItemDefaultOrderedGood = Enumerable.Empty<int>().DefaultIfEmpty().OrderBy(x => x);
                try { oneItemSpecificOrdered.Union(oneItemDefaultOrderedGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemSpecificOrdered.Union(oneItemDefaultOrderedGood, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemDefaultOrderedGood.Union(oneItemSpecificOrdered); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemDefaultOrderedGood.Union(oneItemSpecificOrdered, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                var oneItemSpecificOrderedGood = Enumerable.Empty<int>().DefaultIfEmpty(4).OrderBy(x => x);
                try { oneItemSpecificOrdered.Union(oneItemSpecificOrderedGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemSpecificOrdered.Union(oneItemSpecificOrderedGood, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemSpecificOrderedGood.Union(oneItemSpecificOrdered); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemSpecificOrderedGood.Union(oneItemSpecificOrdered, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                Helper.ForEachEnumerableExpression(
                    oneItemSpecificOrdered,
                    new int[0],
                    res => { },
                    @"(bad, good) =>
                      {
                        try { bad.Union(good); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""first"", exc.ParamName); }
                        try { bad.Union(good, new UnionTests._IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""first"", exc.ParamName); }
                        try { good.Union(bad); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""second"", exc.ParamName); }
                        try { good.Union(bad, new UnionTests._IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""second"", exc.ParamName); }

                        return Helper.NoCallValue;
                      }",
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );
            }
        }
    }
}
