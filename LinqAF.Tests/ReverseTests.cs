using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using TestHelpers;

namespace LinqAF.Tests
{
    [TestClass]
    public class ReverseTests
    {
        [TestMethod]
        public void Universal()
        {
            var enums = Helper.AllEnumerables();

            foreach (var e in enums)
            {
                System.Collections.Generic.List<string> missing;
                if (!Helper.Implements(e, typeof(LinqAF.Impl.IReverse<,,>), out missing))
                {
                    Assert.Fail($"{e.Name} does not implement IReverse ({string.Join(", ", missing)})");
                }
            }
        }

        [TestMethod]
        public void Simple()
        {
            var asReverse = new[] { 1, 2, 3 }.Reverse();

            Assert.IsTrue(asReverse.GetType().IsValueType);

            var res = new List<int>();
            foreach(var item in asReverse)
            {
                res.Add(item);
            }

            Assert.AreEqual(3, res.Count);
            Assert.AreEqual(3, res[0]);
            Assert.AreEqual(2, res[1]);
            Assert.AreEqual(1, res[2]);
        }

        [TestMethod]
        public void ReverseRange()
        {
            var asRange = Enumerable.Range(10, 5);
            var asReverse = asRange.Reverse();

            Assert.IsTrue(asReverse.GetType().IsValueType);

            var res = new List<int>();
            foreach(var item in asReverse)
            {
                res.Add(item);
            }

            Assert.AreEqual(5, res.Count);
            Assert.AreEqual(14, res[0]);
            Assert.AreEqual(13, res[1]);
            Assert.AreEqual(12, res[2]);
            Assert.AreEqual(11, res[3]);
            Assert.AreEqual(10, res[4]);
        }

        [TestMethod]
        public void ReverseReverseRange()
        {
            var asRange = Enumerable.Range(10, 5);
            var asReverse = asRange.Reverse();
            var asReverseReverse = asReverse.Reverse();

            Assert.IsTrue(asReverseReverse.GetType().IsValueType);

            var res = new List<int>();
            foreach (var item in asReverseReverse)
            {
                res.Add(item);
            }

            Assert.AreEqual(5, res.Count);
            Assert.AreEqual(10, res[0]);
            Assert.AreEqual(11, res[1]);
            Assert.AreEqual(12, res[2]);
            Assert.AreEqual(13, res[3]);
            Assert.AreEqual(14, res[4]);
        }

        [TestMethod]
        public void Chaining()
        {
            Helper.ForEachEnumerableExpression(
                new object[0],
                new[] { 1, 3, 5, 7, 9 },
                res =>
                {
                    Assert.AreEqual(5, res.Count);
                    Assert.AreEqual(9, res[0]);
                    Assert.AreEqual(7, res[1]);
                    Assert.AreEqual(5, res[2]);
                    Assert.AreEqual(3, res[3]);
                    Assert.AreEqual(1, res[4]);
                },
                "(_, a) => a.Reverse()",
                typeof(List<>),
                typeof(SortedSet<>),
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

            Assert.IsTrue(empty.Reverse().SequenceEqual(new int[0]));
            Assert.IsTrue(emptyOrdered.Reverse().SequenceEqual(new int[0]));
            Assert.IsTrue(groupByDefault.Reverse().SequenceEqual(new[] { groupByDefault.ElementAt(2), groupByDefault.ElementAt(1), groupByDefault.ElementAt(0) }, new _GroupingComparer<int>()));
            Assert.IsTrue(groupBySpecific.Reverse().SequenceEqual(new[] { groupBySpecific.ElementAt(2), groupBySpecific.ElementAt(1), groupBySpecific.ElementAt(0) }, new _GroupingComparer<string>()));
            Assert.IsTrue(lookupDefault.Reverse().SequenceEqual(new[] { lookupDefault.ElementAt(2), lookupDefault.ElementAt(1), lookupDefault.ElementAt(0) }, new _GroupingComparer<int>()));
            Assert.IsTrue(lookupSpecific.Reverse().SequenceEqual(new[] { lookupSpecific.ElementAt(2), lookupSpecific.ElementAt(1), lookupSpecific.ElementAt(0) }, new _GroupingComparer<int>()));
            Assert.IsTrue(range.Reverse().SequenceEqual(new[] { 5, 4, 3, 2, 1 }));
            Assert.IsTrue(repeat.Reverse().SequenceEqual(new[] { "foo", "foo", "foo", "foo", "foo" }));
            Assert.IsTrue(reverseRange.Reverse().SequenceEqual(new[] { 1, 2, 3, 4, 5 }));
            Assert.IsTrue(oneItemDefault.Reverse().SequenceEqual(new[] { 0 }));
            Assert.IsTrue(oneItemSpecific.Reverse().SequenceEqual(new[] { 4 }));
            Assert.IsTrue(oneItemDefaultOrdered.Reverse().SequenceEqual(new[] { 0 }));
            Assert.IsTrue(oneItemSpecificOrdered.Reverse().SequenceEqual(new[] { 4 }));
        }

        [TestMethod]
        public void Malformed()
        {
            Helper.ForEachMalformedEnumerableExpression<string>(
                @"a =>
                  {
                    try
                    {
                        a.Reverse();
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

            try { empty.Reverse(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            try { emptyOrdered.Reverse(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            try { groupByDefault.Reverse(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            try { groupBySpecific.Reverse(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            try { lookupDefault.Reverse(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            try { lookupSpecific.Reverse(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            try { range.Reverse(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            try { repeat.Reverse(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            try { reverseRange.Reverse(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            try { oneItemDefault.Reverse(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            try { oneItemSpecific.Reverse(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            try { oneItemDefaultOrdered.Reverse(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            try { oneItemSpecificOrdered.Reverse(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
        }
    }
}
