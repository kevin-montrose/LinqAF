using System;
using System.Collections.Generic;

namespace LinqAF.Benchmark.Helpers
{
    internal class IntComparer : IEqualityComparer<int>, IComparer<int>
    {
        public int Compare(int x, int y) => x.CompareTo(y);

        public bool Equals(int x, int y) => x == y;

        public int GetHashCode(int obj) => obj;
    }
}
