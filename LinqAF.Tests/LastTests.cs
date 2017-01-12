using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TestHelpers;

namespace LinqAF.Tests
{
    [TestClass]
    public class LastTests
    {
        [TestMethod]
        public void Universal()
        {
            var enums = Helper.AllEnumerables();

            foreach (var e in enums)
            {
                System.Collections.Generic.List<string> missing;
                if (!Helper.Implements(e, typeof(LinqAF.Impl.ILast<>), out missing))
                {
                    Assert.Fail($"{e.Name} does not implement ILast ({string.Join(", ", missing)})");
                }
            }
        }

        [TestMethod]
        public void Chaining()
        {
            foreach (var e in Helper.GetEnumerables(new[] { 2, 4, 6 }))
            {
                Assert.AreEqual(6, e.Last());

                Func<int, bool> f = i => i < 5;
                Assert.AreEqual(4, e.Last(f));

                Assert.AreEqual(6, e.LastOrDefault());
                Assert.AreEqual(4, e.LastOrDefault(f));
            }

            foreach (var e in Helper.GetEnumerables(new int[0]))
            {
                Assert.AreEqual(0, e.LastOrDefault());

                Func<int, bool> f = i => i < 5;
                Assert.AreEqual(0, e.LastOrDefault(f));
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
                try { empty.Last(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Sequence was empty", exc.Message); }
                try { empty.Last(x => true); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Sequence was empty", exc.Message); }
                Assert.AreEqual(0, empty.LastOrDefault());
                Assert.AreEqual(0, empty.LastOrDefault(x => true));
            }

            // emptyOrdered
            {
                try { emptyOrdered.Last(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Sequence was empty", exc.Message); }
                try { emptyOrdered.Last(x => true); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Sequence was empty", exc.Message); }
                Assert.AreEqual(0, emptyOrdered.LastOrDefault());
                Assert.AreEqual(0, emptyOrdered.LastOrDefault(x => true));
            }

            // groupByDefault
            {
                Assert.AreEqual(3, groupByDefault.Last().Key);
                Assert.AreEqual(3, groupByDefault.Last(x => true).Key);
                Assert.AreEqual(3, groupByDefault.LastOrDefault().Key);
                Assert.AreEqual(3, groupByDefault.LastOrDefault(x => true).Key);
            }

            // groupBySpecific
            {
                Assert.AreEqual("foo", groupBySpecific.Last().Key);
                Assert.AreEqual("foo", groupBySpecific.Last(x => true).Key);
                Assert.AreEqual("foo", groupBySpecific.LastOrDefault().Key);
                Assert.AreEqual("foo", groupBySpecific.LastOrDefault(x => true).Key);
            }

            // lookup
            {
                Assert.AreEqual(3, lookup.Last().Key);
                Assert.AreEqual(3, lookup.Last(x => true).Key);
                Assert.AreEqual(3, lookup.LastOrDefault().Key);
                Assert.AreEqual(3, lookup.LastOrDefault(x => true).Key);
            }

            // range
            {
                Assert.AreEqual(5, range.Last());
                Assert.AreEqual(5, range.Last(x => true));
                Assert.AreEqual(5, range.LastOrDefault());
                Assert.AreEqual(5, range.LastOrDefault(x => true));
            }

            // repeat
            {
                Assert.AreEqual("foo", repeat.First());
                Assert.AreEqual("foo", repeat.First(x => true));
                Assert.AreEqual("foo", repeat.FirstOrDefault());
                Assert.AreEqual("foo", repeat.FirstOrDefault(x => true));
            }

            // reverseRange
            {
                Assert.AreEqual(1, reverseRange.Last());
                Assert.AreEqual(1, reverseRange.Last(x => true));
                Assert.AreEqual(1, reverseRange.LastOrDefault());
                Assert.AreEqual(1, reverseRange.LastOrDefault(x => true));
            }

            // oneItemDefault
            {
                Assert.AreEqual(0, oneItemDefault.Last());
                Assert.AreEqual(0, oneItemDefault.Last(x => true));
                Assert.AreEqual(0, oneItemDefault.LastOrDefault());
                Assert.AreEqual(0, oneItemDefault.LastOrDefault(x => true));
            }

            // oneItemSpecific
            {
                Assert.AreEqual(4, oneItemSpecific.Last());
                Assert.AreEqual(4, oneItemSpecific.Last(x => true));
                Assert.AreEqual(4, oneItemSpecific.LastOrDefault());
                Assert.AreEqual(4, oneItemSpecific.LastOrDefault(x => true));
            }

            // oneItemDefaultOrdered
            {
                Assert.AreEqual(0, oneItemDefaultOrdered.Last());
                Assert.AreEqual(0, oneItemDefaultOrdered.Last(x => true));
                Assert.AreEqual(0, oneItemDefaultOrdered.LastOrDefault());
                Assert.AreEqual(0, oneItemDefaultOrdered.LastOrDefault(x => true));
            }

            // oneItemSpecificOrdered
            {
                Assert.AreEqual(4, oneItemSpecificOrdered.Last());
                Assert.AreEqual(4, oneItemSpecificOrdered.Last(x => true));
                Assert.AreEqual(4, oneItemSpecificOrdered.LastOrDefault());
                Assert.AreEqual(4, oneItemSpecificOrdered.LastOrDefault(x => true));
            }
        }

        [TestMethod]
        public void Chaining_Dictionary()
        {
            var dict = new System.Collections.Generic.Dictionary<int, int> { [1] = 2, [3] = 4 };
            var sortedDict = new System.Collections.Generic.SortedDictionary<int, int> { [1] = 2, [3] = 4 };

            System.Collections.Generic.KeyValuePair<int, int> dictKvp = default(System.Collections.Generic.KeyValuePair<int, int>);
            foreach (var kv in dict)
            {
                dictKvp = kv;
            }

            System.Collections.Generic.KeyValuePair<int, int> sortedDictKvp = default(System.Collections.Generic.KeyValuePair<int, int>);
            foreach (var kv in sortedDict)
            {
                sortedDictKvp = kv;
            }

            Assert.AreEqual(dictKvp, dict.Last());
            Assert.AreEqual(dictKvp, dict.Last(x => true));
            Assert.AreEqual(dictKvp, dict.LastOrDefault());
            Assert.AreEqual(dictKvp, dict.LastOrDefault(x => true));
            Assert.AreEqual(sortedDictKvp, sortedDict.Last());
            Assert.AreEqual(sortedDictKvp, sortedDict.Last(x => true));
            Assert.AreEqual(sortedDictKvp, sortedDict.LastOrDefault());
            Assert.AreEqual(sortedDictKvp, sortedDict.LastOrDefault(x => true));
        }

        [TestMethod]
        public void Errors()
        {
            foreach (var e in Helper.GetEnumerables(new object[] { new object() }))
            {
                Func<object, bool> d1 = null;
                try
                {
                    e.Last(d1);
                    Assert.Fail();
                }
                catch (ArgumentNullException exc)
                {
                    Assert.AreEqual("predicate", exc.ParamName);
                }

                try
                {
                    e.LastOrDefault(d1);
                    Assert.Fail();
                }
                catch (ArgumentNullException exc)
                {
                    Assert.AreEqual("predicate", exc.ParamName);
                }
            }

            foreach (var e in Helper.GetEnumerables(new int[0]))
            {
                var eGenType = Helper.GetGenericTypeDefinition(((object)e).GetType());
                if (eGenType == typeof(DefaultIfEmptySpecificEnumerable<,,>) || eGenType == typeof(DefaultIfEmptyDefaultEnumerable<,,>))
                {
                    continue;
                }

                Func<int, bool> d1 = _ => true;
                try
                {
                    e.Last(d1);
                    Assert.Fail();
                }
                catch (InvalidOperationException exc)
                {
                    Assert.AreEqual("No items matched predicate", exc.Message);
                }

                try
                {
                    e.Last();
                    Assert.Fail();
                }
                catch (InvalidOperationException exc)
                {
                    Assert.AreEqual("Sequence was empty", exc.Message);
                }
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
                try { empty.Last(default(Func<int, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
                try { empty.LastOrDefault(default(Func<int, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
            }

            // emptyOrdered
            {
                try { emptyOrdered.Last(default(Func<int, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
                try { emptyOrdered.LastOrDefault(default(Func<int, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
            }

            // groupByDefault
            {
                try { groupByDefault.Last(default(Func<GroupingEnumerable<int, int>, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
                try { groupByDefault.LastOrDefault(default(Func<GroupingEnumerable<int, int>, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
            }

            // groupBySpecific
            {
                try { groupBySpecific.Last(default(Func<GroupingEnumerable<string, string>, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
                try { groupBySpecific.LastOrDefault(default(Func<GroupingEnumerable<string, string>, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
            }

            // lookup
            {
                try { lookup.Last(default(Func<GroupingEnumerable<int, int>, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
                try { lookup.LastOrDefault(default(Func<GroupingEnumerable<int, int>, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
            }

            // range
            {
                try { range.Last(default(Func<int, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
                try { range.LastOrDefault(default(Func<int, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
            }

            // repeat
            {
                try { repeat.Last(default(Func<string, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
                try { repeat.LastOrDefault(default(Func<string, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
            }

            // reverseRange
            {
                try { reverseRange.Last(default(Func<int, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
                try { reverseRange.LastOrDefault(default(Func<int, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
            }

            // oneItemDefault
            {
                try { oneItemDefault.Last(default(Func<int, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
                try { oneItemDefault.LastOrDefault(default(Func<int, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
            }

            // oneItemSpecific
            {
                try { oneItemSpecific.Last(default(Func<int, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
                try { oneItemSpecific.LastOrDefault(default(Func<int, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
            }

            // oneItemDefaultOrdered
            {
                try { oneItemDefaultOrdered.Last(default(Func<int, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
                try { oneItemDefaultOrdered.LastOrDefault(default(Func<int, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
            }

            // oneItemSpecificOrdered
            {
                try { oneItemSpecificOrdered.Last(default(Func<int, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
                try { oneItemSpecificOrdered.LastOrDefault(default(Func<int, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
            }
        }

        [TestMethod]
        public void Errors_Dictionary()
        {
            var dict = new System.Collections.Generic.Dictionary<int, int> { [1] = 2, [3] = 4 };
            var sortedDict = new System.Collections.Generic.SortedDictionary<int, int> { [1] = 2, [3] = 4 };

            try { dict.Last(null); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
            try { dict.LastOrDefault(null); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
            try { sortedDict.Last(null); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
            try { sortedDict.LastOrDefault(null); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
        }

        [TestMethod]
        public void Malformed()
        {
            foreach (var e in Helper.GetMalformedEnumerables<int>())
            {
                Func<int, bool> nope = x => x == 0;

                try
                {
                    e.Last(nope);
                    Assert.Fail();
                }
                catch (ArgumentException exc)
                {
                    Assert.AreEqual("source", exc.ParamName);
                }

                try
                {
                    e.Last();
                    Assert.Fail();
                }
                catch (ArgumentException exc)
                {
                    Assert.AreEqual("source", exc.ParamName);
                }

                try
                {
                    e.LastOrDefault(nope);
                    Assert.Fail();
                }
                catch (ArgumentException exc)
                {
                    Assert.AreEqual("source", exc.ParamName);
                }

                try
                {
                    e.LastOrDefault();
                    Assert.Fail();
                }
                catch (ArgumentException exc)
                {
                    Assert.AreEqual("source", exc.ParamName);
                }
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
                try { empty.Last(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { empty.Last(x => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { empty.LastOrDefault(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { empty.LastOrDefault(x => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // emptyOrdered
            {
                try { emptyOrdered.Last(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { emptyOrdered.Last(x => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { emptyOrdered.LastOrDefault(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { emptyOrdered.LastOrDefault(x => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // groupByDefault
            {
                try { groupByDefault.Last(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupByDefault.Last(x => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupByDefault.LastOrDefault(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupByDefault.LastOrDefault(x => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // groupBySpecific
            {
                try { groupBySpecific.Last(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupBySpecific.Last(x => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupBySpecific.LastOrDefault(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupBySpecific.LastOrDefault(x => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // lookup
            {
                try { lookup.Last(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookup.Last(x => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookup.LastOrDefault(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookup.LastOrDefault(x => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // range
            {
                try { range.Last(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { range.Last(x => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { range.LastOrDefault(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { range.LastOrDefault(x => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // repeat
            {
                try { repeat.Last(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { repeat.Last(x => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { repeat.LastOrDefault(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { repeat.LastOrDefault(x => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // reverseRange
            {
                try { reverseRange.Last(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { reverseRange.Last(x => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { reverseRange.LastOrDefault(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { reverseRange.LastOrDefault(x => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // oneItemDefault
            {
                try { oneItemDefault.Last(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefault.Last(x => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefault.LastOrDefault(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefault.LastOrDefault(x => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // oneItemSpecific
            {
                try { oneItemSpecific.Last(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecific.Last(x => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecific.LastOrDefault(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecific.LastOrDefault(x => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // oneItemDefaultOrdered
            {
                try { oneItemDefaultOrdered.Last(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefaultOrdered.Last(x => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefaultOrdered.LastOrDefault(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefaultOrdered.LastOrDefault(x => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // oneItemSpecificOrdered
            {
                try { oneItemSpecificOrdered.Last(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecificOrdered.Last(x => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecificOrdered.LastOrDefault(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecificOrdered.LastOrDefault(x => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }
        }

        [TestMethod]
        public void Simple()
        {
            var e = Enumerable.Range(5, 5);
            var ix = e.Last(i => i % 2 == 0);

            Assert.AreEqual(8, ix);
        }

        [TestMethod]
        public void Empty()
        {
            var e = new int[0];

            try
            {
                var ix = e.Last(i => 5 % 2 == 0);
                Assert.Fail("Shouldn't be possible");
            }
            catch (InvalidOperationException x)
            {
                Assert.AreEqual("No items matched predicate", x.Message);
            }
        }

        [TestMethod]
        public void Default()
        {
            var e = Enumerable.Range(0, 5);
            var a = e.LastOrDefault(ix => ix == 4);
            var b = e.LastOrDefault(ix => ix == 10);

            Assert.AreEqual(4, a);
            Assert.AreEqual(default(int), b);
        }
    }
}
