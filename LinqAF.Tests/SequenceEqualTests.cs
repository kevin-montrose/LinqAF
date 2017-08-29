using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using TestHelpers;

namespace LinqAF.Tests
{
    [TestClass]
    public class SequenceEqualTests
    {
        [TestMethod]
        public void Universal()
        {
            var enums = Helper.AllEnumerables();

            foreach (var e in enums)
            {
                System.Collections.Generic.List<string> missing;
                if (!Helper.Implements(e, typeof(LinqAF.Impl.ISequenceEqual<>), out missing))
                {
                    Assert.Fail($"{e.Name} does not implement ISequenceEqual ({string.Join(", ", missing)})");
                }
            }
        }

        [TestMethod]
        public void AcceptsAllEnumerables()
        {
            var missingSimple = new List<string>();
            var missingComparer = new List<string>();

            var isequenceequal = typeof(Impl.ISequenceEqual<>);
            var enums = Helper.AllEnumerables(includeWeirdOnes: false);
            foreach (var e in enums)
            {
                var simple =
                    isequenceequal
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
                    isequenceequal
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

                                if (p != e) return false;

                                var iec = ps[1].ParameterType;
                                if (iec.IsGenericType && !iec.IsGenericTypeDefinition)
                                {
                                    iec = Helper.GetGenericTypeDefinition(iec);
                                }

                                return iec == typeof(IEqualityComparer<>);
                            }
                        )
                        .SingleOrDefault();
                if (comparer == null) missingComparer.Add(e.Name);
            }

            if (missingSimple.Any())
            {
                Assert.Fail("Missing simple methods for: \r\n" + string.Join("\r\n", missingSimple));
            }

