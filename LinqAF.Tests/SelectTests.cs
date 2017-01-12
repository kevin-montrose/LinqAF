using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using LinqAF;
using TestHelpers;

namespace LinqAF.Tests
{
    [TestClass]
    public class SelectTests
    {
        [TestMethod]
        public void Universal()
        {
            var enums = Helper.AllEnumerables();

            foreach (var e in enums)
            {
                System.Collections.Generic.List<string> missing;
                if (!Helper.Implements(e, typeof(LinqAF.Impl.ISelect<,,>), out missing))
                {
                    Assert.Fail($"{e.Name} does not implement ISelect ({string.Join(", ", missing)})");
                }
            }
        }

        [TestMethod]
        public void Chaining()
        {
            var vals = new[] { "foo", "bar", "fizz", "buzz" };

            // simple
            {
                Helper.ForEachEnumerableExpression(
                    vals,
                    new[] { 1, 2, 3 },
                    res =>
                    {
                        Assert.AreEqual(3, res.Count);
                        Assert.AreEqual("bar", res[0]);
                        Assert.AreEqual("fizz", res[1]);
                        Assert.AreEqual("buzz", res[2]);
                    },
                    "(vals, a) => a.Select(x => vals[x])",
                    typeof(EmptyEnumerable<>),
                    typeof(EmptyOrderedEnumerable<>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupEnumerable<,>)
                );
            }

            // indexed
            {
                Helper.ForEachEnumerableExpression(
                    vals,
                    new[] { 1, 2, 3 },
                    res =>
                    {
                        Assert.AreEqual(3, res.Count);
                        Assert.AreEqual("barfoo", res[0]);
                        Assert.AreEqual("fizzbar", res[1]);
                        Assert.AreEqual("buzzfizz", res[2]);
                    },
                    "(vals, a) => a.Select((x, ix) => vals[x] + vals[ix])",
                    typeof(EmptyEnumerable<>),
                    typeof(EmptyOrderedEnumerable<>),
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupEnumerable<,>)
                );
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
                Assert.IsFalse(empty.Select(x => x).Any());
                Assert.IsFalse(empty.Select((x, ix) => x).Any());
            }

            // emptyOrdered
            {
                Assert.IsFalse(emptyOrdered.Select(x => x).Any());
                Assert.IsFalse(emptyOrdered.Select((x, ix) => x).Any());
            }

            // groupByDefault
            {
                Assert.IsTrue(groupByDefault.Select(x => x).SequenceEqual(groupByDefault, new _GroupingComparer<int>()));
                Assert.IsTrue(groupByDefault.Select((x, ix) => x).SequenceEqual(groupByDefault, new _GroupingComparer<int>()));
            }

            // groupBySpecific
            {
                Assert.IsTrue(groupBySpecific.Select(x => x).SequenceEqual(groupBySpecific, new _GroupingComparer<string>()));
                Assert.IsTrue(groupBySpecific.Select((x, ix) => x).SequenceEqual(groupBySpecific, new _GroupingComparer<string>()));
            }

            // lookup
            {
                Assert.IsTrue(lookup.Select(x => x).SequenceEqual(groupByDefault, new _GroupingComparer<int>()));
                Assert.IsTrue(lookup.Select((x, ix) => x).SequenceEqual(groupByDefault, new _GroupingComparer<int>()));
            }

            // range
            {
                Assert.IsTrue(range.Select(x => x).SequenceEqual(range));
                Assert.IsTrue(range.Select((x, ix) => x).SequenceEqual(range));
            }

            // repeat
            {
                Assert.IsTrue(repeat.Select(x => x).SequenceEqual(repeat));
                Assert.IsTrue(repeat.Select((x, ix) => x).SequenceEqual(repeat));
            }

            // reverseRange
            {
                Assert.IsTrue(reverseRange.Select(x => x).SequenceEqual(reverseRange));
                Assert.IsTrue(reverseRange.Select((x, ix) => x).SequenceEqual(reverseRange));
            }

            // oneItemDefault
            {
                Assert.IsTrue(oneItemDefault.Select(x => x).SequenceEqual(oneItemDefault));
                Assert.IsTrue(oneItemDefault.Select((x, ix) => x).SequenceEqual(oneItemDefault));
            }

