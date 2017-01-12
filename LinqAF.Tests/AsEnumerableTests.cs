using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using TestHelpers;

namespace LinqAF.Tests
{
    [TestClass]
    public class AsEnumerableTests
    {
        [TestMethod]
        public void Universal()
        {
            var enums = Helper.AllEnumerables();

            foreach (var e in enums)
            {
                System.Collections.Generic.List<string> missing;
                if (!Helper.Implements(e, typeof(Impl.IElementAt<>), out missing))
                {
                    Assert.Fail($"{e.Name} does not implement IElementAt ({string.Join(", ", missing)})");
                }
            }
        }

        [TestMethod]
        public void Chaining()
        {
            foreach (var e in Helper.GetEnumerables(new[] { 2, 4, 6 }))
            {
                IEnumerable<int> asE = e.AsEnumerable();

                var res = new List<int>();
                foreach(var item in asE)
                {
                    res.Add(item);
                }

                Assert.IsTrue(res.SequenceEqual(new[] { 2, 4, 6 }));
            }
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
                Assert.IsTrue(empty.SequenceEqual(empty.AsEnumerable()));
            }

            // emptyOrdered
            {
                Assert.IsTrue(emptyOrdered.SequenceEqual(emptyOrdered.AsEnumerable()));
            }

            // groupByDefault
            {
                Assert.IsTrue(groupByDefault.SequenceEqual(groupByDefault.AsEnumerable(), new _GroupingComparer<int>()));
            }

            // groupBySpecific
            {
                Assert.IsTrue(groupBySpecific.SequenceEqual(groupBySpecific.AsEnumerable(), new _GroupingComparer<string>()));
            }

            // lookup
            {
                Assert.IsTrue(lookup.SequenceEqual(lookup.AsEnumerable(), new _GroupingComparer<int>()));
            }

            // range
            {
                Assert.IsTrue(range.SequenceEqual(range.AsEnumerable()));
            }

            // repeat
            {
                Assert.IsTrue(repeat.SequenceEqual(repeat.AsEnumerable()));
            }

            // reverseRange
            {
                Assert.IsTrue(reverseRange.SequenceEqual(reverseRange.AsEnumerable()));
            }

            // oneItemDefault
            {
                Assert.IsTrue(oneItemDefault.SequenceEqual(oneItemDefault.AsEnumerable()));
            }

            // oneItemSpecific
            {
                Assert.IsTrue(oneItemSpecific.SequenceEqual(oneItemSpecific.AsEnumerable()));
            }

            // oneItemDefaultOrdered
            {
                Assert.IsTrue(oneItemDefaultOrdered.SequenceEqual(oneItemDefaultOrdered.AsEnumerable()));
            }

            // oneItemSpecificOrdered
            {
                Assert.IsTrue(oneItemSpecificOrdered.SequenceEqual(oneItemSpecificOrdered.AsEnumerable()));
            }
        }

        [TestMethod]
        public void Malformed()
        {
            foreach (var e in Helper.GetMalformedEnumerables<string>())
            {
                try { e.AsEnumerable(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }
        }

        [TestMethod]
        public void Malformed_Weird()
        {
            var empty = new EmptyEnumerable<int>();
            var emptyOrdered = new EmptyOrderedEnumerable<int>();
            var groupByDefault = new GroupByDefaultEnumerable<int, int, int, EmptyEnumerable<int>, EmptyEnumerator<int>>();
            var groupBySpecific = new GroupBySpecificEnumerable<int, int, int, EmptyEnumerable<int>, EmptyEnumerator<int>>();
            var lookup = new LookupEnumerable<int, int>();
            var range = new RangeEnumerable<int>();
            var repeat = new RepeatEnumerable<int>();
            var reverseRange = new ReverseRangeEnumerable<int>();
            var oneItemDefault = new OneItemDefaultEnumerable<int>();
            var oneItemSpecific = new OneItemSpecificEnumerable<int>();
            var oneItemDefaultOrdered = new OneItemDefaultOrderedEnumerable<int>();
            var oneItemSpecificOrdered = new OneItemSpecificOrderedEnumerable<int>();

            // empty
            {
                try { empty.AsEnumerable(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // emptyOrdered
            {
                try { emptyOrdered.AsEnumerable(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // groupByDefault
            {
                try { groupByDefault.AsEnumerable(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // groupBySpecific
            {
                try { groupBySpecific.AsEnumerable(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // lookup
            {
                try { lookup.AsEnumerable(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // range
            {
                try { range.AsEnumerable(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // repeat
            {
                try { repeat.AsEnumerable(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // reverseRange
            {
                try { reverseRange.AsEnumerable(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // oneItemDefault
            {
                try { oneItemDefault.AsEnumerable(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // oneItemSpecific
            {
                try { oneItemSpecific.AsEnumerable(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // oneItemDefaultOrdered
            {
                try { oneItemDefaultOrdered.AsEnumerable(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // oneItemSpecificOrdered
            {
                try { oneItemSpecificOrdered.AsEnumerable(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }
        }
    }
}
