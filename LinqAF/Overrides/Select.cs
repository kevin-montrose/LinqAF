using LinqAF.Impl;
using System;

namespace LinqAF
{
    public partial struct SelectEnumerable<TInItem, TOutItem, TInnerEnumerable, TInnerEnumerator>
    {
        public SelectSelectEnumerable<TSelect_OutItem, TInItem, TInnerEnumerable, TInnerEnumerator, ChainedProjection<TSelect_OutItem, TInItem, TOutItem, SingleProjection<TSelect_OutItem, TOutItem>, SingleProjection<TOutItem, TInItem>>> Select<TSelect_OutItem>(Func<TOutItem, TSelect_OutItem> selector)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");
            var bridge = CommonImplementation.Bridge(this.Mapper, selector, nameof(selector));

            return new SelectSelectEnumerable<TSelect_OutItem, TInItem, TInnerEnumerable, TInnerEnumerator, ChainedProjection<TSelect_OutItem, TInItem, TOutItem, SingleProjection<TSelect_OutItem, TOutItem>, SingleProjection<TOutItem, TInItem>>>(ref Inner, ref bridge);
        }

        public SelectWhereEnumerable<TOutItem, TInItem, TInnerEnumerable, TInnerEnumerator, SingleProjection<TOutItem, TInItem>, SinglePredicate<TOutItem>> Where(Func<TOutItem, bool> predicate)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");
            var project = new SingleProjection<TOutItem, TInItem>(this.Mapper);
            var bridged = CommonImplementation.Bridge(predicate, nameof(predicate));

            return new SelectWhereEnumerable<TOutItem, TInItem, TInnerEnumerable, TInnerEnumerator, SingleProjection<TOutItem, TInItem>, SinglePredicate<TOutItem>>(ref Inner, ref project, ref bridged);
        }
    }
}
