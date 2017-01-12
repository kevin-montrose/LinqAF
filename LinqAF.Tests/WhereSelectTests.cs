using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace LinqAF.Tests
{
    [TestClass]
    public class WhereSelectTests
    {
        [TestMethod]
        public void Single()
        {
            var asWhere = new[] { 1, 2, 3 }.Where(x => x % 2 == 0);
            var asWhereSelect = asWhere.Select(i => i.ToString());
            Assert.IsTrue(asWhereSelect.GetType().IsValueType);

            var res = new List<string>();
            foreach(var item in asWhereSelect)
            {
                res.Add(item);
            }

            Assert.AreEqual(1, res.Count);
            Assert.AreEqual("2", res[0]);
        }

        [TestMethod]
        public void Double()
        {
            var asWhere = new[] { 1, 2, 3 }.Where(x => x % 2 == 0);
            var asWhereSelect = asWhere.Select(i => i.ToString()).Select(x => double.Parse(x + "." + x));
            Assert.IsTrue(asWhereSelect.GetType().IsValueType);

            var res = new List<double>();
            foreach (var item in asWhereSelect)
            {
                res.Add(item);
            }

            Assert.AreEqual(1, res.Count);
            Assert.AreEqual(2.2, res[0]);
        }

        [TestMethod]
        public void Triple()
        {
            var asWhere = new[] { 1, 2, 3 }.Where(x => x % 2 == 0);
            var asWhereSelect = asWhere.Select(i => i.ToString()).Select(x => double.Parse(x + "." + x)).Select(y => (long)((2.2 * y) / 3));
            Assert.IsTrue(asWhereSelect.GetType().IsValueType);

            var res = new List<long>();
            foreach (var item in asWhereSelect)
            {
                res.Add(item);
            }

            Assert.AreEqual(1, res.Count);
            Assert.AreEqual(1L, res[0]);
        }

        [TestMethod]
        public void Errors()
        {
            var asWhere = new[] { 1, 2, 3 }.Where(x => true);

            try
            {
                asWhere.Select(default(Func<int, bool>));
                Assert.Fail();
            }
            catch (ArgumentNullException exc)
            {
                Assert.AreEqual("selector", exc.ParamName);
            }
        }

        [TestMethod]
        public void Malformed()
        {
            var asWhere = new WhereEnumerable<int, EmptyEnumerable<int>, EmptyEnumerator<int>>();

            try
            {
                asWhere.Select(x => x);
                Assert.Fail();
            }
            catch (ArgumentException exc)
            {
                Assert.AreEqual("source", exc.ParamName);
            }
        }
    }
}
