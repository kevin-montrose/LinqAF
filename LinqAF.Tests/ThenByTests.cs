using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Reflection;
using TestHelpers;

namespace LinqAF.Tests
{
    [TestClass]
    public class ThenByTests
    {
        [TestMethod]
        public void Universal()
        {
            var returnedTypes = new HashSet<Type>();
            foreach(var type in typeof(EmptyEnumerable<>).Assembly.GetTypes().Where(w => w.IsPublic))
            {
                foreach(var mtd in type.GetMethods(BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance).Where(m => m.Name == "OrderBy" || m.Name == "OrderByDescending"))
                {
                    var ret = mtd.ReturnType;
                    if(ret.IsGenericType && !ret.IsGenericTypeDefinition)
                    {
                        ret = ret.GetGenericTypeDefinition();
                    }

                    returnedTypes.Add(ret);
                }
            }

            foreach(var e in returnedTypes)
            {
                System.Collections.Generic.List<string> missing;
                if (!Helper.Implements(e, typeof(LinqAF.Impl.IThenBy<,,,,>), out missing))
                {
                    Assert.Fail($"{e.Name} does not implement IThenBy ({string.Join(", ", missing)})");
                }
            }
        }
        
        class Foo
        {
            public int Int { get; set; }
            public string String { get; set; }

            public Foo(int i, string s)
            {
                Int = i;
                String = s;
            }
        }

        [TestMethod]
        public void AscendingThenAscending()
        {
            var asOrderBy = new[] { new Foo(1, "wut"), new Foo(44, "wat"), new Foo(0, "fizz"), new Foo(0, "buzz") }.OrderBy(x => x.Int);
            var asThenBy = asOrderBy.ThenBy(x => x.String);

            Assert.IsTrue(asThenBy.GetType().IsValueType);

            var res = new List<Foo>();
            foreach(var item in asThenBy)
            {
                res.Add(item);
            }

            Assert.AreEqual(4, res.Count);
            Assert.AreEqual(0, res[0].Int);
            Assert.AreEqual("buzz", res[0].String);
            Assert.AreEqual(0, res[1].Int);
            Assert.AreEqual("fizz", res[1].String);
            Assert.AreEqual(1, res[2].Int);
            Assert.AreEqual("wut", res[2].String);
            Assert.AreEqual(44, res[3].Int);
            Assert.AreEqual("wat", res[3].String);
        }

        [TestMethod]
        public void AscendingThenDescending()
        {
            var asOrderBy = new[] { new Foo(1, "wut"), new Foo(44, "wat"), new Foo(0, "fizz"), new Foo(0, "buzz") }.OrderBy(x => x.Int);
            var asThenBy = asOrderBy.ThenByDescending(x => x.String);

            Assert.IsTrue(asThenBy.GetType().IsValueType);

            var res = new List<Foo>();
            foreach (var item in asThenBy)
            {
                res.Add(item);
            }

            Assert.AreEqual(4, res.Count);
            Assert.AreEqual(0, res[0].Int);
            Assert.AreEqual("fizz", res[0].String);
            Assert.AreEqual(0, res[1].Int);
            Assert.AreEqual("buzz", res[1].String);
            Assert.AreEqual(1, res[2].Int);
            Assert.AreEqual("wut", res[2].String);
            Assert.AreEqual(44, res[3].Int);
            Assert.AreEqual("wat", res[3].String);
        }

