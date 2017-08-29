using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using TestHelpers;

namespace LinqAF.Tests
{
    [TestClass]
    public class IntersectTests
    {
        [TestMethod]
        public void Universal()
        {
            var enums = Helper.AllEnumerables();

            foreach (var e in enums)
            {
                System.Collections.Generic.List<string> missing;
                if (!Helper.Implements(e, typeof(LinqAF.Impl.IIntersect<,,>), out missing))
                {
                    Assert.Fail($"{e.Name} does not implement IIntersect ({string.Join(", ", missing)})");
                }
            }
        }

        [TestMethod]
        public void AcceptsAllEnumerables()
        {
            var missingSimple = new List<string>();
            var missingComparer = new List<string>();

            var iunion = typeof(Impl.IIntersect<,,>);
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
            var left = new[] { 1, 2, 3, 3, 4 }.Select(i => i);
            var right = new[] { 3, 4, 4, 5 }.Select(i => i);
            var asIntersect = left.Intersect(right);

            Assert.IsTrue(asIntersect.GetType().IsValueType);

            var res = new List<int>();
            foreach (var item in asIntersect)
            {
                res.Add(item);
            }

            Assert.AreEqual(2, res.Count);
            Assert.AreEqual(3, res[0]);
            Assert.AreEqual(4, res[1]);
        }

        public class _Comparer : IEqualityComparer<string>
        {
            public bool Equals(string x, string y)
            {
                return (x?.Length ?? 0) == (y?.Length ?? 0);
            }

            public int GetHashCode(string obj)
            {
                return (obj?.Length ?? 0).GetHashCode();
            }
        }

        [TestMethod]
        public void Comparer()
        {
            var left = new[] { 1, 22, 33, 44 }.Select(i => i.ToString());
            var right = new[] { 5, 66, 777 }.Select(i => i.ToString());
            var asIntersect = left.Intersect(right, new _Comparer());

            Assert.IsTrue(asIntersect.GetType().IsValueType);

            var res = new List<string>();
            foreach (var item in asIntersect)
            {
                res.Add(item);
            }

            Assert.AreEqual(2, res.Count);
            Assert.AreEqual("1", res[0]);
            Assert.AreEqual("22", res[1]);
        }



        [TestMethod]
        public void Chaining_Default1()
        {
            var allTypes = Helper.GetEnumerables<int>(new int[0]).Select(h => ((object)h).GetType()).OrderBy(o => o.FullName).ToArray();
            var subset = allTypes.Where((t, ix) => ix % 2 == 0).ToArray();

            // default
            {
                Helper.ForEachEnumerableNoRetExpression(
                    new[] { 1, 2, 3, 4 },
                    @"a => 
                    Helper.ForEachEnumerableExpression(
                        a,
                        new[] { 3, 4, 5, 6 },
                        res =>
                        {
                            Assert.AreEqual(2, res.Count);
                            Assert.AreEqual(3, res[0]);
                            Assert.AreEqual(4, res[1]);
                        },
                        ""(a, b) => a.Intersect(b)"",
                        typeof(EmptyEnumerable<>),
                        typeof(EmptyOrderedEnumerable<>),
                        typeof(GroupByDefaultEnumerable<,,,,>),
                        typeof(GroupBySpecificEnumerable<,,,,>),
                        typeof(LookupDefaultEnumerable<,>),
                        typeof(LookupSpecificEnumerable<,>)
                    )",
                    subset.Concat(
                        new[]
                        {
                            typeof(EmptyEnumerable<>),
                            typeof(EmptyOrderedEnumerable<>),
                            typeof(GroupByDefaultEnumerable<,,,,>),
                            typeof(GroupBySpecificEnumerable<,,,,>),
                            typeof(LookupDefaultEnumerable<,>),
                            typeof(LookupSpecificEnumerable<,>)
                        }
                    )
                    .ToArray()
                );
            }
        }

        [TestMethod]
        public void Chaining_Default2()
        {
            var allTypes = Helper.GetEnumerables<int>(new int[0]).Select(h => ((object)h).GetType()).OrderBy(o => o.FullName).ToArray();
            var subset = allTypes.Where((t, ix) => ix % 2 == 1).ToArray();

            // default
            {
                Helper.ForEachEnumerableNoRetExpression(
                    new[] { 1, 2, 3, 4 },
                    @"a => 
                    Helper.ForEachEnumerableExpression(
                        a,
                        new[] { 3, 4, 5, 6 },
                        res =>
                        {
                            Assert.AreEqual(2, res.Count);
                            Assert.AreEqual(3, res[0]);
                            Assert.AreEqual(4, res[1]);
                        },
                        ""(a, b) => a.Intersect(b)"",
                        typeof(EmptyEnumerable<>),
                        typeof(EmptyOrderedEnumerable<>),
                        typeof(GroupByDefaultEnumerable<,,,,>),
                        typeof(GroupBySpecificEnumerable<,,,,>),
                        typeof(LookupDefaultEnumerable<,>),
                        typeof(LookupSpecificEnumerable<,>)
                    )",
                    subset.Concat(
                        new[]
                        {
                            typeof(EmptyEnumerable<>),
                            typeof(EmptyOrderedEnumerable<>),
                            typeof(GroupByDefaultEnumerable<,,,,>),
                            typeof(GroupBySpecificEnumerable<,,,,>),
                            typeof(LookupDefaultEnumerable<,>),
                            typeof(LookupSpecificEnumerable<,>)
                        }
                    )
                    .ToArray()
                );
            }
        }

        [TestMethod]
        public void Chaining_Specific1()
        {
            var allTypes = Helper.GetEnumerables<int>(new int[0]).Select(h => ((object)h).GetType()).OrderBy(o => o.FullName).ToArray();
            var subset = allTypes.Where((t, ix) => ix % 2 == 0).ToArray();

            // specific
            {
                Helper.ForEachEnumerableNoRetExpression(
                    new[] { "foo", "fizz", "hello" },
                    @"a => 
                    Helper.ForEachEnumerableExpression(
                        a,
                        new[] { ""bar"", ""world"" },
                        res =>
                        {
                            Assert.AreEqual(2, res.Count);
                            Assert.AreEqual(""foo"", res[0]);
                            Assert.AreEqual(""hello"", res[1]);
                        },
                        ""(a, b) => a.Intersect(b, new IntersectTests._Comparer())"",
                        typeof(EmptyEnumerable<>),
                        typeof(EmptyOrderedEnumerable<>),
                        typeof(LookupDefaultEnumerable<,>),
                        typeof(LookupSpecificEnumerable<,>),
                        typeof(GroupByDefaultEnumerable<,,,,>),
                        typeof(GroupBySpecificEnumerable<,,,,>)
                    )",
                    subset.Concat(
                        new[]
                        {
                            typeof(EmptyEnumerable<>),
                            typeof(EmptyOrderedEnumerable<>),
                            typeof(GroupByDefaultEnumerable<,,,,>),
                            typeof(GroupBySpecificEnumerable<,,,,>),
                            typeof(LookupDefaultEnumerable<,>),
                            typeof(LookupSpecificEnumerable<,>)
                        }
                    )
                    .ToArray()
                );
            }
        }

