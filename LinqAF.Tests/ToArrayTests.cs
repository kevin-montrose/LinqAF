using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using TestHelpers;
using System.Reflection;
using System.Text;

namespace LinqAF.Tests
{
    [TestClass]
    public class ToArrayTests
    {
        [TestMethod]
        public void InstanceExtensionNoOverlap()
        {
            Dictionary<MethodInfo, List<MethodInfo>> instOverlaps, extOverlaps;
            Helper.GetOverlappingMethods(typeof(Impl.IToArray<>), out instOverlaps, out extOverlaps);

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
        public void Universal()
        {
            var enums = Helper.AllEnumerables();

            foreach (var e in enums)
            {
                System.Collections.Generic.List<string> missing;
                if (!Helper.Implements(e, typeof(LinqAF.Impl.IToArray<>), out missing))
                {
                    Assert.Fail($"{e.Name} does not implement IToArray ({string.Join(", ", missing)})");
                }
            }
        }

        [TestMethod]
        public void Simple()
        {
            var e = Enumerable.Range(0, 5).Select(x => x * 2);
            var res = e.ToArray();

            Assert.AreEqual(5, res.Length);
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
            var res1 = e1.ToArray();

            Assert.AreEqual(0, res1.Length);

            var e2 = new[] { 1, 2, 3 }.Where(x => x > 4);
            var res2 = e2.ToArray();

            Assert.AreEqual(0, res2.Length);
        }

        [TestMethod]
        public void Chaining()
        {
            foreach (var e in Helper.GetEnumerables(new[] { 1, 3, 2 }))
            {
                var x = e.ToArray();
                Assert.AreEqual(typeof(int[]), x.GetType());
                var y = (int[])x;
                Assert.AreEqual(3, y.Length);
                Assert.AreEqual(1, y[0]);
                Assert.AreEqual(3, y[1]);
                Assert.AreEqual(2, y[2]);
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

            Assert.IsTrue(empty.ToArray().SequenceEqual(new int[0]));
            Assert.IsTrue(emptyOrdered.ToArray().SequenceEqual(new int[0]));
            Assert.IsTrue(groupByDefault.ToArray().SequenceEqual(new[] { groupByDefault.ElementAt(0), groupByDefault.ElementAt(1), groupByDefault.ElementAt(2) }, new _GroupingComparer<int>()));
            Assert.IsTrue(groupBySpecific.ToArray().SequenceEqual(new[] { groupBySpecific.ElementAt(0), groupBySpecific.ElementAt(1), groupBySpecific.ElementAt(2) }, new _GroupingComparer<string>()));
            Assert.IsTrue(lookupDefault.ToArray().SequenceEqual(new[] { groupByDefault.ElementAt(0), groupByDefault.ElementAt(1), groupByDefault.ElementAt(2) }, new _GroupingComparer<int>()));
            Assert.IsTrue(lookupSpecific.ToArray().SequenceEqual(new[] { groupByDefault.ElementAt(0), groupByDefault.ElementAt(1), groupByDefault.ElementAt(2) }, new _GroupingComparer<int>()));
            Assert.IsTrue(range.ToArray().SequenceEqual(new[] { 1, 2, 3, 4, 5 }));
            Assert.IsTrue(repeat.ToArray().SequenceEqual(new[] { "foo", "foo", "foo", "foo", "foo" }));
            Assert.IsTrue(reverseRange.ToArray().SequenceEqual(new[] { 5, 4, 3, 2, 1 }));
            Assert.IsTrue(oneItemDefault.ToArray().SequenceEqual(new[] { 0 }));
            Assert.IsTrue(oneItemSpecific.ToArray().SequenceEqual(new[] { 4 }));
            Assert.IsTrue(oneItemDefaultOrdered.ToArray().SequenceEqual(new[] { 0 }));
            Assert.IsTrue(oneItemSpecificOrdered.ToArray().SequenceEqual(new[] { 4 }));
        }

        [TestMethod]
        public void Malformed()
        {
            Helper.ForEachMalformedEnumerableExpression<string>(
                @"a =>
                  {
                    try
                    {
                        a.ToArray();
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
            var range = new RangeEnumerable();
            var repeat = new RepeatEnumerable<int>();
            var reverseRange = new ReverseRangeEnumerable();
            var oneItemDefault = new OneItemDefaultEnumerable<int>();
            var oneItemSpecific = new OneItemSpecificEnumerable<int>();
            var oneItemDefaultOrdered = new OneItemDefaultOrderedEnumerable<int>();
            var oneItemSpecificOrdered = new OneItemSpecificOrderedEnumerable<int>();

            try
            {
                empty.ToArray();
                Assert.Fail();
            }
            catch (ArgumentException exc)
            {
                Assert.AreEqual("source", exc.ParamName);
            }

            try
            {
                emptyOrdered.ToArray();
                Assert.Fail();
            }
            catch (ArgumentException exc)
            {
                Assert.AreEqual("source", exc.ParamName);
            }

            try
            {
                groupByDefault.ToArray();
                Assert.Fail();
            }
            catch (ArgumentException exc)
            {
                Assert.AreEqual("source", exc.ParamName);
            }

            try
            {
                groupBySpecific.ToArray();
                Assert.Fail();
            }
            catch (ArgumentException exc)
            {
                Assert.AreEqual("source", exc.ParamName);
            }

            try
            {
                lookupDefault.ToArray();
                Assert.Fail();
            }
            catch (ArgumentException exc)
            {
                Assert.AreEqual("source", exc.ParamName);
            }

            try
            {
                lookupSpecific.ToArray();
                Assert.Fail();
            }
            catch (ArgumentException exc)
            {
                Assert.AreEqual("source", exc.ParamName);
            }

            try
            {
                range.ToArray();
                Assert.Fail();
            }
            catch (ArgumentException exc)
            {
                Assert.AreEqual("source", exc.ParamName);
            }

            try
            {
                repeat.ToArray();
                Assert.Fail();
            }
            catch (ArgumentException exc)
            {
                Assert.AreEqual("source", exc.ParamName);
            }

            try
            {
                reverseRange.ToArray();
                Assert.Fail();
            }
            catch (ArgumentException exc)
            {
                Assert.AreEqual("source", exc.ParamName);
            }

            try
            {
                oneItemDefault.ToArray();
                Assert.Fail();
            }
            catch (ArgumentException exc)
            {
                Assert.AreEqual("source", exc.ParamName);
            }

            try
            {
                oneItemSpecific.ToArray();
                Assert.Fail();
            }
            catch (ArgumentException exc)
            {
                Assert.AreEqual("source", exc.ParamName);
            }

            try
            {
                oneItemDefaultOrdered.ToArray();
                Assert.Fail();
            }
            catch (ArgumentException exc)
            {
                Assert.AreEqual("source", exc.ParamName);
            }

            try
            {
                oneItemSpecificOrdered.ToArray();
                Assert.Fail();
            }
            catch (ArgumentException exc)
            {
                Assert.AreEqual("source", exc.ParamName);
            }
        }
    }
}
