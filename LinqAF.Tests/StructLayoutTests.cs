using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace LinqAF.Tests
{
    [TestClass]
    public class StructLayoutTests
    {
        [TestMethod]
        public void AllStructs()
        {
            var missing = new HashSet<string>();

            foreach(var type in typeof(LinqAF.Enumerable).Assembly.GetTypes())
            {
                if (!type.IsValueType) continue;

                if (!type.IsAutoLayout)
                {
                    missing.Add(type.Name);
                }
            }

            Assert.AreEqual(0, missing.Count, string.Join(", ", missing));
        }
    }
}
