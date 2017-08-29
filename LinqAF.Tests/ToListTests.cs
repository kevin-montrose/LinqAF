using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using TestHelpers;

namespace LinqAF.Tests
{
    [TestClass]
    public class ToListTests
    {
        [TestMethod]
        public void Universal()
        {
            var enums = Helper.AllEnumerables();

            foreach (var e in enums)
            {
                System.Collections.Generic.List<string> missing;
                if (!Helper.Implements(e, typeof(LinqAF.Impl.IToList<>), out missing))
                {
                    Assert.Fail($"{e.Name} does not implement IToList ({string.Join(", ", missing)})");
                }
            }
        }

        [TestMethod]
        public void Simple()
        {
            var e = Enumerable.Range(0, 5).Select(x => x * 2);
            var res = e.ToList();

            Assert.AreEqual(5, res.Count);
            Assert.AreEqual(0, res[0]);
            Assert.AreEqual(2, res[1]);
            Assert.AreEqual(4, res[2]);
            Assert.AreEqual(6, res[3]);
            Assert.AreEqual(8, res[4]);
        }

        [TestMethod]
        public void Empty()
        {
            var e1 = Enumerable.Empty<int>();
            var res1 = e1.ToList();

            Assert.AreEqual(0, res1.Count);

            var e2 = new[] { 1, 2, 3 }.Where(x => x > 4);
            var res2 = e2.ToList();

            Assert.AreEqual(0, res2.Count);
        }

        [TestMethod]
        public void Chaining()
        {
            foreach(var e in Helper.GetEnumerables(new [] {  "a", "bb", "ccc"}))
            {
                var x = e.ToList();
                Assert.AreEqual(typeof(System.Collections.Generic.List<string>), x.GetType());
                var y = (System.Collections.Generic.List<string>)x;
                Assert.AreEqual(3, y.Count);
                Assert.AreEqual("a", y[0]);
                Assert.AreEqual("bb", y[1]);
                Assert.AreEqual("ccc", y[2]);
            }
        }

        class _GroupingComparer<T> : IEqualityComparer<GroupingEnumerable<T, T>>
        {
            public bool Equals(GroupingEnumerable<T, T> x, GroupingEnumerable<T, T> y)
            {
                if (!x.Key.Equals(y.Key)) return false;

                return x.SequenceEqual(y);
            }

            public int GetHashCode(GroupingEnumerable<T, T> obj)
            {
                var x = obj.Key.GetHashCode();
                foreach (var item in obj)
                {
                    x *= 17;
                    x += item.GetHashCode();
                }

                return x;
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

            Assert.IsTrue(empty.ToList().SequenceEqual(new int[0]));
            Assert.IsTrue(emptyOrdered.ToList().SequenceEqual(new int[0]));
            Assert.IsTrue(groupByDefault.ToList().SequenceEqual(new[] { groupByDefault.ElementAt(0), groupByDefault.ElementAt(1), groupByDefault.ElementAt(2) }, new _GroupingComparer<int>()));
            Assert.IsTrue(groupBySpecific.ToList().SequenceEqual(new[] { groupBySpecific.ElementAt(0), groupBySpecific.ElementAt(1), groupBySpecific.ElementAt(2) }, new _GroupingComparer<string>()));
            Assert.IsTrue(lookupDefault.ToList().SequenceEqual(new[] { groupByDefault.ElementAt(0), groupByDefault.ElementAt(1), groupByDefault.ElementAt(2) }, new _GroupingComparer<int>()));
            Assert.IsTrue(lookupSpecific.ToList().SequenceEqual(new[] { groupByDefault.ElementAt(0), groupByDefault.ElementAt(1), groupByDefault.ElementAt(2) }, new _GroupingComparer<int>()));
            Assert.IsTrue(range.ToList().SequenceEqual(new[] { 1, 2, 3, 4, 5 }));
            Assert.IsTrue(repeat.ToList().SequenceEqual(new[] { "foo", "foo", "foo", "foo", "foo" }));
            Assert.IsTrue(reverseRange.ToList().SequenceEqual(new[] { 5, 4, 3, 2, 1 }));
            Assert.IsTrue(oneItemDefault.ToList().SequenceEqual(new[] { 0 }));
            Assert.IsTrue(oneItemSpecific.ToList().SequenceEqual(new[] { 4 }));
            Assert.IsTrue(oneItemDefaultOrdered.ToList().SequenceEqual(new[] { 0 }));
            Assert.IsTrue(oneItemSpecificOrdered.ToList().SequenceEqual(new[] { 4 }));
        }

        [TestMethod]
        public void Malformed()
        {
            Helper.ForEachMalformedEnumerableExpression<string>(
                @"a =>
                  {
                    try
                    {
                        a.ToList();
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

            try
            {
                empty.ToList();
                Assert.Fail();
            }
            catch (ArgumentException exc)
            {
                Assert.AreEqual("source", exc.ParamName);
            }

            try
            {
                emptyOrdered.ToList();
                Assert.Fail();
            }
            catch (ArgumentException exc)
            {
                Assert.AreEqual("source", exc.ParamName);
            }

            try
            {
                groupByDefault.ToList();
                Assert.Fail();
            }
            catch (ArgumentException exc)
            {
                Assert.AreEqual("source", exc.ParamName);
            }

            try
            {
                groupBySpecific.ToList();
                Assert.Fail();
            }
            catch (ArgumentException exc)
            {
                Assert.AreEqual("source", exc.ParamName);
            }

            try
            {
                lookupDefault.ToList();
                Assert.Fail();
            }
            catch (ArgumentException exc)
            {
                Assert.AreEqual("source", exc.ParamName);
            }

            try
            {
                lookupSpecific.ToList();
                Assert.Fail();
            }
            catch (ArgumentException exc)
            {
                Assert.AreEqual("source", exc.ParamName);
            }

            try
            {
                range.ToList();
                Assert.Fail();
            }
            catch (ArgumentException exc)
            {
                Assert.AreEqual("source", exc.ParamName);
            }

            try
            {
                repeat.ToList();
                Assert.Fail();
            }
            catch (ArgumentException exc)
            {
                Assert.AreEqual("source", exc.ParamName);
            }

            try
            {
                reverseRange.ToList();
                Assert.Fail();
            }
            catch (ArgumentException exc)
            {
                Assert.AreEqual("source", exc.ParamName);
            }

            try
            {
                oneItemDefault.ToList();
                Assert.Fail();
            }
            catch (ArgumentException exc)
            {
                Assert.AreEqual("source", exc.ParamName);
            }

            try
            {
                oneItemSpecific.ToList();
                Assert.Fail();
            }
            catch (ArgumentException exc)
            {
                Assert.AreEqual("source", exc.ParamName);
            }

            try
            {
                oneItemDefaultOrdered.ToList();
                Assert.Fail();
            }
            catch (ArgumentException exc)
            {
                Assert.AreEqual("source", exc.ParamName);
            }

            try
            {
                oneItemSpecificOrdered.ToList();
                Assert.Fail();
            }
            catch (ArgumentException exc)
            {
                Assert.AreEqual("source", exc.ParamName);
            }
        }
    }
}
