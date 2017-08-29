using LinqAF.Impl;
using System;

namespace LinqAF
{
    public partial struct ReverseEnumerable<TItem, TEnumerable, TEnumerator>
    {
        public TEnumerable Reverse()
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");

            return Inner;
        }
    }
}
