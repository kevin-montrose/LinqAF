using System.Collections.Generic;

namespace LinqAF.Benchmark.Helpers
{
    class CharComparer : IEqualityComparer<char>, IComparer<char>
    {
        public int Compare(char x, char y) => x.CompareTo(y);

        public bool Equals(char x, char y) => x == y;

        public int GetHashCode(char obj) => (int)obj;
    }
}
