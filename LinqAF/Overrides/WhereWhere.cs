using LinqAF.Impl;
using System;

namespace LinqAF
{
    public partial struct WhereWhereEnumerable<TItem, TInnerEnumerable, TInnerEnumerator, TPredicate>
    {
        public WhereWhereEnumerable<TItem, TInnerEnumerable, TInnerEnumerator, ChainedPredicate<TItem, TPredicate, SinglePredicate<TItem>>> Where(Func<TItem, bool> predicate)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "source");
            var bridge = CommonImplementation.Bridge(ref Predicate, predicate, nameof(predicate));

            return new WhereWhereEnumerable<TItem, TInnerEnumerable, TInnerEnumerator, ChainedPredicate<TItem, TPredicate, SinglePredicate<TItem>>>(ref Inner, ref bridge);
        }

        public WhereSelectEnumerable<TSelect_OutItem, TItem, TInnerEnumerable, TInnerEnumerator, TPredicate, SingleProjection<TSelect_OutItem, TItem>> Select<TSelect_OutItem>(Func<TItem, TSelect_OutItem> selector)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "source");
            var bridge = CommonImplementation.Bridge(selector, nameof(selector));

            return new WhereSelectEnumerable<TSelect_OutItem, TItem, TInnerEnumerable, TInnerEnumerator, TPredicate, SingleProjection<TSelect_OutItem, TItem>>(ref Inner, ref Predicate, ref bridge);
        }
    }
}
