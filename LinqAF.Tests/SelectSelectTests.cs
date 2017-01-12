using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace LinqAF.Tests
{
    [TestClass]
    public class SelectSelectTests
    {
        [TestMethod]
        public void Single()
        {
            var select = new[] { 1, 2, 3 }.Select(x => x + 1);
            var selectSelect = select.Select(x => x.ToString());

            Assert.IsTrue(selectSelect.GetType().IsValueType);

            var res = new List<string>();
            foreach (var item in selectSelect)
            {
                res.Add(item);
            }

            Assert.AreEqual(3, res.Count);
            Assert.AreEqual("2", res[0]);
            Assert.AreEqual("3", res[1]);
            Assert.AreEqual("4", res[2]);
        }

        [TestMethod]
        public void Double()
        {
            var select = new[] { 1, 2, 3 }.Select(x => x + 1);
            var selectSelect = select.Select(x => x.ToString()).Select(y => double.Parse(y + "." + y));

            Assert.IsTrue(selectSelect.GetType().IsValueType);

            var res = new List<double>();
            foreach (var item in selectSelect)
            {
                res.Add(item);
            }

            Assert.AreEqual(3, res.Count);
            Assert.AreEqual(2.2, res[0]);
            Assert.AreEqual(3.3, res[1]);
            Assert.AreEqual(4.4, res[2]);
        }

        [TestMethod]
        public void Triple()
        {
            var select = new[] { 1, 2, 3 }.Select(x => x + 1);
            var selectSelect = select.Select(x => x.ToString()).Select(y => double.Parse(y + "." + y)).Select(z => (long)(z * 1.5));

            Assert.IsTrue(selectSelect.GetType().IsValueType);

            var res = new List<long>();
            foreach (var item in selectSelect)
            {
                res.Add(item);
            }

            Assert.AreEqual(3, res.Count);
            Assert.AreEqual(3L, res[0]);
            Assert.AreEqual(4L, res[1]);
            Assert.AreEqual(6L, res[2]);
        }

        [TestMethod]
        public void ChainedWhere()
        {
            var select = new[] { 1, 2, 3 }.Select(x => x * 10);
            var selectSelect = select.Select(x => x.ToString());
            var selectWhere = selectSelect.Where(x => x.StartsWith("2"));

            Assert.IsTrue(selectSelect.GetType().IsValueType);

            var res = new List<string>();
            foreach (var item in selectWhere)
            {
                res.Add(item);
            }

            Assert.AreEqual(1, res.Count);
            Assert.AreEqual("20", res[0]);
        }

        [TestMethod]
        public void Errors()
        {
            var foo = new string[0].Select(x => x);

            try
            {
                foo.Select(default(Func<string, string>));
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
            var e = new SelectEnumerable<int, int, EmptyEnumerable<int>, EmptyEnumerator<int>>();

            try
            {
                var _ = e.Select(x => x);
                Assert.Fail();
            }
            catch (ArgumentException exc)
            {
                Assert.AreEqual("source", exc.ParamName);
            }
        }
    }
}
