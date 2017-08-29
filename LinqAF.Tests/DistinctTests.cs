using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using TestHelpers;

namespace LinqAF.Tests
{
    [TestClass]
    public class DistinctTests
    {
        [TestMethod]
        public void Universal()
        {
            var enums = Helper.AllEnumerables();

            foreach (var e in enums)
            {
                System.Collections.Generic.List<string> missing;
                if (!Helper.Implements(e, typeof(LinqAF.Impl.IDistinct<,,>), out missing))
                {
                    Assert.Fail($"{e.Name} does not implement IDistinct ({string.Join(", ", missing)})");
                }
            }
        }

        [TestMethod]
        public void SimpleDefault()
        {
            {
                var asDistinct = new[] { 1, 2, 3, 4, 4, 3, 2, 1 }.Distinct();

                Assert.IsTrue(asDistinct.GetType().IsValueType);

                var res = new List<int>();
                foreach (var item in asDistinct)
                {
                    res.Add(item);
                }

                Assert.AreEqual(4, res.Count);
                Assert.AreEqual(1, res[0]);
                Assert.AreEqual(2, res[1]);
                Assert.AreEqual(3, res[2]);
                Assert.AreEqual(4, res[3]);
            }
        }

        [TestMethod]
        public void SimpleSpecific()
        {
            {
                var asDistinct = new[] { "hello", "world", "foo", "bar", "HELLO", "BAR" }.Distinct(StringComparer.InvariantCultureIgnoreCase);

                Assert.IsTrue(asDistinct.GetType().IsValueType);

                var res = new List<string>();
                foreach (var item in asDistinct)
                {
                    res.Add(item);
                }

                Assert.AreEqual(4, res.Count);
                Assert.AreEqual("hello", res[0]);
                Assert.AreEqual("world", res[1]);
                Assert.AreEqual("foo", res[2]);
                Assert.AreEqual("bar", res[3]);
            }
        }

        public class _Chaining : IEqualityComparer<string>
        {
            public bool Equals(string x, string y) => x.Length == y.Length;

            public int GetHashCode(string obj) => obj.Length;
        }

        [TestMethod]
        public void Chaining()
        {
            // default
            {
                Helper.ForEachEnumerableExpression(
                    new object[0],
                    new[] { 1, 2, 3, 3, 4, 2, 5 },
                    res =>
                    {
                        Assert.AreEqual(5, res.Count);
                        Assert.AreEqual(1, res[0]);
                        Assert.AreEqual(2, res[1]);
                        Assert.AreEqual(3, res[2]);
                        Assert.AreEqual(4, res[3]);
                        Assert.AreEqual(5, res[4]);
                    },
                    "(_, a)  => a.Distinct()",
                    typeof(DistinctDefaultEnumerable<,,>),
                    typeof(DistinctSpecificEnumerable<,,>),
                    typeof(EmptyEnumerable<>),
                    typeof(EmptyOrderedEnumerable<>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>),
                    typeof(ExceptDefaultEnumerable<,,,,>),
                    typeof(ExceptSpecificEnumerable<,,,,>),
                    typeof(UnionDefaultEnumerable<,,,,>),
                    typeof(UnionSpecificEnumerable<,,,,>)
                );
            }

            // specific
            {
                Helper.ForEachEnumerableExpression(
                    new object[0],
                    new[] { "hello", "world", "fizz", "buzz" },
                    res =>
                    {
                        Assert.AreEqual(2, res.Count);
                        Assert.AreEqual("hello", res[0]);
                        Assert.AreEqual("fizz", res[1]);
                    },
                    "(_, a) => a.Distinct(new DistinctTests._Chaining())",
                    typeof(EmptyEnumerable<>),
                    typeof(EmptyOrderedEnumerable<>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupDefaultEnumerable<,>),
                    typeof(LookupSpecificEnumerable<,>)
                );
            }
        }

        class _IntComparer : IEqualityComparer<int>
        {
            public bool Equals(int x, int y) => x == y;

            public int GetHashCode(int obj) => obj;
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
                Assert.IsTrue(empty.Distinct().SequenceEqual(new int[0]));
                Assert.IsTrue(empty.Distinct(new _IntComparer()).SequenceEqual(new int[0]));
            }

