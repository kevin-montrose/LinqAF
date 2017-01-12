using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using TestHelpers;

namespace LinqAF.Tests
{
    [TestClass]
    public class RangeTests
    {
        [TestMethod]
        public void Simple()
        {
            var asRange = Enumerable.Range(5, 5);

            Assert.IsTrue(asRange.GetType().IsValueType);

            var ret = new List<int>();
            foreach(var item in asRange)
            {
                ret.Add(item);
            }

            Assert.AreEqual(5, ret.Count);
            Assert.AreEqual(5, ret[0]);
            Assert.AreEqual(6, ret[1]);
            Assert.AreEqual(7, ret[2]);
            Assert.AreEqual(8, ret[3]);
            Assert.AreEqual(9, ret[4]);
        }
    }
}
