using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using TestHelpers;

namespace LinqAF.Tests
{
    [TestClass]
    public class ExceptTests
    {
        static void _InstanceExtensionNoOverlapImpl(int spread, int take)
        {
            Dictionary<MethodInfo, List<MethodInfo>> instOverlaps, extOverlaps;
            Helper.GetOverlappingMethods(typeof(Impl.IExcept<,,>), out instOverlaps, out extOverlaps, spread, take);

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
                if (!Helper.Implements(e, typeof(LinqAF.Impl.IExcept<,,>), out missing))
                {
                    Assert.Fail($"{e.Name} does not implement IExcept ({string.Join(", ", missing)})");
                }
            }
        }

        [TestMethod]
        public void Simple()
        {
            var e = new[] { 1, 2, 3, 4 }.Except(new[] { 4, 5, 2, 2 });

            Assert.IsTrue(e.GetType().IsValueType);

            var res = new List<int>();
            foreach (var item in e)
            {
                res.Add(item);
            }

            Assert.AreEqual(2, res.Count);
            Assert.AreEqual(1, res[0]);
            Assert.AreEqual(3, res[1]);
        }

        public class _Comparer : IEqualityComparer<string>
        {
            public bool Equals(string x, string y) => x.Length == y.Length;

            public int GetHashCode(string obj) => obj.Length;
        }

        [TestMethod]
        public void Comparer()
        {
            var e = new[] { "hello", "foo", "fizz" }.Except(new[] { "world", "nope", "buzz" }, new _Comparer());

            Assert.IsTrue(e.GetType().IsValueType);

            var res = new List<string>();
            foreach (var item in e)
            {
                res.Add(item);
            }

            Assert.AreEqual(1, res.Count);
            Assert.AreEqual("foo", res[0]);
        }

