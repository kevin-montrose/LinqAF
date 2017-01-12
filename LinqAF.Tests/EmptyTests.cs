using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using LinqAF;
using System.Collections.Generic;
using TestHelpers;

namespace LinqAF.Tests
{
    [TestClass]
    public class EmptyTests
    {
        [TestMethod]
        public void Simple()
        {
            var asEmpty = Enumerable.Empty<int>();

            Assert.IsTrue(asEmpty.GetType().IsValueType);

            var e = asEmpty.GetEnumerator();
            Assert.IsFalse(e.MoveNext());
        }

        [TestMethod]
        public void Ordered()
        {
            var asEmpty = Enumerable.Empty<int>().OrderBy(x => x);

            Assert.IsTrue(asEmpty.GetType().IsValueType);

            var e = asEmpty.GetEnumerator();
            Assert.IsFalse(e.MoveNext());
        }
    }
}