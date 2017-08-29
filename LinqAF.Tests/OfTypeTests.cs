using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestHelpers;

namespace LinqAF.Tests
{
    [TestClass]
    public class OfTypeTests
    {
        [TestMethod]
        public void Universal()
        {
            var enums = Helper.AllEnumerables();

            foreach (var e in enums)
            {
                System.Collections.Generic.List<string> missing;
                if (!Helper.Implements(e, typeof(LinqAF.Impl.IOfType<,,>), out missing))
                {
                    Assert.Fail($"{e.Name} does not implement IOfType ({string.Join(", ", missing)})");
                }
            }
        }

        [TestMethod]
        public void Chaining()
        {
            Helper.ForEachEnumerableExpression(
                new object[0],
                new object[] { "foo", 1, "bar", 1.2 },
                res =>
                {
                    Assert.AreEqual(1, res.Count);
                    Assert.AreEqual(1.2, res[0]);
                },
                "(_, a) => a.OfType<double>()",
                typeof(EmptyEnumerable<>),
                typeof(EmptyOrderedEnumerable<>),
                typeof(LookupDefaultEnumerable<,>),
                typeof(LookupSpecificEnumerable<,>),
                typeof(GroupByDefaultEnumerable<,,,,>),
                typeof(GroupBySpecificEnumerable<,,,,>)
            );
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

            Assert.AreEqual(0, empty.OfType<object>().Count());
            Assert.AreEqual(0, emptyOrdered.OfType<object>().Count());
            Assert.AreEqual(0, groupByDefault.OfType<string>().Count());
            Assert.AreEqual(0, groupBySpecific.OfType<string>().Count());
            Assert.AreEqual(0, lookupDefault.OfType<string>().Count());
            Assert.AreEqual(0, lookupSpecific.OfType<string>().Count());
            Assert.AreEqual(0, range.OfType<string>().Count());
            Assert.AreEqual(0, repeat.OfType<int>().Count());
            Assert.AreEqual(0, reverseRange.OfType<string>().Count());
            Assert.AreEqual(0, oneItemDefault.OfType<string>().Count());
            Assert.AreEqual(0, oneItemSpecific.OfType<string>().Count());
            Assert.AreEqual(0, oneItemDefaultOrdered.OfType<string>().Count());
            Assert.AreEqual(0, oneItemSpecificOrdered.OfType<string>().Count());
        }

        [TestMethod]
        public void Malformed()
        {
            Helper.ForEachMalformedEnumerableExpression<object>(
                @"a =>
                  {
                    try
                    {
                        a.OfType<string>();
                        Assert.Fail();
                    }
                    catch(ArgumentException exc)
                    {
                        Assert.AreEqual(""source"", exc.ParamName);
                    }
                  }",
                typeof(LookupDefaultEnumerable<,>),
                typeof(LookupSpecificEnumerable<,>),
                typeof(GroupByDefaultEnumerable<,,,,>),
                typeof(GroupBySpecificEnumerable<,,,,>)
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

            try { empty.OfType<object>(); Assert.Fail(); }catch(ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            try { emptyOrdered.OfType<object>(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            try { groupByDefault.OfType<object>(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            try { groupBySpecific.OfType<object>(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            try { lookupDefault.OfType<object>(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            try { lookupSpecific.OfType<object>(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            try { range.OfType<object>(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            try { repeat.OfType<object>(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            try { reverseRange.OfType<object>(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            try { oneItemDefault.OfType<object>(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            try { oneItemSpecific.OfType<object>(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            try { oneItemDefaultOrdered.OfType<object>(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            try { oneItemSpecificOrdered.OfType<object>(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
        }

        [TestMethod]
        public void Simple()
        {
            var e = new object[] { 1, "foo", 2 };
            var asOfType = e.OfType<int>();

            Assert.IsTrue(asOfType.GetType().IsValueType);

            var res = new List<int>();
            foreach(var item in asOfType)
            {
                res.Add(item);
            }

            Assert.AreEqual(2, res.Count);
            Assert.AreEqual(1, res[0]);
            Assert.AreEqual(2, res[1]);
        }

        [TestMethod]
        public void Empty()
        {
            var e = new object[0];
            var asOfType = e.OfType<string>();

            Assert.IsTrue(asOfType.GetType().IsValueType);

            var res = new List<string>();
            foreach(var item in asOfType)
            {
                res.Add(item);
            }

            Assert.AreEqual(0, res.Count);
        }
    }
}
