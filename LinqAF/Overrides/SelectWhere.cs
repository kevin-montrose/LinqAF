using LinqAF.Impl;
using System;

namespace LinqAF
{
    public partial struct SelectWhereEnumerable<TOutItem, TInnerItem, TInnerEnumerable, TInnerEnumerator, TProjection, TPredicate>
    {
        public SelectWhereEnumerable<TOutItem, TInnerItem, TInnerEnumerable, TInnerEnumerator, TProjection, ChainedPredicate<TOutItem, SinglePredicate<TOutItem>, TPredicate>> Where(Func<TOutItem, bool> predicate)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");
            var bridged = CommonImplementation.Bridge(predicate, nameof(predicate), ref Predicate);

            return new SelectWhereEnumerable<TOutItem, TInnerItem, TInnerEnumerable, TInnerEnumerator, TProjection, ChainedPredicate<TOutItem, SinglePredicate<TOutItem>, TPredicate>>(ref Inner, ref Project, ref bridged);
        }
    }
}