        [TestMethod]
        public void Chaining_Default()
        {
            // default
            {
                Helper.ForEachEnumerableNoRetExpression(
                    new[] { 1, 2, 3, 4 },
                    @"a =>
                      Helper.ForEachEnumerableExpression(
                        a,
                        new[] { 4, 5, 2, 2 },
                        res =>
                        {
                            Assert.AreEqual(2, res.Count);
                            Assert.AreEqual(1, res[0]);
                            Assert.AreEqual(3, res[1]);
                        },
                        @""(a, b) => a.Except(b)"",
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

        [TestMethod]
        public void Chaining_Specific()
        {
            // specific
            {
                Helper.ForEachEnumerableNoRetExpression(
                    new[] { "hello", "foo", "fizz" },
                    @"a =>
                      Helper.ForEachEnumerableExpression(
                        a,
                        new[] { ""world"", ""nope"", ""buzz"" },
                        res =>
                        {
                            Assert.AreEqual(1, res.Count);
                            Assert.AreEqual(""foo"", res[0]);
                        },
                        @""(a, b) => a.Except(b, new ExceptTests._Comparer())"",
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
                Assert.IsTrue(empty.Except(empty).SequenceEqual(new int[0]));
                Assert.IsTrue(empty.Except(empty, new _IntComparer()).SequenceEqual(new int[0]));
                Assert.IsTrue(empty.Except(emptyOrdered).SequenceEqual(new int[0]));
                Assert.IsTrue(empty.Except(emptyOrdered, new _IntComparer()).SequenceEqual(new int[0]));
                Assert.IsTrue(empty.Except(range).SequenceEqual(new int[0]));
                Assert.IsTrue(empty.Except(range, new _IntComparer()).SequenceEqual(new int[0]));
                Assert.IsTrue(empty.Except(Enumerable.Repeat(1, 5)).SequenceEqual(new int[0]));
                Assert.IsTrue(empty.Except(Enumerable.Repeat(1, 5), new _IntComparer()).SequenceEqual(new int[0]));
                Assert.IsTrue(empty.Except(reverseRange).SequenceEqual(new int[0]));
                Assert.IsTrue(empty.Except(reverseRange, new _IntComparer()).SequenceEqual(new int[0]));
                Assert.IsTrue(empty.Except(oneItemDefault).SequenceEqual(new int[0]));
                Assert.IsTrue(empty.Except(oneItemDefault, new _IntComparer()).SequenceEqual(new int[0]));
                Assert.IsTrue(empty.Except(oneItemSpecific).SequenceEqual(new int[0]));
                Assert.IsTrue(empty.Except(oneItemSpecific, new _IntComparer()).SequenceEqual(new int[0]));
                Assert.IsTrue(empty.Except(oneItemDefaultOrdered).SequenceEqual(new int[0]));
                Assert.IsTrue(empty.Except(oneItemDefaultOrdered, new _IntComparer()).SequenceEqual(new int[0]));
                Assert.IsTrue(empty.Except(oneItemSpecificOrdered).SequenceEqual(new int[0]));
                Assert.IsTrue(empty.Except(oneItemSpecificOrdered, new _IntComparer()).SequenceEqual(new int[0]));

                Helper.ForEachEnumerableExpression(
                    empty,
                    new[] { 1 },
                    res => { },
                    @"(a, b) =>
                      {
                        Assert.IsTrue(a.Except(b).SequenceEqual(new int[0]));
                        Assert.IsTrue(a.Except(b, new ExceptTests._IntComparer()).SequenceEqual(new int[0]));

                        Assert.IsTrue(b.Except(a).SequenceEqual(new [] { 1 }));
                        Assert.IsTrue(b.Except(a, new ExceptTests._IntComparer()).SequenceEqual(new [] { 1 }));

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
                Assert.IsTrue(emptyOrdered.Except(empty).SequenceEqual(new int[0]));
                Assert.IsTrue(emptyOrdered.Except(empty, new _IntComparer()).SequenceEqual(new int[0]));
                Assert.IsTrue(emptyOrdered.Except(emptyOrdered).SequenceEqual(new int[0]));
                Assert.IsTrue(emptyOrdered.Except(emptyOrdered, new _IntComparer()).SequenceEqual(new int[0]));
                Assert.IsTrue(emptyOrdered.Except(range).SequenceEqual(new int[0]));
                Assert.IsTrue(emptyOrdered.Except(range, new _IntComparer()).SequenceEqual(new int[0]));
                Assert.IsTrue(emptyOrdered.Except(Enumerable.Repeat(1, 5)).SequenceEqual(new int[0]));
                Assert.IsTrue(emptyOrdered.Except(Enumerable.Repeat(1, 5), new _IntComparer()).SequenceEqual(new int[0]));
                Assert.IsTrue(emptyOrdered.Except(reverseRange).SequenceEqual(new int[0]));
                Assert.IsTrue(emptyOrdered.Except(reverseRange, new _IntComparer()).SequenceEqual(new int[0]));
                Assert.IsTrue(emptyOrdered.Except(oneItemDefault).SequenceEqual(new int[0]));
                Assert.IsTrue(emptyOrdered.Except(oneItemDefault, new _IntComparer()).SequenceEqual(new int[0]));
                Assert.IsTrue(emptyOrdered.Except(oneItemSpecific).SequenceEqual(new int[0]));
                Assert.IsTrue(emptyOrdered.Except(oneItemSpecific, new _IntComparer()).SequenceEqual(new int[0]));
                Assert.IsTrue(emptyOrdered.Except(oneItemDefaultOrdered).SequenceEqual(new int[0]));
                Assert.IsTrue(emptyOrdered.Except(oneItemDefaultOrdered, new _IntComparer()).SequenceEqual(new int[0]));
                Assert.IsTrue(emptyOrdered.Except(oneItemSpecificOrdered).SequenceEqual(new int[0]));
                Assert.IsTrue(emptyOrdered.Except(oneItemSpecificOrdered, new _IntComparer()).SequenceEqual(new int[0]));

                Helper.ForEachEnumerableExpression(
                    emptyOrdered,
                    new[] { 1 },
                    res => { },
                    @"(a, b) =>
                      {
                        Assert.IsTrue(a.Except(b).SequenceEqual(new int[0]));
                        Assert.IsTrue(a.Except(b, new ExceptTests._IntComparer()).SequenceEqual(new int[0]));

                        Assert.IsTrue(b.Except(a).SequenceEqual(new [] { 1 }));
                        Assert.IsTrue(b.Except(a, new ExceptTests._IntComparer()).SequenceEqual(new [] { 1 }));

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
                Assert.IsTrue(groupByDefault.Except(Enumerable.Empty<GroupingEnumerable<int, int>>()).SequenceEqual(groupByDefault, new _GroupingComparer<int>()));
                Assert.IsTrue(groupByDefault.Except(Enumerable.Empty<GroupingEnumerable<int, int>>(), new _GroupingComparer<int>()).SequenceEqual(groupByDefault, new _GroupingComparer<int>()));
                Assert.IsTrue(groupByDefault.Except(Enumerable.Empty<GroupingEnumerable<int, int>>().OrderBy(x => x)).SequenceEqual(groupByDefault, new _GroupingComparer<int>()));
                Assert.IsTrue(groupByDefault.Except(Enumerable.Empty<GroupingEnumerable<int, int>>().OrderBy(x => x), new _GroupingComparer<int>()).SequenceEqual(groupByDefault, new _GroupingComparer<int>()));
                Assert.IsTrue(groupByDefault.Except(Enumerable.Repeat(groupByDefault.First(), 5)).SequenceEqual(groupByDefault, new _GroupingComparer<int>()));
                Assert.IsTrue(groupByDefault.Except(Enumerable.Repeat(groupByDefault.First(), 5), new _GroupingComparer<int>()).SequenceEqual(new[] { groupByDefault.ElementAt(1), groupByDefault.ElementAt(2) }, new _GroupingComparer<int>()));
                //Assert.IsTrue(groupByDefault.Except(Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty()).SequenceEqual(groupByDefault, new _GroupingComparer<int>()));
                //Assert.IsTrue(groupByDefault.Except(Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty(), new _GroupingComparer<int>()).SequenceEqual(groupByDefault, new _GroupingComparer<int>()));
                Assert.IsTrue(groupByDefault.Except(Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty(groupByDefault.First())).SequenceEqual(groupByDefault, new _GroupingComparer<int>()));
                Assert.IsTrue(groupByDefault.Except(Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty(groupByDefault.First()), new _GroupingComparer<int>()).SequenceEqual(new[] { groupByDefault.ElementAt(1), groupByDefault.ElementAt(2) }, new _GroupingComparer<int>()));
                //Assert.IsTrue(groupByDefault.Except(Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty().OrderBy(x => x)).SequenceEqual(groupByDefault, new _GroupingComparer<int>()));
                //Assert.IsTrue(groupByDefault.Except(Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty().OrderBy(x => x), new _GroupingComparer<int>()).SequenceEqual(groupByDefault, new _GroupingComparer<int>()));
                Assert.IsTrue(groupByDefault.Except(Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty(groupByDefault.First()).OrderBy(x => x)).SequenceEqual(groupByDefault, new _GroupingComparer<int>()));
                Assert.IsTrue(groupByDefault.Except(Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty(groupByDefault.First()).OrderBy(x => x), new _GroupingComparer<int>()).SequenceEqual(new[] { groupByDefault.ElementAt(1), groupByDefault.ElementAt(2) }, new _GroupingComparer<int>()));
            }

            // groupBySpecific
            {
                Assert.IsTrue(groupBySpecific.Except(Enumerable.Empty<GroupingEnumerable<string, string>>()).SequenceEqual(groupBySpecific, new _GroupingComparer<string>()));
                Assert.IsTrue(groupBySpecific.Except(Enumerable.Empty<GroupingEnumerable<string, string>>(), new _GroupingComparer<string>()).SequenceEqual(groupBySpecific, new _GroupingComparer<string>()));
                Assert.IsTrue(groupBySpecific.Except(Enumerable.Empty<GroupingEnumerable<string, string>>().OrderBy(x => x)).SequenceEqual(groupBySpecific, new _GroupingComparer<string>()));
                Assert.IsTrue(groupBySpecific.Except(Enumerable.Empty<GroupingEnumerable<string, string>>().OrderBy(x => x), new _GroupingComparer<string>()).SequenceEqual(groupBySpecific, new _GroupingComparer<string>()));
                Assert.IsTrue(groupBySpecific.Except(Enumerable.Repeat(groupBySpecific.First(), 5)).SequenceEqual(groupBySpecific, new _GroupingComparer<string>()));
                Assert.IsTrue(groupBySpecific.Except(Enumerable.Repeat(groupBySpecific.First(), 5), new _GroupingComparer<string>()).SequenceEqual(new[] { groupBySpecific.ElementAt(1), groupBySpecific.ElementAt(2) }, new _GroupingComparer<string>()));
                //Assert.IsTrue(groupBySpecific.Except(Enumerable.Empty<GroupingEnumerable<string, string>>().DefaultIfEmpty()).SequenceEqual(groupBySpecific, new _GroupingComparer<string>()));
                //Assert.IsTrue(groupBySpecific.Except(Enumerable.Empty<GroupingEnumerable<string, string>>().DefaultIfEmpty(), new _GroupingComparer<string>()).SequenceEqual(groupBySpecific, new _GroupingComparer<string>()));
                Assert.IsTrue(groupBySpecific.Except(Enumerable.Empty<GroupingEnumerable<string, string>>().DefaultIfEmpty(groupBySpecific.First())).SequenceEqual(groupBySpecific, new _GroupingComparer<string>()));
                Assert.IsTrue(groupBySpecific.Except(Enumerable.Empty<GroupingEnumerable<string, string>>().DefaultIfEmpty(groupBySpecific.First()), new _GroupingComparer<string>()).SequenceEqual(new[] { groupBySpecific.ElementAt(1), groupBySpecific.ElementAt(2) }, new _GroupingComparer<string>()));
                //Assert.IsTrue(groupBySpecific.Except(Enumerable.Empty<GroupingEnumerable<string, string>>().DefaultIfEmpty().OrderBy(x => x)).SequenceEqual(groupBySpecific, new _GroupingComparer<string>()));
                //Assert.IsTrue(groupBySpecific.Except(Enumerable.Empty<GroupingEnumerable<string, string>>().DefaultIfEmpty().OrderBy(x => x), new _GroupingComparer<string>()).SequenceEqual(groupBySpecific, new _GroupingComparer<string>()));
                Assert.IsTrue(groupBySpecific.Except(Enumerable.Empty<GroupingEnumerable<string, string>>().DefaultIfEmpty(groupBySpecific.First()).OrderBy(x => x)).SequenceEqual(groupBySpecific, new _GroupingComparer<string>()));
                Assert.IsTrue(groupBySpecific.Except(Enumerable.Empty<GroupingEnumerable<string, string>>().DefaultIfEmpty(groupBySpecific.First()).OrderBy(x => x), new _GroupingComparer<string>()).SequenceEqual(new[] { groupBySpecific.ElementAt(1), groupBySpecific.ElementAt(2) }, new _GroupingComparer<string>()));
            }

            // lookup
            {
                Assert.IsTrue(lookup.Except(Enumerable.Empty<GroupingEnumerable<int, int>>()).SequenceEqual(lookup, new _GroupingComparer<int>()));
                Assert.IsTrue(lookup.Except(Enumerable.Empty<GroupingEnumerable<int, int>>(), new _GroupingComparer<int>()).SequenceEqual(lookup, new _GroupingComparer<int>()));
                Assert.IsTrue(lookup.Except(Enumerable.Empty<GroupingEnumerable<int, int>>().OrderBy(x => x)).SequenceEqual(lookup, new _GroupingComparer<int>()));
                Assert.IsTrue(lookup.Except(Enumerable.Empty<GroupingEnumerable<int, int>>().OrderBy(x => x), new _GroupingComparer<int>()).SequenceEqual(lookup, new _GroupingComparer<int>()));
                Assert.IsTrue(lookup.Except(Enumerable.Repeat(lookup.First(), 5)).SequenceEqual(lookup, new _GroupingComparer<int>()));
                Assert.IsTrue(lookup.Except(Enumerable.Repeat(lookup.First(), 5), new _GroupingComparer<int>()).SequenceEqual(new[] { lookup.ElementAt(1), lookup.ElementAt(2) }, new _GroupingComparer<int>()));
                //Assert.IsTrue(lookup.Except(Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty()).SequenceEqual(lookup, new _GroupingComparer<int>()));
                //Assert.IsTrue(lookup.Except(Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty(), new _GroupingComparer<int>()).SequenceEqual(lookup, new _GroupingComparer<int>()));
                Assert.IsTrue(lookup.Except(Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty(lookup.First())).SequenceEqual(lookup, new _GroupingComparer<int>()));
                Assert.IsTrue(lookup.Except(Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty(lookup.First()), new _GroupingComparer<int>()).SequenceEqual(new[] { lookup.ElementAt(1), lookup.ElementAt(2) }, new _GroupingComparer<int>()));
                //Assert.IsTrue(lookup.Except(Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty().OrderBy(x => x)).SequenceEqual(lookup, new _GroupingComparer<int>()));
                //Assert.IsTrue(lookup.Except(Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty().OrderBy(x => x), new _GroupingComparer<int>()).SequenceEqual(lookup, new _GroupingComparer<int>()));
                Assert.IsTrue(lookup.Except(Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty(lookup.First()).OrderBy(x => x)).SequenceEqual(lookup, new _GroupingComparer<int>()));
                Assert.IsTrue(lookup.Except(Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty(lookup.First()).OrderBy(x => x), new _GroupingComparer<int>()).SequenceEqual(new[] { lookup.ElementAt(1), lookup.ElementAt(2) }, new _GroupingComparer<int>()));
            }

            // range
            {
                Assert.IsTrue(range.Except(empty).SequenceEqual(new[] { 1, 2, 3, 4, 5 }));
                Assert.IsTrue(range.Except(empty, new _IntComparer()).SequenceEqual(new[] { 1, 2, 3, 4, 5 }));
                Assert.IsTrue(range.Except(emptyOrdered).SequenceEqual(new[] { 1, 2, 3, 4, 5 }));
                Assert.IsTrue(range.Except(emptyOrdered, new _IntComparer()).SequenceEqual(new[] { 1, 2, 3, 4, 5 }));
                Assert.IsTrue(range.Except(range).SequenceEqual(new int[0]));
                Assert.IsTrue(range.Except(range, new _IntComparer()).SequenceEqual(new int[0]));
                Assert.IsTrue(range.Except(Enumerable.Repeat(1, 5)).SequenceEqual(new[] { 2, 3, 4, 5 }));
                Assert.IsTrue(range.Except(Enumerable.Repeat(1, 5), new _IntComparer()).SequenceEqual(new[] { 2, 3, 4, 5 }));
                Assert.IsTrue(range.Except(reverseRange).SequenceEqual(new int[0]));
                Assert.IsTrue(range.Except(reverseRange, new _IntComparer()).SequenceEqual(new int[0]));
                Assert.IsTrue(range.Except(oneItemDefault).SequenceEqual(range));
                Assert.IsTrue(range.Except(oneItemDefault, new _IntComparer()).SequenceEqual(range));
                Assert.IsTrue(range.Except(oneItemSpecific).SequenceEqual(new[] { 1, 2, 3, 5 }));
                Assert.IsTrue(range.Except(oneItemSpecific, new _IntComparer()).SequenceEqual(new[] { 1, 2, 3, 5 }));
                Assert.IsTrue(range.Except(oneItemDefaultOrdered).SequenceEqual(range));
                Assert.IsTrue(range.Except(oneItemDefaultOrdered, new _IntComparer()).SequenceEqual(range));
                Assert.IsTrue(range.Except(oneItemSpecificOrdered).SequenceEqual(new[] { 1, 2, 3, 5 }));
                Assert.IsTrue(range.Except(oneItemSpecificOrdered, new _IntComparer()).SequenceEqual(new[] { 1, 2, 3, 5 }));

                Helper.ForEachEnumerableExpression(
                    range,
                    new[] { 1 },
                    res => { },
                    @"(a, b) =>
                      {
                        Assert.IsTrue(a.Except(b).SequenceEqual(new [] { 2, 3, 4, 5 }));
                        Assert.IsTrue(a.Except(b, new ExceptTests._IntComparer()).SequenceEqual(new [] { 2, 3, 4, 5 }));

                        Assert.IsTrue(b.Except(a).SequenceEqual(new int [0]));
                        Assert.IsTrue(b.Except(a, new ExceptTests._IntComparer()).SequenceEqual(new int [0]));

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
                Assert.IsTrue(repeat.Except(Enumerable.Empty<string>()).SequenceEqual(new[] { "foo" }));
                Assert.IsTrue(repeat.Except(Enumerable.Empty<string>(), StringComparer.InvariantCultureIgnoreCase).SequenceEqual(new[] { "foo" }));
                Assert.IsTrue(repeat.Except(Enumerable.Empty<string>().OrderBy(x => x)).SequenceEqual(new[] { "foo" }));
                Assert.IsTrue(repeat.Except(Enumerable.Empty<string>().OrderBy(x => x), StringComparer.InvariantCultureIgnoreCase).SequenceEqual(new[] { "foo" }));
                Assert.IsTrue(repeat.Except(repeat).SequenceEqual(new string[0]));
                Assert.IsTrue(repeat.Except(repeat, StringComparer.InvariantCultureIgnoreCase).SequenceEqual(new string[0]));
                Assert.IsTrue(repeat.Except(Enumerable.Empty<string>().DefaultIfEmpty()).SequenceEqual(new[] { "foo" }));
                Assert.IsTrue(repeat.Except(Enumerable.Empty<string>().DefaultIfEmpty(), StringComparer.InvariantCultureIgnoreCase).SequenceEqual(new[] { "foo" }));
                Assert.IsTrue(repeat.Except(Enumerable.Empty<string>().DefaultIfEmpty("foo")).SequenceEqual(new string[0]));
                Assert.IsTrue(repeat.Except(Enumerable.Empty<string>().DefaultIfEmpty("foo"), StringComparer.InvariantCultureIgnoreCase).SequenceEqual(new string[0]));
                Assert.IsTrue(repeat.Except(Enumerable.Empty<string>().DefaultIfEmpty().OrderBy(x => x)).SequenceEqual(new[] { "foo" }));
                Assert.IsTrue(repeat.Except(Enumerable.Empty<string>().DefaultIfEmpty().OrderBy(x => x), StringComparer.InvariantCultureIgnoreCase).SequenceEqual(new[] { "foo" }));
                Assert.IsTrue(repeat.Except(Enumerable.Empty<string>().DefaultIfEmpty("foo").OrderBy(x => x)).SequenceEqual(new string[0]));
                Assert.IsTrue(repeat.Except(Enumerable.Empty<string>().DefaultIfEmpty("foo").OrderBy(x => x), StringComparer.InvariantCultureIgnoreCase).SequenceEqual(new string[0]));

                Helper.ForEachEnumerableExpression(
                    repeat,
                    new[] { "bar" },
                    res => { },
                    @"(a, b) =>
                      {
                        Assert.IsTrue(a.Except(b).SequenceEqual(new [] { ""foo"" }));
                        Assert.IsTrue(a.Except(b, StringComparer.InvariantCultureIgnoreCase).SequenceEqual(new [] { ""foo"" }));

                        Assert.IsTrue(b.Except(a).SequenceEqual(new [] { ""bar"" }));
                        Assert.IsTrue(b.Except(a, StringComparer.InvariantCultureIgnoreCase).SequenceEqual(new [] { ""bar"" }));

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
                Assert.IsTrue(reverseRange.Except(empty).SequenceEqual(new[] { 5, 4, 3, 2, 1 }));
                Assert.IsTrue(reverseRange.Except(empty, new _IntComparer()).SequenceEqual(new[] { 5, 4, 3, 2, 1 }));
                Assert.IsTrue(reverseRange.Except(emptyOrdered).SequenceEqual(new[] { 5, 4, 3, 2, 1 }));
                Assert.IsTrue(reverseRange.Except(emptyOrdered, new _IntComparer()).SequenceEqual(new[] { 5, 4, 3, 2, 1 }));
                Assert.IsTrue(reverseRange.Except(range).SequenceEqual(new int[0]));
                Assert.IsTrue(reverseRange.Except(range, new _IntComparer()).SequenceEqual(new int[0]));
                Assert.IsTrue(reverseRange.Except(Enumerable.Repeat(1, 5)).SequenceEqual(new[] { 5, 4, 3, 2 }));
                Assert.IsTrue(reverseRange.Except(Enumerable.Repeat(1, 5), new _IntComparer()).SequenceEqual(new[] { 5, 4, 3, 2 }));
                Assert.IsTrue(reverseRange.Except(reverseRange).SequenceEqual(new int[0]));
                Assert.IsTrue(reverseRange.Except(reverseRange, new _IntComparer()).SequenceEqual(new int[0]));
                Assert.IsTrue(reverseRange.Except(oneItemDefault).SequenceEqual(reverseRange));
                Assert.IsTrue(reverseRange.Except(oneItemDefault, new _IntComparer()).SequenceEqual(reverseRange));
                Assert.IsTrue(reverseRange.Except(oneItemSpecific).SequenceEqual(new[] { 5, 3, 2, 1 }));
                Assert.IsTrue(reverseRange.Except(oneItemSpecific, new _IntComparer()).SequenceEqual(new[] { 5, 3, 2, 1 }));
                Assert.IsTrue(reverseRange.Except(oneItemDefaultOrdered).SequenceEqual(reverseRange));
                Assert.IsTrue(reverseRange.Except(oneItemDefaultOrdered, new _IntComparer()).SequenceEqual(reverseRange));
                Assert.IsTrue(reverseRange.Except(oneItemSpecificOrdered).SequenceEqual(new[] { 5, 3, 2, 1 }));
                Assert.IsTrue(reverseRange.Except(oneItemSpecificOrdered, new _IntComparer()).SequenceEqual(new[] { 5, 3, 2, 1 }));

                Helper.ForEachEnumerableExpression(
                    reverseRange,
                    new[] { 1 },
                    res => { },
                    @"(a, b) =>
                      {
                        Assert.IsTrue(a.Except(b).SequenceEqual(new [] { 5, 4, 3, 2 }));
                        Assert.IsTrue(a.Except(b, new ExceptTests._IntComparer()).SequenceEqual(new [] { 5, 4, 3, 2 }));

                        Assert.IsTrue(b.Except(a).SequenceEqual(new int [0]));
                        Assert.IsTrue(b.Except(a, new ExceptTests._IntComparer()).SequenceEqual(new int [0]));

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
                Assert.IsTrue(oneItemDefault.Except(empty).SequenceEqual(oneItemDefault));
                Assert.IsTrue(oneItemDefault.Except(empty, new _IntComparer()).SequenceEqual(oneItemDefault));
                Assert.IsTrue(oneItemDefault.Except(emptyOrdered).SequenceEqual(oneItemDefault));
                Assert.IsTrue(oneItemDefault.Except(emptyOrdered, new _IntComparer()).SequenceEqual(oneItemDefault));
                Assert.IsTrue(oneItemDefault.Except(range).SequenceEqual(oneItemDefault));
                Assert.IsTrue(oneItemDefault.Except(range, new _IntComparer()).SequenceEqual(oneItemDefault));
                Assert.IsTrue(oneItemDefault.Except(Enumerable.Repeat(1, 5)).SequenceEqual(oneItemDefault));
                Assert.IsTrue(oneItemDefault.Except(Enumerable.Repeat(1, 5), new _IntComparer()).SequenceEqual(oneItemDefault));
                Assert.IsTrue(oneItemDefault.Except(reverseRange).SequenceEqual(oneItemDefault));
                Assert.IsTrue(oneItemDefault.Except(reverseRange, new _IntComparer()).SequenceEqual(oneItemDefault));
                Assert.IsTrue(oneItemDefault.Except(oneItemDefault).SequenceEqual(new int[0]));
                Assert.IsTrue(oneItemDefault.Except(oneItemDefault, new _IntComparer()).SequenceEqual(new int[0]));
                Assert.IsTrue(oneItemDefault.Except(oneItemSpecific).SequenceEqual(oneItemDefault));
                Assert.IsTrue(oneItemDefault.Except(oneItemSpecific, new _IntComparer()).SequenceEqual(oneItemDefault));
                Assert.IsTrue(oneItemDefault.Except(oneItemDefaultOrdered).SequenceEqual(new int[0]));
                Assert.IsTrue(oneItemDefault.Except(oneItemDefaultOrdered, new _IntComparer()).SequenceEqual(new int[0]));
                Assert.IsTrue(oneItemDefault.Except(oneItemSpecificOrdered).SequenceEqual(oneItemDefault));
                Assert.IsTrue(oneItemDefault.Except(oneItemSpecificOrdered, new _IntComparer()).SequenceEqual(oneItemDefault));

                Helper.ForEachEnumerableExpression(
                    oneItemDefault,
                    new[] { 1 },
                    res => { },
                    @"(a, b) =>
                      {
                        Assert.IsTrue(a.Except(b).SequenceEqual(new [] { 0 }));
                        Assert.IsTrue(a.Except(b, new ExceptTests._IntComparer()).SequenceEqual(new [] { 0 }));

                        Assert.IsTrue(b.Except(a).SequenceEqual(new [] { 1 }));
                        Assert.IsTrue(b.Except(a, new ExceptTests._IntComparer()).SequenceEqual(new [] { 1 }));

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
                Assert.IsTrue(oneItemSpecific.Except(empty).SequenceEqual(oneItemSpecific));
                Assert.IsTrue(oneItemSpecific.Except(empty, new _IntComparer()).SequenceEqual(oneItemSpecific));
                Assert.IsTrue(oneItemSpecific.Except(emptyOrdered).SequenceEqual(oneItemSpecific));
                Assert.IsTrue(oneItemSpecific.Except(emptyOrdered, new _IntComparer()).SequenceEqual(oneItemSpecific));
                Assert.IsTrue(oneItemSpecific.Except(range).SequenceEqual(new int[0]));
                Assert.IsTrue(oneItemSpecific.Except(range, new _IntComparer()).SequenceEqual(new int[0]));
                Assert.IsTrue(oneItemSpecific.Except(Enumerable.Repeat(1, 5)).SequenceEqual(oneItemSpecific));
                Assert.IsTrue(oneItemSpecific.Except(Enumerable.Repeat(1, 5), new _IntComparer()).SequenceEqual(oneItemSpecific));
                Assert.IsTrue(oneItemSpecific.Except(reverseRange).SequenceEqual(new int[0]));
                Assert.IsTrue(oneItemSpecific.Except(reverseRange, new _IntComparer()).SequenceEqual(new int[0]));
                Assert.IsTrue(oneItemSpecific.Except(oneItemDefault).SequenceEqual(oneItemSpecific));
                Assert.IsTrue(oneItemSpecific.Except(oneItemDefault, new _IntComparer()).SequenceEqual(oneItemSpecific));
                Assert.IsTrue(oneItemSpecific.Except(oneItemSpecific).SequenceEqual(new int[0]));
                Assert.IsTrue(oneItemSpecific.Except(oneItemSpecific, new _IntComparer()).SequenceEqual(new int[0]));
                Assert.IsTrue(oneItemSpecific.Except(oneItemDefaultOrdered).SequenceEqual(oneItemSpecific));
                Assert.IsTrue(oneItemSpecific.Except(oneItemDefaultOrdered, new _IntComparer()).SequenceEqual(oneItemSpecific));
                Assert.IsTrue(oneItemSpecific.Except(oneItemSpecificOrdered).SequenceEqual(new int[0]));
                Assert.IsTrue(oneItemSpecific.Except(oneItemSpecificOrdered, new _IntComparer()).SequenceEqual(new int[0]));

                Helper.ForEachEnumerableExpression(
                    oneItemSpecific,
                    new[] { 1 },
                    res => { },
                    @"(a, b) =>
                      {
                        Assert.IsTrue(a.Except(b).SequenceEqual(new [] { 4 }));
                        Assert.IsTrue(a.Except(b, new ExceptTests._IntComparer()).SequenceEqual(new [] { 4 }));

                        Assert.IsTrue(b.Except(a).SequenceEqual(new [] { 1 }));
                        Assert.IsTrue(b.Except(a, new ExceptTests._IntComparer()).SequenceEqual(new [] { 1 }));

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
                Assert.IsTrue(oneItemDefaultOrdered.Except(empty).SequenceEqual(oneItemDefault));
                Assert.IsTrue(oneItemDefaultOrdered.Except(empty, new _IntComparer()).SequenceEqual(oneItemDefault));
                Assert.IsTrue(oneItemDefaultOrdered.Except(emptyOrdered).SequenceEqual(oneItemDefault));
                Assert.IsTrue(oneItemDefaultOrdered.Except(emptyOrdered, new _IntComparer()).SequenceEqual(oneItemDefault));
                Assert.IsTrue(oneItemDefaultOrdered.Except(range).SequenceEqual(oneItemDefault));
                Assert.IsTrue(oneItemDefaultOrdered.Except(range, new _IntComparer()).SequenceEqual(oneItemDefault));
                Assert.IsTrue(oneItemDefaultOrdered.Except(Enumerable.Repeat(1, 5)).SequenceEqual(oneItemDefault));
                Assert.IsTrue(oneItemDefaultOrdered.Except(Enumerable.Repeat(1, 5), new _IntComparer()).SequenceEqual(oneItemDefault));
                Assert.IsTrue(oneItemDefaultOrdered.Except(reverseRange).SequenceEqual(oneItemDefault));
                Assert.IsTrue(oneItemDefaultOrdered.Except(reverseRange, new _IntComparer()).SequenceEqual(oneItemDefault));
                Assert.IsTrue(oneItemDefaultOrdered.Except(oneItemDefault).SequenceEqual(new int[0]));
                Assert.IsTrue(oneItemDefaultOrdered.Except(oneItemDefault, new _IntComparer()).SequenceEqual(new int[0]));
                Assert.IsTrue(oneItemDefaultOrdered.Except(oneItemSpecific).SequenceEqual(oneItemDefault));
                Assert.IsTrue(oneItemDefaultOrdered.Except(oneItemSpecific, new _IntComparer()).SequenceEqual(oneItemDefault));
                Assert.IsTrue(oneItemDefaultOrdered.Except(oneItemDefaultOrdered).SequenceEqual(new int[0]));
                Assert.IsTrue(oneItemDefaultOrdered.Except(oneItemDefaultOrdered, new _IntComparer()).SequenceEqual(new int[0]));
                Assert.IsTrue(oneItemDefaultOrdered.Except(oneItemSpecificOrdered).SequenceEqual(oneItemDefault));
                Assert.IsTrue(oneItemDefaultOrdered.Except(oneItemSpecificOrdered, new _IntComparer()).SequenceEqual(oneItemDefault));

                Helper.ForEachEnumerableExpression(
                    oneItemDefaultOrdered,
                    new[] { 1 },
                    res => { },
                    @"(a, b) =>
                      {
                        Assert.IsTrue(a.Except(b).SequenceEqual(new [] { 0 }));
                        Assert.IsTrue(a.Except(b, new ExceptTests._IntComparer()).SequenceEqual(new [] { 0 }));

                        Assert.IsTrue(b.Except(a).SequenceEqual(new [] { 1 }));
                        Assert.IsTrue(b.Except(a, new ExceptTests._IntComparer()).SequenceEqual(new [] { 1 }));

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
                Assert.IsTrue(oneItemSpecificOrdered.Except(empty).SequenceEqual(oneItemSpecific));
                Assert.IsTrue(oneItemSpecificOrdered.Except(empty, new _IntComparer()).SequenceEqual(oneItemSpecific));
                Assert.IsTrue(oneItemSpecificOrdered.Except(emptyOrdered).SequenceEqual(oneItemSpecific));
                Assert.IsTrue(oneItemSpecificOrdered.Except(emptyOrdered, new _IntComparer()).SequenceEqual(oneItemSpecific));
                Assert.IsTrue(oneItemSpecificOrdered.Except(range).SequenceEqual(new int[0]));
                Assert.IsTrue(oneItemSpecificOrdered.Except(range, new _IntComparer()).SequenceEqual(new int[0]));
                Assert.IsTrue(oneItemSpecificOrdered.Except(Enumerable.Repeat(1, 5)).SequenceEqual(oneItemSpecific));
                Assert.IsTrue(oneItemSpecificOrdered.Except(Enumerable.Repeat(1, 5), new _IntComparer()).SequenceEqual(oneItemSpecific));
                Assert.IsTrue(oneItemSpecificOrdered.Except(reverseRange).SequenceEqual(new int[0]));
                Assert.IsTrue(oneItemSpecificOrdered.Except(reverseRange, new _IntComparer()).SequenceEqual(new int[0]));
                Assert.IsTrue(oneItemSpecificOrdered.Except(oneItemDefault).SequenceEqual(oneItemSpecific));
                Assert.IsTrue(oneItemSpecificOrdered.Except(oneItemDefault, new _IntComparer()).SequenceEqual(oneItemSpecific));
                Assert.IsTrue(oneItemSpecificOrdered.Except(oneItemSpecific).SequenceEqual(new int[0]));
                Assert.IsTrue(oneItemSpecificOrdered.Except(oneItemSpecific, new _IntComparer()).SequenceEqual(new int[0]));
                Assert.IsTrue(oneItemSpecificOrdered.Except(oneItemDefaultOrdered).SequenceEqual(oneItemSpecific));
                Assert.IsTrue(oneItemSpecificOrdered.Except(oneItemDefaultOrdered, new _IntComparer()).SequenceEqual(oneItemSpecific));
                Assert.IsTrue(oneItemSpecificOrdered.Except(oneItemSpecificOrdered).SequenceEqual(new int[0]));
                Assert.IsTrue(oneItemSpecificOrdered.Except(oneItemSpecificOrdered, new _IntComparer()).SequenceEqual(new int[0]));

                Helper.ForEachEnumerableExpression(
                    oneItemSpecificOrdered,
                    new[] { 1 },
                    res => { },
                    @"(a, b) =>
                      {
                        Assert.IsTrue(a.Except(b).SequenceEqual(new [] { 4 }));
                        Assert.IsTrue(a.Except(b, new ExceptTests._IntComparer()).SequenceEqual(new [] { 4 }));

                        Assert.IsTrue(b.Except(a).SequenceEqual(new [] { 1 }));
                        Assert.IsTrue(b.Except(a, new ExceptTests._IntComparer()).SequenceEqual(new [] { 1 }));

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
            // default
            {
                Helper.ForEachMalformedEnumerableExpression<int>(
                    @"a =>
                      Helper.ForEachEnumerableExpression(
                        a,
                        new[] { 4, 5, 2, 2 },
                        res => {},
                        @""(a, b) => 
                           {
                            try
                            {
                                a.Except(b);
                                Assert.Fail();
                            }
                            catch(ArgumentException exc)
                            {
                                Assert.AreEqual(""""first"""", exc.ParamName);
                            }

                            try
                            {
                                b.Except(a);
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
            // specific
            {
                Helper.ForEachMalformedEnumerableExpression<string>(
                    @"a =>
                      Helper.ForEachEnumerableExpression(
                        a,
                        new[] { ""foo"" },
                        res => {},
                        @""(a, b) => 
                           {
                            try
                            {
                                a.Except(b, new ExceptTests._Comparer());
                                Assert.Fail();
                            }
                            catch(ArgumentException exc)
                            {
                                Assert.AreEqual(""""first"""", exc.ParamName);
                            }

                            try
                            {
                                b.Except(a, new ExceptTests._Comparer());
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

            // groupByDefault
            {
                try { groupByDefault.Except(Enumerable.Empty<GroupingEnumerable<int, int>>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { groupByDefault.Except(Enumerable.Empty<GroupingEnumerable<int, int>>(), new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Empty<GroupingEnumerable<int, int>>().Except(groupByDefault); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Empty<GroupingEnumerable<int, int>>().Except(groupByDefault, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { groupByDefault.Except(Enumerable.Empty<GroupingEnumerable<int, int>>().OrderBy(x => x)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { groupByDefault.Except(Enumerable.Empty<GroupingEnumerable<int, int>>().OrderBy(x => x), new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Empty<GroupingEnumerable<int, int>>().OrderBy(x => x).Except(groupByDefault); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Empty<GroupingEnumerable<int, int>>().OrderBy(x => x).Except(groupByDefault, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { groupByDefault.Except((new[] { 1 }).GroupBy(x => x, new _IntComparer())); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { groupByDefault.Except((new[] { 1 }).GroupBy(x => x, new _IntComparer()), new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { (new[] { 1 }).GroupBy(x => x, new _IntComparer()).Except(groupByDefault); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { (new[] { 1 }).GroupBy(x => x, new _IntComparer()).Except(groupByDefault, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { groupByDefault.Except((new[] { 1 }).ToLookup(x => x)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { groupByDefault.Except((new[] { 1 }).ToLookup(x => x), new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { (new[] { 1 }).ToLookup(x => x).Except(groupByDefault); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { (new[] { 1 }).ToLookup(x => x).Except(groupByDefault, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { groupByDefault.Except(Enumerable.Repeat((new[] { 1 }).ToLookup(x => x).First(), 1)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { groupByDefault.Except(Enumerable.Repeat((new[] { 1 }).ToLookup(x => x).First(), 1), new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Repeat((new[] { 1 }).ToLookup(x => x).First(), 1).Except(groupByDefault); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Repeat((new[] { 1 }).ToLookup(x => x).First(), 1).Except(groupByDefault, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { groupByDefault.Except(Enumerable.Repeat((new[] { 1 }).ToLookup(x => x).First(), 1).Reverse()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { groupByDefault.Except(Enumerable.Repeat((new[] { 1 }).ToLookup(x => x).First(), 1).Reverse(), new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Repeat((new[] { 1 }).ToLookup(x => x).First(), 1).Reverse().Except(groupByDefault); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Repeat((new[] { 1 }).ToLookup(x => x).First(), 1).Reverse().Except(groupByDefault, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { groupByDefault.Except(Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { groupByDefault.Except(Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty(), new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty().Except(groupByDefault); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty().Except(groupByDefault, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { groupByDefault.Except(Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty((new[] { 1 }).ToLookup(x => x).First())); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { groupByDefault.Except(Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty((new[] { 1 }).ToLookup(x => x).First()), new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty((new[] { 1 }).ToLookup(x => x).First()).Except(groupByDefault); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty((new[] { 1 }).ToLookup(x => x).First()).Except(groupByDefault, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { groupByDefault.Except(Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty().OrderBy(x => x)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { groupByDefault.Except(Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty().OrderBy(x => x), new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty().OrderBy(x => x).Except(groupByDefault); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty().OrderBy(x => x).Except(groupByDefault, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { groupByDefault.Except(Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty((new[] { 1 }).ToLookup(x => x).First()).OrderBy(x => x)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { groupByDefault.Except(Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty((new[] { 1 }).ToLookup(x => x).First()).OrderBy(x => x), new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty((new[] { 1 }).ToLookup(x => x).First()).OrderBy(x => x).Except(groupByDefault); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty((new[] { 1 }).ToLookup(x => x).First()).OrderBy(x => x).Except(groupByDefault, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                Helper.ForEachEnumerableExpression(
                    groupByDefault,
                    new GroupingEnumerable<int, int>[0],
                    res => { },
                    @"(a, b) =>
                      {
                        try
                        {
                            a.Except(b);
                            Assert.Fail();
                        }
                        catch(ArgumentException exc)
                        {
                            Assert.AreEqual(""first"", exc.ParamName);
                        }

                        try
                        {
                            a.Except(b, new ExceptTests._GroupingComparer<int>());
                            Assert.Fail();
                        }
                        catch(ArgumentException exc)
                        {
                            Assert.AreEqual(""first"", exc.ParamName);
                        }

                        try
                        {
                            b.Except(a);
                            Assert.Fail();
                        }
                        catch(ArgumentException exc)
                        {
                            Assert.AreEqual(""second"", exc.ParamName);
                        }

                        try
                        {
                            b.Except(a, new ExceptTests._GroupingComparer<int>());
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
            }

            // groupBySpecific
            {
                try { groupBySpecific.Except(Enumerable.Empty<GroupingEnumerable<int, int>>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { groupBySpecific.Except(Enumerable.Empty<GroupingEnumerable<int, int>>(), new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Empty<GroupingEnumerable<int, int>>().Except(groupBySpecific); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Empty<GroupingEnumerable<int, int>>().Except(groupBySpecific, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { groupBySpecific.Except(Enumerable.Empty<GroupingEnumerable<int, int>>().OrderBy(x => x)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { groupBySpecific.Except(Enumerable.Empty<GroupingEnumerable<int, int>>().OrderBy(x => x), new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Empty<GroupingEnumerable<int, int>>().OrderBy(x => x).Except(groupBySpecific); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Empty<GroupingEnumerable<int, int>>().OrderBy(x => x).Except(groupBySpecific, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { groupBySpecific.Except((new[] { 1 }).GroupBy(x => x, new _IntComparer())); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { groupBySpecific.Except((new[] { 1 }).GroupBy(x => x, new _IntComparer()), new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { (new[] { 1 }).GroupBy(x => x, new _IntComparer()).Except(groupBySpecific); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { (new[] { 1 }).GroupBy(x => x, new _IntComparer()).Except(groupBySpecific, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { groupBySpecific.Except((new[] { 1 }).ToLookup(x => x)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { groupBySpecific.Except((new[] { 1 }).ToLookup(x => x), new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { (new[] { 1 }).ToLookup(x => x).Except(groupBySpecific); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { (new[] { 1 }).ToLookup(x => x).Except(groupBySpecific, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { groupBySpecific.Except(Enumerable.Repeat((new[] { 1 }).ToLookup(x => x).First(), 1)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { groupBySpecific.Except(Enumerable.Repeat((new[] { 1 }).ToLookup(x => x).First(), 1), new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Repeat((new[] { 1 }).ToLookup(x => x).First(), 1).Except(groupBySpecific); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Repeat((new[] { 1 }).ToLookup(x => x).First(), 1).Except(groupBySpecific, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { groupBySpecific.Except(Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { groupBySpecific.Except(Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty(), new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty().Except(groupBySpecific); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty().Except(groupBySpecific, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { groupBySpecific.Except(Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty((new[] { 1 }).ToLookup(x => x).First())); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { groupBySpecific.Except(Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty((new[] { 1 }).ToLookup(x => x).First()), new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty((new[] { 1 }).ToLookup(x => x).First()).Except(groupBySpecific); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty((new[] { 1 }).ToLookup(x => x).First()).Except(groupBySpecific, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { groupBySpecific.Except(Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty().OrderBy(x => x)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { groupBySpecific.Except(Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty().OrderBy(x => x), new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty().OrderBy(x => x).Except(groupBySpecific); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty().OrderBy(x => x).Except(groupBySpecific, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { groupBySpecific.Except(Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty((new[] { 1 }).ToLookup(x => x).First()).OrderBy(x => x)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { groupBySpecific.Except(Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty((new[] { 1 }).ToLookup(x => x).First()).OrderBy(x => x), new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty((new[] { 1 }).ToLookup(x => x).First()).OrderBy(x => x).Except(groupBySpecific); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty((new[] { 1 }).ToLookup(x => x).First()).OrderBy(x => x).Except(groupBySpecific, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                Helper.ForEachEnumerableExpression(
                    groupBySpecific,
                    new GroupingEnumerable<int, int>[0],
                    res => { },
                    @"(a, b) =>
                      {
                        try
                        {
                            a.Except(b);
                            Assert.Fail();
                        }
                        catch(ArgumentException exc)
                        {
                            Assert.AreEqual(""first"", exc.ParamName);
                        }

                        try
                        {
                            a.Except(b, new ExceptTests._GroupingComparer<int>());
                            Assert.Fail();
                        }
                        catch(ArgumentException exc)
                        {
                            Assert.AreEqual(""first"", exc.ParamName);
                        }

                        try
                        {
                            b.Except(a);
                            Assert.Fail();
                        }
                        catch(ArgumentException exc)
                        {
                            Assert.AreEqual(""second"", exc.ParamName);
                        }

                        try
                        {
                            b.Except(a, new ExceptTests._GroupingComparer<int>());
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
            }

            // lookup
            {
                try { lookupDefault.Except(Enumerable.Empty<GroupingEnumerable<int, int>>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { lookupDefault.Except(Enumerable.Empty<GroupingEnumerable<int, int>>(), new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Empty<GroupingEnumerable<int, int>>().Except(lookupDefault); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Empty<GroupingEnumerable<int, int>>().Except(lookupDefault, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { lookupDefault.Except(Enumerable.Empty<GroupingEnumerable<int, int>>().OrderBy(x => x)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { lookupDefault.Except(Enumerable.Empty<GroupingEnumerable<int, int>>().OrderBy(x => x), new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Empty<GroupingEnumerable<int, int>>().OrderBy(x => x).Except(lookupDefault); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Empty<GroupingEnumerable<int, int>>().OrderBy(x => x).Except(lookupDefault, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { lookupDefault.Except((new[] { 1 }).GroupBy(x => x, new _IntComparer())); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { lookupDefault.Except((new[] { 1 }).GroupBy(x => x, new _IntComparer()), new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { (new[] { 1 }).GroupBy(x => x, new _IntComparer()).Except(lookupDefault); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { (new[] { 1 }).GroupBy(x => x, new _IntComparer()).Except(lookupDefault, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { lookupDefault.Except((new[] { 1 }).ToLookup(x => x)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { lookupDefault.Except((new[] { 1 }).ToLookup(x => x), new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { (new[] { 1 }).ToLookup(x => x).Except(lookupDefault); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { (new[] { 1 }).ToLookup(x => x).Except(lookupDefault, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { lookupDefault.Except(Enumerable.Repeat((new[] { 1 }).ToLookup(x => x).First(), 1)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { lookupDefault.Except(Enumerable.Repeat((new[] { 1 }).ToLookup(x => x).First(), 1), new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Repeat((new[] { 1 }).ToLookup(x => x).First(), 1).Except(lookupDefault); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Repeat((new[] { 1 }).ToLookup(x => x).First(), 1).Except(lookupDefault, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { lookupDefault.Except(Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { lookupDefault.Except(Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty(), new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty().Except(lookupDefault); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty().Except(lookupDefault, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { lookupDefault.Except(Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty((new[] { 1 }).ToLookup(x => x).First())); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { lookupDefault.Except(Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty((new[] { 1 }).ToLookup(x => x).First()), new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty((new[] { 1 }).ToLookup(x => x).First()).Except(lookupDefault); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty((new[] { 1 }).ToLookup(x => x).First()).Except(lookupDefault, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { lookupDefault.Except(Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty().OrderBy(x => x)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { lookupDefault.Except(Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty().OrderBy(x => x), new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty().OrderBy(x => x).Except(lookupDefault); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty().OrderBy(x => x).Except(lookupDefault, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { lookupDefault.Except(Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty((new[] { 1 }).ToLookup(x => x).First()).OrderBy(x => x)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { lookupDefault.Except(Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty((new[] { 1 }).ToLookup(x => x).First()).OrderBy(x => x), new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty((new[] { 1 }).ToLookup(x => x).First()).OrderBy(x => x).Except(lookupDefault); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty((new[] { 1 }).ToLookup(x => x).First()).OrderBy(x => x).Except(lookupDefault, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                Helper.ForEachEnumerableExpression(
                    lookupDefault,
                    new GroupingEnumerable<int, int>[0],
                    res => { },
                    @"(a, b) =>
                      {
                        try
                        {
                            a.Except(b);
                            Assert.Fail();
                        }
                        catch(ArgumentException exc)
                        {
                            Assert.AreEqual(""first"", exc.ParamName);
                        }

                        try
                        {
                            a.Except(b, new ExceptTests._GroupingComparer<int>());
                            Assert.Fail();
                        }
                        catch(ArgumentException exc)
                        {
                            Assert.AreEqual(""first"", exc.ParamName);
                        }

                        try
                        {
                            b.Except(a);
                            Assert.Fail();
                        }
                        catch(ArgumentException exc)
                        {
                            Assert.AreEqual(""second"", exc.ParamName);
                        }

                        try
                        {
                            b.Except(a, new ExceptTests._GroupingComparer<int>());
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
            }

            // lookupSpecific
            {
                try { lookupSpecific.Except(Enumerable.Empty<GroupingEnumerable<int, int>>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { lookupSpecific.Except(Enumerable.Empty<GroupingEnumerable<int, int>>(), new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Empty<GroupingEnumerable<int, int>>().Except(lookupSpecific); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Empty<GroupingEnumerable<int, int>>().Except(lookupSpecific, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { lookupSpecific.Except(Enumerable.Empty<GroupingEnumerable<int, int>>().OrderBy(x => x)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { lookupSpecific.Except(Enumerable.Empty<GroupingEnumerable<int, int>>().OrderBy(x => x), new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Empty<GroupingEnumerable<int, int>>().OrderBy(x => x).Except(lookupSpecific); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Empty<GroupingEnumerable<int, int>>().OrderBy(x => x).Except(lookupSpecific, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { lookupSpecific.Except((new[] { 1 }).GroupBy(x => x, new _IntComparer())); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { lookupSpecific.Except((new[] { 1 }).GroupBy(x => x, new _IntComparer()), new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { (new[] { 1 }).GroupBy(x => x, new _IntComparer()).Except(lookupSpecific); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { (new[] { 1 }).GroupBy(x => x, new _IntComparer()).Except(lookupSpecific, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { lookupSpecific.Except((new[] { 1 }).ToLookup(x => x)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { lookupSpecific.Except((new[] { 1 }).ToLookup(x => x), new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { (new[] { 1 }).ToLookup(x => x).Except(lookupSpecific); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { (new[] { 1 }).ToLookup(x => x).Except(lookupSpecific, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { lookupSpecific.Except(Enumerable.Repeat((new[] { 1 }).ToLookup(x => x).First(), 1)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { lookupSpecific.Except(Enumerable.Repeat((new[] { 1 }).ToLookup(x => x).First(), 1), new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Repeat((new[] { 1 }).ToLookup(x => x).First(), 1).Except(lookupSpecific); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Repeat((new[] { 1 }).ToLookup(x => x).First(), 1).Except(lookupSpecific, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { lookupSpecific.Except(Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { lookupSpecific.Except(Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty(), new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty().Except(lookupSpecific); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty().Except(lookupSpecific, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { lookupSpecific.Except(Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty((new[] { 1 }).ToLookup(x => x).First())); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { lookupSpecific.Except(Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty((new[] { 1 }).ToLookup(x => x).First()), new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty((new[] { 1 }).ToLookup(x => x).First()).Except(lookupSpecific); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty((new[] { 1 }).ToLookup(x => x).First()).Except(lookupSpecific, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { lookupSpecific.Except(Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty().OrderBy(x => x)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { lookupSpecific.Except(Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty().OrderBy(x => x), new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty().OrderBy(x => x).Except(lookupSpecific); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty().OrderBy(x => x).Except(lookupSpecific, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { lookupSpecific.Except(Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty((new[] { 1 }).ToLookup(x => x).First()).OrderBy(x => x)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { lookupSpecific.Except(Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty((new[] { 1 }).ToLookup(x => x).First()).OrderBy(x => x), new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty((new[] { 1 }).ToLookup(x => x).First()).OrderBy(x => x).Except(lookupSpecific); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Empty<GroupingEnumerable<int, int>>().DefaultIfEmpty((new[] { 1 }).ToLookup(x => x).First()).OrderBy(x => x).Except(lookupSpecific, new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                Helper.ForEachEnumerableExpression(
                    lookupSpecific,
                    new GroupingEnumerable<int, int>[0],
                    res => { },
                    @"(a, b) =>
                      {
                        try
                        {
                            a.Except(b);
                            Assert.Fail();
                        }
                        catch(ArgumentException exc)
                        {
                            Assert.AreEqual(""first"", exc.ParamName);
                        }

                        try
                        {
                            a.Except(b, new ExceptTests._GroupingComparer<int>());
                            Assert.Fail();
                        }
                        catch(ArgumentException exc)
                        {
                            Assert.AreEqual(""first"", exc.ParamName);
                        }

                        try
                        {
                            b.Except(a);
                            Assert.Fail();
                        }
                        catch(ArgumentException exc)
                        {
                            Assert.AreEqual(""second"", exc.ParamName);
                        }

                        try
                        {
                            b.Except(a, new ExceptTests._GroupingComparer<int>());
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
            }

            // range
            {
                try { range.Except(Enumerable.Empty<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { range.Except(Enumerable.Empty<int>(), new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Empty<int>().Except(range); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Empty<int>().Except(range, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { range.Except(Enumerable.Range(1, 2)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { range.Except(Enumerable.Range(1, 2), new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Range(1, 2).Except(range); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Range(1, 2).Except(range, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { range.Except(Enumerable.Repeat(1, 1)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { range.Except(Enumerable.Repeat(1, 1), new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Repeat(1, 1).Except(range); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Repeat(1, 1).Except(range, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { range.Except(Enumerable.Repeat(1, 1).Reverse()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { range.Except(Enumerable.Repeat(1, 1).Reverse(), new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Repeat(1, 1).Reverse().Except(range); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Repeat(1, 1).Reverse().Except(range, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { range.Except(Enumerable.Empty<int>().DefaultIfEmpty()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { range.Except(Enumerable.Empty<int>().DefaultIfEmpty(), new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Empty<int>().DefaultIfEmpty().Except(range); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Empty<int>().DefaultIfEmpty().Except(range, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { range.Except(Enumerable.Empty<int>().DefaultIfEmpty(4)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { range.Except(Enumerable.Empty<int>().DefaultIfEmpty(4), new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Empty<int>().DefaultIfEmpty(4).Except(range); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Empty<int>().DefaultIfEmpty(4).Except(range, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { range.Except(Enumerable.Empty<int>().DefaultIfEmpty().OrderBy(x => x)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { range.Except(Enumerable.Empty<int>().DefaultIfEmpty().OrderBy(x => x), new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Empty<int>().DefaultIfEmpty().OrderBy(x => x).Except(range); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Empty<int>().DefaultIfEmpty().OrderBy(x => x).Except(range, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { range.Except(Enumerable.Empty<int>().DefaultIfEmpty(4).OrderBy(x => x)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { range.Except(Enumerable.Empty<int>().DefaultIfEmpty(4).OrderBy(x => x), new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Empty<int>().DefaultIfEmpty(4).OrderBy(x => x).Except(range); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Empty<int>().DefaultIfEmpty(4).OrderBy(x => x).Except(range, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                Helper.ForEachEnumerableExpression(
                    range,
                    new int[0],
                    res => { },
                    @"(a, b) =>
                      {
                        try
                        {
                            a.Except(b);
                            Assert.Fail();
                        }
                        catch(ArgumentException exc)
                        {
                            Assert.AreEqual(""first"", exc.ParamName);
                        }

                        try
                        {
                            a.Except(b, new ExceptTests._IntComparer());
                            Assert.Fail();
                        }
                        catch(ArgumentException exc)
                        {
                            Assert.AreEqual(""first"", exc.ParamName);
                        }

                        try
                        {
                            b.Except(a);
                            Assert.Fail();
                        }
                        catch(ArgumentException exc)
                        {
                            Assert.AreEqual(""second"", exc.ParamName);
                        }

                        try
                        {
                            b.Except(a, new ExceptTests._IntComparer());
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
            }

            // repeat
            {
                try { repeat.Except(Enumerable.Empty<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { repeat.Except(Enumerable.Empty<int>(), new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Empty<int>().Except(repeat); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Empty<int>().Except(repeat, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { repeat.Except(Enumerable.Range(1, 2)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { repeat.Except(Enumerable.Range(1, 2), new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Range(1, 2).Except(repeat); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Range(1, 2).Except(repeat, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { repeat.Except(Enumerable.Repeat(1, 1)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { repeat.Except(Enumerable.Repeat(1, 1), new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Repeat(1, 1).Except(repeat); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Repeat(1, 1).Except(repeat, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { repeat.Except(Enumerable.Repeat(1, 1).Reverse()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { repeat.Except(Enumerable.Repeat(1, 1).Reverse(), new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Repeat(1, 1).Reverse().Except(repeat); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Repeat(1, 1).Reverse().Except(repeat, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { repeat.Except(Enumerable.Empty<int>().DefaultIfEmpty()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { repeat.Except(Enumerable.Empty<int>().DefaultIfEmpty(), new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Empty<int>().DefaultIfEmpty().Except(repeat); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Empty<int>().DefaultIfEmpty().Except(repeat, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { repeat.Except(Enumerable.Empty<int>().DefaultIfEmpty(4)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { repeat.Except(Enumerable.Empty<int>().DefaultIfEmpty(4), new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Empty<int>().DefaultIfEmpty(4).Except(repeat); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Empty<int>().DefaultIfEmpty(4).Except(repeat, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { repeat.Except(Enumerable.Empty<int>().DefaultIfEmpty().OrderBy(x => x)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { repeat.Except(Enumerable.Empty<int>().DefaultIfEmpty().OrderBy(x => x), new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Empty<int>().DefaultIfEmpty().OrderBy(x => x).Except(repeat); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Empty<int>().DefaultIfEmpty().OrderBy(x => x).Except(repeat, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { repeat.Except(Enumerable.Empty<int>().DefaultIfEmpty(4).OrderBy(x => x)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { repeat.Except(Enumerable.Empty<int>().DefaultIfEmpty(4).OrderBy(x => x), new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Empty<int>().DefaultIfEmpty(4).OrderBy(x => x).Except(repeat); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Empty<int>().DefaultIfEmpty(4).OrderBy(x => x).Except(repeat, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                Helper.ForEachEnumerableExpression(
                    repeat,
                    new int[0],
                    res => { },
                    @"(a, b) =>
                      {
                        try
                        {
                            a.Except(b);
                            Assert.Fail();
                        }
                        catch(ArgumentException exc)
                        {
                            Assert.AreEqual(""first"", exc.ParamName);
                        }

                        try
                        {
                            a.Except(b, new ExceptTests._IntComparer());
                            Assert.Fail();
                        }
                        catch(ArgumentException exc)
                        {
                            Assert.AreEqual(""first"", exc.ParamName);
                        }

                        try
                        {
                            b.Except(a);
                            Assert.Fail();
                        }
                        catch(ArgumentException exc)
                        {
                            Assert.AreEqual(""second"", exc.ParamName);
                        }

                        try
                        {
                            b.Except(a, new ExceptTests._IntComparer());
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
            }

            // reverseRange
            {
                try { reverseRange.Except(Enumerable.Empty<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { reverseRange.Except(Enumerable.Empty<int>(), new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Empty<int>().Except(reverseRange); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Empty<int>().Except(reverseRange, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { reverseRange.Except(Enumerable.Range(1, 2)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { reverseRange.Except(Enumerable.Range(1, 2), new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Range(1, 2).Except(reverseRange); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Range(1, 2).Except(reverseRange, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { reverseRange.Except(Enumerable.Repeat(1, 1)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { reverseRange.Except(Enumerable.Repeat(1, 1), new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Repeat(1, 1).Except(reverseRange); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Repeat(1, 1).Except(reverseRange, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { reverseRange.Except(Enumerable.Repeat(1, 1).Reverse()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { reverseRange.Except(Enumerable.Repeat(1, 1).Reverse(), new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Repeat(1, 1).Reverse().Except(reverseRange); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Repeat(1, 1).Reverse().Except(reverseRange, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { reverseRange.Except(Enumerable.Empty<int>().DefaultIfEmpty()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { reverseRange.Except(Enumerable.Empty<int>().DefaultIfEmpty(), new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Empty<int>().DefaultIfEmpty().Except(reverseRange); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Empty<int>().DefaultIfEmpty().Except(reverseRange, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { reverseRange.Except(Enumerable.Empty<int>().DefaultIfEmpty(4)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { reverseRange.Except(Enumerable.Empty<int>().DefaultIfEmpty(4), new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Empty<int>().DefaultIfEmpty(4).Except(reverseRange); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Empty<int>().DefaultIfEmpty(4).Except(reverseRange, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { reverseRange.Except(Enumerable.Empty<int>().DefaultIfEmpty().OrderBy(x => x)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { reverseRange.Except(Enumerable.Empty<int>().DefaultIfEmpty().OrderBy(x => x), new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Empty<int>().DefaultIfEmpty().OrderBy(x => x).Except(reverseRange); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Empty<int>().DefaultIfEmpty().OrderBy(x => x).Except(reverseRange, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { reverseRange.Except(Enumerable.Empty<int>().DefaultIfEmpty(4).OrderBy(x => x)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { reverseRange.Except(Enumerable.Empty<int>().DefaultIfEmpty(4).OrderBy(x => x), new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Empty<int>().DefaultIfEmpty(4).OrderBy(x => x).Except(reverseRange); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Empty<int>().DefaultIfEmpty(4).OrderBy(x => x).Except(reverseRange, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                Helper.ForEachEnumerableExpression(
                    reverseRange,
                    new int[0],
                    res => { },
                    @"(a, b) =>
                      {
                        try
                        {
                            a.Except(b);
                            Assert.Fail();
                        }
                        catch(ArgumentException exc)
                        {
                            Assert.AreEqual(""first"", exc.ParamName);
                        }

                        try
                        {
                            a.Except(b, new ExceptTests._IntComparer());
                            Assert.Fail();
                        }
                        catch(ArgumentException exc)
                        {
                            Assert.AreEqual(""first"", exc.ParamName);
                        }

                        try
                        {
                            b.Except(a);
                            Assert.Fail();
                        }
                        catch(ArgumentException exc)
                        {
                            Assert.AreEqual(""second"", exc.ParamName);
                        }

                        try
                        {
                            b.Except(a, new ExceptTests._IntComparer());
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
            }

            // oneItemDefault
            {
                try { oneItemDefault.Except(Enumerable.Empty<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemDefault.Except(Enumerable.Empty<int>(), new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Empty<int>().Except(oneItemDefault); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Empty<int>().Except(oneItemDefault, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemDefault.Except(Enumerable.Range(1, 2)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemDefault.Except(Enumerable.Range(1, 2), new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Range(1, 2).Except(oneItemDefault); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Range(1, 2).Except(oneItemDefault, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemDefault.Except(Enumerable.Repeat(1, 1)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemDefault.Except(Enumerable.Repeat(1, 1), new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Repeat(1, 1).Except(oneItemDefault); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Repeat(1, 1).Except(oneItemDefault, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemDefault.Except(Enumerable.Repeat(1, 1).Reverse()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemDefault.Except(Enumerable.Repeat(1, 1).Reverse(), new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Repeat(1, 1).Reverse().Except(oneItemDefault); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Repeat(1, 1).Reverse().Except(oneItemDefault, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemDefault.Except(Enumerable.Empty<int>().DefaultIfEmpty()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemDefault.Except(Enumerable.Empty<int>().DefaultIfEmpty(), new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Empty<int>().DefaultIfEmpty().Except(oneItemDefault); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Empty<int>().DefaultIfEmpty().Except(oneItemDefault, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemDefault.Except(Enumerable.Empty<int>().DefaultIfEmpty(4)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemDefault.Except(Enumerable.Empty<int>().DefaultIfEmpty(4), new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Empty<int>().DefaultIfEmpty(4).Except(oneItemDefault); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Empty<int>().DefaultIfEmpty(4).Except(oneItemDefault, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemDefault.Except(Enumerable.Empty<int>().DefaultIfEmpty().OrderBy(x => x)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemDefault.Except(Enumerable.Empty<int>().DefaultIfEmpty().OrderBy(x => x), new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Empty<int>().DefaultIfEmpty().OrderBy(x => x).Except(oneItemDefault); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Empty<int>().DefaultIfEmpty().OrderBy(x => x).Except(oneItemDefault, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemDefault.Except(Enumerable.Empty<int>().DefaultIfEmpty(4).OrderBy(x => x)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemDefault.Except(Enumerable.Empty<int>().DefaultIfEmpty(4).OrderBy(x => x), new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Empty<int>().DefaultIfEmpty(4).OrderBy(x => x).Except(oneItemDefault); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Empty<int>().DefaultIfEmpty(4).OrderBy(x => x).Except(oneItemDefault, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                Helper.ForEachEnumerableExpression(
                    oneItemDefault,
                    new int[0],
                    res => { },
                    @"(a, b) =>
                      {
                        try
                        {
                            a.Except(b);
                            Assert.Fail();
                        }
                        catch(ArgumentException exc)
                        {
                            Assert.AreEqual(""first"", exc.ParamName);
                        }

                        try
                        {
                            a.Except(b, new ExceptTests._IntComparer());
                            Assert.Fail();
                        }
                        catch(ArgumentException exc)
                        {
                            Assert.AreEqual(""first"", exc.ParamName);
                        }

                        try
                        {
                            b.Except(a);
                            Assert.Fail();
                        }
                        catch(ArgumentException exc)
                        {
                            Assert.AreEqual(""second"", exc.ParamName);
                        }

                        try
                        {
                            b.Except(a, new ExceptTests._IntComparer());
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
            }

            // oneItemSpecific
            {
                try { oneItemSpecific.Except(Enumerable.Empty<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemSpecific.Except(Enumerable.Empty<int>(), new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Empty<int>().Except(oneItemSpecific); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Empty<int>().Except(oneItemSpecific, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemSpecific.Except(Enumerable.Range(1, 2)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemSpecific.Except(Enumerable.Range(1, 2), new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Range(1, 2).Except(oneItemSpecific); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Range(1, 2).Except(oneItemSpecific, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemSpecific.Except(Enumerable.Repeat(1, 1)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemSpecific.Except(Enumerable.Repeat(1, 1), new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Repeat(1, 1).Except(oneItemSpecific); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Repeat(1, 1).Except(oneItemSpecific, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemSpecific.Except(Enumerable.Repeat(1, 1).Reverse()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemSpecific.Except(Enumerable.Repeat(1, 1).Reverse(), new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Repeat(1, 1).Reverse().Except(oneItemSpecific); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Repeat(1, 1).Reverse().Except(oneItemSpecific, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemSpecific.Except(Enumerable.Empty<int>().DefaultIfEmpty()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemSpecific.Except(Enumerable.Empty<int>().DefaultIfEmpty(), new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Empty<int>().DefaultIfEmpty().Except(oneItemSpecific); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Empty<int>().DefaultIfEmpty().Except(oneItemSpecific, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemSpecific.Except(Enumerable.Empty<int>().DefaultIfEmpty(4)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemSpecific.Except(Enumerable.Empty<int>().DefaultIfEmpty(4), new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Empty<int>().DefaultIfEmpty(4).Except(oneItemSpecific); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Empty<int>().DefaultIfEmpty(4).Except(oneItemSpecific, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemSpecific.Except(Enumerable.Empty<int>().DefaultIfEmpty().OrderBy(x => x)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemSpecific.Except(Enumerable.Empty<int>().DefaultIfEmpty().OrderBy(x => x), new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Empty<int>().DefaultIfEmpty().OrderBy(x => x).Except(oneItemSpecific); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Empty<int>().DefaultIfEmpty().OrderBy(x => x).Except(oneItemSpecific, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemSpecific.Except(Enumerable.Empty<int>().DefaultIfEmpty(4).OrderBy(x => x)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemSpecific.Except(Enumerable.Empty<int>().DefaultIfEmpty(4).OrderBy(x => x), new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Empty<int>().DefaultIfEmpty(4).OrderBy(x => x).Except(oneItemSpecific); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Empty<int>().DefaultIfEmpty(4).OrderBy(x => x).Except(oneItemSpecific, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                Helper.ForEachEnumerableExpression(
                    oneItemSpecific,
                    new int[0],
                    res => { },
                    @"(a, b) =>
                      {
                        try
                        {
                            a.Except(b);
                            Assert.Fail();
                        }
                        catch(ArgumentException exc)
                        {
                            Assert.AreEqual(""first"", exc.ParamName);
                        }

                        try
                        {
                            a.Except(b, new ExceptTests._IntComparer());
                            Assert.Fail();
                        }
                        catch(ArgumentException exc)
                        {
                            Assert.AreEqual(""first"", exc.ParamName);
                        }

                        try
                        {
                            b.Except(a);
                            Assert.Fail();
                        }
                        catch(ArgumentException exc)
                        {
                            Assert.AreEqual(""second"", exc.ParamName);
                        }

                        try
                        {
                            b.Except(a, new ExceptTests._IntComparer());
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
            }

            // oneItemDefaultOrdered
            {
                try { oneItemDefaultOrdered.Except(Enumerable.Empty<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemDefaultOrdered.Except(Enumerable.Empty<int>(), new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Empty<int>().Except(oneItemDefaultOrdered); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Empty<int>().Except(oneItemDefaultOrdered, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemDefaultOrdered.Except(Enumerable.Range(1, 2)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemDefaultOrdered.Except(Enumerable.Range(1, 2), new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Range(1, 2).Except(oneItemDefaultOrdered); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Range(1, 2).Except(oneItemDefaultOrdered, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemDefaultOrdered.Except(Enumerable.Repeat(1, 1)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemDefaultOrdered.Except(Enumerable.Repeat(1, 1), new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Repeat(1, 1).Except(oneItemDefaultOrdered); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Repeat(1, 1).Except(oneItemDefaultOrdered, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemDefaultOrdered.Except(Enumerable.Repeat(1, 1).Reverse()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemDefaultOrdered.Except(Enumerable.Repeat(1, 1).Reverse(), new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Repeat(1, 1).Reverse().Except(oneItemDefaultOrdered); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Repeat(1, 1).Reverse().Except(oneItemDefaultOrdered, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemDefaultOrdered.Except(Enumerable.Empty<int>().DefaultIfEmpty()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemDefaultOrdered.Except(Enumerable.Empty<int>().DefaultIfEmpty(), new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Empty<int>().DefaultIfEmpty().Except(oneItemDefaultOrdered); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Empty<int>().DefaultIfEmpty().Except(oneItemDefaultOrdered, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemDefaultOrdered.Except(Enumerable.Empty<int>().DefaultIfEmpty(4)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemDefaultOrdered.Except(Enumerable.Empty<int>().DefaultIfEmpty(4), new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Empty<int>().DefaultIfEmpty(4).Except(oneItemDefaultOrdered); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Empty<int>().DefaultIfEmpty(4).Except(oneItemDefaultOrdered, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemDefaultOrdered.Except(Enumerable.Empty<int>().DefaultIfEmpty().OrderBy(x => x)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemDefaultOrdered.Except(Enumerable.Empty<int>().DefaultIfEmpty().OrderBy(x => x), new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Empty<int>().DefaultIfEmpty().OrderBy(x => x).Except(oneItemDefaultOrdered); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Empty<int>().DefaultIfEmpty().OrderBy(x => x).Except(oneItemDefaultOrdered, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemDefaultOrdered.Except(Enumerable.Empty<int>().DefaultIfEmpty(4).OrderBy(x => x)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemDefaultOrdered.Except(Enumerable.Empty<int>().DefaultIfEmpty(4).OrderBy(x => x), new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Empty<int>().DefaultIfEmpty(4).OrderBy(x => x).Except(oneItemDefaultOrdered); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Empty<int>().DefaultIfEmpty(4).OrderBy(x => x).Except(oneItemDefaultOrdered, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                Helper.ForEachEnumerableExpression(
                    oneItemDefaultOrdered,
                    new int[0],
                    res => { },
                    @"(a, b) =>
                      {
                        try
                        {
                            a.Except(b);
                            Assert.Fail();
                        }
                        catch(ArgumentException exc)
                        {
                            Assert.AreEqual(""first"", exc.ParamName);
                        }

                        try
                        {
                            a.Except(b, new ExceptTests._IntComparer());
                            Assert.Fail();
                        }
                        catch(ArgumentException exc)
                        {
                            Assert.AreEqual(""first"", exc.ParamName);
                        }

                        try
                        {
                            b.Except(a);
                            Assert.Fail();
                        }
                        catch(ArgumentException exc)
                        {
                            Assert.AreEqual(""second"", exc.ParamName);
                        }

                        try
                        {
                            b.Except(a, new ExceptTests._IntComparer());
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
            }

            // oneItemSpecificOrdered
            {
                try { oneItemSpecificOrdered.Except(Enumerable.Empty<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemSpecificOrdered.Except(Enumerable.Empty<int>(), new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Empty<int>().Except(oneItemSpecificOrdered); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Empty<int>().Except(oneItemSpecificOrdered, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemSpecificOrdered.Except(Enumerable.Range(1, 2)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemSpecificOrdered.Except(Enumerable.Range(1, 2), new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Range(1, 2).Except(oneItemSpecificOrdered); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Range(1, 2).Except(oneItemSpecificOrdered, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemSpecificOrdered.Except(Enumerable.Repeat(1, 1)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemSpecificOrdered.Except(Enumerable.Repeat(1, 1), new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Repeat(1, 1).Except(oneItemSpecificOrdered); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Repeat(1, 1).Except(oneItemSpecificOrdered, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemSpecificOrdered.Except(Enumerable.Repeat(1, 1).Reverse()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemSpecificOrdered.Except(Enumerable.Repeat(1, 1).Reverse(), new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Repeat(1, 1).Reverse().Except(oneItemSpecificOrdered); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Repeat(1, 1).Reverse().Except(oneItemSpecificOrdered, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemSpecificOrdered.Except(Enumerable.Empty<int>().DefaultIfEmpty()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemSpecificOrdered.Except(Enumerable.Empty<int>().DefaultIfEmpty(), new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Empty<int>().DefaultIfEmpty().Except(oneItemSpecificOrdered); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Empty<int>().DefaultIfEmpty().Except(oneItemSpecificOrdered, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemSpecificOrdered.Except(Enumerable.Empty<int>().DefaultIfEmpty(4)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemSpecificOrdered.Except(Enumerable.Empty<int>().DefaultIfEmpty(4), new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Empty<int>().DefaultIfEmpty(4).Except(oneItemSpecificOrdered); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Empty<int>().DefaultIfEmpty(4).Except(oneItemSpecificOrdered, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemSpecificOrdered.Except(Enumerable.Empty<int>().DefaultIfEmpty().OrderBy(x => x)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemSpecificOrdered.Except(Enumerable.Empty<int>().DefaultIfEmpty().OrderBy(x => x), new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Empty<int>().DefaultIfEmpty().OrderBy(x => x).Except(oneItemSpecificOrdered); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Empty<int>().DefaultIfEmpty().OrderBy(x => x).Except(oneItemSpecificOrdered, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { oneItemSpecificOrdered.Except(Enumerable.Empty<int>().DefaultIfEmpty(4).OrderBy(x => x)); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { oneItemSpecificOrdered.Except(Enumerable.Empty<int>().DefaultIfEmpty(4).OrderBy(x => x), new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("first", exc.ParamName); }
                try { Enumerable.Empty<int>().DefaultIfEmpty(4).OrderBy(x => x).Except(oneItemSpecificOrdered); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }
                try { Enumerable.Empty<int>().DefaultIfEmpty(4).OrderBy(x => x).Except(oneItemSpecificOrdered, new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("second", exc.ParamName); }

                Helper.ForEachEnumerableExpression(
                    oneItemSpecificOrdered,
                    new int[0],
                    res => { },
                    @"(a, b) =>
                      {
                        try
                        {
                            a.Except(b);
                            Assert.Fail();
                        }
                        catch(ArgumentException exc)
                        {
                            Assert.AreEqual(""first"", exc.ParamName);
                        }

                        try
                        {
                            a.Except(b, new ExceptTests._IntComparer());
                            Assert.Fail();
                        }
                        catch(ArgumentException exc)
                        {
                            Assert.AreEqual(""first"", exc.ParamName);
                        }

                        try
                        {
                            b.Except(a);
                            Assert.Fail();
                        }
                        catch(ArgumentException exc)
                        {
                            Assert.AreEqual(""second"", exc.ParamName);
                        }

                        try
                        {
                            b.Except(a, new ExceptTests._IntComparer());
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
            }
        }
    }
}
