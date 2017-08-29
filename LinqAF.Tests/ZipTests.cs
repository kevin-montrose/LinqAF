using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using TestHelpers;

namespace LinqAF.Tests
{
    [TestClass]
    public class ZipTests
    {
        [TestMethod]
        public void Universal()
        {
            var enums = Helper.AllEnumerables();

            foreach (var e in enums)
            {
                System.Collections.Generic.List<string> missing;
                if (!Helper.Implements(e, typeof(LinqAF.Impl.IZip<,,>), out missing))
                {
                    Assert.Fail($"{e.Name} does not implement IZip ({string.Join(", ", missing)})");
                }
            }
        }

        [TestMethod]
        public void AcceptsAllEnumerables()
        {
            var missingMethod = new List<string>();

            var izip = typeof(Impl.IZip<,,>);
            var enums = Helper.AllEnumerables(includeWeirdOnes: false);
            foreach (var e in enums)
            {
                var mtd =
                    izip
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
                if (mtd == null) missingMethod.Add(e.Name);
            }

            if (missingMethod.Any())
            {
                Assert.Fail("Missing method for: \r\n" + string.Join("\r\n", missingMethod));
            }
        }

        [TestMethod]
        public void Chaining()
        {
            Helper.ForEachEnumerableNoRetExpression(
                new[] { 1, 2, 3 },
                @"a =>
                  Helper.ForEachEnumerableExpression(
                    a,
                    new [] { 4, 5, 6 },
                    res =>
                    {
                        Assert.AreEqual(3, res.Count);
                        Assert.AreEqual(5, res[0]);
                        Assert.AreEqual(7, res[1]);
                        Assert.AreEqual(9, res[2]);
                    },
                    @""(a, b) => a.Zip(b, (x, y) => x + y)"",
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

        class _GroupingComparer<T> : IEqualityComparer<GroupingEnumerable<T, T>>
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
                Assert.IsFalse(empty.Zip(empty, (a, b) => a).Any());
                Assert.IsFalse(empty.Zip(emptyOrdered, (a, b) => a).Any());
                var emptyGroupingInt = Enumerable.Empty<GroupingEnumerable<int, int>>();
                Assert.IsFalse(emptyGroupingInt.Zip(groupByDefault, (a, b) => a).Any());
                var emptyGroupingString = Enumerable.Empty<GroupingEnumerable<string, string>>();
                Assert.IsFalse(emptyGroupingString.Zip(groupBySpecific, (a, b) => a).Any());
                Assert.IsFalse(emptyGroupingInt.Zip(lookup, (a, b) => a).Any());
                Assert.IsFalse(empty.Zip(range, (a, b) => a).Any());
                var emptyString = Enumerable.Empty<string>();
                Assert.IsFalse(emptyString.Zip(repeat, (a, b) => a).Any());
                Assert.IsFalse(empty.Zip(reverseRange, (a, b) => a).Any());
                Assert.IsFalse(empty.Zip(oneItemDefault, (a, b) => a).Any());
                Assert.IsFalse(empty.Zip(oneItemSpecific, (a, b) => a).Any());
                Assert.IsFalse(empty.Zip(oneItemDefaultOrdered, (a, b) => a).Any());
                Assert.IsFalse(empty.Zip(oneItemSpecificOrdered, (a, b) => a).Any());

                Helper.ForEachEnumerableExpression(
                    empty,
                    new int[0],
                    res => { },
                    @"(a, b) =>
                      {
                        Assert.IsFalse(a.Zip(b, (x, y) => x).Any());    

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
                Assert.IsFalse(emptyOrdered.Zip(empty, (a, b) => a).Any());
                Assert.IsFalse(emptyOrdered.Zip(emptyOrdered, (a, b) => a).Any());
                var emptyGroupingInt = Enumerable.Empty<GroupingEnumerable<int, int>>();
                Assert.IsFalse(emptyGroupingInt.Zip(groupByDefault, (a, b) => a).Any());
                var emptyOrderedGroupingString = Enumerable.Empty<GroupingEnumerable<string, string>>().OrderBy(x => x);
                Assert.IsFalse(emptyOrderedGroupingString.Zip(groupBySpecific, (a, b) => a).Any());
                Assert.IsFalse(emptyGroupingInt.Zip(lookup, (a, b) => a).Any());
                Assert.IsFalse(emptyOrdered.Zip(range, (a, b) => a).Any());
                var emptyOrderedString = Enumerable.Empty<string>().OrderBy(x => x);
                Assert.IsFalse(emptyOrderedString.Zip(repeat, (a, b) => a).Any());
                Assert.IsFalse(emptyOrdered.Zip(reverseRange, (a, b) => a).Any());
                Assert.IsFalse(emptyOrdered.Zip(oneItemDefault, (a, b) => a).Any());
                Assert.IsFalse(emptyOrdered.Zip(oneItemSpecific, (a, b) => a).Any());
                Assert.IsFalse(emptyOrdered.Zip(oneItemDefaultOrdered, (a, b) => a).Any());
                Assert.IsFalse(emptyOrdered.Zip(oneItemSpecificOrdered, (a, b) => a).Any());

                Helper.ForEachEnumerableExpression(
                    emptyOrdered,
                    new int[0],
                    res => { },
                    @"(a, b) =>
                      {
                        Assert.IsFalse(a.Zip(b, (x, y) => x).Any());    

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
                Assert.IsFalse(groupByDefault.Zip(empty, (a, b) => a).Any());
                Assert.IsFalse(groupByDefault.Zip(emptyOrdered, (a, b) => a).Any());
                Assert.IsTrue(groupByDefault.Zip(groupByDefault, (a, b) => a).SequenceEqual(groupByDefault, new _GroupingComparer<int>()));
                Assert.IsTrue(groupByDefault.Zip(groupBySpecific, (a, b) => a).SequenceEqual(groupByDefault, new _GroupingComparer<int>()));
                Assert.IsTrue(groupByDefault.Zip(lookup, (a, b) => a).SequenceEqual(groupByDefault, new _GroupingComparer<int>()));
                Assert.IsTrue(groupByDefault.Zip(range, (a, b) => a).SequenceEqual(groupByDefault, new _GroupingComparer<int>()));
                Assert.IsTrue(groupByDefault.Zip(repeat, (a, b) => a).SequenceEqual(groupByDefault, new _GroupingComparer<int>()));
                Assert.IsTrue(groupByDefault.Zip(reverseRange, (a, b) => a).SequenceEqual(groupByDefault, new _GroupingComparer<int>()));

                var defaultIfEmptyDefault = new int[0].DefaultIfEmpty();
                Assert.IsTrue(groupByDefault.Zip(defaultIfEmptyDefault, (a, b) => a).SequenceEqual(new[] { groupByDefault.First() }, new _GroupingComparer<int>()));
                var defaultIfEmptySpecific = new int[0].DefaultIfEmpty(4);
                Assert.IsTrue(groupByDefault.Zip(defaultIfEmptySpecific, (a, b) => a).SequenceEqual(new[] { groupByDefault.First() }, new _GroupingComparer<int>()));

                Assert.IsTrue(groupByDefault.Zip(oneItemDefault, (a, b) => a).SequenceEqual(new[] { groupByDefault.First() }, new _GroupingComparer<int>()));
                Assert.IsTrue(groupByDefault.Zip(oneItemSpecific, (a, b) => a).SequenceEqual(new[] { groupByDefault.First() }, new _GroupingComparer<int>()));
                Assert.IsTrue(groupByDefault.Zip(oneItemDefaultOrdered, (a, b) => a).SequenceEqual(new[] { groupByDefault.First() }, new _GroupingComparer<int>()));
                Assert.IsTrue(groupByDefault.Zip(oneItemSpecificOrdered, (a, b) => a).SequenceEqual(new[] { groupByDefault.First() }, new _GroupingComparer<int>()));

                Helper.ForEachEnumerableExpression(
                    groupByDefault,
                    new int[0],
                    res => { },
                    @"(a, b) =>
                      {
                        Assert.IsFalse(a.Zip(b, (x, y) => x).Any());    

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

            // groupBySpecific
            {
                Assert.IsFalse(groupBySpecific.Zip(empty, (a, b) => a).Any());
                Assert.IsFalse(groupBySpecific.Zip(emptyOrdered, (a, b) => a).Any());
                Assert.IsTrue(groupBySpecific.Zip(groupByDefault, (a, b) => a).SequenceEqual(groupBySpecific, new _GroupingComparer<string>()));
                Assert.IsTrue(groupBySpecific.Zip(groupBySpecific, (a, b) => a).SequenceEqual(groupBySpecific, new _GroupingComparer<string>()));
                Assert.IsTrue(groupBySpecific.Zip(lookup, (a, b) => a).SequenceEqual(groupBySpecific, new _GroupingComparer<string>()));
                Assert.IsTrue(groupBySpecific.Zip(range, (a, b) => a).SequenceEqual(groupBySpecific, new _GroupingComparer<string>()));
                Assert.IsTrue(groupBySpecific.Zip(repeat, (a, b) => a).SequenceEqual(groupBySpecific, new _GroupingComparer<string>()));
                Assert.IsTrue(groupBySpecific.Zip(reverseRange, (a, b) => a).SequenceEqual(groupBySpecific, new _GroupingComparer<string>()));

                var defaultIfEmptyDefault = new int[0].DefaultIfEmpty();
                Assert.IsTrue(groupBySpecific.Zip(defaultIfEmptyDefault, (a, b) => a).SequenceEqual(new[] { groupBySpecific.First() }, new _GroupingComparer<string>()));
                var defaultIfEmptySpecific = new int[0].DefaultIfEmpty(4);
                Assert.IsTrue(groupBySpecific.Zip(defaultIfEmptySpecific, (a, b) => a).SequenceEqual(new[] { groupBySpecific.First() }, new _GroupingComparer<string>()));

                Assert.IsTrue(groupBySpecific.Zip(oneItemDefault, (a, b) => a).SequenceEqual(new[] { groupBySpecific.First() }, new _GroupingComparer<string>()));
                Assert.IsTrue(groupBySpecific.Zip(oneItemSpecific, (a, b) => a).SequenceEqual(new[] { groupBySpecific.First() }, new _GroupingComparer<string>()));
                Assert.IsTrue(groupBySpecific.Zip(oneItemDefaultOrdered, (a, b) => a).SequenceEqual(new[] { groupBySpecific.First() }, new _GroupingComparer<string>()));
                Assert.IsTrue(groupBySpecific.Zip(oneItemSpecificOrdered, (a, b) => a).SequenceEqual(new[] { groupBySpecific.First() }, new _GroupingComparer<string>()));

                Helper.ForEachEnumerableExpression(
                    groupBySpecific,
                    new int[0],
                    res => { },
                    @"(a, b) =>
                      {
                        Assert.IsFalse(a.Zip(b, (x, y) => x).Any());    

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

            // lookup
            {
                Assert.IsFalse(lookup.Zip(empty, (a, b) => a).Any());
                Assert.IsFalse(lookup.Zip(emptyOrdered, (a, b) => a).Any());
                Assert.IsTrue(lookup.Zip(groupByDefault, (a, b) => a).SequenceEqual(lookup, new _GroupingComparer<int>()));
                Assert.IsTrue(lookup.Zip(groupBySpecific, (a, b) => a).SequenceEqual(lookup, new _GroupingComparer<int>()));
                Assert.IsTrue(lookup.Zip(lookup, (a, b) => a).SequenceEqual(lookup, new _GroupingComparer<int>()));
                Assert.IsTrue(lookup.Zip(range, (a, b) => a).SequenceEqual(lookup, new _GroupingComparer<int>()));
                Assert.IsTrue(lookup.Zip(repeat, (a, b) => a).SequenceEqual(lookup, new _GroupingComparer<int>()));
                Assert.IsTrue(lookup.Zip(reverseRange, (a, b) => a).SequenceEqual(lookup, new _GroupingComparer<int>()));

                var defaultIfEmptyDefault = new int[0].DefaultIfEmpty();
                Assert.IsTrue(lookup.Zip(defaultIfEmptyDefault, (a, b) => a).SequenceEqual(new[] { lookup.First() }, new _GroupingComparer<int>()));
                var defaultIfEmptySpecific = new int[0].DefaultIfEmpty(4);
                Assert.IsTrue(lookup.Zip(defaultIfEmptySpecific, (a, b) => a).SequenceEqual(new[] { lookup.First() }, new _GroupingComparer<int>()));

                Assert.IsTrue(lookup.Zip(oneItemDefault, (a, b) => a).SequenceEqual(new[] { lookup.First() }, new _GroupingComparer<int>()));
                Assert.IsTrue(lookup.Zip(oneItemSpecific, (a, b) => a).SequenceEqual(new[] { lookup.First() }, new _GroupingComparer<int>()));
                Assert.IsTrue(lookup.Zip(oneItemDefaultOrdered, (a, b) => a).SequenceEqual(new[] { lookup.First() }, new _GroupingComparer<int>()));
                Assert.IsTrue(lookup.Zip(oneItemSpecificOrdered, (a, b) => a).SequenceEqual(new[] { lookup.First() }, new _GroupingComparer<int>()));

                Helper.ForEachEnumerableExpression(
                    lookup,
                    new int[0],
                    res => { },
                    @"(a, b) =>
                      {
                        Assert.IsFalse(a.Zip(b, (x, y) => x).Any());    

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

            // range
            {
                Assert.IsFalse(range.Zip(empty, (a, b) => a).Any());
                Assert.IsFalse(range.Zip(emptyOrdered, (a, b) => a).Any());
                Assert.IsTrue(range.Zip(groupByDefault, (a, b) => a).SequenceEqual(new[] { 1, 2, 3 }));
                Assert.IsTrue(range.Zip(groupBySpecific, (a, b) => a).SequenceEqual(new[] { 1, 2, 3 }));
                Assert.IsTrue(range.Zip(lookup, (a, b) => a).SequenceEqual(new[] { 1, 2, 3 }));
                Assert.IsTrue(range.Zip(range, (a, b) => a).SequenceEqual(range));
                Assert.IsTrue(range.Zip(repeat, (a, b) => a).SequenceEqual(range));
                Assert.IsTrue(range.Zip(reverseRange, (a, b) => a).SequenceEqual(range));

                var defaultIfEmptyDefault = new int[0].DefaultIfEmpty();
                Assert.IsTrue(range.Zip(defaultIfEmptyDefault, (a, b) => a).SequenceEqual(new[] { 1 }));
                var defaultIfEmptySpecific = new int[0].DefaultIfEmpty(4);
                Assert.IsTrue(range.Zip(defaultIfEmptySpecific, (a, b) => a).SequenceEqual(new[] { 1 }));

                Assert.IsTrue(range.Zip(oneItemDefault, (a, b) => a).SequenceEqual(new[] { range.First() }));
                Assert.IsTrue(range.Zip(oneItemSpecific, (a, b) => a).SequenceEqual(new[] { range.First() }));
                Assert.IsTrue(range.Zip(oneItemDefaultOrdered, (a, b) => a).SequenceEqual(new[] { range.First() }));
                Assert.IsTrue(range.Zip(oneItemSpecificOrdered, (a, b) => a).SequenceEqual(new[] { range.First() }));

                Helper.ForEachEnumerableExpression(
                    range,
                    new int[0],
                    res => { },
                    @"(a, b) =>
                      {
                        Assert.IsFalse(a.Zip(b, (x, y) => x).Any());    

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

            // repeat
            {
                Assert.IsFalse(repeat.Zip(empty, (a, b) => a).Any());
                Assert.IsFalse(repeat.Zip(emptyOrdered, (a, b) => a).Any());
                Assert.IsTrue(repeat.Zip(groupByDefault, (a, b) => a).SequenceEqual(new[] { "foo", "foo", "foo" }));
                Assert.IsTrue(repeat.Zip(groupBySpecific, (a, b) => a).SequenceEqual(new[] { "foo", "foo", "foo" }));
                Assert.IsTrue(repeat.Zip(lookup, (a, b) => a).SequenceEqual(new[] { "foo", "foo", "foo" }));
                Assert.IsTrue(repeat.Zip(range, (a, b) => a).SequenceEqual(repeat));
                Assert.IsTrue(repeat.Zip(repeat, (a, b) => a).SequenceEqual(repeat));
                Assert.IsTrue(repeat.Zip(reverseRange, (a, b) => a).SequenceEqual(repeat));

                var defaultIfEmptyDefault = new int[0].DefaultIfEmpty();
                Assert.IsTrue(repeat.Zip(defaultIfEmptyDefault, (a, b) => a).SequenceEqual(new[] { "foo" }));
                var defaultIfEmptySpecific = new int[0].DefaultIfEmpty(4);
                Assert.IsTrue(repeat.Zip(defaultIfEmptySpecific, (a, b) => a).SequenceEqual(new[] { "foo" }));

                Assert.IsTrue(repeat.Zip(oneItemDefault, (a, b) => a).SequenceEqual(new[] { repeat.First() }));
                Assert.IsTrue(repeat.Zip(oneItemSpecific, (a, b) => a).SequenceEqual(new[] { repeat.First() }));
                Assert.IsTrue(repeat.Zip(oneItemDefaultOrdered, (a, b) => a).SequenceEqual(new[] { repeat.First() }));
                Assert.IsTrue(repeat.Zip(oneItemSpecificOrdered, (a, b) => a).SequenceEqual(new[] { repeat.First() }));

                Helper.ForEachEnumerableExpression(
                    repeat,
                    new int[0],
                    res => { },
                    @"(a, b) =>
                      {
                        Assert.IsFalse(a.Zip(b, (x, y) => x).Any());    

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

            // reverseRange
            {
                Assert.IsFalse(reverseRange.Zip(empty, (a, b) => a).Any());
                Assert.IsFalse(reverseRange.Zip(emptyOrdered, (a, b) => a).Any());
                Assert.IsTrue(reverseRange.Zip(groupByDefault, (a, b) => a).SequenceEqual(new[] { 5, 4, 3 }));
                Assert.IsTrue(reverseRange.Zip(groupBySpecific, (a, b) => a).SequenceEqual(new[] { 5, 4, 3 }));
                Assert.IsTrue(reverseRange.Zip(lookup, (a, b) => a).SequenceEqual(new[] { 5, 4, 3 }));
                Assert.IsTrue(reverseRange.Zip(range, (a, b) => a).SequenceEqual(reverseRange));
                Assert.IsTrue(reverseRange.Zip(repeat, (a, b) => a).SequenceEqual(reverseRange));
                Assert.IsTrue(reverseRange.Zip(reverseRange, (a, b) => a).SequenceEqual(reverseRange));

                var defaultIfEmptyDefault = new int[0].DefaultIfEmpty();
                Assert.IsTrue(reverseRange.Zip(defaultIfEmptyDefault, (a, b) => a).SequenceEqual(new[] { 5 }));
                var defaultIfEmptySpecific = new int[0].DefaultIfEmpty(4);
                Assert.IsTrue(reverseRange.Zip(defaultIfEmptySpecific, (a, b) => a).SequenceEqual(new[] { 5 }));

                Assert.IsTrue(reverseRange.Zip(oneItemDefault, (a, b) => a).SequenceEqual(new[] { reverseRange.First() }));
                Assert.IsTrue(reverseRange.Zip(oneItemSpecific, (a, b) => a).SequenceEqual(new[] { reverseRange.First() }));
                Assert.IsTrue(reverseRange.Zip(oneItemDefaultOrdered, (a, b) => a).SequenceEqual(new[] { reverseRange.First() }));
                Assert.IsTrue(reverseRange.Zip(oneItemSpecificOrdered, (a, b) => a).SequenceEqual(new[] { reverseRange.First() }));

                Helper.ForEachEnumerableExpression(
                    reverseRange,
                    new int[0],
                    res => { },
                    @"(a, b) =>
                      {
                        Assert.IsFalse(a.Zip(b, (x, y) => x).Any());    

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

            // oneItemDefault
            {
                Assert.IsFalse(oneItemDefault.Zip(empty, (a, b) => a).Any());
                Assert.IsFalse(oneItemDefault.Zip(emptyOrdered, (a, b) => a).Any());
                Assert.IsTrue(oneItemDefault.Zip(groupByDefault, (a, b) => a).SequenceEqual(oneItemDefault));
                Assert.IsTrue(oneItemDefault.Zip(groupBySpecific, (a, b) => a).SequenceEqual(oneItemDefault));
                Assert.IsTrue(oneItemDefault.Zip(lookup, (a, b) => a).SequenceEqual(oneItemDefault));
                Assert.IsTrue(oneItemDefault.Zip(range, (a, b) => a).SequenceEqual(oneItemDefault));
                Assert.IsTrue(oneItemDefault.Zip(repeat, (a, b) => a).SequenceEqual(oneItemDefault));
                Assert.IsTrue(oneItemDefault.Zip(reverseRange, (a, b) => a).SequenceEqual(oneItemDefault));

                var defaultIfEmptyDefault = new int[0].DefaultIfEmpty();
                Assert.IsTrue(oneItemDefault.Zip(defaultIfEmptyDefault, (a, b) => a).SequenceEqual(oneItemDefault));
                var defaultIfEmptySpecific = new int[0].DefaultIfEmpty(4);
                Assert.IsTrue(oneItemDefault.Zip(defaultIfEmptySpecific, (a, b) => a).SequenceEqual(oneItemDefault));

                Assert.IsTrue(oneItemDefault.Zip(oneItemDefault, (a, b) => a).SequenceEqual(oneItemDefault));
                Assert.IsTrue(oneItemDefault.Zip(oneItemSpecific, (a, b) => a).SequenceEqual(oneItemDefault));
                Assert.IsTrue(oneItemDefault.Zip(oneItemDefaultOrdered, (a, b) => a).SequenceEqual(oneItemDefault));
                Assert.IsTrue(oneItemDefault.Zip(oneItemSpecificOrdered, (a, b) => a).SequenceEqual(oneItemDefault));

                Helper.ForEachEnumerableExpression(
                    oneItemDefault,
                    new int[0],
                    res => { },
                    @"(a, b) =>
                      {
                        Assert.IsFalse(a.Zip(b, (x, y) => x).Any());    

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
                Assert.IsFalse(oneItemSpecific.Zip(empty, (a, b) => a).Any());
                Assert.IsFalse(oneItemSpecific.Zip(emptyOrdered, (a, b) => a).Any());
                Assert.IsTrue(oneItemSpecific.Zip(groupByDefault, (a, b) => a).SequenceEqual(oneItemSpecific));
                Assert.IsTrue(oneItemSpecific.Zip(groupBySpecific, (a, b) => a).SequenceEqual(oneItemSpecific));
                Assert.IsTrue(oneItemSpecific.Zip(lookup, (a, b) => a).SequenceEqual(oneItemSpecific));
                Assert.IsTrue(oneItemSpecific.Zip(range, (a, b) => a).SequenceEqual(oneItemSpecific));
                Assert.IsTrue(oneItemSpecific.Zip(repeat, (a, b) => a).SequenceEqual(oneItemSpecific));
                Assert.IsTrue(oneItemSpecific.Zip(reverseRange, (a, b) => a).SequenceEqual(oneItemSpecific));

                var defaultIfEmptyDefault = new int[0].DefaultIfEmpty();
                Assert.IsTrue(oneItemSpecific.Zip(defaultIfEmptyDefault, (a, b) => a).SequenceEqual(oneItemSpecific));
                var defaultIfEmptySpecific = new int[0].DefaultIfEmpty(4);
                Assert.IsTrue(oneItemSpecific.Zip(defaultIfEmptySpecific, (a, b) => a).SequenceEqual(oneItemSpecific));

                Assert.IsTrue(oneItemSpecific.Zip(oneItemDefault, (a, b) => a).SequenceEqual(oneItemSpecific));
                Assert.IsTrue(oneItemSpecific.Zip(oneItemSpecific, (a, b) => a).SequenceEqual(oneItemSpecific));
                Assert.IsTrue(oneItemSpecific.Zip(oneItemDefaultOrdered, (a, b) => a).SequenceEqual(oneItemSpecific));
                Assert.IsTrue(oneItemSpecific.Zip(oneItemSpecificOrdered, (a, b) => a).SequenceEqual(oneItemSpecific));

                Helper.ForEachEnumerableExpression(
                    oneItemSpecific,
                    new int[0],
                    res => { },
                    @"(a, b) =>
                      {
                        Assert.IsFalse(a.Zip(b, (x, y) => x).Any());    

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
                Assert.IsFalse(oneItemDefaultOrdered.Zip(empty, (a, b) => a).Any());
                Assert.IsFalse(oneItemDefaultOrdered.Zip(emptyOrdered, (a, b) => a).Any());
                Assert.IsTrue(oneItemDefaultOrdered.Zip(groupByDefault, (a, b) => a).SequenceEqual(oneItemDefaultOrdered));
                Assert.IsTrue(oneItemDefaultOrdered.Zip(groupBySpecific, (a, b) => a).SequenceEqual(oneItemDefaultOrdered));
                Assert.IsTrue(oneItemDefaultOrdered.Zip(lookup, (a, b) => a).SequenceEqual(oneItemDefaultOrdered));
                Assert.IsTrue(oneItemDefaultOrdered.Zip(range, (a, b) => a).SequenceEqual(oneItemDefaultOrdered));
                Assert.IsTrue(oneItemDefaultOrdered.Zip(repeat, (a, b) => a).SequenceEqual(oneItemDefaultOrdered));
                Assert.IsTrue(oneItemDefaultOrdered.Zip(reverseRange, (a, b) => a).SequenceEqual(oneItemDefaultOrdered));

                var defaultIfEmptyDefault = new int[0].DefaultIfEmpty();
                Assert.IsTrue(oneItemDefaultOrdered.Zip(defaultIfEmptyDefault, (a, b) => a).SequenceEqual(oneItemDefaultOrdered));
                var defaultIfEmptySpecific = new int[0].DefaultIfEmpty(4);
                Assert.IsTrue(oneItemDefaultOrdered.Zip(defaultIfEmptySpecific, (a, b) => a).SequenceEqual(oneItemDefaultOrdered));

                Assert.IsTrue(oneItemDefaultOrdered.Zip(oneItemDefault, (a, b) => a).SequenceEqual(oneItemDefaultOrdered));
                Assert.IsTrue(oneItemDefaultOrdered.Zip(oneItemSpecific, (a, b) => a).SequenceEqual(oneItemDefaultOrdered));
                Assert.IsTrue(oneItemDefaultOrdered.Zip(oneItemDefaultOrdered, (a, b) => a).SequenceEqual(oneItemDefaultOrdered));
                Assert.IsTrue(oneItemDefaultOrdered.Zip(oneItemSpecificOrdered, (a, b) => a).SequenceEqual(oneItemDefaultOrdered));

                Helper.ForEachEnumerableExpression(
                    oneItemDefaultOrdered,
                    new int[0],
                    res => { },
                    @"(a, b) =>
                      {
                        Assert.IsFalse(a.Zip(b, (x, y) => x).Any());    

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
                Assert.IsFalse(oneItemSpecificOrdered.Zip(empty, (a, b) => a).Any());
                Assert.IsFalse(oneItemSpecificOrdered.Zip(emptyOrdered, (a, b) => a).Any());
                Assert.IsTrue(oneItemSpecificOrdered.Zip(groupByDefault, (a, b) => a).SequenceEqual(oneItemSpecificOrdered));
                Assert.IsTrue(oneItemSpecificOrdered.Zip(groupBySpecific, (a, b) => a).SequenceEqual(oneItemSpecificOrdered));
                Assert.IsTrue(oneItemSpecificOrdered.Zip(lookup, (a, b) => a).SequenceEqual(oneItemSpecificOrdered));
                Assert.IsTrue(oneItemSpecificOrdered.Zip(range, (a, b) => a).SequenceEqual(oneItemSpecificOrdered));
                Assert.IsTrue(oneItemSpecificOrdered.Zip(repeat, (a, b) => a).SequenceEqual(oneItemSpecificOrdered));
                Assert.IsTrue(oneItemSpecificOrdered.Zip(reverseRange, (a, b) => a).SequenceEqual(oneItemSpecificOrdered));

                var defaultIfEmptyDefault = new int[0].DefaultIfEmpty();
                Assert.IsTrue(oneItemSpecificOrdered.Zip(defaultIfEmptyDefault, (a, b) => a).SequenceEqual(oneItemSpecificOrdered));
                var defaultIfEmptySpecific = new int[0].DefaultIfEmpty(4);
                Assert.IsTrue(oneItemSpecificOrdered.Zip(defaultIfEmptySpecific, (a, b) => a).SequenceEqual(oneItemSpecificOrdered));

                Assert.IsTrue(oneItemSpecificOrdered.Zip(oneItemDefault, (a, b) => a).SequenceEqual(oneItemSpecificOrdered));
                Assert.IsTrue(oneItemSpecificOrdered.Zip(oneItemSpecific, (a, b) => a).SequenceEqual(oneItemSpecificOrdered));
                Assert.IsTrue(oneItemSpecificOrdered.Zip(oneItemDefaultOrdered, (a, b) => a).SequenceEqual(oneItemSpecificOrdered));
                Assert.IsTrue(oneItemSpecificOrdered.Zip(oneItemSpecificOrdered, (a, b) => a).SequenceEqual(oneItemSpecificOrdered));

                Helper.ForEachEnumerableExpression(
                    oneItemSpecificOrdered,
                    new int[0],
                    res => { },
                    @"(a, b) =>
                      {
                        Assert.IsFalse(a.Zip(b, (x, y) => x).Any());    

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
        public void Errors()
        {
            Helper.ForEachEnumerableNoRetExpression(
                new[] { 1, 2, 3 },
                @"a =>
                  Helper.ForEachEnumerableExpression(
                    a,
                    new [] { 4, 5, 6 },
                    res => { },
                    @""(a, b) => 
                       {
                        try
                        {
                            a.Zip(b, default(Func<int, int, int>));
                            Assert.Fail();
                        }
                        catch(ArgumentNullException exc)
                        {
                            Assert.AreEqual(""""resultSelector"""", exc.ParamName);
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
                try { empty.Zip(empty, default(Func<int, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { empty.Zip(emptyOrdered, default(Func<int, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { empty.Zip(groupByDefault, default(Func<int, GroupingEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { empty.Zip(groupBySpecific, default(Func<int, GroupingEnumerable<string, string>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { empty.Zip(lookup, default(Func<int, GroupingEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { empty.Zip(range, default(Func<int, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { empty.Zip(repeat, default(Func<int, string, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { empty.Zip(reverseRange, default(Func<int, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { empty.Zip(oneItemDefault, default(Func<int, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { empty.Zip(oneItemSpecific, default(Func<int, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { empty.Zip(oneItemDefaultOrdered, default(Func<int, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { empty.Zip(oneItemSpecificOrdered, default(Func<int, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                Helper.ForEachEnumerableExpression(
                    empty,
                    new int[0],
                    res => { },
                    @"(a, b) =>
                      {
                        try
                        {
                            a.Zip(b, default(Func<int, int, int>));
                            Assert.Fail();
                        }
                        catch(ArgumentNullException exc)
                        {
                            Assert.AreEqual(""resultSelector"", exc.ParamName);
                        }

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
                try { emptyOrdered.Zip(empty, default(Func<int, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { emptyOrdered.Zip(emptyOrdered, default(Func<int, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { emptyOrdered.Zip(groupByDefault, default(Func<int, GroupingEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { emptyOrdered.Zip(groupBySpecific, default(Func<int, GroupingEnumerable<string, string>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { emptyOrdered.Zip(lookup, default(Func<int, GroupingEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { emptyOrdered.Zip(range, default(Func<int, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { emptyOrdered.Zip(repeat, default(Func<int, string, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { emptyOrdered.Zip(reverseRange, default(Func<int, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { emptyOrdered.Zip(oneItemDefault, default(Func<int, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { emptyOrdered.Zip(oneItemSpecific, default(Func<int, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { emptyOrdered.Zip(oneItemDefaultOrdered, default(Func<int, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { emptyOrdered.Zip(oneItemSpecificOrdered, default(Func<int, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                Helper.ForEachEnumerableExpression(
                    emptyOrdered,
                    new int[0],
                    res => { },
                    @"(a, b) =>
                      {
                        try
                        {
                            a.Zip(b, default(Func<int, int, int>));
                            Assert.Fail();
                        }
                        catch(ArgumentNullException exc)
                        {
                            Assert.AreEqual(""resultSelector"", exc.ParamName);
                        }

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
                try { groupByDefault.Zip(empty, default(Func<GroupingEnumerable<int, int>, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { groupByDefault.Zip(emptyOrdered, default(Func<GroupingEnumerable<int, int>, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { groupByDefault.Zip(groupByDefault, default(Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { groupByDefault.Zip(groupBySpecific, default(Func<GroupingEnumerable<int, int>, GroupingEnumerable<string, string>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { groupByDefault.Zip(lookup, default(Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { groupByDefault.Zip(range, default(Func<GroupingEnumerable<int, int>, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { groupByDefault.Zip(repeat, default(Func<GroupingEnumerable<int, int>, string, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { groupByDefault.Zip(reverseRange, default(Func<GroupingEnumerable<int, int>, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { groupByDefault.Zip(oneItemDefault, default(Func<GroupingEnumerable<int, int>, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { groupByDefault.Zip(oneItemSpecific, default(Func<GroupingEnumerable<int, int>, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { groupByDefault.Zip(oneItemDefaultOrdered, default(Func<GroupingEnumerable<int, int>, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { groupByDefault.Zip(oneItemSpecificOrdered, default(Func<GroupingEnumerable<int, int>, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                Helper.ForEachEnumerableExpression(
                    groupByDefault,
                    new int[0],
                    res => { },
                    @"(a, b) =>
                      {
                        try
                        {
                            a.Zip(b, default(Func<GroupingEnumerable<int, int>, int, int>));
                            Assert.Fail();
                        }
                        catch(ArgumentNullException exc)
                        {
                            Assert.AreEqual(""resultSelector"", exc.ParamName);
                        }

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
                try { groupBySpecific.Zip(empty, default(Func<GroupingEnumerable<string, string>, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { groupBySpecific.Zip(emptyOrdered, default(Func<GroupingEnumerable<string, string>, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { groupBySpecific.Zip(groupByDefault, default(Func<GroupingEnumerable<string, string>, GroupingEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { groupBySpecific.Zip(groupBySpecific, default(Func<GroupingEnumerable<string, string>, GroupingEnumerable<string, string>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { groupBySpecific.Zip(lookup, default(Func<GroupingEnumerable<string, string>, GroupingEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { groupBySpecific.Zip(range, default(Func<GroupingEnumerable<string, string>, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { groupBySpecific.Zip(repeat, default(Func<GroupingEnumerable<string, string>, string, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { groupBySpecific.Zip(reverseRange, default(Func<GroupingEnumerable<string, string>, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { groupBySpecific.Zip(oneItemDefault, default(Func<GroupingEnumerable<string, string>, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { groupBySpecific.Zip(oneItemSpecific, default(Func<GroupingEnumerable<string, string>, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { groupBySpecific.Zip(oneItemDefaultOrdered, default(Func<GroupingEnumerable<string, string>, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { groupBySpecific.Zip(oneItemSpecificOrdered, default(Func<GroupingEnumerable<string, string>, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                Helper.ForEachEnumerableExpression(
                    groupBySpecific,
                    new int[0],
                    res => { },
                    @"(a, b) =>
                      {
                        try
                        {
                            a.Zip(b, default(Func<GroupingEnumerable<string, string>, int, int>));
                            Assert.Fail();
                        }
                        catch(ArgumentNullException exc)
                        {
                            Assert.AreEqual(""resultSelector"", exc.ParamName);
                        }

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
                try { lookup.Zip(empty, default(Func<GroupingEnumerable<int, int>, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { lookup.Zip(emptyOrdered, default(Func<GroupingEnumerable<int, int>, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { lookup.Zip(groupByDefault, default(Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { lookup.Zip(groupBySpecific, default(Func<GroupingEnumerable<int, int>, GroupingEnumerable<string, string>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { lookup.Zip(lookup, default(Func<GroupingEnumerable<int, int>, GroupingEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { lookup.Zip(range, default(Func<GroupingEnumerable<int, int>, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { lookup.Zip(repeat, default(Func<GroupingEnumerable<int, int>, string, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { lookup.Zip(reverseRange, default(Func<GroupingEnumerable<int, int>, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { lookup.Zip(oneItemDefault, default(Func<GroupingEnumerable<int, int>, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { lookup.Zip(oneItemSpecific, default(Func<GroupingEnumerable<int, int>, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { lookup.Zip(oneItemDefaultOrdered, default(Func<GroupingEnumerable<int, int>, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { lookup.Zip(oneItemSpecificOrdered, default(Func<GroupingEnumerable<int, int>, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                Helper.ForEachEnumerableExpression(
                    lookup,
                    new int[0],
                    res => { },
                    @"(a, b) =>
                      {
                        try
                        {
                            a.Zip(b, default(Func<GroupingEnumerable<int, int>, int, int>));
                            Assert.Fail();
                        }
                        catch(ArgumentNullException exc)
                        {
                            Assert.AreEqual(""resultSelector"", exc.ParamName);
                        }

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
                try { range.Zip(empty, default(Func<int, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { range.Zip(emptyOrdered, default(Func<int, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { range.Zip(groupByDefault, default(Func<int, GroupingEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { range.Zip(groupBySpecific, default(Func<int, GroupingEnumerable<string, string>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { range.Zip(lookup, default(Func<int, GroupingEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { range.Zip(range, default(Func<int, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { range.Zip(repeat, default(Func<int, string, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { range.Zip(reverseRange, default(Func<int, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { range.Zip(oneItemDefault, default(Func<int, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { range.Zip(oneItemSpecific, default(Func<int, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { range.Zip(oneItemDefaultOrdered, default(Func<int, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { range.Zip(oneItemSpecificOrdered, default(Func<int, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                Helper.ForEachEnumerableExpression(
                    range,
                    new int[0],
                    res => { },
                    @"(a, b) =>
                      {
                        try
                        {
                            a.Zip(b, default(Func<int, int, int>));
                            Assert.Fail();
                        }
                        catch(ArgumentNullException exc)
                        {
                            Assert.AreEqual(""resultSelector"", exc.ParamName);
                        }

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
                try { repeat.Zip(empty, default(Func<string, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { repeat.Zip(emptyOrdered, default(Func<string, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { repeat.Zip(groupByDefault, default(Func<string, GroupingEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { repeat.Zip(groupBySpecific, default(Func<string, GroupingEnumerable<string, string>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { repeat.Zip(lookup, default(Func<string, GroupingEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { repeat.Zip(range, default(Func<string, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { repeat.Zip(repeat, default(Func<string, string, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { repeat.Zip(reverseRange, default(Func<string, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { repeat.Zip(oneItemDefault, default(Func<string, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { repeat.Zip(oneItemSpecific, default(Func<string, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { repeat.Zip(oneItemDefaultOrdered, default(Func<string, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { repeat.Zip(oneItemSpecificOrdered, default(Func<string, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                Helper.ForEachEnumerableExpression(
                    repeat,
                    new int[0],
                    res => { },
                    @"(a, b) =>
                      {
                        try
                        {
                            a.Zip(b, default(Func<string, int, int>));
                            Assert.Fail();
                        }
                        catch(ArgumentNullException exc)
                        {
                            Assert.AreEqual(""resultSelector"", exc.ParamName);
                        }

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
                try { reverseRange.Zip(empty, default(Func<int, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { reverseRange.Zip(emptyOrdered, default(Func<int, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { reverseRange.Zip(groupByDefault, default(Func<int, GroupingEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { reverseRange.Zip(groupBySpecific, default(Func<int, GroupingEnumerable<string, string>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { reverseRange.Zip(lookup, default(Func<int, GroupingEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { reverseRange.Zip(range, default(Func<int, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { reverseRange.Zip(repeat, default(Func<int, string, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { reverseRange.Zip(reverseRange, default(Func<int, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { reverseRange.Zip(oneItemDefault, default(Func<int, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { reverseRange.Zip(oneItemSpecific, default(Func<int, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { reverseRange.Zip(oneItemDefaultOrdered, default(Func<int, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { reverseRange.Zip(oneItemSpecificOrdered, default(Func<int, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                Helper.ForEachEnumerableExpression(
                    reverseRange,
                    new int[0],
                    res => { },
                    @"(a, b) =>
                      {
                        try
                        {
                            a.Zip(b, default(Func<int, int, int>));
                            Assert.Fail();
                        }
                        catch(ArgumentNullException exc)
                        {
                            Assert.AreEqual(""resultSelector"", exc.ParamName);
                        }

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
                try { oneItemDefault.Zip(empty, default(Func<int, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemDefault.Zip(emptyOrdered, default(Func<int, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemDefault.Zip(groupByDefault, default(Func<int, GroupingEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemDefault.Zip(groupBySpecific, default(Func<int, GroupingEnumerable<string, string>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemDefault.Zip(lookup, default(Func<int, GroupingEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemDefault.Zip(range, default(Func<int, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemDefault.Zip(repeat, default(Func<int, string, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemDefault.Zip(reverseRange, default(Func<int, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemDefault.Zip(oneItemDefault, default(Func<int, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemDefault.Zip(oneItemSpecific, default(Func<int, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemDefault.Zip(oneItemDefaultOrdered, default(Func<int, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemDefault.Zip(oneItemSpecificOrdered, default(Func<int, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                Helper.ForEachEnumerableExpression(
                    oneItemDefault,
                    new int[0],
                    res => { },
                    @"(a, b) =>
                      {
                        try
                        {
                            a.Zip(b, default(Func<int, int, int>));
                            Assert.Fail();
                        }
                        catch(ArgumentNullException exc)
                        {
                            Assert.AreEqual(""resultSelector"", exc.ParamName);
                        }

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
                try { oneItemSpecific.Zip(empty, default(Func<int, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemSpecific.Zip(emptyOrdered, default(Func<int, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemSpecific.Zip(groupByDefault, default(Func<int, GroupingEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemSpecific.Zip(groupBySpecific, default(Func<int, GroupingEnumerable<string, string>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemSpecific.Zip(lookup, default(Func<int, GroupingEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemSpecific.Zip(range, default(Func<int, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemSpecific.Zip(repeat, default(Func<int, string, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemSpecific.Zip(reverseRange, default(Func<int, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemSpecific.Zip(oneItemDefault, default(Func<int, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemSpecific.Zip(oneItemSpecific, default(Func<int, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemSpecific.Zip(oneItemDefaultOrdered, default(Func<int, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemSpecific.Zip(oneItemSpecificOrdered, default(Func<int, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                Helper.ForEachEnumerableExpression(
                    oneItemSpecific,
                    new int[0],
                    res => { },
                    @"(a, b) =>
                      {
                        try
                        {
                            a.Zip(b, default(Func<int, int, int>));
                            Assert.Fail();
                        }
                        catch(ArgumentNullException exc)
                        {
                            Assert.AreEqual(""resultSelector"", exc.ParamName);
                        }

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
                try { oneItemDefaultOrdered.Zip(empty, default(Func<int, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemDefaultOrdered.Zip(emptyOrdered, default(Func<int, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemDefaultOrdered.Zip(groupByDefault, default(Func<int, GroupingEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemDefaultOrdered.Zip(groupBySpecific, default(Func<int, GroupingEnumerable<string, string>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemDefaultOrdered.Zip(lookup, default(Func<int, GroupingEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemDefaultOrdered.Zip(range, default(Func<int, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemDefaultOrdered.Zip(repeat, default(Func<int, string, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemDefaultOrdered.Zip(reverseRange, default(Func<int, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemDefaultOrdered.Zip(oneItemDefault, default(Func<int, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemDefaultOrdered.Zip(oneItemSpecific, default(Func<int, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemDefaultOrdered.Zip(oneItemDefaultOrdered, default(Func<int, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemDefaultOrdered.Zip(oneItemSpecificOrdered, default(Func<int, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                Helper.ForEachEnumerableExpression(
                    oneItemDefaultOrdered,
                    new int[0],
                    res => { },
                    @"(a, b) =>
                      {
                        try
                        {
                            a.Zip(b, default(Func<int, int, int>));
                            Assert.Fail();
                        }
                        catch(ArgumentNullException exc)
                        {
                            Assert.AreEqual(""resultSelector"", exc.ParamName);
                        }

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
                try { oneItemSpecificOrdered.Zip(empty, default(Func<int, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemSpecificOrdered.Zip(emptyOrdered, default(Func<int, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemSpecificOrdered.Zip(groupByDefault, default(Func<int, GroupingEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemSpecificOrdered.Zip(groupBySpecific, default(Func<int, GroupingEnumerable<string, string>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemSpecificOrdered.Zip(lookup, default(Func<int, GroupingEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemSpecificOrdered.Zip(range, default(Func<int, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemSpecificOrdered.Zip(repeat, default(Func<int, string, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemSpecificOrdered.Zip(reverseRange, default(Func<int, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemSpecificOrdered.Zip(oneItemDefault, default(Func<int, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemSpecificOrdered.Zip(oneItemSpecific, default(Func<int, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemSpecificOrdered.Zip(oneItemDefaultOrdered, default(Func<int, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }
                try { oneItemSpecificOrdered.Zip(oneItemSpecificOrdered, default(Func<int, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("resultSelector", exc.ParamName); }

                Helper.ForEachEnumerableExpression(
                    oneItemSpecificOrdered,
                    new int[0],
                    res => { },
                    @"(a, b) =>
                      {
                        try
                        {
                            a.Zip(b, default(Func<int, int, int>));
                            Assert.Fail();
                        }
                        catch(ArgumentNullException exc)
                        {
                            Assert.AreEqual(""resultSelector"", exc.ParamName);
                        }

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
        public void Malformed1()
        {
            Helper.ForEachMalformedEnumerableExpression<string>(
                @"a =>
                  Helper.ForEachEnumerableExpression(
                    a,
                    new string[0],
                    res => { },
                    @""(a,b) =>
                       {
                        try
                        {
                            a.Zip(b, (x, y) => x + y);
                            Assert.Fail();
                        }
                        catch(ArgumentException exc)
                        {
                            Assert.AreEqual(""""first"""", exc.ParamName);
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
        public void Malformed2()
        {
            Helper.ForEachMalformedEnumerableExpression<string>(
                @"a =>
                  Helper.ForEachEnumerableExpression(
                    a,
                    new string[0],
                    res => { },
                    @""(a,b) =>
                       {
                        try
                        {
                            b.Zip(a, (x, y) => x + y);
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
                try { empty.Zip(emptyGood, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { emptyGood.Zip(empty, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var emptyOrderedGood = Enumerable.Empty<int>().OrderBy(x => x);
                try { empty.Zip(emptyOrderedGood, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { emptyOrderedGood.Zip(empty, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var groupByDefaultGood = new[] { 1 }.GroupBy(x => x);
                try { empty.Zip(groupByDefaultGood, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { groupByDefaultGood.Zip(empty, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var groupBySpecificGood = new[] { "foo" }.GroupBy(x => x, StringComparer.InvariantCultureIgnoreCase);
                try { empty.Zip(groupBySpecificGood, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { groupBySpecificGood.Zip(empty, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var lookupGood = new[] { 1 }.ToLookup(x => x);
                try { empty.Zip(lookupGood, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { lookupGood.Zip(empty, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var rangeGood = Enumerable.Range(1, 1);
                try { empty.Zip(rangeGood, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { rangeGood.Zip(empty, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var repeatGood = Enumerable.Repeat(1, 1);
                try { empty.Zip(repeatGood, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { repeatGood.Zip(empty, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var reverseRangeGood = Enumerable.Range(1, 1).Reverse();
                try { empty.Zip(reverseRangeGood, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { reverseRangeGood.Zip(empty, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var oneItemDefaultGood = Enumerable.Empty<int>().DefaultIfEmpty();
                try { empty.Zip(oneItemDefaultGood, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemDefaultGood.Zip(empty, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var oneItemSpecificGood = Enumerable.Empty<int>().DefaultIfEmpty(4);
                try { empty.Zip(oneItemSpecificGood, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemSpecificGood.Zip(empty, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var oneItemDefaultOrderedGood = oneItemDefaultGood.OrderBy(x => x);
                try { empty.Zip(oneItemDefaultOrderedGood, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemDefaultOrderedGood.Zip(empty, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var oneItemSpecificOrderedGood = oneItemSpecificGood.OrderBy(x => x);
                try { empty.Zip(oneItemSpecificOrderedGood, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemSpecificOrderedGood.Zip(empty, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                Helper.ForEachEnumerableExpression(
                    empty,
                    new int[0],
                    res => { },
                    @"(a, b) =>
                      {
                        try { a.Zip(b, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""first"", exc.ParamName); }
                        try { b.Zip(a, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""second"", exc.ParamName); }

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
                try { emptyOrdered.Zip(emptyGood, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { emptyGood.Zip(emptyOrdered, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var emptyOrderedGood = Enumerable.Empty<int>().OrderBy(x => x);
                try { emptyOrdered.Zip(emptyOrderedGood, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { emptyOrderedGood.Zip(emptyOrdered, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var groupByDefaultGood = new[] { 1 }.GroupBy(x => x);
                try { emptyOrdered.Zip(groupByDefaultGood, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { groupByDefaultGood.Zip(emptyOrdered, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var groupBySpecificGood = new[] { "foo" }.GroupBy(x => x, StringComparer.InvariantCultureIgnoreCase);
                try { emptyOrdered.Zip(groupBySpecificGood, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { groupBySpecificGood.Zip(emptyOrdered, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var lookupGood = new[] { 1 }.ToLookup(x => x);
                try { emptyOrdered.Zip(lookupGood, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { lookupGood.Zip(emptyOrdered, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var rangeGood = Enumerable.Range(1, 1);
                try { emptyOrdered.Zip(rangeGood, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { rangeGood.Zip(emptyOrdered, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var repeatGood = Enumerable.Repeat(1, 1);
                try { emptyOrdered.Zip(repeatGood, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { repeatGood.Zip(emptyOrdered, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var reverseRangeGood = Enumerable.Range(1, 1).Reverse();
                try { emptyOrdered.Zip(reverseRangeGood, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { reverseRangeGood.Zip(emptyOrdered, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var oneItemDefaultGood = Enumerable.Empty<int>().DefaultIfEmpty();
                try { emptyOrdered.Zip(oneItemDefaultGood, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemDefaultGood.Zip(emptyOrdered, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var oneItemSpecificGood = Enumerable.Empty<int>().DefaultIfEmpty(4);
                try { emptyOrdered.Zip(oneItemSpecificGood, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemSpecificGood.Zip(emptyOrdered, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var oneItemDefaultOrderedGood = oneItemDefaultGood.OrderBy(x => x);
                try { emptyOrdered.Zip(oneItemDefaultOrderedGood, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemDefaultOrderedGood.Zip(emptyOrdered, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var oneItemSpecificOrderedGood = oneItemSpecificGood.OrderBy(x => x);
                try { emptyOrdered.Zip(oneItemSpecificOrderedGood, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemSpecificOrderedGood.Zip(emptyOrdered, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                Helper.ForEachEnumerableExpression(
                    emptyOrdered,
                    new int[0],
                    res => { },
                    @"(a, b) =>
                      {
                        try { a.Zip(b, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""first"", exc.ParamName); }
                        try { b.Zip(a, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""second"", exc.ParamName); }

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
                var emptyGood = Enumerable.Empty<int>();
                try { groupByDefault.Zip(emptyGood, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { emptyGood.Zip(groupByDefault, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var emptyOrderedGood = Enumerable.Empty<int>().OrderBy(x => x);
                try { groupByDefault.Zip(emptyOrderedGood, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { emptyOrderedGood.Zip(groupByDefault, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var groupByDefaultGood = new[] { 1 }.GroupBy(x => x);
                try { groupByDefault.Zip(groupByDefaultGood, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { groupByDefaultGood.Zip(groupByDefault, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var groupBySpecificGood = new[] { "foo" }.GroupBy(x => x, StringComparer.InvariantCultureIgnoreCase);
                try { groupByDefault.Zip(groupBySpecificGood, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { groupBySpecificGood.Zip(groupByDefault, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var lookupGood = new[] { 1 }.ToLookup(x => x);
                try { groupByDefault.Zip(lookupGood, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { lookupGood.Zip(groupByDefault, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var rangeGood = Enumerable.Range(1, 1);
                try { groupByDefault.Zip(rangeGood, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { rangeGood.Zip(groupByDefault, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var repeatGood = Enumerable.Repeat(1, 1);
                try { groupByDefault.Zip(repeatGood, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { repeatGood.Zip(groupByDefault, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var reverseRangeGood = Enumerable.Range(1, 1).Reverse();
                try { groupByDefault.Zip(reverseRangeGood, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { reverseRangeGood.Zip(groupByDefault, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var oneItemDefaultGood = Enumerable.Empty<int>().DefaultIfEmpty();
                try { groupByDefault.Zip(oneItemDefaultGood, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemDefaultGood.Zip(groupByDefault, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var oneItemSpecificGood = Enumerable.Empty<int>().DefaultIfEmpty(4);
                try { groupByDefault.Zip(oneItemSpecificGood, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemSpecificGood.Zip(groupByDefault, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var oneItemDefaultOrderedGood = oneItemDefaultGood.OrderBy(x => x);
                try { groupByDefault.Zip(oneItemDefaultOrderedGood, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemDefaultOrderedGood.Zip(groupByDefault, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var oneItemSpecificOrderedGood = oneItemSpecificGood.OrderBy(x => x);
                try { groupByDefault.Zip(oneItemSpecificOrderedGood, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemSpecificOrderedGood.Zip(groupByDefault, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                Helper.ForEachEnumerableExpression(
                    groupByDefault,
                    new int[0],
                    res => { },
                    @"(a, b) =>
                      {
                        try { a.Zip(b, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""first"", exc.ParamName); }
                        try { b.Zip(a, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""second"", exc.ParamName); }

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
                var emptyGood = Enumerable.Empty<int>();
                try { groupBySpecific.Zip(emptyGood, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { emptyGood.Zip(groupBySpecific, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var emptyOrderedGood = Enumerable.Empty<int>().OrderBy(x => x);
                try { groupBySpecific.Zip(emptyOrderedGood, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { emptyOrderedGood.Zip(groupBySpecific, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var groupByDefaultGood = new[] { 1 }.GroupBy(x => x);
                try { groupBySpecific.Zip(groupByDefaultGood, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { groupByDefaultGood.Zip(groupBySpecific, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var groupBySpecificGood = new[] { "foo" }.GroupBy(x => x, StringComparer.InvariantCultureIgnoreCase);
                try { groupBySpecific.Zip(groupBySpecificGood, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { groupBySpecificGood.Zip(groupBySpecific, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var lookupGood = new[] { 1 }.ToLookup(x => x);
                try { groupBySpecific.Zip(lookupGood, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { lookupGood.Zip(groupBySpecific, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var rangeGood = Enumerable.Range(1, 1);
                try { groupBySpecific.Zip(rangeGood, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { rangeGood.Zip(groupBySpecific, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var repeatGood = Enumerable.Repeat(1, 1);
                try { groupBySpecific.Zip(repeatGood, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { repeatGood.Zip(groupBySpecific, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var reverseRangeGood = Enumerable.Range(1, 1).Reverse();
                try { groupBySpecific.Zip(reverseRangeGood, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { reverseRangeGood.Zip(groupBySpecific, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var oneItemDefaultGood = Enumerable.Empty<int>().DefaultIfEmpty();
                try { groupBySpecific.Zip(oneItemDefaultGood, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemDefaultGood.Zip(groupBySpecific, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var oneItemSpecificGood = Enumerable.Empty<int>().DefaultIfEmpty(4);
                try { groupBySpecific.Zip(oneItemSpecificGood, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemSpecificGood.Zip(groupBySpecific, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var oneItemDefaultOrderedGood = oneItemDefaultGood.OrderBy(x => x);
                try { groupBySpecific.Zip(oneItemDefaultOrderedGood, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemDefaultOrderedGood.Zip(groupBySpecific, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var oneItemSpecificOrderedGood = oneItemSpecificGood.OrderBy(x => x);
                try { groupBySpecific.Zip(oneItemSpecificOrderedGood, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemSpecificOrderedGood.Zip(groupBySpecific, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                Helper.ForEachEnumerableExpression(
                    groupBySpecific,
                    new int[0],
                    res => { },
                    @"(a, b) =>
                      {
                        try { a.Zip(b, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""first"", exc.ParamName); }
                        try { b.Zip(a, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""second"", exc.ParamName); }

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
                var emptyGood = Enumerable.Empty<int>();
                try { lookup.Zip(emptyGood, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { emptyGood.Zip(lookup, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var emptyOrderedGood = Enumerable.Empty<int>().OrderBy(x => x);
                try { lookup.Zip(emptyOrderedGood, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { emptyOrderedGood.Zip(lookup, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var groupByDefaultGood = new[] { 1 }.GroupBy(x => x);
                try { lookup.Zip(groupByDefaultGood, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { groupByDefaultGood.Zip(lookup, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var groupBySpecificGood = new[] { "foo" }.GroupBy(x => x, StringComparer.InvariantCultureIgnoreCase);
                try { lookup.Zip(groupBySpecificGood, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { groupBySpecificGood.Zip(lookup, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var lookupGood = new[] { 1 }.ToLookup(x => x);
                try { lookup.Zip(lookupGood, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { lookupGood.Zip(lookup, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var rangeGood = Enumerable.Range(1, 1);
                try { lookup.Zip(rangeGood, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { rangeGood.Zip(lookup, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var repeatGood = Enumerable.Repeat(1, 1);
                try { lookup.Zip(repeatGood, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { repeatGood.Zip(lookup, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var reverseRangeGood = Enumerable.Range(1, 1).Reverse();
                try { lookup.Zip(reverseRangeGood, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { reverseRangeGood.Zip(lookup, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var oneItemDefaultGood = Enumerable.Empty<int>().DefaultIfEmpty();
                try { lookup.Zip(oneItemDefaultGood, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemDefaultGood.Zip(lookup, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var oneItemSpecificGood = Enumerable.Empty<int>().DefaultIfEmpty(4);
                try { lookup.Zip(oneItemSpecificGood, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemSpecificGood.Zip(lookup, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var oneItemDefaultOrderedGood = oneItemDefaultGood.OrderBy(x => x);
                try { lookup.Zip(oneItemDefaultOrderedGood, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemDefaultOrderedGood.Zip(lookup, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var oneItemSpecificOrderedGood = oneItemSpecificGood.OrderBy(x => x);
                try { lookup.Zip(oneItemSpecificOrderedGood, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemSpecificOrderedGood.Zip(lookup, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                Helper.ForEachEnumerableExpression(
                    lookup,
                    new int[0],
                    res => { },
                    @"(a, b) =>
                      {
                        try { a.Zip(b, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""first"", exc.ParamName); }
                        try { b.Zip(a, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""second"", exc.ParamName); }

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
                try { range.Zip(emptyGood, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { emptyGood.Zip(range, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var emptyOrderedGood = Enumerable.Empty<int>().OrderBy(x => x);
                try { range.Zip(emptyOrderedGood, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { emptyOrderedGood.Zip(range, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var groupByDefaultGood = new[] { 1 }.GroupBy(x => x);
                try { range.Zip(groupByDefaultGood, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { groupByDefaultGood.Zip(range, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var groupBySpecificGood = new[] { "foo" }.GroupBy(x => x, StringComparer.InvariantCultureIgnoreCase);
                try { range.Zip(groupBySpecificGood, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { groupBySpecificGood.Zip(range, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var lookupGood = new[] { 1 }.ToLookup(x => x);
                try { range.Zip(lookupGood, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { lookupGood.Zip(range, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var rangeGood = Enumerable.Range(1, 1);
                try { range.Zip(rangeGood, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { rangeGood.Zip(range, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var repeatGood = Enumerable.Repeat(1, 1);
                try { range.Zip(repeatGood, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { repeatGood.Zip(range, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var reverseRangeGood = Enumerable.Range(1, 1).Reverse();
                try { range.Zip(reverseRangeGood, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { reverseRangeGood.Zip(range, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var oneItemDefaultGood = Enumerable.Empty<int>().DefaultIfEmpty();
                try { range.Zip(oneItemDefaultGood, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemDefaultGood.Zip(range, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var oneItemSpecificGood = Enumerable.Empty<int>().DefaultIfEmpty(4);
                try { range.Zip(oneItemSpecificGood, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemSpecificGood.Zip(range, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var oneItemDefaultOrderedGood = oneItemDefaultGood.OrderBy(x => x);
                try { range.Zip(oneItemDefaultOrderedGood, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemDefaultOrderedGood.Zip(range, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var oneItemSpecificOrderedGood = oneItemSpecificGood.OrderBy(x => x);
                try { range.Zip(oneItemSpecificOrderedGood, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemSpecificOrderedGood.Zip(range, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                Helper.ForEachEnumerableExpression(
                    range,
                    new int[0],
                    res => { },
                    @"(a, b) =>
                      {
                        try { a.Zip(b, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""first"", exc.ParamName); }
                        try { b.Zip(a, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""second"", exc.ParamName); }

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
                try { repeat.Zip(emptyGood, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { emptyGood.Zip(repeat, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var emptyOrderedGood = Enumerable.Empty<int>().OrderBy(x => x);
                try { repeat.Zip(emptyOrderedGood, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { emptyOrderedGood.Zip(repeat, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var groupByDefaultGood = new[] { 1 }.GroupBy(x => x);
                try { repeat.Zip(groupByDefaultGood, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { groupByDefaultGood.Zip(repeat, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var groupBySpecificGood = new[] { "foo" }.GroupBy(x => x, StringComparer.InvariantCultureIgnoreCase);
                try { repeat.Zip(groupBySpecificGood, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { groupBySpecificGood.Zip(repeat, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var lookupGood = new[] { 1 }.ToLookup(x => x);
                try { repeat.Zip(lookupGood, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { lookupGood.Zip(repeat, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var rangeGood = Enumerable.Range(1, 1);
                try { repeat.Zip(rangeGood, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { rangeGood.Zip(repeat, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var repeatGood = Enumerable.Repeat(1, 1);
                try { repeat.Zip(repeatGood, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { repeatGood.Zip(repeat, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var reverseRangeGood = Enumerable.Range(1, 1).Reverse();
                try { repeat.Zip(reverseRangeGood, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { reverseRangeGood.Zip(repeat, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var oneItemDefaultGood = Enumerable.Empty<int>().DefaultIfEmpty();
                try { repeat.Zip(oneItemDefaultGood, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemDefaultGood.Zip(repeat, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var oneItemSpecificGood = Enumerable.Empty<int>().DefaultIfEmpty(4);
                try { repeat.Zip(oneItemSpecificGood, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemSpecificGood.Zip(repeat, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var oneItemDefaultOrderedGood = oneItemDefaultGood.OrderBy(x => x);
                try { repeat.Zip(oneItemDefaultOrderedGood, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemDefaultOrderedGood.Zip(repeat, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var oneItemSpecificOrderedGood = oneItemSpecificGood.OrderBy(x => x);
                try { repeat.Zip(oneItemSpecificOrderedGood, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemSpecificOrderedGood.Zip(repeat, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                Helper.ForEachEnumerableExpression(
                    repeat,
                    new int[0],
                    res => { },
                    @"(a, b) =>
                      {
                        try { a.Zip(b, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""first"", exc.ParamName); }
                        try { b.Zip(a, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""second"", exc.ParamName); }

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
                try { reverseRange.Zip(emptyGood, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { emptyGood.Zip(reverseRange, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var emptyOrderedGood = Enumerable.Empty<int>().OrderBy(x => x);
                try { reverseRange.Zip(emptyOrderedGood, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { emptyOrderedGood.Zip(reverseRange, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var groupByDefaultGood = new[] { 1 }.GroupBy(x => x);
                try { reverseRange.Zip(groupByDefaultGood, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { groupByDefaultGood.Zip(reverseRange, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var groupBySpecificGood = new[] { "foo" }.GroupBy(x => x, StringComparer.InvariantCultureIgnoreCase);
                try { reverseRange.Zip(groupBySpecificGood, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { groupBySpecificGood.Zip(reverseRange, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var lookupGood = new[] { 1 }.ToLookup(x => x);
                try { reverseRange.Zip(lookupGood, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { lookupGood.Zip(reverseRange, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var rangeGood = Enumerable.Range(1, 1);
                try { reverseRange.Zip(rangeGood, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { rangeGood.Zip(reverseRange, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var repeatGood = Enumerable.Repeat(1, 1);
                try { reverseRange.Zip(repeatGood, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { repeatGood.Zip(reverseRange, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var reverseRangeGood = Enumerable.Range(1, 1).Reverse();
                try { reverseRange.Zip(reverseRangeGood, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { reverseRangeGood.Zip(reverseRange, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var oneItemDefaultGood = Enumerable.Empty<int>().DefaultIfEmpty();
                try { reverseRange.Zip(oneItemDefaultGood, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemDefaultGood.Zip(reverseRange, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var oneItemSpecificGood = Enumerable.Empty<int>().DefaultIfEmpty(4);
                try { reverseRange.Zip(oneItemSpecificGood, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemSpecificGood.Zip(reverseRange, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var oneItemDefaultOrderedGood = oneItemDefaultGood.OrderBy(x => x);
                try { reverseRange.Zip(oneItemDefaultOrderedGood, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemDefaultOrderedGood.Zip(reverseRange, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var oneItemSpecificOrderedGood = oneItemSpecificGood.OrderBy(x => x);
                try { reverseRange.Zip(oneItemSpecificOrderedGood, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemSpecificOrderedGood.Zip(reverseRange, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                Helper.ForEachEnumerableExpression(
                    reverseRange,
                    new int[0],
                    res => { },
                    @"(a, b) =>
                      {
                        try { a.Zip(b, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""first"", exc.ParamName); }
                        try { b.Zip(a, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""second"", exc.ParamName); }

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
                try { oneItemDefault.Zip(emptyGood, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { emptyGood.Zip(oneItemDefault, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var emptyOrderedGood = Enumerable.Empty<int>().OrderBy(x => x);
                try { oneItemDefault.Zip(emptyOrderedGood, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { emptyOrderedGood.Zip(oneItemDefault, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var groupByDefaultGood = new[] { 1 }.GroupBy(x => x);
                try { oneItemDefault.Zip(groupByDefaultGood, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { groupByDefaultGood.Zip(oneItemDefault, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var groupBySpecificGood = new[] { "foo" }.GroupBy(x => x, StringComparer.InvariantCultureIgnoreCase);
                try { oneItemDefault.Zip(groupBySpecificGood, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { groupBySpecificGood.Zip(oneItemDefault, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var lookupGood = new[] { 1 }.ToLookup(x => x);
                try { oneItemDefault.Zip(lookupGood, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { lookupGood.Zip(oneItemDefault, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var rangeGood = Enumerable.Range(1, 1);
                try { oneItemDefault.Zip(rangeGood, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { rangeGood.Zip(oneItemDefault, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var repeatGood = Enumerable.Repeat(1, 1);
                try { oneItemDefault.Zip(repeatGood, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { repeatGood.Zip(oneItemDefault, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var reverseRangeGood = Enumerable.Range(1, 1).Reverse();
                try { oneItemDefault.Zip(reverseRangeGood, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { reverseRangeGood.Zip(oneItemDefault, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var oneItemDefaultGood = Enumerable.Empty<int>().DefaultIfEmpty();
                try { oneItemDefault.Zip(oneItemDefaultGood, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemDefaultGood.Zip(oneItemDefault, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var oneItemSpecificGood = Enumerable.Empty<int>().DefaultIfEmpty(4);
                try { oneItemDefault.Zip(oneItemSpecificGood, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemSpecificGood.Zip(oneItemDefault, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var oneItemDefaultOrderedGood = oneItemDefaultGood.OrderBy(x => x);
                try { oneItemDefault.Zip(oneItemDefaultOrderedGood, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemDefaultOrderedGood.Zip(oneItemDefault, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var oneItemSpecificOrderedGood = oneItemSpecificGood.OrderBy(x => x);
                try { oneItemDefault.Zip(oneItemSpecificOrderedGood, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemSpecificOrderedGood.Zip(oneItemDefault, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                Helper.ForEachEnumerableExpression(
                    oneItemDefault,
                    new int[0],
                    res => { },
                    @"(a, b) =>
                      {
                        try { a.Zip(b, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""first"", exc.ParamName); }
                        try { b.Zip(a, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""second"", exc.ParamName); }

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
                try { oneItemSpecific.Zip(emptyGood, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { emptyGood.Zip(oneItemSpecific, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var emptyOrderedGood = Enumerable.Empty<int>().OrderBy(x => x);
                try { oneItemSpecific.Zip(emptyOrderedGood, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { emptyOrderedGood.Zip(oneItemSpecific, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var groupByDefaultGood = new[] { 1 }.GroupBy(x => x);
                try { oneItemSpecific.Zip(groupByDefaultGood, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { groupByDefaultGood.Zip(oneItemSpecific, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var groupBySpecificGood = new[] { "foo" }.GroupBy(x => x, StringComparer.InvariantCultureIgnoreCase);
                try { oneItemSpecific.Zip(groupBySpecificGood, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { groupBySpecificGood.Zip(oneItemSpecific, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var lookupGood = new[] { 1 }.ToLookup(x => x);
                try { oneItemSpecific.Zip(lookupGood, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { lookupGood.Zip(oneItemSpecific, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var rangeGood = Enumerable.Range(1, 1);
                try { oneItemSpecific.Zip(rangeGood, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { rangeGood.Zip(oneItemSpecific, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var repeatGood = Enumerable.Repeat(1, 1);
                try { oneItemSpecific.Zip(repeatGood, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { repeatGood.Zip(oneItemSpecific, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var reverseRangeGood = Enumerable.Range(1, 1).Reverse();
                try { oneItemSpecific.Zip(reverseRangeGood, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { reverseRangeGood.Zip(oneItemSpecific, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var oneItemDefaultGood = Enumerable.Empty<int>().DefaultIfEmpty();
                try { oneItemSpecific.Zip(oneItemDefaultGood, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemDefaultGood.Zip(oneItemSpecific, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var oneItemSpecificGood = Enumerable.Empty<int>().DefaultIfEmpty(4);
                try { oneItemSpecific.Zip(oneItemSpecificGood, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemSpecificGood.Zip(oneItemSpecific, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var oneItemDefaultOrderedGood = oneItemDefaultGood.OrderBy(x => x);
                try { oneItemSpecific.Zip(oneItemDefaultOrderedGood, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemDefaultOrderedGood.Zip(oneItemSpecific, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var oneItemSpecificOrderedGood = oneItemSpecificGood.OrderBy(x => x);
                try { oneItemSpecific.Zip(oneItemSpecificOrderedGood, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemSpecificOrderedGood.Zip(oneItemSpecific, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                Helper.ForEachEnumerableExpression(
                    oneItemSpecific,
                    new int[0],
                    res => { },
                    @"(a, b) =>
                      {
                        try { a.Zip(b, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""first"", exc.ParamName); }
                        try { b.Zip(a, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""second"", exc.ParamName); }

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
                try { oneItemDefaultOrdered.Zip(emptyGood, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { emptyGood.Zip(oneItemDefaultOrdered, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var emptyOrderedGood = Enumerable.Empty<int>().OrderBy(x => x);
                try { oneItemDefaultOrdered.Zip(emptyOrderedGood, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { emptyOrderedGood.Zip(oneItemDefaultOrdered, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var groupByDefaultGood = new[] { 1 }.GroupBy(x => x);
                try { oneItemDefaultOrdered.Zip(groupByDefaultGood, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { groupByDefaultGood.Zip(oneItemDefaultOrdered, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var groupBySpecificGood = new[] { "foo" }.GroupBy(x => x, StringComparer.InvariantCultureIgnoreCase);
                try { oneItemDefaultOrdered.Zip(groupBySpecificGood, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { groupBySpecificGood.Zip(oneItemDefaultOrdered, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var lookupGood = new[] { 1 }.ToLookup(x => x);
                try { oneItemDefaultOrdered.Zip(lookupGood, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { lookupGood.Zip(oneItemDefaultOrdered, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var rangeGood = Enumerable.Range(1, 1);
                try { oneItemDefaultOrdered.Zip(rangeGood, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { rangeGood.Zip(oneItemDefaultOrdered, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var repeatGood = Enumerable.Repeat(1, 1);
                try { oneItemDefaultOrdered.Zip(repeatGood, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { repeatGood.Zip(oneItemDefaultOrdered, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var reverseRangeGood = Enumerable.Range(1, 1).Reverse();
                try { oneItemDefaultOrdered.Zip(reverseRangeGood, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { reverseRangeGood.Zip(oneItemDefaultOrdered, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var oneItemDefaultGood = Enumerable.Empty<int>().DefaultIfEmpty();
                try { oneItemDefaultOrdered.Zip(oneItemDefaultGood, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemDefaultGood.Zip(oneItemDefaultOrdered, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var oneItemSpecificGood = Enumerable.Empty<int>().DefaultIfEmpty(4);
                try { oneItemDefaultOrdered.Zip(oneItemSpecificGood, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemSpecificGood.Zip(oneItemDefaultOrdered, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var oneItemDefaultOrderedGood = oneItemDefaultGood.OrderBy(x => x);
                try { oneItemDefaultOrdered.Zip(oneItemDefaultOrderedGood, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemDefaultOrderedGood.Zip(oneItemDefaultOrdered, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var oneItemSpecificOrderedGood = oneItemSpecificGood.OrderBy(x => x);
                try { oneItemDefaultOrdered.Zip(oneItemSpecificOrderedGood, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemSpecificOrderedGood.Zip(oneItemDefaultOrdered, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                Helper.ForEachEnumerableExpression(
                    oneItemDefaultOrdered,
                    new int[0],
                    res => { },
                    @"(a, b) =>
                      {
                        try { a.Zip(b, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""first"", exc.ParamName); }
                        try { b.Zip(a, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""second"", exc.ParamName); }

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
                try { oneItemSpecificOrdered.Zip(emptyGood, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { emptyGood.Zip(oneItemSpecificOrdered, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var emptyOrderedGood = Enumerable.Empty<int>().OrderBy(x => x);
                try { oneItemSpecificOrdered.Zip(emptyOrderedGood, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { emptyOrderedGood.Zip(oneItemSpecificOrdered, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var groupByDefaultGood = new[] { 1 }.GroupBy(x => x);
                try { oneItemSpecificOrdered.Zip(groupByDefaultGood, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { groupByDefaultGood.Zip(oneItemSpecificOrdered, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var groupBySpecificGood = new[] { "foo" }.GroupBy(x => x, StringComparer.InvariantCultureIgnoreCase);
                try { oneItemSpecificOrdered.Zip(groupBySpecificGood, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { groupBySpecificGood.Zip(oneItemSpecificOrdered, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var lookupGood = new[] { 1 }.ToLookup(x => x);
                try { oneItemSpecificOrdered.Zip(lookupGood, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { lookupGood.Zip(oneItemSpecificOrdered, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var rangeGood = Enumerable.Range(1, 1);
                try { oneItemSpecificOrdered.Zip(rangeGood, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { rangeGood.Zip(oneItemSpecificOrdered, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var repeatGood = Enumerable.Repeat(1, 1);
                try { oneItemSpecificOrdered.Zip(repeatGood, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { repeatGood.Zip(oneItemSpecificOrdered, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var reverseRangeGood = Enumerable.Range(1, 1).Reverse();
                try { oneItemSpecificOrdered.Zip(reverseRangeGood, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { reverseRangeGood.Zip(oneItemSpecificOrdered, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var oneItemDefaultGood = Enumerable.Empty<int>().DefaultIfEmpty();
                try { oneItemSpecificOrdered.Zip(oneItemDefaultGood, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemDefaultGood.Zip(oneItemSpecificOrdered, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var oneItemSpecificGood = Enumerable.Empty<int>().DefaultIfEmpty(4);
                try { oneItemSpecificOrdered.Zip(oneItemSpecificGood, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemSpecificGood.Zip(oneItemSpecificOrdered, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var oneItemDefaultOrderedGood = oneItemDefaultGood.OrderBy(x => x);
                try { oneItemSpecificOrdered.Zip(oneItemDefaultOrderedGood, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemDefaultOrderedGood.Zip(oneItemSpecificOrdered, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                var oneItemSpecificOrderedGood = oneItemSpecificGood.OrderBy(x => x);
                try { oneItemSpecificOrdered.Zip(oneItemSpecificOrderedGood, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemSpecificOrderedGood.Zip(oneItemSpecificOrdered, (a, b) => a); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                Helper.ForEachEnumerableExpression(
                    oneItemSpecificOrdered,
                    new int[0],
                    res => { },
                    @"(a, b) =>
                      {
                        try { a.Zip(b, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""first"", exc.ParamName); }
                        try { b.Zip(a, (x, y) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""second"", exc.ParamName); }

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
        public void Simple()
        {
            var a = new[] { 1, 2, 3 };
            var b = new[] { 4, 5, 6 };
            var asZip = a.Zip(b, (x, y) => x + y);

            Assert.IsTrue(asZip.GetType().IsValueType);

            var res = new List<int>();
            foreach (var item in asZip)
            {
                res.Add(item);
            }

            Assert.AreEqual(3, res.Count);
            Assert.AreEqual(5, res[0]);
            Assert.AreEqual(7, res[1]);
            Assert.AreEqual(9, res[2]);
        }
    }
}
