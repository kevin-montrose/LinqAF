using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TestHelpers;
using System.Reflection;
using System.Text;
using System.Collections.Generic;

namespace LinqAF.Tests
{
    [TestClass]
    public class ToHashSetTests
    {
        [TestMethod]
        public void InstanceExtensionNoOverlap()
        {
            Dictionary<MethodInfo, List<MethodInfo>> instOverlaps, extOverlaps;
            Helper.GetOverlappingMethods(typeof(Impl.IToHashSet<>), out instOverlaps, out extOverlaps);

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
                if (!Helper.Implements(e, typeof(LinqAF.Impl.IToHashSet<>), out missing))
                {
                    Assert.Fail($"{e.Name} does not implement IToHashSet ({string.Join(", ", missing)})");
                }
            }
        }

        [TestMethod]
        public void Chaining()
        {
            foreach (var e in Helper.GetEnumerables(new[] { 1, 3, 2 }))
            {
                var x = e.ToHashSet();
                Assert.AreEqual(typeof(System.Collections.Generic.HashSet<int>), x.GetType());
                var y = (System.Collections.Generic.HashSet<int>)x;
                Assert.AreEqual(3, y.Count);
                Assert.IsTrue(y.Contains(1));
                Assert.IsTrue(y.Contains(2));
                Assert.IsTrue(y.Contains(3));
            }

            foreach (var e in Helper.GetEnumerables(new[] { "foo", "FOO", "bar" }))
            {
                var x = e.ToHashSet(StringComparer.InvariantCultureIgnoreCase);
                Assert.AreEqual(typeof(System.Collections.Generic.HashSet<string>), x.GetType());
                var y = (System.Collections.Generic.HashSet<string>)x;
                Assert.AreEqual(2, y.Count);
                Assert.IsTrue(y.Contains("foo"));
                Assert.IsTrue(y.Contains("FOO"));
                Assert.IsTrue(y.Contains("Bar"));
            }
        }

        [TestMethod]
        public void Malformed()
        {
            foreach (var e in Helper.GetMalformedEnumerables<string>())
            {
                try
                {
                    var x = e.ToHashSet();
                    Assert.Fail();
                }
                catch (ArgumentException x)
                {
                    Assert.AreEqual("source", x.ParamName);
                }

                try
                {
                    var x = e.ToHashSet(StringComparer.InvariantCulture);
                    Assert.Fail();
                }
                catch (ArgumentException x)
                {
                    Assert.AreEqual("source", x.ParamName);
                }
            }
        }
    }
}
