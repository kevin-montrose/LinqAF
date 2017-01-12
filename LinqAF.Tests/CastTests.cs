using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using TestHelpers;

namespace LinqAF.Tests
{
    [TestClass]
    public class CastTests
    {
        [TestMethod]
        public void Universal()
        {
            var enums = Helper.AllEnumerables();

            foreach (var e in enums)
            {
                System.Collections.Generic.List<string> missing;
                if (!Helper.Implements(e, typeof(LinqAF.Impl.ICast<,,>), out missing))
                {
                    Assert.Fail($"{e.Name} does not implement ICast ({string.Join(", ", missing)})");
                }
            }
        }

        public class _ChainingBase { }
        public class _ChainingDerived: _ChainingBase
        {
            public string Value { get; private set; }
            public _ChainingDerived(string val)
            {
                Value = val;
            }
        }

        [TestMethod]
        public void Chaining()
        {
            var arr = new _ChainingBase[] { new _ChainingDerived("foo"), new _ChainingDerived("bar") };
            
            Helper.ForEachEnumerableExpression(
                new object[0],
                arr,
                res =>
                {
                    Assert.AreEqual(2, res.Count);
                    Assert.AreEqual("foo", res[0].Value);
                    Assert.AreEqual("bar", res[1].Value);
                },
                "(_, a) => a.Cast<CastTests._ChainingDerived>()",
                typeof(EmptyEnumerable<>),
                typeof(EmptyOrderedEnumerable<>),
                typeof(GroupByDefaultEnumerable<,,,,>),
                typeof(GroupBySpecificEnumerable<,,,,>),
                typeof(LookupEnumerable<,>)
            );
        }

        [TestMethod]
        public void Chaining_Weird()
        {
            var empty = Enumerable.Empty<object>();
            var emptyOrdered = empty.OrderBy(x => x);
            var groupByDefault = new[] { 1, 1, 2, 2, 3, 3 }.GroupBy(x => x);
            var groupBySpecific = new[] { "hello", "HELLO", "world", "WORLD", "foo", "FOO" }.GroupBy(x => x, StringComparer.OrdinalIgnoreCase);
            var lookup = new object[] { 1, 1, 2, 2, 3, 3 }.ToLookup(x => x);
            var range = Enumerable.Range(1, 5);
            var repeat = Enumerable.Repeat((object)"foo", 5);
            var reverseRange = Enumerable.Range(1, 5).Reverse();
            var oneItemDefault = Enumerable.Empty<object>().DefaultIfEmpty();
            var oneItemSpecific = Enumerable.Empty<object>().DefaultIfEmpty("foo");
            var oneItemDefaultOrdered = oneItemDefault.OrderBy(x => x);
            var oneItemSpecificOrdered = oneItemSpecific.OrderBy(x => x);

            Assert.IsTrue(empty.Cast<int>().SequenceEqual(new int[0]));
            Assert.IsTrue(emptyOrdered.Cast<int>().SequenceEqual(new int[0]));

            try
            {
                groupByDefault.Cast<string>().ToList();
                Assert.Fail();
            }
            catch (InvalidCastException) { }

            try
            {
                groupBySpecific.Cast<string>().ToList();
                Assert.Fail();
            }
            catch (InvalidCastException) { }

            try
            {
                lookup.Cast<string>().ToList();
                Assert.Fail();
            }
            catch (InvalidCastException) { }

            try
            {
                range.Cast<string>().ToList();
                Assert.Fail();
            }
            catch (InvalidCastException) { }

            Assert.IsTrue(repeat.Cast<string>().SequenceEqual(new[] { "foo", "foo", "foo", "foo", "foo" }));

            try
            {
                reverseRange.Cast<string>().ToList();
                Assert.Fail();
            }
            catch (InvalidCastException) { }

            Assert.IsTrue(oneItemDefault.Cast<string>().SequenceEqual(new string[] { null }));
            Assert.IsTrue(oneItemSpecific.Cast<string>().SequenceEqual(new string[] { "foo" }));
            Assert.IsTrue(oneItemDefaultOrdered.Cast<string>().SequenceEqual(new string[] { null }));
            Assert.IsTrue(oneItemSpecificOrdered.Cast<string>().SequenceEqual(new string[] { "foo" }));
        }

        [TestMethod]
        public void Malformed()
        {
            Helper.ForEachMalformedEnumerableExpression<object>(
                @"a => { try { a.Cast<string>(); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""source"", exc.ParamName); } }",
                typeof(GroupByDefaultEnumerable<,,,,>),
                typeof(GroupBySpecificEnumerable<,,,,>),
                typeof(LookupEnumerable<,>)
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

            try
            {
                empty.Cast<object>();
                Assert.Fail();
            }
            catch (ArgumentException exc)
            {
                Assert.AreEqual("source", exc.ParamName);
            }

            try
            {
                emptyOrdered.Cast<object>();
                Assert.Fail();
            }
            catch (ArgumentException exc)
            {
                Assert.AreEqual("source", exc.ParamName);
            }

            try
            {
                groupByDefault.Cast<object>();
                Assert.Fail();
            }
            catch (ArgumentException exc)
            {
                Assert.AreEqual("source", exc.ParamName);
            }

            try
            {
                groupBySpecific.Cast<object>();
                Assert.Fail();
            }
            catch (ArgumentException exc)
            {
                Assert.AreEqual("source", exc.ParamName);
            }

            try
            {
                lookup.Cast<object>();
                Assert.Fail();
            }
            catch (ArgumentException exc)
            {
                Assert.AreEqual("source", exc.ParamName);
            }

            try
            {
                range.Cast<object>();
                Assert.Fail();
            }
            catch (ArgumentException exc)
            {
                Assert.AreEqual("source", exc.ParamName);
            }

            try
            {
                repeat.Cast<object>();
                Assert.Fail();
            }
            catch (ArgumentException exc)
            {
                Assert.AreEqual("source", exc.ParamName);
            }

            try
            {
                reverseRange.Cast<object>();
                Assert.Fail();
            }
            catch (ArgumentException exc)
            {
                Assert.AreEqual("source", exc.ParamName);
            }

            try
            {
                oneItemDefault.Cast<object>();
                Assert.Fail();
            }
            catch (ArgumentException exc)
            {
                Assert.AreEqual("source", exc.ParamName);
            }

            try
            {
                oneItemSpecific.Cast<object>();
                Assert.Fail();
            }
            catch (ArgumentException exc)
            {
                Assert.AreEqual("source", exc.ParamName);
            }

            try
            {
                oneItemDefaultOrdered.Cast<object>();
                Assert.Fail();
            }
            catch (ArgumentException exc)
            {
                Assert.AreEqual("source", exc.ParamName);
            }

            try
            {
                oneItemSpecificOrdered.Cast<object>();
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
            var e = new object[] { 1, 2, 3 };
            var asCast = e.Cast<int>();

            Assert.IsTrue(asCast.GetType().IsValueType);

            var res = new List<int>();
            foreach(var item in asCast)
            {
                res.Add(item);
            }

            Assert.AreEqual(3, res.Count);
            Assert.AreEqual(1, res[0]);
            Assert.AreEqual(2, res[1]);
            Assert.AreEqual(3, res[2]);
        }

        [TestMethod]
        public void Empty()
        {
            var e = new object[0];
            var asCast = e.Cast<string>();

            Assert.IsTrue(asCast.GetType().IsValueType);

            var res = new List<string>();
            foreach(var item in asCast)
            {
                res.Add(item);
            }

            Assert.AreEqual(0, res.Count);
        }
    }
}
