using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TestHelpers;

namespace LinqAF.Tests
{
    [TestClass]
    public class FirstTests
    {        
        [TestMethod]
        public void Universal()
        {
            var enums = Helper.AllEnumerables();

            foreach (var e in enums)
            {
                System.Collections.Generic.List<string> missing;
                if (!Helper.Implements(e, typeof(LinqAF.Impl.IFirst<>), out missing))
                {
                    Assert.Fail($"{e.Name} does not implement IFirst ({string.Join(", ", missing)})");
                }
            }
        }

        [TestMethod]
        public void Chaining()
        {
            foreach (var e in Helper.GetEnumerables(new[] { 2, 4, 6 }))
            {
                Assert.AreEqual(2, e.First());
                Assert.AreEqual(2, e.FirstOrDefault());

                Func<int, bool> f = i => i > 3;
                Assert.AreEqual(4, e.First(f));
                Assert.AreEqual(4, e.FirstOrDefault(f));
            }

            foreach (var e in Helper.GetEnumerables(new int[0]))
            {
                Func<int, bool> f = i => i > 3;
                Assert.AreEqual(0, e.FirstOrDefault());
                Assert.AreEqual(0, e.FirstOrDefault(f));
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
                try { empty.First(); Assert.Fail(); }catch(InvalidOperationException exc) { Assert.AreEqual("Sequence was empty", exc.Message); }
                try { empty.First(x => true); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Sequence was empty", exc.Message); }
                Assert.AreEqual(0, empty.FirstOrDefault());
                Assert.AreEqual(0, empty.FirstOrDefault(x => true));
            }

            // emptyOrdered
            {
                try { emptyOrdered.First(); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Sequence was empty", exc.Message); }
                try { emptyOrdered.First(x => true); Assert.Fail(); } catch (InvalidOperationException exc) { Assert.AreEqual("Sequence was empty", exc.Message); }
                Assert.AreEqual(0, emptyOrdered.FirstOrDefault());
                Assert.AreEqual(0, emptyOrdered.FirstOrDefault(x => true));
            }

            // groupByDefault
            {
                Assert.AreEqual(1, groupByDefault.First().Key);
                Assert.AreEqual(1, groupByDefault.First(x => true).Key);
                Assert.AreEqual(1, groupByDefault.FirstOrDefault().Key);
                Assert.AreEqual(1, groupByDefault.FirstOrDefault(x => true).Key);
            }

            // groupBySpecific
            {
                Assert.AreEqual("hello", groupBySpecific.First().Key);
                Assert.AreEqual("hello", groupBySpecific.First(x => true).Key);
                Assert.AreEqual("hello", groupBySpecific.FirstOrDefault().Key);
                Assert.AreEqual("hello", groupBySpecific.FirstOrDefault(x => true).Key);
            }

            // lookup
            {
                Assert.AreEqual(1, lookup.First().Key);
                Assert.AreEqual(1, lookup.First(x => true).Key);
                Assert.AreEqual(1, lookup.FirstOrDefault().Key);
                Assert.AreEqual(1, lookup.FirstOrDefault(x => true).Key);
            }

            // range
            {
                Assert.AreEqual(1, range.First());
                Assert.AreEqual(1, range.First(x => true));
                Assert.AreEqual(1, range.FirstOrDefault());
                Assert.AreEqual(1, range.FirstOrDefault(x => true));
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
                Assert.AreEqual(5, reverseRange.First());
                Assert.AreEqual(5, reverseRange.First(x => true));
                Assert.AreEqual(5, reverseRange.FirstOrDefault());
                Assert.AreEqual(5, reverseRange.FirstOrDefault(x => true));
            }

            // oneItemDefault
            {
                Assert.AreEqual(0, oneItemDefault.First());
                Assert.AreEqual(0, oneItemDefault.First(x => true));
                Assert.AreEqual(0, oneItemDefault.FirstOrDefault());
                Assert.AreEqual(0, oneItemDefault.FirstOrDefault(x => true));
            }

            // oneItemSpecific
            {
                Assert.AreEqual(4, oneItemSpecific.First());
                Assert.AreEqual(4, oneItemSpecific.First(x => true));
                Assert.AreEqual(4, oneItemSpecific.FirstOrDefault());
                Assert.AreEqual(4, oneItemSpecific.FirstOrDefault(x => true));
            }

            // oneItemDefaultOrdered
            {
                Assert.AreEqual(0, oneItemDefaultOrdered.First());
                Assert.AreEqual(0, oneItemDefaultOrdered.First(x => true));
                Assert.AreEqual(0, oneItemDefaultOrdered.FirstOrDefault());
                Assert.AreEqual(0, oneItemDefaultOrdered.FirstOrDefault(x => true));
            }

            // oneItemSpecificOrdered
            {
                Assert.AreEqual(4, oneItemSpecificOrdered.First());
                Assert.AreEqual(4, oneItemSpecificOrdered.First(x => true));
                Assert.AreEqual(4, oneItemSpecificOrdered.FirstOrDefault());
                Assert.AreEqual(4, oneItemSpecificOrdered.FirstOrDefault(x => true));
            }
        }

        [TestMethod]
        public void Chaining_Dictionary()
        {
            var dict = new System.Collections.Generic.Dictionary<int, int> { [1] = 2, [3] = 4 };
            var sortedDict = new System.Collections.Generic.SortedDictionary<int, int> { [1] = 2, [3] = 4 };

            System.Collections.Generic.KeyValuePair<int, int> dictKvp = default(System.Collections.Generic.KeyValuePair<int, int>);
            foreach(var kv in dict)
            {
                dictKvp = kv;
                break;
            }

            System.Collections.Generic.KeyValuePair<int, int> sortedDictKvp = default(System.Collections.Generic.KeyValuePair<int, int>);
            foreach(var kv in sortedDict)
            {
                sortedDictKvp = kv;
                break;
            }

            Assert.AreEqual(dictKvp, dict.First());
            Assert.AreEqual(dictKvp, dict.First(x => true));
            Assert.AreEqual(dictKvp, dict.FirstOrDefault());
            Assert.AreEqual(dictKvp, dict.FirstOrDefault(x => true));
            Assert.AreEqual(sortedDictKvp, sortedDict.First());
            Assert.AreEqual(sortedDictKvp, sortedDict.First(x => true));
            Assert.AreEqual(sortedDictKvp, sortedDict.FirstOrDefault());
            Assert.AreEqual(sortedDictKvp, sortedDict.FirstOrDefault(x => true));
        }

        [TestMethod]
        public void Errors()
        {
            foreach (var e in Helper.GetEnumerables(new object[] { new object() }))
            {
                Func<object, bool> d1 = null;
                try
                {
                    e.First(d1);
                    Assert.Fail();
                }
                catch (ArgumentNullException exc)
                {
                    Assert.AreEqual("predicate", exc.ParamName);
                }

                try
                {
                    e.FirstOrDefault(d1);
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
                if(eGenType == typeof(DefaultIfEmptySpecificEnumerable<,,>) || eGenType == typeof(DefaultIfEmptyDefaultEnumerable<,,>))
                {
                    continue;
                }

                Func<int, bool> d1 = _ => true;
                try
                {
                    e.First(d1);
                    Assert.Fail();
                }
                catch (InvalidOperationException exc)
                {
                    Assert.AreEqual("No items matched predicate", exc.Message);
                }

                try
                {
                    e.First();
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
                try { empty.First(default(Func<int, bool>)); Assert.Fail(); }catch(ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
                try { empty.FirstOrDefault(default(Func<int, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
            }

            // emptyOrdered
            {
                try { emptyOrdered.First(default(Func<int, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
                try { emptyOrdered.FirstOrDefault(default(Func<int, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
            }

            // groupByDefault
            {
                try { groupByDefault.First(default(Func<GroupingEnumerable<int, int>, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
                try { groupByDefault.FirstOrDefault(default(Func<GroupingEnumerable<int, int>, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
            }

            // groupBySpecific
            {
                try { groupBySpecific.First(default(Func<GroupingEnumerable<string, string>, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
                try { groupBySpecific.FirstOrDefault(default(Func<GroupingEnumerable<string, string>, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
            }

            // lookup
            {
                try { lookup.First(default(Func<GroupingEnumerable<int, int>, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
                try { lookup.FirstOrDefault(default(Func<GroupingEnumerable<int, int>, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
            }

            // range
            {
                try { range.First(default(Func<int, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
                try { range.FirstOrDefault(default(Func<int, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
            }

            // repeat
            {
                try { repeat.First(default(Func<string, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
                try { repeat.FirstOrDefault(default(Func<string, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
            }

            // reverseRange
            {
                try { reverseRange.First(default(Func<int, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
                try { reverseRange.FirstOrDefault(default(Func<int, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
            }

            // oneItemDefault
            {
                try { oneItemDefault.First(default(Func<int, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
                try { oneItemDefault.FirstOrDefault(default(Func<int, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
            }

            // oneItemSpecific
            {
                try { oneItemSpecific.First(default(Func<int, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
                try { oneItemSpecific.FirstOrDefault(default(Func<int, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
            }

            // oneItemDefaultOrdered
            {
                try { oneItemDefaultOrdered.First(default(Func<int, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
                try { oneItemDefaultOrdered.FirstOrDefault(default(Func<int, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
            }

            // oneItemSpecificOrdered
            {
                try { oneItemSpecificOrdered.First(default(Func<int, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
                try { oneItemSpecificOrdered.FirstOrDefault(default(Func<int, bool>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
            }
        }

        [TestMethod]
        public void Errors_Dictionary()
        {
            var dict = new System.Collections.Generic.Dictionary<int, int> { [1] = 2, [3] = 4 };
            var sortedDict = new System.Collections.Generic.SortedDictionary<int, int> { [1] = 2, [3] = 4 };

            try { dict.First(null); Assert.Fail(); }catch(ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
            try { dict.FirstOrDefault(null); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
            try { sortedDict.First(null); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
            try { sortedDict.FirstOrDefault(null); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("predicate", exc.ParamName); }
        }

        [TestMethod]
        public void Malformed()
        {
            foreach (var e in Helper.GetMalformedEnumerables<int>())
            {
                Func<int, bool> nope = x => x == 0;

                try
                {
                    e.First(nope);
                    Assert.Fail();
                }
                catch (ArgumentException exc)
                {
                    Assert.AreEqual("source", exc.ParamName);
                }

                try
                {
                    e.First();
                    Assert.Fail();
                }
                catch (ArgumentException exc)
                {
                    Assert.AreEqual("source", exc.ParamName);
                }

                try
                {
                    e.FirstOrDefault(nope);
                    Assert.Fail();
                }
                catch (ArgumentException exc)
                {
                    Assert.AreEqual("source", exc.ParamName);
                }

                try
                {
                    e.FirstOrDefault();
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
                try { empty.First(); Assert.Fail(); }catch(ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { empty.First(x => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { empty.FirstOrDefault(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { empty.FirstOrDefault(x => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // emptyOrdered
            {
                try { emptyOrdered.First(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { emptyOrdered.First(x => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { emptyOrdered.FirstOrDefault(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { emptyOrdered.FirstOrDefault(x => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // groupByDefault
            {
                try { groupByDefault.First(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupByDefault.First(x => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupByDefault.FirstOrDefault(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupByDefault.FirstOrDefault(x => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // groupBySpecific
            {
                try { groupBySpecific.First(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupBySpecific.First(x => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupBySpecific.FirstOrDefault(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { groupBySpecific.FirstOrDefault(x => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // lookup
            {
                try { lookup.First(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookup.First(x => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookup.FirstOrDefault(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { lookup.FirstOrDefault(x => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // range
            {
                try { range.First(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { range.First(x => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { range.FirstOrDefault(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { range.FirstOrDefault(x => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // repeat
            {
                try { repeat.First(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { repeat.First(x => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { repeat.FirstOrDefault(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { repeat.FirstOrDefault(x => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // reverseRange
            {
                try { reverseRange.First(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { reverseRange.First(x => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { reverseRange.FirstOrDefault(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { reverseRange.FirstOrDefault(x => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // oneItemDefault
            {
                try { oneItemDefault.First(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefault.First(x => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefault.FirstOrDefault(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefault.FirstOrDefault(x => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // oneItemSpecific
            {
                try { oneItemSpecific.First(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecific.First(x => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecific.FirstOrDefault(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecific.FirstOrDefault(x => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // oneItemDefaultOrdered
            {
                try { oneItemDefaultOrdered.First(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefaultOrdered.First(x => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefaultOrdered.FirstOrDefault(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemDefaultOrdered.FirstOrDefault(x => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // oneItemSpecificOrdered
            {
                try { oneItemSpecificOrdered.First(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecificOrdered.First(x => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecificOrdered.FirstOrDefault(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { oneItemSpecificOrdered.FirstOrDefault(x => true); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }
        }

        [TestMethod]
        public void Simple()
        {
            var e = Enumerable.Range(5, 5);
            var ix = e.First(i => i % 2 == 0);

            Assert.AreEqual(6, ix);
        }

        [TestMethod]
        public void Empty()
        {
            var e = new int[0];

            try
            {
                var ix = e.First();
                Assert.Fail("Shouldn't be possible");
            }
            catch (InvalidOperationException x)
            {
                Assert.AreEqual("Sequence was empty", x.Message);
            }

            try
            {
                var ix = e.First(i => 5 % 2 == 0);
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
            var a = e.FirstOrDefault(ix => ix == 4);
            var b = e.FirstOrDefault(ix => ix == 10);

            Assert.AreEqual(4, a);
            Assert.AreEqual(default(int), b);
        }
    }
}
