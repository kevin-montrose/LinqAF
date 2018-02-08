using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using TestHelpers;

namespace LinqAF.Tests
{
    [TestClass]
    public class CastTests
    {
        [TestMethod]
        public void InstanceExtensionNoOverlap()
        {
            Dictionary<MethodInfo, List<MethodInfo>> instOverlaps, extOverlaps;
            Helper.GetOverlappingMethods(typeof(Impl.ICast<,,>), out instOverlaps, out extOverlaps);

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
                typeof(LookupDefaultEnumerable<,>),
                typeof(LookupSpecificEnumerable<,>)
            );
        }

        class _IntComparer : IEqualityComparer<object>
        {
            public new bool Equals(object x, object y) => (int)x == (int)y;

            public int GetHashCode(object obj) => (int)obj;
        }

        [TestMethod]
        public void Chaining_Weird()
        {
            var empty = Enumerable.Empty<object>();
            var emptyOrdered = empty.OrderBy(x => x);
            var groupByDefault = new[] { 1, 1, 2, 2, 3, 3 }.GroupBy(x => x);
            var groupBySpecific = new[] { "hello", "HELLO", "world", "WORLD", "foo", "FOO" }.GroupBy(x => x, StringComparer.OrdinalIgnoreCase);
            var lookupDefault = new object[] { 1, 1, 2, 2, 3, 3 }.ToLookup(x => x);
            var lookupSpecific = new object[] { 1, 1, 2, 2, 3, 3 }.ToLookup(x => x, new _IntComparer());
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
                lookupDefault.Cast<string>().ToList();
                Assert.Fail();
            }
            catch (InvalidCastException) { }

            try
            {
                lookupSpecific.Cast<string>().ToList();
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
            var lookupSpecific = new LookupDefaultEnumerable<int, int>();
            var range = new RangeEnumerable();
            var repeat = new RepeatEnumerable<int>();
            var reverseRange = new ReverseRangeEnumerable();
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
                lookupDefault.Cast<object>();
                Assert.Fail();
            }
            catch (ArgumentException exc)
            {
                Assert.AreEqual("source", exc.ParamName);
            }

            try
            {
                lookupSpecific.Cast<object>();
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
