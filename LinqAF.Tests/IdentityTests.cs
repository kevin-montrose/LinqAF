using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using LinqAF;
using System.Collections.Generic;
using TestHelpers;

namespace LinqAF.Tests
{
    [TestClass]
    public class IdentityTests
    {
        [TestMethod]
        public void Simple()
        {
            var e = new List<int> { 1, 2, 3 };
            var asIdent = Enumerable.Empty<int>().Concat(e);

            Assert.IsTrue(asIdent.GetType().IsValueType);

            var res = new List<int>();
            foreach(var item in asIdent)
            {
                res.Add(item);
            }

            Assert.AreEqual(3, res.Count);
            Assert.AreEqual(1, res[0]);
            Assert.AreEqual(2, res[1]);
            Assert.AreEqual(3, res[2]);
        }
    }
}
