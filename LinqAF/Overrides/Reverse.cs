using System;

namespace LinqAF
{
    public partial struct ReverseEnumerable<TItem, TEnumerable, TEnumerator>
    {
        public TEnumerable Reverse()
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "source");

            return Inner;
        }
    }
}
