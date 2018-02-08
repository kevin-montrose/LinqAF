using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using TestHelpers;

namespace LinqAF.Tests
{
    [TestClass]
    public class ConcatTests
    {
        static void _InstanceExtensionNoOverlapImpl(int spread, int take)
        {
            Dictionary<MethodInfo, List<MethodInfo>> instOverlaps, extOverlaps;
            Helper.GetOverlappingMethods(typeof(Impl.IConcat<,,>), out instOverlaps, out extOverlaps, spread, take);

            if (instOverlaps.Count > 0)
            {
                var failure = new StringBuilder();
                foreach (var kv in instOverlaps)
                {
                    failure.AppendLine("For " + kv.Key);
                    failure.AppendLine("\t" +
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
                    failure.AppendLine("\t"+
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
                if (!Helper.Implements(e, typeof(LinqAF.Impl.IConcat<,,>), out missing))
                {
                    Assert.Fail($"{e.Name} does not implement IConcat ({string.Join(", ", missing)})");
                }
            }
        }

        [TestMethod]
        public void AcceptsAllEnumerables()
        {
            var missingMethod = new List<string>();

            var iconcat = typeof(Impl.IConcat<,,>);
            var enums = Helper.AllEnumerables(includeWeirdOnes: false);
            foreach (var e in enums)
            {
                var mtd =
                    iconcat
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
                if (mtd == null) missingMethod.Add(e.Name);
            }

            if (missingMethod.Any())
            {
                Assert.Fail("Missing method for: \r\n" + string.Join("\r\n", missingMethod));
            }
        }

        [TestMethod]
        public void Chaining_1()
        {
            var toSkip = Helper.AllEnumerables().Where((_, ix) => ix % 2 == 0).ToArray();
            toSkip = toSkip.Concat(
                new[]
                {
                    typeof(EmptyEnumerable<>),
                    typeof(EmptyOrderedEnumerable<>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                }
            ).ToArray();

            Helper.ForEachEnumerableNoRetExpression(
                new[] { 1, 2, 3 },
                @"a =>
                    Helper.ForEachEnumerableExpression(
                        a,
                        new[] { 4, 5, 6 },
                        res =>
                        {
                            Assert.AreEqual(6, res.Count);
                            Assert.AreEqual(1, res[0]);
                            Assert.AreEqual(2, res[1]);
                            Assert.AreEqual(3, res[2]);
                            Assert.AreEqual(4, res[3]);
                            Assert.AreEqual(5, res[4]);
                            Assert.AreEqual(6, res[5]);
                        },
                        ""(a, b) => a.Concat(b)"",
                        typeof(EmptyEnumerable<>),
                        typeof(EmptyOrderedEnumerable<>),
                        typeof(GroupByDefaultEnumerable<,,,,>),
                        typeof(GroupBySpecificEnumerable<,,,,>),
                        typeof(LookupDefaultEnumerable<,>),
                        typeof(LookupSpecificEnumerable<,>)
                    )",
                toSkip
            );

            {
                var concatEmpty = Enumerable.Empty<int>().Concat(new[] { 1, 2, 3 });
                var res = new List<int>();
                foreach (var item in concatEmpty)
                {
                    res.Add(item);
                }

                Assert.AreEqual(3, res.Count);
                Assert.AreEqual(1, res[0]);
                Assert.AreEqual(2, res[1]);
                Assert.AreEqual(3, res[2]);
            }
        }

        [TestMethod]
        public void Chaining_2()
        {
            var toSkip = Helper.AllEnumerables().Where((_, ix) => ix % 2 == 1).ToArray();
            toSkip = toSkip.Concat(
                new[]
                {
                    typeof(EmptyEnumerable<>),
                    typeof(EmptyOrderedEnumerable<>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                }
            ).ToArray();

            Helper.ForEachEnumerableNoRetExpression(
                new[] { 1, 2, 3 },
                @"a =>
                    Helper.ForEachEnumerableExpression(
                        a,
                        new[] { 4, 5, 6 },
                        res =>
                        {
                            Assert.AreEqual(6, res.Count);
                            Assert.AreEqual(1, res[0]);
                            Assert.AreEqual(2, res[1]);
                            Assert.AreEqual(3, res[2]);
                            Assert.AreEqual(4, res[3]);
                            Assert.AreEqual(5, res[4]);
                            Assert.AreEqual(6, res[5]);
                        },
                        ""(a, b) => a.Concat(b)"",
                        typeof(EmptyEnumerable<>),
                        typeof(EmptyOrderedEnumerable<>),
                        typeof(GroupByDefaultEnumerable<,,,,>),
                        typeof(GroupBySpecificEnumerable<,,,,>),
                        typeof(LookupDefaultEnumerable<,>),
                        typeof(LookupSpecificEnumerable<,>)
                    )",
                toSkip
            );

            {
                var concatEmpty = Enumerable.Empty<int>().Concat(new[] { 1, 2, 3 });
                var res = new List<int>();
                foreach (var item in concatEmpty)
                {
                    res.Add(item);
                }

                Assert.AreEqual(3, res.Count);
                Assert.AreEqual(1, res[0]);
                Assert.AreEqual(2, res[1]);
                Assert.AreEqual(3, res[2]);
            }
        }

        class _GroupingComparer<T, V> : IEqualityComparer<GroupingEnumerable<T, V>>
        {
            public bool Equals(GroupingEnumerable<T, V> x, GroupingEnumerable<T, V> y)
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

            public int GetHashCode(GroupingEnumerable<T, V> obj)
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

        class _IntComparer : IEqualityComparer<int>
        {
            public bool Equals(int x, int y) => x == y;

            public int GetHashCode(int obj) => obj;
        }

        [TestMethod]
        public void Chaining_Weird()
        {
            var empty = Enumerable.Empty<int>();
            var emptyOrdered = empty.OrderBy(x => x);
            var groupByDefault = new[] { 1, 1, 2, 2, 3, 3 }.GroupBy(x => x);
            var groupBySpecific = new[] { "hello", "HELLO", "world", "WORLD", "foo", "FOO" }.GroupBy(x => x, StringComparer.OrdinalIgnoreCase);
            var lookupDefault = new int[] { 1, 1, 2, 2, 3, 3 }.ToLookup(x => x);
            var lookupSpecific = new int[] { 1, 1, 2, 2, 3, 3 }.ToLookup(x => x, new _IntComparer());
            var range = Enumerable.Range(1, 5);
            var repeat = Enumerable.Repeat("foo", 5);
            var reverseRange = Enumerable.Range(1, 5).Reverse();
            var oneItemDefault = Enumerable.Empty<int>().DefaultIfEmpty();
            var oneItemSpecific = Enumerable.Empty<int>().DefaultIfEmpty(4);
            var oneItemDefaultOrdered = oneItemDefault.OrderBy(x => x);
            var oneItemSpecificOrdered = oneItemSpecific.OrderBy(x => x);

            // empty
            {
                Helper.ForEachEnumerableExpression(
                    empty,
                    new[] { 1, 2, 3 },
                    res =>
                    {
                        Assert.AreEqual(3, res.Count);
                        Assert.AreEqual(1, res[0]);
                        Assert.AreEqual(2, res[1]);
                        Assert.AreEqual(3, res[2]);
                    },
                    @"(a, b) => a.Concat(b)",
                    typeof(EmptyEnumerable<>),
                    typeof(EmptyOrderedEnumerable<>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );

                Assert.IsTrue(empty.Concat(empty).SequenceEqual(new int[0]));
                Assert.IsTrue(empty.Concat(emptyOrdered).SequenceEqual(new int[0]));
                Assert.IsTrue(Enumerable.Empty<GroupingEnumerable<int, int>>().Concat(groupByDefault).SequenceEqual(groupByDefault, new _GroupingComparer<int, int>()));
                Assert.IsTrue(Enumerable.Empty<GroupingEnumerable<string, string>>().Concat(groupBySpecific).SequenceEqual(groupBySpecific, new _GroupingComparer<string, string>()));
                Assert.IsTrue(Enumerable.Empty<GroupingEnumerable<int, int>>().Concat(lookupDefault).SequenceEqual(lookupDefault, new _GroupingComparer<int, int>()));
                Assert.IsTrue(Enumerable.Empty<GroupingEnumerable<int, int>>().Concat(lookupSpecific).SequenceEqual(lookupSpecific, new _GroupingComparer<int, int>()));
                Assert.IsTrue(empty.Concat(range).SequenceEqual(range));
                Assert.IsTrue(Enumerable.Empty<string>().Concat(repeat).SequenceEqual(repeat));
                Assert.IsTrue(empty.Concat(reverseRange).SequenceEqual(reverseRange));
                Assert.IsTrue(empty.Concat(oneItemDefault).SequenceEqual(oneItemDefault));
                Assert.IsTrue(empty.Concat(oneItemSpecific).SequenceEqual(oneItemSpecific));
                Assert.IsTrue(empty.Concat(oneItemDefaultOrdered).SequenceEqual(oneItemDefaultOrdered));
                Assert.IsTrue(empty.Concat(oneItemSpecificOrdered).SequenceEqual(oneItemSpecificOrdered));
            }

            // emptyOrdered
            {
                Helper.ForEachEnumerableExpression(
                    emptyOrdered,
                    new[] { 1, 2, 3 },
                    res =>
                    {
                        Assert.AreEqual(3, res.Count);
                        Assert.AreEqual(1, res[0]);
                        Assert.AreEqual(2, res[1]);
                        Assert.AreEqual(3, res[2]);
                    },
                    @"(a, b) => a.Concat(b)",
                    typeof(EmptyEnumerable<>),
                    typeof(EmptyOrderedEnumerable<>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );

                Assert.IsTrue(emptyOrdered.Concat(empty).SequenceEqual(new int[0]));
                Assert.IsTrue(emptyOrdered.Concat(emptyOrdered).SequenceEqual(new int[0]));
                Assert.IsTrue(Enumerable.Empty<GroupingEnumerable<int, int>>().OrderBy(x => x).Concat(groupByDefault).SequenceEqual(groupByDefault, new _GroupingComparer<int, int>()));
                Assert.IsTrue(Enumerable.Empty<GroupingEnumerable<string, string>>().OrderBy(x => x).Concat(groupBySpecific).SequenceEqual(groupBySpecific, new _GroupingComparer<string, string>()));
                Assert.IsTrue(Enumerable.Empty<GroupingEnumerable<int, int>>().OrderBy(x => x).Concat(lookupDefault).SequenceEqual(lookupDefault, new _GroupingComparer<int, int>()));
                Assert.IsTrue(Enumerable.Empty<GroupingEnumerable<int, int>>().OrderBy(x => x).Concat(lookupSpecific).SequenceEqual(lookupSpecific, new _GroupingComparer<int, int>()));
                Assert.IsTrue(emptyOrdered.Concat(range).SequenceEqual(range));
                Assert.IsTrue(Enumerable.Empty<string>().OrderBy(x => x).Concat(repeat).SequenceEqual(repeat));
                Assert.IsTrue(emptyOrdered.Concat(reverseRange).SequenceEqual(reverseRange));
                Assert.IsTrue(emptyOrdered.Concat(oneItemDefault).SequenceEqual(oneItemDefault));
                Assert.IsTrue(emptyOrdered.Concat(oneItemSpecific).SequenceEqual(oneItemSpecific));
                Assert.IsTrue(emptyOrdered.Concat(oneItemDefaultOrdered).SequenceEqual(oneItemDefaultOrdered));
                Assert.IsTrue(emptyOrdered.Concat(oneItemSpecificOrdered).SequenceEqual(oneItemSpecificOrdered));
            }

            // groupByDefault
            {
                // identity
                {
                    var e = groupByDefault.Concat(new[] { groupByDefault.First() });
                    var res = new List<GroupingEnumerable<int, int>>();
                    foreach (var item in e)
                    {
                        res.Add(item);
                    }
                    Assert.AreEqual(4, res.Count);
                    Assert.AreEqual(1, res[0].Key);
                    Assert.AreEqual(2, res[0].Count());
                    Assert.IsTrue(res[0].All(r => r == 1));
                    Assert.AreEqual(2, res[1].Key);
                    Assert.AreEqual(2, res[1].Count());
                    Assert.IsTrue(res[1].All(r => r == 2));
                    Assert.AreEqual(3, res[2].Key);
                    Assert.AreEqual(2, res[2].Count());
                    Assert.IsTrue(res[2].All(r => r == 3));
                    Assert.AreEqual(1, res[3].Key);
                    Assert.AreEqual(2, res[3].Count());
                    Assert.IsTrue(res[3].All(r => r == 1));
                }

                // groupByDefault
                {
                    var e = groupByDefault.Concat(new[] { 4, 4 }.GroupBy(x => x));
                    var res = new List<GroupingEnumerable<int, int>>();
                    foreach (var item in e)
                    {
                        res.Add(item);
                    }
                    Assert.AreEqual(4, res.Count);
                    Assert.AreEqual(1, res[0].Key);
                    Assert.AreEqual(2, res[0].Count());
                    Assert.IsTrue(res[0].All(r => r == 1));
                    Assert.AreEqual(2, res[1].Key);
                    Assert.AreEqual(2, res[1].Count());
                    Assert.IsTrue(res[1].All(r => r == 2));
                    Assert.AreEqual(3, res[2].Key);
                    Assert.AreEqual(2, res[2].Count());
                    Assert.IsTrue(res[2].All(r => r == 3));
                    Assert.AreEqual(4, res[3].Key);
                    Assert.AreEqual(2, res[3].Count());
                    Assert.IsTrue(res[3].All(r => r == 4));
                }

                // groupBySpecific
                {
                    var e = groupByDefault.Concat(new[] { 4, 4 }.GroupBy(x => x, new _IntComparer()));
                    var res = new List<GroupingEnumerable<int, int>>();
                    foreach (var item in e)
                    {
                        res.Add(item);
                    }
                    Assert.AreEqual(4, res.Count);
                    Assert.AreEqual(1, res[0].Key);
                    Assert.AreEqual(2, res[0].Count());
                    Assert.IsTrue(res[0].All(r => r == 1));
                    Assert.AreEqual(2, res[1].Key);
                    Assert.AreEqual(2, res[1].Count());
                    Assert.IsTrue(res[1].All(r => r == 2));
                    Assert.AreEqual(3, res[2].Key);
                    Assert.AreEqual(2, res[2].Count());
                    Assert.IsTrue(res[2].All(r => r == 3));
                    Assert.AreEqual(4, res[3].Key);
                    Assert.AreEqual(2, res[3].Count());
                    Assert.IsTrue(res[3].All(r => r == 4));
                }

                // lookupDefault
                {
                    var e = groupByDefault.Concat(new[] { 4, 4 }.ToLookup(x => x));
                    var res = new List<GroupingEnumerable<int, int>>();
                    foreach (var item in e)
                    {
                        res.Add(item);
                    }
                    Assert.AreEqual(4, res.Count);
                    Assert.AreEqual(1, res[0].Key);
                    Assert.AreEqual(2, res[0].Count());
                    Assert.IsTrue(res[0].All(r => r == 1));
                    Assert.AreEqual(2, res[1].Key);
                    Assert.AreEqual(2, res[1].Count());
                    Assert.IsTrue(res[1].All(r => r == 2));
                    Assert.AreEqual(3, res[2].Key);
                    Assert.AreEqual(2, res[2].Count());
                    Assert.IsTrue(res[2].All(r => r == 3));
                    Assert.AreEqual(4, res[3].Key);
                    Assert.AreEqual(2, res[3].Count());
                    Assert.IsTrue(res[3].All(r => r == 4));
                }

                // lookupSpecific
                {
                    var e = groupByDefault.Concat(new[] { 4, 4 }.ToLookup(x => x, new _IntComparer()));
                    var res = new List<GroupingEnumerable<int, int>>();
                    foreach (var item in e)
                    {
                        res.Add(item);
                    }
                    Assert.AreEqual(4, res.Count);
                    Assert.AreEqual(1, res[0].Key);
                    Assert.AreEqual(2, res[0].Count());
                    Assert.IsTrue(res[0].All(r => r == 1));
                    Assert.AreEqual(2, res[1].Key);
                    Assert.AreEqual(2, res[1].Count());
                    Assert.IsTrue(res[1].All(r => r == 2));
                    Assert.AreEqual(3, res[2].Key);
                    Assert.AreEqual(2, res[2].Count());
                    Assert.IsTrue(res[2].All(r => r == 3));
                    Assert.AreEqual(4, res[3].Key);
                    Assert.AreEqual(2, res[3].Count());
                    Assert.IsTrue(res[3].All(r => r == 4));
                }
            }

            // groupBySpecific
            {
                // identity
                {
                    var e = groupBySpecific.Concat(new[] { groupBySpecific.First() });
                    var res = new List<GroupingEnumerable<string, string>>();
                    foreach (var item in e)
                    {
                        res.Add(item);
                    }
                    Assert.AreEqual(4, res.Count);
                    Assert.AreEqual("hello", res[0].Key);
                    Assert.AreEqual(2, res[0].Count());
                    Assert.IsTrue(res[0].All(r => r.Equals("hello", StringComparison.InvariantCultureIgnoreCase)));
                    Assert.AreEqual("world", res[1].Key);
                    Assert.AreEqual(2, res[1].Count());
                    Assert.IsTrue(res[1].All(r => r.Equals("world", StringComparison.InvariantCultureIgnoreCase)));
                    Assert.AreEqual("foo", res[2].Key);
                    Assert.AreEqual(2, res[2].Count());
                    Assert.IsTrue(res[2].All(r => r.Equals("foo", StringComparison.InvariantCultureIgnoreCase)));
                    Assert.AreEqual("hello", res[3].Key);
                    Assert.AreEqual(2, res[3].Count());
                    Assert.IsTrue(res[3].All(r => r.Equals("hello", StringComparison.InvariantCultureIgnoreCase)));
                }

                // groupByDefault
                {
                    var e = groupBySpecific.Concat(new[] { "fizz", "fizz" }.GroupBy(x => x));
                    var res = new List<GroupingEnumerable<string, string>>();
                    foreach (var item in e)
                    {
                        res.Add(item);
                    }
                    Assert.AreEqual(4, res.Count);
                    Assert.AreEqual("hello", res[0].Key);
                    Assert.AreEqual(2, res[0].Count());
                    Assert.IsTrue(res[0].All(r => r.Equals("hello", StringComparison.InvariantCultureIgnoreCase)));
                    Assert.AreEqual("world", res[1].Key);
                    Assert.AreEqual(2, res[1].Count());
                    Assert.IsTrue(res[1].All(r => r.Equals("world", StringComparison.InvariantCultureIgnoreCase)));
                    Assert.AreEqual("foo", res[2].Key);
                    Assert.AreEqual(2, res[2].Count());
                    Assert.IsTrue(res[2].All(r => r.Equals("foo", StringComparison.InvariantCultureIgnoreCase)));
                    Assert.AreEqual("fizz", res[3].Key);
                    Assert.AreEqual(2, res[3].Count());
                    Assert.IsTrue(res[3].All(r => r == "fizz"));
                }

                // groupBySpecific
                {
                    var e = groupBySpecific.Concat(groupBySpecific);
                    var res = new List<GroupingEnumerable<string, string>>();
                    foreach (var item in e)
                    {
                        res.Add(item);
                    }
                    Assert.AreEqual(6, res.Count);
                    Assert.AreEqual("hello", res[0].Key);
                    Assert.AreEqual(2, res[0].Count());
                    Assert.IsTrue(res[0].All(r => r.Equals("hello", StringComparison.InvariantCultureIgnoreCase)));
                    Assert.AreEqual("world", res[1].Key);
                    Assert.AreEqual(2, res[1].Count());
                    Assert.IsTrue(res[1].All(r => r.Equals("world", StringComparison.InvariantCultureIgnoreCase)));
                    Assert.AreEqual("foo", res[2].Key);
                    Assert.AreEqual(2, res[2].Count());
                    Assert.IsTrue(res[2].All(r => r.Equals("foo", StringComparison.InvariantCultureIgnoreCase)));
                    Assert.AreEqual("hello", res[3].Key);
                    Assert.AreEqual(2, res[3].Count());
                    Assert.IsTrue(res[3].All(r => r.Equals("hello", StringComparison.InvariantCultureIgnoreCase)));
                    Assert.AreEqual("world", res[4].Key);
                    Assert.AreEqual(2, res[4].Count());
                    Assert.IsTrue(res[4].All(r => r.Equals("world", StringComparison.InvariantCultureIgnoreCase)));
                    Assert.AreEqual("foo", res[5].Key);
                    Assert.AreEqual(2, res[5].Count());
                    Assert.IsTrue(res[5].All(r => r.Equals("foo", StringComparison.InvariantCultureIgnoreCase)));
                }

                // lookupDefault
                {
                    var e = groupBySpecific.Concat(new[] { "fizz", "fizz" }.ToLookup(x => x));
                    var res = new List<GroupingEnumerable<string, string>>();
                    foreach (var item in e)
                    {
                        res.Add(item);
                    }
                    Assert.AreEqual(4, res.Count);
                    Assert.AreEqual("hello", res[0].Key);
                    Assert.AreEqual(2, res[0].Count());
                    Assert.IsTrue(res[0].All(r => r.Equals("hello", StringComparison.InvariantCultureIgnoreCase)));
                    Assert.AreEqual("world", res[1].Key);
                    Assert.AreEqual(2, res[1].Count());
                    Assert.IsTrue(res[1].All(r => r.Equals("world", StringComparison.InvariantCultureIgnoreCase)));
                    Assert.AreEqual("foo", res[2].Key);
                    Assert.AreEqual(2, res[2].Count());
                    Assert.IsTrue(res[2].All(r => r.Equals("foo", StringComparison.InvariantCultureIgnoreCase)));
                    Assert.AreEqual("fizz", res[3].Key);
                    Assert.AreEqual(2, res[3].Count());
                    Assert.IsTrue(res[3].All(r => r == "fizz"));
                }

                // lookupSpecific
                {
                    var e = groupBySpecific.Concat(new[] { "fizz", "fizz" }.ToLookup(x => x, StringComparer.InvariantCultureIgnoreCase));
                    var res = new List<GroupingEnumerable<string, string>>();
                    foreach (var item in e)
                    {
                        res.Add(item);
                    }
                    Assert.AreEqual(4, res.Count);
                    Assert.AreEqual("hello", res[0].Key);
                    Assert.AreEqual(2, res[0].Count());
                    Assert.IsTrue(res[0].All(r => r.Equals("hello", StringComparison.InvariantCultureIgnoreCase)));
                    Assert.AreEqual("world", res[1].Key);
                    Assert.AreEqual(2, res[1].Count());
                    Assert.IsTrue(res[1].All(r => r.Equals("world", StringComparison.InvariantCultureIgnoreCase)));
                    Assert.AreEqual("foo", res[2].Key);
                    Assert.AreEqual(2, res[2].Count());
                    Assert.IsTrue(res[2].All(r => r.Equals("foo", StringComparison.InvariantCultureIgnoreCase)));
                    Assert.AreEqual("fizz", res[3].Key);
                    Assert.AreEqual(2, res[3].Count());
                    Assert.IsTrue(res[3].All(r => r == "fizz"));
                }
            }

            // lookupDefault
            {
                // identity
                {
                    var e = lookupDefault.Concat(new[] { groupByDefault.First() });
                    var res = new List<GroupingEnumerable<int, int>>();
                    foreach (var item in e)
                    {
                        res.Add(item);
                    }
                    Assert.AreEqual(4, res.Count);
                    Assert.AreEqual(1, res[0].Key);
                    Assert.AreEqual(2, res[0].Count());
                    Assert.IsTrue(res[0].All(r => r == 1));
                    Assert.AreEqual(2, res[1].Key);
                    Assert.AreEqual(2, res[1].Count());
                    Assert.IsTrue(res[1].All(r => r == 2));
                    Assert.AreEqual(3, res[2].Key);
                    Assert.AreEqual(2, res[2].Count());
                    Assert.IsTrue(res[2].All(r => r == 3));
                    Assert.AreEqual(1, res[3].Key);
                    Assert.AreEqual(2, res[3].Count());
                    Assert.IsTrue(res[3].All(r => r == 1));
                }

                // groupByDefault
                {
                    var e = lookupDefault.Concat(new[] { 4, 4 }.GroupBy(x => x));
                    var res = new List<GroupingEnumerable<int, int>>();
                    foreach (var item in e)
                    {
                        res.Add(item);
                    }
                    Assert.AreEqual(4, res.Count);
                    Assert.AreEqual(1, res[0].Key);
                    Assert.AreEqual(2, res[0].Count());
                    Assert.IsTrue(res[0].All(r => r == 1));
                    Assert.AreEqual(2, res[1].Key);
                    Assert.AreEqual(2, res[1].Count());
                    Assert.IsTrue(res[1].All(r => r == 2));
                    Assert.AreEqual(3, res[2].Key);
                    Assert.AreEqual(2, res[2].Count());
                    Assert.IsTrue(res[2].All(r => r == 3));
                    Assert.AreEqual(4, res[3].Key);
                    Assert.AreEqual(2, res[3].Count());
                    Assert.IsTrue(res[3].All(r => r == 4));
                }

                // groupBySpecific
                {
                    var e = lookupDefault.Concat(new[] { 4, 4 }.GroupBy(x => x, new _IntComparer()));
                    var res = new List<GroupingEnumerable<int, int>>();
                    foreach (var item in e)
                    {
                        res.Add(item);
                    }
                    Assert.AreEqual(4, res.Count);
                    Assert.AreEqual(1, res[0].Key);
                    Assert.AreEqual(2, res[0].Count());
                    Assert.IsTrue(res[0].All(r => r == 1));
                    Assert.AreEqual(2, res[1].Key);
                    Assert.AreEqual(2, res[1].Count());
                    Assert.IsTrue(res[1].All(r => r == 2));
                    Assert.AreEqual(3, res[2].Key);
                    Assert.AreEqual(2, res[2].Count());
                    Assert.IsTrue(res[2].All(r => r == 3));
                    Assert.AreEqual(4, res[3].Key);
                    Assert.AreEqual(2, res[3].Count());
                    Assert.IsTrue(res[3].All(r => r == 4));
                }

                // lookupDefault
                {
                    var e = lookupDefault.Concat(new[] { 4, 4 }.ToLookup(x => x));
                    var res = new List<GroupingEnumerable<int, int>>();
                    foreach (var item in e)
                    {
                        res.Add(item);
                    }
                    Assert.AreEqual(4, res.Count);
                    Assert.AreEqual(1, res[0].Key);
                    Assert.AreEqual(2, res[0].Count());
                    Assert.IsTrue(res[0].All(r => r == 1));
                    Assert.AreEqual(2, res[1].Key);
                    Assert.AreEqual(2, res[1].Count());
                    Assert.IsTrue(res[1].All(r => r == 2));
                    Assert.AreEqual(3, res[2].Key);
                    Assert.AreEqual(2, res[2].Count());
                    Assert.IsTrue(res[2].All(r => r == 3));
                    Assert.AreEqual(4, res[3].Key);
                    Assert.AreEqual(2, res[3].Count());
                    Assert.IsTrue(res[3].All(r => r == 4));
                }

                // lookupSpecific
                {
                    var e = lookupDefault.Concat(new[] { 4, 4 }.ToLookup(x => x, new _IntComparer()));
                    var res = new List<GroupingEnumerable<int, int>>();
                    foreach (var item in e)
                    {
                        res.Add(item);
                    }
                    Assert.AreEqual(4, res.Count);
                    Assert.AreEqual(1, res[0].Key);
                    Assert.AreEqual(2, res[0].Count());
                    Assert.IsTrue(res[0].All(r => r == 1));
                    Assert.AreEqual(2, res[1].Key);
                    Assert.AreEqual(2, res[1].Count());
                    Assert.IsTrue(res[1].All(r => r == 2));
                    Assert.AreEqual(3, res[2].Key);
                    Assert.AreEqual(2, res[2].Count());
                    Assert.IsTrue(res[2].All(r => r == 3));
                    Assert.AreEqual(4, res[3].Key);
                    Assert.AreEqual(2, res[3].Count());
                    Assert.IsTrue(res[3].All(r => r == 4));
                }
            }

            // range
            {
                Helper.ForEachEnumerableExpression(
                    range,
                    new[] { 6 },
                    res =>
                    {
                        Assert.AreEqual(6, res.Count);
                        Assert.AreEqual(1, res[0]);
                        Assert.AreEqual(2, res[1]);
                        Assert.AreEqual(3, res[2]);
                        Assert.AreEqual(4, res[3]);
                        Assert.AreEqual(5, res[4]);
                        Assert.AreEqual(6, res[5]);
                    },
                    @"(a, b) => a.Concat(b)",
                    typeof(EmptyEnumerable<>),
                    typeof(EmptyOrderedEnumerable<>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );

                Assert.IsTrue(range.Concat(empty).SequenceEqual(range));
                Assert.IsTrue(range.Concat(emptyOrdered).SequenceEqual(range));
                Assert.IsTrue(range.Concat(reverseRange).SequenceEqual(new[] { 1, 2, 3, 4, 5, 5, 4, 3, 2, 1 }));
                Assert.IsTrue(range.Concat(oneItemDefault).SequenceEqual(new[] { 1, 2, 3, 4, 5, 0 }));
                Assert.IsTrue(range.Concat(oneItemSpecific).SequenceEqual(new[] { 1, 2, 3, 4, 5, 4 }));
                Assert.IsTrue(range.Concat(oneItemDefaultOrdered).SequenceEqual(new[] { 1, 2, 3, 4, 5, 0 }));
                Assert.IsTrue(range.Concat(oneItemSpecificOrdered).SequenceEqual(new[] { 1, 2, 3, 4, 5, 4 }));
            }

            // reverseRange
            {
                Helper.ForEachEnumerableExpression(
                    reverseRange,
                    new[] { 6 },
                    res =>
                    {
                        Assert.AreEqual(6, res.Count);
                        Assert.AreEqual(5, res[0]);
                        Assert.AreEqual(4, res[1]);
                        Assert.AreEqual(3, res[2]);
                        Assert.AreEqual(2, res[3]);
                        Assert.AreEqual(1, res[4]);
                        Assert.AreEqual(6, res[5]);
                    },
                    @"(a, b) => a.Concat(b)",
                    typeof(EmptyEnumerable<>),
                    typeof(EmptyOrderedEnumerable<>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );

                Assert.IsTrue(reverseRange.Concat(empty).SequenceEqual(reverseRange));
                Assert.IsTrue(reverseRange.Concat(emptyOrdered).SequenceEqual(reverseRange));
                Assert.IsTrue(reverseRange.Concat(reverseRange).SequenceEqual(new[] { 5, 4, 3, 2, 1, 5, 4, 3, 2, 1 }));

                Assert.IsTrue(reverseRange.Concat(oneItemDefault).SequenceEqual(new[] { 5, 4, 3, 2, 1, 0 }));
                Assert.IsTrue(reverseRange.Concat(oneItemSpecific).SequenceEqual(new[] { 5, 4, 3, 2, 1, 4 }));
                Assert.IsTrue(reverseRange.Concat(oneItemDefaultOrdered).SequenceEqual(new[] { 5, 4, 3, 2, 1, 0 }));
                Assert.IsTrue(reverseRange.Concat(oneItemSpecificOrdered).SequenceEqual(new[] { 5, 4, 3, 2, 1, 4 }));
            }

            // oneItemDefault
            {
                Helper.ForEachEnumerableExpression(
                    oneItemDefault,
                    new[] { 6 },
                    res =>
                    {
                        Assert.AreEqual(2, res.Count);
                        Assert.AreEqual(0, res[0]);
                        Assert.AreEqual(6, res[1]);
                    },
                    @"(a, b) => a.Concat(b)",
                    typeof(EmptyEnumerable<>),
                    typeof(EmptyOrderedEnumerable<>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );

                Assert.IsTrue(oneItemDefault.Concat(empty).SequenceEqual(oneItemDefault));
                Assert.IsTrue(oneItemDefault.Concat(emptyOrdered).SequenceEqual(oneItemDefault));
                // default groupings are non-sensical
                Assert.IsTrue(oneItemDefault.Concat(range).SequenceEqual(new[] { 0, 1, 2, 3, 4, 5 }));
                Assert.IsTrue(oneItemDefault.Concat(Enumerable.Repeat(1, 1)).SequenceEqual(new[] { 0, 1 }));
                Assert.IsTrue(oneItemDefault.Concat(reverseRange).SequenceEqual(new[] { 0, 5, 4, 3, 2, 1 }));

                Assert.IsTrue(oneItemDefault.Concat(oneItemDefault).SequenceEqual(new[] { 0, 0 }));
                Assert.IsTrue(oneItemDefault.Concat(oneItemSpecific).SequenceEqual(new[] { 0, 4 }));
                Assert.IsTrue(oneItemDefault.Concat(oneItemDefaultOrdered).SequenceEqual(new[] { 0, 0 }));
                Assert.IsTrue(oneItemDefault.Concat(oneItemSpecificOrdered).SequenceEqual(new[] { 0, 4 }));
            }

            // oneItemSpecific
            {
                Helper.ForEachEnumerableExpression(
                    oneItemSpecific,
                    new[] { 6 },
                    res =>
                    {
                        Assert.AreEqual(2, res.Count);
                        Assert.AreEqual(4, res[0]);
                        Assert.AreEqual(6, res[1]);
                    },
                    @"(a, b) => a.Concat(b)",
                    typeof(EmptyEnumerable<>),
                    typeof(EmptyOrderedEnumerable<>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );

                Assert.IsTrue(oneItemSpecific.Concat(empty).SequenceEqual(oneItemSpecific));
                Assert.IsTrue(oneItemSpecific.Concat(emptyOrdered).SequenceEqual(oneItemSpecific));

                var oneItemSpecificGroupingInt = Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty(groupByDefault.First());
                Assert.IsTrue(oneItemSpecificGroupingInt.Concat(groupByDefault).SequenceEqual(new[] { groupByDefault.ElementAt(0), groupByDefault.ElementAt(0), groupByDefault.ElementAt(1), groupByDefault.ElementAt(2) }, new _GroupingComparer<int, int>()));

                var oneItemSpecificGroupingString = Enumerable.Empty<GroupingEnumerable<string, string>>().DefaultIfEmpty(groupBySpecific.First());
                Assert.IsTrue(oneItemSpecificGroupingString.Concat(groupBySpecific).SequenceEqual(new[] { groupBySpecific.ElementAt(0), groupBySpecific.ElementAt(0), groupBySpecific.ElementAt(1), groupBySpecific.ElementAt(2) }, new _GroupingComparer<string, string>()));

                Assert.IsTrue(oneItemSpecificGroupingInt.Concat(lookupDefault).SequenceEqual(new[] { lookupDefault.ElementAt(0), lookupDefault.ElementAt(0), lookupDefault.ElementAt(1), lookupDefault.ElementAt(2) }, new _GroupingComparer<int, int>()));

                Assert.IsTrue(oneItemSpecificGroupingInt.Concat(lookupSpecific).SequenceEqual(new[] { lookupSpecific.ElementAt(0), lookupSpecific.ElementAt(0), lookupSpecific.ElementAt(1), lookupSpecific.ElementAt(2) }, new _GroupingComparer<int, int>()));

                Assert.IsTrue(oneItemSpecific.Concat(range).SequenceEqual(new[] { 4, 1, 2, 3, 4, 5 }));
                Assert.IsTrue(oneItemSpecific.Concat(Enumerable.Repeat(1, 1)).SequenceEqual(new[] { 4, 1 }));
                Assert.IsTrue(oneItemSpecific.Concat(reverseRange).SequenceEqual(new[] { 4, 5, 4, 3, 2, 1 }));

                Assert.IsTrue(oneItemSpecific.Concat(oneItemDefault).SequenceEqual(new[] { 4, 0 }));
                Assert.IsTrue(oneItemSpecific.Concat(oneItemSpecific).SequenceEqual(new[] { 4, 4 }));
                Assert.IsTrue(oneItemSpecific.Concat(oneItemDefaultOrdered).SequenceEqual(new[] { 4, 0 }));
                Assert.IsTrue(oneItemSpecific.Concat(oneItemSpecificOrdered).SequenceEqual(new[] { 4, 4 }));
            }

            // oneItemDefaultOrdered
            {
                Helper.ForEachEnumerableExpression(
                    oneItemDefaultOrdered,
                    new[] { 6 },
                    res =>
                    {
                        Assert.AreEqual(2, res.Count);
                        Assert.AreEqual(0, res[0]);
                        Assert.AreEqual(6, res[1]);
                    },
                    @"(a, b) => a.Concat(b)",
                    typeof(EmptyEnumerable<>),
                    typeof(EmptyOrderedEnumerable<>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );

                Assert.IsTrue(oneItemDefaultOrdered.Concat(empty).SequenceEqual(oneItemDefault));
                Assert.IsTrue(oneItemDefaultOrdered.Concat(emptyOrdered).SequenceEqual(oneItemDefault));
                // default groupings are non-sensical
                Assert.IsTrue(oneItemDefaultOrdered.Concat(range).SequenceEqual(new[] { 0, 1, 2, 3, 4, 5 }));
                Assert.IsTrue(oneItemDefaultOrdered.Concat(Enumerable.Repeat(1, 1)).SequenceEqual(new[] { 0, 1 }));
                Assert.IsTrue(oneItemDefaultOrdered.Concat(reverseRange).SequenceEqual(new[] { 0, 5, 4, 3, 2, 1 }));

                Assert.IsTrue(oneItemDefaultOrdered.Concat(oneItemDefault).SequenceEqual(new[] { 0, 0 }));
                Assert.IsTrue(oneItemDefaultOrdered.Concat(oneItemSpecific).SequenceEqual(new[] { 0, 4 }));
                Assert.IsTrue(oneItemDefaultOrdered.Concat(oneItemDefaultOrdered).SequenceEqual(new[] { 0, 0 }));
                Assert.IsTrue(oneItemDefaultOrdered.Concat(oneItemSpecificOrdered).SequenceEqual(new[] { 0, 4 }));
            }

            // oneItemSpecificOrdered
            {
                Helper.ForEachEnumerableExpression(
                    oneItemSpecificOrdered,
                    new[] { 6 },
                    res =>
                    {
                        Assert.AreEqual(2, res.Count);
                        Assert.AreEqual(4, res[0]);
                        Assert.AreEqual(6, res[1]);
                    },
                    @"(a, b) => a.Concat(b)",
                    typeof(EmptyEnumerable<>),
                    typeof(EmptyOrderedEnumerable<>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );

                Assert.IsTrue(oneItemSpecificOrdered.Concat(empty).SequenceEqual(oneItemSpecific));
                Assert.IsTrue(oneItemSpecificOrdered.Concat(emptyOrdered).SequenceEqual(oneItemSpecific));

                var oneItemSpecificOrderedGroupingInt = Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty(groupByDefault.First()).OrderBy(x => x);
                Assert.IsTrue(oneItemSpecificOrderedGroupingInt.Concat(groupByDefault).SequenceEqual(new[] { groupByDefault.ElementAt(0), groupByDefault.ElementAt(0), groupByDefault.ElementAt(1), groupByDefault.ElementAt(2) }, new _GroupingComparer<int, int>()));

                var oneItemSpecificOrderedGroupingString = Enumerable.Empty<GroupingEnumerable<string, string>>().DefaultIfEmpty(groupBySpecific.First()).OrderBy(x => x);
                Assert.IsTrue(oneItemSpecificOrderedGroupingString.Concat(groupBySpecific).SequenceEqual(new[] { groupBySpecific.ElementAt(0), groupBySpecific.ElementAt(0), groupBySpecific.ElementAt(1), groupBySpecific.ElementAt(2) }, new _GroupingComparer<string, string>()));

                Assert.IsTrue(oneItemSpecificOrderedGroupingInt.Concat(lookupDefault).SequenceEqual(new[] { lookupDefault.ElementAt(0), lookupDefault.ElementAt(0), lookupDefault.ElementAt(1), lookupDefault.ElementAt(2) }, new _GroupingComparer<int, int>()));

                Assert.IsTrue(oneItemSpecificOrderedGroupingInt.Concat(lookupSpecific).SequenceEqual(new[] { lookupSpecific.ElementAt(0), lookupSpecific.ElementAt(0), lookupSpecific.ElementAt(1), lookupSpecific.ElementAt(2) }, new _GroupingComparer<int, int>()));

                Assert.IsTrue(oneItemSpecificOrdered.Concat(range).SequenceEqual(new[] { 4, 1, 2, 3, 4, 5 }));
                Assert.IsTrue(oneItemSpecificOrdered.Concat(Enumerable.Repeat(1, 1)).SequenceEqual(new[] { 4, 1 }));
                Assert.IsTrue(oneItemSpecificOrdered.Concat(reverseRange).SequenceEqual(new[] { 4, 5, 4, 3, 2, 1 }));

                Assert.IsTrue(oneItemSpecificOrdered.Concat(oneItemDefault).SequenceEqual(new[] { 4, 0 }));
                Assert.IsTrue(oneItemSpecificOrdered.Concat(oneItemSpecific).SequenceEqual(new[] { 4, 4 }));
                Assert.IsTrue(oneItemSpecificOrdered.Concat(oneItemDefaultOrdered).SequenceEqual(new[] { 4, 0 }));
                Assert.IsTrue(oneItemSpecificOrdered.Concat(oneItemSpecificOrdered).SequenceEqual(new[] { 4, 4 }));
            }
        }

        [TestMethod]
        public void Errors()
        {
            Helper.ForEachEnumerableNoRetExpression(
                new int[0],
                @"a => 
                  { 
                    try 
                    { 
                        a.Concat(default(IEnumerable<int>)); 
                        Assert.Fail(); 
                    } 
                    catch (ArgumentNullException exc) 
                    { 
                        Assert.AreEqual(""second"", exc.ParamName); 
                    }

                    try 
                    { 
                        a.Concat(default(int[])); 
                        Assert.Fail(); 
                    } 
                    catch (ArgumentNullException exc) 
                    { 
                        Assert.AreEqual(""second"", exc.ParamName); 
                    }

                    try 
                    { 
                        a.Concat(default(Dictionary<int, object>.KeyCollection)); 
                        Assert.Fail(); 
                    } 
                    catch (ArgumentNullException exc) 
                    { 
                        Assert.AreEqual(""second"", exc.ParamName); 
                    }

                    try 
                    { 
                        a.Concat(default(Dictionary<object, int>.ValueCollection)); 
                        Assert.Fail(); 
                    } 
                    catch (ArgumentNullException exc) 
                    { 
                        Assert.AreEqual(""second"", exc.ParamName); 
                    }

                    try 
                    { 
                        a.Concat(default(HashSet<int>)); 
                        Assert.Fail(); 
                    } 
                    catch (ArgumentNullException exc) 
                    { 
                        Assert.AreEqual(""second"", exc.ParamName); 
                    }

                    try 
                    { 
                        a.Concat(default(LinkedList<int>)); 
                        Assert.Fail(); 
                    } 
                    catch (ArgumentNullException exc) 
                    { 
                        Assert.AreEqual(""second"", exc.ParamName); 
                    }

                    try 
                    { 
                        a.Concat(default(List<int>)); 
                        Assert.Fail(); 
                    } 
                    catch (ArgumentNullException exc) 
                    { 
                        Assert.AreEqual(""second"", exc.ParamName); 
                    }

                    try 
                    { 
                        a.Concat(default(Queue<int>)); 
                        Assert.Fail(); 
                    } 
                    catch (ArgumentNullException exc) 
                    { 
                        Assert.AreEqual(""second"", exc.ParamName); 
                    }

                    try 
                    { 
                        a.Concat(default(SortedDictionary<int, object>.KeyCollection)); 
                        Assert.Fail(); 
                    } 
                    catch (ArgumentNullException exc) 
                    { 
                        Assert.AreEqual(""second"", exc.ParamName); 
                    }

                    try 
                    { 
                        a.Concat(default(SortedDictionary<object, int>.ValueCollection)); 
                        Assert.Fail(); 
                    } 
                    catch (ArgumentNullException exc) 
                    { 
                        Assert.AreEqual(""second"", exc.ParamName); 
                    }

                    try 
                    { 
                        a.Concat(default(SortedSet<int>)); 
                        Assert.Fail(); 
                    } 
                    catch (ArgumentNullException exc) 
                    { 
                        Assert.AreEqual(""second"", exc.ParamName); 
                    }

                    try 
                    { 
                        a.Concat(default(Stack<int>)); 
                        Assert.Fail(); 
                    } 
                    catch (ArgumentNullException exc) 
                    { 
                        Assert.AreEqual(""second"", exc.ParamName); 
                    }
                  }",
                typeof(GroupByDefaultEnumerable<,,,,>),
                typeof(GroupBySpecificEnumerable<,,,,>),
                typeof(LookupDefaultEnumerable<,>),
                typeof(LookupSpecificEnumerable<,>)
            );
        }

        [TestMethod]
        public void Errors_Weird()
        {
            var empty = Enumerable.Empty<int>();
            var emptyOrdered = empty.OrderBy(x => x);
            var groupByDefault = new[] { 1, 1, 2, 2, 3, 3 }.GroupBy(x => x);
            var groupBySpecific = new[] { "hello", "HELLO", "world", "WORLD", "foo", "FOO" }.GroupBy(x => x, StringComparer.OrdinalIgnoreCase);
            var lookupDefault = new int[] { 1, 1, 2, 2, 3, 3 }.ToLookup(x => x);
            var lookupSpecific = new int[] { 1, 1, 2, 2, 3, 3 }.ToLookup(x => x, new _IntComparer());
            var range = Enumerable.Range(1, 5);
            var repeat = Enumerable.Repeat("foo", 5);
            var reverseRange = Enumerable.Range(1, 5).Reverse();
            var oneItemDefault = Enumerable.Empty<int>().DefaultIfEmpty();
            var oneItemSpecific = Enumerable.Empty<int>().DefaultIfEmpty(4);
            var oneItemDefaultOrdered = oneItemDefault.OrderBy(x => x);
            var oneItemSpecificOrdered = oneItemSpecific.OrderBy(x => x);

            // empty
            {
                try { empty.Concat(default(int[])); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { empty.Concat(default(IEnumerable<int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { empty.Concat(default(Dictionary<int, object>.KeyCollection)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { empty.Concat(default(Dictionary<object, int>.ValueCollection)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { empty.Concat(default(HashSet<int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { empty.Concat(default(LinkedList<int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { empty.Concat(default(List<int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { empty.Concat(default(Queue<int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { empty.Concat(default(SortedDictionary<int, object>.KeyCollection)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { empty.Concat(default(SortedDictionary<object, int>.ValueCollection)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { empty.Concat(default(SortedSet<int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { empty.Concat(default(Stack<int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("second", exc.ParamName); }
            }

            // emptyOrdered
            {
                try { emptyOrdered.Concat(default(int[])); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { emptyOrdered.Concat(default(IEnumerable<int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { emptyOrdered.Concat(default(Dictionary<int, object>.KeyCollection)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { emptyOrdered.Concat(default(Dictionary<object, int>.ValueCollection)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { emptyOrdered.Concat(default(HashSet<int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { emptyOrdered.Concat(default(LinkedList<int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { emptyOrdered.Concat(default(List<int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { emptyOrdered.Concat(default(Queue<int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { emptyOrdered.Concat(default(SortedDictionary<int, object>.KeyCollection)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { emptyOrdered.Concat(default(SortedDictionary<object, int>.ValueCollection)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { emptyOrdered.Concat(default(SortedSet<int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { emptyOrdered.Concat(default(Stack<int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("second", exc.ParamName); }
            }

            // groupByDefault
            {
                try { groupByDefault.Concat(default(GroupingEnumerable<int, int>[])); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { groupByDefault.Concat(default(IEnumerable<GroupingEnumerable<int, int>>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { groupByDefault.Concat(default(Dictionary<GroupingEnumerable<int, int>, object>.KeyCollection)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { groupByDefault.Concat(default(Dictionary<object, GroupingEnumerable<int, int>>.ValueCollection)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { groupByDefault.Concat(default(HashSet<GroupingEnumerable<int, int>>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { groupByDefault.Concat(default(LinkedList<GroupingEnumerable<int, int>>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { groupByDefault.Concat(default(List<GroupingEnumerable<int, int>>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { groupByDefault.Concat(default(Queue<GroupingEnumerable<int, int>>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { groupByDefault.Concat(default(SortedDictionary<GroupingEnumerable<int, int>, object>.KeyCollection)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { groupByDefault.Concat(default(SortedDictionary<object, GroupingEnumerable<int, int>>.ValueCollection)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { groupByDefault.Concat(default(SortedSet<GroupingEnumerable<int, int>>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { groupByDefault.Concat(default(Stack<GroupingEnumerable<int, int>>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("second", exc.ParamName); }
            }

            // groupBySpecific
            {
                try { groupBySpecific.Concat(default(GroupingEnumerable<string, string>[])); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { groupBySpecific.Concat(default(IEnumerable<GroupingEnumerable<string, string>>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { groupBySpecific.Concat(default(Dictionary<GroupingEnumerable<string, string>, object>.KeyCollection)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { groupBySpecific.Concat(default(Dictionary<object, GroupingEnumerable<string, string>>.ValueCollection)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { groupBySpecific.Concat(default(HashSet<GroupingEnumerable<string, string>>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { groupBySpecific.Concat(default(LinkedList<GroupingEnumerable<string, string>>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { groupBySpecific.Concat(default(List<GroupingEnumerable<string, string>>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { groupBySpecific.Concat(default(Queue<GroupingEnumerable<string, string>>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { groupBySpecific.Concat(default(SortedDictionary<GroupingEnumerable<string, string>, object>.KeyCollection)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { groupBySpecific.Concat(default(SortedDictionary<object, GroupingEnumerable<string, string>>.ValueCollection)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { groupBySpecific.Concat(default(SortedSet<GroupingEnumerable<string, string>>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { groupBySpecific.Concat(default(Stack<GroupingEnumerable<string, string>>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("second", exc.ParamName); }
            }

            // lookupDefault
            {
                try { lookupDefault.Concat(default(GroupingEnumerable<int, int>[])); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { lookupDefault.Concat(default(IEnumerable<GroupingEnumerable<int, int>>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { lookupDefault.Concat(default(Dictionary<GroupingEnumerable<int, int>, object>.KeyCollection)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { lookupDefault.Concat(default(Dictionary<object, GroupingEnumerable<int, int>>.ValueCollection)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { lookupDefault.Concat(default(HashSet<GroupingEnumerable<int, int>>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { lookupDefault.Concat(default(LinkedList<GroupingEnumerable<int, int>>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { lookupDefault.Concat(default(List<GroupingEnumerable<int, int>>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { lookupDefault.Concat(default(Queue<GroupingEnumerable<int, int>>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { lookupDefault.Concat(default(SortedDictionary<GroupingEnumerable<int, int>, object>.KeyCollection)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { lookupDefault.Concat(default(SortedDictionary<object, GroupingEnumerable<int, int>>.ValueCollection)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { lookupDefault.Concat(default(SortedSet<GroupingEnumerable<int, int>>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { lookupDefault.Concat(default(Stack<GroupingEnumerable<int, int>>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("second", exc.ParamName); }
            }

            // lookupSpecific
            {
                try { lookupSpecific.Concat(default(GroupingEnumerable<int, int>[])); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { lookupSpecific.Concat(default(IEnumerable<GroupingEnumerable<int, int>>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { lookupSpecific.Concat(default(Dictionary<GroupingEnumerable<int, int>, object>.KeyCollection)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { lookupSpecific.Concat(default(Dictionary<object, GroupingEnumerable<int, int>>.ValueCollection)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { lookupSpecific.Concat(default(HashSet<GroupingEnumerable<int, int>>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { lookupSpecific.Concat(default(LinkedList<GroupingEnumerable<int, int>>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { lookupSpecific.Concat(default(List<GroupingEnumerable<int, int>>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { lookupSpecific.Concat(default(Queue<GroupingEnumerable<int, int>>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { lookupSpecific.Concat(default(SortedDictionary<GroupingEnumerable<int, int>, object>.KeyCollection)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { lookupSpecific.Concat(default(SortedDictionary<object, GroupingEnumerable<int, int>>.ValueCollection)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { lookupSpecific.Concat(default(SortedSet<GroupingEnumerable<int, int>>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { lookupSpecific.Concat(default(Stack<GroupingEnumerable<int, int>>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("second", exc.ParamName); }
            }

            // range
            {
                try { range.Concat(default(int[])); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { range.Concat(default(IEnumerable<int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { range.Concat(default(Dictionary<int, object>.KeyCollection)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { range.Concat(default(Dictionary<object, int>.ValueCollection)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { range.Concat(default(HashSet<int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { range.Concat(default(LinkedList<int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { range.Concat(default(List<int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { range.Concat(default(Queue<int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { range.Concat(default(SortedDictionary<int, object>.KeyCollection)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { range.Concat(default(SortedDictionary<object, int>.ValueCollection)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { range.Concat(default(SortedSet<int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { range.Concat(default(Stack<int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("second", exc.ParamName); }
            }

            // reverseRange
            {
                try { reverseRange.Concat(default(int[])); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { reverseRange.Concat(default(IEnumerable<int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { reverseRange.Concat(default(Dictionary<int, object>.KeyCollection)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { reverseRange.Concat(default(Dictionary<object, int>.ValueCollection)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { reverseRange.Concat(default(HashSet<int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { reverseRange.Concat(default(LinkedList<int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { reverseRange.Concat(default(List<int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { reverseRange.Concat(default(Queue<int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { reverseRange.Concat(default(SortedDictionary<int, object>.KeyCollection)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { reverseRange.Concat(default(SortedDictionary<object, int>.ValueCollection)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { reverseRange.Concat(default(SortedSet<int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { reverseRange.Concat(default(Stack<int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("second", exc.ParamName); }
            }

            // oneItemDefault
            {
                try { oneItemDefault.Concat(default(int[])); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemDefault.Concat(default(IEnumerable<int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemDefault.Concat(default(Dictionary<int, object>.KeyCollection)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemDefault.Concat(default(Dictionary<object, int>.ValueCollection)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemDefault.Concat(default(HashSet<int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemDefault.Concat(default(LinkedList<int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemDefault.Concat(default(List<int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemDefault.Concat(default(Queue<int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemDefault.Concat(default(SortedDictionary<int, object>.KeyCollection)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemDefault.Concat(default(SortedDictionary<object, int>.ValueCollection)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemDefault.Concat(default(SortedSet<int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemDefault.Concat(default(Stack<int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("second", exc.ParamName); }
            }

            // oneItemSpecific
            {
                try { oneItemSpecific.Concat(default(int[])); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemSpecific.Concat(default(IEnumerable<int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemSpecific.Concat(default(Dictionary<int, object>.KeyCollection)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemSpecific.Concat(default(Dictionary<object, int>.ValueCollection)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemSpecific.Concat(default(HashSet<int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemSpecific.Concat(default(LinkedList<int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemSpecific.Concat(default(List<int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemSpecific.Concat(default(Queue<int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemSpecific.Concat(default(SortedDictionary<int, object>.KeyCollection)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemSpecific.Concat(default(SortedDictionary<object, int>.ValueCollection)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemSpecific.Concat(default(SortedSet<int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemSpecific.Concat(default(Stack<int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("second", exc.ParamName); }
            }

            // oneItemDefaultOrdered
            {
                try { oneItemDefaultOrdered.Concat(default(int[])); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemDefaultOrdered.Concat(default(IEnumerable<int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemDefaultOrdered.Concat(default(Dictionary<int, object>.KeyCollection)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemDefaultOrdered.Concat(default(Dictionary<object, int>.ValueCollection)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemDefaultOrdered.Concat(default(HashSet<int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemDefaultOrdered.Concat(default(LinkedList<int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemDefaultOrdered.Concat(default(List<int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemDefaultOrdered.Concat(default(Queue<int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemDefaultOrdered.Concat(default(SortedDictionary<int, object>.KeyCollection)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemDefaultOrdered.Concat(default(SortedDictionary<object, int>.ValueCollection)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemDefaultOrdered.Concat(default(SortedSet<int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemDefaultOrdered.Concat(default(Stack<int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("second", exc.ParamName); }
            }

            // oneItemSpecificOrdered
            {
                try { oneItemSpecificOrdered.Concat(default(int[])); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemSpecificOrdered.Concat(default(IEnumerable<int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemSpecificOrdered.Concat(default(Dictionary<int, object>.KeyCollection)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemSpecificOrdered.Concat(default(Dictionary<object, int>.ValueCollection)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemSpecificOrdered.Concat(default(HashSet<int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemSpecificOrdered.Concat(default(LinkedList<int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemSpecificOrdered.Concat(default(List<int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemSpecificOrdered.Concat(default(Queue<int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemSpecificOrdered.Concat(default(SortedDictionary<int, object>.KeyCollection)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemSpecificOrdered.Concat(default(SortedDictionary<object, int>.ValueCollection)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemSpecificOrdered.Concat(default(SortedSet<int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemSpecificOrdered.Concat(default(Stack<int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("second", exc.ParamName); }
            }
        }

        [TestMethod]
        public void Malformed()
        {
            Helper.ForEachMalformedEnumerableExpression<int>(
                @"a => { try { a.Concat(new[] { 1, 2, 3 }); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""first"", exc.ParamName); } }",
                typeof(GroupByDefaultEnumerable<,,,,>),
                typeof(GroupBySpecificEnumerable<,,,,>),
                typeof(LookupDefaultEnumerable<,>),
                typeof(LookupSpecificEnumerable<,>)
            );

            Helper.ForEachMalformedEnumerableExpression<int>(
                @"a =>
                    Helper.ForEachEnumerableExpression(
                        a,
                        new int[0],
                        res => { },
                        @""(a, b) => 
                           { 
                                try 
                                { 
                                    b.Concat(a); 
                                    Assert.Fail(); 
                                } catch (ArgumentException exc) 
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

        [TestMethod]
        public void Malformed_Weird()
        {
            var empty = new EmptyEnumerable<int>();
            var emptyOrdered = new EmptyOrderedEnumerable<int>();
            var groupByDefault = new GroupByDefaultEnumerable<int, int, int, EmptyEnumerable<int>, EmptyEnumerator<int>>();
            var groupBySpecific = new GroupBySpecificEnumerable<int, int, int, EmptyEnumerable<int>, EmptyEnumerator<int>>();
            var lookupDefault = new LookupDefaultEnumerable<int, int>();
            var lookupSpecific = new LookupSpecificEnumerable<int, int>();
            var range = new RangeEnumerable();
            var repeat = new RepeatEnumerable<int>();
            var reverseRange = new ReverseRangeEnumerable();
            var oneItemDefault = new OneItemDefaultEnumerable<int>();
            var oneItemSpecific = new OneItemSpecificEnumerable<int>();
            var oneItemDefaultOrdered = new OneItemDefaultOrderedEnumerable<int>();
            var oneItemSpecificOrdered = new OneItemSpecificOrderedEnumerable<int>();

            // empty
            {
                Helper.ForEachEnumerableExpression(
                    empty,
                    new int[0],
                    res => { },
                    @"(a, b) =>
                      {
                        try
                        {
                            a.Concat(b);
                            Assert.Fail();
                        }
                        catch(ArgumentException exc)
                        {
                            Assert.AreEqual(""first"", exc.ParamName);
                        }

                        try
                        {
                            b.Concat(a);
                            Assert.Fail();
                        }
                        catch(ArgumentException exc)
                        {
                            Assert.AreEqual(""second"", exc.ParamName);
                        }

                        return Helper.NoCallValue;
                      }",
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );

                // empty
                {
                    try
                    {
                        empty.Concat(Enumerable.Empty<int>());
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("first", exc.ParamName);
                    }

                    try
                    {
                        Enumerable.Empty<int>().Concat(empty);
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("second", exc.ParamName);
                    }
                }

                // emptyOrdered
                {
                    try
                    {
                        empty.Concat(Enumerable.Empty<int>().OrderBy(x => x));
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("first", exc.ParamName);
                    }

                    try
                    {
                        Enumerable.Empty<int>().OrderBy(x => x).Concat(empty);
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("second", exc.ParamName);
                    }
                }

                // groupByDefault
                {
                    var emptyGrouping = new EmptyEnumerable<GroupingEnumerable<int, int>>();
                    try
                    {
                        emptyGrouping.Concat(new int[0].GroupBy(x => x));
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("first", exc.ParamName);
                    }

                    try
                    {
                        new int[0].GroupBy(x => x).Concat(emptyGrouping);
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("second", exc.ParamName);
                    }
                }

                // groupBySpecific
                {
                    var emptyGrouping = new EmptyEnumerable<GroupingEnumerable<int, int>>();
                    try
                    {
                        emptyGrouping.Concat(new int[0].GroupBy(x => x, new _IntComparer()));
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("first", exc.ParamName);
                    }

                    try
                    {
                        new int[0].GroupBy(x => x, new _IntComparer()).Concat(emptyGrouping);
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("second", exc.ParamName);
                    }
                }

                // lookupDefault
                {
                    var emptyGrouping = new EmptyEnumerable<GroupingEnumerable<int, int>>();
                    try
                    {
                        emptyGrouping.Concat(new int[0].ToLookup(x => x));
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("first", exc.ParamName);
                    }

                    try
                    {
                        new int[0].ToLookup(x => x).Concat(emptyGrouping);
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("second", exc.ParamName);
                    }
                }

                // lookupSpecific
                {
                    var emptyGrouping = new EmptyEnumerable<GroupingEnumerable<int, int>>();
                    try
                    {
                        emptyGrouping.Concat(new int[0].ToLookup(x => x, new _IntComparer()));
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("first", exc.ParamName);
                    }

                    try
                    {
                        new int[0].ToLookup(x => x, new _IntComparer()).Concat(emptyGrouping);
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("second", exc.ParamName);
                    }
                }

                // range
                {
                    try
                    {
                        empty.Concat(Enumerable.Range(1, 5));
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("first", exc.ParamName);
                    }

                    try
                    {
                        Enumerable.Range(1, 5).Concat(empty);
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("second", exc.ParamName);
                    }
                }

                // repeat
                {
                    try
                    {
                        empty.Concat(Enumerable.Repeat(1, 5));
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("first", exc.ParamName);
                    }

                    try
                    {
                        Enumerable.Repeat(1, 5).Concat(empty);
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("second", exc.ParamName);
                    }
                }

                // reverseRange
                {
                    try
                    {
                        empty.Concat(Enumerable.Range(1, 5).Reverse());
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("first", exc.ParamName);
                    }

                    try
                    {
                        Enumerable.Range(1, 5).Reverse().Concat(empty);
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("second", exc.ParamName);
                    }
                }

                // oneItemDefault, oneItemSpecific, oneItemDefaultOrdered, oneItemSpecificOrdered
                {
                    try { empty.Concat(Enumerable.Empty<int>().DefaultIfEmpty()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                    try { Enumerable.Empty<int>().DefaultIfEmpty().Concat(empty); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                    try { empty.Concat(Enumerable.Empty<int>().DefaultIfEmpty(4)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                    try { Enumerable.Empty<int>().DefaultIfEmpty(4).Concat(empty); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                    try { empty.Concat(Enumerable.Empty<int>().DefaultIfEmpty().OrderBy(x => x)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                    try { Enumerable.Empty<int>().DefaultIfEmpty().OrderBy(x => x).Concat(empty); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                    try { empty.Concat(Enumerable.Empty<int>().DefaultIfEmpty(4).OrderBy(x => x)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                    try { Enumerable.Empty<int>().DefaultIfEmpty(4).OrderBy(x => x).Concat(empty); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                }
            }

            // emptyOrdered
            {
                Helper.ForEachEnumerableExpression(
                    emptyOrdered,
                    new int[0],
                    res => { },
                    @"(a, b) =>
                      {
                        try
                        {
                            a.Concat(b);
                            Assert.Fail();
                        }
                        catch(ArgumentException exc)
                        {
                            Assert.AreEqual(""first"", exc.ParamName);
                        }

                        try
                        {
                            b.Concat(a);
                            Assert.Fail();
                        }
                        catch(ArgumentException exc)
                        {
                            Assert.AreEqual(""second"", exc.ParamName);
                        }

                        return Helper.NoCallValue;
                      }",
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );

                // empty
                {
                    try
                    {
                        emptyOrdered.Concat(Enumerable.Empty<int>());
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("first", exc.ParamName);
                    }

                    try
                    {
                        Enumerable.Empty<int>().Concat(emptyOrdered);
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("second", exc.ParamName);
                    }
                }

                // emptyOrdered
                {
                    try
                    {
                        emptyOrdered.Concat(Enumerable.Empty<int>().OrderBy(x => x));
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("first", exc.ParamName);
                    }

                    try
                    {
                        Enumerable.Empty<int>().OrderBy(x => x).Concat(emptyOrdered);
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("second", exc.ParamName);
                    }
                }

                // groupByDefault
                {
                    var emptyGrouping = new EmptyOrderedEnumerable<GroupingEnumerable<int, int>>();
                    try
                    {
                        emptyGrouping.Concat(new int[0].GroupBy(x => x));
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("first", exc.ParamName);
                    }

                    try
                    {
                        new int[0].GroupBy(x => x).Concat(emptyGrouping);
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("second", exc.ParamName);
                    }
                }

                // groupBySpecific
                {
                    var emptyGrouping = new EmptyOrderedEnumerable<GroupingEnumerable<int, int>>();
                    try
                    {
                        emptyGrouping.Concat(new int[0].GroupBy(x => x, new _IntComparer()));
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("first", exc.ParamName);
                    }

                    try
                    {
                        new int[0].GroupBy(x => x, new _IntComparer()).Concat(emptyGrouping);
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("second", exc.ParamName);
                    }
                }

                // lookupDefault
                {
                    var emptyGrouping = new EmptyOrderedEnumerable<GroupingEnumerable<int, int>>();
                    try
                    {
                        emptyGrouping.Concat(new int[0].ToLookup(x => x));
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("first", exc.ParamName);
                    }

                    try
                    {
                        new int[0].ToLookup(x => x).Concat(emptyGrouping);
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("second", exc.ParamName);
                    }
                }

                // lookupSpecific
                {
                    var emptyGrouping = new EmptyOrderedEnumerable<GroupingEnumerable<int, int>>();
                    try
                    {
                        emptyGrouping.Concat(new int[0].ToLookup(x => x, new _IntComparer()));
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("first", exc.ParamName);
                    }

                    try
                    {
                        new int[0].ToLookup(x => x, new _IntComparer()).Concat(emptyGrouping);
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("second", exc.ParamName);
                    }
                }

                // range
                {
                    try
                    {
                        emptyOrdered.Concat(Enumerable.Range(1, 5));
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("first", exc.ParamName);
                    }

                    try
                    {
                        Enumerable.Range(1, 5).Concat(emptyOrdered);
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("second", exc.ParamName);
                    }
                }

                // repeat
                {
                    try
                    {
                        emptyOrdered.Concat(Enumerable.Repeat(1, 5));
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("first", exc.ParamName);
                    }

                    try
                    {
                        Enumerable.Repeat(1, 5).Concat(emptyOrdered);
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("second", exc.ParamName);
                    }
                }

                // reverseRange
                {
                    try
                    {
                        emptyOrdered.Concat(Enumerable.Range(1, 5).Reverse());
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("first", exc.ParamName);
                    }

                    try
                    {
                        Enumerable.Range(1, 5).Reverse().Concat(emptyOrdered);
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("second", exc.ParamName);
                    }
                }

                // oneItemDefault, oneItemSpecific, oneItemDefaultOrdered, oneItemSpecificOrdered
                {
                    try { emptyOrdered.Concat(Enumerable.Empty<int>().DefaultIfEmpty()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                    try { Enumerable.Empty<int>().DefaultIfEmpty().Concat(emptyOrdered); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                    try { emptyOrdered.Concat(Enumerable.Empty<int>().DefaultIfEmpty(4)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                    try { Enumerable.Empty<int>().DefaultIfEmpty(4).Concat(emptyOrdered); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                    try { emptyOrdered.Concat(Enumerable.Empty<int>().DefaultIfEmpty().OrderBy(x => x)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                    try { Enumerable.Empty<int>().DefaultIfEmpty().OrderBy(x => x).Concat(emptyOrdered); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                    try { emptyOrdered.Concat(Enumerable.Empty<int>().DefaultIfEmpty(4).OrderBy(x => x)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                    try { Enumerable.Empty<int>().DefaultIfEmpty(4).OrderBy(x => x).Concat(emptyOrdered); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                }
            }

            // groupByDefault
            {
                // empty
                {
                    try
                    {
                        groupByDefault.Concat(Enumerable.Empty<GroupingEnumerable<int, int>>());
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("first", exc.ParamName);
                    }

                    try
                    {
                        Enumerable.Empty<GroupingEnumerable<int, int>>().Concat(groupByDefault);
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("second", exc.ParamName);
                    }
                }

                // emptyOrdered
                {
                    try
                    {
                        groupByDefault.Concat(Enumerable.Empty<GroupingEnumerable<int, int>>().OrderBy(x => x));
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("first", exc.ParamName);
                    }

                    try
                    {
                        Enumerable.Empty<GroupingEnumerable<int, int>>().OrderBy(x => x).Concat(groupByDefault);
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("second", exc.ParamName);
                    }
                }

                // groupByDefault
                {
                    try
                    {
                        groupByDefault.Concat(new int[0].GroupBy(x => x));
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("first", exc.ParamName);
                    }

                    try
                    {
                        new int[0].GroupBy(x => x).Concat(groupByDefault);
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("second", exc.ParamName);
                    }
                }

                // groupBySpecific
                {
                    try
                    {
                        groupByDefault.Concat(new int[0].GroupBy(x => x, new _IntComparer()));
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("first", exc.ParamName);
                    }

                    try
                    {
                        new int[0].GroupBy(x => x, new _IntComparer()).Concat(groupByDefault);
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("second", exc.ParamName);
                    }
                }

                // lookupDefault
                {
                    try
                    {
                        groupByDefault.Concat(new int[0].ToLookup(x => x));
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("first", exc.ParamName);
                    }

                    try
                    {
                        new int[0].ToLookup(x => x).Concat(groupByDefault);
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("second", exc.ParamName);
                    }
                }

                // lookupSpecific
                {
                    try
                    {
                        groupByDefault.Concat(new int[0].ToLookup(x => x, new _IntComparer()));
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("first", exc.ParamName);
                    }

                    try
                    {
                        new int[0].ToLookup(x => x, new _IntComparer()).Concat(groupByDefault);
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("second", exc.ParamName);
                    }
                }

                // skipping range & reverse range

                // repeat
                {
                    try
                    {
                        groupByDefault.Concat(Enumerable.Repeat(new[] { 1 }.GroupBy(x => x).First(), 2));
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("first", exc.ParamName);
                    }

                    try
                    {
                        Enumerable.Repeat(new[] { 1 }.GroupBy(x => x).First(), 2).Concat(groupByDefault);
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("second", exc.ParamName);
                    }
                }

                // oneItemDefault, oneItemSpecific, oneItemDefaultOrdered, oneItemSpecificOrdered
                {
                    var goodGrouping = new[] { 1 }.GroupBy(x => x).First();
                    try { groupByDefault.Concat(Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                    try { Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty().Concat(groupByDefault); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                    try { groupByDefault.Concat(Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty(goodGrouping)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                    try { Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty(goodGrouping).Concat(groupByDefault); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                    try { groupByDefault.Concat(Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty().OrderBy(x => x)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                    try { Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty().OrderBy(x => x).Concat(groupByDefault); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                    try { groupByDefault.Concat(Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty(goodGrouping).OrderBy(x => x)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                    try { Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty(goodGrouping).OrderBy(x => x).Concat(groupByDefault); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                }
            }

            // groupBySpecific
            {
                // empty
                {
                    try
                    {
                        groupBySpecific.Concat(Enumerable.Empty<GroupingEnumerable<int, int>>());
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("first", exc.ParamName);
                    }

                    try
                    {
                        Enumerable.Empty<GroupingEnumerable<int, int>>().Concat(groupBySpecific);
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("second", exc.ParamName);
                    }
                }

                // emptyOrdered
                {
                    try
                    {
                        groupBySpecific.Concat(Enumerable.Empty<GroupingEnumerable<int, int>>().OrderBy(x => x));
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("first", exc.ParamName);
                    }

                    try
                    {
                        Enumerable.Empty<GroupingEnumerable<int, int>>().OrderBy(x => x).Concat(groupBySpecific);
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("second", exc.ParamName);
                    }
                }

                // groupByDefault
                {
                    try
                    {
                        groupBySpecific.Concat(new int[0].GroupBy(x => x));
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("first", exc.ParamName);
                    }

                    try
                    {
                        new int[0].GroupBy(x => x).Concat(groupBySpecific);
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("second", exc.ParamName);
                    }
                }

                // groupBySpecific
                {
                    try
                    {
                        groupBySpecific.Concat(new int[0].GroupBy(x => x, new _IntComparer()));
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("first", exc.ParamName);
                    }

                    try
                    {
                        new int[0].GroupBy(x => x, new _IntComparer()).Concat(groupBySpecific);
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("second", exc.ParamName);
                    }
                }

                // lookupDefault
                {
                    try
                    {
                        groupBySpecific.Concat(new int[0].ToLookup(x => x));
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("first", exc.ParamName);
                    }

                    try
                    {
                        new int[0].ToLookup(x => x).Concat(groupBySpecific);
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("second", exc.ParamName);
                    }
                }

                // lookupSpecific
                {
                    try
                    {
                        groupBySpecific.Concat(new int[0].ToLookup(x => x, new _IntComparer()));
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("first", exc.ParamName);
                    }

                    try
                    {
                        new int[0].ToLookup(x => x, new _IntComparer()).Concat(groupBySpecific);
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("second", exc.ParamName);
                    }
                }

                // skipping range & reverse range

                // repeat
                {
                    try
                    {
                        groupBySpecific.Concat(Enumerable.Repeat(new[] { 1 }.GroupBy(x => x).First(), 2));
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("first", exc.ParamName);
                    }

                    try
                    {
                        Enumerable.Repeat(new[] { 1 }.GroupBy(x => x).First(), 2).Concat(groupBySpecific);
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("second", exc.ParamName);
                    }
                }

                // oneItemDefault, oneItemSpecific, oneItemDefaultOrdered, oneItemSpecificOrdered
                {
                    var goodGrouping = new[] { 1 }.GroupBy(x => x).First();
                    try { groupBySpecific.Concat(Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                    try { Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty().Concat(groupBySpecific); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                    try { groupBySpecific.Concat(Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty(goodGrouping)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                    try { Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty(goodGrouping).Concat(groupBySpecific); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                    try { groupBySpecific.Concat(Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty().OrderBy(x => x)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                    try { Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty().OrderBy(x => x).Concat(groupBySpecific); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                    try { groupBySpecific.Concat(Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty(goodGrouping).OrderBy(x => x)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                    try { Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty(goodGrouping).OrderBy(x => x).Concat(groupBySpecific); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                }
            }

            // lookupDefault
            {
                // empty
                {
                    try
                    {
                        lookupDefault.Concat(Enumerable.Empty<GroupingEnumerable<int, int>>());
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("first", exc.ParamName);
                    }

                    try
                    {
                        Enumerable.Empty<GroupingEnumerable<int, int>>().Concat(lookupDefault);
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("second", exc.ParamName);
                    }
                }

                // emptyOrdered
                {
                    try
                    {
                        lookupDefault.Concat(Enumerable.Empty<GroupingEnumerable<int, int>>().OrderBy(x => x));
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("first", exc.ParamName);
                    }

                    try
                    {
                        Enumerable.Empty<GroupingEnumerable<int, int>>().OrderBy(x => x).Concat(lookupDefault);
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("second", exc.ParamName);
                    }
                }

                // groupByDefault
                {
                    try
                    {
                        lookupDefault.Concat(new int[0].GroupBy(x => x));
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("first", exc.ParamName);
                    }

                    try
                    {
                        new int[0].GroupBy(x => x).Concat(lookupDefault);
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("second", exc.ParamName);
                    }
                }

                // groupBySpecific
                {
                    try
                    {
                        lookupDefault.Concat(new int[0].GroupBy(x => x, new _IntComparer()));
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("first", exc.ParamName);
                    }

                    try
                    {
                        new int[0].GroupBy(x => x, new _IntComparer()).Concat(lookupDefault);
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("second", exc.ParamName);
                    }
                }

                // lookupDefault
                {
                    try
                    {
                        lookupDefault.Concat(new int[0].ToLookup(x => x));
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("first", exc.ParamName);
                    }

                    try
                    {
                        new int[0].ToLookup(x => x).Concat(lookupDefault);
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("second", exc.ParamName);
                    }
                }

                // lookupSpecific
                {
                    try
                    {
                        lookupDefault.Concat(new int[0].ToLookup(x => x, new _IntComparer()));
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("first", exc.ParamName);
                    }

                    try
                    {
                        new int[0].ToLookup(x => x, new _IntComparer()).Concat(lookupDefault);
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("second", exc.ParamName);
                    }
                }

                // skipping range & reverse range

                // repeat
                {
                    try
                    {
                        lookupDefault.Concat(Enumerable.Repeat(new[] { 1 }.GroupBy(x => x).First(), 2));
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("first", exc.ParamName);
                    }

                    try
                    {
                        Enumerable.Repeat(new[] { 1 }.GroupBy(x => x).First(), 2).Concat(lookupDefault);
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("second", exc.ParamName);
                    }
                }

                // oneItemDefault, oneItemSpecific, oneItemDefaultOrdered, oneItemSpecificOrdered
                {
                    var goodGrouping = new[] { 1 }.GroupBy(x => x).First();
                    try { lookupDefault.Concat(Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                    try { Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty().Concat(lookupDefault); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                    try { lookupDefault.Concat(Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty(goodGrouping)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                    try { Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty(goodGrouping).Concat(lookupDefault); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                    try { lookupDefault.Concat(Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty().OrderBy(x => x)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                    try { Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty().OrderBy(x => x).Concat(lookupDefault); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                    try { lookupDefault.Concat(Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty(goodGrouping).OrderBy(x => x)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                    try { Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty(goodGrouping).OrderBy(x => x).Concat(lookupDefault); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                }
            }

            // lookupSpecific
            {
                // empty
                {
                    try
                    {
                        lookupSpecific.Concat(Enumerable.Empty<GroupingEnumerable<int, int>>());
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("first", exc.ParamName);
                    }

                    try
                    {
                        Enumerable.Empty<GroupingEnumerable<int, int>>().Concat(lookupSpecific);
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("second", exc.ParamName);
                    }
                }

                // emptyOrdered
                {
                    try
                    {
                        lookupSpecific.Concat(Enumerable.Empty<GroupingEnumerable<int, int>>().OrderBy(x => x));
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("first", exc.ParamName);
                    }

                    try
                    {
                        Enumerable.Empty<GroupingEnumerable<int, int>>().OrderBy(x => x).Concat(lookupSpecific);
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("second", exc.ParamName);
                    }
                }

                // groupByDefault
                {
                    try
                    {
                        lookupSpecific.Concat(new int[0].GroupBy(x => x));
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("first", exc.ParamName);
                    }

                    try
                    {
                        new int[0].GroupBy(x => x).Concat(lookupSpecific);
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("second", exc.ParamName);
                    }
                }

                // groupBySpecific
                {
                    try
                    {
                        lookupSpecific.Concat(new int[0].GroupBy(x => x, new _IntComparer()));
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("first", exc.ParamName);
                    }

                    try
                    {
                        new int[0].GroupBy(x => x, new _IntComparer()).Concat(lookupSpecific);
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("second", exc.ParamName);
                    }
                }

                // lookupDefault
                {
                    try
                    {
                        lookupSpecific.Concat(new int[0].ToLookup(x => x));
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("first", exc.ParamName);
                    }

                    try
                    {
                        new int[0].ToLookup(x => x).Concat(lookupSpecific);
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("second", exc.ParamName);
                    }
                }

                // lookupSpecific
                {
                    try
                    {
                        lookupSpecific.Concat(new int[0].ToLookup(x => x, new _IntComparer()));
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("first", exc.ParamName);
                    }

                    try
                    {
                        new int[0].ToLookup(x => x, new _IntComparer()).Concat(lookupSpecific);
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("second", exc.ParamName);
                    }
                }

                // skipping range & reverse range

                // repeat
                {
                    try
                    {
                        lookupSpecific.Concat(Enumerable.Repeat(new[] { 1 }.GroupBy(x => x).First(), 2));
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("first", exc.ParamName);
                    }

                    try
                    {
                        Enumerable.Repeat(new[] { 1 }.GroupBy(x => x).First(), 2).Concat(lookupSpecific);
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("second", exc.ParamName);
                    }
                }

                // oneItemDefault, oneItemSpecific, oneItemDefaultOrdered, oneItemSpecificOrdered
                {
                    var goodGrouping = new[] { 1 }.GroupBy(x => x).First();
                    try { lookupSpecific.Concat(Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                    try { Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty().Concat(lookupSpecific); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                    try { lookupSpecific.Concat(Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty(goodGrouping)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                    try { Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty(goodGrouping).Concat(lookupSpecific); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                    try { lookupSpecific.Concat(Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty().OrderBy(x => x)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                    try { Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty().OrderBy(x => x).Concat(lookupSpecific); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                    try { lookupSpecific.Concat(Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty(goodGrouping).OrderBy(x => x)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                    try { Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty(goodGrouping).OrderBy(x => x).Concat(lookupSpecific); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                }
            }

            // range
            {
                Helper.ForEachEnumerableExpression(
                    range,
                    new int[0],
                    res => { },
                    @"(a, b) =>
                      {
                        try
                        {
                            a.Concat(b);
                            Assert.Fail();
                        }
                        catch(ArgumentException exc)
                        {
                            Assert.AreEqual(""first"", exc.ParamName);
                        }

                        try
                        {
                            b.Concat(a);
                            Assert.Fail();
                        }
                        catch(ArgumentException exc)
                        {
                            Assert.AreEqual(""second"", exc.ParamName);
                        }

                        return Helper.NoCallValue;
                      }",
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );

                // empty
                {
                    try
                    {
                        range.Concat(Enumerable.Empty<int>());
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("first", exc.ParamName);
                    }

                    try
                    {
                        Enumerable.Empty<int>().Concat(range);
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("second", exc.ParamName);
                    }
                }

                // emptyOrdered
                {
                    try
                    {
                        range.Concat(Enumerable.Empty<int>().OrderBy(x => x));
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("first", exc.ParamName);
                    }

                    try
                    {
                        Enumerable.Empty<int>().OrderBy(x => x).Concat(range);
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("second", exc.ParamName);
                    }
                }

                // range
                {
                    try
                    {
                        range.Concat(Enumerable.Range(1, 5));
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("first", exc.ParamName);
                    }

                    try
                    {
                        Enumerable.Range(1, 5).Concat(range);
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("second", exc.ParamName);
                    }
                }

                // repeat
                {
                    try
                    {
                        range.Concat(Enumerable.Repeat(1, 5));
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("first", exc.ParamName);
                    }

                    try
                    {
                        Enumerable.Repeat(1, 5).Concat(range);
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("second", exc.ParamName);
                    }
                }

                // reverseRange
                {
                    try
                    {
                        empty.Concat(Enumerable.Range(1, 5).Reverse());
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("first", exc.ParamName);
                    }

                    try
                    {
                        Enumerable.Range(1, 5).Reverse().Concat(empty);
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("second", exc.ParamName);
                    }
                }

                // oneItemDefault, oneItemSpecific, oneItemDefaultOrdered, oneItemSpecificOrdered
                {
                    try { range.Concat(Enumerable.Empty<int>().DefaultIfEmpty()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                    try { Enumerable.Empty<int>().DefaultIfEmpty().Concat(range); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                    try { range.Concat(Enumerable.Empty<int>().DefaultIfEmpty(4)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                    try { Enumerable.Empty<int>().DefaultIfEmpty(4).Concat(range); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                    try { range.Concat(Enumerable.Empty<int>().DefaultIfEmpty().OrderBy(x => x)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                    try { Enumerable.Empty<int>().DefaultIfEmpty().OrderBy(x => x).Concat(range); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                    try { range.Concat(Enumerable.Empty<int>().DefaultIfEmpty(4).OrderBy(x => x)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                    try { Enumerable.Empty<int>().DefaultIfEmpty(4).OrderBy(x => x).Concat(range); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                }
            }

            // repeat
            {
                Helper.ForEachEnumerableExpression(
                    repeat,
                    new int[0],
                    res => { },
                    @"(a, b) =>
                      {
                        try
                        {
                            a.Concat(b);
                            Assert.Fail();
                        }
                        catch(ArgumentException exc)
                        {
                            Assert.AreEqual(""first"", exc.ParamName);
                        }

                        try
                        {
                            b.Concat(a);
                            Assert.Fail();
                        }
                        catch(ArgumentException exc)
                        {
                            Assert.AreEqual(""second"", exc.ParamName);
                        }

                        return Helper.NoCallValue;
                      }",
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );

                // empty
                {
                    try
                    {
                        repeat.Concat(Enumerable.Empty<int>());
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("first", exc.ParamName);
                    }

                    try
                    {
                        Enumerable.Empty<int>().Concat(repeat);
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("second", exc.ParamName);
                    }
                }

                // emptyOrdered
                {
                    try
                    {
                        repeat.Concat(Enumerable.Empty<int>().OrderBy(x => x));
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("first", exc.ParamName);
                    }

                    try
                    {
                        Enumerable.Empty<int>().OrderBy(x => x).Concat(repeat);
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("second", exc.ParamName);
                    }
                }

                // groupByDefault
                {
                    var repeatGrouping = new RepeatEnumerable<GroupingEnumerable<int, int>>();
                    try
                    {
                        repeatGrouping.Concat(new int[0].GroupBy(x => x));
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("first", exc.ParamName);
                    }

                    try
                    {
                        new int[0].GroupBy(x => x).Concat(repeatGrouping);
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("second", exc.ParamName);
                    }
                }

                // groupBySpecific
                {
                    var repeatGrouping = new RepeatEnumerable<GroupingEnumerable<int, int>>();
                    try
                    {
                        repeatGrouping.Concat(new int[0].GroupBy(x => x, new _IntComparer()));
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("first", exc.ParamName);
                    }

                    try
                    {
                        new int[0].GroupBy(x => x, new _IntComparer()).Concat(repeatGrouping);
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("second", exc.ParamName);
                    }
                }

                // lookupDefault
                {
                    var repeatGrouping = new RepeatEnumerable<GroupingEnumerable<int, int>>();
                    try
                    {
                        repeatGrouping.Concat(new int[0].ToLookup(x => x));
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("first", exc.ParamName);
                    }

                    try
                    {
                        new int[0].ToLookup(x => x).Concat(repeatGrouping);
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("second", exc.ParamName);
                    }
                }

                // lookupSpecific
                {
                    var repeatGrouping = new RepeatEnumerable<GroupingEnumerable<int, int>>();
                    try
                    {
                        repeatGrouping.Concat(new int[0].ToLookup(x => x, new _IntComparer()));
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("first", exc.ParamName);
                    }

                    try
                    {
                        new int[0].ToLookup(x => x, new _IntComparer()).Concat(repeatGrouping);
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("second", exc.ParamName);
                    }
                }

                // range
                {
                    try
                    {
                        repeat.Concat(Enumerable.Range(1, 5));
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("first", exc.ParamName);
                    }

                    try
                    {
                        Enumerable.Range(1, 5).Concat(repeat);
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("second", exc.ParamName);
                    }
                }

                // repeat
                {
                    try
                    {
                        repeat.Concat(Enumerable.Repeat(1, 5));
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("first", exc.ParamName);
                    }

                    try
                    {
                        Enumerable.Repeat(1, 5).Concat(repeat);
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("second", exc.ParamName);
                    }
                }

                // reverseRange
                {
                    try
                    {
                        repeat.Concat(Enumerable.Range(1, 5).Reverse());
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("first", exc.ParamName);
                    }

                    try
                    {
                        Enumerable.Range(1, 5).Reverse().Concat(repeat);
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("second", exc.ParamName);
                    }
                }

                // oneItemDefault, oneItemSpecific, oneItemDefaultOrdered, oneItemSpecificOrdered
                {
                    try { repeat.Concat(Enumerable.Empty<int>().DefaultIfEmpty()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                    try { Enumerable.Empty<int>().DefaultIfEmpty().Concat(repeat); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                    try { repeat.Concat(Enumerable.Empty<int>().DefaultIfEmpty(4)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                    try { Enumerable.Empty<int>().DefaultIfEmpty(4).Concat(repeat); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                    try { repeat.Concat(Enumerable.Empty<int>().DefaultIfEmpty().OrderBy(x => x)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                    try { Enumerable.Empty<int>().DefaultIfEmpty().OrderBy(x => x).Concat(repeat); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                    try { repeat.Concat(Enumerable.Empty<int>().DefaultIfEmpty(4).OrderBy(x => x)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                    try { Enumerable.Empty<int>().DefaultIfEmpty(4).OrderBy(x => x).Concat(repeat); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                }
            }

            // reverseRange
            {
                Helper.ForEachEnumerableExpression(
                    reverseRange,
                    new int[0],
                    res => { },
                    @"(a, b) =>
                      {
                        try
                        {
                            a.Concat(b);
                            Assert.Fail();
                        }
                        catch(ArgumentException exc)
                        {
                            Assert.AreEqual(""first"", exc.ParamName);
                        }

                        try
                        {
                            b.Concat(a);
                            Assert.Fail();
                        }
                        catch(ArgumentException exc)
                        {
                            Assert.AreEqual(""second"", exc.ParamName);
                        }

                        return Helper.NoCallValue;
                      }",
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );

                // empty
                {
                    try
                    {
                        reverseRange.Concat(Enumerable.Empty<int>());
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("first", exc.ParamName);
                    }

                    try
                    {
                        Enumerable.Empty<int>().Concat(reverseRange);
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("second", exc.ParamName);
                    }
                }

                // emptyOrdered
                {
                    try
                    {
                        reverseRange.Concat(Enumerable.Empty<int>().OrderBy(x => x));
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("first", exc.ParamName);
                    }

                    try
                    {
                        Enumerable.Empty<int>().OrderBy(x => x).Concat(reverseRange);
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("second", exc.ParamName);
                    }
                }

                // range
                {
                    try
                    {
                        reverseRange.Concat(Enumerable.Range(1, 5));
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("first", exc.ParamName);
                    }

                    try
                    {
                        Enumerable.Range(1, 5).Concat(reverseRange);
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("second", exc.ParamName);
                    }
                }

                // repeat
                {
                    try
                    {
                        reverseRange.Concat(Enumerable.Repeat(1, 5));
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("first", exc.ParamName);
                    }

                    try
                    {
                        Enumerable.Repeat(1, 5).Concat(reverseRange);
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("second", exc.ParamName);
                    }
                }

                // reverseRange
                {
                    try
                    {
                        reverseRange.Concat(Enumerable.Range(1, 5).Reverse());
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("first", exc.ParamName);
                    }

                    try
                    {
                        Enumerable.Range(1, 5).Reverse().Concat(reverseRange);
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("second", exc.ParamName);
                    }
                }

                // oneItemDefault, oneItemSpecific, oneItemDefaultOrdered, oneItemSpecificOrdered
                {
                    try { reverseRange.Concat(Enumerable.Empty<int>().DefaultIfEmpty()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                    try { Enumerable.Empty<int>().DefaultIfEmpty().Concat(reverseRange); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                    try { reverseRange.Concat(Enumerable.Empty<int>().DefaultIfEmpty(4)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                    try { Enumerable.Empty<int>().DefaultIfEmpty(4).Concat(reverseRange); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                    try { reverseRange.Concat(Enumerable.Empty<int>().DefaultIfEmpty().OrderBy(x => x)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                    try { Enumerable.Empty<int>().DefaultIfEmpty().OrderBy(x => x).Concat(reverseRange); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                    try { reverseRange.Concat(Enumerable.Empty<int>().DefaultIfEmpty(4).OrderBy(x => x)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                    try { Enumerable.Empty<int>().DefaultIfEmpty(4).OrderBy(x => x).Concat(reverseRange); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                }
            }

            // oneItemDefault
            {
                Helper.ForEachEnumerableExpression(
                    oneItemDefault,
                    new int[0],
                    res => { },
                    @"(a, b) =>
                      {
                        try
                        {
                            a.Concat(b);
                            Assert.Fail();
                        }
                        catch(ArgumentException exc)
                        {
                            Assert.AreEqual(""first"", exc.ParamName);
                        }

                        try
                        {
                            b.Concat(a);
                            Assert.Fail();
                        }
                        catch(ArgumentException exc)
                        {
                            Assert.AreEqual(""second"", exc.ParamName);
                        }

                        return Helper.NoCallValue;
                      }",
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );

                // empty
                {
                    try
                    {
                        oneItemDefault.Concat(Enumerable.Empty<int>());
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("first", exc.ParamName);
                    }

                    try
                    {
                        Enumerable.Empty<int>().Concat(oneItemDefault);
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("second", exc.ParamName);
                    }
                }

                // emptyOrdered
                {
                    try
                    {
                        oneItemDefault.Concat(Enumerable.Empty<int>().OrderBy(x => x));
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("first", exc.ParamName);
                    }

                    try
                    {
                        Enumerable.Empty<int>().OrderBy(x => x).Concat(oneItemDefault);
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("second", exc.ParamName);
                    }
                }

                // groupByDefault
                {
                    var oneItemDefaultGrouping = new OneItemDefaultEnumerable<GroupingEnumerable<int, int>>();
                    try
                    {
                        oneItemDefaultGrouping.Concat(new int[0].GroupBy(x => x));
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("first", exc.ParamName);
                    }

                    try
                    {
                        new int[0].GroupBy(x => x).Concat(oneItemDefaultGrouping);
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("second", exc.ParamName);
                    }
                }

                // groupBySpecific
                {
                    var oneItemDefaultGrouping = new OneItemDefaultEnumerable<GroupingEnumerable<int, int>>();
                    try
                    {
                        oneItemDefaultGrouping.Concat(new int[0].GroupBy(x => x, new _IntComparer()));
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("first", exc.ParamName);
                    }

                    try
                    {
                        new int[0].GroupBy(x => x, new _IntComparer()).Concat(oneItemDefaultGrouping);
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("second", exc.ParamName);
                    }
                }

                // lookupDefault
                {
                    var oneItemDefaultGrouping = new OneItemDefaultEnumerable<GroupingEnumerable<int, int>>();
                    try
                    {
                        oneItemDefaultGrouping.Concat(new int[0].ToLookup(x => x));
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("first", exc.ParamName);
                    }

                    try
                    {
                        new int[0].ToLookup(x => x).Concat(oneItemDefaultGrouping);
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("second", exc.ParamName);
                    }
                }

                // lookupSpecific
                {
                    var oneItemDefaultGrouping = new OneItemDefaultEnumerable<GroupingEnumerable<int, int>>();
                    try
                    {
                        oneItemDefaultGrouping.Concat(new int[0].ToLookup(x => x, new _IntComparer()));
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("first", exc.ParamName);
                    }

                    try
                    {
                        new int[0].ToLookup(x => x, new _IntComparer()).Concat(oneItemDefaultGrouping);
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("second", exc.ParamName);
                    }
                }

                // range
                {
                    try
                    {
                        oneItemDefault.Concat(Enumerable.Range(1, 5));
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("first", exc.ParamName);
                    }

                    try
                    {
                        Enumerable.Range(1, 5).Concat(oneItemDefault);
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("second", exc.ParamName);
                    }
                }

                // repeat
                {
                    try
                    {
                        oneItemDefault.Concat(Enumerable.Repeat(1, 5));
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("first", exc.ParamName);
                    }

                    try
                    {
                        Enumerable.Repeat(1, 5).Concat(oneItemDefault);
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("second", exc.ParamName);
                    }
                }

                // reverseRange
                {
                    try
                    {
                        oneItemDefault.Concat(Enumerable.Range(1, 5).Reverse());
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("first", exc.ParamName);
                    }

                    try
                    {
                        Enumerable.Range(1, 5).Reverse().Concat(oneItemDefault);
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("second", exc.ParamName);
                    }
                }

                // oneItemDefault, oneItemSpecific, oneItemDefaultOrdered, oneItemSpecificOrdered
                {
                    try { oneItemDefault.Concat(Enumerable.Empty<int>().DefaultIfEmpty()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                    try { Enumerable.Empty<int>().DefaultIfEmpty().Concat(oneItemDefault); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                    try { oneItemDefault.Concat(Enumerable.Empty<int>().DefaultIfEmpty(4)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                    try { Enumerable.Empty<int>().DefaultIfEmpty(4).Concat(oneItemDefault); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                    try { oneItemDefault.Concat(Enumerable.Empty<int>().DefaultIfEmpty().OrderBy(x => x)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                    try { Enumerable.Empty<int>().DefaultIfEmpty().OrderBy(x => x).Concat(oneItemDefault); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                    try { oneItemDefault.Concat(Enumerable.Empty<int>().DefaultIfEmpty(4).OrderBy(x => x)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                    try { Enumerable.Empty<int>().DefaultIfEmpty(4).OrderBy(x => x).Concat(oneItemDefault); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                }
            }

            // oneItemSpecific
            {
                Helper.ForEachEnumerableExpression(
                    oneItemSpecific,
                    new int[0],
                    res => { },
                    @"(a, b) =>
                      {
                        try
                        {
                            a.Concat(b);
                            Assert.Fail();
                        }
                        catch(ArgumentException exc)
                        {
                            Assert.AreEqual(""first"", exc.ParamName);
                        }

                        try
                        {
                            b.Concat(a);
                            Assert.Fail();
                        }
                        catch(ArgumentException exc)
                        {
                            Assert.AreEqual(""second"", exc.ParamName);
                        }

                        return Helper.NoCallValue;
                      }",
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );

                // empty
                {
                    try
                    {
                        oneItemSpecific.Concat(Enumerable.Empty<int>());
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("first", exc.ParamName);
                    }

                    try
                    {
                        Enumerable.Empty<int>().Concat(oneItemSpecific);
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("second", exc.ParamName);
                    }
                }

                // emptyOrdered
                {
                    try
                    {
                        oneItemSpecific.Concat(Enumerable.Empty<int>().OrderBy(x => x));
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("first", exc.ParamName);
                    }

                    try
                    {
                        Enumerable.Empty<int>().OrderBy(x => x).Concat(oneItemSpecific);
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("second", exc.ParamName);
                    }
                }

                // groupByDefault
                {
                    var oneItemSpecificGrouping = new OneItemSpecificEnumerable<GroupingEnumerable<int, int>>();
                    try
                    {
                        oneItemSpecificGrouping.Concat(new int[0].GroupBy(x => x));
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("first", exc.ParamName);
                    }

                    try
                    {
                        new int[0].GroupBy(x => x).Concat(oneItemSpecificGrouping);
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("second", exc.ParamName);
                    }
                }

                // groupBySpecific
                {
                    var oneItemSpecificGrouping = new OneItemSpecificEnumerable<GroupingEnumerable<int, int>>();
                    try
                    {
                        oneItemSpecificGrouping.Concat(new int[0].GroupBy(x => x, new _IntComparer()));
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("first", exc.ParamName);
                    }

                    try
                    {
                        new int[0].GroupBy(x => x, new _IntComparer()).Concat(oneItemSpecificGrouping);
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("second", exc.ParamName);
                    }
                }

                // lookupDefault
                {
                    var oneItemSpecificGrouping = new OneItemSpecificEnumerable<GroupingEnumerable<int, int>>();
                    try
                    {
                        oneItemSpecificGrouping.Concat(new int[0].ToLookup(x => x));
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("first", exc.ParamName);
                    }

                    try
                    {
                        new int[0].ToLookup(x => x).Concat(oneItemSpecificGrouping);
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("second", exc.ParamName);
                    }
                }

                // lookupSpecific
                {
                    var oneItemSpecificGrouping = new OneItemSpecificEnumerable<GroupingEnumerable<int, int>>();
                    try
                    {
                        oneItemSpecificGrouping.Concat(new int[0].ToLookup(x => x, new _IntComparer()));
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("first", exc.ParamName);
                    }

                    try
                    {
                        new int[0].ToLookup(x => x, new _IntComparer()).Concat(oneItemSpecificGrouping);
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("second", exc.ParamName);
                    }
                }

                // range
                {
                    try
                    {
                        oneItemSpecific.Concat(Enumerable.Range(1, 5));
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("first", exc.ParamName);
                    }

                    try
                    {
                        Enumerable.Range(1, 5).Concat(oneItemSpecific);
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("second", exc.ParamName);
                    }
                }

                // repeat
                {
                    try
                    {
                        oneItemSpecific.Concat(Enumerable.Repeat(1, 5));
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("first", exc.ParamName);
                    }

                    try
                    {
                        Enumerable.Repeat(1, 5).Concat(oneItemSpecific);
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("second", exc.ParamName);
                    }
                }

                // reverseRange
                {
                    try
                    {
                        oneItemSpecific.Concat(Enumerable.Range(1, 5).Reverse());
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("first", exc.ParamName);
                    }

                    try
                    {
                        Enumerable.Range(1, 5).Reverse().Concat(oneItemSpecific);
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("second", exc.ParamName);
                    }
                }

                // oneItemDefault, oneItemSpecific, oneItemDefaultOrdered, oneItemSpecificOrdered
                {
                    try { oneItemSpecific.Concat(Enumerable.Empty<int>().DefaultIfEmpty()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                    try { Enumerable.Empty<int>().DefaultIfEmpty().Concat(oneItemSpecific); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                    try { oneItemSpecific.Concat(Enumerable.Empty<int>().DefaultIfEmpty(4)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                    try { Enumerable.Empty<int>().DefaultIfEmpty(4).Concat(oneItemSpecific); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                    try { oneItemSpecific.Concat(Enumerable.Empty<int>().DefaultIfEmpty().OrderBy(x => x)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                    try { Enumerable.Empty<int>().DefaultIfEmpty().OrderBy(x => x).Concat(oneItemSpecific); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                    try { oneItemSpecific.Concat(Enumerable.Empty<int>().DefaultIfEmpty(4).OrderBy(x => x)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                    try { Enumerable.Empty<int>().DefaultIfEmpty(4).OrderBy(x => x).Concat(oneItemSpecific); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                }
            }

            // oneItemDefaultOrdered
            {
                Helper.ForEachEnumerableExpression(
                    oneItemDefaultOrdered,
                    new int[0],
                    res => { },
                    @"(a, b) =>
                      {
                        try
                        {
                            a.Concat(b);
                            Assert.Fail();
                        }
                        catch(ArgumentException exc)
                        {
                            Assert.AreEqual(""first"", exc.ParamName);
                        }

                        try
                        {
                            b.Concat(a);
                            Assert.Fail();
                        }
                        catch(ArgumentException exc)
                        {
                            Assert.AreEqual(""second"", exc.ParamName);
                        }

                        return Helper.NoCallValue;
                      }",
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );

                // empty
                {
                    try
                    {
                        oneItemDefaultOrdered.Concat(Enumerable.Empty<int>());
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("first", exc.ParamName);
                    }

                    try
                    {
                        Enumerable.Empty<int>().Concat(oneItemDefaultOrdered);
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("second", exc.ParamName);
                    }
                }

                // emptyOrdered
                {
                    try
                    {
                        oneItemDefaultOrdered.Concat(Enumerable.Empty<int>().OrderBy(x => x));
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("first", exc.ParamName);
                    }

                    try
                    {
                        Enumerable.Empty<int>().OrderBy(x => x).Concat(oneItemDefaultOrdered);
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("second", exc.ParamName);
                    }
                }

                // groupByDefault
                {
                    var oneItemDefaultOrderedGrouping = new OneItemDefaultOrderedEnumerable<GroupingEnumerable<int, int>>();
                    try
                    {
                        oneItemDefaultOrderedGrouping.Concat(new int[0].GroupBy(x => x));
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("first", exc.ParamName);
                    }

                    try
                    {
                        new int[0].GroupBy(x => x).Concat(oneItemDefaultOrderedGrouping);
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("second", exc.ParamName);
                    }
                }

                // groupBySpecific
                {
                    var oneItemDefaultOrderedGrouping = new OneItemDefaultOrderedEnumerable<GroupingEnumerable<int, int>>();
                    try
                    {
                        oneItemDefaultOrderedGrouping.Concat(new int[0].GroupBy(x => x, new _IntComparer()));
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("first", exc.ParamName);
                    }

                    try
                    {
                        new int[0].GroupBy(x => x, new _IntComparer()).Concat(oneItemDefaultOrderedGrouping);
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("second", exc.ParamName);
                    }
                }

                // lookupDefault
                {
                    var oneItemDefaultOrderedGrouping = new OneItemDefaultOrderedEnumerable<GroupingEnumerable<int, int>>();
                    try
                    {
                        oneItemDefaultOrderedGrouping.Concat(new int[0].ToLookup(x => x));
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("first", exc.ParamName);
                    }

                    try
                    {
                        new int[0].ToLookup(x => x).Concat(oneItemDefaultOrderedGrouping);
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("second", exc.ParamName);
                    }
                }

                // lookupSpecific
                {
                    var oneItemDefaultOrderedGrouping = new OneItemDefaultOrderedEnumerable<GroupingEnumerable<int, int>>();
                    try
                    {
                        oneItemDefaultOrderedGrouping.Concat(new int[0].ToLookup(x => x, new _IntComparer()));
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("first", exc.ParamName);
                    }

                    try
                    {
                        new int[0].ToLookup(x => x, new _IntComparer()).Concat(oneItemDefaultOrderedGrouping);
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("second", exc.ParamName);
                    }
                }

                // range
                {
                    try
                    {
                        oneItemDefaultOrdered.Concat(Enumerable.Range(1, 5));
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("first", exc.ParamName);
                    }

                    try
                    {
                        Enumerable.Range(1, 5).Concat(oneItemDefaultOrdered);
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("second", exc.ParamName);
                    }
                }

                // repeat
                {
                    try
                    {
                        oneItemDefaultOrdered.Concat(Enumerable.Repeat(1, 5));
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("first", exc.ParamName);
                    }

                    try
                    {
                        Enumerable.Repeat(1, 5).Concat(oneItemDefaultOrdered);
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("second", exc.ParamName);
                    }
                }

                // reverseRange
                {
                    try
                    {
                        oneItemDefaultOrdered.Concat(Enumerable.Range(1, 5).Reverse());
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("first", exc.ParamName);
                    }

                    try
                    {
                        Enumerable.Range(1, 5).Reverse().Concat(oneItemDefaultOrdered);
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("second", exc.ParamName);
                    }
                }

                // oneItemDefault, oneItemSpecific, oneItemDefaultOrdered, oneItemSpecificOrdered
                {
                    try { oneItemDefaultOrdered.Concat(Enumerable.Empty<int>().DefaultIfEmpty()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                    try { Enumerable.Empty<int>().DefaultIfEmpty().Concat(oneItemDefaultOrdered); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                    try { oneItemDefaultOrdered.Concat(Enumerable.Empty<int>().DefaultIfEmpty(4)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                    try { Enumerable.Empty<int>().DefaultIfEmpty(4).Concat(oneItemDefaultOrdered); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                    try { oneItemDefaultOrdered.Concat(Enumerable.Empty<int>().DefaultIfEmpty().OrderBy(x => x)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                    try { Enumerable.Empty<int>().DefaultIfEmpty().OrderBy(x => x).Concat(oneItemDefaultOrdered); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                    try { oneItemDefaultOrdered.Concat(Enumerable.Empty<int>().DefaultIfEmpty(4).OrderBy(x => x)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                    try { Enumerable.Empty<int>().DefaultIfEmpty(4).OrderBy(x => x).Concat(oneItemDefaultOrdered); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                }
            }

            // oneItemSpecificOrdered
            {
                Helper.ForEachEnumerableExpression(
                    oneItemSpecificOrdered,
                    new int[0],
                    res => { },
                    @"(a, b) =>
                      {
                        try
                        {
                            a.Concat(b);
                            Assert.Fail();
                        }
                        catch(ArgumentException exc)
                        {
                            Assert.AreEqual(""first"", exc.ParamName);
                        }

                        try
                        {
                            b.Concat(a);
                            Assert.Fail();
                        }
                        catch(ArgumentException exc)
                        {
                            Assert.AreEqual(""second"", exc.ParamName);
                        }

                        return Helper.NoCallValue;
                      }",
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );

                // empty
                {
                    try
                    {
                        oneItemSpecificOrdered.Concat(Enumerable.Empty<int>());
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("first", exc.ParamName);
                    }

                    try
                    {
                        Enumerable.Empty<int>().Concat(oneItemSpecificOrdered);
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("second", exc.ParamName);
                    }
                }

                // emptyOrdered
                {
                    try
                    {
                        oneItemSpecificOrdered.Concat(Enumerable.Empty<int>().OrderBy(x => x));
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("first", exc.ParamName);
                    }

                    try
                    {
                        Enumerable.Empty<int>().OrderBy(x => x).Concat(oneItemSpecificOrdered);
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("second", exc.ParamName);
                    }
                }

                // groupByDefault
                {
                    var oneItemSpecificOrderedGrouping = new OneItemSpecificOrderedEnumerable<GroupingEnumerable<int, int>>();
                    try
                    {
                        oneItemSpecificOrderedGrouping.Concat(new int[0].GroupBy(x => x));
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("first", exc.ParamName);
                    }

                    try
                    {
                        new int[0].GroupBy(x => x).Concat(oneItemSpecificOrderedGrouping);
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("second", exc.ParamName);
                    }
                }

                // groupBySpecific
                {
                    var oneItemSpecificOrderedGrouping = new OneItemSpecificOrderedEnumerable<GroupingEnumerable<int, int>>();
                    try
                    {
                        oneItemSpecificOrderedGrouping.Concat(new int[0].GroupBy(x => x, new _IntComparer()));
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("first", exc.ParamName);
                    }

                    try
                    {
                        new int[0].GroupBy(x => x, new _IntComparer()).Concat(oneItemSpecificOrderedGrouping);
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("second", exc.ParamName);
                    }
                }

                // lookupDefault
                {
                    var oneItemSpecificOrderedGrouping = new OneItemSpecificOrderedEnumerable<GroupingEnumerable<int, int>>();
                    try
                    {
                        oneItemSpecificOrderedGrouping.Concat(new int[0].ToLookup(x => x));
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("first", exc.ParamName);
                    }

                    try
                    {
                        new int[0].ToLookup(x => x).Concat(oneItemSpecificOrderedGrouping);
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("second", exc.ParamName);
                    }
                }

                // lookupSpecific
                {
                    var oneItemSpecificOrderedGrouping = new OneItemSpecificOrderedEnumerable<GroupingEnumerable<int, int>>();
                    try
                    {
                        oneItemSpecificOrderedGrouping.Concat(new int[0].ToLookup(x => x, new _IntComparer()));
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("first", exc.ParamName);
                    }

                    try
                    {
                        new int[0].ToLookup(x => x, new _IntComparer()).Concat(oneItemSpecificOrderedGrouping);
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("second", exc.ParamName);
                    }
                }

                // range
                {
                    try
                    {
                        oneItemSpecificOrdered.Concat(Enumerable.Range(1, 5));
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("first", exc.ParamName);
                    }

                    try
                    {
                        Enumerable.Range(1, 5).Concat(oneItemSpecificOrdered);
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("second", exc.ParamName);
                    }
                }

                // repeat
                {
                    try
                    {
                        oneItemSpecificOrdered.Concat(Enumerable.Repeat(1, 5));
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("first", exc.ParamName);
                    }

                    try
                    {
                        Enumerable.Repeat(1, 5).Concat(oneItemSpecificOrdered);
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("second", exc.ParamName);
                    }
                }

                // reverseRange
                {
                    try
                    {
                        oneItemSpecificOrdered.Concat(Enumerable.Range(1, 5).Reverse());
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("first", exc.ParamName);
                    }

                    try
                    {
                        Enumerable.Range(1, 5).Reverse().Concat(oneItemSpecificOrdered);
                        Assert.Fail();
                    }
                    catch (ArgumentException exc)
                    {
                        Assert.AreEqual("second", exc.ParamName);
                    }
                }

                // oneItemDefault, oneItemSpecific, oneItemDefaultOrdered, oneItemSpecificOrdered
                {
                    try { oneItemSpecificOrdered.Concat(Enumerable.Empty<int>().DefaultIfEmpty()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                    try { Enumerable.Empty<int>().DefaultIfEmpty().Concat(oneItemSpecificOrdered); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                    try { oneItemSpecificOrdered.Concat(Enumerable.Empty<int>().DefaultIfEmpty(4)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                    try { Enumerable.Empty<int>().DefaultIfEmpty(4).Concat(oneItemSpecificOrdered); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                    try { oneItemSpecificOrdered.Concat(Enumerable.Empty<int>().DefaultIfEmpty().OrderBy(x => x)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                    try { Enumerable.Empty<int>().DefaultIfEmpty().OrderBy(x => x).Concat(oneItemSpecificOrdered); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                    try { oneItemSpecificOrdered.Concat(Enumerable.Empty<int>().DefaultIfEmpty(4).OrderBy(x => x)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                    try { Enumerable.Empty<int>().DefaultIfEmpty(4).OrderBy(x => x).Concat(oneItemSpecificOrdered); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                }
            }
        }

        [TestMethod]
        public void Simple()
        {
            var e1 = new[] { 1, 2 };
            var e2 = new[] { 3, 4 };

            var asConcat = e1.Concat(e2);

            Assert.IsTrue(asConcat.GetType().IsValueType);

            var ret = new List<int>();
            foreach (var item in asConcat)
            {
                ret.Add(item);
            }

            Assert.AreEqual(4, ret.Count);
            Assert.AreEqual(1, ret[0]);
            Assert.AreEqual(2, ret[1]);
            Assert.AreEqual(3, ret[2]);
            Assert.AreEqual(4, ret[3]);
        }

        [TestMethod]
        public void Empty()
        {
            var e1 = Enumerable.Empty<int>();
            var e2 = Enumerable.Empty<int>();

            var asConcat = e1.Concat(e2);

            Assert.IsTrue(asConcat.GetType().IsValueType);

            var e = asConcat.GetEnumerator();

            Assert.IsFalse(e.MoveNext());
        }
    }
}