        [TestMethod]
        public void Errors()
        {
            var foo = new int[0].OrderBy(o => o);
            var bar = Enumerable.Empty<int>().OrderBy(o => o);

            // ascending, default
            {
                try { foo.ThenBy(default(Func<int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { bar.ThenBy(default(Func<int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
            }

            // descending, default
            {
                try { foo.ThenByDescending(default(Func<int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { bar.ThenByDescending(default(Func<int, int>)); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
            }

            // ascending, specific
            {
                try { foo.ThenBy(default(Func<int, string>), StringComparer.InvariantCulture); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { bar.ThenBy(default(Func<int, string>), StringComparer.InvariantCulture); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
            }

            // descending, specific
            {
                try { foo.ThenByDescending(default(Func<int, string>), StringComparer.InvariantCulture); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
                try { bar.ThenByDescending(default(Func<int, string>), StringComparer.InvariantCulture); Assert.Fail(); } catch (ArgumentNullException exc) { Assert.AreEqual("keySelector", exc.ParamName); }
            }
        }

        [TestMethod]
        public void Malformed()
        {
            var foo = new OrderByEnumerable<int, int, EmptyEnumerable<int>, EmptyEnumerator<int>, SingleComparerAscending<int, int>>();
            var bar = new EmptyOrderedEnumerable<int>();

            // ascending, default
            {
                try { foo.ThenBy(x => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { bar.ThenBy(x => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // descending, default
            {
                try { foo.ThenByDescending(x => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { bar.ThenByDescending(x => x); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // ascending, specific
            {
                try { foo.ThenBy(x => "", StringComparer.InvariantCultureIgnoreCase); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { bar.ThenBy(x => "", StringComparer.InvariantCultureIgnoreCase); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }

            // descending, specific
            {
                try { foo.ThenByDescending(x => "", StringComparer.InvariantCultureIgnoreCase); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
                try { bar.ThenByDescending(x => "", StringComparer.InvariantCultureIgnoreCase); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual("source", exc.ParamName); }
            }
        }

        class _CreateOrderedEnumerable : IComparer<int>
        {
            public int Compare(int x, int y) => x.CompareTo(y);
        }

        [TestMethod]
        public void CreateOrderedEnumerable()
        {
            var i =
                new[]
                {
                    new { First = 2, Second = 3, Third = 1},
                    new { First = 1, Second = 3, Third = 9},
                    new { First = 2, Second = 5, Third = 9}
                };
            
            var e1 = i.OrderBy(x => x.First);
            var e2 = e1.CreateOrderedEnumerable(y => y.Second, new _CreateOrderedEnumerable(), true);
            var e3 = e2.CreateOrderedEnumerable(z => z.Third, new _CreateOrderedEnumerable(), false);

            var r1 = new List<int>();
            foreach(var x in e1)
            {
                r1.Add(x.Second);
            }

            var r2 = new List<int>();
            foreach (var x in e2)
            {
                r2.Add(x.Third);
            }

            var r3 = new List<int>();
            foreach (var x in e3)
            {
                r3.Add(x.First);
            }

            Assert.IsTrue(new[] { 3, 3, 5 }.SequenceEqual(r1));
            Assert.IsTrue(new[] { 9, 9, 1 }.SequenceEqual(r2));
            Assert.IsTrue(new[] { 1, 2, 2 }.SequenceEqual(r3));
        }

        [TestMethod]
        public void CreateOrderedEnumerable_Weird()
        {
            // emptyOrdered
            {
                var i = Enumerable.Empty<int>();

                var e1 = i.OrderBy(x => x);
                var e2 = e1.CreateOrderedEnumerable(y => y, new _CreateOrderedEnumerable(), true);
                var e3 = e2.CreateOrderedEnumerable(z => z, new _CreateOrderedEnumerable(), false);

                var r1 = new List<int>();
                foreach (var x in e1)
                {
                    r1.Add(x);
                }

                var r2 = new List<int>();
                foreach (var x in e2)
                {
                    r2.Add(x);
                }

                var r3 = new List<int>();
                foreach (var x in e3)
                {
                    r3.Add(x);
                }

                Assert.IsTrue(!r1.Any());
                Assert.IsTrue(!r2.Any());
                Assert.IsTrue(!r3.Any());
            }

            // oneItemDefaultOrdered
            {
                var i = Enumerable.Empty<int>().DefaultIfEmpty();
                
                var e1 = i.OrderBy(x => x);
                var e2 = e1.CreateOrderedEnumerable(y => y, new _CreateOrderedEnumerable(), true);
                var e3 = e2.CreateOrderedEnumerable(z => z, new _CreateOrderedEnumerable(), false);

                var r1 = new List<int>();
                foreach (var x in e1)
                {
                    r1.Add(x);
                }

                var r2 = new List<int>();
                foreach (var x in e2)
                {
                    r2.Add(x);
                }

                var r3 = new List<int>();
                foreach (var x in e3)
                {
                    r3.Add(x);
                }

                Assert.IsTrue(new[] { 0 }.SequenceEqual(r1));
                Assert.IsTrue(new[] { 0 }.SequenceEqual(r2));
                Assert.IsTrue(new[] { 0 }.SequenceEqual(r3));
            }

            // oneItemSpecificOrdered
            {
                var i = Enumerable.Empty<int>().DefaultIfEmpty(4);

                var e1 = i.OrderBy(x => x);
                var e2 = e1.CreateOrderedEnumerable(y => y, new _CreateOrderedEnumerable(), true);
                var e3 = e2.CreateOrderedEnumerable(z => z, new _CreateOrderedEnumerable(), false);

                var r1 = new List<int>();
                foreach (var x in e1)
                {
                    r1.Add(x);
                }

                var r2 = new List<int>();
                foreach (var x in e2)
                {
                    r2.Add(x);
                }

                var r3 = new List<int>();
                foreach (var x in e3)
                {
                    r3.Add(x);
                }

                Assert.IsTrue(new[] { 4 }.SequenceEqual(r1));
                Assert.IsTrue(new[] { 4 }.SequenceEqual(r2));
                Assert.IsTrue(new[] { 4 }.SequenceEqual(r3));
            }
        }

        [TestMethod]
        public void CreateOrderedEnumerable_Errors()
        {
            var i =
                new[]
                {
                    new { First = 2, Second = 3, Third = 1},
                    new { First = 1, Second = 3, Third = 9},
                    new { First = 2, Second = 5, Third = 9}
                };

            var e = i.OrderBy(x => x.First);

            try
            {
                e.CreateOrderedEnumerable(null, new _CreateOrderedEnumerable(), true);
                Assert.Fail();
            }
            catch (ArgumentNullException exc)
            {
                Assert.AreEqual("keySelector", exc.ParamName);
            }
        }

        [TestMethod]
        public void CreateOrderedEnumerable_Errors_Weird()
        {
            // emptyOrdered
            {
                var e = Enumerable.Empty<int>().OrderBy(x => x);

                try
                {
                    e.CreateOrderedEnumerable(null, new _CreateOrderedEnumerable(), true);
                    Assert.Fail();
                }
                catch (ArgumentNullException exc)
                {
                    Assert.AreEqual("keySelector", exc.ParamName);
                }
            }

            // oneItemDefaultOrdered
            {
                var e = Enumerable.Empty<int>().DefaultIfEmpty().OrderBy(x => x);

                try
                {
                    e.CreateOrderedEnumerable(null, new _CreateOrderedEnumerable(), true);
                    Assert.Fail();
                }
                catch (ArgumentNullException exc)
                {
                    Assert.AreEqual("keySelector", exc.ParamName);
                }
            }

            // oneItemSpecificOrdered
            {
                var e = Enumerable.Empty<int>().DefaultIfEmpty(4).OrderBy(x => x);

                try
                {
                    e.CreateOrderedEnumerable(null, new _CreateOrderedEnumerable(), true);
                    Assert.Fail();
                }
                catch (ArgumentNullException exc)
                {
                    Assert.AreEqual("keySelector", exc.ParamName);
                }
            }
        }

        [TestMethod]
        public void CreateOrderedEnumerable_Malformed()
        {
            var e = new OrderByEnumerable<int, int, EmptyEnumerable<int>, EmptyEnumerator<int>, DefaultAscending<int, int>>();

            try
            {
                e.CreateOrderedEnumerable(null, new _CreateOrderedEnumerable(), true);
                Assert.Fail();
            }
            catch (ArgumentException exc)
            {
                Assert.AreEqual("source", exc.ParamName);
            }
        }

        [TestMethod]
        public void CreateOrderedEnumerable_Malformed_Weird()
        {
            // emptyOrdered
            {
                var e = new EmptyOrderedEnumerable<int>();

                try
                {
                    e.CreateOrderedEnumerable(null, new _CreateOrderedEnumerable(), true);
                    Assert.Fail();
                }
                catch (ArgumentException exc)
                {
                    Assert.AreEqual("source", exc.ParamName);
                }
            }

            // oneItemDefaultOrdered
            {
                var e = new OneItemDefaultOrderedEnumerable<int>();

                try
                {
                    e.CreateOrderedEnumerable(null, new _CreateOrderedEnumerable(), true);
                    Assert.Fail();
                }
                catch (ArgumentException exc)
                {
                    Assert.AreEqual("source", exc.ParamName);
                }
            }

            // oneItemSpecificOrdered
            {
                var e = new OneItemSpecificOrderedEnumerable<int>();

                try
                {
                    e.CreateOrderedEnumerable(null, new _CreateOrderedEnumerable(), true);
                    Assert.Fail();
                }
                catch (ArgumentException exc)
                {
                    Assert.AreEqual("source", exc.ParamName);
                }
            }
        }
    }
}