            // emptyOrdered
            {
                Assert.IsTrue(emptyOrdered.Distinct().SequenceEqual(new int[0]));
                Assert.IsTrue(emptyOrdered.Distinct(new _IntComparer()).SequenceEqual(new int[0]));
            }

            // groupByDefault
            {
                Assert.IsTrue(groupByDefault.Distinct().SequenceEqual(new[] { groupByDefault.ElementAt(0), groupByDefault.ElementAt(1), groupByDefault.ElementAt(2) }, new _GroupingComparer<int>()));
                Assert.IsTrue(groupByDefault.Distinct(new _GroupingComparer<int>()).SequenceEqual(new[] { groupByDefault.ElementAt(0), groupByDefault.ElementAt(1), groupByDefault.ElementAt(2) }, new _GroupingComparer<int>()));
            }

            // groupBySpecific
            {
                Assert.IsTrue(groupBySpecific.Distinct().SequenceEqual(new[] { groupBySpecific.ElementAt(0), groupBySpecific.ElementAt(1), groupBySpecific.ElementAt(2) }, new _GroupingComparer<string>()));
                Assert.IsTrue(groupBySpecific.Distinct(new _GroupingComparer<string>()).SequenceEqual(new[] { groupBySpecific.ElementAt(0), groupBySpecific.ElementAt(1), groupBySpecific.ElementAt(2) }, new _GroupingComparer<string>()));
            }

            // lookupDefault
            {
                Assert.IsTrue(lookupDefault.Distinct().SequenceEqual(new[] { lookupDefault.ElementAt(0), lookupDefault.ElementAt(1), lookupDefault.ElementAt(2) }, new _GroupingComparer<int>()));
                Assert.IsTrue(lookupDefault.Distinct(new _GroupingComparer<int>()).SequenceEqual(new[] { lookupDefault.ElementAt(0), lookupDefault.ElementAt(1), lookupDefault.ElementAt(2) }, new _GroupingComparer<int>()));
            }

            // lookupSpecific
            {
                Assert.IsTrue(lookupSpecific.Distinct().SequenceEqual(new[] { lookupSpecific.ElementAt(0), lookupSpecific.ElementAt(1), lookupSpecific.ElementAt(2) }, new _GroupingComparer<int>()));
                Assert.IsTrue(lookupSpecific.Distinct(new _GroupingComparer<int>()).SequenceEqual(new[] { lookupSpecific.ElementAt(0), lookupSpecific.ElementAt(1), lookupSpecific.ElementAt(2) }, new _GroupingComparer<int>()));
            }

            // range
            {
                Assert.IsTrue(range.Distinct().SequenceEqual(new[] { 1, 2, 3, 4, 5 }));
                Assert.IsTrue(range.Distinct(new _IntComparer()).SequenceEqual(new[] { 1, 2, 3, 4, 5 }));
            }

            // repeat
            {
                Assert.IsTrue(repeat.Distinct().SequenceEqual(new[] { "foo" }));
                Assert.IsTrue(repeat.Distinct(StringComparer.InvariantCultureIgnoreCase).SequenceEqual(new[] { "foo" }));
            }

            // reverseRange
            {
                Assert.IsTrue(reverseRange.Distinct().SequenceEqual(new[] { 5, 4, 3, 2, 1 }));
                Assert.IsTrue(reverseRange.Distinct(new _IntComparer()).SequenceEqual(new[] { 5, 4, 3, 2, 1 }));
            }

            // oneItemDefault
            {
                Assert.IsTrue(oneItemDefault.Distinct().SequenceEqual(new[] { 0 }));
                Assert.IsTrue(oneItemDefault.Distinct(new _IntComparer()).SequenceEqual(new[] { 0 }));
            }

            // oneItemSpecific
            {
                Assert.IsTrue(oneItemSpecific.Distinct().SequenceEqual(new[] { 4 }));
                Assert.IsTrue(oneItemSpecific.Distinct(new _IntComparer()).SequenceEqual(new[] { 4 }));
            }

