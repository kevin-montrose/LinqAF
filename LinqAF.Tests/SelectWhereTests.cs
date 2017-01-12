using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace LinqAF.Tests
{
    [TestClass]
    public class SelectWhereTests
    {
        [TestMethod]
        public void Single()
        {
            var select = new[] { 1, 2, 3 }.Select(x => x * 2);
            var selectWhere = select.Where(t => t % 3 == 0);

            Assert.IsTrue(selectWhere.GetType().IsValueType);

            var res = new List<int>();
            foreach (var item in selectWhere)
            {
                res.Add(item);
            }

            Assert.AreEqual(1, res.Count);
            Assert.AreEqual(6, res[0]);
        }

        [TestMethod]
        public void Double()
        {
            var select = new[] { 1, 2, 3, 4, 5, 6 }.Select(x => x * 2);
            var selectWhere = select.Where(t => t % 3 == 0).Where(t => t > 7);

            Assert.IsTrue(selectWhere.GetType().IsValueType);

            var res = new List<int>();
            foreach (var item in selectWhere)
            {
                res.Add(item);
            }

            Assert.AreEqual(1, res.Count);
            Assert.AreEqual(12, res[0]);
        }

        [TestMethod]
        public void Triple()
        {
            var select = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }.Select(x => x * 2);
            var selectWhere = select.Where(t => t % 3 == 0).Where(t => t > 7).Where(t => t / 9 > 1);

            Assert.IsTrue(selectWhere.GetType().IsValueType);

            var res = new List<int>();
            foreach (var item in selectWhere)
            {
                res.Add(item);
            }

            Assert.AreEqual(1, res.Count);
            Assert.AreEqual(18, res[0]);
        }

        [TestMethod]
        public void Errors()
        {
            var e = new[] { 1 }.Select(i => i);

            try
            {
                var _ = e.Where(default(Func<int, bool>));
                Assert.Fail();
            }
            catch (ArgumentNullException exc)
            {
                Assert.AreEqual("predicate", exc.ParamName);
            }
        }

        [TestMethod]
        public void Malformed()
        {
            var e = new SelectEnumerable<int, int, EmptyEnumerable<int>, EmptyEnumerator<int>>();

            try
            {
                var _ = e.Where(x => true);
                Assert.Fail();
            }
            catch (ArgumentException exc)
            {
                Assert.AreEqual("source", exc.ParamName);
            }
        }
    }
}