        [TestMethod]
        public void Chaining_Specific2()
        {
            var allTypes = Helper.GetEnumerables<int>(new int[0]).Select(h => ((object)h).GetType()).OrderBy(o => o.FullName).ToArray();
            var subset = allTypes.Where((t, ix) => ix % 2 == 1).ToArray();

            // specific
            {
                Helper.ForEachEnumerableNoRetExpression(
                    new[] { "foo", "fizz", "hello" },
                    @"a => 
                    Helper.ForEachEnumerableExpression(
                        a,
                        new[] { ""bar"", ""world"" },
                        res =>
                        {
                            Assert.AreEqual(2, res.Count);
                            Assert.AreEqual(""foo"", res[0]);
                            Assert.AreEqual(""hello"", res[1]);
                        },
                        ""(a, b) => a.Intersect(b, new IntersectTests._Comparer())"",
                        typeof(EmptyEnumerable<>),
                        typeof(EmptyOrderedEnumerable<>),
                        typeof(LookupDefaultEnumerable<,>),
                        typeof(LookupSpecificEnumerable<,>),
                        typeof(GroupByDefaultEnumerable<,,,,>),
                        typeof(GroupBySpecificEnumerable<,,,,>)
                    )",
                    subset.Concat(
                        new[]
                        {
                            typeof(EmptyEnumerable<>),
                            typeof(EmptyOrderedEnumerable<>),
                            typeof(GroupByDefaultEnumerable<,,,,>),
                            typeof(GroupBySpecificEnumerable<,,,,>),
                            typeof(LookupDefaultEnumerable<,>),
                            typeof(LookupSpecificEnumerable<,>)
                        }
                    )
                    .ToArray()
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
                Assert.IsFalse(empty.Intersect(empty).Any());
                Assert.IsFalse(empty.Intersect(empty, new _IntComparer()).Any());
                Assert.IsFalse(empty.Intersect(emptyOrdered).Any());
                Assert.IsFalse(empty.Intersect(emptyOrdered, new _IntComparer()).Any());
                Assert.IsFalse(empty.Intersect(range).Any());
                Assert.IsFalse(empty.Intersect(range, new _IntComparer()).Any());
                Assert.IsFalse(empty.Intersect(Enumerable.Repeat(1, 1)).Any());
                Assert.IsFalse(empty.Intersect(Enumerable.Repeat(1, 1), new _IntComparer()).Any());
                Assert.IsFalse(empty.Intersect(reverseRange).Any());
                Assert.IsFalse(empty.Intersect(reverseRange, new _IntComparer()).Any());
                Assert.IsFalse(empty.Intersect(oneItemDefault).Any());
                Assert.IsFalse(empty.Intersect(oneItemDefault, new _IntComparer()).Any());
                Assert.IsFalse(empty.Intersect(oneItemSpecific).Any());
                Assert.IsFalse(empty.Intersect(oneItemSpecific, new _IntComparer()).Any());
                Assert.IsFalse(empty.Intersect(oneItemDefaultOrdered).Any());
                Assert.IsFalse(empty.Intersect(oneItemDefaultOrdered, new _IntComparer()).Any());
                Assert.IsFalse(empty.Intersect(oneItemSpecificOrdered).Any());
                Assert.IsFalse(empty.Intersect(oneItemSpecificOrdered, new _IntComparer()).Any());

                Helper.ForEachEnumerableExpression(
                    empty,
                    new[] { 1 },
                    res => { },
                    @"(a, b) =>
                      {
                        Assert.IsFalse(a.Intersect(b).Any());
                        Assert.IsFalse(a.Intersect(b, new IntersectTests._IntComparer()).Any());

                        return Helper.NoCallValue;
                      }",
                    typeof(EmptyEnumerable<>),
                    typeof(EmptyOrderedEnumerable<>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );
            }

            // emptyOrdered
            {
                Assert.IsFalse(emptyOrdered.Intersect(empty).Any());
                Assert.IsFalse(emptyOrdered.Intersect(empty, new _IntComparer()).Any());
                Assert.IsFalse(emptyOrdered.Intersect(emptyOrdered).Any());
                Assert.IsFalse(emptyOrdered.Intersect(emptyOrdered, new _IntComparer()).Any());
                Assert.IsFalse(emptyOrdered.Intersect(range).Any());
                Assert.IsFalse(emptyOrdered.Intersect(range, new _IntComparer()).Any());
                Assert.IsFalse(emptyOrdered.Intersect(Enumerable.Repeat(1, 1)).Any());
                Assert.IsFalse(emptyOrdered.Intersect(Enumerable.Repeat(1, 1), new _IntComparer()).Any());
                Assert.IsFalse(emptyOrdered.Intersect(reverseRange).Any());
                Assert.IsFalse(emptyOrdered.Intersect(reverseRange, new _IntComparer()).Any());
                Assert.IsFalse(emptyOrdered.Intersect(oneItemDefault).Any());
                Assert.IsFalse(emptyOrdered.Intersect(oneItemDefault, new _IntComparer()).Any());
                Assert.IsFalse(emptyOrdered.Intersect(oneItemSpecific).Any());
                Assert.IsFalse(emptyOrdered.Intersect(oneItemSpecific, new _IntComparer()).Any());
                Assert.IsFalse(emptyOrdered.Intersect(oneItemDefaultOrdered).Any());
                Assert.IsFalse(emptyOrdered.Intersect(oneItemDefaultOrdered, new _IntComparer()).Any());
                Assert.IsFalse(emptyOrdered.Intersect(oneItemSpecificOrdered).Any());
                Assert.IsFalse(emptyOrdered.Intersect(oneItemSpecificOrdered, new _IntComparer()).Any());

                Helper.ForEachEnumerableExpression(
                    emptyOrdered,
                    new[] { 1 },
                    res => { },
                    @"(a, b) =>
                      {
                        Assert.IsFalse(a.Intersect(b).Any());
                        Assert.IsFalse(a.Intersect(b, new IntersectTests._IntComparer()).Any());

                        return Helper.NoCallValue;
                      }",
                    typeof(EmptyEnumerable<>),
                    typeof(EmptyOrderedEnumerable<>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );
            }

            // groupByDefault
            {
                var dict = new Dictionary<GroupingEnumerable<int, int>, object>();
                dict.Add(groupByDefault.First(), new object());
                Assert.IsFalse(groupByDefault.Intersect(dict.Keys).Any());
                Assert.IsTrue(groupByDefault.Intersect(dict.Keys, new _GroupingComparer<int>()).SequenceEqual(new[] { groupByDefault.First() }, new _GroupingComparer<int>()));
                var sortedDict = new SortedDictionary<GroupingEnumerable<int, int>, object>();
                sortedDict.Add(groupByDefault.First(), new object());
                var sortedSet = new SortedSet<GroupingEnumerable<int, int>>();
                sortedSet.Add(groupByDefault.First());
                Assert.IsFalse(groupByDefault.Intersect(sortedSet).Any());
                Assert.IsTrue(groupByDefault.Intersect(sortedSet, new _GroupingComparer<int>()).SequenceEqual(new[] { groupByDefault.First() }, new _GroupingComparer<int>()));
                Assert.IsFalse(groupByDefault.Intersect(sortedDict.Keys).Any());
                Assert.IsTrue(groupByDefault.Intersect(sortedDict.Keys, new _GroupingComparer<int>()).SequenceEqual(new[] { groupByDefault.First() }, new _GroupingComparer<int>()));
                Assert.IsFalse(groupByDefault.Intersect(Enumerable.Empty<GroupingEnumerable<int, int>>()).Any());
                Assert.IsFalse(groupByDefault.Intersect(Enumerable.Empty<GroupingEnumerable<int, int>>(), new _GroupingComparer<int>()).Any());
                Assert.IsFalse(groupByDefault.Intersect(Enumerable.Empty<GroupingEnumerable<int, int>>().OrderBy(x => x)).Any());
                Assert.IsFalse(groupByDefault.Intersect(Enumerable.Empty<GroupingEnumerable<int, int>>().OrderBy(x => x), new _GroupingComparer<int>()).Any());
                Assert.IsFalse(groupByDefault.Intersect(groupByDefault).Any());
                Assert.IsTrue(groupByDefault.Intersect(groupByDefault, new _GroupingComparer<int>()).SequenceEqual(groupByDefault, new _GroupingComparer<int>()));
                Assert.IsFalse(groupByDefault.Intersect(new[] { 1, 1 }.GroupBy(x => x, new _IntComparer())).Any());
                Assert.IsTrue(groupByDefault.Intersect(new[] { 1, 1 }.GroupBy(x => x, new _IntComparer()), new _GroupingComparer<int>()).SequenceEqual(new[] { groupByDefault.First() }, new _GroupingComparer<int>()));
                // note: intersectDefault cannot really be done
                var intersectSpecific = new[] { groupByDefault.First() }.Intersect(new[] { groupByDefault.First() }, new _GroupingComparer<int>());
                Assert.IsFalse(groupByDefault.Intersect(intersectSpecific).Any());
                Assert.IsTrue(groupByDefault.Intersect(intersectSpecific, new _GroupingComparer<int>()).SequenceEqual(intersectSpecific, new _GroupingComparer<int>()));
                Assert.IsFalse(groupByDefault.Intersect(lookup).Any());
                Assert.IsTrue(groupByDefault.Intersect(lookup, new _GroupingComparer<int>()).SequenceEqual(groupByDefault, new _GroupingComparer<int>()));
                var orderBy = new[] { groupByDefault.First() }.OrderBy(o => o.Key);
                Assert.IsFalse(groupByDefault.Intersect(orderBy).Any());
                Assert.IsTrue(groupByDefault.Intersect(orderBy, new _GroupingComparer<int>()).SequenceEqual(orderBy, new _GroupingComparer<int>()));
                var skipWhile = new[] { groupByDefault.First() }.SkipWhile(x => false);
                Assert.IsFalse(groupByDefault.Intersect(skipWhile).Any());
                Assert.IsTrue(groupByDefault.Intersect(skipWhile, new _GroupingComparer<int>()).SequenceEqual(skipWhile, new _GroupingComparer<int>()));
                var takeWhile = new[] { groupByDefault.First() }.TakeWhile(x => true);
                Assert.IsFalse(groupByDefault.Intersect(takeWhile).Any());
                Assert.IsTrue(groupByDefault.Intersect(takeWhile, new _GroupingComparer<int>()).SequenceEqual(takeWhile, new _GroupingComparer<int>()));
                var where = new[] { groupByDefault.First() }.Where(x => true);
                Assert.IsFalse(groupByDefault.Intersect(where).Any());
                Assert.IsTrue(groupByDefault.Intersect(where, new _GroupingComparer<int>()).SequenceEqual(where, new _GroupingComparer<int>()));
                var whereWhere = new[] { groupByDefault.First() }.Where(x => true).Where(x => true);
                Assert.IsFalse(groupByDefault.Intersect(whereWhere).Any());
                Assert.IsTrue(groupByDefault.Intersect(whereWhere, new _GroupingComparer<int>()).SequenceEqual(whereWhere, new _GroupingComparer<int>()));

                var oneItemSpecificGroupBy = Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty(groupByDefault.First());
                var oneItemSpecificOrderedGroupBy = oneItemSpecificGroupBy.OrderBy(x => x);
                // default can't really be done
                Assert.IsFalse(groupByDefault.Intersect(oneItemSpecificGroupBy).Any());
                Assert.IsTrue(groupByDefault.Intersect(oneItemSpecificGroupBy, new _GroupingComparer<int>()).SequenceEqual(oneItemSpecificGroupBy, new _GroupingComparer<int>()));
                // default can't really be done
                Assert.IsFalse(groupByDefault.Intersect(oneItemSpecificOrderedGroupBy).Any());
                Assert.IsTrue(groupByDefault.Intersect(oneItemSpecificOrderedGroupBy, new _GroupingComparer<int>()).SequenceEqual(oneItemSpecificOrderedGroupBy, new _GroupingComparer<int>()));

                Helper.ForEachEnumerableExpression(
                    groupByDefault,
                    groupByDefault.ToArray(),
                    res => { },
                    @"(a, b) =>
                      {
                        Assert.IsFalse(a.Intersect(b).Any());
                        Assert.IsTrue(a.Intersect(b, new IntersectTests._GroupingComparer<int>()).SequenceEqual(a, new IntersectTests._GroupingComparer<int>()));

                        return Helper.NoCallValue;
                      }",
                    typeof(Dictionary<,>.KeyCollection),
                    typeof(SortedDictionary<,>.KeyCollection),
                    typeof(SortedSet<>),
                    typeof(EmptyEnumerable<>),
                    typeof(EmptyOrderedEnumerable<>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(IntersectDefaultEnumerable<,,,,>),
                    typeof(IntersectSpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>),
                    typeof(OrderByEnumerable<,,,,>),
                    typeof(SkipWhileEnumerable<,,>),    // the instance provided by Helper isn't super condusive to GroupingEnumerable<>
                    typeof(TakeWhileEnumerable<,,>),    // the instance provided by Helper isn't super condusive to GroupingEnumerable<>
                    typeof(WhereEnumerable<,,>),        // the instance provided by Helper isn't super condusive to GroupingEnumerable<>
                    typeof(WhereWhereEnumerable<,,,>)   // the instance provided by Helper isn't super condusive to GroupingEnumerable<>
                );
            }

            // groupBySpecific
            {
                var dict = new Dictionary<GroupingEnumerable<string, string>, object>();
                dict.Add(groupBySpecific.First(), new object());
                Assert.IsFalse(groupBySpecific.Intersect(dict.Keys).Any());
                Assert.IsTrue(groupBySpecific.Intersect(dict.Keys, new _GroupingComparer<string>()).SequenceEqual(new[] { groupBySpecific.First() }, new _GroupingComparer<string>()));
                var sortedDict = new SortedDictionary<GroupingEnumerable<string, string>, object>();
                sortedDict.Add(groupBySpecific.First(), new object());
                var sortedSet = new SortedSet<GroupingEnumerable<string, string>>();
                sortedSet.Add(groupBySpecific.First());
                Assert.IsFalse(groupBySpecific.Intersect(sortedSet).Any());
                Assert.IsTrue(groupBySpecific.Intersect(sortedSet, new _GroupingComparer<string>()).SequenceEqual(new[] { groupBySpecific.First() }, new _GroupingComparer<string>()));
                Assert.IsFalse(groupBySpecific.Intersect(sortedDict.Keys).Any());
                Assert.IsTrue(groupBySpecific.Intersect(sortedDict.Keys, new _GroupingComparer<string>()).SequenceEqual(new[] { groupBySpecific.First() }, new _GroupingComparer<string>()));
                Assert.IsFalse(groupBySpecific.Intersect(Enumerable.Empty<GroupingEnumerable<string, string>>()).Any());
                Assert.IsFalse(groupBySpecific.Intersect(Enumerable.Empty<GroupingEnumerable<string, string>>(), new _GroupingComparer<string>()).Any());
                Assert.IsFalse(groupBySpecific.Intersect(Enumerable.Empty<GroupingEnumerable<string, string>>().OrderBy(x => x)).Any());
                Assert.IsFalse(groupBySpecific.Intersect(Enumerable.Empty<GroupingEnumerable<string, string>>().OrderBy(x => x), new _GroupingComparer<string>()).Any());
                Assert.IsFalse(groupBySpecific.Intersect(groupBySpecific).Any());
                Assert.IsTrue(groupBySpecific.Intersect(groupBySpecific, new _GroupingComparer<string>()).SequenceEqual(groupBySpecific, new _GroupingComparer<string>()));
                Assert.IsFalse(groupBySpecific.Intersect(new[] { "hello", "HELLO" }.GroupBy(x => x, StringComparer.OrdinalIgnoreCase)).Any());
                Assert.IsTrue(groupBySpecific.Intersect(new[] { "hello", "HELLO" }.GroupBy(x => x, StringComparer.OrdinalIgnoreCase), new _GroupingComparer<string>()).SequenceEqual(new[] { groupBySpecific.First() }, new _GroupingComparer<string>()));
                // note: intersectDefault cannot really be done
                var intersectSpecific = new[] { groupBySpecific.First() }.Intersect(new[] { groupBySpecific.First() }, new _GroupingComparer<string>());
                Assert.IsFalse(groupBySpecific.Intersect(intersectSpecific).Any());
                Assert.IsTrue(groupBySpecific.Intersect(intersectSpecific, new _GroupingComparer<string>()).SequenceEqual(intersectSpecific, new _GroupingComparer<string>()));
                var stringLookup = new[] { "hello", "HELLO", "world", "WORLD", "foo", "FOO" }.ToLookup(x => x, StringComparer.OrdinalIgnoreCase);
                Assert.IsFalse(groupBySpecific.Intersect(stringLookup).Any());
                Assert.IsTrue(groupBySpecific.Intersect(stringLookup, new _GroupingComparer<string>()).SequenceEqual(groupBySpecific, new _GroupingComparer<string>()));
                var orderBy = new[] { groupBySpecific.First() }.OrderBy(o => o.Key);
                Assert.IsFalse(groupBySpecific.Intersect(orderBy).Any());
                Assert.IsTrue(groupBySpecific.Intersect(orderBy, new _GroupingComparer<string>()).SequenceEqual(orderBy, new _GroupingComparer<string>()));
                var skipWhile = new[] { groupBySpecific.First() }.SkipWhile(x => false);
                Assert.IsFalse(groupBySpecific.Intersect(skipWhile).Any());
                Assert.IsTrue(groupBySpecific.Intersect(skipWhile, new _GroupingComparer<string>()).SequenceEqual(skipWhile, new _GroupingComparer<string>()));
                var takeWhile = new[] { groupBySpecific.First() }.TakeWhile(x => true);
                Assert.IsFalse(groupBySpecific.Intersect(takeWhile).Any());
                Assert.IsTrue(groupBySpecific.Intersect(takeWhile, new _GroupingComparer<string>()).SequenceEqual(takeWhile, new _GroupingComparer<string>()));
                var where = new[] { groupBySpecific.First() }.Where(x => true);
                Assert.IsFalse(groupBySpecific.Intersect(where).Any());
                Assert.IsTrue(groupBySpecific.Intersect(where, new _GroupingComparer<string>()).SequenceEqual(where, new _GroupingComparer<string>()));
                var whereWhere = new[] { groupBySpecific.First() }.Where(x => true).Where(x => true);
                Assert.IsFalse(groupBySpecific.Intersect(whereWhere).Any());
                Assert.IsTrue(groupBySpecific.Intersect(whereWhere, new _GroupingComparer<string>()).SequenceEqual(whereWhere, new _GroupingComparer<string>()));

                var oneItemSpecificGroupBy = Enumerable.Empty<GroupingEnumerable<string, string>>().DefaultIfEmpty(groupBySpecific.First());
                var oneItemSpecificOrderedGroupBy = oneItemSpecificGroupBy.OrderBy(x => x);
                // default can't really be done
                Assert.IsFalse(groupBySpecific.Intersect(oneItemSpecificGroupBy).Any());
                Assert.IsTrue(groupBySpecific.Intersect(oneItemSpecificGroupBy, new _GroupingComparer<string>()).SequenceEqual(oneItemSpecificGroupBy, new _GroupingComparer<string>()));
                // default can't really be done
                Assert.IsFalse(groupBySpecific.Intersect(oneItemSpecificOrderedGroupBy).Any());
                Assert.IsTrue(groupBySpecific.Intersect(oneItemSpecificOrderedGroupBy, new _GroupingComparer<string>()).SequenceEqual(oneItemSpecificOrderedGroupBy, new _GroupingComparer<string>()));

                Helper.ForEachEnumerableExpression(
                    groupBySpecific,
                    groupBySpecific.ToArray(),
                    res => { },
                    @"(a, b) =>
                      {
                        Assert.IsFalse(a.Intersect(b).Any());
                        Assert.IsTrue(a.Intersect(b, new IntersectTests._GroupingComparer<string>()).SequenceEqual(a, new IntersectTests._GroupingComparer<string>()));

                        return Helper.NoCallValue;
                      }",
                    typeof(Dictionary<,>.KeyCollection),
                    typeof(SortedDictionary<,>.KeyCollection),
                    typeof(SortedSet<>),
                    typeof(EmptyEnumerable<>),
                    typeof(EmptyOrderedEnumerable<>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(IntersectDefaultEnumerable<,,,,>),
                    typeof(IntersectSpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>),
                    typeof(OrderByEnumerable<,,,,>),
                    typeof(SkipWhileEnumerable<,,>),    // the instance provided by Helper isn't super condusive to GroupingEnumerable<>
                    typeof(TakeWhileEnumerable<,,>),    // the instance provided by Helper isn't super condusive to GroupingEnumerable<>
                    typeof(WhereEnumerable<,,>),        // the instance provided by Helper isn't super condusive to GroupingEnumerable<>
                    typeof(WhereWhereEnumerable<,,,>)   // the instance provided by Helper isn't super condusive to GroupingEnumerable<>
                );
            }

            // lookup
            {
                var dict = new Dictionary<GroupingEnumerable<int, int>, object>();
                dict.Add(lookup.First(), new object());
                Assert.IsFalse(lookup.Intersect(dict.Keys).Any());
                Assert.IsTrue(lookup.Intersect(dict.Keys, new _GroupingComparer<int>()).SequenceEqual(new[] { lookup.First() }, new _GroupingComparer<int>()));
                var sortedDict = new SortedDictionary<GroupingEnumerable<int, int>, object>();
                sortedDict.Add(lookup.First(), new object());
                var sortedSet = new SortedSet<GroupingEnumerable<int, int>>();
                sortedSet.Add(lookup.First());
                Assert.IsFalse(lookup.Intersect(sortedSet).Any());
                Assert.IsTrue(lookup.Intersect(sortedSet, new _GroupingComparer<int>()).SequenceEqual(new[] { lookup.First() }, new _GroupingComparer<int>()));
                Assert.IsFalse(lookup.Intersect(sortedDict.Keys).Any());
                Assert.IsTrue(lookup.Intersect(sortedDict.Keys, new _GroupingComparer<int>()).SequenceEqual(new[] { lookup.First() }, new _GroupingComparer<int>()));
                Assert.IsFalse(lookup.Intersect(Enumerable.Empty<GroupingEnumerable<int, int>>()).Any());
                Assert.IsFalse(lookup.Intersect(Enumerable.Empty<GroupingEnumerable<int, int>>(), new _GroupingComparer<int>()).Any());
                Assert.IsFalse(lookup.Intersect(Enumerable.Empty<GroupingEnumerable<int, int>>().OrderBy(x => x)).Any());
                Assert.IsFalse(lookup.Intersect(Enumerable.Empty<GroupingEnumerable<int, int>>().OrderBy(x => x), new _GroupingComparer<int>()).Any());
                Assert.IsFalse(lookup.Intersect(lookup).Any());
                Assert.IsTrue(lookup.Intersect(lookup, new _GroupingComparer<int>()).SequenceEqual(lookup, new _GroupingComparer<int>()));
                Assert.IsFalse(lookup.Intersect(new[] { 1, 1 }.GroupBy(x => x, new _IntComparer())).Any());
                Assert.IsTrue(lookup.Intersect(new[] { 1, 1 }.GroupBy(x => x, new _IntComparer()), new _GroupingComparer<int>()).SequenceEqual(new[] { lookup.First() }, new _GroupingComparer<int>()));
                // note: intersectDefault cannot really be done
                var intersectSpecific = new[] { lookup.First() }.Intersect(new[] { lookup.First() }, new _GroupingComparer<int>());
                Assert.IsFalse(lookup.Intersect(intersectSpecific).Any());
                Assert.IsTrue(lookup.Intersect(intersectSpecific, new _GroupingComparer<int>()).SequenceEqual(intersectSpecific, new _GroupingComparer<int>()));
                Assert.IsFalse(lookup.Intersect(lookup).Any());
                Assert.IsTrue(lookup.Intersect(lookup, new _GroupingComparer<int>()).SequenceEqual(lookup, new _GroupingComparer<int>()));
                var orderBy = new[] { lookup.First() }.OrderBy(o => o.Key);
                Assert.IsFalse(lookup.Intersect(orderBy).Any());
                Assert.IsTrue(lookup.Intersect(orderBy, new _GroupingComparer<int>()).SequenceEqual(orderBy, new _GroupingComparer<int>()));
                var skipWhile = new[] { lookup.First() }.SkipWhile(x => false);
                Assert.IsFalse(lookup.Intersect(skipWhile).Any());
                Assert.IsTrue(lookup.Intersect(skipWhile, new _GroupingComparer<int>()).SequenceEqual(skipWhile, new _GroupingComparer<int>()));
                var takeWhile = new[] { lookup.First() }.TakeWhile(x => true);
                Assert.IsFalse(lookup.Intersect(takeWhile).Any());
                Assert.IsTrue(lookup.Intersect(takeWhile, new _GroupingComparer<int>()).SequenceEqual(takeWhile, new _GroupingComparer<int>()));
                var where = new[] { lookup.First() }.Where(x => true);
                Assert.IsFalse(lookup.Intersect(where).Any());
                Assert.IsTrue(lookup.Intersect(where, new _GroupingComparer<int>()).SequenceEqual(where, new _GroupingComparer<int>()));
                var whereWhere = new[] { lookup.First() }.Where(x => true).Where(x => true);
                Assert.IsFalse(lookup.Intersect(whereWhere).Any());
                Assert.IsTrue(lookup.Intersect(whereWhere, new _GroupingComparer<int>()).SequenceEqual(whereWhere, new _GroupingComparer<int>()));

                var oneItemSpecificGroupBy = Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty(lookup.First());
                var oneItemSpecificOrderedGroupBy = oneItemSpecificGroupBy.OrderBy(x => x);
                // default can't really be done
                Assert.IsFalse(lookup.Intersect(oneItemSpecificGroupBy).Any());
                Assert.IsTrue(lookup.Intersect(oneItemSpecificGroupBy, new _GroupingComparer<int>()).SequenceEqual(oneItemSpecificGroupBy, new _GroupingComparer<int>()));
                // default can't really be done
                Assert.IsFalse(lookup.Intersect(oneItemSpecificOrderedGroupBy).Any());
                Assert.IsTrue(lookup.Intersect(oneItemSpecificOrderedGroupBy, new _GroupingComparer<int>()).SequenceEqual(oneItemSpecificOrderedGroupBy, new _GroupingComparer<int>()));

                Helper.ForEachEnumerableExpression(
                    lookup,
                    lookup.ToArray(),
                    res => { },
                    @"(a, b) =>
                      {
                        Assert.IsFalse(a.Intersect(b).Any());
                        Assert.IsTrue(a.Intersect(b, new IntersectTests._GroupingComparer<int>()).SequenceEqual(a, new IntersectTests._GroupingComparer<int>()));

                        return Helper.NoCallValue;
                      }",
                    typeof(Dictionary<,>.KeyCollection),
                    typeof(SortedDictionary<,>.KeyCollection),
                    typeof(SortedSet<>),
                    typeof(EmptyEnumerable<>),
                    typeof(EmptyOrderedEnumerable<>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(IntersectDefaultEnumerable<,,,,>),
                    typeof(IntersectSpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>),
                    typeof(OrderByEnumerable<,,,,>),
                    typeof(SkipWhileEnumerable<,,>),    // the instance provided by Helper isn't super condusive to GroupingEnumerable<>
                    typeof(TakeWhileEnumerable<,,>),    // the instance provided by Helper isn't super condusive to GroupingEnumerable<>
                    typeof(WhereEnumerable<,,>),        // the instance provided by Helper isn't super condusive to GroupingEnumerable<>
                    typeof(WhereWhereEnumerable<,,,>)   // the instance provided by Helper isn't super condusive to GroupingEnumerable<>
                );
            }

            // range
            {
                Assert.IsFalse(range.Intersect(empty).Any());
                Assert.IsFalse(range.Intersect(empty, new _IntComparer()).Any());
                Assert.IsFalse(range.Intersect(emptyOrdered).Any());
                Assert.IsFalse(range.Intersect(emptyOrdered, new _IntComparer()).Any());
                Assert.IsTrue(range.Intersect(range).SequenceEqual(range));
                Assert.IsTrue(range.Intersect(range, new _IntComparer()).SequenceEqual(range));
                Assert.IsTrue(range.Intersect(Enumerable.Repeat(1, 5)).SequenceEqual(new[] { 1 }));
                Assert.IsTrue(range.Intersect(Enumerable.Repeat(1, 5), new _IntComparer()).SequenceEqual(new[] { 1 }));
                Assert.IsTrue(range.Intersect(reverseRange).SequenceEqual(range));
                Assert.IsTrue(range.Intersect(reverseRange, new _IntComparer()).SequenceEqual(range));
                Assert.IsTrue(range.Intersect(oneItemDefault).SequenceEqual(new int[0]));
                Assert.IsTrue(range.Intersect(oneItemDefault, new _IntComparer()).SequenceEqual(new int[0]));
                Assert.IsTrue(range.Intersect(oneItemSpecific).SequenceEqual(new[] { 4 }));
                Assert.IsTrue(range.Intersect(oneItemSpecific, new _IntComparer()).SequenceEqual(new[] { 4 }));
                Assert.IsTrue(range.Intersect(oneItemDefaultOrdered).SequenceEqual(new int[0]));
                Assert.IsTrue(range.Intersect(oneItemDefaultOrdered, new _IntComparer()).SequenceEqual(new int[0]));
                Assert.IsTrue(range.Intersect(oneItemSpecificOrdered).SequenceEqual(new[] { 4 }));
                Assert.IsTrue(range.Intersect(oneItemSpecificOrdered, new _IntComparer()).SequenceEqual(new[] { 4 }));

                Helper.ForEachEnumerableExpression(
                    range,
                    range.ToArray(),
                    res => { },
                    @"(a, b) =>
                      {
                        Assert.IsTrue(a.Intersect(b).SequenceEqual(a));
                        Assert.IsTrue(a.Intersect(b, new IntersectTests._IntComparer()).SequenceEqual(a));

                        return Helper.NoCallValue;
                      }",
                    typeof(EmptyEnumerable<>),
                    typeof(EmptyOrderedEnumerable<>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );
            }

            // repeat
            {
                Assert.IsFalse(repeat.Intersect(Enumerable.Empty<string>()).Any());
                Assert.IsFalse(repeat.Intersect(Enumerable.Empty<string>(), StringComparer.InvariantCultureIgnoreCase).Any());
                Assert.IsFalse(repeat.Intersect(Enumerable.Empty<string>().OrderBy(x => x)).Any());
                Assert.IsFalse(repeat.Intersect(Enumerable.Empty<string>().OrderBy(x => x), StringComparer.InvariantCultureIgnoreCase).Any());
                Assert.IsTrue(repeat.Intersect(repeat).SequenceEqual(new[] { "foo" }));
                Assert.IsTrue(repeat.Intersect(repeat, StringComparer.InvariantCultureIgnoreCase).SequenceEqual(new[] { "foo" }));

                var oneItemDefaultString = Enumerable.Empty<string>().DefaultIfEmpty();
                var oneItemSpecificString = Enumerable.Empty<string>().DefaultIfEmpty("bar");
                var oneItemDefaultOrderedString = oneItemDefaultString.OrderBy(x => x);
                var oneItemSpecificOrderedString = oneItemSpecificString.OrderBy(x => x);

                Assert.IsTrue(repeat.Intersect(oneItemDefaultString).SequenceEqual(new string[0]));
                Assert.IsTrue(repeat.Intersect(oneItemDefaultString, StringComparer.InvariantCultureIgnoreCase).SequenceEqual(new string[0]));
                Assert.IsTrue(repeat.Intersect(oneItemSpecificString).SequenceEqual(new string[0]));
                Assert.IsTrue(repeat.Intersect(oneItemSpecificString, StringComparer.InvariantCultureIgnoreCase).SequenceEqual(new string[0]));
                Assert.IsTrue(repeat.Intersect(oneItemDefaultOrderedString).SequenceEqual(new string[0]));
                Assert.IsTrue(repeat.Intersect(oneItemDefaultOrderedString, StringComparer.InvariantCultureIgnoreCase).SequenceEqual(new string[0]));
                Assert.IsTrue(repeat.Intersect(oneItemSpecificOrderedString).SequenceEqual(new string[0]));
                Assert.IsTrue(repeat.Intersect(oneItemSpecificOrderedString, StringComparer.InvariantCultureIgnoreCase).SequenceEqual(new string[0]));

                Helper.ForEachEnumerableExpression(
                    repeat,
                    repeat.ToArray(),
                    res => { },
                    @"(a, b) =>
                      {
                        Assert.IsTrue(a.Intersect(b).SequenceEqual(new[] { ""foo"" }));
                        Assert.IsTrue(a.Intersect(b, StringComparer.InvariantCultureIgnoreCase).SequenceEqual(new[] { ""foo"" }));

                        return Helper.NoCallValue;
                      }",
                    typeof(EmptyEnumerable<>),
                    typeof(EmptyOrderedEnumerable<>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );
            }

            // reverseRange
            {
                Assert.IsFalse(reverseRange.Intersect(empty).Any());
                Assert.IsFalse(reverseRange.Intersect(empty, new _IntComparer()).Any());
                Assert.IsFalse(reverseRange.Intersect(emptyOrdered).Any());
                Assert.IsFalse(reverseRange.Intersect(emptyOrdered, new _IntComparer()).Any());
                Assert.IsTrue(reverseRange.Intersect(range).SequenceEqual(reverseRange));
                Assert.IsTrue(reverseRange.Intersect(range, new _IntComparer()).SequenceEqual(reverseRange));
                Assert.IsTrue(reverseRange.Intersect(Enumerable.Repeat(1, 5)).SequenceEqual(new[] { 1 }));
                Assert.IsTrue(reverseRange.Intersect(Enumerable.Repeat(1, 5), new _IntComparer()).SequenceEqual(new[] { 1 }));
                Assert.IsTrue(reverseRange.Intersect(reverseRange).SequenceEqual(reverseRange));
                Assert.IsTrue(reverseRange.Intersect(reverseRange, new _IntComparer()).SequenceEqual(reverseRange));

                Assert.IsTrue(reverseRange.Intersect(oneItemDefault).SequenceEqual(new int[0]));
                Assert.IsTrue(reverseRange.Intersect(oneItemDefault, new _IntComparer()).SequenceEqual(new int[0]));
                Assert.IsTrue(reverseRange.Intersect(oneItemSpecific).SequenceEqual(new[] { 4 }));
                Assert.IsTrue(reverseRange.Intersect(oneItemSpecific, new _IntComparer()).SequenceEqual(new[] { 4 }));
                Assert.IsTrue(reverseRange.Intersect(oneItemDefaultOrdered).SequenceEqual(new int[0]));
                Assert.IsTrue(reverseRange.Intersect(oneItemDefaultOrdered, new _IntComparer()).SequenceEqual(new int[0]));
                Assert.IsTrue(reverseRange.Intersect(oneItemSpecificOrdered).SequenceEqual(new[] { 4 }));
                Assert.IsTrue(reverseRange.Intersect(oneItemSpecificOrdered, new _IntComparer()).SequenceEqual(new[] { 4 }));

                Helper.ForEachEnumerableExpression(
                    reverseRange,
                    reverseRange.ToArray(),
                    res => { },
                    @"(a, b) =>
                      {
                        Assert.IsTrue(a.Intersect(b).SequenceEqual(a));
                        Assert.IsTrue(a.Intersect(b, new IntersectTests._IntComparer()).SequenceEqual(a));

                        return Helper.NoCallValue;
                      }",
                    typeof(EmptyEnumerable<>),
                    typeof(EmptyOrderedEnumerable<>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );
            }

            // oneItemDefault
            {
                Assert.IsFalse(oneItemDefault.Intersect(empty).Any());
                Assert.IsFalse(oneItemDefault.Intersect(empty, new _IntComparer()).Any());
                Assert.IsFalse(oneItemDefault.Intersect(emptyOrdered).Any());
                Assert.IsFalse(oneItemDefault.Intersect(emptyOrdered, new _IntComparer()).Any());
                Assert.IsTrue(oneItemDefault.Intersect(range).SequenceEqual(new int[0]));
                Assert.IsTrue(oneItemDefault.Intersect(range, new _IntComparer()).SequenceEqual(new int[0]));
                Assert.IsTrue(oneItemDefault.Intersect(Enumerable.Repeat(1, 5)).SequenceEqual(new int[0]));
                Assert.IsTrue(oneItemDefault.Intersect(Enumerable.Repeat(1, 5), new _IntComparer()).SequenceEqual(new int[0]));
                Assert.IsTrue(oneItemDefault.Intersect(reverseRange).SequenceEqual(new int[0]));
                Assert.IsTrue(oneItemDefault.Intersect(reverseRange, new _IntComparer()).SequenceEqual(new int[0]));

                Assert.IsTrue(oneItemDefault.Intersect(oneItemDefault).SequenceEqual(oneItemDefault));
                Assert.IsTrue(oneItemDefault.Intersect(oneItemDefault, new _IntComparer()).SequenceEqual(oneItemDefault));
                Assert.IsTrue(oneItemDefault.Intersect(oneItemSpecific).SequenceEqual(new int[0]));
                Assert.IsTrue(oneItemDefault.Intersect(oneItemSpecific, new _IntComparer()).SequenceEqual(new int[0]));
                Assert.IsTrue(oneItemDefault.Intersect(oneItemDefaultOrdered).SequenceEqual(oneItemDefault));
                Assert.IsTrue(oneItemDefault.Intersect(oneItemDefaultOrdered, new _IntComparer()).SequenceEqual(oneItemDefault));
                Assert.IsTrue(oneItemDefault.Intersect(oneItemSpecificOrdered).SequenceEqual(new int[0]));
                Assert.IsTrue(oneItemDefault.Intersect(oneItemSpecificOrdered, new _IntComparer()).SequenceEqual(new int[0]));

                Helper.ForEachEnumerableExpression(
                    oneItemDefault,
                    new[] { 1 },
                    res => { },
                    @"(a, b) =>
                      {
                        Assert.IsTrue(a.Intersect(b).SequenceEqual(new int[0]));
                        Assert.IsTrue(a.Intersect(b, new IntersectTests._IntComparer()).SequenceEqual(new int[0]));

                        return Helper.NoCallValue;
                      }",
                    typeof(EmptyEnumerable<>),
                    typeof(EmptyOrderedEnumerable<>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );
            }

            // oneItemSpecific
            {
                Assert.IsFalse(oneItemSpecific.Intersect(empty).Any());
                Assert.IsFalse(oneItemSpecific.Intersect(empty, new _IntComparer()).Any());
                Assert.IsFalse(oneItemSpecific.Intersect(emptyOrdered).Any());
                Assert.IsFalse(oneItemSpecific.Intersect(emptyOrdered, new _IntComparer()).Any());
                Assert.IsTrue(oneItemSpecific.Intersect(range).SequenceEqual(new[] { 4 }));
                Assert.IsTrue(oneItemSpecific.Intersect(range, new _IntComparer()).SequenceEqual(new[] { 4 }));
                Assert.IsTrue(oneItemSpecific.Intersect(Enumerable.Repeat(1, 5)).SequenceEqual(new int[0]));
                Assert.IsTrue(oneItemSpecific.Intersect(Enumerable.Repeat(1, 5), new _IntComparer()).SequenceEqual(new int[0]));
                Assert.IsTrue(oneItemSpecific.Intersect(reverseRange).SequenceEqual(new[] { 4 }));
                Assert.IsTrue(oneItemSpecific.Intersect(reverseRange, new _IntComparer()).SequenceEqual(new[] { 4 }));

                Assert.IsTrue(oneItemSpecific.Intersect(oneItemDefault).SequenceEqual(new int[0]));
                Assert.IsTrue(oneItemSpecific.Intersect(oneItemDefault, new _IntComparer()).SequenceEqual(new int[0]));
                Assert.IsTrue(oneItemSpecific.Intersect(oneItemSpecific).SequenceEqual(oneItemSpecific));
                Assert.IsTrue(oneItemSpecific.Intersect(oneItemSpecific, new _IntComparer()).SequenceEqual(oneItemSpecific));
                Assert.IsTrue(oneItemSpecific.Intersect(oneItemDefaultOrdered).SequenceEqual(new int[0]));
                Assert.IsTrue(oneItemSpecific.Intersect(oneItemDefaultOrdered, new _IntComparer()).SequenceEqual(new int[0]));
                Assert.IsTrue(oneItemSpecific.Intersect(oneItemSpecificOrdered).SequenceEqual(oneItemSpecific));
                Assert.IsTrue(oneItemSpecific.Intersect(oneItemSpecificOrdered, new _IntComparer()).SequenceEqual(oneItemSpecific));

                Helper.ForEachEnumerableExpression(
                    oneItemSpecific,
                    new[] { 1 },
                    res => { },
                    @"(a, b) =>
                      {
                        Assert.IsTrue(a.Intersect(b).SequenceEqual(new int[0]));
                        Assert.IsTrue(a.Intersect(b, new IntersectTests._IntComparer()).SequenceEqual(new int[0]));

                        return Helper.NoCallValue;
                      }",
                    typeof(EmptyEnumerable<>),
                    typeof(EmptyOrderedEnumerable<>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );
            }

            // oneItemDefaultOrdered
            {
                Assert.IsFalse(oneItemDefaultOrdered.Intersect(empty).Any());
                Assert.IsFalse(oneItemDefaultOrdered.Intersect(empty, new _IntComparer()).Any());
                Assert.IsFalse(oneItemDefaultOrdered.Intersect(emptyOrdered).Any());
                Assert.IsFalse(oneItemDefaultOrdered.Intersect(emptyOrdered, new _IntComparer()).Any());
                Assert.IsTrue(oneItemDefaultOrdered.Intersect(range).SequenceEqual(new int[0]));
                Assert.IsTrue(oneItemDefaultOrdered.Intersect(range, new _IntComparer()).SequenceEqual(new int[0]));
                Assert.IsTrue(oneItemDefaultOrdered.Intersect(Enumerable.Repeat(1, 5)).SequenceEqual(new int[0]));
                Assert.IsTrue(oneItemDefaultOrdered.Intersect(Enumerable.Repeat(1, 5), new _IntComparer()).SequenceEqual(new int[0]));
                Assert.IsTrue(oneItemDefaultOrdered.Intersect(reverseRange).SequenceEqual(new int[0]));
                Assert.IsTrue(oneItemDefaultOrdered.Intersect(reverseRange, new _IntComparer()).SequenceEqual(new int[0]));

                Assert.IsTrue(oneItemDefaultOrdered.Intersect(oneItemDefault).SequenceEqual(oneItemDefault));
                Assert.IsTrue(oneItemDefaultOrdered.Intersect(oneItemDefault, new _IntComparer()).SequenceEqual(oneItemDefault));
                Assert.IsTrue(oneItemDefaultOrdered.Intersect(oneItemSpecific).SequenceEqual(new int[0]));
                Assert.IsTrue(oneItemDefaultOrdered.Intersect(oneItemSpecific, new _IntComparer()).SequenceEqual(new int[0]));
                Assert.IsTrue(oneItemDefaultOrdered.Intersect(oneItemDefaultOrdered).SequenceEqual(oneItemDefault));
                Assert.IsTrue(oneItemDefaultOrdered.Intersect(oneItemDefaultOrdered, new _IntComparer()).SequenceEqual(oneItemDefault));
                Assert.IsTrue(oneItemDefaultOrdered.Intersect(oneItemSpecificOrdered).SequenceEqual(new int[0]));
                Assert.IsTrue(oneItemDefaultOrdered.Intersect(oneItemSpecificOrdered, new _IntComparer()).SequenceEqual(new int[0]));

                Helper.ForEachEnumerableExpression(
                    oneItemDefaultOrdered,
                    new[] { 1 },
                    res => { },
                    @"(a, b) =>
                      {
                        Assert.IsTrue(a.Intersect(b).SequenceEqual(new int[0]));
                        Assert.IsTrue(a.Intersect(b, new IntersectTests._IntComparer()).SequenceEqual(new int[0]));

                        return Helper.NoCallValue;
                      }",
                    typeof(EmptyEnumerable<>),
                    typeof(EmptyOrderedEnumerable<>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );
            }

            // oneItemSpecificOrdered
            {
                Assert.IsFalse(oneItemSpecificOrdered.Intersect(empty).Any());
                Assert.IsFalse(oneItemSpecificOrdered.Intersect(empty, new _IntComparer()).Any());
                Assert.IsFalse(oneItemSpecificOrdered.Intersect(emptyOrdered).Any());
                Assert.IsFalse(oneItemSpecificOrdered.Intersect(emptyOrdered, new _IntComparer()).Any());
                Assert.IsTrue(oneItemSpecificOrdered.Intersect(range).SequenceEqual(new[] { 4 }));
                Assert.IsTrue(oneItemSpecificOrdered.Intersect(range, new _IntComparer()).SequenceEqual(new[] { 4 }));
                Assert.IsTrue(oneItemSpecificOrdered.Intersect(Enumerable.Repeat(1, 5)).SequenceEqual(new int[0]));
                Assert.IsTrue(oneItemSpecificOrdered.Intersect(Enumerable.Repeat(1, 5), new _IntComparer()).SequenceEqual(new int[0]));
                Assert.IsTrue(oneItemSpecificOrdered.Intersect(reverseRange).SequenceEqual(new[] { 4 }));
                Assert.IsTrue(oneItemSpecificOrdered.Intersect(reverseRange, new _IntComparer()).SequenceEqual(new[] { 4 }));

                Assert.IsTrue(oneItemSpecificOrdered.Intersect(oneItemDefault).SequenceEqual(new int[0]));
                Assert.IsTrue(oneItemSpecificOrdered.Intersect(oneItemDefault, new _IntComparer()).SequenceEqual(new int[0]));
                Assert.IsTrue(oneItemSpecificOrdered.Intersect(oneItemSpecific).SequenceEqual(oneItemSpecific));
                Assert.IsTrue(oneItemSpecificOrdered.Intersect(oneItemSpecific, new _IntComparer()).SequenceEqual(oneItemSpecific));
                Assert.IsTrue(oneItemSpecificOrdered.Intersect(oneItemDefaultOrdered).SequenceEqual(new int[0]));
                Assert.IsTrue(oneItemSpecificOrdered.Intersect(oneItemDefaultOrdered, new _IntComparer()).SequenceEqual(new int[0]));
                Assert.IsTrue(oneItemSpecificOrdered.Intersect(oneItemSpecificOrdered).SequenceEqual(oneItemSpecific));
                Assert.IsTrue(oneItemSpecificOrdered.Intersect(oneItemSpecificOrdered, new _IntComparer()).SequenceEqual(oneItemSpecific));

                Helper.ForEachEnumerableExpression(
                    oneItemSpecificOrdered,
                    new[] { 1 },
                    res => { },
                    @"(a, b) =>
                      {
                        Assert.IsTrue(a.Intersect(b).SequenceEqual(new int[0]));
                        Assert.IsTrue(a.Intersect(b, new IntersectTests._IntComparer()).SequenceEqual(new int[0]));

                        return Helper.NoCallValue;
                      }",
                    typeof(EmptyEnumerable<>),
                    typeof(EmptyOrderedEnumerable<>),
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
                                a.Intersect(b);
                                Assert.Fail();
                            }
                            catch(ArgumentException exc)
                            {
                                Assert.AreEqual(""""first"""", exc.ParamName);
                            }

                            try
                            {
                                b.Intersect(a);
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
                                a.Intersect(b, StringComparer.InvariantCultureIgnoreCase);
                                Assert.Fail();
                            }
                            catch(ArgumentException exc)
                            {
                                Assert.AreEqual(""""first"""", exc.ParamName);
                            }

                            try
                            {
                                b.Intersect(a, StringComparer.InvariantCultureIgnoreCase);
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
            var range = new RangeEnumerable<int>();
            var repeat = new RepeatEnumerable<int>();
            var reverseRange = new ReverseRangeEnumerable<int>();
            var oneItemDefault = new OneItemDefaultEnumerable<int>();
            var oneItemSpecific = new OneItemSpecificEnumerable<int>();
            var oneItemDefaultOrdered = new OneItemDefaultOrderedEnumerable<int>();
            var oneItemSpecificOrdered = new OneItemSpecificOrderedEnumerable<int>();

            // empty
            {
                try { empty.Intersect(Enumerable.Empty<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { empty.Intersect(Enumerable.Empty<int>(), new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Empty<int>().Intersect(empty); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Empty<int>().Intersect(empty, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { empty.Intersect(Enumerable.Empty<int>().OrderBy(x => x)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { empty.Intersect(Enumerable.Empty<int>().OrderBy(x => x), new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Empty<int>().OrderBy(x => x).Intersect(empty); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Empty<int>().OrderBy(x => x).Intersect(empty, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var badEmptyGrouping = new EmptyEnumerable<GroupingEnumerable<int, int>>();
                try { badEmptyGrouping.Intersect(new[] { 1 }.GroupBy(x => x)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { badEmptyGrouping.Intersect(new[] { 1 }.GroupBy(x => x), new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { new[] { 1 }.GroupBy(x => x).Intersect(badEmptyGrouping); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { new[] { 1 }.GroupBy(x => x).Intersect(badEmptyGrouping, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { badEmptyGrouping.Intersect(new[] { 1 }.GroupBy(x => x, new _IntComparer())); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { badEmptyGrouping.Intersect(new[] { 1 }.GroupBy(x => x, new _IntComparer()), new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { new[] { 1 }.GroupBy(x => x, new _IntComparer()).Intersect(badEmptyGrouping); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { new[] { 1 }.GroupBy(x => x, new _IntComparer()).Intersect(badEmptyGrouping, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { badEmptyGrouping.Intersect(new[] { 1 }.ToLookup(x => x)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { badEmptyGrouping.Intersect(new[] { 1 }.ToLookup(x => x), new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { new[] { 1 }.ToLookup(x => x).Intersect(badEmptyGrouping); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { new[] { 1 }.ToLookup(x => x).Intersect(badEmptyGrouping, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { empty.Intersect(Enumerable.Range(1, 1)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { empty.Intersect(Enumerable.Range(1, 1), new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Range(1, 1).Intersect(empty); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Range(1, 1).Intersect(empty, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { empty.Intersect(Enumerable.Repeat(1, 1)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { empty.Intersect(Enumerable.Repeat(1, 1), new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Repeat(1, 1).Intersect(empty); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Repeat(1, 1).Intersect(empty, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { empty.Intersect(Enumerable.Range(1, 1).Reverse()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { empty.Intersect(Enumerable.Range(1, 1).Reverse(), new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Range(1, 1).Reverse().Intersect(empty); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Range(1, 1).Reverse().Intersect(empty, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { empty.Intersect(Enumerable.Empty<int>().DefaultIfEmpty()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { empty.Intersect(Enumerable.Empty<int>().DefaultIfEmpty(), new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Empty<int>().DefaultIfEmpty().Intersect(empty); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Empty<int>().DefaultIfEmpty().Intersect(empty, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { empty.Intersect(Enumerable.Empty<int>().DefaultIfEmpty(4)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { empty.Intersect(Enumerable.Empty<int>().DefaultIfEmpty(4), new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Empty<int>().DefaultIfEmpty(4).Intersect(empty); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Empty<int>().DefaultIfEmpty(4).Intersect(empty, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { empty.Intersect(Enumerable.Empty<int>().DefaultIfEmpty().OrderBy(x => x)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { empty.Intersect(Enumerable.Empty<int>().DefaultIfEmpty().OrderBy(x => x), new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Empty<int>().DefaultIfEmpty().OrderBy(x => x).Intersect(empty); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Empty<int>().DefaultIfEmpty().OrderBy(x => x).Intersect(empty, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { empty.Intersect(Enumerable.Empty<int>().DefaultIfEmpty(4).OrderBy(x => x)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { empty.Intersect(Enumerable.Empty<int>().DefaultIfEmpty(4).OrderBy(x => x), new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Empty<int>().DefaultIfEmpty(4).OrderBy(x => x).Intersect(empty); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Empty<int>().DefaultIfEmpty(4).OrderBy(x => x).Intersect(empty, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                Helper.ForEachEnumerableExpression(
                    empty,
                    new int[0],
                    res => { },
                    @"(a, b) =>
                      {
                        try { a.Intersect(b); Assert.Fail(); }catch(ArgumentException exc){ Assert.AreEqual(""first"", exc.ParamName); }
                        try { a.Intersect(b, new IntersectTests._IntComparer()); Assert.Fail(); }catch(ArgumentException exc){ Assert.AreEqual(""first"", exc.ParamName); }

                        try { b.Intersect(a); Assert.Fail(); }catch(ArgumentException exc){ Assert.AreEqual(""second"", exc.ParamName); }
                        try { b.Intersect(a, new IntersectTests._IntComparer()); Assert.Fail(); }catch(ArgumentException exc){ Assert.AreEqual(""second"", exc.ParamName); }

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
                try { emptyOrdered.Intersect(Enumerable.Empty<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { emptyOrdered.Intersect(Enumerable.Empty<int>(), new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Empty<int>().Intersect(emptyOrdered); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Empty<int>().Intersect(emptyOrdered, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { emptyOrdered.Intersect(Enumerable.Empty<int>().OrderBy(x => x)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { emptyOrdered.Intersect(Enumerable.Empty<int>().OrderBy(x => x), new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Empty<int>().OrderBy(x => x).Intersect(emptyOrdered); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Empty<int>().OrderBy(x => x).Intersect(emptyOrdered, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var badEmptyOrderedGrouping = new EmptyOrderedEnumerable<GroupingEnumerable<int, int>>();
                try { badEmptyOrderedGrouping.Intersect(new[] { 1 }.GroupBy(x => x)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { badEmptyOrderedGrouping.Intersect(new[] { 1 }.GroupBy(x => x), new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { new[] { 1 }.GroupBy(x => x).Intersect(badEmptyOrderedGrouping); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { new[] { 1 }.GroupBy(x => x).Intersect(badEmptyOrderedGrouping, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { badEmptyOrderedGrouping.Intersect(new[] { 1 }.GroupBy(x => x, new _IntComparer())); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { badEmptyOrderedGrouping.Intersect(new[] { 1 }.GroupBy(x => x, new _IntComparer()), new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { new[] { 1 }.GroupBy(x => x, new _IntComparer()).Intersect(badEmptyOrderedGrouping); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { new[] { 1 }.GroupBy(x => x, new _IntComparer()).Intersect(badEmptyOrderedGrouping, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { badEmptyOrderedGrouping.Intersect(new[] { 1 }.ToLookup(x => x)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { badEmptyOrderedGrouping.Intersect(new[] { 1 }.ToLookup(x => x), new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { new[] { 1 }.ToLookup(x => x).Intersect(badEmptyOrderedGrouping); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { new[] { 1 }.ToLookup(x => x).Intersect(badEmptyOrderedGrouping, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { emptyOrdered.Intersect(Enumerable.Range(1, 1)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { emptyOrdered.Intersect(Enumerable.Range(1, 1), new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Range(1, 1).Intersect(emptyOrdered); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Range(1, 1).Intersect(emptyOrdered, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { emptyOrdered.Intersect(Enumerable.Repeat(1, 1)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { emptyOrdered.Intersect(Enumerable.Repeat(1, 1), new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Repeat(1, 1).Intersect(emptyOrdered); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Repeat(1, 1).Intersect(emptyOrdered, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { emptyOrdered.Intersect(Enumerable.Range(1, 1).Reverse()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { emptyOrdered.Intersect(Enumerable.Range(1, 1).Reverse(), new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Range(1, 1).Reverse().Intersect(emptyOrdered); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Range(1, 1).Reverse().Intersect(emptyOrdered, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { emptyOrdered.Intersect(Enumerable.Empty<int>().DefaultIfEmpty()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { emptyOrdered.Intersect(Enumerable.Empty<int>().DefaultIfEmpty(), new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Empty<int>().DefaultIfEmpty().Intersect(emptyOrdered); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Empty<int>().DefaultIfEmpty().Intersect(emptyOrdered, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { emptyOrdered.Intersect(Enumerable.Empty<int>().DefaultIfEmpty(4)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { emptyOrdered.Intersect(Enumerable.Empty<int>().DefaultIfEmpty(4), new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Empty<int>().DefaultIfEmpty(4).Intersect(emptyOrdered); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Empty<int>().DefaultIfEmpty(4).Intersect(emptyOrdered, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { emptyOrdered.Intersect(Enumerable.Empty<int>().DefaultIfEmpty().OrderBy(x => x)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { emptyOrdered.Intersect(Enumerable.Empty<int>().DefaultIfEmpty().OrderBy(x => x), new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Empty<int>().DefaultIfEmpty().OrderBy(x => x).Intersect(emptyOrdered); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Empty<int>().DefaultIfEmpty().OrderBy(x => x).Intersect(emptyOrdered, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { emptyOrdered.Intersect(Enumerable.Empty<int>().DefaultIfEmpty(4).OrderBy(x => x)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { emptyOrdered.Intersect(Enumerable.Empty<int>().DefaultIfEmpty(4).OrderBy(x => x), new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Empty<int>().DefaultIfEmpty(4).OrderBy(x => x).Intersect(emptyOrdered); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Empty<int>().DefaultIfEmpty(4).OrderBy(x => x).Intersect(emptyOrdered, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                Helper.ForEachEnumerableExpression(
                    emptyOrdered,
                    new int[0],
                    res => { },
                    @"(a, b) =>
                      {
                        try { a.Intersect(b); Assert.Fail(); }catch(ArgumentException exc){ Assert.AreEqual(""first"", exc.ParamName); }
                        try { a.Intersect(b, new IntersectTests._IntComparer()); Assert.Fail(); }catch(ArgumentException exc){ Assert.AreEqual(""first"", exc.ParamName); }

                        try { b.Intersect(a); Assert.Fail(); }catch(ArgumentException exc){ Assert.AreEqual(""second"", exc.ParamName); }
                        try { b.Intersect(a, new IntersectTests._IntComparer()); Assert.Fail(); }catch(ArgumentException exc){ Assert.AreEqual(""second"", exc.ParamName); }

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
                try { groupByDefault.Intersect(Enumerable.Empty<GroupingEnumerable<int, int>>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { groupByDefault.Intersect(Enumerable.Empty<GroupingEnumerable<int, int>>(), new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Empty<GroupingEnumerable<int, int>>().Intersect(groupByDefault); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Empty<GroupingEnumerable<int, int>>().Intersect(groupByDefault, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { groupByDefault.Intersect(Enumerable.Empty<GroupingEnumerable<int, int>>().OrderBy(x => x)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { groupByDefault.Intersect(Enumerable.Empty<GroupingEnumerable<int, int>>().OrderBy(x => x), new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Empty<GroupingEnumerable<int, int>>().OrderBy(x => x).Intersect(groupByDefault); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Empty<GroupingEnumerable<int, int>>().OrderBy(x => x).Intersect(groupByDefault, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { groupByDefault.Intersect(new[] { 1 }.GroupBy(x => x)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { groupByDefault.Intersect(new[] { 1 }.GroupBy(x => x), new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { new[] { 1 }.GroupBy(x => x).Intersect(groupByDefault); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { new[] { 1 }.GroupBy(x => x).Intersect(groupByDefault, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { groupByDefault.Intersect(new[] { 1 }.GroupBy(x => x, new _IntComparer())); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { groupByDefault.Intersect(new[] { 1 }.GroupBy(x => x, new _IntComparer()), new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { new[] { 1 }.GroupBy(x => x, new _IntComparer()).Intersect(groupByDefault); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { new[] { 1 }.GroupBy(x => x, new _IntComparer()).Intersect(groupByDefault, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { groupByDefault.Intersect(new[] { 1 }.ToLookup(x => x)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { groupByDefault.Intersect(new[] { 1 }.ToLookup(x => x), new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { new[] { 1 }.ToLookup(x => x).Intersect(groupByDefault); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { new[] { 1 }.ToLookup(x => x).Intersect(groupByDefault, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                // range isn't sensical
                try { empty.Intersect(Enumerable.Repeat(1, 1)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { empty.Intersect(Enumerable.Repeat(1, 1), new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Repeat(1, 1).Intersect(empty); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Repeat(1, 1).Intersect(empty, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                // reverseRange isn't sensiscal
                var goodGrouping = new[] { 1 }.GroupBy(x => x).First();
                try { groupByDefault.Intersect(Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { groupByDefault.Intersect(Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty(), new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty().Intersect(groupByDefault); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty().Intersect(groupByDefault, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { groupByDefault.Intersect(Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty(goodGrouping)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { groupByDefault.Intersect(Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty(goodGrouping), new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty(goodGrouping).Intersect(groupByDefault); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty(goodGrouping).Intersect(groupByDefault, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { groupByDefault.Intersect(Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty().OrderBy(x => x)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { groupByDefault.Intersect(Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty().OrderBy(x => x), new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty().OrderBy(x => x).Intersect(groupByDefault); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty().OrderBy(x => x).Intersect(groupByDefault, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { groupByDefault.Intersect(Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty(goodGrouping).OrderBy(x => x)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { groupByDefault.Intersect(Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty(goodGrouping).OrderBy(x => x), new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty(goodGrouping).OrderBy(x => x).Intersect(groupByDefault); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty(goodGrouping).OrderBy(x => x).Intersect(groupByDefault, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                Helper.ForEachEnumerableExpression(
                    groupByDefault,
                    new GroupingEnumerable<int, int>[0],
                    res => { },
                    @"(a, b) =>
                      {
                        try { a.Intersect(b); Assert.Fail(); }catch(ArgumentException exc){ Assert.AreEqual(""first"", exc.ParamName); }
                        try { a.Intersect(b, new IntersectTests._GroupingComparer<int>()); Assert.Fail(); }catch(ArgumentException exc){ Assert.AreEqual(""first"", exc.ParamName); }

                        try { b.Intersect(a); Assert.Fail(); }catch(ArgumentException exc){ Assert.AreEqual(""second"", exc.ParamName); }
                        try { b.Intersect(a, new IntersectTests._GroupingComparer<int>()); Assert.Fail(); }catch(ArgumentException exc){ Assert.AreEqual(""second"", exc.ParamName); }

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
                try { groupBySpecific.Intersect(Enumerable.Empty<GroupingEnumerable<int, int>>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { groupBySpecific.Intersect(Enumerable.Empty<GroupingEnumerable<int, int>>(), new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Empty<GroupingEnumerable<int, int>>().Intersect(groupBySpecific); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Empty<GroupingEnumerable<int, int>>().Intersect(groupBySpecific, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { groupBySpecific.Intersect(Enumerable.Empty<GroupingEnumerable<int, int>>().OrderBy(x => x)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { groupBySpecific.Intersect(Enumerable.Empty<GroupingEnumerable<int, int>>().OrderBy(x => x), new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Empty<GroupingEnumerable<int, int>>().OrderBy(x => x).Intersect(groupBySpecific); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Empty<GroupingEnumerable<int, int>>().OrderBy(x => x).Intersect(groupBySpecific, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { groupBySpecific.Intersect(new[] { 1 }.GroupBy(x => x)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { groupBySpecific.Intersect(new[] { 1 }.GroupBy(x => x), new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { new[] { 1 }.GroupBy(x => x).Intersect(groupBySpecific); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { new[] { 1 }.GroupBy(x => x).Intersect(groupBySpecific, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { groupBySpecific.Intersect(new[] { 1 }.GroupBy(x => x, new _IntComparer())); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { groupBySpecific.Intersect(new[] { 1 }.GroupBy(x => x, new _IntComparer()), new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { new[] { 1 }.GroupBy(x => x, new _IntComparer()).Intersect(groupBySpecific); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { new[] { 1 }.GroupBy(x => x, new _IntComparer()).Intersect(groupBySpecific, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { groupBySpecific.Intersect(new[] { 1 }.ToLookup(x => x)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { groupBySpecific.Intersect(new[] { 1 }.ToLookup(x => x), new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { new[] { 1 }.ToLookup(x => x).Intersect(groupBySpecific); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { new[] { 1 }.ToLookup(x => x).Intersect(groupBySpecific, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                // range isn't sensical
                try { empty.Intersect(Enumerable.Repeat(1, 1)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { empty.Intersect(Enumerable.Repeat(1, 1), new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Repeat(1, 1).Intersect(empty); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Repeat(1, 1).Intersect(empty, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                // reverseRange isn't sensiscal
                var goodGrouping = new[] { 1 }.GroupBy(x => x).First();
                try { groupBySpecific.Intersect(Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { groupBySpecific.Intersect(Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty(), new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty().Intersect(groupBySpecific); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty().Intersect(groupBySpecific, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { groupBySpecific.Intersect(Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty(goodGrouping)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { groupBySpecific.Intersect(Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty(goodGrouping), new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty(goodGrouping).Intersect(groupBySpecific); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty(goodGrouping).Intersect(groupBySpecific, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { groupBySpecific.Intersect(Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty().OrderBy(x => x)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { groupBySpecific.Intersect(Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty().OrderBy(x => x), new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty().OrderBy(x => x).Intersect(groupBySpecific); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty().OrderBy(x => x).Intersect(groupBySpecific, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { groupBySpecific.Intersect(Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty(goodGrouping).OrderBy(x => x)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { groupBySpecific.Intersect(Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty(goodGrouping).OrderBy(x => x), new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty(goodGrouping).OrderBy(x => x).Intersect(groupBySpecific); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty(goodGrouping).OrderBy(x => x).Intersect(groupBySpecific, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                Helper.ForEachEnumerableExpression(
                    groupBySpecific,
                    new GroupingEnumerable<int, int>[0],
                    res => { },
                    @"(a, b) =>
                      {
                        try { a.Intersect(b); Assert.Fail(); }catch(ArgumentException exc){ Assert.AreEqual(""first"", exc.ParamName); }
                        try { a.Intersect(b, new IntersectTests._GroupingComparer<int>()); Assert.Fail(); }catch(ArgumentException exc){ Assert.AreEqual(""first"", exc.ParamName); }

                        try { b.Intersect(a); Assert.Fail(); }catch(ArgumentException exc){ Assert.AreEqual(""second"", exc.ParamName); }
                        try { b.Intersect(a, new IntersectTests._GroupingComparer<int>()); Assert.Fail(); }catch(ArgumentException exc){ Assert.AreEqual(""second"", exc.ParamName); }

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
                try { lookup.Intersect(Enumerable.Empty<GroupingEnumerable<int, int>>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { lookup.Intersect(Enumerable.Empty<GroupingEnumerable<int, int>>(), new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Empty<GroupingEnumerable<int, int>>().Intersect(lookup); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Empty<GroupingEnumerable<int, int>>().Intersect(lookup, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { lookup.Intersect(Enumerable.Empty<GroupingEnumerable<int, int>>().OrderBy(x => x)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { lookup.Intersect(Enumerable.Empty<GroupingEnumerable<int, int>>().OrderBy(x => x), new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Empty<GroupingEnumerable<int, int>>().OrderBy(x => x).Intersect(lookup); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Empty<GroupingEnumerable<int, int>>().OrderBy(x => x).Intersect(lookup, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { lookup.Intersect(new[] { 1 }.GroupBy(x => x)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { lookup.Intersect(new[] { 1 }.GroupBy(x => x), new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { new[] { 1 }.GroupBy(x => x).Intersect(lookup); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { new[] { 1 }.GroupBy(x => x).Intersect(lookup, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { lookup.Intersect(new[] { 1 }.GroupBy(x => x, new _IntComparer())); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { lookup.Intersect(new[] { 1 }.GroupBy(x => x, new _IntComparer()), new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { new[] { 1 }.GroupBy(x => x, new _IntComparer()).Intersect(lookup); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { new[] { 1 }.GroupBy(x => x, new _IntComparer()).Intersect(lookup, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { lookup.Intersect(new[] { 1 }.ToLookup(x => x)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { lookup.Intersect(new[] { 1 }.ToLookup(x => x), new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { new[] { 1 }.ToLookup(x => x).Intersect(lookup); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { new[] { 1 }.ToLookup(x => x).Intersect(lookup, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                // range isn't sensical
                try { empty.Intersect(Enumerable.Repeat(1, 1)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { empty.Intersect(Enumerable.Repeat(1, 1), new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Repeat(1, 1).Intersect(empty); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Repeat(1, 1).Intersect(empty, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                // reverseRange isn't sensiscal
                var goodGrouping = new[] { 1 }.GroupBy(x => x).First();
                try { lookup.Intersect(Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { lookup.Intersect(Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty(), new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty().Intersect(lookup); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty().Intersect(lookup, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { lookup.Intersect(Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty(goodGrouping)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { lookup.Intersect(Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty(goodGrouping), new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty(goodGrouping).Intersect(lookup); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty(goodGrouping).Intersect(lookup, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { lookup.Intersect(Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty().OrderBy(x => x)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { lookup.Intersect(Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty().OrderBy(x => x), new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty().OrderBy(x => x).Intersect(lookup); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty().OrderBy(x => x).Intersect(lookup, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { lookup.Intersect(Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty(goodGrouping).OrderBy(x => x)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { lookup.Intersect(Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty(goodGrouping).OrderBy(x => x), new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty(goodGrouping).OrderBy(x => x).Intersect(lookup); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty(goodGrouping).OrderBy(x => x).Intersect(lookup, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                Helper.ForEachEnumerableExpression(
                    lookup,
                    new GroupingEnumerable<int, int>[0],
                    res => { },
                    @"(a, b) =>
                      {
                        try { a.Intersect(b); Assert.Fail(); }catch(ArgumentException exc){ Assert.AreEqual(""first"", exc.ParamName); }
                        try { a.Intersect(b, new IntersectTests._GroupingComparer<int>()); Assert.Fail(); }catch(ArgumentException exc){ Assert.AreEqual(""first"", exc.ParamName); }

                        try { b.Intersect(a); Assert.Fail(); }catch(ArgumentException exc){ Assert.AreEqual(""second"", exc.ParamName); }
                        try { b.Intersect(a, new IntersectTests._GroupingComparer<int>()); Assert.Fail(); }catch(ArgumentException exc){ Assert.AreEqual(""second"", exc.ParamName); }

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
                try { range.Intersect(Enumerable.Empty<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { range.Intersect(Enumerable.Empty<int>(), new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Empty<int>().Intersect(range); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Empty<int>().Intersect(range, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { range.Intersect(Enumerable.Empty<int>().OrderBy(x => x)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { range.Intersect(Enumerable.Empty<int>().OrderBy(x => x), new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Empty<int>().OrderBy(x => x).Intersect(range); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Empty<int>().OrderBy(x => x).Intersect(range, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var badRangeGrouping = new RangeEnumerable<GroupingEnumerable<int, int>>();
                try { badRangeGrouping.Intersect(new[] { 1 }.GroupBy(x => x)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { badRangeGrouping.Intersect(new[] { 1 }.GroupBy(x => x), new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { new[] { 1 }.GroupBy(x => x).Intersect(badRangeGrouping); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { new[] { 1 }.GroupBy(x => x).Intersect(badRangeGrouping, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { badRangeGrouping.Intersect(new[] { 1 }.GroupBy(x => x, new _IntComparer())); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { badRangeGrouping.Intersect(new[] { 1 }.GroupBy(x => x, new _IntComparer()), new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { new[] { 1 }.GroupBy(x => x, new _IntComparer()).Intersect(badRangeGrouping); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { new[] { 1 }.GroupBy(x => x, new _IntComparer()).Intersect(badRangeGrouping, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { badRangeGrouping.Intersect(new[] { 1 }.ToLookup(x => x)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { badRangeGrouping.Intersect(new[] { 1 }.ToLookup(x => x), new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { new[] { 1 }.ToLookup(x => x).Intersect(badRangeGrouping); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { new[] { 1 }.ToLookup(x => x).Intersect(badRangeGrouping, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { range.Intersect(Enumerable.Range(1, 1)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { range.Intersect(Enumerable.Range(1, 1), new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Range(1, 1).Intersect(range); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Range(1, 1).Intersect(range, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { range.Intersect(Enumerable.Repeat(1, 1)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { range.Intersect(Enumerable.Repeat(1, 1), new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Repeat(1, 1).Intersect(range); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Repeat(1, 1).Intersect(range, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { range.Intersect(Enumerable.Range(1, 1).Reverse()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { range.Intersect(Enumerable.Range(1, 1).Reverse(), new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Range(1, 1).Reverse().Intersect(range); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Range(1, 1).Reverse().Intersect(range, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { range.Intersect(Enumerable.Empty<int>().DefaultIfEmpty()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { range.Intersect(Enumerable.Empty<int>().DefaultIfEmpty(), new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Empty<int>().DefaultIfEmpty().Intersect(range); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Empty<int>().DefaultIfEmpty().Intersect(range, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { range.Intersect(Enumerable.Empty<int>().DefaultIfEmpty(4)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { range.Intersect(Enumerable.Empty<int>().DefaultIfEmpty(4), new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Empty<int>().DefaultIfEmpty(4).Intersect(range); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Empty<int>().DefaultIfEmpty(4).Intersect(range, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { range.Intersect(Enumerable.Empty<int>().DefaultIfEmpty().OrderBy(x => x)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { range.Intersect(Enumerable.Empty<int>().DefaultIfEmpty().OrderBy(x => x), new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Empty<int>().DefaultIfEmpty().OrderBy(x => x).Intersect(range); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Empty<int>().DefaultIfEmpty().OrderBy(x => x).Intersect(range, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { range.Intersect(Enumerable.Empty<int>().DefaultIfEmpty(4).OrderBy(x => x)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { range.Intersect(Enumerable.Empty<int>().DefaultIfEmpty(4).OrderBy(x => x), new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Empty<int>().DefaultIfEmpty(4).OrderBy(x => x).Intersect(range); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Empty<int>().DefaultIfEmpty(4).OrderBy(x => x).Intersect(range, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                Helper.ForEachEnumerableExpression(
                    range,
                    new int[0],
                    res => { },
                    @"(a, b) =>
                      {
                        try { a.Intersect(b); Assert.Fail(); }catch(ArgumentException exc){ Assert.AreEqual(""first"", exc.ParamName); }
                        try { a.Intersect(b, new IntersectTests._IntComparer()); Assert.Fail(); }catch(ArgumentException exc){ Assert.AreEqual(""first"", exc.ParamName); }

                        try { b.Intersect(a); Assert.Fail(); }catch(ArgumentException exc){ Assert.AreEqual(""second"", exc.ParamName); }
                        try { b.Intersect(a, new IntersectTests._IntComparer()); Assert.Fail(); }catch(ArgumentException exc){ Assert.AreEqual(""second"", exc.ParamName); }

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
                try { repeat.Intersect(Enumerable.Empty<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { repeat.Intersect(Enumerable.Empty<int>(), new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Empty<int>().Intersect(repeat); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Empty<int>().Intersect(repeat, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { repeat.Intersect(Enumerable.Empty<int>().OrderBy(x => x)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { repeat.Intersect(Enumerable.Empty<int>().OrderBy(x => x), new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Empty<int>().OrderBy(x => x).Intersect(repeat); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Empty<int>().OrderBy(x => x).Intersect(repeat, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var badRepeatGrouping = new RepeatEnumerable<GroupingEnumerable<int, int>>();
                try { badRepeatGrouping.Intersect(new[] { 1 }.GroupBy(x => x)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { badRepeatGrouping.Intersect(new[] { 1 }.GroupBy(x => x), new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { new[] { 1 }.GroupBy(x => x).Intersect(badRepeatGrouping); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { new[] { 1 }.GroupBy(x => x).Intersect(badRepeatGrouping, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { badRepeatGrouping.Intersect(new[] { 1 }.GroupBy(x => x, new _IntComparer())); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { badRepeatGrouping.Intersect(new[] { 1 }.GroupBy(x => x, new _IntComparer()), new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { new[] { 1 }.GroupBy(x => x, new _IntComparer()).Intersect(badRepeatGrouping); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { new[] { 1 }.GroupBy(x => x, new _IntComparer()).Intersect(badRepeatGrouping, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { badRepeatGrouping.Intersect(new[] { 1 }.ToLookup(x => x)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { badRepeatGrouping.Intersect(new[] { 1 }.ToLookup(x => x), new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { new[] { 1 }.ToLookup(x => x).Intersect(badRepeatGrouping); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { new[] { 1 }.ToLookup(x => x).Intersect(badRepeatGrouping, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { repeat.Intersect(Enumerable.Range(1, 1)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { repeat.Intersect(Enumerable.Range(1, 1), new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Range(1, 1).Intersect(repeat); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Range(1, 1).Intersect(repeat, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { repeat.Intersect(Enumerable.Repeat(1, 1)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { repeat.Intersect(Enumerable.Repeat(1, 1), new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Repeat(1, 1).Intersect(repeat); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Repeat(1, 1).Intersect(repeat, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { repeat.Intersect(Enumerable.Range(1, 1).Reverse()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { repeat.Intersect(Enumerable.Range(1, 1).Reverse(), new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Range(1, 1).Reverse().Intersect(repeat); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Range(1, 1).Reverse().Intersect(repeat, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { repeat.Intersect(Enumerable.Empty<int>().DefaultIfEmpty()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { repeat.Intersect(Enumerable.Empty<int>().DefaultIfEmpty(), new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Empty<int>().DefaultIfEmpty().Intersect(repeat); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Empty<int>().DefaultIfEmpty().Intersect(repeat, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { repeat.Intersect(Enumerable.Empty<int>().DefaultIfEmpty(4)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { repeat.Intersect(Enumerable.Empty<int>().DefaultIfEmpty(4), new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Empty<int>().DefaultIfEmpty(4).Intersect(repeat); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Empty<int>().DefaultIfEmpty(4).Intersect(repeat, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { repeat.Intersect(Enumerable.Empty<int>().DefaultIfEmpty().OrderBy(x => x)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { repeat.Intersect(Enumerable.Empty<int>().DefaultIfEmpty().OrderBy(x => x), new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Empty<int>().DefaultIfEmpty().OrderBy(x => x).Intersect(repeat); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Empty<int>().DefaultIfEmpty().OrderBy(x => x).Intersect(repeat, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { repeat.Intersect(Enumerable.Empty<int>().DefaultIfEmpty(4).OrderBy(x => x)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { repeat.Intersect(Enumerable.Empty<int>().DefaultIfEmpty(4).OrderBy(x => x), new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Empty<int>().DefaultIfEmpty(4).OrderBy(x => x).Intersect(repeat); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Empty<int>().DefaultIfEmpty(4).OrderBy(x => x).Intersect(repeat, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                Helper.ForEachEnumerableExpression(
                    repeat,
                    new int[0],
                    res => { },
                    @"(a, b) =>
                      {
                        try { a.Intersect(b); Assert.Fail(); }catch(ArgumentException exc){ Assert.AreEqual(""first"", exc.ParamName); }
                        try { a.Intersect(b, new IntersectTests._IntComparer()); Assert.Fail(); }catch(ArgumentException exc){ Assert.AreEqual(""first"", exc.ParamName); }

                        try { b.Intersect(a); Assert.Fail(); }catch(ArgumentException exc){ Assert.AreEqual(""second"", exc.ParamName); }
                        try { b.Intersect(a, new IntersectTests._IntComparer()); Assert.Fail(); }catch(ArgumentException exc){ Assert.AreEqual(""second"", exc.ParamName); }

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
                try { reverseRange.Intersect(Enumerable.Empty<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { reverseRange.Intersect(Enumerable.Empty<int>(), new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Empty<int>().Intersect(reverseRange); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Empty<int>().Intersect(reverseRange, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { reverseRange.Intersect(Enumerable.Empty<int>().OrderBy(x => x)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { reverseRange.Intersect(Enumerable.Empty<int>().OrderBy(x => x), new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Empty<int>().OrderBy(x => x).Intersect(reverseRange); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Empty<int>().OrderBy(x => x).Intersect(reverseRange, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var badReverseRangeGrouping = new ReverseRangeEnumerable<GroupingEnumerable<int, int>>();
                try { badReverseRangeGrouping.Intersect(new[] { 1 }.GroupBy(x => x)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { badReverseRangeGrouping.Intersect(new[] { 1 }.GroupBy(x => x), new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { new[] { 1 }.GroupBy(x => x).Intersect(badReverseRangeGrouping); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { new[] { 1 }.GroupBy(x => x).Intersect(badReverseRangeGrouping, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { badReverseRangeGrouping.Intersect(new[] { 1 }.GroupBy(x => x, new _IntComparer())); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { badReverseRangeGrouping.Intersect(new[] { 1 }.GroupBy(x => x, new _IntComparer()), new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { new[] { 1 }.GroupBy(x => x, new _IntComparer()).Intersect(badReverseRangeGrouping); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { new[] { 1 }.GroupBy(x => x, new _IntComparer()).Intersect(badReverseRangeGrouping, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { badReverseRangeGrouping.Intersect(new[] { 1 }.ToLookup(x => x)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { badReverseRangeGrouping.Intersect(new[] { 1 }.ToLookup(x => x), new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { new[] { 1 }.ToLookup(x => x).Intersect(badReverseRangeGrouping); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { new[] { 1 }.ToLookup(x => x).Intersect(badReverseRangeGrouping, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { reverseRange.Intersect(Enumerable.Range(1, 1)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { reverseRange.Intersect(Enumerable.Range(1, 1), new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Range(1, 1).Intersect(reverseRange); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Range(1, 1).Intersect(reverseRange, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { reverseRange.Intersect(Enumerable.Repeat(1, 1)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { reverseRange.Intersect(Enumerable.Repeat(1, 1), new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Repeat(1, 1).Intersect(reverseRange); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Repeat(1, 1).Intersect(reverseRange, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { reverseRange.Intersect(Enumerable.Range(1, 1).Reverse()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { reverseRange.Intersect(Enumerable.Range(1, 1).Reverse(), new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Range(1, 1).Reverse().Intersect(reverseRange); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Range(1, 1).Reverse().Intersect(reverseRange, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { reverseRange.Intersect(Enumerable.Empty<int>().DefaultIfEmpty()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { reverseRange.Intersect(Enumerable.Empty<int>().DefaultIfEmpty(), new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Empty<int>().DefaultIfEmpty().Intersect(reverseRange); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Empty<int>().DefaultIfEmpty().Intersect(reverseRange, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { reverseRange.Intersect(Enumerable.Empty<int>().DefaultIfEmpty(4)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { reverseRange.Intersect(Enumerable.Empty<int>().DefaultIfEmpty(4), new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Empty<int>().DefaultIfEmpty(4).Intersect(reverseRange); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Empty<int>().DefaultIfEmpty(4).Intersect(reverseRange, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { reverseRange.Intersect(Enumerable.Empty<int>().DefaultIfEmpty().OrderBy(x => x)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { reverseRange.Intersect(Enumerable.Empty<int>().DefaultIfEmpty().OrderBy(x => x), new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Empty<int>().DefaultIfEmpty().OrderBy(x => x).Intersect(reverseRange); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Empty<int>().DefaultIfEmpty().OrderBy(x => x).Intersect(reverseRange, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { reverseRange.Intersect(Enumerable.Empty<int>().DefaultIfEmpty(4).OrderBy(x => x)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { reverseRange.Intersect(Enumerable.Empty<int>().DefaultIfEmpty(4).OrderBy(x => x), new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Empty<int>().DefaultIfEmpty(4).OrderBy(x => x).Intersect(reverseRange); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Empty<int>().DefaultIfEmpty(4).OrderBy(x => x).Intersect(reverseRange, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                Helper.ForEachEnumerableExpression(
                    reverseRange,
                    new int[0],
                    res => { },
                    @"(a, b) =>
                      {
                        try { a.Intersect(b); Assert.Fail(); }catch(ArgumentException exc){ Assert.AreEqual(""first"", exc.ParamName); }
                        try { a.Intersect(b, new IntersectTests._IntComparer()); Assert.Fail(); }catch(ArgumentException exc){ Assert.AreEqual(""first"", exc.ParamName); }

                        try { b.Intersect(a); Assert.Fail(); }catch(ArgumentException exc){ Assert.AreEqual(""second"", exc.ParamName); }
                        try { b.Intersect(a, new IntersectTests._IntComparer()); Assert.Fail(); }catch(ArgumentException exc){ Assert.AreEqual(""second"", exc.ParamName); }

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
                try { oneItemDefault.Intersect(Enumerable.Empty<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemDefault.Intersect(Enumerable.Empty<int>(), new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Empty<int>().Intersect(oneItemDefault); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Empty<int>().Intersect(oneItemDefault, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemDefault.Intersect(Enumerable.Empty<int>().OrderBy(x => x)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemDefault.Intersect(Enumerable.Empty<int>().OrderBy(x => x), new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Empty<int>().OrderBy(x => x).Intersect(oneItemDefault); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Empty<int>().OrderBy(x => x).Intersect(oneItemDefault, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var badOneItemDefaultGrouping = new OneItemDefaultEnumerable<GroupingEnumerable<int, int>>();
                try { badOneItemDefaultGrouping.Intersect(new[] { 1 }.GroupBy(x => x)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { badOneItemDefaultGrouping.Intersect(new[] { 1 }.GroupBy(x => x), new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { new[] { 1 }.GroupBy(x => x).Intersect(badOneItemDefaultGrouping); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { new[] { 1 }.GroupBy(x => x).Intersect(badOneItemDefaultGrouping, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { badOneItemDefaultGrouping.Intersect(new[] { 1 }.GroupBy(x => x, new _IntComparer())); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { badOneItemDefaultGrouping.Intersect(new[] { 1 }.GroupBy(x => x, new _IntComparer()), new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { new[] { 1 }.GroupBy(x => x, new _IntComparer()).Intersect(badOneItemDefaultGrouping); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { new[] { 1 }.GroupBy(x => x, new _IntComparer()).Intersect(badOneItemDefaultGrouping, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { badOneItemDefaultGrouping.Intersect(new[] { 1 }.ToLookup(x => x)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { badOneItemDefaultGrouping.Intersect(new[] { 1 }.ToLookup(x => x), new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { new[] { 1 }.ToLookup(x => x).Intersect(badOneItemDefaultGrouping); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { new[] { 1 }.ToLookup(x => x).Intersect(badOneItemDefaultGrouping, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemDefault.Intersect(Enumerable.Range(1, 1)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemDefault.Intersect(Enumerable.Range(1, 1), new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Range(1, 1).Intersect(oneItemDefault); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Range(1, 1).Intersect(oneItemDefault, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemDefault.Intersect(Enumerable.Repeat(1, 1)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemDefault.Intersect(Enumerable.Repeat(1, 1), new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Repeat(1, 1).Intersect(oneItemDefault); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Repeat(1, 1).Intersect(oneItemDefault, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemDefault.Intersect(Enumerable.Range(1, 1).Reverse()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemDefault.Intersect(Enumerable.Range(1, 1).Reverse(), new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Range(1, 1).Reverse().Intersect(oneItemDefault); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Range(1, 1).Reverse().Intersect(oneItemDefault, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemDefault.Intersect(Enumerable.Empty<int>().DefaultIfEmpty()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemDefault.Intersect(Enumerable.Empty<int>().DefaultIfEmpty(), new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Empty<int>().DefaultIfEmpty().Intersect(oneItemDefault); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Empty<int>().DefaultIfEmpty().Intersect(oneItemDefault, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemDefault.Intersect(Enumerable.Empty<int>().DefaultIfEmpty(4)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemDefault.Intersect(Enumerable.Empty<int>().DefaultIfEmpty(4), new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Empty<int>().DefaultIfEmpty(4).Intersect(oneItemDefault); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Empty<int>().DefaultIfEmpty(4).Intersect(oneItemDefault, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemDefault.Intersect(Enumerable.Empty<int>().DefaultIfEmpty().OrderBy(x => x)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemDefault.Intersect(Enumerable.Empty<int>().DefaultIfEmpty().OrderBy(x => x), new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Empty<int>().DefaultIfEmpty().OrderBy(x => x).Intersect(oneItemDefault); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Empty<int>().DefaultIfEmpty().OrderBy(x => x).Intersect(oneItemDefault, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemDefault.Intersect(Enumerable.Empty<int>().DefaultIfEmpty(4).OrderBy(x => x)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemDefault.Intersect(Enumerable.Empty<int>().DefaultIfEmpty(4).OrderBy(x => x), new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Empty<int>().DefaultIfEmpty(4).OrderBy(x => x).Intersect(oneItemDefault); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Empty<int>().DefaultIfEmpty(4).OrderBy(x => x).Intersect(oneItemDefault, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                Helper.ForEachEnumerableExpression(
                    oneItemDefault,
                    new int[0],
                    res => { },
                    @"(a, b) =>
                      {
                        try { a.Intersect(b); Assert.Fail(); }catch(ArgumentException exc){ Assert.AreEqual(""first"", exc.ParamName); }
                        try { a.Intersect(b, new IntersectTests._IntComparer()); Assert.Fail(); }catch(ArgumentException exc){ Assert.AreEqual(""first"", exc.ParamName); }

                        try { b.Intersect(a); Assert.Fail(); }catch(ArgumentException exc){ Assert.AreEqual(""second"", exc.ParamName); }
                        try { b.Intersect(a, new IntersectTests._IntComparer()); Assert.Fail(); }catch(ArgumentException exc){ Assert.AreEqual(""second"", exc.ParamName); }

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
                try { oneItemSpecific.Intersect(Enumerable.Empty<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemSpecific.Intersect(Enumerable.Empty<int>(), new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Empty<int>().Intersect(oneItemSpecific); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Empty<int>().Intersect(oneItemSpecific, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemSpecific.Intersect(Enumerable.Empty<int>().OrderBy(x => x)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemSpecific.Intersect(Enumerable.Empty<int>().OrderBy(x => x), new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Empty<int>().OrderBy(x => x).Intersect(oneItemSpecific); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Empty<int>().OrderBy(x => x).Intersect(oneItemSpecific, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var badOneItemSpecificGrouping = new OneItemSpecificEnumerable<GroupingEnumerable<int, int>>();
                try { badOneItemSpecificGrouping.Intersect(new[] { 1 }.GroupBy(x => x)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { badOneItemSpecificGrouping.Intersect(new[] { 1 }.GroupBy(x => x), new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { new[] { 1 }.GroupBy(x => x).Intersect(badOneItemSpecificGrouping); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { new[] { 1 }.GroupBy(x => x).Intersect(badOneItemSpecificGrouping, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { badOneItemSpecificGrouping.Intersect(new[] { 1 }.GroupBy(x => x, new _IntComparer())); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { badOneItemSpecificGrouping.Intersect(new[] { 1 }.GroupBy(x => x, new _IntComparer()), new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { new[] { 1 }.GroupBy(x => x, new _IntComparer()).Intersect(badOneItemSpecificGrouping); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { new[] { 1 }.GroupBy(x => x, new _IntComparer()).Intersect(badOneItemSpecificGrouping, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { badOneItemSpecificGrouping.Intersect(new[] { 1 }.ToLookup(x => x)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { badOneItemSpecificGrouping.Intersect(new[] { 1 }.ToLookup(x => x), new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { new[] { 1 }.ToLookup(x => x).Intersect(badOneItemSpecificGrouping); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { new[] { 1 }.ToLookup(x => x).Intersect(badOneItemSpecificGrouping, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemSpecific.Intersect(Enumerable.Range(1, 1)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemSpecific.Intersect(Enumerable.Range(1, 1), new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Range(1, 1).Intersect(oneItemSpecific); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Range(1, 1).Intersect(oneItemSpecific, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemSpecific.Intersect(Enumerable.Repeat(1, 1)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemSpecific.Intersect(Enumerable.Repeat(1, 1), new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Repeat(1, 1).Intersect(oneItemSpecific); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Repeat(1, 1).Intersect(oneItemSpecific, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemSpecific.Intersect(Enumerable.Range(1, 1).Reverse()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemSpecific.Intersect(Enumerable.Range(1, 1).Reverse(), new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Range(1, 1).Reverse().Intersect(oneItemSpecific); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Range(1, 1).Reverse().Intersect(oneItemSpecific, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemSpecific.Intersect(Enumerable.Empty<int>().DefaultIfEmpty()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemSpecific.Intersect(Enumerable.Empty<int>().DefaultIfEmpty(), new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Empty<int>().DefaultIfEmpty().Intersect(oneItemSpecific); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Empty<int>().DefaultIfEmpty().Intersect(oneItemSpecific, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemSpecific.Intersect(Enumerable.Empty<int>().DefaultIfEmpty(4)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemSpecific.Intersect(Enumerable.Empty<int>().DefaultIfEmpty(4), new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Empty<int>().DefaultIfEmpty(4).Intersect(oneItemSpecific); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Empty<int>().DefaultIfEmpty(4).Intersect(oneItemSpecific, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemSpecific.Intersect(Enumerable.Empty<int>().DefaultIfEmpty().OrderBy(x => x)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemSpecific.Intersect(Enumerable.Empty<int>().DefaultIfEmpty().OrderBy(x => x), new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Empty<int>().DefaultIfEmpty().OrderBy(x => x).Intersect(oneItemSpecific); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Empty<int>().DefaultIfEmpty().OrderBy(x => x).Intersect(oneItemSpecific, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemSpecific.Intersect(Enumerable.Empty<int>().DefaultIfEmpty(4).OrderBy(x => x)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemSpecific.Intersect(Enumerable.Empty<int>().DefaultIfEmpty(4).OrderBy(x => x), new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Empty<int>().DefaultIfEmpty(4).OrderBy(x => x).Intersect(oneItemSpecific); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Empty<int>().DefaultIfEmpty(4).OrderBy(x => x).Intersect(oneItemSpecific, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                Helper.ForEachEnumerableExpression(
                    oneItemSpecific,
                    new int[0],
                    res => { },
                    @"(a, b) =>
                      {
                        try { a.Intersect(b); Assert.Fail(); }catch(ArgumentException exc){ Assert.AreEqual(""first"", exc.ParamName); }
                        try { a.Intersect(b, new IntersectTests._IntComparer()); Assert.Fail(); }catch(ArgumentException exc){ Assert.AreEqual(""first"", exc.ParamName); }

                        try { b.Intersect(a); Assert.Fail(); }catch(ArgumentException exc){ Assert.AreEqual(""second"", exc.ParamName); }
                        try { b.Intersect(a, new IntersectTests._IntComparer()); Assert.Fail(); }catch(ArgumentException exc){ Assert.AreEqual(""second"", exc.ParamName); }

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
                try { oneItemDefaultOrdered.Intersect(Enumerable.Empty<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemDefaultOrdered.Intersect(Enumerable.Empty<int>(), new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Empty<int>().Intersect(oneItemDefaultOrdered); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Empty<int>().Intersect(oneItemDefaultOrdered, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemDefaultOrdered.Intersect(Enumerable.Empty<int>().OrderBy(x => x)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemDefaultOrdered.Intersect(Enumerable.Empty<int>().OrderBy(x => x), new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Empty<int>().OrderBy(x => x).Intersect(oneItemDefaultOrdered); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Empty<int>().OrderBy(x => x).Intersect(oneItemDefaultOrdered, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var badOneItemDefaultOrderedGrouping = new OneItemDefaultOrderedEnumerable<GroupingEnumerable<int, int>>();
                try { badOneItemDefaultOrderedGrouping.Intersect(new[] { 1 }.GroupBy(x => x)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { badOneItemDefaultOrderedGrouping.Intersect(new[] { 1 }.GroupBy(x => x), new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { new[] { 1 }.GroupBy(x => x).Intersect(badOneItemDefaultOrderedGrouping); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { new[] { 1 }.GroupBy(x => x).Intersect(badOneItemDefaultOrderedGrouping, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { badOneItemDefaultOrderedGrouping.Intersect(new[] { 1 }.GroupBy(x => x, new _IntComparer())); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { badOneItemDefaultOrderedGrouping.Intersect(new[] { 1 }.GroupBy(x => x, new _IntComparer()), new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { new[] { 1 }.GroupBy(x => x, new _IntComparer()).Intersect(badOneItemDefaultOrderedGrouping); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { new[] { 1 }.GroupBy(x => x, new _IntComparer()).Intersect(badOneItemDefaultOrderedGrouping, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { badOneItemDefaultOrderedGrouping.Intersect(new[] { 1 }.ToLookup(x => x)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { badOneItemDefaultOrderedGrouping.Intersect(new[] { 1 }.ToLookup(x => x), new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { new[] { 1 }.ToLookup(x => x).Intersect(badOneItemDefaultOrderedGrouping); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { new[] { 1 }.ToLookup(x => x).Intersect(badOneItemDefaultOrderedGrouping, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemDefaultOrdered.Intersect(Enumerable.Range(1, 1)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemDefaultOrdered.Intersect(Enumerable.Range(1, 1), new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Range(1, 1).Intersect(oneItemDefaultOrdered); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Range(1, 1).Intersect(oneItemDefaultOrdered, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemDefaultOrdered.Intersect(Enumerable.Repeat(1, 1)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemDefaultOrdered.Intersect(Enumerable.Repeat(1, 1), new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Repeat(1, 1).Intersect(oneItemDefaultOrdered); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Repeat(1, 1).Intersect(oneItemDefaultOrdered, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemDefaultOrdered.Intersect(Enumerable.Range(1, 1).Reverse()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemDefaultOrdered.Intersect(Enumerable.Range(1, 1).Reverse(), new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Range(1, 1).Reverse().Intersect(oneItemDefaultOrdered); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Range(1, 1).Reverse().Intersect(oneItemDefaultOrdered, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemDefaultOrdered.Intersect(Enumerable.Empty<int>().DefaultIfEmpty()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemDefaultOrdered.Intersect(Enumerable.Empty<int>().DefaultIfEmpty(), new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Empty<int>().DefaultIfEmpty().Intersect(oneItemDefaultOrdered); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Empty<int>().DefaultIfEmpty().Intersect(oneItemDefaultOrdered, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemDefaultOrdered.Intersect(Enumerable.Empty<int>().DefaultIfEmpty(4)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemDefaultOrdered.Intersect(Enumerable.Empty<int>().DefaultIfEmpty(4), new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Empty<int>().DefaultIfEmpty(4).Intersect(oneItemDefaultOrdered); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Empty<int>().DefaultIfEmpty(4).Intersect(oneItemDefaultOrdered, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemDefaultOrdered.Intersect(Enumerable.Empty<int>().DefaultIfEmpty().OrderBy(x => x)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemDefaultOrdered.Intersect(Enumerable.Empty<int>().DefaultIfEmpty().OrderBy(x => x), new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Empty<int>().DefaultIfEmpty().OrderBy(x => x).Intersect(oneItemDefaultOrdered); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Empty<int>().DefaultIfEmpty().OrderBy(x => x).Intersect(oneItemDefaultOrdered, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemDefaultOrdered.Intersect(Enumerable.Empty<int>().DefaultIfEmpty(4).OrderBy(x => x)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemDefaultOrdered.Intersect(Enumerable.Empty<int>().DefaultIfEmpty(4).OrderBy(x => x), new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Empty<int>().DefaultIfEmpty(4).OrderBy(x => x).Intersect(oneItemDefaultOrdered); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Empty<int>().DefaultIfEmpty(4).OrderBy(x => x).Intersect(oneItemDefaultOrdered, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                Helper.ForEachEnumerableExpression(
                    oneItemDefaultOrdered,
                    new int[0],
                    res => { },
                    @"(a, b) =>
                      {
                        try { a.Intersect(b); Assert.Fail(); }catch(ArgumentException exc){ Assert.AreEqual(""first"", exc.ParamName); }
                        try { a.Intersect(b, new IntersectTests._IntComparer()); Assert.Fail(); }catch(ArgumentException exc){ Assert.AreEqual(""first"", exc.ParamName); }

                        try { b.Intersect(a); Assert.Fail(); }catch(ArgumentException exc){ Assert.AreEqual(""second"", exc.ParamName); }
                        try { b.Intersect(a, new IntersectTests._IntComparer()); Assert.Fail(); }catch(ArgumentException exc){ Assert.AreEqual(""second"", exc.ParamName); }

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
                try { oneItemSpecificOrdered.Intersect(Enumerable.Empty<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemSpecificOrdered.Intersect(Enumerable.Empty<int>(), new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Empty<int>().Intersect(oneItemSpecificOrdered); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Empty<int>().Intersect(oneItemSpecificOrdered, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemSpecificOrdered.Intersect(Enumerable.Empty<int>().OrderBy(x => x)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemSpecificOrdered.Intersect(Enumerable.Empty<int>().OrderBy(x => x), new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Empty<int>().OrderBy(x => x).Intersect(oneItemSpecificOrdered); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Empty<int>().OrderBy(x => x).Intersect(oneItemSpecificOrdered, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var badOneItemSpecificOrderedGrouping = new OneItemSpecificOrderedEnumerable<GroupingEnumerable<int, int>>();
                try { badOneItemSpecificOrderedGrouping.Intersect(new[] { 1 }.GroupBy(x => x)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { badOneItemSpecificOrderedGrouping.Intersect(new[] { 1 }.GroupBy(x => x), new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { new[] { 1 }.GroupBy(x => x).Intersect(badOneItemSpecificOrderedGrouping); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { new[] { 1 }.GroupBy(x => x).Intersect(badOneItemSpecificOrderedGrouping, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { badOneItemSpecificOrderedGrouping.Intersect(new[] { 1 }.GroupBy(x => x, new _IntComparer())); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { badOneItemSpecificOrderedGrouping.Intersect(new[] { 1 }.GroupBy(x => x, new _IntComparer()), new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { new[] { 1 }.GroupBy(x => x, new _IntComparer()).Intersect(badOneItemSpecificOrderedGrouping); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { new[] { 1 }.GroupBy(x => x, new _IntComparer()).Intersect(badOneItemSpecificOrderedGrouping, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { badOneItemSpecificOrderedGrouping.Intersect(new[] { 1 }.ToLookup(x => x)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { badOneItemSpecificOrderedGrouping.Intersect(new[] { 1 }.ToLookup(x => x), new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { new[] { 1 }.ToLookup(x => x).Intersect(badOneItemSpecificOrderedGrouping); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { new[] { 1 }.ToLookup(x => x).Intersect(badOneItemSpecificOrderedGrouping, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemSpecificOrdered.Intersect(Enumerable.Range(1, 1)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemSpecificOrdered.Intersect(Enumerable.Range(1, 1), new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Range(1, 1).Intersect(oneItemSpecificOrdered); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Range(1, 1).Intersect(oneItemSpecificOrdered, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemSpecificOrdered.Intersect(Enumerable.Repeat(1, 1)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemSpecificOrdered.Intersect(Enumerable.Repeat(1, 1), new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Repeat(1, 1).Intersect(oneItemSpecificOrdered); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Repeat(1, 1).Intersect(oneItemSpecificOrdered, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemSpecificOrdered.Intersect(Enumerable.Range(1, 1).Reverse()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemSpecificOrdered.Intersect(Enumerable.Range(1, 1).Reverse(), new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Range(1, 1).Reverse().Intersect(oneItemSpecificOrdered); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Range(1, 1).Reverse().Intersect(oneItemSpecificOrdered, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemSpecificOrdered.Intersect(Enumerable.Empty<int>().DefaultIfEmpty()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemSpecificOrdered.Intersect(Enumerable.Empty<int>().DefaultIfEmpty(), new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Empty<int>().DefaultIfEmpty().Intersect(oneItemSpecificOrdered); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Empty<int>().DefaultIfEmpty().Intersect(oneItemSpecificOrdered, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemSpecificOrdered.Intersect(Enumerable.Empty<int>().DefaultIfEmpty(4)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemSpecificOrdered.Intersect(Enumerable.Empty<int>().DefaultIfEmpty(4), new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Empty<int>().DefaultIfEmpty(4).Intersect(oneItemSpecificOrdered); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Empty<int>().DefaultIfEmpty(4).Intersect(oneItemSpecificOrdered, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemSpecificOrdered.Intersect(Enumerable.Empty<int>().DefaultIfEmpty().OrderBy(x => x)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemSpecificOrdered.Intersect(Enumerable.Empty<int>().DefaultIfEmpty().OrderBy(x => x), new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Empty<int>().DefaultIfEmpty().OrderBy(x => x).Intersect(oneItemSpecificOrdered); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Empty<int>().DefaultIfEmpty().OrderBy(x => x).Intersect(oneItemSpecificOrdered, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemSpecificOrdered.Intersect(Enumerable.Empty<int>().DefaultIfEmpty(4).OrderBy(x => x)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemSpecificOrdered.Intersect(Enumerable.Empty<int>().DefaultIfEmpty(4).OrderBy(x => x), new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Empty<int>().DefaultIfEmpty(4).OrderBy(x => x).Intersect(oneItemSpecificOrdered); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Empty<int>().DefaultIfEmpty(4).OrderBy(x => x).Intersect(oneItemSpecificOrdered, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                Helper.ForEachEnumerableExpression(
                    oneItemSpecificOrdered,
                    new int[0],
                    res => { },
                    @"(a, b) =>
                      {
                        try { a.Intersect(b); Assert.Fail(); }catch(ArgumentException exc){ Assert.AreEqual(""first"", exc.ParamName); }
                        try { a.Intersect(b, new IntersectTests._IntComparer()); Assert.Fail(); }catch(ArgumentException exc){ Assert.AreEqual(""first"", exc.ParamName); }

                        try { b.Intersect(a); Assert.Fail(); }catch(ArgumentException exc){ Assert.AreEqual(""second"", exc.ParamName); }
                        try { b.Intersect(a, new IntersectTests._IntComparer()); Assert.Fail(); }catch(ArgumentException exc){ Assert.AreEqual(""second"", exc.ParamName); }

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