            // oneItemSpecific
            {
                Assert.IsTrue(oneItemSpecific.Select(x => x).SequenceEqual(oneItemSpecific));
                Assert.IsTrue(oneItemSpecific.Select((x, ix) => x).SequenceEqual(oneItemSpecific));
            }

            // oneItemDefaultOrdered
            {
                Assert.IsTrue(oneItemDefaultOrdered.Select(x => x).SequenceEqual(oneItemDefaultOrdered));
                Assert.IsTrue(oneItemDefaultOrdered.Select((x, ix) => x).SequenceEqual(oneItemDefaultOrdered));
            }

            // oneItemSpecificOrdered
            {
                Assert.IsTrue(oneItemSpecificOrdered.Select(x => x).SequenceEqual(oneItemSpecificOrdered));
                Assert.IsTrue(oneItemSpecificOrdered.Select((x, ix) => x).SequenceEqual(oneItemSpecificOrdered));
            }
        }

        [TestMethod]
        public void Errors()
        {
            // default
            {
                Helper.ForEachEnumerableNoRetExpression(
                    new[] { 1, 2, 3 },
                    @"a => { try { a.Select(default(Func<int, double>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""selector"", exc.ParamName); } }",
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupEnumerable<,>)
                );
            }

            // indexed
            {
                Helper.ForEachEnumerableNoRetExpression(
                    new[] { 1, 2, 3 },
                    @"a => { try { a.Select(default(Func<int, int, double>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual(""selector"", exc.ParamName); } }",
                    typeof(GroupByDefaultEnumerable<,,,,>),
                    typeof(GroupBySpecificEnumerable<,,,,>),
                    typeof(LookupEnumerable<,>)
                );
            }
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
                try { empty.Select(default(Func<int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { empty.Select(default(Func<int, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
            }

            // emptyOrdered
            {
                try { emptyOrdered.Select(default(Func<int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { emptyOrdered.Select(default(Func<int, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
            }

            // groupByDefault
            {
                try { groupByDefault.Select(default(Func<GroupingEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { groupByDefault.Select(default(Func<GroupingEnumerable<int, int>, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
            }

            // groupBySpecific
            {
                try { groupBySpecific.Select(default(Func<GroupingEnumerable<string, string>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { groupBySpecific.Select(default(Func<GroupingEnumerable<string, string>, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
            }

            // lookup
            {
                try { lookup.Select(default(Func<GroupingEnumerable<int, int>, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { lookup.Select(default(Func<GroupingEnumerable<int, int>, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
            }

            // range
            {
                try { range.Select(default(Func<int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { range.Select(default(Func<int, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
            }

            // repeat
            {
                try { repeat.Select(default(Func<string, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { repeat.Select(default(Func<string, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
            }

            // reverseRange
            {
                try { reverseRange.Select(default(Func<int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { reverseRange.Select(default(Func<int, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
            }

            // oneItemDefault
            {
                try { oneItemDefault.Select(default(Func<int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemDefault.Select(default(Func<int, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
            }

            // oneItemSpecific
            {
                try { oneItemSpecific.Select(default(Func<int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemSpecific.Select(default(Func<int, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
            }

            // oneItemDefaultOrdered
            {
                try { oneItemDefaultOrdered.Select(default(Func<int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemDefaultOrdered.Select(default(Func<int, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
            }

            // oneItemSpecificOrdered
            {
                try { oneItemSpecificOrdered.Select(default(Func<int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
                try { oneItemSpecificOrdered.Select(default(Func<int, int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("selector", exc.ParamName); }
            }
        }

        [TestMethod]
        public void Malformed()
        {
            Helper.ForEachMalformedEnumerableExpression<string>(
                @"a =>
                  {
                    try
                    {
                        var _ = a.Select(x => x);
                        Assert.Fail();
                    }
                    catch(ArgumentException exc)
                    {
                        Assert.AreEqual(""source"", exc.ParamName);
                    }
                  }",
                  typeof(EmptyEnumerable<>),
                  typeof(EmptyOrderedEnumerable<>)
            );

            Helper.ForEachMalformedEnumerableExpression<string>(
                @"a =>
                  {
                    try
                    {
                        var _ = a.Select((x, ix) => x);
                        Assert.Fail();
                    }
                    catch(ArgumentException exc)
                    {
                        Assert.AreEqual(""source"", exc.ParamName);
                    }
                  }",
                  typeof(EmptyEnumerable<>),
                  typeof(EmptyOrderedEnumerable<>)
            );
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
                try { empty.Select(x => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { empty.Select((x, ix) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // emptyOrdered
            {
                try { emptyOrdered.Select(x => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { emptyOrdered.Select((x, ix) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // groupByDefault
            {
                try { groupByDefault.Select(x => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupByDefault.Select((x, ix) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // groupBySpecific
            {
                try { groupBySpecific.Select(x => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupBySpecific.Select((x, ix) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // lookup
            {
                try { lookup.Select(x => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookup.Select((x, ix) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // range
            {
                try { range.Select(x => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { range.Select((x, ix) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // repeat
            {
                try { repeat.Select(x => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { repeat.Select((x, ix) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // reverseRange
            {
                try { reverseRange.Select(x => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { reverseRange.Select((x, ix) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // oneItemDefault
            {
                try { oneItemDefault.Select(x => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefault.Select((x, ix) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // oneItemSpecific
            {
                try { oneItemSpecific.Select(x => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecific.Select((x, ix) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // oneItemDefaultOrdered
            {
                try { oneItemDefaultOrdered.Select(x => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefaultOrdered.Select((x, ix) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // oneItemSpecificOrdered
            {
                try { oneItemSpecificOrdered.Select(x => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecificOrdered.Select((x, ix) => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }
        }

        [TestMethod]
        public void Simple()
        {
            var foo = new[] { 1, 2, 3, 4, 5 };
            var asSelect = foo.Select(i => i * 2);

            Assert.IsTrue(asSelect.GetType().IsValueType);

            var ret = new List<int>();
            foreach (var item in asSelect)
            {
                ret.Add(item);
            }

            Assert.AreEqual(5, ret.Count);
            Assert.AreEqual(2, ret[0]);
            Assert.AreEqual(4, ret[1]);
            Assert.AreEqual(6, ret[2]);
            Assert.AreEqual(8, ret[3]);
            Assert.AreEqual(10, ret[4]);
        }

        [TestMethod]
        public void Empty()
        {
            var foo = System.Linq.Enumerable.Empty<int>();
            var asSelect = foo.Select(i => i * 2);

            Assert.IsTrue(asSelect.GetType().IsValueType);

            var e = asSelect.GetEnumerator();
            Assert.IsFalse(e.MoveNext());
        }

        static IEnumerable<int> _Infinite()
        {
            var prev = 0;

            while (true)
            {
                yield return prev;
                prev++;
            }
        }

        [TestMethod]
        public void Infinite()
        {
            var foo = _Infinite();
            var asSelect = foo.Select(i => i * 2);

            Assert.IsTrue(asSelect.GetType().IsValueType);

            var ret = new List<int>();

            foreach (var item in asSelect)
            {
                ret.Add(item);
                if (ret.Count == 1000) break;
            }

            Assert.AreEqual(1000, ret.Count);
            Assert.IsTrue(System.Linq.Enumerable.All(ret, i => i >= 0 && i <= 2000));
            Assert.IsTrue(System.Linq.Enumerable.All(ret, i => i / 2 * 2 == i));
            Assert.AreEqual(1000, System.Linq.Enumerable.Count(System.Linq.Enumerable.Distinct(ret)));
        }
        
        [TestMethod]
        public void Indexed()
        {
            var foo = new[] { 2, 4, 8, 16 };
            var asSelect = foo.Select((_, i) => i);

            Assert.IsTrue(asSelect.GetType().IsValueType);

            var ret = new List<int>();
            foreach (var item in asSelect)
            {
                ret.Add(item);
            }

            Assert.AreEqual(4, ret.Count);
            Assert.AreEqual(0, ret[0]);
            Assert.AreEqual(1, ret[1]);
            Assert.AreEqual(2, ret[2]);
            Assert.AreEqual(3, ret[3]);
        }
    }
}
