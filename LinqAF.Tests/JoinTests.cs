using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using TestHelpers;
using System;
using System.Reflection;
using System.Text;

namespace LinqAF.Tests
{
    [TestClass]
    public class JoinTests
    {
        static void _InstanceExtensionNoOverlapImpl(int spread, int take)
        {
            Dictionary<MethodInfo, List<MethodInfo>> instOverlaps, extOverlaps;
            Helper.GetOverlappingMethods(typeof(Impl.IJoin<,,>), out instOverlaps, out extOverlaps, spread, take);

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

        static void _Universal(IEnumerable<Type> enums)
        {
            foreach (var e in enums)
            {
                System.Collections.Generic.List<string> missing;
                if (!Helper.Implements(e, typeof(Impl.IJoin<,,>), out missing))
                {
                    Assert.Fail($"{e.Name} does not implement IJoin ({string.Join(", ", missing)})");
                }
            }
        }

        [TestMethod]
        public void Universal1()
        {
            var enums = Helper.AllEnumerables().Where((_, ix) => ix % 2 == 0).AsEnumerable();
            _Universal(enums);
        }

        [TestMethod]
        public void Universal2()
        {
            var enums = Helper.AllEnumerables().Where((_, ix) => ix % 2 == 1).AsEnumerable();
            _Universal(enums);
        }

        [TestMethod]
        public void AcceptsAllEnumerables()
        {
            var missingSimple = new List<string>();
            var missingComparer = new List<string>();

            var ijoin = typeof(Impl.IJoin<,,>);
            var enums = Helper.AllEnumerables(includeWeirdOnes: false);
            foreach (var e in enums)
            {
                var simple =
                    ijoin
                        .GetMethods()
                        .Where(
                            m =>
                            {
                                var ps = Helper.GetParameters(m);
                                if (ps.Length != 4) return false;

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
                    ijoin
                        .GetMethods()
                        .Where(
                            m =>
                            {
                                var ps = Helper.GetParameters(m);
                                if (ps.Length != 5) return false;

                                var p = ps[0].ParameterType;

                                if (p.IsGenericType && !p.IsGenericTypeDefinition)
                                {
                                    p = Helper.GetGenericTypeDefinition(p);
                                }

                                var c = ps[4].ParameterType;
                                if (Helper.GetGenericTypeDefinition(c) != typeof(IEqualityComparer<>)) return false;

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
            var e1 = new[] { 1, 2, 3, 4 }.Select(i => i);
            var e2 = new[] { "a", "bb", "ccc", "ddd", "eeeeee" }.Select((s, ix) => s);
            var asJoin = e1.Join(e2, a => a, b => b.Length, (a, b) => a + b);

            Assert.IsTrue(asJoin.GetType().IsValueType);

            var res = new List<string>();
            foreach (var item in asJoin)
            {
                res.Add(item);
            }

            Assert.AreEqual(4, res.Count);
            Assert.AreEqual("1a", res[0]);
            Assert.AreEqual("2bb", res[1]);
            Assert.AreEqual("3ccc", res[2]);
            Assert.AreEqual("3ddd", res[3]);
        }

        public class _Comparer : IEqualityComparer<string>
        {
            public bool Equals(string x, string y)
            {
                return (x?.Length ?? 0) == (y?.Length ?? 0);
            }

            public int GetHashCode(string obj)
            {
                return obj.Length.GetHashCode();
            }
        }

        [TestMethod]
        public void Comparer()
        {
            var e1 = new[] { "aaaa", "bbb", "cc", "d", "" }.Select(i => i);
            var e2 = new[] { "f", "gg", "hhh", "iiii", "jjjjj" }.Select(i => i);
            var asJoin = e1.Join(e2, a => a, b => b, (a, b) => b + a, new _Comparer());

            var res = new List<string>();
            foreach (var item in asJoin)
            {
                res.Add(item);
            }

            Assert.AreEqual(4, res.Count);
            Assert.AreEqual("iiiiaaaa", res[0]);
            Assert.AreEqual("hhhbbb", res[1]);
            Assert.AreEqual("ggcc", res[2]);
            Assert.AreEqual("fd", res[3]);
        }

        [TestMethod]
        public void Chaining()
        {
            // default
            {
                Helper.ForEachEnumerableNoRetExpression(
                    new[] { 1, 2, 3 },
                    @"a =>
                        Helper.ForEachEnumerableExpression(
                            a,
                            new [] { ""foo"", ""hello"", ""f"" },
                            res =>
                            {
                                Assert.AreEqual(2, res.Count);
                                Assert.AreEqual(""1f"", res[0]);
                                Assert.AreEqual(""3foo"", res[1]);
                            },
                            ""(a, b) => a.Join(b, i => i, j => j.Length, (x, y) => x + y)"",
                            typeof(EmptyEnumerable<>),
                            typeof(EmptyOrderedEnumerable<>),
                            typeof(LookupDefaultEnumerable<,>),
                            typeof(LookupSpecificEnumerable<,>),
                            typeof(GroupByDefaultEnumerable<,,,,>),
                            typeof(GroupBySpecificEnumerable<,,,,>)
                        )",
                    typeof(EmptyEnumerable<>),
                    typeof(EmptyOrderedEnumerable<>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>)
                );
            }

            // specific
            {
                Helper.ForEachEnumerableNoRetExpression(
                    new[] { "bar", "world", "", "x" },
                    @"a =>
                        Helper.ForEachEnumerableExpression(
                            a,
                            new [] { ""foo"", ""hello"", ""f"" },
                            res =>
                            {
                                Assert.AreEqual(3, res.Count);
                                Assert.AreEqual(""barfoo"", res[0]);
                                Assert.AreEqual(""worldhello"", res[1]);
                                Assert.AreEqual(""xf"", res[2]);
                            },
                            ""(a, b) => a.Join(b, i => i, j => j, (x, y) => x + y, new JoinTests._Comparer())"",
                            typeof(EmptyEnumerable<>),
                            typeof(EmptyOrderedEnumerable<>),
                            typeof(LookupDefaultEnumerable<,>),
                            typeof(LookupSpecificEnumerable<,>),
                            typeof(GroupByDefaultEnumerable<,,,,>),
                            typeof(GroupBySpecificEnumerable<,,,,>)
                        )",
                    typeof(EmptyEnumerable<>),
                    typeof(EmptyOrderedEnumerable<>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>)
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
                Assert.IsFalse(empty.Join(empty, x => x, x => x, (a, b) => a + b).Any());
                Assert.IsFalse(empty.Join(empty, x => x, x => x, (a, b) => a + b, new _IntComparer()).Any());
                Assert.IsFalse(empty.Join(emptyOrdered, x => x, x => x, (a, b) => a + b).Any());
                Assert.IsFalse(empty.Join(emptyOrdered, x => x, x => x, (a, b) => a + b, new _IntComparer()).Any());
                Assert.IsFalse(empty.Join(range, x => x, x => x, (a, b) => a + b).Any());
                Assert.IsFalse(empty.Join(range, x => x, x => x, (a, b) => a + b, new _IntComparer()).Any());
                Assert.IsFalse(empty.Join(Enumerable.Repeat(1, 1), x => x, x => x, (a, b) => a + b).Any());
                Assert.IsFalse(empty.Join(Enumerable.Repeat(1, 1), x => x, x => x, (a, b) => a + b, new _IntComparer()).Any());
                Assert.IsFalse(empty.Join(reverseRange, x => x, x => x, (a, b) => a + b).Any());
                Assert.IsFalse(empty.Join(reverseRange, x => x, x => x, (a, b) => a + b, new _IntComparer()).Any());

                Assert.IsFalse(empty.Join(oneItemDefault, x => x, x => x, (a, b) => a + b).Any());
                Assert.IsFalse(empty.Join(oneItemDefault, x => x, x => x, (a, b) => a + b, new _IntComparer()).Any());
                Assert.IsFalse(empty.Join(oneItemSpecific, x => x, x => x, (a, b) => a + b).Any());
                Assert.IsFalse(empty.Join(oneItemSpecific, x => x, x => x, (a, b) => a + b, new _IntComparer()).Any());
                Assert.IsFalse(empty.Join(oneItemDefaultOrdered, x => x, x => x, (a, b) => a + b).Any());
                Assert.IsFalse(empty.Join(oneItemDefaultOrdered, x => x, x => x, (a, b) => a + b, new _IntComparer()).Any());
                Assert.IsFalse(empty.Join(oneItemSpecificOrdered, x => x, x => x, (a, b) => a + b).Any());
                Assert.IsFalse(empty.Join(oneItemSpecificOrdered, x => x, x => x, (a, b) => a + b, new _IntComparer()).Any());

                Helper.ForEachEnumerableExpression(
                    empty,
                    new int[0],
                    res => { },
                    @"(a, b) =>
                      {
                        Assert.IsFalse(a.Join(b, x => x, x => x, (c, d) => c + d).Any());
                        Assert.IsFalse(a.Join(b, x => x, x => x, (c, d) => c + d, new JoinTests._IntComparer()).Any());

                        return Helper.NoCallValue;
                      }",
                    typeof(EmptyEnumerable<>),
                    typeof(EmptyOrderedEnumerable<>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>)
                );
            }

            // emptyOrdered
            {
                Assert.IsFalse(emptyOrdered.Join(empty, x => x, x => x, (a, b) => a + b).Any());
                Assert.IsFalse(emptyOrdered.Join(empty, x => x, x => x, (a, b) => a + b, new _IntComparer()).Any());
                Assert.IsFalse(emptyOrdered.Join(emptyOrdered, x => x, x => x, (a, b) => a + b).Any());
                Assert.IsFalse(emptyOrdered.Join(emptyOrdered, x => x, x => x, (a, b) => a + b, new _IntComparer()).Any());
                Assert.IsFalse(emptyOrdered.Join(range, x => x, x => x, (a, b) => a + b).Any());
                Assert.IsFalse(emptyOrdered.Join(range, x => x, x => x, (a, b) => a + b, new _IntComparer()).Any());
                Assert.IsFalse(emptyOrdered.Join(Enumerable.Repeat(1, 1), x => x, x => x, (a, b) => a + b).Any());
                Assert.IsFalse(emptyOrdered.Join(Enumerable.Repeat(1, 1), x => x, x => x, (a, b) => a + b, new _IntComparer()).Any());
                Assert.IsFalse(emptyOrdered.Join(reverseRange, x => x, x => x, (a, b) => a + b).Any());
                Assert.IsFalse(emptyOrdered.Join(reverseRange, x => x, x => x, (a, b) => a + b, new _IntComparer()).Any());

                Assert.IsFalse(emptyOrdered.Join(oneItemDefault, x => x, x => x, (a, b) => a + b).Any());
                Assert.IsFalse(emptyOrdered.Join(oneItemDefault, x => x, x => x, (a, b) => a + b, new _IntComparer()).Any());
                Assert.IsFalse(emptyOrdered.Join(oneItemSpecific, x => x, x => x, (a, b) => a + b).Any());
                Assert.IsFalse(emptyOrdered.Join(oneItemSpecific, x => x, x => x, (a, b) => a + b, new _IntComparer()).Any());
                Assert.IsFalse(emptyOrdered.Join(oneItemDefaultOrdered, x => x, x => x, (a, b) => a + b).Any());
                Assert.IsFalse(emptyOrdered.Join(oneItemDefaultOrdered, x => x, x => x, (a, b) => a + b, new _IntComparer()).Any());
                Assert.IsFalse(emptyOrdered.Join(oneItemSpecificOrdered, x => x, x => x, (a, b) => a + b).Any());
                Assert.IsFalse(emptyOrdered.Join(oneItemSpecificOrdered, x => x, x => x, (a, b) => a + b, new _IntComparer()).Any());

                Helper.ForEachEnumerableExpression(
                    emptyOrdered,
                    new int[0],
                    res => { },
                    @"(a, b) =>
                      {
                        Assert.IsFalse(a.Join(b, x => x, x => x, (c, d) => c + d).Any());
                        Assert.IsFalse(a.Join(b, x => x, x => x, (c, d) => c + d, new JoinTests._IntComparer()).Any());

                        return Helper.NoCallValue;
                      }",
                    typeof(EmptyEnumerable<>),
                    typeof(EmptyOrderedEnumerable<>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>)
                );
            }

            // groupByDefault
            {
                Assert.IsFalse(groupByDefault.Join(Enumerable.Empty<GroupingEnumerable<int, int>>(), x => x, x => x, (a, b) => a).Any());
                Assert.IsFalse(groupByDefault.Join(Enumerable.Empty<GroupingEnumerable<int, int>>(), x => x, x => x, (a, b) => a, new _GroupingComparer<int>()).Any());
                Assert.IsFalse(groupByDefault.Join(Enumerable.Empty<GroupingEnumerable<int, int>>().OrderBy(x => x), x => x, x => x, (a, b) => a).Any());
                Assert.IsFalse(groupByDefault.Join(Enumerable.Empty<GroupingEnumerable<int, int>>().OrderBy(x => x), x => x, x => x, (a, b) => a, new _GroupingComparer<int>()).Any());
                Assert.IsFalse(groupByDefault.Join(groupByDefault, x => x, x => x, (a, b) => a).Any());
                Assert.IsTrue(groupByDefault.Join(groupByDefault, x => x, x => x, (a, b) => a, new _GroupingComparer<int>()).SequenceEqual(groupByDefault, new _GroupingComparer<int>()));
                var specific = new[] { 1, 1, 2, 2, 3, 3 }.GroupBy(x => x, new _IntComparer());
                Assert.IsFalse(groupByDefault.Join(specific, x => x, x => x, (a, b) => a).Any());
                Assert.IsTrue(groupByDefault.Join(specific, x => x, x => x, (a, b) => a, new _GroupingComparer<int>()).SequenceEqual(groupByDefault, new _GroupingComparer<int>()));
                Assert.IsFalse(groupByDefault.Join(lookup, x => x, x => x, (a, b) => a).Any());
                Assert.IsTrue(groupByDefault.Join(lookup, x => x, x => x, (a, b) => a, new _GroupingComparer<int>()).SequenceEqual(groupByDefault, new _GroupingComparer<int>()));
                Assert.IsFalse(groupByDefault.Join(groupByDefault.SkipWhile(f => false), x => x, x => x, (a, b) => a).Any());
                Assert.IsTrue(groupByDefault.Join(groupByDefault.SkipWhile(f => false), x => x, x => x, (a, b) => a, new _GroupingComparer<int>()).SequenceEqual(groupByDefault, new _GroupingComparer<int>()));
                Assert.IsFalse(groupByDefault.Join(groupByDefault.TakeWhile(f => true), x => x, x => x, (a, b) => a).Any());
                Assert.IsTrue(groupByDefault.Join(groupByDefault.TakeWhile(f => true), x => x, x => x, (a, b) => a, new _GroupingComparer<int>()).SequenceEqual(groupByDefault, new _GroupingComparer<int>()));
                Assert.IsFalse(groupByDefault.Join(groupByDefault.Where(f => true), x => x, x => x, (a, b) => a).Any());
                Assert.IsTrue(groupByDefault.Join(groupByDefault.Where(f => true), x => x, x => x, (a, b) => a, new _GroupingComparer<int>()).SequenceEqual(groupByDefault, new _GroupingComparer<int>()));
                Assert.IsFalse(groupByDefault.Join(groupByDefault.Where(f => true).Where(f => true), x => x, x => x, (a, b) => a).Any());
                Assert.IsTrue(groupByDefault.Join(groupByDefault.Where(f => true).Where(f => true), x => x, x => x, (a, b) => a, new _GroupingComparer<int>()).SequenceEqual(groupByDefault, new _GroupingComparer<int>()));

                var orderBy = new GroupingEnumerable<int, int>[0].OrderBy(x => x);
                Assert.IsFalse(groupByDefault.Join(orderBy, x => x, x => x, (c, d) => c).Any());
                Assert.IsFalse(groupByDefault.Join(orderBy, x => x, x => x, (c, d) => c, new _GroupingComparer<int>()).Any());

                // default doesn't make sense
                var oneItemSpecificGrouping = Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty(groupByDefault.First());
                Assert.IsTrue(groupByDefault.Join(oneItemSpecificGrouping, x => x, x => x, (a, b) => a).SequenceEqual(new GroupingEnumerable<int, int>[0], new _GroupingComparer<int>()));
                Assert.IsTrue(groupByDefault.Join(oneItemSpecificGrouping, x => x, x => x, (a, b) => a, new _GroupingComparer<int>()).SequenceEqual(new[] { groupByDefault.First() }, new _GroupingComparer<int>()));
                // defaultOrdered doesn't make sense
                var oneItemSpecificOrderedGrouping = oneItemSpecificGrouping.OrderBy(x => x);
                Assert.IsTrue(groupByDefault.Join(oneItemSpecificOrderedGrouping, x => x, x => x, (a, b) => a).SequenceEqual(new GroupingEnumerable<int, int>[0], new _GroupingComparer<int>()));
                Assert.IsTrue(groupByDefault.Join(oneItemSpecificOrderedGrouping, x => x, x => x, (a, b) => a, new _GroupingComparer<int>()).SequenceEqual(new[] { groupByDefault.First() }, new _GroupingComparer<int>()));

                Helper.ForEachEnumerableExpression(
                    groupByDefault,
                    new[] { groupByDefault.First() },
                    res => { },
                    @"(a, b) =>
                      {
                        Assert.IsFalse(a.Join(b, x => x, x => x, (c, d) => c).Any());
                        Assert.IsTrue(a.Join(b, x => x, x => x, (c, d) => c, new JoinTests._GroupingComparer<int>()).SequenceEqual(b, new JoinTests._GroupingComparer<int>()));

                        return Helper.NoCallValue;
                      }",
                    typeof(EmptyEnumerable<>),
                    typeof(EmptyOrderedEnumerable<>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(SkipWhileEnumerable<,,>),        // the instance provided by Helper isn't super condusive to GroupingEnumerable<>
                    typeof(TakeWhileEnumerable<,,>),        // the instance provided by Helper isn't super condusive to GroupingEnumerable<>
                    typeof(WhereEnumerable<,,>),            // the instance provided by Helper isn't super condusive to GroupingEnumerable<>
                    typeof(WhereWhereEnumerable<,,,>),      // the instance provided by Helper isn't super condusive to GroupingEnumerable<>
                    typeof(OrderByEnumerable<,,,,>)         // the instance provided by Helper isn't super condusive to GroupingEnumerable<>
                );
            }

            // groupBySpecific
            {
                Assert.IsFalse(groupBySpecific.Join(Enumerable.Empty<GroupingEnumerable<string, string>>(), x => x, x => x, (a, b) => a).Any());
                Assert.IsFalse(groupBySpecific.Join(Enumerable.Empty<GroupingEnumerable<string, string>>(), x => x, x => x, (a, b) => a, new _GroupingComparer<string>()).Any());
                Assert.IsFalse(groupBySpecific.Join(Enumerable.Empty<GroupingEnumerable<string, string>>().OrderBy(x => x), x => x, x => x, (a, b) => a).Any());
                Assert.IsFalse(groupBySpecific.Join(Enumerable.Empty<GroupingEnumerable<string, string>>().OrderBy(x => x), x => x, x => x, (a, b) => a, new _GroupingComparer<string>()).Any());
                var @default = new[] { "hello", "HELLO", "world", "WORLD", "foo", "FOO" }.GroupBy(x => x);
                Assert.IsFalse(groupBySpecific.Join(@default, x => x, x => x, (a, b) => a).Any());
                var defaultShouldMatch = new[] { groupBySpecific.ElementAt(0), groupBySpecific.ElementAt(0), groupBySpecific.ElementAt(1), groupBySpecific.ElementAt(1), groupBySpecific.ElementAt(2), groupBySpecific.ElementAt(2) };
                Assert.IsTrue(groupBySpecific.Join(@default, x => x.Key, x => x.Key, (a, b) => a, StringComparer.InvariantCultureIgnoreCase).SequenceEqual(defaultShouldMatch, new _GroupingComparer<string>()));
                Assert.IsFalse(groupBySpecific.Join(groupBySpecific, x => x, x => x, (a, b) => a).Any());
                Assert.IsTrue(groupBySpecific.Join(groupBySpecific, x => x, x => x, (a, b) => a, new _GroupingComparer<string>()).SequenceEqual(groupBySpecific, new _GroupingComparer<string>()));
                var stringLookup = new[] { "hello", "HELLO", "world", "WORLD", "foo", "FOO" }.ToLookup(x => x, StringComparer.InvariantCultureIgnoreCase);
                Assert.IsFalse(groupBySpecific.Join(stringLookup, x => x, x => x, (a, b) => a).Any());
                Assert.IsTrue(groupBySpecific.Join(stringLookup, x => x, x => x, (a, b) => a, new _GroupingComparer<string>()).SequenceEqual(groupBySpecific, new _GroupingComparer<string>()));
                Assert.IsFalse(groupBySpecific.Join(groupBySpecific.SkipWhile(f => false), x => x, x => x, (a, b) => a).Any());
                Assert.IsTrue(groupBySpecific.Join(groupBySpecific.SkipWhile(f => false), x => x, x => x, (a, b) => a, new _GroupingComparer<string>()).SequenceEqual(groupBySpecific, new _GroupingComparer<string>()));
                Assert.IsFalse(groupBySpecific.Join(groupBySpecific.TakeWhile(f => true), x => x, x => x, (a, b) => a).Any());
                Assert.IsTrue(groupBySpecific.Join(groupBySpecific.TakeWhile(f => true), x => x, x => x, (a, b) => a, new _GroupingComparer<string>()).SequenceEqual(groupBySpecific, new _GroupingComparer<string>()));
                Assert.IsFalse(groupBySpecific.Join(groupBySpecific.Where(f => true), x => x, x => x, (a, b) => a).Any());
                Assert.IsTrue(groupBySpecific.Join(groupBySpecific.Where(f => true), x => x, x => x, (a, b) => a, new _GroupingComparer<string>()).SequenceEqual(groupBySpecific, new _GroupingComparer<string>()));
                Assert.IsFalse(groupBySpecific.Join(groupBySpecific.Where(f => true).Where(f => true), x => x, x => x, (a, b) => a).Any());
                Assert.IsTrue(groupBySpecific.Join(groupBySpecific.Where(f => true).Where(f => true), x => x, x => x, (a, b) => a, new _GroupingComparer<string>()).SequenceEqual(groupBySpecific, new _GroupingComparer<string>()));

                var orderBy = new GroupingEnumerable<string, string>[0].OrderBy(x => x);
                Assert.IsFalse(groupBySpecific.Join(orderBy, x => x, x => x, (c, d) => c).Any());
                Assert.IsFalse(groupBySpecific.Join(orderBy, x => x, x => x, (c, d) => c, new _GroupingComparer<string>()).Any());

                // default doesn't make sense
                var oneItemSpecificGrouping = Enumerable.Empty<GroupingEnumerable<string, string>>().DefaultIfEmpty(groupBySpecific.First());
                Assert.IsTrue(groupBySpecific.Join(oneItemSpecificGrouping, x => x, x => x, (a, b) => a).SequenceEqual(new GroupingEnumerable<string, string>[0], new _GroupingComparer<string>()));
                Assert.IsTrue(groupBySpecific.Join(oneItemSpecificGrouping, x => x, x => x, (a, b) => a, new _GroupingComparer<string>()).SequenceEqual(new[] { groupBySpecific.First() }, new _GroupingComparer<string>()));
                // defaultOrdered doesn't make sense
                var oneItemSpecificOrderedGrouping = oneItemSpecificGrouping.OrderBy(x => x);
                Assert.IsTrue(groupBySpecific.Join(oneItemSpecificOrderedGrouping, x => x, x => x, (a, b) => a).SequenceEqual(new GroupingEnumerable<string, string>[0], new _GroupingComparer<string>()));
                Assert.IsTrue(groupBySpecific.Join(oneItemSpecificOrderedGrouping, x => x, x => x, (a, b) => a, new _GroupingComparer<string>()).SequenceEqual(new[] { groupBySpecific.First() }, new _GroupingComparer<string>()));

                Helper.ForEachEnumerableExpression(
                    groupBySpecific,
                    new[] { groupBySpecific.First() },
                    res => { },
                    @"(a, b) =>
                      {
                        Assert.IsFalse(a.Join(b, x => x, x => x, (c, d) => c).Any());
                        Assert.IsTrue(a.Join(b, x => x, x => x, (c, d) => c, new JoinTests._GroupingComparer<string>()).SequenceEqual(b, new JoinTests._GroupingComparer<string>()));

                        return Helper.NoCallValue;
                      }",
                    typeof(EmptyEnumerable<>),
                    typeof(EmptyOrderedEnumerable<>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(SkipWhileEnumerable<,,>),    // the instance provided by Helper isn't super condusive to GroupingEnumerable<>
                    typeof(TakeWhileEnumerable<,,>),    // the instance provided by Helper isn't super condusive to GroupingEnumerable<>
                    typeof(WhereEnumerable<,,>),        // the instance provided by Helper isn't super condusive to GroupingEnumerable<>
                    typeof(WhereWhereEnumerable<,,,>),  // the instance provided by Helper isn't super condusive to GroupingEnumerable<>
                    typeof(OrderByEnumerable<,,,,>)     // the instance provided by Helper isn't super condusive to GroupingEnumerable<>
                );
            }

            // lookup
            {
                Assert.IsFalse(lookup.Join(Enumerable.Empty<GroupingEnumerable<int, int>>(), x => x, x => x, (a, b) => a).Any());
                Assert.IsFalse(lookup.Join(Enumerable.Empty<GroupingEnumerable<int, int>>(), x => x, x => x, (a, b) => a, new _GroupingComparer<int>()).Any());
                Assert.IsFalse(lookup.Join(Enumerable.Empty<GroupingEnumerable<int, int>>().OrderBy(x => x), x => x, x => x, (a, b) => a).Any());
                Assert.IsFalse(lookup.Join(Enumerable.Empty<GroupingEnumerable<int, int>>().OrderBy(x => x), x => x, x => x, (a, b) => a, new _GroupingComparer<int>()).Any());
                Assert.IsFalse(lookup.Join(groupByDefault, x => x, x => x, (a, b) => a).Any());
                Assert.IsTrue(lookup.Join(groupByDefault, x => x, x => x, (a, b) => a, new _GroupingComparer<int>()).SequenceEqual(groupByDefault, new _GroupingComparer<int>()));
                var specific = new[] { 1, 1, 2, 2, 3, 3 }.GroupBy(x => x, new _IntComparer());
                Assert.IsFalse(lookup.Join(specific, x => x, x => x, (a, b) => a).Any());
                Assert.IsTrue(lookup.Join(specific, x => x, x => x, (a, b) => a, new _GroupingComparer<int>()).SequenceEqual(groupByDefault, new _GroupingComparer<int>()));
                Assert.IsFalse(lookup.Join(lookup, x => x, x => x, (a, b) => a).Any());
                Assert.IsTrue(lookup.Join(lookup, x => x, x => x, (a, b) => a, new _GroupingComparer<int>()).SequenceEqual(groupByDefault, new _GroupingComparer<int>()));
                Assert.IsFalse(lookup.Join(groupByDefault.SkipWhile(f => false), x => x, x => x, (a, b) => a).Any());
                Assert.IsTrue(lookup.Join(groupByDefault.SkipWhile(f => false), x => x, x => x, (a, b) => a, new _GroupingComparer<int>()).SequenceEqual(groupByDefault, new _GroupingComparer<int>()));
                Assert.IsFalse(lookup.Join(groupByDefault.TakeWhile(f => true), x => x, x => x, (a, b) => a).Any());
                Assert.IsTrue(lookup.Join(groupByDefault.TakeWhile(f => true), x => x, x => x, (a, b) => a, new _GroupingComparer<int>()).SequenceEqual(groupByDefault, new _GroupingComparer<int>()));
                Assert.IsFalse(lookup.Join(groupByDefault.Where(f => true), x => x, x => x, (a, b) => a).Any());
                Assert.IsTrue(lookup.Join(groupByDefault.Where(f => true), x => x, x => x, (a, b) => a, new _GroupingComparer<int>()).SequenceEqual(groupByDefault, new _GroupingComparer<int>()));
                Assert.IsFalse(lookup.Join(groupByDefault.Where(f => true).Where(f => true), x => x, x => x, (a, b) => a).Any());
                Assert.IsTrue(lookup.Join(groupByDefault.Where(f => true).Where(f => true), x => x, x => x, (a, b) => a, new _GroupingComparer<int>()).SequenceEqual(groupByDefault, new _GroupingComparer<int>()));

                var orderBy = new GroupingEnumerable<int, int>[0].OrderBy(x => x);
                Assert.IsFalse(lookup.Join(orderBy, x => x, x => x, (c, d) => c).Any());
                Assert.IsFalse(lookup.Join(orderBy, x => x, x => x, (c, d) => c, new _GroupingComparer<int>()).Any());

                // default doesn't make sense
                var oneItemSpecificGrouping = Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty(lookup.First());
                Assert.IsTrue(lookup.Join(oneItemSpecificGrouping, x => x, x => x, (a, b) => a).SequenceEqual(new GroupingEnumerable<int, int>[0], new _GroupingComparer<int>()));
                Assert.IsTrue(lookup.Join(oneItemSpecificGrouping, x => x, x => x, (a, b) => a, new _GroupingComparer<int>()).SequenceEqual(new[] { lookup.First() }, new _GroupingComparer<int>()));
                // defaultOrdered doesn't make sense
                var oneItemSpecificOrderedGrouping = oneItemSpecificGrouping.OrderBy(x => x);
                Assert.IsTrue(lookup.Join(oneItemSpecificOrderedGrouping, x => x, x => x, (a, b) => a).SequenceEqual(new GroupingEnumerable<int, int>[0], new _GroupingComparer<int>()));
                Assert.IsTrue(lookup.Join(oneItemSpecificOrderedGrouping, x => x, x => x, (a, b) => a, new _GroupingComparer<int>()).SequenceEqual(new[] { lookup.First() }, new _GroupingComparer<int>()));

                Helper.ForEachEnumerableExpression(
                    lookup,
                    new[] { lookup.First() },
                    res => { },
                    @"(a, b) =>
                      {
                        Assert.IsFalse(a.Join(b, x => x, x => x, (c, d) => c).Any());
                        Assert.IsTrue(a.Join(b, x => x, x => x, (c, d) => c, new JoinTests._GroupingComparer<int>()).SequenceEqual(b, new JoinTests._GroupingComparer<int>()));

                        return Helper.NoCallValue;
                      }",
                    typeof(EmptyEnumerable<>),
                    typeof(EmptyOrderedEnumerable<>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(SkipWhileEnumerable<,,>),    // the instance provided by Helper isn't super condusive to GroupingEnumerable<>
                    typeof(TakeWhileEnumerable<,,>),    // the instance provided by Helper isn't super condusive to GroupingEnumerable<>
                    typeof(WhereEnumerable<,,>),        // the instance provided by Helper isn't super condusive to GroupingEnumerable<>
                    typeof(WhereWhereEnumerable<,,,>),  // the instance provided by Helper isn't super condusive to GroupingEnumerable<>
                    typeof(OrderByEnumerable<,,,,>)     // the instance provided by Helper isn't super condusive to GroupingEnumerable<>
                );
            }

            // range
            {
                Assert.IsFalse(range.Join(empty, x => x, x => x, (a, b) => a + b).Any());
                Assert.IsFalse(range.Join(empty, x => x, x => x, (a, b) => a + b, new _IntComparer()).Any());
                Assert.IsFalse(range.Join(emptyOrdered, x => x, x => x, (a, b) => a + b).Any());
                Assert.IsFalse(range.Join(emptyOrdered, x => x, x => x, (a, b) => a + b, new _IntComparer()).Any());
                Assert.IsTrue(range.Join(range, x => x, x => x, (a, b) => a + b).SequenceEqual(new[] { 2, 4, 6, 8, 10 }));
                Assert.IsTrue(range.Join(range, x => x, x => x, (a, b) => a + b, new _IntComparer()).SequenceEqual(new[] { 2, 4, 6, 8, 10 }));
                Assert.IsTrue(range.Join(Enumerable.Repeat(1, 3), x => x, x => x, (a, b) => a + b).SequenceEqual(new[] { 2, 2, 2 }));
                Assert.IsTrue(range.Join(Enumerable.Repeat(1, 3), x => x, x => x, (a, b) => a + b, new _IntComparer()).SequenceEqual(new[] { 2, 2, 2 }));
                Assert.IsTrue(range.Join(reverseRange, x => x, x => x, (a, b) => a + b).SequenceEqual(new[] { 2, 4, 6, 8, 10 }));
                Assert.IsTrue(range.Join(reverseRange, x => x, x => x, (a, b) => a + b, new _IntComparer()).SequenceEqual(new[] { 2, 4, 6, 8, 10 }));

                Assert.IsFalse(range.Join(oneItemDefault, x => x, x => x, (a, b) => a + b).Any());
                Assert.IsFalse(range.Join(oneItemDefault, x => x, x => x, (a, b) => a + b, new _IntComparer()).Any());
                Assert.IsTrue(range.Join(oneItemSpecific, x => x, x => x, (a, b) => a + b).SequenceEqual(new[] { 8 }));
                Assert.IsTrue(range.Join(oneItemSpecific, x => x, x => x, (a, b) => a + b, new _IntComparer()).SequenceEqual(new[] { 8 }));
                Assert.IsFalse(range.Join(oneItemDefaultOrdered, x => x, x => x, (a, b) => a + b).Any());
                Assert.IsFalse(range.Join(oneItemDefaultOrdered, x => x, x => x, (a, b) => a + b, new _IntComparer()).Any());
                Assert.IsTrue(range.Join(oneItemSpecificOrdered, x => x, x => x, (a, b) => a + b).SequenceEqual(new[] { 8 }));
                Assert.IsTrue(range.Join(oneItemSpecificOrdered, x => x, x => x, (a, b) => a + b, new _IntComparer()).SequenceEqual(new[] { 8 }));

                Helper.ForEachEnumerableExpression(
                    range,
                    range.ToArray(),
                    res => { },
                    @"(a, b) =>
                      {
                        Assert.IsTrue(a.Join(b, x => x, x => x, (c, d) => c + d).SequenceEqual(new[] { 2, 4, 6, 8, 10 }));
                        Assert.IsTrue(a.Join(b, x => x, x => x, (c, d) => c + d, new JoinTests._IntComparer()).SequenceEqual(new[] { 2, 4, 6, 8, 10 }));

                        return Helper.NoCallValue;
                      }",
                    typeof(EmptyEnumerable<>),
                    typeof(EmptyOrderedEnumerable<>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>)
                );
            }

            // repeat
            {
                Assert.IsFalse(repeat.Join(Enumerable.Empty<string>(), x => x, x => x, (a, b) => a + b).Any());
                Assert.IsFalse(repeat.Join(Enumerable.Empty<string>(), x => x, x => x, (a, b) => a + b, StringComparer.InvariantCultureIgnoreCase).Any());
                Assert.IsFalse(repeat.Join(Enumerable.Empty<string>().OrderBy(x => x), x => x, x => x, (a, b) => a + b).Any());
                Assert.IsFalse(repeat.Join(Enumerable.Empty<string>().OrderBy(x => x), x => x, x => x, (a, b) => a + b, StringComparer.InvariantCultureIgnoreCase).Any());
                Assert.IsTrue(repeat.Join(repeat, x => x, x => x, (a, b) => a + b).SequenceEqual(new[] { "foofoo", "foofoo", "foofoo", "foofoo", "foofoo", "foofoo", "foofoo", "foofoo", "foofoo", "foofoo", "foofoo", "foofoo", "foofoo", "foofoo", "foofoo", "foofoo", "foofoo", "foofoo", "foofoo", "foofoo", "foofoo", "foofoo", "foofoo", "foofoo", "foofoo" }));
                Assert.IsTrue(repeat.Join(repeat, x => x, x => x, (a, b) => a + b, StringComparer.InvariantCultureIgnoreCase).SequenceEqual(new[] { "foofoo", "foofoo", "foofoo", "foofoo", "foofoo", "foofoo", "foofoo", "foofoo", "foofoo", "foofoo", "foofoo", "foofoo", "foofoo", "foofoo", "foofoo", "foofoo", "foofoo", "foofoo", "foofoo", "foofoo", "foofoo", "foofoo", "foofoo", "foofoo", "foofoo" }));

                var repeatInt = Enumerable.Repeat(4, 1);
                Assert.IsFalse(repeatInt.Join(oneItemDefault, x => x, x => x, (a, b) => a + b).Any());
                Assert.IsFalse(repeatInt.Join(oneItemDefault, x => x, x => x, (a, b) => a + b, new _IntComparer()).Any());
                Assert.IsTrue(repeatInt.Join(oneItemSpecific, x => x, x => x, (a, b) => a + b).SequenceEqual(new[] { 8 }));
                Assert.IsTrue(repeatInt.Join(oneItemSpecific, x => x, x => x, (a, b) => a + b, new _IntComparer()).SequenceEqual(new[] { 8 }));
                Assert.IsFalse(repeatInt.Join(oneItemDefaultOrdered, x => x, x => x, (a, b) => a + b).Any());
                Assert.IsFalse(repeatInt.Join(oneItemDefaultOrdered, x => x, x => x, (a, b) => a + b, new _IntComparer()).Any());
                Assert.IsTrue(repeatInt.Join(oneItemSpecificOrdered, x => x, x => x, (a, b) => a + b).SequenceEqual(new[] { 8 }));
                Assert.IsTrue(repeatInt.Join(oneItemSpecificOrdered, x => x, x => x, (a, b) => a + b, new _IntComparer()).SequenceEqual(new[] { 8 }));

                Helper.ForEachEnumerableExpression(
                    repeat,
                    new[] { "foo" },
                    res => { },
                    @"(a, b) =>
                      {
                        Assert.IsTrue(a.Join(b, x => x, x => x, (c, d) => c + d).SequenceEqual(new[] { ""foofoo"", ""foofoo"", ""foofoo"", ""foofoo"", ""foofoo"" }));
                        Assert.IsTrue(a.Join(b, x => x, x => x, (c, d) => c + d, StringComparer.InvariantCultureIgnoreCase).SequenceEqual(new[] { ""foofoo"", ""foofoo"", ""foofoo"", ""foofoo"", ""foofoo"" }));

                        return Helper.NoCallValue;
                      }",
                    typeof(EmptyEnumerable<>),
                    typeof(EmptyOrderedEnumerable<>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>)
                );
            }

            // reverseRange
            {
                Assert.IsFalse(reverseRange.Join(empty, x => x, x => x, (a, b) => a + b).Any());
                Assert.IsFalse(reverseRange.Join(empty, x => x, x => x, (a, b) => a + b, new _IntComparer()).Any());
                Assert.IsFalse(reverseRange.Join(emptyOrdered, x => x, x => x, (a, b) => a + b).Any());
                Assert.IsFalse(reverseRange.Join(emptyOrdered, x => x, x => x, (a, b) => a + b, new _IntComparer()).Any());
                Assert.IsTrue(reverseRange.Join(range, x => x, x => x, (a, b) => a + b).SequenceEqual(new[] { 10, 8, 6, 4, 2 }));
                Assert.IsTrue(reverseRange.Join(range, x => x, x => x, (a, b) => a + b, new _IntComparer()).SequenceEqual(new[] { 10, 8, 6, 4, 2 }));
                Assert.IsTrue(reverseRange.Join(Enumerable.Repeat(1, 3), x => x, x => x, (a, b) => a + b).SequenceEqual(new[] { 2, 2, 2 }));
                Assert.IsTrue(reverseRange.Join(Enumerable.Repeat(1, 3), x => x, x => x, (a, b) => a + b, new _IntComparer()).SequenceEqual(new[] { 2, 2, 2 }));
                Assert.IsTrue(reverseRange.Join(reverseRange, x => x, x => x, (a, b) => a + b).SequenceEqual(new[] { 10, 8, 6, 4, 2 }));
                Assert.IsTrue(reverseRange.Join(reverseRange, x => x, x => x, (a, b) => a + b, new _IntComparer()).SequenceEqual(new[] { 10, 8, 6, 4, 2 }));

                Assert.IsFalse(reverseRange.Join(oneItemDefault, x => x, x => x, (a, b) => a + b).Any());
                Assert.IsFalse(reverseRange.Join(oneItemDefault, x => x, x => x, (a, b) => a + b, new _IntComparer()).Any());
                Assert.IsTrue(reverseRange.Join(oneItemSpecific, x => x, x => x, (a, b) => a + b).SequenceEqual(new[] { 8 }));
                Assert.IsTrue(reverseRange.Join(oneItemSpecific, x => x, x => x, (a, b) => a + b, new _IntComparer()).SequenceEqual(new[] { 8 }));
                Assert.IsFalse(reverseRange.Join(oneItemDefaultOrdered, x => x, x => x, (a, b) => a + b).Any());
                Assert.IsFalse(reverseRange.Join(oneItemDefaultOrdered, x => x, x => x, (a, b) => a + b, new _IntComparer()).Any());
                Assert.IsTrue(reverseRange.Join(oneItemSpecificOrdered, x => x, x => x, (a, b) => a + b).SequenceEqual(new[] { 8 }));
                Assert.IsTrue(reverseRange.Join(oneItemSpecificOrdered, x => x, x => x, (a, b) => a + b, new _IntComparer()).SequenceEqual(new[] { 8 }));

                Helper.ForEachEnumerableExpression(
                    reverseRange,
                    reverseRange.ToArray(),
                    res => { },
                    @"(a, b) =>
                      {
                        Assert.IsTrue(a.Join(b, x => x, x => x, (c, d) => c + d).SequenceEqual(new[] { 10, 8, 6, 4, 2 }));
                        Assert.IsTrue(a.Join(b, x => x, x => x, (c, d) => c + d, new JoinTests._IntComparer()).SequenceEqual(new[] { 10, 8, 6, 4, 2 }));

                        return Helper.NoCallValue;
                      }",
                    typeof(EmptyEnumerable<>),
                    typeof(EmptyOrderedEnumerable<>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>)
                );
            }

            // oneItemDefault
            {
                Assert.IsFalse(oneItemDefault.Join(empty, x => x, x => x, (a, b) => a + b).Any());
                Assert.IsFalse(oneItemDefault.Join(empty, x => x, x => x, (a, b) => a + b, new _IntComparer()).Any());
                Assert.IsFalse(oneItemDefault.Join(emptyOrdered, x => x, x => x, (a, b) => a + b).Any());
                Assert.IsFalse(oneItemDefault.Join(emptyOrdered, x => x, x => x, (a, b) => a + b, new _IntComparer()).Any());
                Assert.IsTrue(oneItemDefault.Join(range, x => x, x => x, (a, b) => a + b).SequenceEqual(new int[0]));
                Assert.IsTrue(oneItemDefault.Join(range, x => x, x => x, (a, b) => a + b, new _IntComparer()).SequenceEqual(new int[0]));
                Assert.IsTrue(oneItemDefault.Join(Enumerable.Repeat(1, 3), x => x, x => x, (a, b) => a + b).SequenceEqual(new int[0]));
                Assert.IsTrue(oneItemDefault.Join(Enumerable.Repeat(1, 3), x => x, x => x, (a, b) => a + b, new _IntComparer()).SequenceEqual(new int[0]));
                Assert.IsTrue(oneItemDefault.Join(reverseRange, x => x, x => x, (a, b) => a + b).SequenceEqual(new int[0]));
                Assert.IsTrue(oneItemDefault.Join(reverseRange, x => x, x => x, (a, b) => a + b, new _IntComparer()).SequenceEqual(new int[0]));

                Assert.IsTrue(oneItemDefault.Join(oneItemDefault, x => x, x => x, (a, b) => a + b).SequenceEqual(new[] { 0 }));
                Assert.IsTrue(oneItemDefault.Join(oneItemDefault, x => x, x => x, (a, b) => a + b, new _IntComparer()).SequenceEqual(new[] { 0 }));
                Assert.IsTrue(oneItemDefault.Join(oneItemSpecific, x => x, x => x, (a, b) => a + b).SequenceEqual(new int[0]));
                Assert.IsTrue(oneItemDefault.Join(oneItemSpecific, x => x, x => x, (a, b) => a + b, new _IntComparer()).SequenceEqual(new int[0]));
                Assert.IsTrue(oneItemDefault.Join(oneItemDefaultOrdered, x => x, x => x, (a, b) => a + b).SequenceEqual(new[] { 0 }));
                Assert.IsTrue(oneItemDefault.Join(oneItemDefaultOrdered, x => x, x => x, (a, b) => a + b, new _IntComparer()).SequenceEqual(new[] { 0 }));
                Assert.IsTrue(oneItemDefault.Join(oneItemSpecificOrdered, x => x, x => x, (a, b) => a + b).SequenceEqual(new int[0]));
                Assert.IsTrue(oneItemDefault.Join(oneItemSpecificOrdered, x => x, x => x, (a, b) => a + b, new _IntComparer()).SequenceEqual(new int[0]));

                Helper.ForEachEnumerableExpression(
                    oneItemDefault,
                    new[] { 1 },
                    res => { },
                    @"(a, b) =>
                      {
                        Assert.IsTrue(a.Join(b, x => x, x => x, (c, d) => c + d).SequenceEqual(new int[0]));
                        Assert.IsTrue(a.Join(b, x => x, x => x, (c, d) => c + d, new JoinTests._IntComparer()).SequenceEqual(new int[0]));

                        return Helper.NoCallValue;
                      }",
                    typeof(EmptyEnumerable<>),
                    typeof(EmptyOrderedEnumerable<>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>)
                );
            }

            // oneItemSpecific
            {
                Assert.IsFalse(oneItemSpecific.Join(empty, x => x, x => x, (a, b) => a + b).Any());
                Assert.IsFalse(oneItemSpecific.Join(empty, x => x, x => x, (a, b) => a + b, new _IntComparer()).Any());
                Assert.IsFalse(oneItemSpecific.Join(emptyOrdered, x => x, x => x, (a, b) => a + b).Any());
                Assert.IsFalse(oneItemSpecific.Join(emptyOrdered, x => x, x => x, (a, b) => a + b, new _IntComparer()).Any());
                Assert.IsTrue(oneItemSpecific.Join(range, x => x, x => x, (a, b) => a + b).SequenceEqual(new[] { 8 }));
                Assert.IsTrue(oneItemSpecific.Join(range, x => x, x => x, (a, b) => a + b, new _IntComparer()).SequenceEqual(new[] { 8 }));
                Assert.IsTrue(oneItemSpecific.Join(Enumerable.Repeat(1, 3), x => x, x => x, (a, b) => a + b).SequenceEqual(new int[0]));
                Assert.IsTrue(oneItemSpecific.Join(Enumerable.Repeat(1, 3), x => x, x => x, (a, b) => a + b, new _IntComparer()).SequenceEqual(new int[0]));
                Assert.IsTrue(oneItemSpecific.Join(reverseRange, x => x, x => x, (a, b) => a + b).SequenceEqual(new[] { 8 }));
                Assert.IsTrue(oneItemSpecific.Join(reverseRange, x => x, x => x, (a, b) => a + b, new _IntComparer()).SequenceEqual(new[] { 8 }));

                Assert.IsTrue(oneItemSpecific.Join(oneItemDefault, x => x, x => x, (a, b) => a + b).SequenceEqual(new int[0]));
                Assert.IsTrue(oneItemSpecific.Join(oneItemDefault, x => x, x => x, (a, b) => a + b, new _IntComparer()).SequenceEqual(new int[0]));
                Assert.IsTrue(oneItemSpecific.Join(oneItemSpecific, x => x, x => x, (a, b) => a + b).SequenceEqual(new[] { 8 }));
                Assert.IsTrue(oneItemSpecific.Join(oneItemSpecific, x => x, x => x, (a, b) => a + b, new _IntComparer()).SequenceEqual(new[] { 8 }));
                Assert.IsTrue(oneItemSpecific.Join(oneItemDefaultOrdered, x => x, x => x, (a, b) => a + b).SequenceEqual(new int[0]));
                Assert.IsTrue(oneItemSpecific.Join(oneItemDefaultOrdered, x => x, x => x, (a, b) => a + b, new _IntComparer()).SequenceEqual(new int[0]));
                Assert.IsTrue(oneItemSpecific.Join(oneItemSpecificOrdered, x => x, x => x, (a, b) => a + b).SequenceEqual(new[] { 8 }));
                Assert.IsTrue(oneItemSpecific.Join(oneItemSpecificOrdered, x => x, x => x, (a, b) => a + b, new _IntComparer()).SequenceEqual(new[] { 8 }));

                Helper.ForEachEnumerableExpression(
                    oneItemSpecific,
                    new[] { 1 },
                    res => { },
                    @"(a, b) =>
                      {
                        Assert.IsTrue(a.Join(b, x => x, x => x, (c, d) => c + d).SequenceEqual(new int[0]));
                        Assert.IsTrue(a.Join(b, x => x, x => x, (c, d) => c + d, new JoinTests._IntComparer()).SequenceEqual(new int[0]));

                        return Helper.NoCallValue;
                      }",
                    typeof(EmptyEnumerable<>),
                    typeof(EmptyOrderedEnumerable<>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>)
                );
            }

            // oneItemDefaultOrdered
            {
                Assert.IsFalse(oneItemDefaultOrdered.Join(empty, x => x, x => x, (a, b) => a + b).Any());
                Assert.IsFalse(oneItemDefaultOrdered.Join(empty, x => x, x => x, (a, b) => a + b, new _IntComparer()).Any());
                Assert.IsFalse(oneItemDefaultOrdered.Join(emptyOrdered, x => x, x => x, (a, b) => a + b).Any());
                Assert.IsFalse(oneItemDefaultOrdered.Join(emptyOrdered, x => x, x => x, (a, b) => a + b, new _IntComparer()).Any());
                Assert.IsTrue(oneItemDefaultOrdered.Join(range, x => x, x => x, (a, b) => a + b).SequenceEqual(new int[0]));
                Assert.IsTrue(oneItemDefaultOrdered.Join(range, x => x, x => x, (a, b) => a + b, new _IntComparer()).SequenceEqual(new int[0]));
                Assert.IsTrue(oneItemDefaultOrdered.Join(Enumerable.Repeat(1, 3), x => x, x => x, (a, b) => a + b).SequenceEqual(new int[0]));
                Assert.IsTrue(oneItemDefaultOrdered.Join(Enumerable.Repeat(1, 3), x => x, x => x, (a, b) => a + b, new _IntComparer()).SequenceEqual(new int[0]));
                Assert.IsTrue(oneItemDefaultOrdered.Join(reverseRange, x => x, x => x, (a, b) => a + b).SequenceEqual(new int[0]));
                Assert.IsTrue(oneItemDefaultOrdered.Join(reverseRange, x => x, x => x, (a, b) => a + b, new _IntComparer()).SequenceEqual(new int[0]));

                Assert.IsTrue(oneItemDefaultOrdered.Join(oneItemDefault, x => x, x => x, (a, b) => a + b).SequenceEqual(new[] { 0 }));
                Assert.IsTrue(oneItemDefaultOrdered.Join(oneItemDefault, x => x, x => x, (a, b) => a + b, new _IntComparer()).SequenceEqual(new[] { 0 }));
                Assert.IsTrue(oneItemDefaultOrdered.Join(oneItemSpecific, x => x, x => x, (a, b) => a + b).SequenceEqual(new int[0]));
                Assert.IsTrue(oneItemDefaultOrdered.Join(oneItemSpecific, x => x, x => x, (a, b) => a + b, new _IntComparer()).SequenceEqual(new int[0]));
                Assert.IsTrue(oneItemDefaultOrdered.Join(oneItemDefaultOrdered, x => x, x => x, (a, b) => a + b).SequenceEqual(new[] { 0 }));
                Assert.IsTrue(oneItemDefaultOrdered.Join(oneItemDefaultOrdered, x => x, x => x, (a, b) => a + b, new _IntComparer()).SequenceEqual(new[] { 0 }));
                Assert.IsTrue(oneItemDefaultOrdered.Join(oneItemSpecificOrdered, x => x, x => x, (a, b) => a + b).SequenceEqual(new int[0]));
                Assert.IsTrue(oneItemDefaultOrdered.Join(oneItemSpecificOrdered, x => x, x => x, (a, b) => a + b, new _IntComparer()).SequenceEqual(new int[0]));

                Helper.ForEachEnumerableExpression(
                    oneItemDefaultOrdered,
                    new[] { 1 },
                    res => { },
                    @"(a, b) =>
                      {
                        Assert.IsTrue(a.Join(b, x => x, x => x, (c, d) => c + d).SequenceEqual(new int[0]));
                        Assert.IsTrue(a.Join(b, x => x, x => x, (c, d) => c + d, new JoinTests._IntComparer()).SequenceEqual(new int[0]));

                        return Helper.NoCallValue;
                      }",
                    typeof(EmptyEnumerable<>),
                    typeof(EmptyOrderedEnumerable<>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>)
                );
            }

            // oneItemSpecificOrdered
            {
                Assert.IsFalse(oneItemSpecificOrdered.Join(empty, x => x, x => x, (a, b) => a + b).Any());
                Assert.IsFalse(oneItemSpecificOrdered.Join(empty, x => x, x => x, (a, b) => a + b, new _IntComparer()).Any());
                Assert.IsFalse(oneItemSpecificOrdered.Join(emptyOrdered, x => x, x => x, (a, b) => a + b).Any());
                Assert.IsFalse(oneItemSpecificOrdered.Join(emptyOrdered, x => x, x => x, (a, b) => a + b, new _IntComparer()).Any());
                Assert.IsTrue(oneItemSpecificOrdered.Join(range, x => x, x => x, (a, b) => a + b).SequenceEqual(new[] { 8 }));
                Assert.IsTrue(oneItemSpecificOrdered.Join(range, x => x, x => x, (a, b) => a + b, new _IntComparer()).SequenceEqual(new[] { 8 }));
                Assert.IsTrue(oneItemSpecificOrdered.Join(Enumerable.Repeat(1, 3), x => x, x => x, (a, b) => a + b).SequenceEqual(new int[0]));
                Assert.IsTrue(oneItemSpecificOrdered.Join(Enumerable.Repeat(1, 3), x => x, x => x, (a, b) => a + b, new _IntComparer()).SequenceEqual(new int[0]));
                Assert.IsTrue(oneItemSpecificOrdered.Join(reverseRange, x => x, x => x, (a, b) => a + b).SequenceEqual(new[] { 8 }));
                Assert.IsTrue(oneItemSpecificOrdered.Join(reverseRange, x => x, x => x, (a, b) => a + b, new _IntComparer()).SequenceEqual(new[] { 8 }));

                Assert.IsTrue(oneItemSpecificOrdered.Join(oneItemDefault, x => x, x => x, (a, b) => a + b).SequenceEqual(new int[0]));
                Assert.IsTrue(oneItemSpecificOrdered.Join(oneItemDefault, x => x, x => x, (a, b) => a + b, new _IntComparer()).SequenceEqual(new int[0]));
                Assert.IsTrue(oneItemSpecificOrdered.Join(oneItemSpecific, x => x, x => x, (a, b) => a + b).SequenceEqual(new[] { 8 }));
                Assert.IsTrue(oneItemSpecificOrdered.Join(oneItemSpecific, x => x, x => x, (a, b) => a + b, new _IntComparer()).SequenceEqual(new[] { 8 }));
                Assert.IsTrue(oneItemSpecificOrdered.Join(oneItemDefaultOrdered, x => x, x => x, (a, b) => a + b).SequenceEqual(new int[0]));
                Assert.IsTrue(oneItemSpecificOrdered.Join(oneItemDefaultOrdered, x => x, x => x, (a, b) => a + b, new _IntComparer()).SequenceEqual(new int[0]));
                Assert.IsTrue(oneItemSpecificOrdered.Join(oneItemSpecificOrdered, x => x, x => x, (a, b) => a + b).SequenceEqual(new[] { 8 }));
                Assert.IsTrue(oneItemSpecificOrdered.Join(oneItemSpecificOrdered, x => x, x => x, (a, b) => a + b, new _IntComparer()).SequenceEqual(new[] { 8 }));

                Helper.ForEachEnumerableExpression(
                    oneItemSpecificOrdered,
                    new[] { 1 },
                    res => { },
                    @"(a, b) =>
                      {
                        Assert.IsTrue(a.Join(b, x => x, x => x, (c, d) => c + d).SequenceEqual(new int[0]));
                        Assert.IsTrue(a.Join(b, x => x, x => x, (c, d) => c + d, new JoinTests._IntComparer()).SequenceEqual(new int[0]));

                        return Helper.NoCallValue;
                      }",
                    typeof(EmptyEnumerable<>),
                    typeof(EmptyOrderedEnumerable<>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>)
                );
            }
        }

        [TestMethod]
        public void Errors_Default()
        {
            // default
            {
                Helper.ForEachEnumerableNoRetExpression(
                    new string[0],
                    @"a =>
                      Helper.ForEachEnumerableExpression(
                        a,
                        new string[0],
                        res => { },
                        @""(a, b) =>
                           {
                            Func<string, string> outerKey = str => str;
                            Func<string, string> innerKey = str => str;
                            Func<string, string, string> result = (s1, s2) => s1 + s2;

                            try
                            {
                                a.Join(b, default(Func<string, string>), innerKey, result);
                                Assert.Fail();
                            }
                            catch(ArgumentNullException exc)
                            {
                                Assert.AreEqual(""""outerKeySelector"""", exc.ParamName);
                            }

                            try
                            {
                                a.Join(b, outerKey, default(Func<string, string>), result);
                                Assert.Fail();
                            }
                            catch(ArgumentNullException exc)
                            {
                                Assert.AreEqual(""""innerKeySelector"""", exc.ParamName);
                            }

                            try
                            {
                                a.Join(b, outerKey, innerKey, default(Func<string, string, string>));
                                Assert.Fail();
                            }
                            catch(ArgumentNullException exc)
                            {
                                Assert.AreEqual(""""resultSelector"""", exc.ParamName);
                            }

                            return Helper.NoCallValue;
                           }"",
                        typeof(LookupDefaultEnumerable<,>),
                        typeof(LookupSpecificEnumerable<,>),
                        typeof(GroupByDefaultEnumerable<,,,,>),
                        typeof(GroupBySpecificEnumerable<,,,,>)
                      )",
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>)
                );
            }
        }

        [TestMethod]
        public void Errors_Specific()
        {
            // specific
            {
                Helper.ForEachEnumerableNoRetExpression(
                    new string[0],
                    @"a =>
                      Helper.ForEachEnumerableExpression(
                        a,
                        new string[0],
                        res => { },
                        @""(a, b) =>
                           {
                            Func<string, string> outerKey = str => str;
                            Func<string, string> innerKey = str => str;
                            Func<string, string, string> result = (s1, s2) => s1 + s2;

                            try
                            {
                                a.Join(b, default(Func<string, string>), innerKey, result, new JoinTests._Comparer());
                                Assert.Fail();
                            }
                            catch(ArgumentNullException exc)
                            {
                                Assert.AreEqual(""""outerKeySelector"""", exc.ParamName);
                            }

                            try
                            {
                                a.Join(b, outerKey, default(Func<string, string>), result, new JoinTests._Comparer());
                                Assert.Fail();
                            }
                            catch(ArgumentNullException exc)
                            {
                                Assert.AreEqual(""""innerKeySelector"""", exc.ParamName);
                            }

                            try
                            {
                                a.Join(b, outerKey, innerKey, default(Func<string, string, string>), new JoinTests._Comparer());
                                Assert.Fail();
                            }
                            catch(ArgumentNullException exc)
                            {
                                Assert.AreEqual(""""resultSelector"""", exc.ParamName);
                            }

                            return Helper.NoCallValue;
                           }"",
                        typeof(LookupDefaultEnumerable<,>),
                        typeof(LookupSpecificEnumerable<,>),
                        typeof(GroupByDefaultEnumerable<,,,,>),
                        typeof(GroupBySpecificEnumerable<,,,,>)
                      )",
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>)
                );
            }
        }

        [TestMethod]
        public void Errors_Weird()
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
                try { empty.Join(empty, default(Func<int, int>), x => x, (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { empty.Join(empty, x => x, default(Func<int, int>), (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { empty.Join(empty, x => x, x => x, default(Func<int, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { empty.Join(empty, default(Func<int, int>), x => x, (a, b) => a + b, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { empty.Join(empty, x => x, default(Func<int, int>), (a, b) => a + b, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { empty.Join(empty, x => x, x => x, default(Func<int, int, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { empty.Join(emptyOrdered, default(Func<int, int>), x => x, (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { empty.Join(emptyOrdered, x => x, default(Func<int, int>), (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { empty.Join(emptyOrdered, x => x, x => x, default(Func<int, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { empty.Join(emptyOrdered, default(Func<int, int>), x => x, (a, b) => a + b, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { empty.Join(emptyOrdered, x => x, default(Func<int, int>), (a, b) => a + b, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { empty.Join(emptyOrdered, x => x, x => x, default(Func<int, int, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { empty.Join(range, default(Func<int, int>), x => x, (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { empty.Join(range, x => x, default(Func<int, int>), (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { empty.Join(range, x => x, x => x, default(Func<int, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { empty.Join(range, default(Func<int, int>), x => x, (a, b) => a + b, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { empty.Join(range, x => x, default(Func<int, int>), (a, b) => a + b, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { empty.Join(range, x => x, x => x, default(Func<int, int, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { empty.Join(Enumerable.Repeat(1, 1), default(Func<int, int>), x => x, (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { empty.Join(Enumerable.Repeat(1, 1), x => x, default(Func<int, int>), (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { empty.Join(Enumerable.Repeat(1, 1), x => x, x => x, default(Func<int, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { empty.Join(Enumerable.Repeat(1, 1), default(Func<int, int>), x => x, (a, b) => a + b, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { empty.Join(Enumerable.Repeat(1, 1), x => x, default(Func<int, int>), (a, b) => a + b, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { empty.Join(Enumerable.Repeat(1, 1), x => x, x => x, default(Func<int, int, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { empty.Join(reverseRange, default(Func<int, int>), x => x, (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { empty.Join(reverseRange, x => x, default(Func<int, int>), (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { empty.Join(reverseRange, x => x, x => x, default(Func<int, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { empty.Join(reverseRange, default(Func<int, int>), x => x, (a, b) => a + b, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { empty.Join(reverseRange, x => x, default(Func<int, int>), (a, b) => a + b, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { empty.Join(reverseRange, x => x, x => x, default(Func<int, int, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { empty.Join(oneItemDefault, default(Func<int, int>), x => x, (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { empty.Join(oneItemDefault, x => x, default(Func<int, int>), (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { empty.Join(oneItemDefault, x => x, x => x, default(Func<int, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { empty.Join(oneItemDefault, default(Func<int, int>), x => x, (a, b) => a + b, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { empty.Join(oneItemDefault, x => x, default(Func<int, int>), (a, b) => a + b, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { empty.Join(oneItemDefault, x => x, x => x, default(Func<int, int, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { empty.Join(oneItemSpecific, default(Func<int, int>), x => x, (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { empty.Join(oneItemSpecific, x => x, default(Func<int, int>), (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { empty.Join(oneItemSpecific, x => x, x => x, default(Func<int, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { empty.Join(oneItemSpecific, default(Func<int, int>), x => x, (a, b) => a + b, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { empty.Join(oneItemSpecific, x => x, default(Func<int, int>), (a, b) => a + b, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { empty.Join(oneItemSpecific, x => x, x => x, default(Func<int, int, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { empty.Join(oneItemDefaultOrdered, default(Func<int, int>), x => x, (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { empty.Join(oneItemDefaultOrdered, x => x, default(Func<int, int>), (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { empty.Join(oneItemDefaultOrdered, x => x, x => x, default(Func<int, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { empty.Join(oneItemDefaultOrdered, default(Func<int, int>), x => x, (a, b) => a + b, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { empty.Join(oneItemDefaultOrdered, x => x, default(Func<int, int>), (a, b) => a + b, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { empty.Join(oneItemDefaultOrdered, x => x, x => x, default(Func<int, int, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { empty.Join(oneItemSpecificOrdered, default(Func<int, int>), x => x, (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { empty.Join(oneItemSpecificOrdered, x => x, default(Func<int, int>), (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { empty.Join(oneItemSpecificOrdered, x => x, x => x, default(Func<int, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { empty.Join(oneItemSpecificOrdered, default(Func<int, int>), x => x, (a, b) => a + b, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { empty.Join(oneItemSpecificOrdered, x => x, default(Func<int, int>), (a, b) => a + b, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { empty.Join(oneItemSpecificOrdered, x => x, x => x, default(Func<int, int, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                Helper.ForEachEnumerableExpression(
                    empty,
                    new int[0],
                    res => { },
                    @"(l, r) =>
                      {
                        try { l.Join(r, default(Func<int, int>), x => x, (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""outerKeySelector"", exc.ParamName); }
                        try { l.Join(r, x => x, default(Func<int, int>), (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""innerKeySelector"", exc.ParamName); }
                        try { l.Join(r, x => x, x => x, default(Func<int, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""resultSelector"", exc.ParamName); }
                        try { l.Join(r, default(Func<int, int>), x => x, (a, b) => a + b, new JoinTests._IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""outerKeySelector"", exc.ParamName); }
                        try { l.Join(r, x => x, default(Func<int, int>), (a, b) => a + b, new JoinTests._IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""innerKeySelector"", exc.ParamName); }
                        try { l.Join(r, x => x, x => x, default(Func<int, int, int>), new JoinTests._IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""resultSelector"", exc.ParamName); }

                        return Helper.NoCallValue;
                      }",
                    typeof(EmptyEnumerable<>),
                    typeof(EmptyOrderedEnumerable<>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>)
                );
            }

            // emptyOrdered
            {
                try { emptyOrdered.Join(empty, default(Func<int, int>), x => x, (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { emptyOrdered.Join(empty, x => x, default(Func<int, int>), (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { emptyOrdered.Join(empty, x => x, x => x, default(Func<int, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { emptyOrdered.Join(empty, default(Func<int, int>), x => x, (a, b) => a + b, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { emptyOrdered.Join(empty, x => x, default(Func<int, int>), (a, b) => a + b, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { emptyOrdered.Join(empty, x => x, x => x, default(Func<int, int, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { emptyOrdered.Join(emptyOrdered, default(Func<int, int>), x => x, (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { emptyOrdered.Join(emptyOrdered, x => x, default(Func<int, int>), (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { emptyOrdered.Join(emptyOrdered, x => x, x => x, default(Func<int, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { emptyOrdered.Join(emptyOrdered, default(Func<int, int>), x => x, (a, b) => a + b, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { emptyOrdered.Join(emptyOrdered, x => x, default(Func<int, int>), (a, b) => a + b, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { emptyOrdered.Join(emptyOrdered, x => x, x => x, default(Func<int, int, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { emptyOrdered.Join(range, default(Func<int, int>), x => x, (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { emptyOrdered.Join(range, x => x, default(Func<int, int>), (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { emptyOrdered.Join(range, x => x, x => x, default(Func<int, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { emptyOrdered.Join(range, default(Func<int, int>), x => x, (a, b) => a + b, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { emptyOrdered.Join(range, x => x, default(Func<int, int>), (a, b) => a + b, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { emptyOrdered.Join(range, x => x, x => x, default(Func<int, int, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { emptyOrdered.Join(Enumerable.Repeat(1, 1), default(Func<int, int>), x => x, (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { emptyOrdered.Join(Enumerable.Repeat(1, 1), x => x, default(Func<int, int>), (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { emptyOrdered.Join(Enumerable.Repeat(1, 1), x => x, x => x, default(Func<int, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { emptyOrdered.Join(Enumerable.Repeat(1, 1), default(Func<int, int>), x => x, (a, b) => a + b, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { emptyOrdered.Join(Enumerable.Repeat(1, 1), x => x, default(Func<int, int>), (a, b) => a + b, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { emptyOrdered.Join(Enumerable.Repeat(1, 1), x => x, x => x, default(Func<int, int, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { emptyOrdered.Join(reverseRange, default(Func<int, int>), x => x, (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { emptyOrdered.Join(reverseRange, x => x, default(Func<int, int>), (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { emptyOrdered.Join(reverseRange, x => x, x => x, default(Func<int, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { emptyOrdered.Join(reverseRange, default(Func<int, int>), x => x, (a, b) => a + b, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { emptyOrdered.Join(reverseRange, x => x, default(Func<int, int>), (a, b) => a + b, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { emptyOrdered.Join(reverseRange, x => x, x => x, default(Func<int, int, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { emptyOrdered.Join(oneItemDefault, default(Func<int, int>), x => x, (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { emptyOrdered.Join(oneItemDefault, x => x, default(Func<int, int>), (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { emptyOrdered.Join(oneItemDefault, x => x, x => x, default(Func<int, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { emptyOrdered.Join(oneItemDefault, default(Func<int, int>), x => x, (a, b) => a + b, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { emptyOrdered.Join(oneItemDefault, x => x, default(Func<int, int>), (a, b) => a + b, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { emptyOrdered.Join(oneItemDefault, x => x, x => x, default(Func<int, int, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { emptyOrdered.Join(oneItemSpecific, default(Func<int, int>), x => x, (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { emptyOrdered.Join(oneItemSpecific, x => x, default(Func<int, int>), (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { emptyOrdered.Join(oneItemSpecific, x => x, x => x, default(Func<int, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { emptyOrdered.Join(oneItemSpecific, default(Func<int, int>), x => x, (a, b) => a + b, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { emptyOrdered.Join(oneItemSpecific, x => x, default(Func<int, int>), (a, b) => a + b, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { emptyOrdered.Join(oneItemSpecific, x => x, x => x, default(Func<int, int, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { emptyOrdered.Join(oneItemDefaultOrdered, default(Func<int, int>), x => x, (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { emptyOrdered.Join(oneItemDefaultOrdered, x => x, default(Func<int, int>), (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { emptyOrdered.Join(oneItemDefaultOrdered, x => x, x => x, default(Func<int, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { emptyOrdered.Join(oneItemDefaultOrdered, default(Func<int, int>), x => x, (a, b) => a + b, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { emptyOrdered.Join(oneItemDefaultOrdered, x => x, default(Func<int, int>), (a, b) => a + b, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { emptyOrdered.Join(oneItemDefaultOrdered, x => x, x => x, default(Func<int, int, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { emptyOrdered.Join(oneItemSpecificOrdered, default(Func<int, int>), x => x, (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { emptyOrdered.Join(oneItemSpecificOrdered, x => x, default(Func<int, int>), (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { emptyOrdered.Join(oneItemSpecificOrdered, x => x, x => x, default(Func<int, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { emptyOrdered.Join(oneItemSpecificOrdered, default(Func<int, int>), x => x, (a, b) => a + b, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { emptyOrdered.Join(oneItemSpecificOrdered, x => x, default(Func<int, int>), (a, b) => a + b, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { emptyOrdered.Join(oneItemSpecificOrdered, x => x, x => x, default(Func<int, int, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                Helper.ForEachEnumerableExpression(
                    emptyOrdered,
                    new int[0],
                    res => { },
                    @"(l, r) =>
                      {
                        try { l.Join(r, default(Func<int, int>), x => x, (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""outerKeySelector"", exc.ParamName); }
                        try { l.Join(r, x => x, default(Func<int, int>), (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""innerKeySelector"", exc.ParamName); }
                        try { l.Join(r, x => x, x => x, default(Func<int, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""resultSelector"", exc.ParamName); }
                        try { l.Join(r, default(Func<int, int>), x => x, (a, b) => a + b, new JoinTests._IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""outerKeySelector"", exc.ParamName); }
                        try { l.Join(r, x => x, default(Func<int, int>), (a, b) => a + b, new JoinTests._IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""innerKeySelector"", exc.ParamName); }
                        try { l.Join(r, x => x, x => x, default(Func<int, int, int>), new JoinTests._IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""resultSelector"", exc.ParamName); }

                        return Helper.NoCallValue;
                      }",
                    typeof(EmptyEnumerable<>),
                    typeof(EmptyOrderedEnumerable<>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>)
                );
            }

            // groupByDefault
            {
                var emptyGroup = Enumerable.Empty<GroupingEnumerable<int, int>>();
                try { groupByDefault.Join(emptyGroup, default(Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>>), x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { groupByDefault.Join(emptyGroup, x => x, default(Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>>), (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { groupByDefault.Join(emptyGroup, x => x, x => x, default(Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>, GroupingEnumerable<int, int>>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { groupByDefault.Join(emptyGroup, default(Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>>), x => x, (a, b) => a, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { groupByDefault.Join(emptyGroup, x => x, default(Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>>), (a, b) => a, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { groupByDefault.Join(emptyGroup, x => x, x => x, default(Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>, GroupingEnumerable<int, int>>), new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                var emptyOrderedGroup = emptyGroup.OrderBy(x => x);
                try { groupByDefault.Join(emptyOrderedGroup, default(Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>>), x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { groupByDefault.Join(emptyOrderedGroup, x => x, default(Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>>), (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { groupByDefault.Join(emptyOrderedGroup, x => x, x => x, default(Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>, GroupingEnumerable<int, int>>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { groupByDefault.Join(emptyOrderedGroup, default(Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>>), x => x, (a, b) => a, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { groupByDefault.Join(emptyOrderedGroup, x => x, default(Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>>), (a, b) => a, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { groupByDefault.Join(emptyOrderedGroup, x => x, x => x, default(Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>, GroupingEnumerable<int, int>>), new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { groupByDefault.Join(groupByDefault, default(Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>>), x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { groupByDefault.Join(groupByDefault, x => x, default(Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>>), (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { groupByDefault.Join(groupByDefault, x => x, x => x, default(Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>, GroupingEnumerable<int, int>>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { groupByDefault.Join(groupByDefault, default(Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>>), x => x, (a, b) => a, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { groupByDefault.Join(groupByDefault, x => x, default(Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>>), (a, b) => a, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { groupByDefault.Join(groupByDefault, x => x, x => x, default(Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>, GroupingEnumerable<int, int>>), new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                var groupBySpecificInt = new[] { 1 }.GroupBy(x => x, new _IntComparer());
                try { groupByDefault.Join(groupBySpecificInt, default(Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>>), x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { groupByDefault.Join(groupBySpecificInt, x => x, default(Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>>), (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { groupByDefault.Join(groupBySpecificInt, x => x, x => x, default(Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>, GroupingEnumerable<int, int>>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { groupByDefault.Join(groupBySpecificInt, default(Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>>), x => x, (a, b) => a, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { groupByDefault.Join(groupBySpecificInt, x => x, default(Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>>), (a, b) => a, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { groupByDefault.Join(groupBySpecificInt, x => x, x => x, default(Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>, GroupingEnumerable<int, int>>), new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { groupByDefault.Join(lookup, default(Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>>), x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { groupByDefault.Join(lookup, x => x, default(Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>>), (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { groupByDefault.Join(lookup, x => x, x => x, default(Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>, GroupingEnumerable<int, int>>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { groupByDefault.Join(lookup, default(Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>>), x => x, (a, b) => a, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { groupByDefault.Join(lookup, x => x, default(Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>>), (a, b) => a, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { groupByDefault.Join(lookup, x => x, x => x, default(Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>, GroupingEnumerable<int, int>>), new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { groupByDefault.Join(Enumerable.Repeat(groupByDefault.First(), 1), default(Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>>), x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { groupByDefault.Join(Enumerable.Repeat(groupByDefault.First(), 1), x => x, default(Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>>), (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { groupByDefault.Join(Enumerable.Repeat(groupByDefault.First(), 1), x => x, x => x, default(Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>, GroupingEnumerable<int, int>>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { groupByDefault.Join(Enumerable.Repeat(groupByDefault.First(), 1), default(Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>>), x => x, (a, b) => a, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { groupByDefault.Join(Enumerable.Repeat(groupByDefault.First(), 1), x => x, default(Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>>), (a, b) => a, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { groupByDefault.Join(Enumerable.Repeat(groupByDefault.First(), 1), x => x, x => x, default(Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>, GroupingEnumerable<int, int>>), new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                var oneItemDefaultGrouping = Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty();
                try { groupByDefault.Join(oneItemDefaultGrouping, default(Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>>), x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { groupByDefault.Join(oneItemDefaultGrouping, x => x, default(Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>>), (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { groupByDefault.Join(oneItemDefaultGrouping, x => x, x => x, default(Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>, GroupingEnumerable<int, int>>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { groupByDefault.Join(oneItemDefaultGrouping, default(Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>>), x => x, (a, b) => a, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { groupByDefault.Join(oneItemDefaultGrouping, x => x, default(Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>>), (a, b) => a, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { groupByDefault.Join(oneItemDefaultGrouping, x => x, x => x, default(Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>, GroupingEnumerable<int, int>>), new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                var oneItemSpecificGrouping = Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty(groupByDefault.First());
                try { groupByDefault.Join(oneItemSpecificGrouping, default(Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>>), x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { groupByDefault.Join(oneItemSpecificGrouping, x => x, default(Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>>), (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { groupByDefault.Join(oneItemSpecificGrouping, x => x, x => x, default(Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>, GroupingEnumerable<int, int>>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { groupByDefault.Join(oneItemSpecificGrouping, default(Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>>), x => x, (a, b) => a, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { groupByDefault.Join(oneItemSpecificGrouping, x => x, default(Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>>), (a, b) => a, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { groupByDefault.Join(oneItemSpecificGrouping, x => x, x => x, default(Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>, GroupingEnumerable<int, int>>), new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                var oneItemDefaultOrderedGrouping = Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty().OrderBy(x => x);
                try { groupByDefault.Join(oneItemDefaultOrderedGrouping, default(Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>>), x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { groupByDefault.Join(oneItemDefaultOrderedGrouping, x => x, default(Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>>), (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { groupByDefault.Join(oneItemDefaultOrderedGrouping, x => x, x => x, default(Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>, GroupingEnumerable<int, int>>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { groupByDefault.Join(oneItemDefaultOrderedGrouping, default(Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>>), x => x, (a, b) => a, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { groupByDefault.Join(oneItemDefaultOrderedGrouping, x => x, default(Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>>), (a, b) => a, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { groupByDefault.Join(oneItemDefaultOrderedGrouping, x => x, x => x, default(Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>, GroupingEnumerable<int, int>>), new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                var oneItemSpecificOrderedGrouping = Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty(groupByDefault.First()).OrderBy(x => x);
                try { groupByDefault.Join(oneItemSpecificOrderedGrouping, default(Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>>), x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { groupByDefault.Join(oneItemSpecificOrderedGrouping, x => x, default(Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>>), (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { groupByDefault.Join(oneItemSpecificOrderedGrouping, x => x, x => x, default(Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>, GroupingEnumerable<int, int>>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { groupByDefault.Join(oneItemSpecificOrderedGrouping, default(Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>>), x => x, (a, b) => a, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { groupByDefault.Join(oneItemSpecificOrderedGrouping, x => x, default(Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>>), (a, b) => a, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { groupByDefault.Join(oneItemSpecificOrderedGrouping, x => x, x => x, default(Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>, GroupingEnumerable<int, int>>), new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                Helper.ForEachEnumerableExpression(
                    groupByDefault,
                    new[] { groupByDefault.First() },
                    res => { },
                    @"(l, r) =>
                      {
                        try { l.Join(r, default(Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>>), x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""outerKeySelector"", exc.ParamName); }
                        try { l.Join(r, x => x, default(Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>>), (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""innerKeySelector"", exc.ParamName); }
                        try { l.Join(r, x => x, x => x, default(Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>, GroupingEnumerable<int, int>>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""resultSelector"", exc.ParamName); }
                        try { l.Join(r, default(Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>>), x => x, (a, b) => a, new JoinTests._GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""outerKeySelector"", exc.ParamName); }
                        try { l.Join(r, x => x, default(Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>>), (a, b) => a, new JoinTests._GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""innerKeySelector"", exc.ParamName); }
                        try { l.Join(r, x => x, x => x, default(Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>, GroupingEnumerable<int, int>>), new JoinTests._GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""resultSelector"", exc.ParamName); }

                        return Helper.NoCallValue;
                      }",
                    typeof(EmptyEnumerable<>),
                    typeof(EmptyOrderedEnumerable<>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>)
                );
            }

            // groupBySpecific
            {
                var emptyGroup = Enumerable.Empty<GroupingEnumerable<string, string>>();
                try { groupBySpecific.Join(emptyGroup, default(Func<GroupingEnumerable<string, string>, GroupingEnumerable<string, string>>), x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { groupBySpecific.Join(emptyGroup, x => x, default(Func<GroupingEnumerable<string, string>, GroupingEnumerable<string, string>>), (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { groupBySpecific.Join(emptyGroup, x => x, x => x, default(Func<GroupingEnumerable<string, string>, GroupingEnumerable<string, string>, GroupingEnumerable<string, string>>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { groupBySpecific.Join(emptyGroup, default(Func<GroupingEnumerable<string, string>, GroupingEnumerable<string, string>>), x => x, (a, b) => a, new _GroupingComparer<string>()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { groupBySpecific.Join(emptyGroup, x => x, default(Func<GroupingEnumerable<string, string>, GroupingEnumerable<string, string>>), (a, b) => a, new _GroupingComparer<string>()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { groupBySpecific.Join(emptyGroup, x => x, x => x, default(Func<GroupingEnumerable<string, string>, GroupingEnumerable<string, string>, GroupingEnumerable<string, string>>), new _GroupingComparer<string>()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                var emptyOrderedGroup = emptyGroup.OrderBy(x => x);
                try { groupBySpecific.Join(emptyOrderedGroup, default(Func<GroupingEnumerable<string, string>, GroupingEnumerable<string, string>>), x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { groupBySpecific.Join(emptyOrderedGroup, x => x, default(Func<GroupingEnumerable<string, string>, GroupingEnumerable<string, string>>), (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { groupBySpecific.Join(emptyOrderedGroup, x => x, x => x, default(Func<GroupingEnumerable<string, string>, GroupingEnumerable<string, string>, GroupingEnumerable<string, string>>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { groupBySpecific.Join(emptyOrderedGroup, default(Func<GroupingEnumerable<string, string>, GroupingEnumerable<string, string>>), x => x, (a, b) => a, new _GroupingComparer<string>()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { groupBySpecific.Join(emptyOrderedGroup, x => x, default(Func<GroupingEnumerable<string, string>, GroupingEnumerable<string, string>>), (a, b) => a, new _GroupingComparer<string>()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { groupBySpecific.Join(emptyOrderedGroup, x => x, x => x, default(Func<GroupingEnumerable<string, string>, GroupingEnumerable<string, string>, GroupingEnumerable<string, string>>), new _GroupingComparer<string>()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                var groupByDefaultString = new[] { "foo" }.GroupBy(x => x);
                try { groupBySpecific.Join(groupByDefaultString, default(Func<GroupingEnumerable<string, string>, GroupingEnumerable<string, string>>), x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { groupBySpecific.Join(groupByDefaultString, x => x, default(Func<GroupingEnumerable<string, string>, GroupingEnumerable<string, string>>), (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { groupBySpecific.Join(groupByDefaultString, x => x, x => x, default(Func<GroupingEnumerable<string, string>, GroupingEnumerable<string, string>, GroupingEnumerable<string, string>>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { groupBySpecific.Join(groupByDefaultString, default(Func<GroupingEnumerable<string, string>, GroupingEnumerable<string, string>>), x => x, (a, b) => a, new _GroupingComparer<string>()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { groupBySpecific.Join(groupByDefaultString, x => x, default(Func<GroupingEnumerable<string, string>, GroupingEnumerable<string, string>>), (a, b) => a, new _GroupingComparer<string>()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { groupBySpecific.Join(groupByDefaultString, x => x, x => x, default(Func<GroupingEnumerable<string, string>, GroupingEnumerable<string, string>, GroupingEnumerable<string, string>>), new _GroupingComparer<string>()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { groupBySpecific.Join(groupBySpecific, default(Func<GroupingEnumerable<string, string>, GroupingEnumerable<string, string>>), x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { groupBySpecific.Join(groupBySpecific, x => x, default(Func<GroupingEnumerable<string, string>, GroupingEnumerable<string, string>>), (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { groupBySpecific.Join(groupBySpecific, x => x, x => x, default(Func<GroupingEnumerable<string, string>, GroupingEnumerable<string, string>, GroupingEnumerable<string, string>>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { groupBySpecific.Join(groupBySpecific, default(Func<GroupingEnumerable<string, string>, GroupingEnumerable<string, string>>), x => x, (a, b) => a, new _GroupingComparer<string>()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { groupBySpecific.Join(groupBySpecific, x => x, default(Func<GroupingEnumerable<string, string>, GroupingEnumerable<string, string>>), (a, b) => a, new _GroupingComparer<string>()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { groupBySpecific.Join(groupBySpecific, x => x, x => x, default(Func<GroupingEnumerable<string, string>, GroupingEnumerable<string, string>, GroupingEnumerable<string, string>>), new _GroupingComparer<string>()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                var lookupString = new[] { "foo" }.ToLookup(x => x);
                try { groupBySpecific.Join(lookupString, default(Func<GroupingEnumerable<string, string>, GroupingEnumerable<string, string>>), x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { groupBySpecific.Join(lookupString, x => x, default(Func<GroupingEnumerable<string, string>, GroupingEnumerable<string, string>>), (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { groupBySpecific.Join(lookupString, x => x, x => x, default(Func<GroupingEnumerable<string, string>, GroupingEnumerable<string, string>, GroupingEnumerable<string, string>>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { groupBySpecific.Join(lookupString, default(Func<GroupingEnumerable<string, string>, GroupingEnumerable<string, string>>), x => x, (a, b) => a, new _GroupingComparer<string>()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { groupBySpecific.Join(lookupString, x => x, default(Func<GroupingEnumerable<string, string>, GroupingEnumerable<string, string>>), (a, b) => a, new _GroupingComparer<string>()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { groupBySpecific.Join(lookupString, x => x, x => x, default(Func<GroupingEnumerable<string, string>, GroupingEnumerable<string, string>, GroupingEnumerable<string, string>>), new _GroupingComparer<string>()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { groupBySpecific.Join(Enumerable.Repeat(groupBySpecific.First(), 1), default(Func<GroupingEnumerable<string, string>, GroupingEnumerable<string, string>>), x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { groupBySpecific.Join(Enumerable.Repeat(groupBySpecific.First(), 1), x => x, default(Func<GroupingEnumerable<string, string>, GroupingEnumerable<string, string>>), (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { groupBySpecific.Join(Enumerable.Repeat(groupBySpecific.First(), 1), x => x, x => x, default(Func<GroupingEnumerable<string, string>, GroupingEnumerable<string, string>, GroupingEnumerable<string, string>>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { groupBySpecific.Join(Enumerable.Repeat(groupBySpecific.First(), 1), default(Func<GroupingEnumerable<string, string>, GroupingEnumerable<string, string>>), x => x, (a, b) => a, new _GroupingComparer<string>()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { groupBySpecific.Join(Enumerable.Repeat(groupBySpecific.First(), 1), x => x, default(Func<GroupingEnumerable<string, string>, GroupingEnumerable<string, string>>), (a, b) => a, new _GroupingComparer<string>()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { groupBySpecific.Join(Enumerable.Repeat(groupBySpecific.First(), 1), x => x, x => x, default(Func<GroupingEnumerable<string, string>, GroupingEnumerable<string, string>, GroupingEnumerable<string, string>>), new _GroupingComparer<string>()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                var oneItemDefaultGrouping = Enumerable.Empty<GroupingEnumerable<string, string>>().DefaultIfEmpty();
                try { groupBySpecific.Join(oneItemDefaultGrouping, default(Func<GroupingEnumerable<string, string>, GroupingEnumerable<string, string>>), x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { groupBySpecific.Join(oneItemDefaultGrouping, x => x, default(Func<GroupingEnumerable<string, string>, GroupingEnumerable<string, string>>), (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { groupBySpecific.Join(oneItemDefaultGrouping, x => x, x => x, default(Func<GroupingEnumerable<string, string>, GroupingEnumerable<string, string>, GroupingEnumerable<string, string>>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { groupBySpecific.Join(oneItemDefaultGrouping, default(Func<GroupingEnumerable<string, string>, GroupingEnumerable<string, string>>), x => x, (a, b) => a, new _GroupingComparer<string>()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { groupBySpecific.Join(oneItemDefaultGrouping, x => x, default(Func<GroupingEnumerable<string, string>, GroupingEnumerable<string, string>>), (a, b) => a, new _GroupingComparer<string>()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { groupBySpecific.Join(oneItemDefaultGrouping, x => x, x => x, default(Func<GroupingEnumerable<string, string>, GroupingEnumerable<string, string>, GroupingEnumerable<string, string>>), new _GroupingComparer<string>()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                var oneItemSpecificGrouping = Enumerable.Empty<GroupingEnumerable<string, string>>().DefaultIfEmpty(groupBySpecific.First());
                try { groupBySpecific.Join(oneItemSpecificGrouping, default(Func<GroupingEnumerable<string, string>, GroupingEnumerable<string, string>>), x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { groupBySpecific.Join(oneItemSpecificGrouping, x => x, default(Func<GroupingEnumerable<string, string>, GroupingEnumerable<string, string>>), (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { groupBySpecific.Join(oneItemSpecificGrouping, x => x, x => x, default(Func<GroupingEnumerable<string, string>, GroupingEnumerable<string, string>, GroupingEnumerable<string, string>>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { groupBySpecific.Join(oneItemSpecificGrouping, default(Func<GroupingEnumerable<string, string>, GroupingEnumerable<string, string>>), x => x, (a, b) => a, new _GroupingComparer<string>()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { groupBySpecific.Join(oneItemSpecificGrouping, x => x, default(Func<GroupingEnumerable<string, string>, GroupingEnumerable<string, string>>), (a, b) => a, new _GroupingComparer<string>()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { groupBySpecific.Join(oneItemSpecificGrouping, x => x, x => x, default(Func<GroupingEnumerable<string, string>, GroupingEnumerable<string, string>, GroupingEnumerable<string, string>>), new _GroupingComparer<string>()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                var oneItemDefaultOrderedGrouping = Enumerable.Empty<GroupingEnumerable<string, string>>().DefaultIfEmpty().OrderBy(x => x);
                try { groupBySpecific.Join(oneItemDefaultOrderedGrouping, default(Func<GroupingEnumerable<string, string>, GroupingEnumerable<string, string>>), x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { groupBySpecific.Join(oneItemDefaultOrderedGrouping, x => x, default(Func<GroupingEnumerable<string, string>, GroupingEnumerable<string, string>>), (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { groupBySpecific.Join(oneItemDefaultOrderedGrouping, x => x, x => x, default(Func<GroupingEnumerable<string, string>, GroupingEnumerable<string, string>, GroupingEnumerable<string, string>>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { groupBySpecific.Join(oneItemDefaultOrderedGrouping, default(Func<GroupingEnumerable<string, string>, GroupingEnumerable<string, string>>), x => x, (a, b) => a, new _GroupingComparer<string>()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { groupBySpecific.Join(oneItemDefaultOrderedGrouping, x => x, default(Func<GroupingEnumerable<string, string>, GroupingEnumerable<string, string>>), (a, b) => a, new _GroupingComparer<string>()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { groupBySpecific.Join(oneItemDefaultOrderedGrouping, x => x, x => x, default(Func<GroupingEnumerable<string, string>, GroupingEnumerable<string, string>, GroupingEnumerable<string, string>>), new _GroupingComparer<string>()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                var oneItemSpecificOrderedGrouping = Enumerable.Empty<GroupingEnumerable<string, string>>().DefaultIfEmpty(groupBySpecific.First()).OrderBy(x => x);
                try { groupBySpecific.Join(oneItemSpecificOrderedGrouping, default(Func<GroupingEnumerable<string, string>, GroupingEnumerable<string, string>>), x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { groupBySpecific.Join(oneItemSpecificOrderedGrouping, x => x, default(Func<GroupingEnumerable<string, string>, GroupingEnumerable<string, string>>), (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { groupBySpecific.Join(oneItemSpecificOrderedGrouping, x => x, x => x, default(Func<GroupingEnumerable<string, string>, GroupingEnumerable<string, string>, GroupingEnumerable<string, string>>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { groupBySpecific.Join(oneItemSpecificOrderedGrouping, default(Func<GroupingEnumerable<string, string>, GroupingEnumerable<string, string>>), x => x, (a, b) => a, new _GroupingComparer<string>()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { groupBySpecific.Join(oneItemSpecificOrderedGrouping, x => x, default(Func<GroupingEnumerable<string, string>, GroupingEnumerable<string, string>>), (a, b) => a, new _GroupingComparer<string>()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { groupBySpecific.Join(oneItemSpecificOrderedGrouping, x => x, x => x, default(Func<GroupingEnumerable<string, string>, GroupingEnumerable<string, string>, GroupingEnumerable<string, string>>), new _GroupingComparer<string>()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                Helper.ForEachEnumerableExpression(
                    groupBySpecific,
                    new[] { groupBySpecific.First() },
                    res => { },
                    @"(l, r) =>
                      {
                        try { l.Join(r, default(Func<GroupingEnumerable<string, string>, GroupingEnumerable<string, string>>), x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""outerKeySelector"", exc.ParamName); }
                        try { l.Join(r, x => x, default(Func<GroupingEnumerable<string, string>, GroupingEnumerable<string, string>>), (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""innerKeySelector"", exc.ParamName); }
                        try { l.Join(r, x => x, x => x, default(Func<GroupingEnumerable<string, string>, GroupingEnumerable<string, string>, GroupingEnumerable<string, string>>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""resultSelector"", exc.ParamName); }
                        try { l.Join(r, default(Func<GroupingEnumerable<string, string>, GroupingEnumerable<string, string>>), x => x, (a, b) => a, new JoinTests._GroupingComparer<string>()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""outerKeySelector"", exc.ParamName); }
                        try { l.Join(r, x => x, default(Func<GroupingEnumerable<string, string>, GroupingEnumerable<string, string>>), (a, b) => a, new JoinTests._GroupingComparer<string>()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""innerKeySelector"", exc.ParamName); }
                        try { l.Join(r, x => x, x => x, default(Func<GroupingEnumerable<string, string>, GroupingEnumerable<string, string>, GroupingEnumerable<string, string>>), new JoinTests._GroupingComparer<string>()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""resultSelector"", exc.ParamName); }

                        return Helper.NoCallValue;
                      }",
                    typeof(EmptyEnumerable<>),
                    typeof(EmptyOrderedEnumerable<>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>)
                );
            }

            // lookup
            {
                var emptyGroup = Enumerable.Empty<GroupingEnumerable<int, int>>();
                try { lookup.Join(emptyGroup, default(Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>>), x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { lookup.Join(emptyGroup, x => x, default(Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>>), (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { lookup.Join(emptyGroup, x => x, x => x, default(Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>, GroupingEnumerable<int, int>>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { lookup.Join(emptyGroup, default(Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>>), x => x, (a, b) => a, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { lookup.Join(emptyGroup, x => x, default(Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>>), (a, b) => a, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { lookup.Join(emptyGroup, x => x, x => x, default(Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>, GroupingEnumerable<int, int>>), new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                var emptyOrderedGroup = emptyGroup.OrderBy(x => x);
                try { lookup.Join(emptyOrderedGroup, default(Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>>), x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { lookup.Join(emptyOrderedGroup, x => x, default(Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>>), (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { lookup.Join(emptyOrderedGroup, x => x, x => x, default(Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>, GroupingEnumerable<int, int>>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { lookup.Join(emptyOrderedGroup, default(Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>>), x => x, (a, b) => a, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { lookup.Join(emptyOrderedGroup, x => x, default(Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>>), (a, b) => a, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { lookup.Join(emptyOrderedGroup, x => x, x => x, default(Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>, GroupingEnumerable<int, int>>), new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { lookup.Join(groupByDefault, default(Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>>), x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { lookup.Join(groupByDefault, x => x, default(Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>>), (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { lookup.Join(groupByDefault, x => x, x => x, default(Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>, GroupingEnumerable<int, int>>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { lookup.Join(groupByDefault, default(Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>>), x => x, (a, b) => a, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { lookup.Join(groupByDefault, x => x, default(Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>>), (a, b) => a, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { lookup.Join(groupByDefault, x => x, x => x, default(Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>, GroupingEnumerable<int, int>>), new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                var groupBySpecificInt = new[] { 1 }.GroupBy(x => x, new _IntComparer());
                try { lookup.Join(groupBySpecificInt, default(Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>>), x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { lookup.Join(groupBySpecificInt, x => x, default(Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>>), (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { lookup.Join(groupBySpecificInt, x => x, x => x, default(Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>, GroupingEnumerable<int, int>>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { lookup.Join(groupBySpecificInt, default(Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>>), x => x, (a, b) => a, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { lookup.Join(groupBySpecificInt, x => x, default(Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>>), (a, b) => a, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { lookup.Join(groupBySpecificInt, x => x, x => x, default(Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>, GroupingEnumerable<int, int>>), new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { lookup.Join(lookup, default(Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>>), x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { lookup.Join(lookup, x => x, default(Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>>), (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { lookup.Join(lookup, x => x, x => x, default(Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>, GroupingEnumerable<int, int>>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { lookup.Join(lookup, default(Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>>), x => x, (a, b) => a, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { lookup.Join(lookup, x => x, default(Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>>), (a, b) => a, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { lookup.Join(lookup, x => x, x => x, default(Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>, GroupingEnumerable<int, int>>), new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { lookup.Join(Enumerable.Repeat(groupByDefault.First(), 1), default(Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>>), x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { lookup.Join(Enumerable.Repeat(groupByDefault.First(), 1), x => x, default(Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>>), (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { lookup.Join(Enumerable.Repeat(groupByDefault.First(), 1), x => x, x => x, default(Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>, GroupingEnumerable<int, int>>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { lookup.Join(Enumerable.Repeat(groupByDefault.First(), 1), default(Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>>), x => x, (a, b) => a, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { lookup.Join(Enumerable.Repeat(groupByDefault.First(), 1), x => x, default(Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>>), (a, b) => a, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { lookup.Join(Enumerable.Repeat(groupByDefault.First(), 1), x => x, x => x, default(Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>, GroupingEnumerable<int, int>>), new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                var oneItemDefaultGrouping = Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty();
                try { lookup.Join(oneItemDefaultGrouping, default(Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>>), x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { lookup.Join(oneItemDefaultGrouping, x => x, default(Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>>), (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { lookup.Join(oneItemDefaultGrouping, x => x, x => x, default(Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>, GroupingEnumerable<int, int>>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { lookup.Join(oneItemDefaultGrouping, default(Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>>), x => x, (a, b) => a, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { lookup.Join(oneItemDefaultGrouping, x => x, default(Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>>), (a, b) => a, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { lookup.Join(oneItemDefaultGrouping, x => x, x => x, default(Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>, GroupingEnumerable<int, int>>), new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                var oneItemSpecificGrouping = Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty(groupByDefault.First());
                try { lookup.Join(oneItemSpecificGrouping, default(Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>>), x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { lookup.Join(oneItemSpecificGrouping, x => x, default(Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>>), (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { lookup.Join(oneItemSpecificGrouping, x => x, x => x, default(Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>, GroupingEnumerable<int, int>>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { lookup.Join(oneItemSpecificGrouping, default(Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>>), x => x, (a, b) => a, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { lookup.Join(oneItemSpecificGrouping, x => x, default(Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>>), (a, b) => a, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { lookup.Join(oneItemSpecificGrouping, x => x, x => x, default(Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>, GroupingEnumerable<int, int>>), new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                var oneItemDefaultOrderedGrouping = Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty().OrderBy(x => x);
                try { lookup.Join(oneItemDefaultOrderedGrouping, default(Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>>), x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { lookup.Join(oneItemDefaultOrderedGrouping, x => x, default(Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>>), (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { lookup.Join(oneItemDefaultOrderedGrouping, x => x, x => x, default(Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>, GroupingEnumerable<int, int>>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { lookup.Join(oneItemDefaultOrderedGrouping, default(Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>>), x => x, (a, b) => a, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { lookup.Join(oneItemDefaultOrderedGrouping, x => x, default(Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>>), (a, b) => a, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { lookup.Join(oneItemDefaultOrderedGrouping, x => x, x => x, default(Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>, GroupingEnumerable<int, int>>), new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                var oneItemSpecificOrderedGrouping = Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty(groupByDefault.First()).OrderBy(x => x);
                try { lookup.Join(oneItemSpecificOrderedGrouping, default(Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>>), x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { lookup.Join(oneItemSpecificOrderedGrouping, x => x, default(Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>>), (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { lookup.Join(oneItemSpecificOrderedGrouping, x => x, x => x, default(Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>, GroupingEnumerable<int, int>>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { lookup.Join(oneItemSpecificOrderedGrouping, default(Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>>), x => x, (a, b) => a, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { lookup.Join(oneItemSpecificOrderedGrouping, x => x, default(Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>>), (a, b) => a, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { lookup.Join(oneItemSpecificOrderedGrouping, x => x, x => x, default(Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>, GroupingEnumerable<int, int>>), new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                Helper.ForEachEnumerableExpression(
                    lookup,
                    new[] { lookup.First() },
                    res => { },
                    @"(l, r) =>
                      {
                        try { l.Join(r, default(Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>>), x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""outerKeySelector"", exc.ParamName); }
                        try { l.Join(r, x => x, default(Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>>), (a, b) => a); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""innerKeySelector"", exc.ParamName); }
                        try { l.Join(r, x => x, x => x, default(Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>, GroupingEnumerable<int, int>>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""resultSelector"", exc.ParamName); }
                        try { l.Join(r, default(Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>>), x => x, (a, b) => a, new JoinTests._GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""outerKeySelector"", exc.ParamName); }
                        try { l.Join(r, x => x, default(Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>>), (a, b) => a, new JoinTests._GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""innerKeySelector"", exc.ParamName); }
                        try { l.Join(r, x => x, x => x, default(Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>, GroupingEnumerable<int, int>>), new JoinTests._GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""resultSelector"", exc.ParamName); }

                        return Helper.NoCallValue;
                      }",
                    typeof(EmptyEnumerable<>),
                    typeof(EmptyOrderedEnumerable<>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>)
                );
            }

            // range
            {
                try { range.Join(empty, default(Func<int, int>), x => x, (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { range.Join(empty, x => x, default(Func<int, int>), (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { range.Join(empty, x => x, x => x, default(Func<int, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { range.Join(empty, default(Func<int, int>), x => x, (a, b) => a + b, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { range.Join(empty, x => x, default(Func<int, int>), (a, b) => a + b, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { range.Join(empty, x => x, x => x, default(Func<int, int, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { range.Join(emptyOrdered, default(Func<int, int>), x => x, (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { range.Join(emptyOrdered, x => x, default(Func<int, int>), (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { range.Join(emptyOrdered, x => x, x => x, default(Func<int, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { range.Join(emptyOrdered, default(Func<int, int>), x => x, (a, b) => a + b, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { range.Join(emptyOrdered, x => x, default(Func<int, int>), (a, b) => a + b, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { range.Join(emptyOrdered, x => x, x => x, default(Func<int, int, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { range.Join(range, default(Func<int, int>), x => x, (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { range.Join(range, x => x, default(Func<int, int>), (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { range.Join(range, x => x, x => x, default(Func<int, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { range.Join(range, default(Func<int, int>), x => x, (a, b) => a + b, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { range.Join(range, x => x, default(Func<int, int>), (a, b) => a + b, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { range.Join(range, x => x, x => x, default(Func<int, int, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { range.Join(Enumerable.Repeat(1, 1), default(Func<int, int>), x => x, (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { range.Join(Enumerable.Repeat(1, 1), x => x, default(Func<int, int>), (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { range.Join(Enumerable.Repeat(1, 1), x => x, x => x, default(Func<int, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { range.Join(Enumerable.Repeat(1, 1), default(Func<int, int>), x => x, (a, b) => a + b, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { range.Join(Enumerable.Repeat(1, 1), x => x, default(Func<int, int>), (a, b) => a + b, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { range.Join(Enumerable.Repeat(1, 1), x => x, x => x, default(Func<int, int, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { range.Join(reverseRange, default(Func<int, int>), x => x, (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { range.Join(reverseRange, x => x, default(Func<int, int>), (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { range.Join(reverseRange, x => x, x => x, default(Func<int, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { range.Join(reverseRange, default(Func<int, int>), x => x, (a, b) => a + b, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { range.Join(reverseRange, x => x, default(Func<int, int>), (a, b) => a + b, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { range.Join(reverseRange, x => x, x => x, default(Func<int, int, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { range.Join(oneItemDefault, default(Func<int, int>), x => x, (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { range.Join(oneItemDefault, x => x, default(Func<int, int>), (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { range.Join(oneItemDefault, x => x, x => x, default(Func<int, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { range.Join(oneItemDefault, default(Func<int, int>), x => x, (a, b) => a + b, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { range.Join(oneItemDefault, x => x, default(Func<int, int>), (a, b) => a + b, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { range.Join(oneItemDefault, x => x, x => x, default(Func<int, int, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { range.Join(oneItemSpecific, default(Func<int, int>), x => x, (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { range.Join(oneItemSpecific, x => x, default(Func<int, int>), (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { range.Join(oneItemSpecific, x => x, x => x, default(Func<int, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { range.Join(oneItemSpecific, default(Func<int, int>), x => x, (a, b) => a + b, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { range.Join(oneItemSpecific, x => x, default(Func<int, int>), (a, b) => a + b, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { range.Join(oneItemSpecific, x => x, x => x, default(Func<int, int, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { range.Join(oneItemDefaultOrdered, default(Func<int, int>), x => x, (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { range.Join(oneItemDefaultOrdered, x => x, default(Func<int, int>), (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { range.Join(oneItemDefaultOrdered, x => x, x => x, default(Func<int, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { range.Join(oneItemDefaultOrdered, default(Func<int, int>), x => x, (a, b) => a + b, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { range.Join(oneItemDefaultOrdered, x => x, default(Func<int, int>), (a, b) => a + b, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { range.Join(oneItemDefaultOrdered, x => x, x => x, default(Func<int, int, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { range.Join(oneItemSpecificOrdered, default(Func<int, int>), x => x, (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { range.Join(oneItemSpecificOrdered, x => x, default(Func<int, int>), (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { range.Join(oneItemSpecificOrdered, x => x, x => x, default(Func<int, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { range.Join(oneItemSpecificOrdered, default(Func<int, int>), x => x, (a, b) => a + b, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { range.Join(oneItemSpecificOrdered, x => x, default(Func<int, int>), (a, b) => a + b, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { range.Join(oneItemSpecificOrdered, x => x, x => x, default(Func<int, int, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                Helper.ForEachEnumerableExpression(
                    range,
                    new int[0],
                    res => { },
                    @"(l, r) =>
                      {
                        try { l.Join(r, default(Func<int, int>), x => x, (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""outerKeySelector"", exc.ParamName); }
                        try { l.Join(r, x => x, default(Func<int, int>), (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""innerKeySelector"", exc.ParamName); }
                        try { l.Join(r, x => x, x => x, default(Func<int, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""resultSelector"", exc.ParamName); }
                        try { l.Join(r, default(Func<int, int>), x => x, (a, b) => a + b, new JoinTests._IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""outerKeySelector"", exc.ParamName); }
                        try { l.Join(r, x => x, default(Func<int, int>), (a, b) => a + b, new JoinTests._IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""innerKeySelector"", exc.ParamName); }
                        try { l.Join(r, x => x, x => x, default(Func<int, int, int>), new JoinTests._IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""resultSelector"", exc.ParamName); }

                        return Helper.NoCallValue;
                      }",
                    typeof(EmptyEnumerable<>),
                    typeof(EmptyOrderedEnumerable<>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>)
                );
            }

            // repeat
            {
                var emptyStr = Enumerable.Empty<string>();
                try { repeat.Join(emptyStr, default(Func<string, string>), x => x, (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { repeat.Join(emptyStr, x => x, default(Func<string, string>), (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { repeat.Join(emptyStr, x => x, x => x, default(Func<string, string, string>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { repeat.Join(emptyStr, default(Func<string, string>), x => x, (a, b) => a + b, StringComparer.InvariantCultureIgnoreCase); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { repeat.Join(emptyStr, x => x, default(Func<string, string>), (a, b) => a + b, StringComparer.InvariantCultureIgnoreCase); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { repeat.Join(emptyStr, x => x, x => x, default(Func<string, string, string>), StringComparer.InvariantCultureIgnoreCase); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                var emptyOrderedStr = emptyStr.OrderBy(x => x);
                try { repeat.Join(emptyOrderedStr, default(Func<string, string>), x => x, (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { repeat.Join(emptyOrderedStr, x => x, default(Func<string, string>), (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { repeat.Join(emptyOrderedStr, x => x, x => x, default(Func<string, string, string>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { repeat.Join(emptyOrderedStr, default(Func<string, string>), x => x, (a, b) => a + b, StringComparer.InvariantCultureIgnoreCase); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { repeat.Join(emptyOrderedStr, x => x, default(Func<string, string>), (a, b) => a + b, StringComparer.InvariantCultureIgnoreCase); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { repeat.Join(emptyOrderedStr, x => x, x => x, default(Func<string, string, string>), StringComparer.InvariantCultureIgnoreCase); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { repeat.Join(repeat, default(Func<string, string>), x => x, (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { repeat.Join(repeat, x => x, default(Func<string, string>), (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { repeat.Join(repeat, x => x, x => x, default(Func<string, string, string>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { repeat.Join(repeat, default(Func<string, string>), x => x, (a, b) => a + b, StringComparer.InvariantCultureIgnoreCase); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { repeat.Join(repeat, x => x, default(Func<string, string>), (a, b) => a + b, StringComparer.InvariantCultureIgnoreCase); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { repeat.Join(repeat, x => x, x => x, default(Func<string, string, string>), StringComparer.InvariantCultureIgnoreCase); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                var repeatInt = Enumerable.Repeat(1, 1);
                try { repeatInt.Join(oneItemDefault, default(Func<int, int>), x => x, (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { repeatInt.Join(oneItemDefault, x => x, default(Func<int, int>), (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { repeatInt.Join(oneItemDefault, x => x, x => x, default(Func<int, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { repeatInt.Join(oneItemDefault, default(Func<int, int>), x => x, (a, b) => a + b, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { repeatInt.Join(oneItemDefault, x => x, default(Func<int, int>), (a, b) => a + b, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { repeatInt.Join(oneItemDefault, x => x, x => x, default(Func<int, int, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { repeatInt.Join(oneItemSpecific, default(Func<int, int>), x => x, (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { repeatInt.Join(oneItemSpecific, x => x, default(Func<int, int>), (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { repeatInt.Join(oneItemSpecific, x => x, x => x, default(Func<int, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { repeatInt.Join(oneItemSpecific, default(Func<int, int>), x => x, (a, b) => a + b, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { repeatInt.Join(oneItemSpecific, x => x, default(Func<int, int>), (a, b) => a + b, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { repeatInt.Join(oneItemSpecific, x => x, x => x, default(Func<int, int, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { repeatInt.Join(oneItemDefaultOrdered, default(Func<int, int>), x => x, (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { repeatInt.Join(oneItemDefaultOrdered, x => x, default(Func<int, int>), (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { repeatInt.Join(oneItemDefaultOrdered, x => x, x => x, default(Func<int, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { repeatInt.Join(oneItemDefaultOrdered, default(Func<int, int>), x => x, (a, b) => a + b, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { repeatInt.Join(oneItemDefaultOrdered, x => x, default(Func<int, int>), (a, b) => a + b, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { repeatInt.Join(oneItemDefaultOrdered, x => x, x => x, default(Func<int, int, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { repeatInt.Join(oneItemSpecificOrdered, default(Func<int, int>), x => x, (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { repeatInt.Join(oneItemSpecificOrdered, x => x, default(Func<int, int>), (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { repeatInt.Join(oneItemSpecificOrdered, x => x, x => x, default(Func<int, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { repeatInt.Join(oneItemSpecificOrdered, default(Func<int, int>), x => x, (a, b) => a + b, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { repeatInt.Join(oneItemSpecificOrdered, x => x, default(Func<int, int>), (a, b) => a + b, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { repeatInt.Join(oneItemSpecificOrdered, x => x, x => x, default(Func<int, int, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                Helper.ForEachEnumerableExpression(
                    repeat,
                    new string[0],
                    res => { },
                    @"(l, r) =>
                      {
                        try { l.Join(r, default(Func<string, string>), x => x, (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""outerKeySelector"", exc.ParamName); }
                        try { l.Join(r, x => x, default(Func<string, string>), (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""innerKeySelector"", exc.ParamName); }
                        try { l.Join(r, x => x, x => x, default(Func<string, string, string>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""resultSelector"", exc.ParamName); }
                        try { l.Join(r, default(Func<string, string>), x => x, (a, b) => a + b, StringComparer.InvariantCultureIgnoreCase); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""outerKeySelector"", exc.ParamName); }
                        try { l.Join(r, x => x, default(Func<string, string>), (a, b) => a + b, StringComparer.InvariantCultureIgnoreCase); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""innerKeySelector"", exc.ParamName); }
                        try { l.Join(r, x => x, x => x, default(Func<string, string, string>), StringComparer.InvariantCultureIgnoreCase); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""resultSelector"", exc.ParamName); }

                        return Helper.NoCallValue;
                      }",
                    typeof(EmptyEnumerable<>),
                    typeof(EmptyOrderedEnumerable<>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>)
                );
            }

            // reverseRange
            {
                try { reverseRange.Join(empty, default(Func<int, int>), x => x, (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { reverseRange.Join(empty, x => x, default(Func<int, int>), (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { reverseRange.Join(empty, x => x, x => x, default(Func<int, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { reverseRange.Join(empty, default(Func<int, int>), x => x, (a, b) => a + b, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { reverseRange.Join(empty, x => x, default(Func<int, int>), (a, b) => a + b, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { reverseRange.Join(empty, x => x, x => x, default(Func<int, int, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { reverseRange.Join(emptyOrdered, default(Func<int, int>), x => x, (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { reverseRange.Join(emptyOrdered, x => x, default(Func<int, int>), (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { reverseRange.Join(emptyOrdered, x => x, x => x, default(Func<int, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { reverseRange.Join(emptyOrdered, default(Func<int, int>), x => x, (a, b) => a + b, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { reverseRange.Join(emptyOrdered, x => x, default(Func<int, int>), (a, b) => a + b, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { reverseRange.Join(emptyOrdered, x => x, x => x, default(Func<int, int, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { reverseRange.Join(range, default(Func<int, int>), x => x, (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { reverseRange.Join(range, x => x, default(Func<int, int>), (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { reverseRange.Join(range, x => x, x => x, default(Func<int, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { reverseRange.Join(range, default(Func<int, int>), x => x, (a, b) => a + b, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { reverseRange.Join(range, x => x, default(Func<int, int>), (a, b) => a + b, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { reverseRange.Join(range, x => x, x => x, default(Func<int, int, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { reverseRange.Join(Enumerable.Repeat(1, 1), default(Func<int, int>), x => x, (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { reverseRange.Join(Enumerable.Repeat(1, 1), x => x, default(Func<int, int>), (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { reverseRange.Join(Enumerable.Repeat(1, 1), x => x, x => x, default(Func<int, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { reverseRange.Join(Enumerable.Repeat(1, 1), default(Func<int, int>), x => x, (a, b) => a + b, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { reverseRange.Join(Enumerable.Repeat(1, 1), x => x, default(Func<int, int>), (a, b) => a + b, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { reverseRange.Join(Enumerable.Repeat(1, 1), x => x, x => x, default(Func<int, int, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { reverseRange.Join(reverseRange, default(Func<int, int>), x => x, (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { reverseRange.Join(reverseRange, x => x, default(Func<int, int>), (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { reverseRange.Join(reverseRange, x => x, x => x, default(Func<int, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { reverseRange.Join(reverseRange, default(Func<int, int>), x => x, (a, b) => a + b, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { reverseRange.Join(reverseRange, x => x, default(Func<int, int>), (a, b) => a + b, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { reverseRange.Join(reverseRange, x => x, x => x, default(Func<int, int, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { reverseRange.Join(oneItemDefault, default(Func<int, int>), x => x, (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { reverseRange.Join(oneItemDefault, x => x, default(Func<int, int>), (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { reverseRange.Join(oneItemDefault, x => x, x => x, default(Func<int, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { reverseRange.Join(oneItemDefault, default(Func<int, int>), x => x, (a, b) => a + b, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { reverseRange.Join(oneItemDefault, x => x, default(Func<int, int>), (a, b) => a + b, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { reverseRange.Join(oneItemDefault, x => x, x => x, default(Func<int, int, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { reverseRange.Join(oneItemSpecific, default(Func<int, int>), x => x, (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { reverseRange.Join(oneItemSpecific, x => x, default(Func<int, int>), (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { reverseRange.Join(oneItemSpecific, x => x, x => x, default(Func<int, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { reverseRange.Join(oneItemSpecific, default(Func<int, int>), x => x, (a, b) => a + b, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { reverseRange.Join(oneItemSpecific, x => x, default(Func<int, int>), (a, b) => a + b, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { reverseRange.Join(oneItemSpecific, x => x, x => x, default(Func<int, int, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { reverseRange.Join(oneItemDefaultOrdered, default(Func<int, int>), x => x, (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { reverseRange.Join(oneItemDefaultOrdered, x => x, default(Func<int, int>), (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { reverseRange.Join(oneItemDefaultOrdered, x => x, x => x, default(Func<int, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { reverseRange.Join(oneItemDefaultOrdered, default(Func<int, int>), x => x, (a, b) => a + b, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { reverseRange.Join(oneItemDefaultOrdered, x => x, default(Func<int, int>), (a, b) => a + b, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { reverseRange.Join(oneItemDefaultOrdered, x => x, x => x, default(Func<int, int, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { reverseRange.Join(oneItemSpecificOrdered, default(Func<int, int>), x => x, (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { reverseRange.Join(oneItemSpecificOrdered, x => x, default(Func<int, int>), (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { reverseRange.Join(oneItemSpecificOrdered, x => x, x => x, default(Func<int, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { reverseRange.Join(oneItemSpecificOrdered, default(Func<int, int>), x => x, (a, b) => a + b, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { reverseRange.Join(oneItemSpecificOrdered, x => x, default(Func<int, int>), (a, b) => a + b, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { reverseRange.Join(oneItemSpecificOrdered, x => x, x => x, default(Func<int, int, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                Helper.ForEachEnumerableExpression(
                    reverseRange,
                    new int[0],
                    res => { },
                    @"(l, r) =>
                      {
                        try { l.Join(r, default(Func<int, int>), x => x, (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""outerKeySelector"", exc.ParamName); }
                        try { l.Join(r, x => x, default(Func<int, int>), (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""innerKeySelector"", exc.ParamName); }
                        try { l.Join(r, x => x, x => x, default(Func<int, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""resultSelector"", exc.ParamName); }
                        try { l.Join(r, default(Func<int, int>), x => x, (a, b) => a + b, new JoinTests._IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""outerKeySelector"", exc.ParamName); }
                        try { l.Join(r, x => x, default(Func<int, int>), (a, b) => a + b, new JoinTests._IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""innerKeySelector"", exc.ParamName); }
                        try { l.Join(r, x => x, x => x, default(Func<int, int, int>), new JoinTests._IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""resultSelector"", exc.ParamName); }

                        return Helper.NoCallValue;
                      }",
                    typeof(EmptyEnumerable<>),
                    typeof(EmptyOrderedEnumerable<>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>)
                );
            }

            // oneItemDefault
            {
                try { oneItemDefault.Join(empty, default(Func<int, int>), x => x, (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemDefault.Join(empty, x => x, default(Func<int, int>), (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemDefault.Join(empty, x => x, x => x, default(Func<int, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemDefault.Join(empty, default(Func<int, int>), x => x, (a, b) => a + b, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemDefault.Join(empty, x => x, default(Func<int, int>), (a, b) => a + b, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemDefault.Join(empty, x => x, x => x, default(Func<int, int, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { oneItemDefault.Join(emptyOrdered, default(Func<int, int>), x => x, (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemDefault.Join(emptyOrdered, x => x, default(Func<int, int>), (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemDefault.Join(emptyOrdered, x => x, x => x, default(Func<int, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemDefault.Join(emptyOrdered, default(Func<int, int>), x => x, (a, b) => a + b, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemDefault.Join(emptyOrdered, x => x, default(Func<int, int>), (a, b) => a + b, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemDefault.Join(emptyOrdered, x => x, x => x, default(Func<int, int, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { oneItemDefault.Join(range, default(Func<int, int>), x => x, (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemDefault.Join(range, x => x, default(Func<int, int>), (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemDefault.Join(range, x => x, x => x, default(Func<int, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemDefault.Join(range, default(Func<int, int>), x => x, (a, b) => a + b, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemDefault.Join(range, x => x, default(Func<int, int>), (a, b) => a + b, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemDefault.Join(range, x => x, x => x, default(Func<int, int, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { oneItemDefault.Join(Enumerable.Repeat(1, 1), default(Func<int, int>), x => x, (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemDefault.Join(Enumerable.Repeat(1, 1), x => x, default(Func<int, int>), (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemDefault.Join(Enumerable.Repeat(1, 1), x => x, x => x, default(Func<int, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemDefault.Join(Enumerable.Repeat(1, 1), default(Func<int, int>), x => x, (a, b) => a + b, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemDefault.Join(Enumerable.Repeat(1, 1), x => x, default(Func<int, int>), (a, b) => a + b, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemDefault.Join(Enumerable.Repeat(1, 1), x => x, x => x, default(Func<int, int, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { oneItemDefault.Join(reverseRange, default(Func<int, int>), x => x, (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemDefault.Join(reverseRange, x => x, default(Func<int, int>), (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemDefault.Join(reverseRange, x => x, x => x, default(Func<int, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemDefault.Join(reverseRange, default(Func<int, int>), x => x, (a, b) => a + b, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemDefault.Join(reverseRange, x => x, default(Func<int, int>), (a, b) => a + b, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemDefault.Join(reverseRange, x => x, x => x, default(Func<int, int, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { oneItemDefault.Join(oneItemDefault, default(Func<int, int>), x => x, (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemDefault.Join(oneItemDefault, x => x, default(Func<int, int>), (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemDefault.Join(oneItemDefault, x => x, x => x, default(Func<int, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemDefault.Join(oneItemDefault, default(Func<int, int>), x => x, (a, b) => a + b, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemDefault.Join(oneItemDefault, x => x, default(Func<int, int>), (a, b) => a + b, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemDefault.Join(oneItemDefault, x => x, x => x, default(Func<int, int, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { oneItemDefault.Join(oneItemSpecific, default(Func<int, int>), x => x, (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemDefault.Join(oneItemSpecific, x => x, default(Func<int, int>), (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemDefault.Join(oneItemSpecific, x => x, x => x, default(Func<int, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemDefault.Join(oneItemSpecific, default(Func<int, int>), x => x, (a, b) => a + b, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemDefault.Join(oneItemSpecific, x => x, default(Func<int, int>), (a, b) => a + b, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemDefault.Join(oneItemSpecific, x => x, x => x, default(Func<int, int, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { oneItemDefault.Join(oneItemDefaultOrdered, default(Func<int, int>), x => x, (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemDefault.Join(oneItemDefaultOrdered, x => x, default(Func<int, int>), (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemDefault.Join(oneItemDefaultOrdered, x => x, x => x, default(Func<int, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemDefault.Join(oneItemDefaultOrdered, default(Func<int, int>), x => x, (a, b) => a + b, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemDefault.Join(oneItemDefaultOrdered, x => x, default(Func<int, int>), (a, b) => a + b, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemDefault.Join(oneItemDefaultOrdered, x => x, x => x, default(Func<int, int, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { oneItemDefault.Join(oneItemSpecificOrdered, default(Func<int, int>), x => x, (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemDefault.Join(oneItemSpecificOrdered, x => x, default(Func<int, int>), (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemDefault.Join(oneItemSpecificOrdered, x => x, x => x, default(Func<int, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemDefault.Join(oneItemSpecificOrdered, default(Func<int, int>), x => x, (a, b) => a + b, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemDefault.Join(oneItemSpecificOrdered, x => x, default(Func<int, int>), (a, b) => a + b, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemDefault.Join(oneItemSpecificOrdered, x => x, x => x, default(Func<int, int, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                Helper.ForEachEnumerableExpression(
                    oneItemDefault,
                    new int[0],
                    res => { },
                    @"(l, r) =>
                      {
                        try { l.Join(r, default(Func<int, int>), x => x, (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""outerKeySelector"", exc.ParamName); }
                        try { l.Join(r, x => x, default(Func<int, int>), (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""innerKeySelector"", exc.ParamName); }
                        try { l.Join(r, x => x, x => x, default(Func<int, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""resultSelector"", exc.ParamName); }
                        try { l.Join(r, default(Func<int, int>), x => x, (a, b) => a + b, new JoinTests._IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""outerKeySelector"", exc.ParamName); }
                        try { l.Join(r, x => x, default(Func<int, int>), (a, b) => a + b, new JoinTests._IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""innerKeySelector"", exc.ParamName); }
                        try { l.Join(r, x => x, x => x, default(Func<int, int, int>), new JoinTests._IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""resultSelector"", exc.ParamName); }

                        return Helper.NoCallValue;
                      }",
                    typeof(EmptyEnumerable<>),
                    typeof(EmptyOrderedEnumerable<>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>)
                );
            }

            // oneItemSpecific
            {
                try { oneItemSpecific.Join(empty, default(Func<int, int>), x => x, (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemSpecific.Join(empty, x => x, default(Func<int, int>), (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemSpecific.Join(empty, x => x, x => x, default(Func<int, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemSpecific.Join(empty, default(Func<int, int>), x => x, (a, b) => a + b, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemSpecific.Join(empty, x => x, default(Func<int, int>), (a, b) => a + b, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemSpecific.Join(empty, x => x, x => x, default(Func<int, int, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { oneItemSpecific.Join(emptyOrdered, default(Func<int, int>), x => x, (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemSpecific.Join(emptyOrdered, x => x, default(Func<int, int>), (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemSpecific.Join(emptyOrdered, x => x, x => x, default(Func<int, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemSpecific.Join(emptyOrdered, default(Func<int, int>), x => x, (a, b) => a + b, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemSpecific.Join(emptyOrdered, x => x, default(Func<int, int>), (a, b) => a + b, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemSpecific.Join(emptyOrdered, x => x, x => x, default(Func<int, int, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { oneItemSpecific.Join(range, default(Func<int, int>), x => x, (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemSpecific.Join(range, x => x, default(Func<int, int>), (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemSpecific.Join(range, x => x, x => x, default(Func<int, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemSpecific.Join(range, default(Func<int, int>), x => x, (a, b) => a + b, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemSpecific.Join(range, x => x, default(Func<int, int>), (a, b) => a + b, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemSpecific.Join(range, x => x, x => x, default(Func<int, int, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { oneItemSpecific.Join(Enumerable.Repeat(1, 1), default(Func<int, int>), x => x, (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemSpecific.Join(Enumerable.Repeat(1, 1), x => x, default(Func<int, int>), (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemSpecific.Join(Enumerable.Repeat(1, 1), x => x, x => x, default(Func<int, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemSpecific.Join(Enumerable.Repeat(1, 1), default(Func<int, int>), x => x, (a, b) => a + b, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemSpecific.Join(Enumerable.Repeat(1, 1), x => x, default(Func<int, int>), (a, b) => a + b, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemSpecific.Join(Enumerable.Repeat(1, 1), x => x, x => x, default(Func<int, int, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { oneItemSpecific.Join(reverseRange, default(Func<int, int>), x => x, (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemSpecific.Join(reverseRange, x => x, default(Func<int, int>), (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemSpecific.Join(reverseRange, x => x, x => x, default(Func<int, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemSpecific.Join(reverseRange, default(Func<int, int>), x => x, (a, b) => a + b, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemSpecific.Join(reverseRange, x => x, default(Func<int, int>), (a, b) => a + b, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemSpecific.Join(reverseRange, x => x, x => x, default(Func<int, int, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { oneItemSpecific.Join(oneItemSpecific, default(Func<int, int>), x => x, (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemSpecific.Join(oneItemSpecific, x => x, default(Func<int, int>), (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemSpecific.Join(oneItemSpecific, x => x, x => x, default(Func<int, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemSpecific.Join(oneItemSpecific, default(Func<int, int>), x => x, (a, b) => a + b, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemSpecific.Join(oneItemSpecific, x => x, default(Func<int, int>), (a, b) => a + b, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemSpecific.Join(oneItemSpecific, x => x, x => x, default(Func<int, int, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { oneItemSpecific.Join(oneItemSpecific, default(Func<int, int>), x => x, (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemSpecific.Join(oneItemSpecific, x => x, default(Func<int, int>), (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemSpecific.Join(oneItemSpecific, x => x, x => x, default(Func<int, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemSpecific.Join(oneItemSpecific, default(Func<int, int>), x => x, (a, b) => a + b, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemSpecific.Join(oneItemSpecific, x => x, default(Func<int, int>), (a, b) => a + b, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemSpecific.Join(oneItemSpecific, x => x, x => x, default(Func<int, int, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { oneItemSpecific.Join(oneItemSpecificOrdered, default(Func<int, int>), x => x, (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemSpecific.Join(oneItemSpecificOrdered, x => x, default(Func<int, int>), (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemSpecific.Join(oneItemSpecificOrdered, x => x, x => x, default(Func<int, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemSpecific.Join(oneItemSpecificOrdered, default(Func<int, int>), x => x, (a, b) => a + b, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemSpecific.Join(oneItemSpecificOrdered, x => x, default(Func<int, int>), (a, b) => a + b, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemSpecific.Join(oneItemSpecificOrdered, x => x, x => x, default(Func<int, int, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { oneItemSpecific.Join(oneItemSpecificOrdered, default(Func<int, int>), x => x, (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemSpecific.Join(oneItemSpecificOrdered, x => x, default(Func<int, int>), (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemSpecific.Join(oneItemSpecificOrdered, x => x, x => x, default(Func<int, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemSpecific.Join(oneItemSpecificOrdered, default(Func<int, int>), x => x, (a, b) => a + b, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemSpecific.Join(oneItemSpecificOrdered, x => x, default(Func<int, int>), (a, b) => a + b, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemSpecific.Join(oneItemSpecificOrdered, x => x, x => x, default(Func<int, int, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                Helper.ForEachEnumerableExpression(
                    oneItemSpecific,
                    new int[0],
                    res => { },
                    @"(l, r) =>
                      {
                        try { l.Join(r, default(Func<int, int>), x => x, (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""outerKeySelector"", exc.ParamName); }
                        try { l.Join(r, x => x, default(Func<int, int>), (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""innerKeySelector"", exc.ParamName); }
                        try { l.Join(r, x => x, x => x, default(Func<int, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""resultSelector"", exc.ParamName); }
                        try { l.Join(r, default(Func<int, int>), x => x, (a, b) => a + b, new JoinTests._IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""outerKeySelector"", exc.ParamName); }
                        try { l.Join(r, x => x, default(Func<int, int>), (a, b) => a + b, new JoinTests._IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""innerKeySelector"", exc.ParamName); }
                        try { l.Join(r, x => x, x => x, default(Func<int, int, int>), new JoinTests._IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""resultSelector"", exc.ParamName); }

                        return Helper.NoCallValue;
                      }",
                    typeof(EmptyEnumerable<>),
                    typeof(EmptyOrderedEnumerable<>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>)
                );
            }

            // oneItemDefaultOrdered
            {
                try { oneItemDefaultOrdered.Join(empty, default(Func<int, int>), x => x, (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemDefaultOrdered.Join(empty, x => x, default(Func<int, int>), (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemDefaultOrdered.Join(empty, x => x, x => x, default(Func<int, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemDefaultOrdered.Join(empty, default(Func<int, int>), x => x, (a, b) => a + b, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemDefaultOrdered.Join(empty, x => x, default(Func<int, int>), (a, b) => a + b, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemDefaultOrdered.Join(empty, x => x, x => x, default(Func<int, int, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { oneItemDefaultOrdered.Join(emptyOrdered, default(Func<int, int>), x => x, (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemDefaultOrdered.Join(emptyOrdered, x => x, default(Func<int, int>), (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemDefaultOrdered.Join(emptyOrdered, x => x, x => x, default(Func<int, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemDefaultOrdered.Join(emptyOrdered, default(Func<int, int>), x => x, (a, b) => a + b, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemDefaultOrdered.Join(emptyOrdered, x => x, default(Func<int, int>), (a, b) => a + b, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemDefaultOrdered.Join(emptyOrdered, x => x, x => x, default(Func<int, int, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { oneItemDefaultOrdered.Join(range, default(Func<int, int>), x => x, (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemDefaultOrdered.Join(range, x => x, default(Func<int, int>), (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemDefaultOrdered.Join(range, x => x, x => x, default(Func<int, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemDefaultOrdered.Join(range, default(Func<int, int>), x => x, (a, b) => a + b, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemDefaultOrdered.Join(range, x => x, default(Func<int, int>), (a, b) => a + b, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemDefaultOrdered.Join(range, x => x, x => x, default(Func<int, int, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { oneItemDefaultOrdered.Join(Enumerable.Repeat(1, 1), default(Func<int, int>), x => x, (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemDefaultOrdered.Join(Enumerable.Repeat(1, 1), x => x, default(Func<int, int>), (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemDefaultOrdered.Join(Enumerable.Repeat(1, 1), x => x, x => x, default(Func<int, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemDefaultOrdered.Join(Enumerable.Repeat(1, 1), default(Func<int, int>), x => x, (a, b) => a + b, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemDefaultOrdered.Join(Enumerable.Repeat(1, 1), x => x, default(Func<int, int>), (a, b) => a + b, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemDefaultOrdered.Join(Enumerable.Repeat(1, 1), x => x, x => x, default(Func<int, int, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { oneItemDefaultOrdered.Join(reverseRange, default(Func<int, int>), x => x, (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemDefaultOrdered.Join(reverseRange, x => x, default(Func<int, int>), (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemDefaultOrdered.Join(reverseRange, x => x, x => x, default(Func<int, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemDefaultOrdered.Join(reverseRange, default(Func<int, int>), x => x, (a, b) => a + b, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemDefaultOrdered.Join(reverseRange, x => x, default(Func<int, int>), (a, b) => a + b, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemDefaultOrdered.Join(reverseRange, x => x, x => x, default(Func<int, int, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { oneItemDefaultOrdered.Join(oneItemDefault, default(Func<int, int>), x => x, (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemDefaultOrdered.Join(oneItemDefault, x => x, default(Func<int, int>), (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemDefaultOrdered.Join(oneItemDefault, x => x, x => x, default(Func<int, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemDefaultOrdered.Join(oneItemDefault, default(Func<int, int>), x => x, (a, b) => a + b, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemDefaultOrdered.Join(oneItemDefault, x => x, default(Func<int, int>), (a, b) => a + b, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemDefaultOrdered.Join(oneItemDefault, x => x, x => x, default(Func<int, int, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { oneItemDefaultOrdered.Join(oneItemSpecific, default(Func<int, int>), x => x, (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemDefaultOrdered.Join(oneItemSpecific, x => x, default(Func<int, int>), (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemDefaultOrdered.Join(oneItemSpecific, x => x, x => x, default(Func<int, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemDefaultOrdered.Join(oneItemSpecific, default(Func<int, int>), x => x, (a, b) => a + b, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemDefaultOrdered.Join(oneItemSpecific, x => x, default(Func<int, int>), (a, b) => a + b, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemDefaultOrdered.Join(oneItemSpecific, x => x, x => x, default(Func<int, int, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { oneItemDefaultOrdered.Join(oneItemDefaultOrdered, default(Func<int, int>), x => x, (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemDefaultOrdered.Join(oneItemDefaultOrdered, x => x, default(Func<int, int>), (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemDefaultOrdered.Join(oneItemDefaultOrdered, x => x, x => x, default(Func<int, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemDefaultOrdered.Join(oneItemDefaultOrdered, default(Func<int, int>), x => x, (a, b) => a + b, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemDefaultOrdered.Join(oneItemDefaultOrdered, x => x, default(Func<int, int>), (a, b) => a + b, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemDefaultOrdered.Join(oneItemDefaultOrdered, x => x, x => x, default(Func<int, int, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { oneItemDefaultOrdered.Join(oneItemSpecificOrdered, default(Func<int, int>), x => x, (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemDefaultOrdered.Join(oneItemSpecificOrdered, x => x, default(Func<int, int>), (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemDefaultOrdered.Join(oneItemSpecificOrdered, x => x, x => x, default(Func<int, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemDefaultOrdered.Join(oneItemSpecificOrdered, default(Func<int, int>), x => x, (a, b) => a + b, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemDefaultOrdered.Join(oneItemSpecificOrdered, x => x, default(Func<int, int>), (a, b) => a + b, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemDefaultOrdered.Join(oneItemSpecificOrdered, x => x, x => x, default(Func<int, int, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                Helper.ForEachEnumerableExpression(
                    oneItemDefaultOrdered,
                    new int[0],
                    res => { },
                    @"(l, r) =>
                      {
                        try { l.Join(r, default(Func<int, int>), x => x, (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""outerKeySelector"", exc.ParamName); }
                        try { l.Join(r, x => x, default(Func<int, int>), (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""innerKeySelector"", exc.ParamName); }
                        try { l.Join(r, x => x, x => x, default(Func<int, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""resultSelector"", exc.ParamName); }
                        try { l.Join(r, default(Func<int, int>), x => x, (a, b) => a + b, new JoinTests._IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""outerKeySelector"", exc.ParamName); }
                        try { l.Join(r, x => x, default(Func<int, int>), (a, b) => a + b, new JoinTests._IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""innerKeySelector"", exc.ParamName); }
                        try { l.Join(r, x => x, x => x, default(Func<int, int, int>), new JoinTests._IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""resultSelector"", exc.ParamName); }

                        return Helper.NoCallValue;
                      }",
                    typeof(EmptyEnumerable<>),
                    typeof(EmptyOrderedEnumerable<>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>)
                );
            }

            // oneItemSpecificOrdered
            {
                try { oneItemSpecificOrdered.Join(empty, default(Func<int, int>), x => x, (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemSpecificOrdered.Join(empty, x => x, default(Func<int, int>), (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemSpecificOrdered.Join(empty, x => x, x => x, default(Func<int, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemSpecificOrdered.Join(empty, default(Func<int, int>), x => x, (a, b) => a + b, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemSpecificOrdered.Join(empty, x => x, default(Func<int, int>), (a, b) => a + b, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemSpecificOrdered.Join(empty, x => x, x => x, default(Func<int, int, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { oneItemSpecificOrdered.Join(emptyOrdered, default(Func<int, int>), x => x, (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemSpecificOrdered.Join(emptyOrdered, x => x, default(Func<int, int>), (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemSpecificOrdered.Join(emptyOrdered, x => x, x => x, default(Func<int, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemSpecificOrdered.Join(emptyOrdered, default(Func<int, int>), x => x, (a, b) => a + b, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemSpecificOrdered.Join(emptyOrdered, x => x, default(Func<int, int>), (a, b) => a + b, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemSpecificOrdered.Join(emptyOrdered, x => x, x => x, default(Func<int, int, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { oneItemSpecificOrdered.Join(range, default(Func<int, int>), x => x, (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemSpecificOrdered.Join(range, x => x, default(Func<int, int>), (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemSpecificOrdered.Join(range, x => x, x => x, default(Func<int, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemSpecificOrdered.Join(range, default(Func<int, int>), x => x, (a, b) => a + b, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemSpecificOrdered.Join(range, x => x, default(Func<int, int>), (a, b) => a + b, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemSpecificOrdered.Join(range, x => x, x => x, default(Func<int, int, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { oneItemSpecificOrdered.Join(Enumerable.Repeat(1, 1), default(Func<int, int>), x => x, (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemSpecificOrdered.Join(Enumerable.Repeat(1, 1), x => x, default(Func<int, int>), (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemSpecificOrdered.Join(Enumerable.Repeat(1, 1), x => x, x => x, default(Func<int, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemSpecificOrdered.Join(Enumerable.Repeat(1, 1), default(Func<int, int>), x => x, (a, b) => a + b, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemSpecificOrdered.Join(Enumerable.Repeat(1, 1), x => x, default(Func<int, int>), (a, b) => a + b, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemSpecificOrdered.Join(Enumerable.Repeat(1, 1), x => x, x => x, default(Func<int, int, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { oneItemSpecificOrdered.Join(reverseRange, default(Func<int, int>), x => x, (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemSpecificOrdered.Join(reverseRange, x => x, default(Func<int, int>), (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemSpecificOrdered.Join(reverseRange, x => x, x => x, default(Func<int, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemSpecificOrdered.Join(reverseRange, default(Func<int, int>), x => x, (a, b) => a + b, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemSpecificOrdered.Join(reverseRange, x => x, default(Func<int, int>), (a, b) => a + b, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemSpecificOrdered.Join(reverseRange, x => x, x => x, default(Func<int, int, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { oneItemSpecificOrdered.Join(oneItemDefault, default(Func<int, int>), x => x, (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemSpecificOrdered.Join(oneItemDefault, x => x, default(Func<int, int>), (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemSpecificOrdered.Join(oneItemDefault, x => x, x => x, default(Func<int, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemSpecificOrdered.Join(oneItemDefault, default(Func<int, int>), x => x, (a, b) => a + b, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemSpecificOrdered.Join(oneItemDefault, x => x, default(Func<int, int>), (a, b) => a + b, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemSpecificOrdered.Join(oneItemDefault, x => x, x => x, default(Func<int, int, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { oneItemSpecificOrdered.Join(oneItemSpecific, default(Func<int, int>), x => x, (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemSpecificOrdered.Join(oneItemSpecific, x => x, default(Func<int, int>), (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemSpecificOrdered.Join(oneItemSpecific, x => x, x => x, default(Func<int, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemSpecificOrdered.Join(oneItemSpecific, default(Func<int, int>), x => x, (a, b) => a + b, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemSpecificOrdered.Join(oneItemSpecific, x => x, default(Func<int, int>), (a, b) => a + b, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemSpecificOrdered.Join(oneItemSpecific, x => x, x => x, default(Func<int, int, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { oneItemSpecificOrdered.Join(oneItemDefaultOrdered, default(Func<int, int>), x => x, (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemSpecificOrdered.Join(oneItemDefaultOrdered, x => x, default(Func<int, int>), (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemSpecificOrdered.Join(oneItemDefaultOrdered, x => x, x => x, default(Func<int, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemSpecificOrdered.Join(oneItemDefaultOrdered, default(Func<int, int>), x => x, (a, b) => a + b, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemSpecificOrdered.Join(oneItemDefaultOrdered, x => x, default(Func<int, int>), (a, b) => a + b, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemSpecificOrdered.Join(oneItemDefaultOrdered, x => x, x => x, default(Func<int, int, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                try { oneItemSpecificOrdered.Join(oneItemSpecificOrdered, default(Func<int, int>), x => x, (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemSpecificOrdered.Join(oneItemSpecificOrdered, x => x, default(Func<int, int>), (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemSpecificOrdered.Join(oneItemSpecificOrdered, x => x, x => x, default(Func<int, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemSpecificOrdered.Join(oneItemSpecificOrdered, default(Func<int, int>), x => x, (a, b) => a + b, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("outerKeySelector", exc.ParamName); }
                try { oneItemSpecificOrdered.Join(oneItemSpecificOrdered, x => x, default(Func<int, int>), (a, b) => a + b, new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("innerKeySelector", exc.ParamName); }
                try { oneItemSpecificOrdered.Join(oneItemSpecificOrdered, x => x, x => x, default(Func<int, int, int>), new _IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                Helper.ForEachEnumerableExpression(
                    oneItemSpecificOrdered,
                    new int[0],
                    res => { },
                    @"(l, r) =>
                      {
                        try { l.Join(r, default(Func<int, int>), x => x, (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""outerKeySelector"", exc.ParamName); }
                        try { l.Join(r, x => x, default(Func<int, int>), (a, b) => a + b); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""innerKeySelector"", exc.ParamName); }
                        try { l.Join(r, x => x, x => x, default(Func<int, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""resultSelector"", exc.ParamName); }
                        try { l.Join(r, default(Func<int, int>), x => x, (a, b) => a + b, new JoinTests._IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""outerKeySelector"", exc.ParamName); }
                        try { l.Join(r, x => x, default(Func<int, int>), (a, b) => a + b, new JoinTests._IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""innerKeySelector"", exc.ParamName); }
                        try { l.Join(r, x => x, x => x, default(Func<int, int, int>), new JoinTests._IntComparer()); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""resultSelector"", exc.ParamName); }

                        return Helper.NoCallValue;
                      }",
                    typeof(EmptyEnumerable<>),
                    typeof(EmptyOrderedEnumerable<>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>)
                );
            }
        }

        [TestMethod]
        public void Malformed_Default1()
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
                            Func<string, string> outerKey = str => str;
                            Func<string, string> innerKey = str => str;
                            Func<string, string, string> result = (s1, s2) => s1 + s2;
            
                            try
                            {
                                a.Join(b, outerKey, innerKey, result);
                                Assert.Fail();
                            }
                            catch(ArgumentException exc)
                            {
                                Assert.AreEqual(""""outer"""", exc.ParamName);
                            }

                            return Helper.NoCallValue;
                           }"",
                        typeof(LookupDefaultEnumerable<,>),
                        typeof(LookupSpecificEnumerable<,>),
                        typeof(GroupByDefaultEnumerable<,,,,>),
                        typeof(GroupBySpecificEnumerable<,,,,>)
                      )",
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>)
                );
            }
        }

        [TestMethod]
        public void Malformed_Default2()
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
                            Func<string, string> outerKey = str => str;
                            Func<string, string> innerKey = str => str;
                            Func<string, string, string> result = (s1, s2) => s1 + s2;

                            try
                            {
                                b.Join(a, outerKey, innerKey, result);
                                Assert.Fail();
                            }
                            catch(ArgumentException exc)
                            {
                                Assert.AreEqual(""""inner"""", exc.ParamName);
                            }

                            return Helper.NoCallValue;
                           }"",
                        typeof(LookupDefaultEnumerable<,>),
                        typeof(LookupSpecificEnumerable<,>),
                        typeof(GroupByDefaultEnumerable<,,,,>),
                        typeof(GroupBySpecificEnumerable<,,,,>)
                      )",
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>)
                );
            }
        }

        [TestMethod]
        public void Malformed_Specific1()
        {
            // specific
            {
                Helper.ForEachMalformedEnumerableExpression<string>(
                    @"a =>
                      Helper.ForEachEnumerableExpression(
                        a,
                        new string[0],
                        res => { },
                        @""(a, b) =>
                           {
                            Func<string, string> outerKey = str => str;
                            Func<string, string> innerKey = str => str;
                            Func<string, string, string> result = (s1, s2) => s1 + s2;
            
                            try
                            {
                                a.Join(b, outerKey, innerKey, result, StringComparer.InvariantCultureIgnoreCase);
                                Assert.Fail();
                            }
                            catch(ArgumentException exc)
                            {
                                Assert.AreEqual(""""outer"""", exc.ParamName);
                            }

                            return Helper.NoCallValue;
                           }"",
                        typeof(LookupDefaultEnumerable<,>),
                        typeof(LookupSpecificEnumerable<,>),
                        typeof(GroupByDefaultEnumerable<,,,,>),
                        typeof(GroupBySpecificEnumerable<,,,,>)
                      )",
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>)
                );
            }
        }

        [TestMethod]
        public void Malformed_Specific2()
        {
            // specific
            {
                Helper.ForEachMalformedEnumerableExpression<string>(
                    @"a =>
                      Helper.ForEachEnumerableExpression(
                        a,
                        new string[0],
                        res => { },
                        @""(a, b) =>
                           {
                            Func<string, string> outerKey = str => str;
                            Func<string, string> innerKey = str => str;
                            Func<string, string, string> result = (s1, s2) => s1 + s2;
            
                            try
                            {
                                b.Join(a, outerKey, innerKey, result, StringComparer.InvariantCultureIgnoreCase);
                                Assert.Fail();
                            }
                            catch(ArgumentException exc)
                            {
                                Assert.AreEqual(""""inner"""", exc.ParamName);
                            }

                            return Helper.NoCallValue;
                           }"",
                        typeof(LookupDefaultEnumerable<,>),
                        typeof(LookupSpecificEnumerable<,>),
                        typeof(GroupByDefaultEnumerable<,,,,>),
                        typeof(GroupBySpecificEnumerable<,,,,>)
                      )",
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>)
                );
            }
        }

        [TestMethod]
        public void Malformed_Weird1()
        {
            var empty = new EmptyEnumerable<int>();
            var emptyOrdered = new EmptyOrderedEnumerable<int>();
            var groupByDefault = new GroupByDefaultEnumerable<int, int, int, EmptyEnumerable<int>, EmptyEnumerator<int>>();
            var groupBySpecific = new GroupBySpecificEnumerable<int, int, int, EmptyEnumerable<int>, EmptyEnumerator<int>>();
            var lookup = new LookupDefaultEnumerable<int, int>();
            var range = new RangeEnumerable();

            // empty
            {
                var emptyGood = Enumerable.Empty<int>();
                try { empty.Join(emptyGood, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { emptyGood.Join(empty, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { empty.Join(emptyGood, x => x, x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { emptyGood.Join(empty, x => x, x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }

                var emptyOrderedGood = Enumerable.Empty<int>().OrderBy(x => x);
                try { empty.Join(emptyOrderedGood, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { emptyOrderedGood.Join(empty, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { empty.Join(emptyOrderedGood, x => x, x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { emptyOrderedGood.Join(empty, x => x, x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }

                var emptyGroupingBad = new EmptyEnumerable<GroupingEnumerable<int, int>>();
                var groupByDefaultGood = new[] { 1 }.GroupBy(x => x);
                try { emptyGroupingBad.Join(groupByDefaultGood, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { groupByDefaultGood.Join(emptyGroupingBad, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { emptyGroupingBad.Join(groupByDefaultGood, x => x, x => x, (a, b) => a, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { groupByDefaultGood.Join(emptyGroupingBad, x => x, x => x, (a, b) => a, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }

                var groupBySpecificGood = new[] { 1 }.GroupBy(x => x, new _IntComparer());
                try { emptyGroupingBad.Join(groupBySpecificGood, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { groupBySpecificGood.Join(emptyGroupingBad, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { emptyGroupingBad.Join(groupBySpecificGood, x => x, x => x, (a, b) => a, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { groupBySpecificGood.Join(emptyGroupingBad, x => x, x => x, (a, b) => a, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }

                var lookupGood = new[] { 1 }.ToLookup(x => x);
                try { emptyGroupingBad.Join(lookupGood, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { lookupGood.Join(emptyGroupingBad, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { emptyGroupingBad.Join(lookupGood, x => x, x => x, (a, b) => a, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { lookupGood.Join(emptyGroupingBad, x => x, x => x, (a, b) => a, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }

                var rangeGood = Enumerable.Range(1, 1);
                try { empty.Join(rangeGood, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { rangeGood.Join(empty, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { empty.Join(rangeGood, x => x, x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { rangeGood.Join(empty, x => x, x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }

                var repeatGood = Enumerable.Repeat(1, 1);
                try { empty.Join(repeatGood, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { repeatGood.Join(empty, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { empty.Join(repeatGood, x => x, x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { repeatGood.Join(empty, x => x, x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }

                var reverseRangeGood = Enumerable.Range(1, 1).Reverse();
                try { empty.Join(reverseRangeGood, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { reverseRangeGood.Join(empty, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { empty.Join(reverseRangeGood, x => x, x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { reverseRangeGood.Join(empty, x => x, x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }

                var oneItemDefaultGood = Enumerable.Empty<int>().DefaultIfEmpty();
                try { empty.Join(oneItemDefaultGood, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemDefaultGood.Join(empty, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { empty.Join(oneItemDefaultGood, x => x, x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemDefaultGood.Join(empty, x => x, x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }

                var oneItemSpecificGood = Enumerable.Empty<int>().DefaultIfEmpty(4);
                try { empty.Join(oneItemSpecificGood, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemSpecificGood.Join(empty, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { empty.Join(oneItemSpecificGood, x => x, x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemSpecificGood.Join(empty, x => x, x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }

                var oneItemDefaultOrderedGood = Enumerable.Empty<int>().DefaultIfEmpty().OrderBy(x => x);
                try { empty.Join(oneItemDefaultOrderedGood, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemDefaultOrderedGood.Join(empty, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { empty.Join(oneItemDefaultOrderedGood, x => x, x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemDefaultOrderedGood.Join(empty, x => x, x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }

                var oneItemSpecificOrderedGood = Enumerable.Empty<int>().DefaultIfEmpty(4).OrderBy(x => x);
                try { empty.Join(oneItemSpecificOrderedGood, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemSpecificOrderedGood.Join(empty, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { empty.Join(oneItemSpecificOrderedGood, x => x, x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemSpecificOrderedGood.Join(empty, x => x, x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }

                Helper.ForEachEnumerableExpression(
                    empty,
                    new int[0],
                    res => { },
                    @"(l, r) =>
                      {
                        try { l.Join(r, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""outer"", exc.ParamName); }
                        try { r.Join(l, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""inner"", exc.ParamName); }
                        try { l.Join(r, x => x, x => x, (a, b) => a, new JoinTests._IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""outer"", exc.ParamName); }
                        try { r.Join(l, x => x, x => x, (a, b) => a, new JoinTests._IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""inner"", exc.ParamName); }
                        
                        return Helper.NoCallValue;
                      }",
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>)
                );
            }

            // emptyOrdered
            {
                var emptyGood = Enumerable.Empty<int>();
                try { emptyOrdered.Join(emptyGood, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { emptyGood.Join(emptyOrdered, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { emptyOrdered.Join(emptyGood, x => x, x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { emptyGood.Join(emptyOrdered, x => x, x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }

                var emptyOrderedGood = Enumerable.Empty<int>().OrderBy(x => x);
                try { emptyOrdered.Join(emptyOrderedGood, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { emptyOrderedGood.Join(emptyOrdered, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { emptyOrdered.Join(emptyOrderedGood, x => x, x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { emptyOrderedGood.Join(emptyOrdered, x => x, x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }

                var emptyOrderedGroupingBad = new EmptyOrderedEnumerable<GroupingEnumerable<int, int>>();
                var groupByDefaultGood = new[] { 1 }.GroupBy(x => x);
                try { emptyOrderedGroupingBad.Join(groupByDefaultGood, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { groupByDefaultGood.Join(emptyOrderedGroupingBad, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { emptyOrderedGroupingBad.Join(groupByDefaultGood, x => x, x => x, (a, b) => a, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { groupByDefaultGood.Join(emptyOrderedGroupingBad, x => x, x => x, (a, b) => a, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }

                var groupBySpecificGood = new[] { 1 }.GroupBy(x => x, new _IntComparer());
                try { emptyOrderedGroupingBad.Join(groupBySpecificGood, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { groupBySpecificGood.Join(emptyOrderedGroupingBad, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { emptyOrderedGroupingBad.Join(groupBySpecificGood, x => x, x => x, (a, b) => a, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { groupBySpecificGood.Join(emptyOrderedGroupingBad, x => x, x => x, (a, b) => a, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }

                var lookupGood = new[] { 1 }.ToLookup(x => x);
                try { emptyOrderedGroupingBad.Join(lookupGood, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { lookupGood.Join(emptyOrderedGroupingBad, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { emptyOrderedGroupingBad.Join(lookupGood, x => x, x => x, (a, b) => a, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { lookupGood.Join(emptyOrderedGroupingBad, x => x, x => x, (a, b) => a, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }

                var rangeGood = Enumerable.Range(1, 1);
                try { emptyOrdered.Join(rangeGood, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { rangeGood.Join(emptyOrdered, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { emptyOrdered.Join(rangeGood, x => x, x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { rangeGood.Join(emptyOrdered, x => x, x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }

                var repeatGood = Enumerable.Repeat(1, 1);
                try { emptyOrdered.Join(repeatGood, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { repeatGood.Join(emptyOrdered, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { emptyOrdered.Join(repeatGood, x => x, x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { repeatGood.Join(emptyOrdered, x => x, x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }

                var reverseRangeGood = Enumerable.Range(1, 1).Reverse();
                try { emptyOrdered.Join(reverseRangeGood, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { reverseRangeGood.Join(emptyOrdered, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { emptyOrdered.Join(reverseRangeGood, x => x, x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { reverseRangeGood.Join(emptyOrdered, x => x, x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }

                var oneItemDefaultGood = Enumerable.Empty<int>().DefaultIfEmpty();
                try { emptyOrdered.Join(oneItemDefaultGood, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemDefaultGood.Join(emptyOrdered, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { emptyOrdered.Join(oneItemDefaultGood, x => x, x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemDefaultGood.Join(emptyOrdered, x => x, x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }

                var oneItemSpecificGood = Enumerable.Empty<int>().DefaultIfEmpty(4);
                try { emptyOrdered.Join(oneItemSpecificGood, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemSpecificGood.Join(emptyOrdered, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { emptyOrdered.Join(oneItemSpecificGood, x => x, x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemSpecificGood.Join(emptyOrdered, x => x, x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }

                var oneItemDefaultOrderedGood = Enumerable.Empty<int>().DefaultIfEmpty().OrderBy(x => x);
                try { emptyOrdered.Join(oneItemDefaultOrderedGood, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemDefaultOrderedGood.Join(emptyOrdered, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { emptyOrdered.Join(oneItemDefaultOrderedGood, x => x, x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemDefaultOrderedGood.Join(emptyOrdered, x => x, x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }

                var oneItemSpecificOrderedGood = Enumerable.Empty<int>().DefaultIfEmpty(4).OrderBy(x => x);
                try { emptyOrdered.Join(oneItemSpecificOrderedGood, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemSpecificOrderedGood.Join(emptyOrdered, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { emptyOrdered.Join(oneItemSpecificOrderedGood, x => x, x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemSpecificOrderedGood.Join(emptyOrdered, x => x, x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }

                Helper.ForEachEnumerableExpression(
                    emptyOrdered,
                    new int[0],
                    res => { },
                    @"(l, r) =>
                      {
                        try { l.Join(r, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""outer"", exc.ParamName); }
                        try { r.Join(l, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""inner"", exc.ParamName); }
                        try { l.Join(r, x => x, x => x, (a, b) => a, new JoinTests._IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""outer"", exc.ParamName); }
                        try { r.Join(l, x => x, x => x, (a, b) => a, new JoinTests._IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""inner"", exc.ParamName); }
                        
                        return Helper.NoCallValue;
                      }",
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>)
                );
            }

            // groupByDefault
            {
                var emptyGood = Enumerable.Empty<GroupingEnumerable<int, int>>();
                try { groupByDefault.Join(emptyGood, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { emptyGood.Join(groupByDefault, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { groupByDefault.Join(emptyGood, x => x, x => x, (a, b) => a, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { emptyGood.Join(groupByDefault, x => x, x => x, (a, b) => a, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }

                var emptyOrderedGood = Enumerable.Empty<GroupingEnumerable<int, int>>().OrderBy(x => x);
                try { groupByDefault.Join(emptyOrderedGood, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { emptyOrderedGood.Join(groupByDefault, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { groupByDefault.Join(emptyOrderedGood, x => x, x => x, (a, b) => a, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { emptyOrderedGood.Join(groupByDefault, x => x, x => x, (a, b) => a, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }

                var groupByDefaultGood = new[] { 1 }.GroupBy(x => x);
                try { groupByDefault.Join(groupByDefaultGood, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { groupByDefaultGood.Join(groupByDefault, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { groupByDefault.Join(groupByDefaultGood, x => x, x => x, (a, b) => a, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { groupByDefaultGood.Join(groupByDefault, x => x, x => x, (a, b) => a, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }

                var groupBySpecificGood = new[] { 1 }.GroupBy(x => x, new _IntComparer());
                try { groupByDefault.Join(groupBySpecificGood, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { groupBySpecificGood.Join(groupByDefault, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { groupByDefault.Join(groupBySpecificGood, x => x, x => x, (a, b) => a, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { groupBySpecificGood.Join(groupByDefault, x => x, x => x, (a, b) => a, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }

                var lookupGood = new[] { 1 }.ToLookup(x => x);
                try { groupByDefault.Join(lookupGood, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { lookupGood.Join(groupByDefault, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { groupByDefault.Join(lookupGood, x => x, x => x, (a, b) => a, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { lookupGood.Join(groupByDefault, x => x, x => x, (a, b) => a, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }

                var repeatGood = Enumerable.Repeat(lookupGood.First(), 1);
                try { groupByDefault.Join(repeatGood, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { repeatGood.Join(groupByDefault, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { groupByDefault.Join(repeatGood, x => x, x => x, (a, b) => a, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { repeatGood.Join(groupByDefault, x => x, x => x, (a, b) => a, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }

                var oneItemDefaultGood = Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty();
                try { groupByDefault.Join(oneItemDefaultGood, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemDefaultGood.Join(groupByDefault, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { groupByDefault.Join(oneItemDefaultGood, x => x, x => x, (a, b) => a, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemDefaultGood.Join(groupByDefault, x => x, x => x, (a, b) => a, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }

                var oneItemSpecificGood = Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty(new[] { 1 }.GroupBy(x => x).First());
                try { groupByDefault.Join(oneItemSpecificGood, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemSpecificGood.Join(groupByDefault, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { groupByDefault.Join(oneItemSpecificGood, x => x, x => x, (a, b) => a, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemSpecificGood.Join(groupByDefault, x => x, x => x, (a, b) => a, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }

                var oneItemDefaultOrderedGood = Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty().OrderBy(x => x);
                try { groupByDefault.Join(oneItemDefaultOrderedGood, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemDefaultOrderedGood.Join(groupByDefault, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { groupByDefault.Join(oneItemDefaultOrderedGood, x => x, x => x, (a, b) => a, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemDefaultOrderedGood.Join(groupByDefault, x => x, x => x, (a, b) => a, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }

                var oneItemSpecificOrderedGood = Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty(new[] { 1 }.GroupBy(x => x).First()).OrderBy(x => x);
                try { groupByDefault.Join(oneItemSpecificOrderedGood, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemSpecificOrderedGood.Join(groupByDefault, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { groupByDefault.Join(oneItemSpecificOrderedGood, x => x, x => x, (a, b) => a, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemSpecificOrderedGood.Join(groupByDefault, x => x, x => x, (a, b) => a, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }

                Helper.ForEachEnumerableExpression(
                    groupByDefault,
                    new[] { lookupGood.First() },
                    res => { },
                    @"(l, r) =>
                      {
                        try { l.Join(r, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""outer"", exc.ParamName); }
                        try { r.Join(l, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""inner"", exc.ParamName); }
                        try { l.Join(r, x => x, x => x, (a, b) => a, new JoinTests._GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""outer"", exc.ParamName); }
                        try { r.Join(l, x => x, x => x, (a, b) => a, new JoinTests._GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""inner"", exc.ParamName); }
                        
                        return Helper.NoCallValue;
                      }",
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>)
                );
            }

            // groupBySpecific
            {
                var emptyGood = Enumerable.Empty<GroupingEnumerable<int, int>>();
                try { groupBySpecific.Join(emptyGood, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { emptyGood.Join(groupBySpecific, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { groupBySpecific.Join(emptyGood, x => x, x => x, (a, b) => a, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { emptyGood.Join(groupBySpecific, x => x, x => x, (a, b) => a, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }

                var emptyOrderedGood = Enumerable.Empty<GroupingEnumerable<int, int>>().OrderBy(x => x);
                try { groupBySpecific.Join(emptyOrderedGood, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { emptyOrderedGood.Join(groupBySpecific, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { groupBySpecific.Join(emptyOrderedGood, x => x, x => x, (a, b) => a, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { emptyOrderedGood.Join(groupBySpecific, x => x, x => x, (a, b) => a, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }

                var groupByDefaultGood = new[] { 1 }.GroupBy(x => x);
                try { groupBySpecific.Join(groupByDefaultGood, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { groupByDefaultGood.Join(groupBySpecific, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { groupBySpecific.Join(groupByDefaultGood, x => x, x => x, (a, b) => a, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { groupByDefaultGood.Join(groupBySpecific, x => x, x => x, (a, b) => a, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }

                var groupBySpecificGood = new[] { 1 }.GroupBy(x => x, new _IntComparer());
                try { groupBySpecific.Join(groupBySpecificGood, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { groupBySpecificGood.Join(groupBySpecific, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { groupBySpecific.Join(groupBySpecificGood, x => x, x => x, (a, b) => a, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { groupBySpecificGood.Join(groupBySpecific, x => x, x => x, (a, b) => a, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }

                var lookupGood = new[] { 1 }.ToLookup(x => x);
                try { groupBySpecific.Join(lookupGood, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { lookupGood.Join(groupBySpecific, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { groupBySpecific.Join(lookupGood, x => x, x => x, (a, b) => a, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { lookupGood.Join(groupBySpecific, x => x, x => x, (a, b) => a, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }

                var repeatGood = Enumerable.Repeat(lookupGood.First(), 1);
                try { groupBySpecific.Join(repeatGood, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { repeatGood.Join(groupBySpecific, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { groupBySpecific.Join(repeatGood, x => x, x => x, (a, b) => a, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { repeatGood.Join(groupBySpecific, x => x, x => x, (a, b) => a, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }

                var oneItemDefaultGood = Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty();
                try { groupBySpecific.Join(oneItemDefaultGood, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemDefaultGood.Join(groupBySpecific, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { groupBySpecific.Join(oneItemDefaultGood, x => x, x => x, (a, b) => a, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemDefaultGood.Join(groupBySpecific, x => x, x => x, (a, b) => a, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }

                var oneItemSpecificGood = Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty(new[] { 1 }.GroupBy(x => x).First());
                try { groupBySpecific.Join(oneItemSpecificGood, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemSpecificGood.Join(groupBySpecific, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { groupBySpecific.Join(oneItemSpecificGood, x => x, x => x, (a, b) => a, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemSpecificGood.Join(groupBySpecific, x => x, x => x, (a, b) => a, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }

                var oneItemDefaultOrderedGood = Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty().OrderBy(x => x);
                try { groupBySpecific.Join(oneItemDefaultOrderedGood, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemDefaultOrderedGood.Join(groupBySpecific, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { groupBySpecific.Join(oneItemDefaultOrderedGood, x => x, x => x, (a, b) => a, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemDefaultOrderedGood.Join(groupBySpecific, x => x, x => x, (a, b) => a, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }

                var oneItemSpecificOrderedGood = Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty(new[] { 1 }.GroupBy(x => x).First()).OrderBy(x => x);
                try { groupBySpecific.Join(oneItemSpecificOrderedGood, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemSpecificOrderedGood.Join(groupBySpecific, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { groupBySpecific.Join(oneItemSpecificOrderedGood, x => x, x => x, (a, b) => a, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemSpecificOrderedGood.Join(groupBySpecific, x => x, x => x, (a, b) => a, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }

                Helper.ForEachEnumerableExpression(
                    groupBySpecific,
                    new[] { lookupGood.First() },
                    res => { },
                    @"(l, r) =>
                      {
                        try { l.Join(r, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""outer"", exc.ParamName); }
                        try { r.Join(l, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""inner"", exc.ParamName); }
                        try { l.Join(r, x => x, x => x, (a, b) => a, new JoinTests._GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""outer"", exc.ParamName); }
                        try { r.Join(l, x => x, x => x, (a, b) => a, new JoinTests._GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""inner"", exc.ParamName); }
                        
                        return Helper.NoCallValue;
                      }",
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>)
                );
            }

            // lookup
            {
                var emptyGood = Enumerable.Empty<GroupingEnumerable<int, int>>();
                try { lookup.Join(emptyGood, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { emptyGood.Join(lookup, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { lookup.Join(emptyGood, x => x, x => x, (a, b) => a, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { emptyGood.Join(lookup, x => x, x => x, (a, b) => a, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }

                var emptyOrderedGood = Enumerable.Empty<GroupingEnumerable<int, int>>().OrderBy(x => x);
                try { lookup.Join(emptyOrderedGood, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { emptyOrderedGood.Join(lookup, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { lookup.Join(emptyOrderedGood, x => x, x => x, (a, b) => a, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { emptyOrderedGood.Join(lookup, x => x, x => x, (a, b) => a, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }

                var groupByDefaultGood = new[] { 1 }.GroupBy(x => x);
                try { lookup.Join(groupByDefaultGood, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { groupByDefaultGood.Join(lookup, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { lookup.Join(groupByDefaultGood, x => x, x => x, (a, b) => a, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { groupByDefaultGood.Join(lookup, x => x, x => x, (a, b) => a, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }

                var groupBySpecificGood = new[] { 1 }.GroupBy(x => x, new _IntComparer());
                try { lookup.Join(groupBySpecificGood, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { groupBySpecificGood.Join(lookup, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { lookup.Join(groupBySpecificGood, x => x, x => x, (a, b) => a, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { groupBySpecificGood.Join(lookup, x => x, x => x, (a, b) => a, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }

                var lookupGood = new[] { 1 }.ToLookup(x => x);
                try { lookup.Join(lookupGood, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { lookupGood.Join(lookup, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { lookup.Join(lookupGood, x => x, x => x, (a, b) => a, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { lookupGood.Join(lookup, x => x, x => x, (a, b) => a, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }

                var repeatGood = Enumerable.Repeat(lookupGood.First(), 1);
                try { lookup.Join(repeatGood, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { repeatGood.Join(lookup, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { lookup.Join(repeatGood, x => x, x => x, (a, b) => a, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { repeatGood.Join(lookup, x => x, x => x, (a, b) => a, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }

                var oneItemDefaultGood = Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty();
                try { lookup.Join(oneItemDefaultGood, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemDefaultGood.Join(lookup, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { lookup.Join(oneItemDefaultGood, x => x, x => x, (a, b) => a, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemDefaultGood.Join(lookup, x => x, x => x, (a, b) => a, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }

                var oneItemSpecificGood = Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty(new[] { 1 }.GroupBy(x => x).First());
                try { lookup.Join(oneItemSpecificGood, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemSpecificGood.Join(lookup, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { lookup.Join(oneItemSpecificGood, x => x, x => x, (a, b) => a, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemSpecificGood.Join(lookup, x => x, x => x, (a, b) => a, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }

                var oneItemDefaultOrderedGood = Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty().OrderBy(x => x);
                try { lookup.Join(oneItemDefaultOrderedGood, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemDefaultOrderedGood.Join(lookup, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { lookup.Join(oneItemDefaultOrderedGood, x => x, x => x, (a, b) => a, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemDefaultOrderedGood.Join(lookup, x => x, x => x, (a, b) => a, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }

                var oneItemSpecificOrderedGood = Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty(new[] { 1 }.GroupBy(x => x).First()).OrderBy(x => x);
                try { lookup.Join(oneItemSpecificOrderedGood, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemSpecificOrderedGood.Join(lookup, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { lookup.Join(oneItemSpecificOrderedGood, x => x, x => x, (a, b) => a, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemSpecificOrderedGood.Join(lookup, x => x, x => x, (a, b) => a, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }

                Helper.ForEachEnumerableExpression(
                    lookup,
                    new[] { lookupGood.First() },
                    res => { },
                    @"(l, r) =>
                      {
                        try { l.Join(r, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""outer"", exc.ParamName); }
                        try { r.Join(l, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""inner"", exc.ParamName); }
                        try { l.Join(r, x => x, x => x, (a, b) => a, new JoinTests._GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""outer"", exc.ParamName); }
                        try { r.Join(l, x => x, x => x, (a, b) => a, new JoinTests._GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""inner"", exc.ParamName); }
                        
                        return Helper.NoCallValue;
                      }",
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>)
                );
            }

            // range
            {
                var emptyGood = Enumerable.Empty<int>();
                try { range.Join(emptyGood, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { emptyGood.Join(range, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { range.Join(emptyGood, x => x, x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { emptyGood.Join(range, x => x, x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }

                var emptyOrderedGood = Enumerable.Empty<int>().OrderBy(x => x);
                try { range.Join(emptyOrderedGood, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { emptyOrderedGood.Join(range, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { range.Join(emptyOrderedGood, x => x, x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { emptyOrderedGood.Join(range, x => x, x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }

                var rangeGood = Enumerable.Range(1, 1);
                try { range.Join(rangeGood, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { rangeGood.Join(range, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { range.Join(rangeGood, x => x, x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { rangeGood.Join(range, x => x, x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }

                var repeatGood = Enumerable.Repeat(1, 1);
                try { range.Join(repeatGood, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { repeatGood.Join(range, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { range.Join(repeatGood, x => x, x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { repeatGood.Join(range, x => x, x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }

                var reverseRangeGood = Enumerable.Range(1, 1).Reverse();
                try { range.Join(reverseRangeGood, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { reverseRangeGood.Join(range, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { range.Join(reverseRangeGood, x => x, x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { reverseRangeGood.Join(range, x => x, x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }

                var oneItemDefaultGood = Enumerable.Empty<int>().DefaultIfEmpty();
                try { range.Join(oneItemDefaultGood, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemDefaultGood.Join(range, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { range.Join(oneItemDefaultGood, x => x, x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemDefaultGood.Join(range, x => x, x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }

                var oneItemSpecificGood = Enumerable.Empty<int>().DefaultIfEmpty(4);
                try { range.Join(oneItemSpecificGood, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemSpecificGood.Join(range, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { range.Join(oneItemSpecificGood, x => x, x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemSpecificGood.Join(range, x => x, x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }

                var oneItemDefaultOrderedGood = Enumerable.Empty<int>().DefaultIfEmpty().OrderBy(x => x);
                try { range.Join(oneItemDefaultOrderedGood, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemDefaultOrderedGood.Join(range, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { range.Join(oneItemDefaultOrderedGood, x => x, x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemDefaultOrderedGood.Join(range, x => x, x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }

                var oneItemSpecificOrderedGood = Enumerable.Empty<int>().DefaultIfEmpty(4).OrderBy(x => x);
                try { range.Join(oneItemSpecificOrderedGood, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemSpecificOrderedGood.Join(range, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { range.Join(oneItemSpecificOrderedGood, x => x, x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemSpecificOrderedGood.Join(range, x => x, x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }

                Helper.ForEachEnumerableExpression(
                    range,
                    new int[0],
                    res => { },
                    @"(l, r) =>
                      {
                        try { l.Join(r, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""outer"", exc.ParamName); }
                        try { r.Join(l, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""inner"", exc.ParamName); }
                        try { l.Join(r, x => x, x => x, (a, b) => a, new JoinTests._IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""outer"", exc.ParamName); }
                        try { r.Join(l, x => x, x => x, (a, b) => a, new JoinTests._IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""inner"", exc.ParamName); }
                        
                        return Helper.NoCallValue;
                      }",
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>)
                );
            }
        }

        [TestMethod]
        public void Malformed_Weird2()
        {
            var repeat = new RepeatEnumerable<int>();
            var reverseRange = new ReverseRangeEnumerable();
            var oneItemDefault = new OneItemDefaultEnumerable<int>();
            var oneItemSpecific = new OneItemSpecificEnumerable<int>();
            var oneItemDefaultOrdered = new OneItemDefaultOrderedEnumerable<int>();
            var oneItemSpecificOrdered = new OneItemSpecificOrderedEnumerable<int>();

            // repeat
            {
                var emptyGood = Enumerable.Empty<int>();
                try { repeat.Join(emptyGood, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { emptyGood.Join(repeat, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { repeat.Join(emptyGood, x => x, x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { emptyGood.Join(repeat, x => x, x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }

                var emptyOrderedGood = Enumerable.Empty<int>().OrderBy(x => x);
                try { repeat.Join(emptyOrderedGood, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { emptyOrderedGood.Join(repeat, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { repeat.Join(emptyOrderedGood, x => x, x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { emptyOrderedGood.Join(repeat, x => x, x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }

                var repeatGroupingBad = new RepeatEnumerable<GroupingEnumerable<int, int>>();
                var groupByDefaultGood = new[] { 1 }.GroupBy(x => x);
                try { repeatGroupingBad.Join(groupByDefaultGood, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { groupByDefaultGood.Join(repeatGroupingBad, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { repeatGroupingBad.Join(groupByDefaultGood, x => x, x => x, (a, b) => a, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { groupByDefaultGood.Join(repeatGroupingBad, x => x, x => x, (a, b) => a, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }

                var groupBySpecificGood = new[] { 1 }.GroupBy(x => x, new _IntComparer());
                try { repeatGroupingBad.Join(groupBySpecificGood, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { groupBySpecificGood.Join(repeatGroupingBad, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { repeatGroupingBad.Join(groupBySpecificGood, x => x, x => x, (a, b) => a, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { groupBySpecificGood.Join(repeatGroupingBad, x => x, x => x, (a, b) => a, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }

                var lookupGood = new[] { 1 }.ToLookup(x => x);
                try { repeatGroupingBad.Join(lookupGood, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { lookupGood.Join(repeatGroupingBad, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { repeatGroupingBad.Join(lookupGood, x => x, x => x, (a, b) => a, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { lookupGood.Join(repeatGroupingBad, x => x, x => x, (a, b) => a, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }

                var rangeGood = Enumerable.Range(1, 1);
                try { repeat.Join(rangeGood, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { rangeGood.Join(repeat, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { repeat.Join(rangeGood, x => x, x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { rangeGood.Join(repeat, x => x, x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }

                var repeatGood = Enumerable.Repeat(1, 1);
                try { repeat.Join(repeatGood, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { repeatGood.Join(repeat, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { repeat.Join(repeatGood, x => x, x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { repeatGood.Join(repeat, x => x, x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }

                var reverseRangeGood = Enumerable.Range(1, 1).Reverse();
                try { repeat.Join(reverseRangeGood, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { reverseRangeGood.Join(repeat, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { repeat.Join(reverseRangeGood, x => x, x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { reverseRangeGood.Join(repeat, x => x, x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }

                var oneItemDefaultGood = Enumerable.Empty<int>().DefaultIfEmpty();
                try { repeat.Join(oneItemDefaultGood, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemDefaultGood.Join(repeat, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { repeat.Join(oneItemDefaultGood, x => x, x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemDefaultGood.Join(repeat, x => x, x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }

                var oneItemSpecificGood = Enumerable.Empty<int>().DefaultIfEmpty(4);
                try { repeat.Join(oneItemSpecificGood, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemSpecificGood.Join(repeat, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { repeat.Join(oneItemSpecificGood, x => x, x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemSpecificGood.Join(repeat, x => x, x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }

                var oneItemDefaultOrderedGood = Enumerable.Empty<int>().DefaultIfEmpty().OrderBy(x => x);
                try { repeat.Join(oneItemDefaultOrderedGood, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemDefaultOrderedGood.Join(repeat, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { repeat.Join(oneItemDefaultOrderedGood, x => x, x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemDefaultOrderedGood.Join(repeat, x => x, x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }

                var oneItemSpecificOrderedGood = Enumerable.Empty<int>().DefaultIfEmpty(4).OrderBy(x => x);
                try { repeat.Join(oneItemSpecificOrderedGood, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemSpecificOrderedGood.Join(repeat, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { repeat.Join(oneItemSpecificOrderedGood, x => x, x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemSpecificOrderedGood.Join(repeat, x => x, x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }

                Helper.ForEachEnumerableExpression(
                    repeat,
                    new int[0],
                    res => { },
                    @"(l, r) =>
                      {
                        try { l.Join(r, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""outer"", exc.ParamName); }
                        try { r.Join(l, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""inner"", exc.ParamName); }
                        try { l.Join(r, x => x, x => x, (a, b) => a, new JoinTests._IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""outer"", exc.ParamName); }
                        try { r.Join(l, x => x, x => x, (a, b) => a, new JoinTests._IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""inner"", exc.ParamName); }
                        
                        return Helper.NoCallValue;
                      }",
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>)
                );
            }

            // reverseRange
            {
                var emptyGood = Enumerable.Empty<int>();
                try { reverseRange.Join(emptyGood, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { emptyGood.Join(reverseRange, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { reverseRange.Join(emptyGood, x => x, x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { emptyGood.Join(reverseRange, x => x, x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }

                var emptyOrderedGood = Enumerable.Empty<int>().OrderBy(x => x);
                try { reverseRange.Join(emptyOrderedGood, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { emptyOrderedGood.Join(reverseRange, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { reverseRange.Join(emptyOrderedGood, x => x, x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { emptyOrderedGood.Join(reverseRange, x => x, x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }

                var rangeGood = Enumerable.Range(1, 1);
                try { reverseRange.Join(rangeGood, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { rangeGood.Join(reverseRange, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { reverseRange.Join(rangeGood, x => x, x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { rangeGood.Join(reverseRange, x => x, x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }

                var repeatGood = Enumerable.Repeat(1, 1);
                try { reverseRange.Join(repeatGood, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { repeatGood.Join(reverseRange, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { reverseRange.Join(repeatGood, x => x, x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { repeatGood.Join(reverseRange, x => x, x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }

                var reverseRangeGood = Enumerable.Range(1, 1).Reverse();
                try { reverseRange.Join(reverseRangeGood, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { reverseRangeGood.Join(reverseRange, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { reverseRange.Join(reverseRangeGood, x => x, x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { reverseRangeGood.Join(reverseRange, x => x, x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }

                var oneItemDefaultGood = Enumerable.Empty<int>().DefaultIfEmpty();
                try { reverseRange.Join(oneItemDefaultGood, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemDefaultGood.Join(reverseRange, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { reverseRange.Join(oneItemDefaultGood, x => x, x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemDefaultGood.Join(reverseRange, x => x, x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }

                var oneItemSpecificGood = Enumerable.Empty<int>().DefaultIfEmpty(4);
                try { reverseRange.Join(oneItemSpecificGood, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemSpecificGood.Join(reverseRange, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { reverseRange.Join(oneItemSpecificGood, x => x, x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemSpecificGood.Join(reverseRange, x => x, x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }

                var oneItemDefaultOrderedGood = Enumerable.Empty<int>().DefaultIfEmpty().OrderBy(x => x);
                try { reverseRange.Join(oneItemDefaultOrderedGood, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemDefaultOrderedGood.Join(reverseRange, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { reverseRange.Join(oneItemDefaultOrderedGood, x => x, x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemDefaultOrderedGood.Join(reverseRange, x => x, x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }

                var oneItemSpecificOrderedGood = Enumerable.Empty<int>().DefaultIfEmpty(4).OrderBy(x => x);
                try { reverseRange.Join(oneItemSpecificOrderedGood, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemSpecificOrderedGood.Join(reverseRange, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { reverseRange.Join(oneItemSpecificOrderedGood, x => x, x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemSpecificOrderedGood.Join(reverseRange, x => x, x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }

                Helper.ForEachEnumerableExpression(
                    reverseRange,
                    new int[0],
                    res => { },
                    @"(l, r) =>
                      {
                        try { l.Join(r, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""outer"", exc.ParamName); }
                        try { r.Join(l, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""inner"", exc.ParamName); }
                        try { l.Join(r, x => x, x => x, (a, b) => a, new JoinTests._IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""outer"", exc.ParamName); }
                        try { r.Join(l, x => x, x => x, (a, b) => a, new JoinTests._IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""inner"", exc.ParamName); }
                        
                        return Helper.NoCallValue;
                      }",
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>)
                );
            }

            // oneItemDefault
            {
                var emptyGood = Enumerable.Empty<int>();
                try { oneItemDefault.Join(emptyGood, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { emptyGood.Join(oneItemDefault, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { oneItemDefault.Join(emptyGood, x => x, x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { emptyGood.Join(oneItemDefault, x => x, x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }

                var emptyOrderedGood = Enumerable.Empty<int>().OrderBy(x => x);
                try { oneItemDefault.Join(emptyOrderedGood, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { emptyOrderedGood.Join(oneItemDefault, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { oneItemDefault.Join(emptyOrderedGood, x => x, x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { emptyOrderedGood.Join(oneItemDefault, x => x, x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }

                var rangeGood = Enumerable.Range(1, 1);
                try { oneItemDefault.Join(rangeGood, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { rangeGood.Join(oneItemDefault, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { oneItemDefault.Join(rangeGood, x => x, x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { rangeGood.Join(oneItemDefault, x => x, x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }

                var repeatGood = Enumerable.Repeat(1, 1);
                try { oneItemDefault.Join(repeatGood, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { repeatGood.Join(oneItemDefault, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { oneItemDefault.Join(repeatGood, x => x, x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { repeatGood.Join(oneItemDefault, x => x, x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }

                var reverseRangeGood = Enumerable.Range(1, 1).Reverse();
                try { oneItemDefault.Join(reverseRangeGood, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { reverseRangeGood.Join(oneItemDefault, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { oneItemDefault.Join(reverseRangeGood, x => x, x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { reverseRangeGood.Join(oneItemDefault, x => x, x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }

                var oneItemDefaultGood = Enumerable.Empty<int>().DefaultIfEmpty();
                try { oneItemDefault.Join(oneItemDefaultGood, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemDefaultGood.Join(oneItemDefault, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { oneItemDefault.Join(oneItemDefaultGood, x => x, x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemDefaultGood.Join(oneItemDefault, x => x, x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }

                var oneItemSpecificGood = Enumerable.Empty<int>().DefaultIfEmpty(4);
                try { oneItemDefault.Join(oneItemSpecificGood, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemSpecificGood.Join(oneItemDefault, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { oneItemDefault.Join(oneItemSpecificGood, x => x, x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemSpecificGood.Join(oneItemDefault, x => x, x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }

                var oneItemDefaultOrderedGood = Enumerable.Empty<int>().DefaultIfEmpty().OrderBy(x => x);
                try { oneItemDefault.Join(oneItemDefaultOrderedGood, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemDefaultOrderedGood.Join(oneItemDefault, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { oneItemDefault.Join(oneItemDefaultOrderedGood, x => x, x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemDefaultOrderedGood.Join(oneItemDefault, x => x, x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }

                var oneItemSpecificOrderedGood = Enumerable.Empty<int>().DefaultIfEmpty(4).OrderBy(x => x);
                try { oneItemDefault.Join(oneItemSpecificOrderedGood, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemSpecificOrderedGood.Join(oneItemDefault, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { oneItemDefault.Join(oneItemSpecificOrderedGood, x => x, x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemSpecificOrderedGood.Join(oneItemDefault, x => x, x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }

                Helper.ForEachEnumerableExpression(
                    oneItemDefault,
                    new int[0],
                    res => { },
                    @"(l, r) =>
                      {
                        try { l.Join(r, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""outer"", exc.ParamName); }
                        try { r.Join(l, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""inner"", exc.ParamName); }
                        try { l.Join(r, x => x, x => x, (a, b) => a, new JoinTests._IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""outer"", exc.ParamName); }
                        try { r.Join(l, x => x, x => x, (a, b) => a, new JoinTests._IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""inner"", exc.ParamName); }
                        
                        return Helper.NoCallValue;
                      }",
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>)
                );
            }

            // oneItemSpecific
            {
                var emptyGood = Enumerable.Empty<int>();
                try { oneItemSpecific.Join(emptyGood, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { emptyGood.Join(oneItemSpecific, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { oneItemSpecific.Join(emptyGood, x => x, x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { emptyGood.Join(oneItemSpecific, x => x, x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }

                var emptyOrderedGood = Enumerable.Empty<int>().OrderBy(x => x);
                try { oneItemSpecific.Join(emptyOrderedGood, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { emptyOrderedGood.Join(oneItemSpecific, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { oneItemSpecific.Join(emptyOrderedGood, x => x, x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { emptyOrderedGood.Join(oneItemSpecific, x => x, x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }

                var rangeGood = Enumerable.Range(1, 1);
                try { oneItemSpecific.Join(rangeGood, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { rangeGood.Join(oneItemSpecific, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { oneItemSpecific.Join(rangeGood, x => x, x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { rangeGood.Join(oneItemSpecific, x => x, x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }

                var repeatGood = Enumerable.Repeat(1, 1);
                try { oneItemSpecific.Join(repeatGood, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { repeatGood.Join(oneItemSpecific, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { oneItemSpecific.Join(repeatGood, x => x, x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { repeatGood.Join(oneItemSpecific, x => x, x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }

                var reverseRangeGood = Enumerable.Range(1, 1).Reverse();
                try { oneItemSpecific.Join(reverseRangeGood, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { reverseRangeGood.Join(oneItemSpecific, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { oneItemSpecific.Join(reverseRangeGood, x => x, x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { reverseRangeGood.Join(oneItemSpecific, x => x, x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }

                var oneItemDefaultGood = Enumerable.Empty<int>().DefaultIfEmpty();
                try { oneItemSpecific.Join(oneItemDefaultGood, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemDefaultGood.Join(oneItemSpecific, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { oneItemSpecific.Join(oneItemDefaultGood, x => x, x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemDefaultGood.Join(oneItemSpecific, x => x, x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }

                var oneItemSpecificGood = Enumerable.Empty<int>().DefaultIfEmpty(4);
                try { oneItemSpecific.Join(oneItemSpecificGood, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemSpecificGood.Join(oneItemSpecific, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { oneItemSpecific.Join(oneItemSpecificGood, x => x, x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemSpecificGood.Join(oneItemSpecific, x => x, x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }

                var oneItemDefaultOrderedGood = Enumerable.Empty<int>().DefaultIfEmpty().OrderBy(x => x);
                try { oneItemSpecific.Join(oneItemDefaultOrderedGood, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemDefaultOrderedGood.Join(oneItemSpecific, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { oneItemSpecific.Join(oneItemDefaultOrderedGood, x => x, x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemDefaultOrderedGood.Join(oneItemSpecific, x => x, x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }

                var oneItemSpecificOrderedGood = Enumerable.Empty<int>().DefaultIfEmpty(4).OrderBy(x => x);
                try { oneItemSpecific.Join(oneItemSpecificOrderedGood, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemSpecificOrderedGood.Join(oneItemSpecific, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { oneItemSpecific.Join(oneItemSpecificOrderedGood, x => x, x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemSpecificOrderedGood.Join(oneItemSpecific, x => x, x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }

                Helper.ForEachEnumerableExpression(
                    oneItemSpecific,
                    new int[0],
                    res => { },
                    @"(l, r) =>
                      {
                        try { l.Join(r, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""outer"", exc.ParamName); }
                        try { r.Join(l, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""inner"", exc.ParamName); }
                        try { l.Join(r, x => x, x => x, (a, b) => a, new JoinTests._IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""outer"", exc.ParamName); }
                        try { r.Join(l, x => x, x => x, (a, b) => a, new JoinTests._IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""inner"", exc.ParamName); }
                        
                        return Helper.NoCallValue;
                      }",
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>)
                );
            }

            // oneItemDefaultOrdered
            {
                var emptyGood = Enumerable.Empty<int>();
                try { oneItemDefaultOrdered.Join(emptyGood, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { emptyGood.Join(oneItemDefaultOrdered, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { oneItemDefaultOrdered.Join(emptyGood, x => x, x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { emptyGood.Join(oneItemDefaultOrdered, x => x, x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }

                var emptyOrderedGood = Enumerable.Empty<int>().OrderBy(x => x);
                try { oneItemDefaultOrdered.Join(emptyOrderedGood, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { emptyOrderedGood.Join(oneItemDefaultOrdered, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { oneItemDefaultOrdered.Join(emptyOrderedGood, x => x, x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { emptyOrderedGood.Join(oneItemDefaultOrdered, x => x, x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }

                var rangeGood = Enumerable.Range(1, 1);
                try { oneItemDefaultOrdered.Join(rangeGood, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { rangeGood.Join(oneItemDefaultOrdered, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { oneItemDefaultOrdered.Join(rangeGood, x => x, x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { rangeGood.Join(oneItemDefaultOrdered, x => x, x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }

                var repeatGood = Enumerable.Repeat(1, 1);
                try { oneItemDefaultOrdered.Join(repeatGood, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { repeatGood.Join(oneItemDefaultOrdered, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { oneItemDefaultOrdered.Join(repeatGood, x => x, x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { repeatGood.Join(oneItemDefaultOrdered, x => x, x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }

                var reverseRangeGood = Enumerable.Range(1, 1).Reverse();
                try { oneItemDefaultOrdered.Join(reverseRangeGood, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { reverseRangeGood.Join(oneItemDefaultOrdered, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { oneItemDefaultOrdered.Join(reverseRangeGood, x => x, x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { reverseRangeGood.Join(oneItemDefaultOrdered, x => x, x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }

                var oneItemDefaultGood = Enumerable.Empty<int>().DefaultIfEmpty();
                try { oneItemDefaultOrdered.Join(oneItemDefaultGood, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemDefaultGood.Join(oneItemDefaultOrdered, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { oneItemDefaultOrdered.Join(oneItemDefaultGood, x => x, x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemDefaultGood.Join(oneItemDefaultOrdered, x => x, x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }

                var oneItemSpecificGood = Enumerable.Empty<int>().DefaultIfEmpty(4);
                try { oneItemDefaultOrdered.Join(oneItemSpecificGood, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemSpecificGood.Join(oneItemDefaultOrdered, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { oneItemDefaultOrdered.Join(oneItemSpecificGood, x => x, x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemSpecificGood.Join(oneItemDefaultOrdered, x => x, x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }

                var oneItemDefaultOrderedGood = Enumerable.Empty<int>().DefaultIfEmpty().OrderBy(x => x);
                try { oneItemDefaultOrdered.Join(oneItemDefaultOrderedGood, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemDefaultOrderedGood.Join(oneItemDefaultOrdered, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { oneItemDefaultOrdered.Join(oneItemDefaultOrderedGood, x => x, x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemDefaultOrderedGood.Join(oneItemDefaultOrdered, x => x, x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }

                var oneItemSpecificOrderedGood = Enumerable.Empty<int>().DefaultIfEmpty(4).OrderBy(x => x);
                try { oneItemDefaultOrdered.Join(oneItemSpecificOrderedGood, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemSpecificOrderedGood.Join(oneItemDefaultOrdered, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { oneItemDefaultOrdered.Join(oneItemSpecificOrderedGood, x => x, x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemSpecificOrderedGood.Join(oneItemDefaultOrdered, x => x, x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }

                Helper.ForEachEnumerableExpression(
                    oneItemDefaultOrdered,
                    new int[0],
                    res => { },
                    @"(l, r) =>
                      {
                        try { l.Join(r, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""outer"", exc.ParamName); }
                        try { r.Join(l, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""inner"", exc.ParamName); }
                        try { l.Join(r, x => x, x => x, (a, b) => a, new JoinTests._IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""outer"", exc.ParamName); }
                        try { r.Join(l, x => x, x => x, (a, b) => a, new JoinTests._IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""inner"", exc.ParamName); }
                        
                        return Helper.NoCallValue;
                      }",
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>)
                );
            }

            // oneItemSpecificOrdered
            {
                var emptyGood = Enumerable.Empty<int>();
                try { oneItemSpecificOrdered.Join(emptyGood, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { emptyGood.Join(oneItemSpecificOrdered, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { oneItemSpecificOrdered.Join(emptyGood, x => x, x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { emptyGood.Join(oneItemSpecificOrdered, x => x, x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }

                var emptyOrderedGood = Enumerable.Empty<int>().OrderBy(x => x);
                try { oneItemSpecificOrdered.Join(emptyOrderedGood, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { emptyOrderedGood.Join(oneItemSpecificOrdered, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { oneItemSpecificOrdered.Join(emptyOrderedGood, x => x, x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { emptyOrderedGood.Join(oneItemSpecificOrdered, x => x, x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }

                var rangeGood = Enumerable.Range(1, 1);
                try { oneItemSpecificOrdered.Join(rangeGood, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { rangeGood.Join(oneItemSpecificOrdered, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { oneItemSpecificOrdered.Join(rangeGood, x => x, x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { rangeGood.Join(oneItemSpecificOrdered, x => x, x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }

                var repeatGood = Enumerable.Repeat(1, 1);
                try { oneItemSpecificOrdered.Join(repeatGood, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { repeatGood.Join(oneItemSpecificOrdered, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { oneItemSpecificOrdered.Join(repeatGood, x => x, x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { repeatGood.Join(oneItemSpecificOrdered, x => x, x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }

                var reverseRangeGood = Enumerable.Range(1, 1).Reverse();
                try { oneItemSpecificOrdered.Join(reverseRangeGood, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { reverseRangeGood.Join(oneItemSpecificOrdered, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { oneItemSpecificOrdered.Join(reverseRangeGood, x => x, x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { reverseRangeGood.Join(oneItemSpecificOrdered, x => x, x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }

                var oneItemDefaultGood = Enumerable.Empty<int>().DefaultIfEmpty();
                try { oneItemSpecificOrdered.Join(oneItemDefaultGood, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemDefaultGood.Join(oneItemSpecificOrdered, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { oneItemSpecificOrdered.Join(oneItemDefaultGood, x => x, x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemDefaultGood.Join(oneItemSpecificOrdered, x => x, x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }

                var oneItemSpecificGood = Enumerable.Empty<int>().DefaultIfEmpty(4);
                try { oneItemSpecificOrdered.Join(oneItemSpecificGood, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemSpecificGood.Join(oneItemSpecificOrdered, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { oneItemSpecificOrdered.Join(oneItemSpecificGood, x => x, x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemSpecificGood.Join(oneItemSpecificOrdered, x => x, x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }

                var oneItemDefaultOrderedGood = Enumerable.Empty<int>().DefaultIfEmpty().OrderBy(x => x);
                try { oneItemSpecificOrdered.Join(oneItemDefaultOrderedGood, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemDefaultOrderedGood.Join(oneItemSpecificOrdered, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { oneItemSpecificOrdered.Join(oneItemDefaultOrderedGood, x => x, x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemDefaultOrderedGood.Join(oneItemSpecificOrdered, x => x, x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }

                var oneItemSpecificOrderedGood = Enumerable.Empty<int>().DefaultIfEmpty(4).OrderBy(x => x);
                try { oneItemSpecificOrdered.Join(oneItemSpecificOrderedGood, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemSpecificOrderedGood.Join(oneItemSpecificOrdered, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }
                try { oneItemSpecificOrdered.Join(oneItemSpecificOrderedGood, x => x, x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("outer", exc.ParamName); }
                try { oneItemSpecificOrderedGood.Join(oneItemSpecificOrdered, x => x, x => x, (a, b) => a, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("inner", exc.ParamName); }

                Helper.ForEachEnumerableExpression(
                    oneItemSpecificOrdered,
                    new int[0],
                    res => { },
                    @"(l, r) =>
                      {
                        try { l.Join(r, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""outer"", exc.ParamName); }
                        try { r.Join(l, x => x, x => x, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""inner"", exc.ParamName); }
                        try { l.Join(r, x => x, x => x, (a, b) => a, new JoinTests._IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""outer"", exc.ParamName); }
                        try { r.Join(l, x => x, x => x, (a, b) => a, new JoinTests._IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""inner"", exc.ParamName); }
                        
                        return Helper.NoCallValue;
                      }",
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>)
                );
            }
        }
    }
}
