using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using TestHelpers;

namespace LinqAF.Tests
{
    [TestClass]
    public class RepeatTests
    {
        [TestMethod]
        public void Simple()
        {
            var asRepeat = Enumerable.Repeat(5, 5);

            Assert.IsTrue(asRepeat.GetType().IsValueType);

            var ret = new List<int>();
            foreach(var item in asRepeat)
            {
                ret.Add(item);
            }

            Assert.AreEqual(5, ret.Count);
            Assert.AreEqual(5, ret[0]);
            Assert.AreEqual(5, ret[1]);
            Assert.AreEqual(5, ret[2]);
            Assert.AreEqual(5, ret[3]);
            Assert.AreEqual(5, ret[4]);
        }
    }
}