            if (missingComparer.Any())
            {
                Assert.Fail("Missing comparer methods for: \r\n" + string.Join("\r\n", missingComparer));
            }
        }

        public class _Comparer : IEqualityComparer<string>
        {
            public bool Equals(string x, string y) => x.Length == y.Length;

            public int GetHashCode(string obj) => obj.Length;
        }

        [TestMethod]
        public void Chaining()
        {
            // default, true
            {
                Helper.ForEachEnumerableNoRetExpression(
                    new int[] { 1, 2, 3 },
                    @"a =>
                    Helper.ForEachEnumerableExpression(
                        a,
                        new [] { 1, 2, 3 },
                        _ => { },
                        ""(a, b) => { Assert.IsTrue(a.SequenceEqual(b)); return Enumerable.Empty<int>(); }"",
                        typeof(EmptyEnumerable<>),
                        typeof(EmptyOrderedEnumerable<>),
                        typeof(GroupByDefaultEnumerable<,,,,>),
                        typeof(GroupBySpecificEnumerable<,,,,>),
                        typeof(LookupDefaultEnumerable<,>),
                        typeof(LookupSpecificEnumerable<,>)
                    )",
                    typeof(EmptyEnumerable<>),
                    typeof(EmptyOrderedEnumerable<>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );
            }

            // default, false
            {
                Helper.ForEachEnumerableNoRetExpression(
                    new int[] { 1, 2, 3 },
                    @"a =>
                    Helper.ForEachEnumerableExpression(
                        a,
                        new [] { 4, 5 },
                        _ => { },
                        ""(a, b) => { Assert.IsFalse(a.SequenceEqual(b)); return Enumerable.Empty<int>(); }"",
                        typeof(EmptyEnumerable<>),
                        typeof(EmptyOrderedEnumerable<>),
                        typeof(GroupByDefaultEnumerable<,,,,>),
                        typeof(GroupBySpecificEnumerable<,,,,>),
                        typeof(LookupDefaultEnumerable<,>),
                        typeof(LookupSpecificEnumerable<,>)
                    )",
                    typeof(EmptyEnumerable<>),
                    typeof(EmptyOrderedEnumerable<>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );
            }

            // specific, true
            {
                Helper.ForEachEnumerableNoRetExpression(
                    new[] { "hello", "foo", "fizz" },
                    @"a =>
                    Helper.ForEachEnumerableExpression(
                        a,
                        new [] { ""world"", ""bar"", ""buzz"" },
                        _ => { },
                        ""(a, b) => { Assert.IsTrue(a.SequenceEqual(b, new SequenceEqualTests._Comparer())); return Enumerable.Empty<string>(); }"",
                        typeof(EmptyEnumerable<>),
                        typeof(EmptyOrderedEnumerable<>),
                        typeof(GroupByDefaultEnumerable<,,,,>),
                        typeof(GroupBySpecificEnumerable<,,,,>),
                        typeof(LookupDefaultEnumerable<,>),
                        typeof(LookupSpecificEnumerable<,>)
                    )",
                    typeof(EmptyEnumerable<>),
                    typeof(EmptyOrderedEnumerable<>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );
            }

            // specific, false
            {
                Helper.ForEachEnumerableNoRetExpression(
                    new[] { "hello", "foo", "fizz" },
                    @"a =>
                    Helper.ForEachEnumerableExpression(
                        a,
                        new [] { ""world"", ""x"", ""yyyy"" },
                        _ => { },
                        ""(a, b) => { Assert.IsFalse(a.SequenceEqual(b, new SequenceEqualTests._Comparer())); return Enumerable.Empty<string>(); }"",
                        typeof(EmptyEnumerable<>),
                        typeof(EmptyOrderedEnumerable<>),
                        typeof(GroupByDefaultEnumerable<,,,,>),
                        typeof(GroupBySpecificEnumerable<,,,,>),
                        typeof(LookupDefaultEnumerable<,>),
                        typeof(LookupSpecificEnumerable<,>)
                    )",
                    typeof(EmptyEnumerable<>),
                    typeof(EmptyOrderedEnumerable<>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
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
                Assert.IsTrue(empty.SequenceEqual(empty));
                Assert.IsTrue(empty.SequenceEqual(empty, new _IntComparer()));
                Assert.IsTrue(empty.SequenceEqual(emptyOrdered));
                Assert.IsTrue(empty.SequenceEqual(emptyOrdered, new _IntComparer()));
                var emptyGroupingInt = Enumerable.Empty<GroupingEnumerable<int, int>>();
                Assert.IsFalse(emptyGroupingInt.SequenceEqual(groupByDefault));
                Assert.IsFalse(emptyGroupingInt.SequenceEqual(groupByDefault, new _GroupingComparer<int>()));
                var emptyGroupingString = Enumerable.Empty<GroupingEnumerable<string, string>>();
                Assert.IsFalse(emptyGroupingString.SequenceEqual(groupBySpecific));
                Assert.IsFalse(emptyGroupingString.SequenceEqual(groupBySpecific, new _GroupingComparer<string>()));
                Assert.IsFalse(emptyGroupingInt.SequenceEqual(lookup));
                Assert.IsFalse(emptyGroupingInt.SequenceEqual(lookup, new _GroupingComparer<int>()));
                Assert.IsFalse(empty.SequenceEqual(range));
                Assert.IsFalse(empty.SequenceEqual(range, new _IntComparer()));
                var repeatInt = Enumerable.Repeat(1, 1);
                Assert.IsFalse(empty.SequenceEqual(repeatInt));
                Assert.IsFalse(empty.SequenceEqual(repeatInt, new _IntComparer()));
                Assert.IsFalse(empty.SequenceEqual(reverseRange));
                Assert.IsFalse(empty.SequenceEqual(reverseRange, new _IntComparer()));

                var defaultIfEmptyDefault = new int[0].DefaultIfEmpty();
                Assert.IsFalse(empty.SequenceEqual(defaultIfEmptyDefault));
                Assert.IsFalse(empty.SequenceEqual(defaultIfEmptyDefault, new _IntComparer()));
                var defaultIfEmptySpecific = new int[0].DefaultIfEmpty(4);
                Assert.IsFalse(empty.SequenceEqual(defaultIfEmptySpecific));
                Assert.IsFalse(empty.SequenceEqual(defaultIfEmptySpecific, new _IntComparer()));

                Assert.IsFalse(empty.SequenceEqual(oneItemDefault));
                Assert.IsFalse(empty.SequenceEqual(oneItemDefault, new _IntComparer()));
                Assert.IsFalse(empty.SequenceEqual(oneItemSpecific));
                Assert.IsFalse(empty.SequenceEqual(oneItemSpecific, new _IntComparer()));
                Assert.IsFalse(empty.SequenceEqual(oneItemDefaultOrdered));
                Assert.IsFalse(empty.SequenceEqual(oneItemDefaultOrdered, new _IntComparer()));
                Assert.IsFalse(empty.SequenceEqual(oneItemSpecificOrdered));
                Assert.IsFalse(empty.SequenceEqual(oneItemSpecificOrdered, new _IntComparer()));

                Helper.ForEachEnumerableExpression(
                    empty,
                    new int[0],
                    res => { },
                    @"(a, b) =>
                      {
                        Assert.IsTrue(a.SequenceEqual(b));
                        Assert.IsTrue(a.SequenceEqual(b, new SequenceEqualTests._IntComparer()));

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
                Assert.IsTrue(emptyOrdered.SequenceEqual(empty));
                Assert.IsTrue(emptyOrdered.SequenceEqual(empty, new _IntComparer()));
                Assert.IsTrue(emptyOrdered.SequenceEqual(emptyOrdered));
                Assert.IsTrue(emptyOrdered.SequenceEqual(emptyOrdered, new _IntComparer()));
                var emptyOrderedGroupingInt = Enumerable.Empty<GroupingEnumerable<int, int>>().OrderBy(x => x);
                Assert.IsFalse(emptyOrderedGroupingInt.SequenceEqual(groupByDefault));
                Assert.IsFalse(emptyOrderedGroupingInt.SequenceEqual(groupByDefault, new _GroupingComparer<int>()));
                var emptyOrderedGroupingString = Enumerable.Empty<GroupingEnumerable<string, string>>().OrderBy(x => x);
                Assert.IsFalse(emptyOrderedGroupingString.SequenceEqual(groupBySpecific));
                Assert.IsFalse(emptyOrderedGroupingString.SequenceEqual(groupBySpecific, new _GroupingComparer<string>()));
                Assert.IsFalse(emptyOrderedGroupingInt.SequenceEqual(lookup));
                Assert.IsFalse(emptyOrderedGroupingInt.SequenceEqual(lookup, new _GroupingComparer<int>()));
                Assert.IsFalse(emptyOrdered.SequenceEqual(range));
                Assert.IsFalse(emptyOrdered.SequenceEqual(range, new _IntComparer()));
                var repeatInt = Enumerable.Repeat(1, 1);
                Assert.IsFalse(emptyOrdered.SequenceEqual(repeatInt));
                Assert.IsFalse(emptyOrdered.SequenceEqual(repeatInt, new _IntComparer()));
                Assert.IsFalse(emptyOrdered.SequenceEqual(reverseRange));
                Assert.IsFalse(emptyOrdered.SequenceEqual(reverseRange, new _IntComparer()));

                var defaultIfEmptyDefault = new int[0].DefaultIfEmpty();
                Assert.IsFalse(emptyOrdered.SequenceEqual(defaultIfEmptyDefault));
                Assert.IsFalse(emptyOrdered.SequenceEqual(defaultIfEmptyDefault, new _IntComparer()));
                var defaultIfEmptySpecific = new int[0].DefaultIfEmpty(4);
                Assert.IsFalse(emptyOrdered.SequenceEqual(defaultIfEmptySpecific));
                Assert.IsFalse(emptyOrdered.SequenceEqual(defaultIfEmptySpecific, new _IntComparer()));

                Assert.IsFalse(emptyOrdered.SequenceEqual(oneItemDefault));
                Assert.IsFalse(emptyOrdered.SequenceEqual(oneItemDefault, new _IntComparer()));
                Assert.IsFalse(emptyOrdered.SequenceEqual(oneItemSpecific));
                Assert.IsFalse(emptyOrdered.SequenceEqual(oneItemSpecific, new _IntComparer()));
                Assert.IsFalse(emptyOrdered.SequenceEqual(oneItemDefaultOrdered));
                Assert.IsFalse(emptyOrdered.SequenceEqual(oneItemDefaultOrdered, new _IntComparer()));
                Assert.IsFalse(emptyOrdered.SequenceEqual(oneItemSpecificOrdered));
                Assert.IsFalse(emptyOrdered.SequenceEqual(oneItemSpecificOrdered, new _IntComparer()));

                Helper.ForEachEnumerableExpression(
                    emptyOrdered,
                    new int[0],
                    res => { },
                    @"(a, b) =>
                      {
                        Assert.IsTrue(a.SequenceEqual(b));
                        Assert.IsTrue(a.SequenceEqual(b, new SequenceEqualTests._IntComparer()));

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
                Assert.IsFalse(groupByDefault.SequenceEqual(emptyGroupingInt));
                Assert.IsFalse(groupByDefault.SequenceEqual(emptyGroupingInt, new _GroupingComparer<int>()));
                var emptyGroupingIntOrdered = emptyGroupingInt.OrderBy(x => x);
                Assert.IsFalse(groupByDefault.SequenceEqual(emptyGroupingIntOrdered));
                Assert.IsFalse(groupByDefault.SequenceEqual(emptyGroupingIntOrdered, new _GroupingComparer<int>()));
                Assert.IsFalse(groupByDefault.SequenceEqual(groupByDefault));
                Assert.IsTrue(groupByDefault.SequenceEqual(groupByDefault, new _GroupingComparer<int>()));
                var groupBySpecificInt = new[] { 1, 1, 2, 2, 3, 3 }.GroupBy(x => x, new _IntComparer());
                Assert.IsFalse(groupByDefault.SequenceEqual(groupBySpecificInt));
                Assert.IsTrue(groupByDefault.SequenceEqual(groupBySpecificInt, new _GroupingComparer<int>()));
                Assert.IsFalse(groupByDefault.SequenceEqual(lookup));
                Assert.IsTrue(groupByDefault.SequenceEqual(lookup, new _GroupingComparer<int>()));
                // range is non-sensical
                var repeatGroupingInt = Enumerable.Repeat(groupByDefault.First(), 1);
                Assert.IsFalse(groupByDefault.SequenceEqual(repeatGroupingInt));
                Assert.IsFalse(groupByDefault.SequenceEqual(repeatGroupingInt, new _GroupingComparer<int>()));
                // reverseRange is non-sensical

                var orderBy = new[] { groupByDefault.First() }.OrderBy(x => x.Key);
                Assert.IsFalse(groupByDefault.SequenceEqual(orderBy));
                Assert.IsFalse(groupByDefault.SequenceEqual(orderBy, new _GroupingComparer<int>()));

                var oneItemDefaultGrouping = Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty();
                Assert.IsFalse(groupByDefault.SequenceEqual(oneItemDefaultGrouping));
                Assert.IsFalse(groupByDefault.SequenceEqual(oneItemDefaultGrouping, new _GroupingComparer<int>()));
                var oneItemSpecificGrouping = Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty(new[] { 1 }.GroupBy(x => x).First());
                Assert.IsFalse(groupByDefault.SequenceEqual(oneItemSpecificGrouping));
                Assert.IsFalse(groupByDefault.SequenceEqual(oneItemSpecificGrouping, new _GroupingComparer<int>()));
                var oneItemDefaultOrderedGrouping = oneItemDefaultGrouping.OrderBy(x => x);
                Assert.IsFalse(groupByDefault.SequenceEqual(oneItemDefaultOrderedGrouping));
                Assert.IsFalse(groupByDefault.SequenceEqual(oneItemDefaultOrderedGrouping, new _GroupingComparer<int>()));
                var oneItemSpecificOrderedGrouping = oneItemSpecificGrouping.OrderBy(x => x);
                Assert.IsFalse(groupByDefault.SequenceEqual(oneItemSpecificOrderedGrouping));
                Assert.IsFalse(groupByDefault.SequenceEqual(oneItemSpecificOrderedGrouping, new _GroupingComparer<int>()));

                Helper.ForEachEnumerableExpression(
                    groupByDefault,
                    new[] { groupByDefault.First() },
                    res => { },
                    @"(a, b) =>
                      {
                        Assert.IsFalse(a.SequenceEqual(b));
                        Assert.IsFalse(a.SequenceEqual(b, new SequenceEqualTests._GroupingComparer<int>()));

                        return Helper.NoCallValue;
                      }",
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>),
                    typeof(OrderByEnumerable<,,,,>)
                );
            }

            // groupBySpecific
            {
                var emptyGroupingString = Enumerable.Empty<GroupingEnumerable<string, string>>();
                Assert.IsFalse(groupBySpecific.SequenceEqual(emptyGroupingString));
                Assert.IsFalse(groupBySpecific.SequenceEqual(emptyGroupingString, new _GroupingComparer<string>()));
                var emptyGroupingStringOrdered = emptyGroupingString.OrderBy(x => x);
                Assert.IsFalse(groupBySpecific.SequenceEqual(emptyGroupingStringOrdered));
                Assert.IsFalse(groupBySpecific.SequenceEqual(emptyGroupingStringOrdered, new _GroupingComparer<string>()));
                var groupByDefaultString = new[] { "hello", "HELLO", "world", "WORLD", "foo", "FOO" }.GroupBy(x => x);
                Assert.IsFalse(groupBySpecific.SequenceEqual(groupByDefaultString));
                Assert.IsFalse(groupBySpecific.SequenceEqual(groupByDefaultString, new _GroupingComparer<string>()));
                Assert.IsFalse(groupBySpecific.SequenceEqual(groupBySpecific));
                Assert.IsTrue(groupBySpecific.SequenceEqual(groupBySpecific, new _GroupingComparer<string>()));
                var lookupString = new[] { "hello", "HELLO", "world", "WORLD", "foo", "FOO" }.ToLookup(x => x, StringComparer.OrdinalIgnoreCase);
                Assert.IsFalse(groupBySpecific.SequenceEqual(lookupString));
                Assert.IsTrue(groupBySpecific.SequenceEqual(lookupString, new _GroupingComparer<string>()));
                // range is non-sensical
                var repeatGroupingString = Enumerable.Repeat(groupBySpecific.First(), 1);
                Assert.IsFalse(groupBySpecific.SequenceEqual(repeatGroupingString));
                Assert.IsFalse(groupBySpecific.SequenceEqual(repeatGroupingString, new _GroupingComparer<string>()));
                // reverseRange is non-sensical

                var orderBy = new[] { groupBySpecific.First() }.OrderBy(x => x.Key);
                Assert.IsFalse(groupBySpecific.SequenceEqual(orderBy));
                Assert.IsFalse(groupBySpecific.SequenceEqual(orderBy, new _GroupingComparer<string>()));

                var oneItemDefaultGrouping = Enumerable.Empty<GroupingEnumerable<string, string>>().DefaultIfEmpty();
                Assert.IsFalse(groupBySpecific.SequenceEqual(oneItemDefaultGrouping));
                Assert.IsFalse(groupBySpecific.SequenceEqual(oneItemDefaultGrouping, new _GroupingComparer<string>()));
                var oneItemSpecificGrouping = Enumerable.Empty<GroupingEnumerable<string, string>>().DefaultIfEmpty(new[] { "blah" }.GroupBy(x => x).First());
                Assert.IsFalse(groupBySpecific.SequenceEqual(oneItemSpecificGrouping));
                Assert.IsFalse(groupBySpecific.SequenceEqual(oneItemSpecificGrouping, new _GroupingComparer<string>()));
                var oneItemDefaultOrderedGrouping = oneItemDefaultGrouping.OrderBy(x => x);
                Assert.IsFalse(groupBySpecific.SequenceEqual(oneItemDefaultOrderedGrouping));
                Assert.IsFalse(groupBySpecific.SequenceEqual(oneItemDefaultOrderedGrouping, new _GroupingComparer<string>()));
                var oneItemSpecificOrderedGrouping = oneItemSpecificGrouping.OrderBy(x => x);
                Assert.IsFalse(groupBySpecific.SequenceEqual(oneItemSpecificOrderedGrouping));
                Assert.IsFalse(groupBySpecific.SequenceEqual(oneItemSpecificOrderedGrouping, new _GroupingComparer<string>()));

                Helper.ForEachEnumerableExpression(
                    groupBySpecific,
                    new[] { groupBySpecific.First() },
                    res => { },
                    @"(a, b) =>
                      {
                        Assert.IsFalse(a.SequenceEqual(b));
                        Assert.IsFalse(a.SequenceEqual(b, new SequenceEqualTests._GroupingComparer<string>()));

                        return Helper.NoCallValue;
                      }",
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>),
                    typeof(OrderByEnumerable<,,,,>)
                );
            }

            // lookup
            {
                var emptyGroupingInt = Enumerable.Empty<GroupingEnumerable<int, int>>();
                Assert.IsFalse(lookup.SequenceEqual(emptyGroupingInt));
                Assert.IsFalse(lookup.SequenceEqual(emptyGroupingInt, new _GroupingComparer<int>()));
                var emptyGroupingIntOrdered = emptyGroupingInt.OrderBy(x => x);
                Assert.IsFalse(lookup.SequenceEqual(emptyGroupingIntOrdered));
                Assert.IsFalse(lookup.SequenceEqual(emptyGroupingIntOrdered, new _GroupingComparer<int>()));
                Assert.IsFalse(lookup.SequenceEqual(groupByDefault));
                Assert.IsTrue(lookup.SequenceEqual(groupByDefault, new _GroupingComparer<int>()));
                var groupBySpecificInt = new[] { 1, 1, 2, 2, 3, 3 }.GroupBy(x => x, new _IntComparer());
                Assert.IsFalse(lookup.SequenceEqual(groupBySpecificInt));
                Assert.IsTrue(lookup.SequenceEqual(groupBySpecificInt, new _GroupingComparer<int>()));
                Assert.IsFalse(lookup.SequenceEqual(lookup));
                Assert.IsTrue(lookup.SequenceEqual(lookup, new _GroupingComparer<int>()));
                // range is non-sensical
                var repeatGroupingInt = Enumerable.Repeat(lookup.First(), 1);
                Assert.IsFalse(lookup.SequenceEqual(repeatGroupingInt));
                Assert.IsFalse(lookup.SequenceEqual(repeatGroupingInt, new _GroupingComparer<int>()));
                // reverseRange is non-sensical

                var orderBy = new[] { lookup.First() }.OrderBy(x => x.Key);
                Assert.IsFalse(lookup.SequenceEqual(orderBy));
                Assert.IsFalse(lookup.SequenceEqual(orderBy, new _GroupingComparer<int>()));

                var oneItemDefaultGrouping = Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty();
                Assert.IsFalse(lookup.SequenceEqual(oneItemDefaultGrouping));
                Assert.IsFalse(lookup.SequenceEqual(oneItemDefaultGrouping, new _GroupingComparer<int>()));
                var oneItemSpecificGrouping = Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty(new[] { 1 }.GroupBy(x => x).First());
                Assert.IsFalse(lookup.SequenceEqual(oneItemSpecificGrouping));
                Assert.IsFalse(lookup.SequenceEqual(oneItemSpecificGrouping, new _GroupingComparer<int>()));
                var oneItemDefaultOrderedGrouping = oneItemDefaultGrouping.OrderBy(x => x);
                Assert.IsFalse(lookup.SequenceEqual(oneItemDefaultOrderedGrouping));
                Assert.IsFalse(lookup.SequenceEqual(oneItemDefaultOrderedGrouping, new _GroupingComparer<int>()));
                var oneItemSpecificOrderedGrouping = oneItemSpecificGrouping.OrderBy(x => x);
                Assert.IsFalse(lookup.SequenceEqual(oneItemSpecificOrderedGrouping));
                Assert.IsFalse(lookup.SequenceEqual(oneItemSpecificOrderedGrouping, new _GroupingComparer<int>()));

                Helper.ForEachEnumerableExpression(
                    lookup,
                    new[] { lookup.First() },
                    res => { },
                    @"(a, b) =>
                      {
                        Assert.IsFalse(a.SequenceEqual(b));
                        Assert.IsFalse(a.SequenceEqual(b, new SequenceEqualTests._GroupingComparer<int>()));

                        return Helper.NoCallValue;
                      }",
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>),
                    typeof(OrderByEnumerable<,,,,>)
                );
            }

            // range
            {
                Assert.IsFalse(range.SequenceEqual(empty));
                Assert.IsFalse(range.SequenceEqual(empty, new _IntComparer()));
                Assert.IsFalse(range.SequenceEqual(emptyOrdered));
                Assert.IsFalse(range.SequenceEqual(emptyOrdered, new _IntComparer()));
                // groupby & lookup are non-sensical
                Assert.IsTrue(range.SequenceEqual(range));
                Assert.IsTrue(range.SequenceEqual(range, new _IntComparer()));
                var repeatInt = Enumerable.Repeat(1, 1);
                Assert.IsFalse(range.SequenceEqual(repeatInt));
                Assert.IsFalse(range.SequenceEqual(repeatInt, new _IntComparer()));
                Assert.IsFalse(range.SequenceEqual(reverseRange));
                Assert.IsFalse(range.SequenceEqual(reverseRange, new _IntComparer()));

                Assert.IsFalse(range.SequenceEqual(oneItemDefault));
                Assert.IsFalse(range.SequenceEqual(oneItemDefault, new _IntComparer()));
                Assert.IsFalse(range.SequenceEqual(oneItemSpecific));
                Assert.IsFalse(range.SequenceEqual(oneItemSpecific, new _IntComparer()));
                Assert.IsFalse(range.SequenceEqual(oneItemDefaultOrdered));
                Assert.IsFalse(range.SequenceEqual(oneItemDefaultOrdered, new _IntComparer()));
                Assert.IsFalse(range.SequenceEqual(oneItemSpecificOrdered));
                Assert.IsFalse(range.SequenceEqual(oneItemSpecificOrdered, new _IntComparer()));

                Helper.ForEachEnumerableExpression(
                    range,
                    new int[0],
                    res => { },
                    @"(a, b) =>
                      {
                        Assert.IsFalse(a.SequenceEqual(b));
                        Assert.IsFalse(a.SequenceEqual(b, new SequenceEqualTests._IntComparer()));

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
                Assert.IsFalse(repeat.SequenceEqual(emptyString));
                Assert.IsFalse(repeat.SequenceEqual(emptyString, StringComparer.InvariantCultureIgnoreCase));
                var emptyOrderedString = emptyString.OrderBy(x => x);
                Assert.IsFalse(repeat.SequenceEqual(emptyOrderedString));
                Assert.IsFalse(repeat.SequenceEqual(emptyOrderedString, StringComparer.InvariantCultureIgnoreCase));
                var groupByDefaultString = new[] { "hello", "HELLO", "world", "WORLD", "foo", "FOO" }.GroupBy(x => x);
                var repeatGroupingString = Enumerable.Repeat(groupByDefaultString.First(), 1);
                Assert.IsFalse(repeatGroupingString.SequenceEqual(groupByDefaultString));
                Assert.IsFalse(repeatGroupingString.SequenceEqual(groupByDefaultString, new _GroupingComparer<string>()));
                Assert.IsFalse(repeatGroupingString.SequenceEqual(groupBySpecific));
                Assert.IsFalse(repeatGroupingString.SequenceEqual(groupBySpecific, new _GroupingComparer<string>()));
                var lookupString = new[] { "hello", "HELLO", "world", "WORLD", "foo", "FOO" }.ToLookup(x => x);
                Assert.IsFalse(repeatGroupingString.SequenceEqual(lookupString));
                Assert.IsFalse(repeatGroupingString.SequenceEqual(lookupString, new _GroupingComparer<string>()));
                var repeatInt = Enumerable.Repeat(1, 1);
                Assert.IsTrue(repeatInt.SequenceEqual(range));
                Assert.IsTrue(repeatInt.SequenceEqual(range, new _IntComparer()));
                Assert.IsTrue(repeat.SequenceEqual(repeat));
                Assert.IsTrue(repeat.SequenceEqual(repeat, StringComparer.InvariantCultureIgnoreCase));
                Assert.IsFalse(repeatInt.SequenceEqual(reverseRange));
                Assert.IsFalse(repeatInt.SequenceEqual(reverseRange, new _IntComparer()));

                Assert.IsFalse(repeatInt.SequenceEqual(oneItemDefault));
                Assert.IsFalse(repeatInt.SequenceEqual(oneItemDefault, new _IntComparer()));
                Assert.IsFalse(repeatInt.SequenceEqual(oneItemSpecific));
                Assert.IsFalse(repeatInt.SequenceEqual(oneItemSpecific, new _IntComparer()));
                Assert.IsFalse(repeatInt.SequenceEqual(oneItemDefaultOrdered));
                Assert.IsFalse(repeatInt.SequenceEqual(oneItemDefaultOrdered, new _IntComparer()));
                Assert.IsFalse(repeatInt.SequenceEqual(oneItemSpecificOrdered));
                Assert.IsFalse(repeatInt.SequenceEqual(oneItemSpecificOrdered, new _IntComparer()));

                Helper.ForEachEnumerableExpression(
                    repeat,
                    new string[0],
                    res => { },
                    @"(a, b) =>
                      {
                        Assert.IsFalse(a.SequenceEqual(b));
                        Assert.IsFalse(a.SequenceEqual(b, StringComparer.InvariantCultureIgnoreCase));

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
                Assert.IsFalse(reverseRange.SequenceEqual(empty));
                Assert.IsFalse(reverseRange.SequenceEqual(empty, new _IntComparer()));
                Assert.IsFalse(reverseRange.SequenceEqual(emptyOrdered));
                Assert.IsFalse(reverseRange.SequenceEqual(emptyOrdered, new _IntComparer()));
                // groupby & lookup are non-sensical
                Assert.IsFalse(reverseRange.SequenceEqual(range));
                Assert.IsFalse(reverseRange.SequenceEqual(range, new _IntComparer()));
                var repeatInt = Enumerable.Repeat(1, 1);
                Assert.IsFalse(reverseRange.SequenceEqual(repeatInt));
                Assert.IsFalse(reverseRange.SequenceEqual(repeatInt, new _IntComparer()));
                Assert.IsTrue(reverseRange.SequenceEqual(reverseRange));
                Assert.IsTrue(reverseRange.SequenceEqual(reverseRange, new _IntComparer()));

                Assert.IsFalse(reverseRange.SequenceEqual(oneItemDefault));
                Assert.IsFalse(reverseRange.SequenceEqual(oneItemDefault, new _IntComparer()));
                Assert.IsFalse(reverseRange.SequenceEqual(oneItemSpecific));
                Assert.IsFalse(reverseRange.SequenceEqual(oneItemSpecific, new _IntComparer()));
                Assert.IsFalse(reverseRange.SequenceEqual(oneItemDefaultOrdered));
                Assert.IsFalse(reverseRange.SequenceEqual(oneItemDefaultOrdered, new _IntComparer()));
                Assert.IsFalse(reverseRange.SequenceEqual(oneItemSpecificOrdered));
                Assert.IsFalse(reverseRange.SequenceEqual(oneItemSpecificOrdered, new _IntComparer()));

                Helper.ForEachEnumerableExpression(
                    reverseRange,
                    new int[0],
                    res => { },
                    @"(a, b) =>
                      {
                        Assert.IsFalse(a.SequenceEqual(b));
                        Assert.IsFalse(a.SequenceEqual(b, new SequenceEqualTests._IntComparer()));

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
                Assert.IsFalse(oneItemDefault.SequenceEqual(empty));
                Assert.IsFalse(oneItemDefault.SequenceEqual(empty, new _IntComparer()));
                Assert.IsFalse(oneItemDefault.SequenceEqual(emptyOrdered));
                Assert.IsFalse(oneItemDefault.SequenceEqual(emptyOrdered, new _IntComparer()));
                // groupby & lookup are non-sensical
                Assert.IsFalse(oneItemDefault.SequenceEqual(range));
                Assert.IsFalse(oneItemDefault.SequenceEqual(range, new _IntComparer()));
                var repeatInt = Enumerable.Repeat(1, 1);
                Assert.IsFalse(oneItemDefault.SequenceEqual(repeatInt));
                Assert.IsFalse(oneItemDefault.SequenceEqual(repeatInt, new _IntComparer()));
                Assert.IsFalse(oneItemDefault.SequenceEqual(reverseRange));
                Assert.IsFalse(oneItemDefault.SequenceEqual(reverseRange, new _IntComparer()));

                Assert.IsTrue(oneItemDefault.SequenceEqual(oneItemDefault));
                Assert.IsTrue(oneItemDefault.SequenceEqual(oneItemDefault, new _IntComparer()));
                Assert.IsFalse(oneItemDefault.SequenceEqual(oneItemSpecific));
                Assert.IsFalse(oneItemDefault.SequenceEqual(oneItemSpecific, new _IntComparer()));
                Assert.IsTrue(oneItemDefault.SequenceEqual(oneItemDefaultOrdered));
                Assert.IsTrue(oneItemDefault.SequenceEqual(oneItemDefaultOrdered, new _IntComparer()));
                Assert.IsFalse(oneItemDefault.SequenceEqual(oneItemSpecificOrdered));
                Assert.IsFalse(oneItemDefault.SequenceEqual(oneItemSpecificOrdered, new _IntComparer()));

                Helper.ForEachEnumerableExpression(
                    oneItemDefault,
                    new[] { 4 },
                    res => { },
                    @"(a, b) =>
                      {
                        Assert.IsFalse(a.SequenceEqual(b));
                        Assert.IsFalse(a.SequenceEqual(b, new SequenceEqualTests._IntComparer()));

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
                Assert.IsFalse(oneItemSpecific.SequenceEqual(empty));
                Assert.IsFalse(oneItemSpecific.SequenceEqual(empty, new _IntComparer()));
                Assert.IsFalse(oneItemSpecific.SequenceEqual(emptyOrdered));
                Assert.IsFalse(oneItemSpecific.SequenceEqual(emptyOrdered, new _IntComparer()));
                // groupby & lookup are non-sensical
                Assert.IsFalse(oneItemSpecific.SequenceEqual(range));
                Assert.IsFalse(oneItemSpecific.SequenceEqual(range, new _IntComparer()));
                var repeatInt = Enumerable.Repeat(1, 1);
                Assert.IsFalse(oneItemSpecific.SequenceEqual(repeatInt));
                Assert.IsFalse(oneItemSpecific.SequenceEqual(repeatInt, new _IntComparer()));
                Assert.IsFalse(oneItemSpecific.SequenceEqual(reverseRange));
                Assert.IsFalse(oneItemSpecific.SequenceEqual(reverseRange, new _IntComparer()));

                Assert.IsFalse(oneItemSpecific.SequenceEqual(oneItemDefault));
                Assert.IsFalse(oneItemSpecific.SequenceEqual(oneItemDefault, new _IntComparer()));
                Assert.IsTrue(oneItemSpecific.SequenceEqual(oneItemSpecific));
                Assert.IsTrue(oneItemSpecific.SequenceEqual(oneItemSpecific, new _IntComparer()));
                Assert.IsFalse(oneItemSpecific.SequenceEqual(oneItemDefaultOrdered));
                Assert.IsFalse(oneItemSpecific.SequenceEqual(oneItemDefaultOrdered, new _IntComparer()));
                Assert.IsTrue(oneItemSpecific.SequenceEqual(oneItemSpecificOrdered));
                Assert.IsTrue(oneItemSpecific.SequenceEqual(oneItemSpecificOrdered, new _IntComparer()));

                Helper.ForEachEnumerableExpression(
                    oneItemSpecific,
                    new[] { 10 },
                    res => { },
                    @"(a, b) =>
                      {
                        Assert.IsFalse(a.SequenceEqual(b));
                        Assert.IsFalse(a.SequenceEqual(b, new SequenceEqualTests._IntComparer()));

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
                Assert.IsFalse(oneItemDefaultOrdered.SequenceEqual(empty));
                Assert.IsFalse(oneItemDefaultOrdered.SequenceEqual(empty, new _IntComparer()));
                Assert.IsFalse(oneItemDefaultOrdered.SequenceEqual(emptyOrdered));
                Assert.IsFalse(oneItemDefaultOrdered.SequenceEqual(emptyOrdered, new _IntComparer()));
                // groupby & lookup are non-sensical
                Assert.IsFalse(oneItemDefaultOrdered.SequenceEqual(range));
                Assert.IsFalse(oneItemDefaultOrdered.SequenceEqual(range, new _IntComparer()));
                var repeatInt = Enumerable.Repeat(1, 1);
                Assert.IsFalse(oneItemDefaultOrdered.SequenceEqual(repeatInt));
                Assert.IsFalse(oneItemDefaultOrdered.SequenceEqual(repeatInt, new _IntComparer()));
                Assert.IsFalse(oneItemDefaultOrdered.SequenceEqual(reverseRange));
                Assert.IsFalse(oneItemDefaultOrdered.SequenceEqual(reverseRange, new _IntComparer()));

                Assert.IsTrue(oneItemDefaultOrdered.SequenceEqual(oneItemDefault));
                Assert.IsTrue(oneItemDefaultOrdered.SequenceEqual(oneItemDefault, new _IntComparer()));
                Assert.IsFalse(oneItemDefaultOrdered.SequenceEqual(oneItemSpecific));
                Assert.IsFalse(oneItemDefaultOrdered.SequenceEqual(oneItemSpecific, new _IntComparer()));
                Assert.IsTrue(oneItemDefaultOrdered.SequenceEqual(oneItemDefaultOrdered));
                Assert.IsTrue(oneItemDefaultOrdered.SequenceEqual(oneItemDefaultOrdered, new _IntComparer()));
                Assert.IsFalse(oneItemDefaultOrdered.SequenceEqual(oneItemSpecificOrdered));
                Assert.IsFalse(oneItemDefaultOrdered.SequenceEqual(oneItemSpecificOrdered, new _IntComparer()));

                Helper.ForEachEnumerableExpression(
                    oneItemDefaultOrdered,
                    new[] { 4 },
                    res => { },
                    @"(a, b) =>
                      {
                        Assert.IsFalse(a.SequenceEqual(b));
                        Assert.IsFalse(a.SequenceEqual(b, new SequenceEqualTests._IntComparer()));

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
                Assert.IsFalse(oneItemSpecificOrdered.SequenceEqual(empty));
                Assert.IsFalse(oneItemSpecificOrdered.SequenceEqual(empty, new _IntComparer()));
                Assert.IsFalse(oneItemSpecificOrdered.SequenceEqual(emptyOrdered));
                Assert.IsFalse(oneItemSpecificOrdered.SequenceEqual(emptyOrdered, new _IntComparer()));
                // groupby & lookup are non-sensical
                Assert.IsFalse(oneItemSpecificOrdered.SequenceEqual(range));
                Assert.IsFalse(oneItemSpecificOrdered.SequenceEqual(range, new _IntComparer()));
                var repeatInt = Enumerable.Repeat(1, 1);
                Assert.IsFalse(oneItemSpecificOrdered.SequenceEqual(repeatInt));
                Assert.IsFalse(oneItemSpecificOrdered.SequenceEqual(repeatInt, new _IntComparer()));
                Assert.IsFalse(oneItemSpecificOrdered.SequenceEqual(reverseRange));
                Assert.IsFalse(oneItemSpecificOrdered.SequenceEqual(reverseRange, new _IntComparer()));

                Assert.IsFalse(oneItemSpecificOrdered.SequenceEqual(oneItemDefault));
                Assert.IsFalse(oneItemSpecificOrdered.SequenceEqual(oneItemDefault, new _IntComparer()));
                Assert.IsTrue(oneItemSpecificOrdered.SequenceEqual(oneItemSpecific));
                Assert.IsTrue(oneItemSpecificOrdered.SequenceEqual(oneItemSpecific, new _IntComparer()));
                Assert.IsFalse(oneItemSpecificOrdered.SequenceEqual(oneItemDefaultOrdered));
                Assert.IsFalse(oneItemSpecificOrdered.SequenceEqual(oneItemDefaultOrdered, new _IntComparer()));
                Assert.IsTrue(oneItemSpecificOrdered.SequenceEqual(oneItemSpecificOrdered));
                Assert.IsTrue(oneItemSpecificOrdered.SequenceEqual(oneItemSpecificOrdered, new _IntComparer()));

                Helper.ForEachEnumerableExpression(
                    oneItemSpecificOrdered,
                    new[] { 10 },
                    res => { },
                    @"(a, b) =>
                      {
                        Assert.IsFalse(a.SequenceEqual(b));
                        Assert.IsFalse(a.SequenceEqual(b, new SequenceEqualTests._IntComparer()));

                        return Helper.NoCallValue;
                      }",
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
                                var _ = a.SequenceEqual(b);
                                Assert.Fail();
                            }
                            catch(ArgumentException exc)
                            {
                                Assert.AreEqual(""""first"""", exc.ParamName);
                            }

                            try
                            {
                                var _ = b.SequenceEqual(a);
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

        [TestMethod]
        public void Malformed_Specific()
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
                                var _ = a.SequenceEqual(b, StringComparer.InvariantCultureIgnoreCase);
                                Assert.Fail();
                            }
                            catch(ArgumentException exc)
                            {
                                Assert.AreEqual(""""first"""", exc.ParamName);
                            }

                            try
                            {
                                var _ = b.SequenceEqual(a, StringComparer.InvariantCultureIgnoreCase);
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
                var emptyGood = Enumerable.Empty<int>();
                try { empty.SequenceEqual(emptyGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { empty.SequenceEqual(emptyGood, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { emptyGood.SequenceEqual(empty); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { emptyGood.SequenceEqual(empty, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var emptyOrderedGood = emptyGood.OrderBy(x => x);
                try { empty.SequenceEqual(emptyOrderedGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { empty.SequenceEqual(emptyOrderedGood, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { emptyOrderedGood.SequenceEqual(empty); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { emptyOrderedGood.SequenceEqual(empty, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var emptyGroupingBad = new EmptyEnumerable<GroupingEnumerable<int, int>>();
                var groupByDefaultGood = new[] { 1 }.GroupBy(x => x);
                try { emptyGroupingBad.SequenceEqual(groupByDefaultGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { emptyGroupingBad.SequenceEqual(groupByDefaultGood, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { groupByDefaultGood.SequenceEqual(emptyGroupingBad); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { groupByDefaultGood.SequenceEqual(emptyGroupingBad, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var groupBySpecificGood = new[] { 1 }.GroupBy(x => x, new _IntComparer());
                try { emptyGroupingBad.SequenceEqual(groupBySpecificGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { emptyGroupingBad.SequenceEqual(groupBySpecificGood, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { groupBySpecificGood.SequenceEqual(emptyGroupingBad); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { groupBySpecificGood.SequenceEqual(emptyGroupingBad, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var lookupGood = new[] { 1 }.ToLookup(x => x);
                try { emptyGroupingBad.SequenceEqual(lookupGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { emptyGroupingBad.SequenceEqual(lookupGood, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { lookupGood.SequenceEqual(emptyGroupingBad); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { lookupGood.SequenceEqual(emptyGroupingBad, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var rangeGood = Enumerable.Range(1, 1);
                try { empty.SequenceEqual(rangeGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { empty.SequenceEqual(rangeGood, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { rangeGood.SequenceEqual(empty); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { rangeGood.SequenceEqual(empty, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var repeatGood = Enumerable.Repeat(1, 1);
                try { empty.SequenceEqual(repeatGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { empty.SequenceEqual(repeatGood, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { repeatGood.SequenceEqual(empty); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { repeatGood.SequenceEqual(empty, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var reverseRangeGood = rangeGood.Reverse();
                try { empty.SequenceEqual(reverseRangeGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { empty.SequenceEqual(reverseRangeGood, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { reverseRangeGood.SequenceEqual(empty); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { reverseRangeGood.SequenceEqual(empty, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                var oneItemDefaultGood = Enumerable.Empty<int>().DefaultIfEmpty();
                try { empty.SequenceEqual(oneItemDefaultGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { empty.SequenceEqual(oneItemDefaultGood, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemDefaultGood.SequenceEqual(empty); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemDefaultGood.SequenceEqual(empty, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var oneItemSpecificGood = Enumerable.Empty<int>().DefaultIfEmpty(4);
                try { empty.SequenceEqual(oneItemSpecificGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { empty.SequenceEqual(oneItemSpecificGood, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemSpecificGood.SequenceEqual(empty); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemSpecificGood.SequenceEqual(empty, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var oneItemDefaultOrderedGood = oneItemDefaultGood.OrderBy(x => x);
                try { empty.SequenceEqual(oneItemDefaultOrderedGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { empty.SequenceEqual(oneItemDefaultOrderedGood, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemDefaultOrderedGood.SequenceEqual(empty); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemDefaultOrderedGood.SequenceEqual(empty, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var oneItemSpecificOrderedGood = oneItemSpecificGood.OrderBy(x => x);
                try { empty.SequenceEqual(oneItemSpecificOrderedGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { empty.SequenceEqual(oneItemSpecificOrderedGood, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemSpecificOrderedGood.SequenceEqual(empty); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemSpecificOrderedGood.SequenceEqual(empty, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                Helper.ForEachEnumerableExpression(
                    empty,
                    new int[0],
                    res => { },
                    @"(a, b) =>
                      {
                        try { a.SequenceEqual(b); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""first"", exc.ParamName); }
                        try { a.SequenceEqual(b, new SequenceEqualTests._IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""first"", exc.ParamName); }
                        try { b.SequenceEqual(a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""second"", exc.ParamName); }
                        try { b.SequenceEqual(a, new SequenceEqualTests._IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""second"", exc.ParamName); }

                        return Helper.NoCallValue;
                      }
                    ",
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );
            }

            // emptyOrdered
            {
                var emptyGood = Enumerable.Empty<int>();
                try { emptyOrdered.SequenceEqual(emptyGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { emptyOrdered.SequenceEqual(emptyGood, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { emptyGood.SequenceEqual(emptyOrdered); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { emptyGood.SequenceEqual(emptyOrdered, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var emptyOrderedGood = emptyGood.OrderBy(x => x);
                try { emptyOrdered.SequenceEqual(emptyOrderedGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { emptyOrdered.SequenceEqual(emptyOrderedGood, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { emptyOrderedGood.SequenceEqual(emptyOrdered); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { emptyOrderedGood.SequenceEqual(emptyOrdered, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var emptyOrderedGroupingBad = new EmptyOrderedEnumerable<GroupingEnumerable<int, int>>();
                var groupByDefaultGood = new[] { 1 }.GroupBy(x => x);
                try { emptyOrderedGroupingBad.SequenceEqual(groupByDefaultGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { emptyOrderedGroupingBad.SequenceEqual(groupByDefaultGood, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { groupByDefaultGood.SequenceEqual(emptyOrderedGroupingBad); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { groupByDefaultGood.SequenceEqual(emptyOrderedGroupingBad, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var groupBySpecificGood = new[] { 1 }.GroupBy(x => x, new _IntComparer());
                try { emptyOrderedGroupingBad.SequenceEqual(groupBySpecificGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { emptyOrderedGroupingBad.SequenceEqual(groupBySpecificGood, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { groupBySpecificGood.SequenceEqual(emptyOrderedGroupingBad); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { groupBySpecificGood.SequenceEqual(emptyOrderedGroupingBad, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var lookupGood = new[] { 1 }.ToLookup(x => x);
                try { emptyOrderedGroupingBad.SequenceEqual(lookupGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { emptyOrderedGroupingBad.SequenceEqual(lookupGood, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { lookupGood.SequenceEqual(emptyOrderedGroupingBad); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { lookupGood.SequenceEqual(emptyOrderedGroupingBad, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var rangeGood = Enumerable.Range(1, 1);
                try { emptyOrdered.SequenceEqual(rangeGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { emptyOrdered.SequenceEqual(rangeGood, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { rangeGood.SequenceEqual(emptyOrdered); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { rangeGood.SequenceEqual(emptyOrdered, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var repeatGood = Enumerable.Repeat(1, 1);
                try { emptyOrdered.SequenceEqual(repeatGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { emptyOrdered.SequenceEqual(repeatGood, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { repeatGood.SequenceEqual(emptyOrdered); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { repeatGood.SequenceEqual(emptyOrdered, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var reverseRangeGood = rangeGood.Reverse();
                try { emptyOrdered.SequenceEqual(reverseRangeGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { emptyOrdered.SequenceEqual(reverseRangeGood, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { reverseRangeGood.SequenceEqual(emptyOrdered); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { reverseRangeGood.SequenceEqual(emptyOrdered, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                var oneItemDefaultGood = Enumerable.Empty<int>().DefaultIfEmpty();
                try { emptyOrdered.SequenceEqual(oneItemDefaultGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { emptyOrdered.SequenceEqual(oneItemDefaultGood, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemDefaultGood.SequenceEqual(emptyOrdered); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemDefaultGood.SequenceEqual(emptyOrdered, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var oneItemSpecificGood = Enumerable.Empty<int>().DefaultIfEmpty(4);
                try { emptyOrdered.SequenceEqual(oneItemSpecificGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { emptyOrdered.SequenceEqual(oneItemSpecificGood, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemSpecificGood.SequenceEqual(emptyOrdered); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemSpecificGood.SequenceEqual(emptyOrdered, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var oneItemDefaultOrderedGood = oneItemDefaultGood.OrderBy(x => x);
                try { emptyOrdered.SequenceEqual(oneItemDefaultOrderedGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { emptyOrdered.SequenceEqual(oneItemDefaultOrderedGood, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemDefaultOrderedGood.SequenceEqual(emptyOrdered); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemDefaultOrderedGood.SequenceEqual(emptyOrdered, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var oneItemSpecificOrderedGood = oneItemSpecificGood.OrderBy(x => x);
                try { emptyOrdered.SequenceEqual(oneItemSpecificOrderedGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { emptyOrdered.SequenceEqual(oneItemSpecificOrderedGood, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemSpecificOrderedGood.SequenceEqual(emptyOrdered); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemSpecificOrderedGood.SequenceEqual(emptyOrdered, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                Helper.ForEachEnumerableExpression(
                    emptyOrdered,
                    new int[0],
                    res => { },
                    @"(a, b) =>
                      {
                        try { a.SequenceEqual(b); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""first"", exc.ParamName); }
                        try { a.SequenceEqual(b, new SequenceEqualTests._IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""first"", exc.ParamName); }
                        try { b.SequenceEqual(a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""second"", exc.ParamName); }
                        try { b.SequenceEqual(a, new SequenceEqualTests._IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""second"", exc.ParamName); }

                        return Helper.NoCallValue;
                      }
                    ",
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );
            }

            // groupByDefault
            {
                var emptyGroupingGood = Enumerable.Empty<GroupingEnumerable<int, int>>();
                try { groupByDefault.SequenceEqual(emptyGroupingGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { groupByDefault.SequenceEqual(emptyGroupingGood, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { emptyGroupingGood.SequenceEqual(groupByDefault); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { emptyGroupingGood.SequenceEqual(groupByDefault, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var emptyGroupingGoodOrdered = emptyGroupingGood.OrderBy(x => x);
                try { groupByDefault.SequenceEqual(emptyGroupingGoodOrdered); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { groupByDefault.SequenceEqual(emptyGroupingGoodOrdered, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { emptyGroupingGoodOrdered.SequenceEqual(groupByDefault); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { emptyGroupingGoodOrdered.SequenceEqual(groupByDefault, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var groupByDefaultGood = new[] { 1 }.GroupBy(x => x);
                try { groupByDefault.SequenceEqual(groupByDefaultGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { groupByDefault.SequenceEqual(groupByDefaultGood, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { groupByDefaultGood.SequenceEqual(groupByDefault); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { groupByDefaultGood.SequenceEqual(groupByDefault, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var groupBySpecificGood = new[] { 1 }.GroupBy(x => x, new _IntComparer());
                try { groupByDefault.SequenceEqual(groupBySpecificGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { groupByDefault.SequenceEqual(groupBySpecificGood, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { groupBySpecificGood.SequenceEqual(groupByDefault); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { groupBySpecificGood.SequenceEqual(groupByDefault, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var lookupGood = new[] { 1 }.ToLookup(x => x);
                try { groupByDefault.SequenceEqual(lookupGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { groupByDefault.SequenceEqual(lookupGood, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { lookupGood.SequenceEqual(groupByDefault); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { lookupGood.SequenceEqual(groupByDefault, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                // range is non-sensical
                var repeatGood = Enumerable.Repeat(groupByDefaultGood.First(), 1);
                try { groupByDefault.SequenceEqual(repeatGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { groupByDefault.SequenceEqual(repeatGood, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { repeatGood.SequenceEqual(groupByDefault); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { repeatGood.SequenceEqual(groupByDefault, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                // reverseRange is non-sensical

                var oneItemDefaultGood = Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty();
                try { groupByDefault.SequenceEqual(oneItemDefaultGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { groupByDefault.SequenceEqual(oneItemDefaultGood, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemDefaultGood.SequenceEqual(groupByDefault); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemDefaultGood.SequenceEqual(groupByDefault, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var oneItemSpecificGood = Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty(new[] { 4 }.GroupBy(x => x).First());
                try { groupByDefault.SequenceEqual(oneItemSpecificGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { groupByDefault.SequenceEqual(oneItemSpecificGood, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemSpecificGood.SequenceEqual(groupByDefault); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemSpecificGood.SequenceEqual(groupByDefault, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var oneItemDefaultOrderedGood = oneItemDefaultGood.OrderBy(x => x);
                try { groupByDefault.SequenceEqual(oneItemDefaultOrderedGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { groupByDefault.SequenceEqual(oneItemDefaultOrderedGood, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemDefaultOrderedGood.SequenceEqual(groupByDefault); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemDefaultOrderedGood.SequenceEqual(groupByDefault, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var oneItemSpecificOrderedGood = oneItemSpecificGood.OrderBy(x => x);
                try { groupByDefault.SequenceEqual(oneItemSpecificOrderedGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { groupByDefault.SequenceEqual(oneItemSpecificOrderedGood, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemSpecificOrderedGood.SequenceEqual(groupByDefault); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemSpecificOrderedGood.SequenceEqual(groupByDefault, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                Helper.ForEachEnumerableExpression(
                    groupByDefault,
                    new[] { groupByDefaultGood.First() },
                    res => { },
                    @"(a, b) =>
                      {
                        try { a.SequenceEqual(b); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""first"", exc.ParamName); }
                        try { a.SequenceEqual(b, new SequenceEqualTests._GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""first"", exc.ParamName); }
                        try { b.SequenceEqual(a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""second"", exc.ParamName); }
                        try { b.SequenceEqual(a, new SequenceEqualTests._GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""second"", exc.ParamName); }

                        return Helper.NoCallValue;
                      }
                    ",
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );
            }

            // groupBySpecific
            {
                var emptyGroupingGood = Enumerable.Empty<GroupingEnumerable<int, int>>();
                try { groupBySpecific.SequenceEqual(emptyGroupingGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { groupBySpecific.SequenceEqual(emptyGroupingGood, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { emptyGroupingGood.SequenceEqual(groupBySpecific); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { emptyGroupingGood.SequenceEqual(groupBySpecific, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var emptyGroupingGoodOrdered = emptyGroupingGood.OrderBy(x => x);
                try { groupBySpecific.SequenceEqual(emptyGroupingGoodOrdered); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { groupBySpecific.SequenceEqual(emptyGroupingGoodOrdered, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { emptyGroupingGoodOrdered.SequenceEqual(groupBySpecific); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { emptyGroupingGoodOrdered.SequenceEqual(groupBySpecific, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var groupByDefaultGood = new[] { 1 }.GroupBy(x => x);
                try { groupBySpecific.SequenceEqual(groupByDefaultGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { groupBySpecific.SequenceEqual(groupByDefaultGood, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { groupByDefaultGood.SequenceEqual(groupBySpecific); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { groupByDefaultGood.SequenceEqual(groupBySpecific, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var groupBySpecificGood = new[] { 1 }.GroupBy(x => x, new _IntComparer());
                try { groupBySpecific.SequenceEqual(groupBySpecificGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { groupBySpecific.SequenceEqual(groupBySpecificGood, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { groupBySpecificGood.SequenceEqual(groupBySpecific); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { groupBySpecificGood.SequenceEqual(groupBySpecific, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var lookupGood = new[] { 1 }.ToLookup(x => x);
                try { groupBySpecific.SequenceEqual(lookupGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { groupBySpecific.SequenceEqual(lookupGood, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { lookupGood.SequenceEqual(groupBySpecific); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { lookupGood.SequenceEqual(groupBySpecific, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                // range is non-sensical
                var repeatGood = Enumerable.Repeat(groupByDefaultGood.First(), 1);
                try { groupBySpecific.SequenceEqual(repeatGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { groupBySpecific.SequenceEqual(repeatGood, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { repeatGood.SequenceEqual(groupBySpecific); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { repeatGood.SequenceEqual(groupBySpecific, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                // reverseRange is non-sensical

                var oneItemDefaultGood = Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty();
                try { groupBySpecific.SequenceEqual(oneItemDefaultGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { groupBySpecific.SequenceEqual(oneItemDefaultGood, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemDefaultGood.SequenceEqual(groupBySpecific); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemDefaultGood.SequenceEqual(groupBySpecific, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var oneItemSpecificGood = Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty(new[] { 4 }.GroupBy(x => x).First());
                try { groupBySpecific.SequenceEqual(oneItemSpecificGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { groupBySpecific.SequenceEqual(oneItemSpecificGood, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemSpecificGood.SequenceEqual(groupBySpecific); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemSpecificGood.SequenceEqual(groupBySpecific, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var oneItemDefaultOrderedGood = oneItemDefaultGood.OrderBy(x => x);
                try { groupBySpecific.SequenceEqual(oneItemDefaultOrderedGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { groupBySpecific.SequenceEqual(oneItemDefaultOrderedGood, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemDefaultOrderedGood.SequenceEqual(groupBySpecific); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemDefaultOrderedGood.SequenceEqual(groupBySpecific, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var oneItemSpecificOrderedGood = oneItemSpecificGood.OrderBy(x => x);
                try { groupBySpecific.SequenceEqual(oneItemSpecificOrderedGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { groupBySpecific.SequenceEqual(oneItemSpecificOrderedGood, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemSpecificOrderedGood.SequenceEqual(groupBySpecific); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemSpecificOrderedGood.SequenceEqual(groupBySpecific, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                Helper.ForEachEnumerableExpression(
                    groupBySpecific,
                    new[] { groupBySpecificGood.First() },
                    res => { },
                    @"(a, b) =>
                      {
                        try { a.SequenceEqual(b); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""first"", exc.ParamName); }
                        try { a.SequenceEqual(b, new SequenceEqualTests._GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""first"", exc.ParamName); }
                        try { b.SequenceEqual(a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""second"", exc.ParamName); }
                        try { b.SequenceEqual(a, new SequenceEqualTests._GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""second"", exc.ParamName); }

                        return Helper.NoCallValue;
                      }
                    ",
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );
            }

            // lookup
            {
                var emptyGroupingGood = Enumerable.Empty<GroupingEnumerable<int, int>>();
                try { lookup.SequenceEqual(emptyGroupingGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { lookup.SequenceEqual(emptyGroupingGood, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { emptyGroupingGood.SequenceEqual(lookup); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { emptyGroupingGood.SequenceEqual(lookup, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var emptyGroupingGoodOrdered = emptyGroupingGood.OrderBy(x => x);
                try { lookup.SequenceEqual(emptyGroupingGoodOrdered); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { lookup.SequenceEqual(emptyGroupingGoodOrdered, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { emptyGroupingGoodOrdered.SequenceEqual(lookup); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { emptyGroupingGoodOrdered.SequenceEqual(lookup, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var groupByDefaultGood = new[] { 1 }.GroupBy(x => x);
                try { lookup.SequenceEqual(groupByDefaultGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { lookup.SequenceEqual(groupByDefaultGood, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { groupByDefaultGood.SequenceEqual(lookup); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { groupByDefaultGood.SequenceEqual(lookup, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var groupBySpecificGood = new[] { 1 }.GroupBy(x => x, new _IntComparer());
                try { lookup.SequenceEqual(groupBySpecificGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { lookup.SequenceEqual(groupBySpecificGood, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { groupBySpecificGood.SequenceEqual(lookup); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { groupBySpecificGood.SequenceEqual(lookup, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var lookupGood = new[] { 1 }.ToLookup(x => x);
                try { lookup.SequenceEqual(lookupGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { lookup.SequenceEqual(lookupGood, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { lookupGood.SequenceEqual(lookup); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { lookupGood.SequenceEqual(lookup, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                // range is non-sensical
                var repeatGood = Enumerable.Repeat(groupByDefaultGood.First(), 1);
                try { lookup.SequenceEqual(repeatGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { lookup.SequenceEqual(repeatGood, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { repeatGood.SequenceEqual(lookup); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { repeatGood.SequenceEqual(lookup, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                // reverseRange is non-sensical

                var oneItemDefaultGood = Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty();
                try { lookup.SequenceEqual(oneItemDefaultGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { lookup.SequenceEqual(oneItemDefaultGood, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemDefaultGood.SequenceEqual(lookup); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemDefaultGood.SequenceEqual(lookup, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var oneItemSpecificGood = Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty(new[] { 4 }.GroupBy(x => x).First());
                try { lookup.SequenceEqual(oneItemSpecificGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { lookup.SequenceEqual(oneItemSpecificGood, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemSpecificGood.SequenceEqual(lookup); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemSpecificGood.SequenceEqual(lookup, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var oneItemDefaultOrderedGood = oneItemDefaultGood.OrderBy(x => x);
                try { lookup.SequenceEqual(oneItemDefaultOrderedGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { lookup.SequenceEqual(oneItemDefaultOrderedGood, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemDefaultOrderedGood.SequenceEqual(lookup); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemDefaultOrderedGood.SequenceEqual(lookup, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var oneItemSpecificOrderedGood = oneItemSpecificGood.OrderBy(x => x);
                try { lookup.SequenceEqual(oneItemSpecificOrderedGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { lookup.SequenceEqual(oneItemSpecificOrderedGood, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemSpecificOrderedGood.SequenceEqual(lookup); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemSpecificOrderedGood.SequenceEqual(lookup, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                Helper.ForEachEnumerableExpression(
                    lookup,
                    new[] { lookupGood.First() },
                    res => { },
                    @"(a, b) =>
                      {
                        try { a.SequenceEqual(b); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""first"", exc.ParamName); }
                        try { a.SequenceEqual(b, new SequenceEqualTests._GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""first"", exc.ParamName); }
                        try { b.SequenceEqual(a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""second"", exc.ParamName); }
                        try { b.SequenceEqual(a, new SequenceEqualTests._GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""second"", exc.ParamName); }

                        return Helper.NoCallValue;
                      }
                    ",
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );
            }

            // range
            {
                var emptyGood = Enumerable.Empty<int>();
                try { range.SequenceEqual(emptyGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { range.SequenceEqual(emptyGood, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { emptyGood.SequenceEqual(range); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { emptyGood.SequenceEqual(range, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var emptyOrderedGood = emptyGood.OrderBy(x => x);
                try { range.SequenceEqual(emptyOrderedGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { range.SequenceEqual(emptyOrderedGood, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { emptyOrderedGood.SequenceEqual(range); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { emptyOrderedGood.SequenceEqual(range, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                // groupby & lookup are non-sensical
                var rangeGood = Enumerable.Range(1, 1);
                try { range.SequenceEqual(rangeGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { range.SequenceEqual(rangeGood, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { rangeGood.SequenceEqual(range); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { rangeGood.SequenceEqual(range, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var repeatGood = Enumerable.Repeat(1, 1);
                try { range.SequenceEqual(repeatGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { range.SequenceEqual(repeatGood, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { repeatGood.SequenceEqual(range); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { repeatGood.SequenceEqual(range, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var reverseRangeGood = rangeGood.Reverse();
                try { range.SequenceEqual(reverseRangeGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { range.SequenceEqual(reverseRangeGood, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { reverseRangeGood.SequenceEqual(range); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { reverseRangeGood.SequenceEqual(range, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                var oneItemDefaultGood = Enumerable.Empty<int>().DefaultIfEmpty();
                try { range.SequenceEqual(oneItemDefaultGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { range.SequenceEqual(oneItemDefaultGood, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemDefaultGood.SequenceEqual(range); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemDefaultGood.SequenceEqual(range, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var oneItemSpecificGood = Enumerable.Empty<int>().DefaultIfEmpty(4);
                try { range.SequenceEqual(oneItemSpecificGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { range.SequenceEqual(oneItemSpecificGood, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemSpecificGood.SequenceEqual(range); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemSpecificGood.SequenceEqual(range, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var oneItemDefaultOrderedGood = oneItemDefaultGood.OrderBy(x => x);
                try { range.SequenceEqual(oneItemDefaultOrderedGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { range.SequenceEqual(oneItemDefaultOrderedGood, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemDefaultOrderedGood.SequenceEqual(range); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemDefaultOrderedGood.SequenceEqual(range, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var oneItemSpecificOrderedGood = oneItemSpecificGood.OrderBy(x => x);
                try { range.SequenceEqual(oneItemSpecificOrderedGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { range.SequenceEqual(oneItemSpecificOrderedGood, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemSpecificOrderedGood.SequenceEqual(range); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemSpecificOrderedGood.SequenceEqual(range, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                Helper.ForEachEnumerableExpression(
                    range,
                    new int[0],
                    res => { },
                    @"(a, b) =>
                      {
                        try { a.SequenceEqual(b); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""first"", exc.ParamName); }
                        try { a.SequenceEqual(b, new SequenceEqualTests._IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""first"", exc.ParamName); }
                        try { b.SequenceEqual(a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""second"", exc.ParamName); }
                        try { b.SequenceEqual(a, new SequenceEqualTests._IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""second"", exc.ParamName); }

                        return Helper.NoCallValue;
                      }
                    ",
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );
            }

            // repeat
            {
                var emptyGood = Enumerable.Empty<int>();
                try { repeat.SequenceEqual(emptyGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { repeat.SequenceEqual(emptyGood, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { emptyGood.SequenceEqual(repeat); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { emptyGood.SequenceEqual(repeat, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var emptyOrderedGood = emptyGood.OrderBy(x => x);
                try { repeat.SequenceEqual(emptyOrderedGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { repeat.SequenceEqual(emptyOrderedGood, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { emptyOrderedGood.SequenceEqual(repeat); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { emptyOrderedGood.SequenceEqual(repeat, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                // groupby & lookup are non-sensical
                var rangeGood = Enumerable.Range(1, 1);
                try { repeat.SequenceEqual(rangeGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { repeat.SequenceEqual(rangeGood, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { rangeGood.SequenceEqual(repeat); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { rangeGood.SequenceEqual(repeat, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var repeatGood = Enumerable.Repeat(1, 1);
                try { repeat.SequenceEqual(repeatGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { repeat.SequenceEqual(repeatGood, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { repeatGood.SequenceEqual(repeat); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { repeatGood.SequenceEqual(repeat, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var reverseRangeGood = rangeGood.Reverse();
                try { repeat.SequenceEqual(reverseRangeGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { repeat.SequenceEqual(reverseRangeGood, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { reverseRangeGood.SequenceEqual(repeat); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { reverseRangeGood.SequenceEqual(repeat, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                var oneItemDefaultGood = Enumerable.Empty<int>().DefaultIfEmpty();
                try { repeat.SequenceEqual(oneItemDefaultGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { repeat.SequenceEqual(oneItemDefaultGood, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemDefaultGood.SequenceEqual(repeat); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemDefaultGood.SequenceEqual(repeat, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var oneItemSpecificGood = Enumerable.Empty<int>().DefaultIfEmpty(4);
                try { repeat.SequenceEqual(oneItemSpecificGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { repeat.SequenceEqual(oneItemSpecificGood, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemSpecificGood.SequenceEqual(repeat); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemSpecificGood.SequenceEqual(repeat, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var oneItemDefaultOrderedGood = oneItemDefaultGood.OrderBy(x => x);
                try { repeat.SequenceEqual(oneItemDefaultOrderedGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { repeat.SequenceEqual(oneItemDefaultOrderedGood, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemDefaultOrderedGood.SequenceEqual(repeat); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemDefaultOrderedGood.SequenceEqual(repeat, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var oneItemSpecificOrderedGood = oneItemSpecificGood.OrderBy(x => x);
                try { repeat.SequenceEqual(oneItemSpecificOrderedGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { repeat.SequenceEqual(oneItemSpecificOrderedGood, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemSpecificOrderedGood.SequenceEqual(repeat); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemSpecificOrderedGood.SequenceEqual(repeat, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                Helper.ForEachEnumerableExpression(
                    repeat,
                    new int[0],
                    res => { },
                    @"(a, b) =>
                      {
                        try { a.SequenceEqual(b); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""first"", exc.ParamName); }
                        try { a.SequenceEqual(b, new SequenceEqualTests._IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""first"", exc.ParamName); }
                        try { b.SequenceEqual(a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""second"", exc.ParamName); }
                        try { b.SequenceEqual(a, new SequenceEqualTests._IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""second"", exc.ParamName); }

                        return Helper.NoCallValue;
                      }
                    ",
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );
            }

            // reverseRange
            {
                var emptyGood = Enumerable.Empty<int>();
                try { reverseRange.SequenceEqual(emptyGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { reverseRange.SequenceEqual(emptyGood, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { emptyGood.SequenceEqual(reverseRange); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { emptyGood.SequenceEqual(reverseRange, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var emptyOrderedGood = emptyGood.OrderBy(x => x);
                try { reverseRange.SequenceEqual(emptyOrderedGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { reverseRange.SequenceEqual(emptyOrderedGood, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { emptyOrderedGood.SequenceEqual(reverseRange); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { emptyOrderedGood.SequenceEqual(reverseRange, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                // groupby & lookup are non-sensical
                var rangeGood = Enumerable.Range(1, 1);
                try { reverseRange.SequenceEqual(rangeGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { reverseRange.SequenceEqual(rangeGood, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { rangeGood.SequenceEqual(reverseRange); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { rangeGood.SequenceEqual(reverseRange, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var repeatGood = Enumerable.Repeat(1, 1);
                try { reverseRange.SequenceEqual(repeatGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { reverseRange.SequenceEqual(repeatGood, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { repeatGood.SequenceEqual(reverseRange); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { repeatGood.SequenceEqual(reverseRange, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var reverseRangeGood = rangeGood.Reverse();
                try { reverseRange.SequenceEqual(reverseRangeGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { reverseRange.SequenceEqual(reverseRangeGood, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { reverseRangeGood.SequenceEqual(reverseRange); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { reverseRangeGood.SequenceEqual(reverseRange, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                var oneItemDefaultGood = Enumerable.Empty<int>().DefaultIfEmpty();
                try { reverseRange.SequenceEqual(oneItemDefaultGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { reverseRange.SequenceEqual(oneItemDefaultGood, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemDefaultGood.SequenceEqual(reverseRange); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemDefaultGood.SequenceEqual(reverseRange, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var oneItemSpecificGood = Enumerable.Empty<int>().DefaultIfEmpty(4);
                try { reverseRange.SequenceEqual(oneItemSpecificGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { reverseRange.SequenceEqual(oneItemSpecificGood, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemSpecificGood.SequenceEqual(reverseRange); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemSpecificGood.SequenceEqual(reverseRange, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var oneItemDefaultOrderedGood = oneItemDefaultGood.OrderBy(x => x);
                try { reverseRange.SequenceEqual(oneItemDefaultOrderedGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { reverseRange.SequenceEqual(oneItemDefaultOrderedGood, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemDefaultOrderedGood.SequenceEqual(reverseRange); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemDefaultOrderedGood.SequenceEqual(reverseRange, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var oneItemSpecificOrderedGood = oneItemSpecificGood.OrderBy(x => x);
                try { reverseRange.SequenceEqual(oneItemSpecificOrderedGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { reverseRange.SequenceEqual(oneItemSpecificOrderedGood, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemSpecificOrderedGood.SequenceEqual(reverseRange); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemSpecificOrderedGood.SequenceEqual(reverseRange, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                Helper.ForEachEnumerableExpression(
                    reverseRange,
                    new int[0],
                    res => { },
                    @"(a, b) =>
                      {
                        try { a.SequenceEqual(b); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""first"", exc.ParamName); }
                        try { a.SequenceEqual(b, new SequenceEqualTests._IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""first"", exc.ParamName); }
                        try { b.SequenceEqual(a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""second"", exc.ParamName); }
                        try { b.SequenceEqual(a, new SequenceEqualTests._IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""second"", exc.ParamName); }

                        return Helper.NoCallValue;
                      }
                    ",
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );
            }

            // oneItemDefault
            {
                var emptyGood = Enumerable.Empty<int>();
                try { oneItemDefault.SequenceEqual(emptyGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemDefault.SequenceEqual(emptyGood, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { emptyGood.SequenceEqual(oneItemDefault); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { emptyGood.SequenceEqual(oneItemDefault, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var emptyOrderedGood = emptyGood.OrderBy(x => x);
                try { oneItemDefault.SequenceEqual(emptyOrderedGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemDefault.SequenceEqual(emptyOrderedGood, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { emptyOrderedGood.SequenceEqual(oneItemDefault); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { emptyOrderedGood.SequenceEqual(oneItemDefault, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                // groupby & lookup are non-sensical
                var rangeGood = Enumerable.Range(1, 1);
                try { oneItemDefault.SequenceEqual(rangeGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemDefault.SequenceEqual(rangeGood, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { rangeGood.SequenceEqual(oneItemDefault); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { rangeGood.SequenceEqual(oneItemDefault, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var repeatGood = Enumerable.Repeat(1, 1);
                try { oneItemDefault.SequenceEqual(repeatGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemDefault.SequenceEqual(repeatGood, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { repeatGood.SequenceEqual(oneItemDefault); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { repeatGood.SequenceEqual(oneItemDefault, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var reverseRangeGood = rangeGood.Reverse();
                try { oneItemDefault.SequenceEqual(reverseRangeGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemDefault.SequenceEqual(reverseRangeGood, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { reverseRangeGood.SequenceEqual(oneItemDefault); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { reverseRangeGood.SequenceEqual(oneItemDefault, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                var oneItemDefaultGood = Enumerable.Empty<int>().DefaultIfEmpty();
                try { oneItemDefault.SequenceEqual(oneItemDefaultGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemDefault.SequenceEqual(oneItemDefaultGood, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemDefaultGood.SequenceEqual(oneItemDefault); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemDefaultGood.SequenceEqual(oneItemDefault, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var oneItemSpecificGood = Enumerable.Empty<int>().DefaultIfEmpty(4);
                try { oneItemDefault.SequenceEqual(oneItemSpecificGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemDefault.SequenceEqual(oneItemSpecificGood, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemSpecificGood.SequenceEqual(oneItemDefault); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemSpecificGood.SequenceEqual(oneItemDefault, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var oneItemDefaultOrderedGood = oneItemDefaultGood.OrderBy(x => x);
                try { oneItemDefault.SequenceEqual(oneItemDefaultOrderedGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemDefault.SequenceEqual(oneItemDefaultOrderedGood, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemDefaultOrderedGood.SequenceEqual(oneItemDefault); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemDefaultOrderedGood.SequenceEqual(oneItemDefault, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var oneItemSpecificOrderedGood = oneItemSpecificGood.OrderBy(x => x);
                try { oneItemDefault.SequenceEqual(oneItemSpecificOrderedGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemDefault.SequenceEqual(oneItemSpecificOrderedGood, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemSpecificOrderedGood.SequenceEqual(oneItemDefault); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemSpecificOrderedGood.SequenceEqual(oneItemDefault, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                Helper.ForEachEnumerableExpression(
                    oneItemDefault,
                    new int[0],
                    res => { },
                    @"(a, b) =>
                      {
                        try { a.SequenceEqual(b); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""first"", exc.ParamName); }
                        try { a.SequenceEqual(b, new SequenceEqualTests._IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""first"", exc.ParamName); }
                        try { b.SequenceEqual(a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""second"", exc.ParamName); }
                        try { b.SequenceEqual(a, new SequenceEqualTests._IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""second"", exc.ParamName); }

                        return Helper.NoCallValue;
                      }
                    ",
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );
            }

            // oneItemSpecific
            {
                var emptyGood = Enumerable.Empty<int>();
                try { oneItemSpecific.SequenceEqual(emptyGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemSpecific.SequenceEqual(emptyGood, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { emptyGood.SequenceEqual(oneItemSpecific); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { emptyGood.SequenceEqual(oneItemSpecific, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var emptyOrderedGood = emptyGood.OrderBy(x => x);
                try { oneItemSpecific.SequenceEqual(emptyOrderedGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemSpecific.SequenceEqual(emptyOrderedGood, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { emptyOrderedGood.SequenceEqual(oneItemSpecific); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { emptyOrderedGood.SequenceEqual(oneItemSpecific, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                // groupby & lookup are non-sensical
                var rangeGood = Enumerable.Range(1, 1);
                try { oneItemSpecific.SequenceEqual(rangeGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemSpecific.SequenceEqual(rangeGood, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { rangeGood.SequenceEqual(oneItemSpecific); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { rangeGood.SequenceEqual(oneItemSpecific, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var repeatGood = Enumerable.Repeat(1, 1);
                try { oneItemSpecific.SequenceEqual(repeatGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemSpecific.SequenceEqual(repeatGood, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { repeatGood.SequenceEqual(oneItemSpecific); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { repeatGood.SequenceEqual(oneItemSpecific, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var reverseRangeGood = rangeGood.Reverse();
                try { oneItemSpecific.SequenceEqual(reverseRangeGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemSpecific.SequenceEqual(reverseRangeGood, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { reverseRangeGood.SequenceEqual(oneItemSpecific); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { reverseRangeGood.SequenceEqual(oneItemSpecific, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                var oneItemDefaultGood = Enumerable.Empty<int>().DefaultIfEmpty();
                try { oneItemSpecific.SequenceEqual(oneItemDefaultGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemSpecific.SequenceEqual(oneItemDefaultGood, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemDefaultGood.SequenceEqual(oneItemSpecific); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemDefaultGood.SequenceEqual(oneItemSpecific, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var oneItemSpecificGood = Enumerable.Empty<int>().DefaultIfEmpty(4);
                try { oneItemSpecific.SequenceEqual(oneItemSpecificGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemSpecific.SequenceEqual(oneItemSpecificGood, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemSpecificGood.SequenceEqual(oneItemSpecific); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemSpecificGood.SequenceEqual(oneItemSpecific, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var oneItemDefaultOrderedGood = oneItemDefaultGood.OrderBy(x => x);
                try { oneItemSpecific.SequenceEqual(oneItemDefaultOrderedGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemSpecific.SequenceEqual(oneItemDefaultOrderedGood, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemDefaultOrderedGood.SequenceEqual(oneItemSpecific); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemDefaultOrderedGood.SequenceEqual(oneItemSpecific, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var oneItemSpecificOrderedGood = oneItemSpecificGood.OrderBy(x => x);
                try { oneItemSpecific.SequenceEqual(oneItemSpecificOrderedGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemSpecific.SequenceEqual(oneItemSpecificOrderedGood, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemSpecificOrderedGood.SequenceEqual(oneItemSpecific); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemSpecificOrderedGood.SequenceEqual(oneItemSpecific, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                Helper.ForEachEnumerableExpression(
                    oneItemSpecific,
                    new int[0],
                    res => { },
                    @"(a, b) =>
                      {
                        try { a.SequenceEqual(b); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""first"", exc.ParamName); }
                        try { a.SequenceEqual(b, new SequenceEqualTests._IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""first"", exc.ParamName); }
                        try { b.SequenceEqual(a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""second"", exc.ParamName); }
                        try { b.SequenceEqual(a, new SequenceEqualTests._IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""second"", exc.ParamName); }

                        return Helper.NoCallValue;
                      }
                    ",
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );
            }

            // oneItemDefaultOrdered
            {
                var emptyGood = Enumerable.Empty<int>();
                try { oneItemDefaultOrdered.SequenceEqual(emptyGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemDefaultOrdered.SequenceEqual(emptyGood, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { emptyGood.SequenceEqual(oneItemDefaultOrdered); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { emptyGood.SequenceEqual(oneItemDefaultOrdered, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var emptyOrderedGood = emptyGood.OrderBy(x => x);
                try { oneItemDefaultOrdered.SequenceEqual(emptyOrderedGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemDefaultOrdered.SequenceEqual(emptyOrderedGood, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { emptyOrderedGood.SequenceEqual(oneItemDefaultOrdered); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { emptyOrderedGood.SequenceEqual(oneItemDefaultOrdered, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                // groupby & lookup are non-sensical
                var rangeGood = Enumerable.Range(1, 1);
                try { oneItemDefaultOrdered.SequenceEqual(rangeGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemDefaultOrdered.SequenceEqual(rangeGood, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { rangeGood.SequenceEqual(oneItemDefaultOrdered); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { rangeGood.SequenceEqual(oneItemDefaultOrdered, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var repeatGood = Enumerable.Repeat(1, 1);
                try { oneItemDefaultOrdered.SequenceEqual(repeatGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemDefaultOrdered.SequenceEqual(repeatGood, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { repeatGood.SequenceEqual(oneItemDefaultOrdered); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { repeatGood.SequenceEqual(oneItemDefaultOrdered, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var reverseRangeGood = rangeGood.Reverse();
                try { oneItemDefaultOrdered.SequenceEqual(reverseRangeGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemDefaultOrdered.SequenceEqual(reverseRangeGood, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { reverseRangeGood.SequenceEqual(oneItemDefaultOrdered); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { reverseRangeGood.SequenceEqual(oneItemDefaultOrdered, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                var oneItemDefaultGood = Enumerable.Empty<int>().DefaultIfEmpty();
                try { oneItemDefaultOrdered.SequenceEqual(oneItemDefaultGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemDefaultOrdered.SequenceEqual(oneItemDefaultGood, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemDefaultGood.SequenceEqual(oneItemDefaultOrdered); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemDefaultGood.SequenceEqual(oneItemDefaultOrdered, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var oneItemSpecificGood = Enumerable.Empty<int>().DefaultIfEmpty(4);
                try { oneItemDefaultOrdered.SequenceEqual(oneItemSpecificGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemDefaultOrdered.SequenceEqual(oneItemSpecificGood, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemSpecificGood.SequenceEqual(oneItemDefaultOrdered); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemSpecificGood.SequenceEqual(oneItemDefaultOrdered, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var oneItemDefaultOrderedGood = oneItemDefaultGood.OrderBy(x => x);
                try { oneItemDefaultOrdered.SequenceEqual(oneItemDefaultOrderedGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemDefaultOrdered.SequenceEqual(oneItemDefaultOrderedGood, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemDefaultOrderedGood.SequenceEqual(oneItemDefaultOrdered); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemDefaultOrderedGood.SequenceEqual(oneItemDefaultOrdered, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var oneItemSpecificOrderedGood = oneItemSpecificGood.OrderBy(x => x);
                try { oneItemDefaultOrdered.SequenceEqual(oneItemSpecificOrderedGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemDefaultOrdered.SequenceEqual(oneItemSpecificOrderedGood, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemSpecificOrderedGood.SequenceEqual(oneItemDefaultOrdered); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemSpecificOrderedGood.SequenceEqual(oneItemDefaultOrdered, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                Helper.ForEachEnumerableExpression(
                    oneItemDefaultOrdered,
                    new int[0],
                    res => { },
                    @"(a, b) =>
                      {
                        try { a.SequenceEqual(b); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""first"", exc.ParamName); }
                        try { a.SequenceEqual(b, new SequenceEqualTests._IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""first"", exc.ParamName); }
                        try { b.SequenceEqual(a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""second"", exc.ParamName); }
                        try { b.SequenceEqual(a, new SequenceEqualTests._IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""second"", exc.ParamName); }

                        return Helper.NoCallValue;
                      }
                    ",
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );
            }

            // oneItemSpecificOrdered
            {
                var emptyGood = Enumerable.Empty<int>();
                try { oneItemSpecificOrdered.SequenceEqual(emptyGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemSpecificOrdered.SequenceEqual(emptyGood, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { emptyGood.SequenceEqual(oneItemSpecificOrdered); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { emptyGood.SequenceEqual(oneItemSpecificOrdered, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var emptyOrderedGood = emptyGood.OrderBy(x => x);
                try { oneItemSpecificOrdered.SequenceEqual(emptyOrderedGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemSpecificOrdered.SequenceEqual(emptyOrderedGood, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { emptyOrderedGood.SequenceEqual(oneItemSpecificOrdered); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { emptyOrderedGood.SequenceEqual(oneItemSpecificOrdered, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                // groupby & lookup are non-sensical
                var rangeGood = Enumerable.Range(1, 1);
                try { oneItemSpecificOrdered.SequenceEqual(rangeGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemSpecificOrdered.SequenceEqual(rangeGood, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { rangeGood.SequenceEqual(oneItemSpecificOrdered); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { rangeGood.SequenceEqual(oneItemSpecificOrdered, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var repeatGood = Enumerable.Repeat(1, 1);
                try { oneItemSpecificOrdered.SequenceEqual(repeatGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemSpecificOrdered.SequenceEqual(repeatGood, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { repeatGood.SequenceEqual(oneItemSpecificOrdered); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { repeatGood.SequenceEqual(oneItemSpecificOrdered, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var reverseRangeGood = rangeGood.Reverse();
                try { oneItemSpecificOrdered.SequenceEqual(reverseRangeGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemSpecificOrdered.SequenceEqual(reverseRangeGood, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { reverseRangeGood.SequenceEqual(oneItemSpecificOrdered); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { reverseRangeGood.SequenceEqual(oneItemSpecificOrdered, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                var oneItemDefaultGood = Enumerable.Empty<int>().DefaultIfEmpty();
                try { oneItemSpecificOrdered.SequenceEqual(oneItemDefaultGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemSpecificOrdered.SequenceEqual(oneItemDefaultGood, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemDefaultGood.SequenceEqual(oneItemSpecificOrdered); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemDefaultGood.SequenceEqual(oneItemSpecificOrdered, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var oneItemSpecificGood = Enumerable.Empty<int>().DefaultIfEmpty(4);
                try { oneItemSpecificOrdered.SequenceEqual(oneItemSpecificGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemSpecificOrdered.SequenceEqual(oneItemSpecificGood, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemSpecificGood.SequenceEqual(oneItemSpecificOrdered); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemSpecificGood.SequenceEqual(oneItemSpecificOrdered, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var oneItemDefaultOrderedGood = oneItemDefaultGood.OrderBy(x => x);
                try { oneItemSpecificOrdered.SequenceEqual(oneItemDefaultOrderedGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemSpecificOrdered.SequenceEqual(oneItemDefaultOrderedGood, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemDefaultOrderedGood.SequenceEqual(oneItemSpecificOrdered); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemDefaultOrderedGood.SequenceEqual(oneItemSpecificOrdered, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var oneItemSpecificOrderedGood = oneItemSpecificGood.OrderBy(x => x);
                try { oneItemSpecificOrdered.SequenceEqual(oneItemSpecificOrderedGood); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemSpecificOrdered.SequenceEqual(oneItemSpecificOrderedGood, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemSpecificOrderedGood.SequenceEqual(oneItemSpecificOrdered); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemSpecificOrderedGood.SequenceEqual(oneItemSpecificOrdered, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                Helper.ForEachEnumerableExpression(
                    oneItemSpecificOrdered,
                    new int[0],
                    res => { },
                    @"(a, b) =>
                      {
                        try { a.SequenceEqual(b); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""first"", exc.ParamName); }
                        try { a.SequenceEqual(b, new SequenceEqualTests._IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""first"", exc.ParamName); }
                        try { b.SequenceEqual(a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""second"", exc.ParamName); }
                        try { b.SequenceEqual(a, new SequenceEqualTests._IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""second"", exc.ParamName); }

                        return Helper.NoCallValue;
                      }
                    ",
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );
            }
        }
    }
}
