using System;

namespace LinqAF.Impl
{
    interface ISelect<TInItem, TThisEnumerable, TThisEnumerator>
        where TThisEnumerable: struct, IStructEnumerable<TInItem, TThisEnumerator>
        where TThisEnumerator: struct, IStructEnumerator<TInItem>
    {
        SelectEnumerable<TInItem, TOutItem, TThisEnumerable, TThisEnumerator> Select<TOutItem>(Func<TInItem, TOutItem> selector);

        SelectIndexedEnumerable<TInItem, TOutItem, TThisEnumerable, TThisEnumerator> Select<TOutItem>(Func<TInItem, int, TOutItem> selector);
    }
}
