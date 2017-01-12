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
    }
}
