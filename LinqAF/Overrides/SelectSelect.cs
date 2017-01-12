using LinqAF.Impl;
using System;

namespace LinqAF
{
    public partial struct SelectSelectEnumerable<TOutItem, TInnerItem, TInnerEnumerable, TInnerEnumerator, TProjection>
    {
        public SelectSelectEnumerable<TSelect_OutItem, TInnerItem, TInnerEnumerable, TInnerEnumerator, ChainedProjection<TSelect_OutItem, TInnerItem, TOutItem, SingleProjection<TSelect_OutItem, TOutItem>, TProjection>> Select<TSelect_OutItem>(Func<TOutItem, TSelect_OutItem> selector)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "source");
            var bridge = CommonImplementation.Bridge<TSelect_OutItem, TInnerItem, TOutItem, TProjection>(ref Projection, selector, nameof(selector));

            return new SelectSelectEnumerable<TSelect_OutItem, TInnerItem, TInnerEnumerable, TInnerEnumerator, ChainedProjection<TSelect_OutItem, TInnerItem, TOutItem, SingleProjection<TSelect_OutItem, TOutItem>, TProjection>>(ref Inner, ref bridge);
        }

        public SelectWhereEnumerable<TOutItem, TInnerItem, TInnerEnumerable, TInnerEnumerator, TProjection, SinglePredicate<TOutItem>> Where(Func<TOutItem, bool> predicate)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "source");
            var bridge = CommonImplementation.Bridge(predicate, nameof(predicate));

            return new SelectWhereEnumerable<TOutItem, TInnerItem, TInnerEnumerable, TInnerEnumerator, TProjection, SinglePredicate<TOutItem>>(ref Inner, ref Projection, ref bridge);
        }
    }
}
