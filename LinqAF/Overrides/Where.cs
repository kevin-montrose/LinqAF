using LinqAF.Impl;
using System;

namespace LinqAF
{
    public partial struct WhereEnumerable<TItem, TInnerEnumerable, TInnerEnumerator>
    {
        public WhereSelectEnumerable<TSelect_OutItem, TItem, TInnerEnumerable, TInnerEnumerator, SinglePredicate<TItem>, SingleProjection<TSelect_OutItem, TItem>> Select<TSelect_OutItem>(Func<TItem, TSelect_OutItem> selector)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");
            var predicate = new SinglePredicate<TItem>(Filter);
            var bridge = CommonImplementation.Bridge(selector, nameof(selector));

            return new WhereSelectEnumerable<TSelect_OutItem, TItem, TInnerEnumerable, TInnerEnumerator, SinglePredicate<TItem>, SingleProjection<TSelect_OutItem, TItem>>(ref Inner, ref predicate, ref bridge);
        }

        public WhereWhereEnumerable<TItem, TInnerEnumerable, TInnerEnumerator, ChainedPredicate<TItem, SinglePredicate<TItem>, SinglePredicate<TItem>>> Where(Func<TItem, bool> predicate)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");
            var bridge = CommonImplementation.Bridge(Filter, predicate, nameof(predicate));

            return new WhereWhereEnumerable<TItem, TInnerEnumerable, TInnerEnumerator, ChainedPredicate<TItem, SinglePredicate<TItem>, SinglePredicate<TItem>>>(ref Inner, ref bridge);
        }
    }
}
