using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TestHelpers;

namespace LinqAF.Tests
{
    [TestClass]
    public class AllTests
    {
        [TestMethod]
        public void Universal()
        {
            var enums = Helper.AllEnumerables();

            foreach (var e in enums)
            {
                System.Collections.Generic.List<string> missing;
                if (!Helper.Implements(e, typeof(Impl.IAll<>), out missing))
                {
                    Assert.Fail($"{e.Name} does not implement IAll ({string.Join(", ", missing)})");
                }
            }
        }

        [TestMethod]
        public void Chaining()
        {
            foreach(var e in Helper.GetEnumerables(new [] { 2, 4, 6 }))
            {
                Func<int, bool> tFunc = a => a % 2 == 0;
                Func<int, bool> fFunc = a => a < 5;
                Assert.IsTrue(e.All(tFunc));
                Assert.IsFalse(e.All(fFunc));
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

            Assert.IsTrue(empty.All(x => x == 0));
            Assert.IsTrue(emptyOrdered.All(x => x == 0));
            Assert.IsTrue(groupByDefault.All(grp => grp.Count() == 2));
            Assert.IsTrue(groupBySpecific.All(grp => grp.Count() == 2));
            Assert.IsTrue(lookup.All(grp => grp.Count() == 2));
            Assert.IsTrue(range.All(r => r >= 1));
            Assert.IsTrue(repeat.All(r => r == "foo"));
            Assert.IsTrue(reverseRange.All(r => r >= 1));
            Assert.IsTrue(oneItemDefault.All(r => r == 0));
            Assert.IsTrue(oneItemSpecific.All(r => r == 4));
            Assert.IsTrue(oneItemDefaultOrdered.All(r => r == 0));
            Assert.IsTrue(oneItemSpecificOrdered.All(r => r == 4));
        }

        [TestMethod]
        public void Chaining_Dictionary()
        {
            var dict = new System.Collections.Generic.Dictionary<int, int> { [1] = 2, [3] = 4 };
            var sortedDict = new System.Collections.Generic.SortedDictionary<int, int> { [1] = 2, [3] = 4 };

            Assert.IsTrue(dict.All(kv => kv.Key < 5));
            Assert.IsTrue(sortedDict.All(kv => kv.Key < 5));
        }

        [TestMethod]
        public void Errors()
        {
            foreach (var e in Helper.GetEnumerables(new object[] { new object() }))
            {
                Func<object, bool> d1 = null;
                try
                {
                    e.All(d1);
                    Assert.Fail();
                }
                catch (ArgumentNullException exc)
                {
                    Assert.AreEqual("predicate", exc.ParamName);
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

            try
            {
                empty.All(default(Func<int, bool>));
                Assert.Fail();
            }
            catch (ArgumentNullException exc)
            {
                Assert.AreEqual("predicate", exc.ParamName);
            }

            try
            {
                emptyOrdered.All(default(Func<int, bool>));
                Assert.Fail();
            }
            catch (ArgumentNullException exc)
            {
                Assert.AreEqual("predicate", exc.ParamName);
            }

            try
            {
                groupByDefault.All(default(Func<GroupingEnumerable<int, int>, bool>));
                Assert.Fail();
            }
            catch (ArgumentNullException exc)
            {
                Assert.AreEqual("predicate", exc.ParamName);
            }

            try
            {
                groupBySpecific.All(default(Func<GroupingEnumerable<string, string>, bool>));
                Assert.Fail();
            }
            catch (ArgumentNullException exc)
            {
                Assert.AreEqual("predicate", exc.ParamName);
            }

            try
            {
                lookup.All(default(Func<GroupingEnumerable<int, int>, bool>));
                Assert.Fail();
            }
            catch (ArgumentNullException exc)
            {
                Assert.AreEqual("predicate", exc.ParamName);
            }

            try
            {
                range.All(default(Func<int, bool>));
                Assert.Fail();
            }
            catch (ArgumentNullException exc)
            {
                Assert.AreEqual("predicate", exc.ParamName);
            }

            try
            {
                repeat.All(default(Func<string, bool>));
                Assert.Fail();
            }
            catch (ArgumentNullException exc)
            {
                Assert.AreEqual("predicate", exc.ParamName);
            }

            try
            {
                reverseRange.All(default(Func<int, bool>));
                Assert.Fail();
            }
            catch (ArgumentNullException exc)
            {
                Assert.AreEqual("predicate", exc.ParamName);
            }

            try
            {
                oneItemDefault.All(default(Func<int, bool>));
                Assert.Fail();
            }
            catch (ArgumentNullException exc)
            {
                Assert.AreEqual("predicate", exc.ParamName);
            }

            try
            {
                oneItemSpecific.All(default(Func<int, bool>));
                Assert.Fail();
            }
            catch (ArgumentNullException exc)
            {
                Assert.AreEqual("predicate", exc.ParamName);
            }

            try
            {
                oneItemDefaultOrdered.All(default(Func<int, bool>));
                Assert.Fail();
            }
            catch (ArgumentNullException exc)
            {
                Assert.AreEqual("predicate", exc.ParamName);
            }

            try
            {
                oneItemSpecificOrdered.All(default(Func<int, bool>));
                Assert.Fail();
            }
            catch (ArgumentNullException exc)
            {
                Assert.AreEqual("predicate", exc.ParamName);
            }
        }

        [TestMethod]
        public void Errors_Dictionary()
        {
            var dict = new System.Collections.Generic.Dictionary<int, int> { [1] = 2, [3] = 4 };
            var sortedDict = new System.Collections.Generic.SortedDictionary<int, int> { [1] = 2, [3] = 4 };

            try { dict.All(null); Assert.Fail(); }catch(ArgumentException exc) { Assert.AreEqual("predicate", exc.ParamName); }
            try { sortedDict.All(null); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("predicate", exc.ParamName); }
        }

        [TestMethod]
        public void Malformed()
        {
            foreach (var e in Helper.GetMalformedEnumerables<int>())
            {
                Func<int, bool> nope = x => x == 0;

                try
                {
                    e.All(nope);
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

            try
            {
                empty.All(x => true);
                Assert.Fail();
            }
            catch(ArgumentException exc)
            {
                Assert.AreEqual("source", exc.ParamName);
            }

            try
            {
                emptyOrdered.All(x => true);
                Assert.Fail();
            }
            catch (ArgumentException exc)
            {
                Assert.AreEqual("source", exc.ParamName);
            }

            try
            {
                groupByDefault.All(x => true);
                Assert.Fail();
            }
            catch (ArgumentException exc)
            {
                Assert.AreEqual("source", exc.ParamName);
            }

            try
            {
                groupBySpecific.All(x => true);
                Assert.Fail();
            }
            catch (ArgumentException exc)
            {
                Assert.AreEqual("source", exc.ParamName);
            }

            try
            {
                lookup.All(x => true);
                Assert.Fail();
            }
            catch (ArgumentException exc)
            {
                Assert.AreEqual("source", exc.ParamName);
            }

            try
            {
                range.All(x => true);
                Assert.Fail();
            }
            catch (ArgumentException exc)
            {
                Assert.AreEqual("source", exc.ParamName);
            }

            try
            {
                repeat.All(x => true);
                Assert.Fail();
            }
            catch (ArgumentException exc)
            {
                Assert.AreEqual("source", exc.ParamName);
            }

            try
            {
                reverseRange.All(x => true);
                Assert.Fail();
            }
            catch (ArgumentException exc)
            {
                Assert.AreEqual("source", exc.ParamName);
            }

            try
            {
                oneItemDefault.All(x => true);
                Assert.Fail();
            }
            catch (ArgumentException exc)
            {
                Assert.AreEqual("source", exc.ParamName);
            }

            try
            {
                oneItemSpecific.All(x => true);
                Assert.Fail();
            }
            catch (ArgumentException exc)
            {
                Assert.AreEqual("source", exc.ParamName);
            }

            try
            {
                oneItemDefaultOrdered.All(x => true);
                Assert.Fail();
            }
            catch (ArgumentException exc)
            {
                Assert.AreEqual("source", exc.ParamName);
            }

            try
            {
                oneItemSpecificOrdered.All(x => true);
                Assert.Fail();
            }
            catch (ArgumentException exc)
            {
                Assert.AreEqual("source", exc.ParamName);
            }
        }

        [TestMethod]
        public void Simple()
        {
            var e = new[] { 1, 2, 3 };

            Assert.IsTrue(e.Any());
        }

        [TestMethod]
        public void Filter()
        {
            var e = new[] { 1, 2, 3 };

            Assert.IsTrue(e.Any(i => i == 2));
            Assert.IsFalse(e.Any(i => i == 4));
        }

        [TestMethod]
        public void Empty()
        {
            var e = new int[0];

            Assert.IsFalse(e.Any());
            Assert.IsFalse(e.Any(i => i == 2));
        }
    }
}
