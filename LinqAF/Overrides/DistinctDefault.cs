using System;

namespace LinqAF
{
    partial struct DistinctDefaultEnumerable<TItem, TInnerEnumerable, TInnerEnumerator>
    {
        public DistinctDefaultEnumerable<TItem, TInnerEnumerable, TInnerEnumerator> Distinct()
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "source");

            return this;
        }
    }
}