            // oneItemDefaultOrdered
            {
                Assert.IsTrue(oneItemDefaultOrdered.Distinct().SequenceEqual(new[] { 0 }));
                Assert.IsTrue(oneItemDefaultOrdered.Distinct(new _IntComparer()).SequenceEqual(new[] { 0 }));
            }

            // oneItemSpecificOrdered
            {
                Assert.IsTrue(oneItemSpecificOrdered.Distinct().SequenceEqual(new[] { 4 }));
                Assert.IsTrue(oneItemSpecificOrdered.Distinct(new _IntComparer()).SequenceEqual(new[] { 4 }));
            }
        }

        [TestMethod]
        public void Chaining_Dictionary()
        {
            var dict = new System.Collections.Generic.Dictionary<int, int> { [1] = 2, [3] = 4 };
            var sortedDict = new System.Collections.Generic.SortedDictionary<int, int> { [1] = 2, [3] = 4 };

            Assert.IsTrue(dict.SequenceEqual(dict.Distinct()));
            Assert.IsTrue(dict.SequenceEqual(dict.Distinct(null)));
            Assert.IsTrue(sortedDict.SequenceEqual(sortedDict.Distinct()));
            Assert.IsTrue(sortedDict.SequenceEqual(sortedDict.Distinct(null)));
        }

        [TestMethod]
        public void Malformed()
        {
            Helper.ForEachMalformedEnumerableExpression<int>(
                @"a =>
                  {
                    try
                    {
                        a.Distinct();
                        Assert.Fail();
                    }
                    catch(ArgumentException exc)
                    {
                        Assert.AreEqual(""source"", exc.ParamName);
                    }
                  }",
                typeof(GroupByDefaultEnumerable<,,,,>),
                typeof(GroupBySpecificEnumerable<,,,,>),
                typeof(LookupDefaultEnumerable<,>),
                typeof(LookupSpecificEnumerable<,>)
            );

            Helper.ForEachMalformedEnumerableExpression<string>(
                @"a =>
                  {
                    try
                    {
                        a.Distinct(new DistinctTests._Chaining());
                        Assert.Fail();
                    }
                    catch(ArgumentException exc)
                    {
                        Assert.AreEqual(""source"", exc.ParamName);
                    }
                  }",
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
            var range = new RangeEnumerable<int>();
            var repeat = new RepeatEnumerable<int>();
            var reverseRange = new ReverseRangeEnumerable<int>();
            var oneItemDefault = new OneItemDefaultEnumerable<int>();
            var oneItemSpecific = new OneItemSpecificEnumerable<int>();
            var oneItemDefaultOrdered = new OneItemDefaultOrderedEnumerable<int>();
            var oneItemSpecificOrdered = new OneItemSpecificOrderedEnumerable<int>();

            // empty
            {
                try { empty.Distinct(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { empty.Distinct(new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // emptyOrdered
            {
                try { emptyOrdered.Distinct(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { emptyOrdered.Distinct(new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // groupByDefault
            {
                try { groupByDefault.Distinct(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupByDefault.Distinct(new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // groupBySpecific
            {
                try { groupBySpecific.Distinct(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupBySpecific.Distinct(new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // lookupDefault
            {
                try { lookupDefault.Distinct(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookupDefault.Distinct(new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // lookupSpecific
            {
                try { lookupSpecific.Distinct(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookupSpecific.Distinct(new _GroupingComparer<int>()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // range
            {
                try { range.Distinct(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { range.Distinct(new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // repeat
            {
                try { repeat.Distinct(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { repeat.Distinct(new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // reverseRange
            {
                try { reverseRange.Distinct(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { reverseRange.Distinct(new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // oneItemDefault
            {
                try { oneItemDefault.Distinct(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefault.Distinct(new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // oneItemSpecific
            {
                try { oneItemSpecific.Distinct(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecific.Distinct(new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // oneItemDefaultOrdered
            {
                try { oneItemDefaultOrdered.Distinct(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefaultOrdered.Distinct(new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // oneItemSpecificOrdered
            {
                try { oneItemSpecificOrdered.Distinct(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecificOrdered.Distinct(new _IntComparer()); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }
        }
    }
}
