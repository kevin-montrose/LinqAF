using LinqAF.Impl;
using System;

namespace LinqAF
{
    partial struct DistinctDefaultEnumerable<TItem, TInnerEnumerable, TInnerEnumerator>
    {
        public DistinctDefaultEnumerable<TItem, TInnerEnumerable, TInnerEnumerator> Distinct()
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");

            return this;
        }
    }
}
