using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using TestHelpers;
using System.Reflection;
using System.Text;

namespace LinqAF.Tests
{
    [TestClass]
    public class PrependTests
    {
        [TestMethod]
        public void InstanceExtensionNoOverlap()
        {
            Dictionary<MethodInfo, List<MethodInfo>> instOverlaps, extOverlaps;
            Helper.GetOverlappingMethods(typeof(Impl.IPrepend<,,>), out instOverlaps, out extOverlaps);

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
                if (!Helper.Implements(e, typeof(Impl.IPrepend<,,>), out missing))
                {
                    Assert.Fail($"{e.Name} does not implement IPrepend ({string.Join(", ", missing)})");
                }
            }
        }

        [TestMethod]
        public void Simple()
        {
            var e = new[] { 1, 2, 3 };
            var asPrepend = e.Prepend(4);

            Assert.IsTrue(asPrepend.GetType().IsValueType);

            var res = new List<int>();
            foreach(var item in asPrepend)
            {
                res.Add(item);
            }

            Assert.AreEqual(4, res.Count);
            Assert.AreEqual(4, res[0]);
            Assert.AreEqual(1, res[1]);
            Assert.AreEqual(2, res[2]);
            Assert.AreEqual(3, res[3]);
        }

        [TestMethod]
        public void Empty()
        {
            var asPrepend = Enumerable.Empty<int>().Prepend(4);

            Assert.IsTrue(asPrepend.GetType().IsValueType);

            var res = new List<int>();
            foreach (var item in asPrepend)
            {
                res.Add(item);
            }

            Assert.AreEqual(1, res.Count);
            Assert.AreEqual(4, res[0]);
        }

        [TestMethod]
        public void Chaining()
        {
            Helper.ForEachEnumerableExpression(
                new object[0],
                new[] { 1, 2, 3 },
                res =>
                {
                    Assert.AreEqual(4, res.Count);
                    Assert.AreEqual(1, res[0]);
                    Assert.AreEqual(1, res[1]);
                    Assert.AreEqual(2, res[2]);
                    Assert.AreEqual(3, res[3]);
                },
                "(_, a) => a.Prepend(1)",
                typeof(EmptyEnumerable<>),
                typeof(EmptyOrderedEnumerable<>),
                typeof(GroupByDefaultEnumerable<,,,,>),
                typeof(GroupBySpecificEnumerable<,,,,>),
                typeof(LookupDefaultEnumerable<,>),
                typeof(LookupSpecificEnumerable<,>)
            );
        }

        [TestMethod]
        public void Malformed()
        {
            Helper.ForEachMalformedEnumerableExpression<int>(
                @"a => { try { a.Prepend(0); Assert.Fail(); } catch (ArgumentException exc) { Assert.AreEqual(""source"", exc.ParamName); } }",
                typeof(GroupByDefaultEnumerable<,,,,>),
                typeof(GroupBySpecificEnumerable<,,,,>),
                typeof(LookupDefaultEnumerable<,>),
                typeof(LookupSpecificEnumerable<,>)
            );
        }
    }
}
