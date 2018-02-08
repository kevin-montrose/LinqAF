using LinqAF.Impl;
using System;

namespace LinqAF
{
    public partial struct TakeLastEnumerable<TItem, TInnerEnumerable, TInnerEnumerator>
    {
        public TakeLastEnumerable<TItem, TInnerEnumerable, TInnerEnumerator> TakeLast(int count)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");

            if (count < 0) count = 0;

            var newCount = Math.Min(count, this.InnerCount);

            return new TakeLastEnumerable<TItem, TInnerEnumerable, TInnerEnumerator>(ref Inner, newCount);
        }
    }
}
