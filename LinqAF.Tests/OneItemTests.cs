using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using LinqAF;
using System.Collections.Generic;

namespace LinqAF.Tests
{
    [TestClass]
    public class OneItemTests
    {
        [TestMethod]
        public void Default()
        {
            var e = Enumerable.Empty<int>().DefaultIfEmpty();

            Assert.IsTrue(e.GetType().IsValueType);

            var res = new List<int>();
            foreach(var item in e)
            {
                res.Add(item);
            }

            Assert.AreEqual(1, res.Count);
            Assert.AreEqual(default(int), res[0]);
        }

        [TestMethod]
        public void DefaultOrdered()
        {
            var e = Enumerable.Empty<int>().DefaultIfEmpty().OrderBy(x => x);

            Assert.IsTrue(e.GetType().IsValueType);

            var res = new List<int>();
            foreach (var item in e)
            {
                res.Add(item);
            }

            Assert.AreEqual(1, res.Count);
            Assert.AreEqual(default(int), res[0]);
        }

        [TestMethod]
        public void Specific()
        {
            var e = Enumerable.Empty<int>().DefaultIfEmpty(4);

            Assert.IsTrue(e.GetType().IsValueType);

            var res = new List<int>();
            foreach (var item in e)
            {
                res.Add(item);
            }

            Assert.AreEqual(1, res.Count);
            Assert.AreEqual(4, res[0]);
        }

        [TestMethod]
        public void SpecificOrdered()
        {
            var e = Enumerable.Empty<int>().DefaultIfEmpty(4).OrderBy(x => x);

            Assert.IsTrue(e.GetType().IsValueType);

            var res = new List<int>();
            foreach (var item in e)
            {
                res.Add(item);
            }

            Assert.AreEqual(1, res.Count);
            Assert.AreEqual(4, res[0]);
        }
    }
}
