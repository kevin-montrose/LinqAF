using LinqAF.Impl;
using System;

namespace LinqAF
{
    public partial struct WhereSelectEnumerable<TOutItem, TInnerItem, TInnerEnumerable, TInnerEnumerator, TPredicate, TProjection>
    {
        public WhereSelectEnumerable<TSelect_OutItem, TInnerItem, TInnerEnumerable, TInnerEnumerator, TPredicate, ChainedProjection<TSelect_OutItem, TInnerItem, TOutItem, SingleProjection<TSelect_OutItem, TOutItem>, TProjection>> Select<TSelect_OutItem>(Func<TOutItem, TSelect_OutItem> selector)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "source");
            var bridge = CommonImplementation.Bridge<TSelect_OutItem, TInnerItem, TOutItem, TProjection>(ref Projection, selector, nameof(selector));

            return new WhereSelectEnumerable<TSelect_OutItem, TInnerItem, TInnerEnumerable, TInnerEnumerator, TPredicate, ChainedProjection<TSelect_OutItem, TInnerItem, TOutItem, SingleProjection<TSelect_OutItem, TOutItem>, TProjection>>(ref Inner, ref Predicate, ref bridge);
        }
    }
}
