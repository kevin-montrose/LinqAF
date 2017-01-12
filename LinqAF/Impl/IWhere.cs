using System;

namespace LinqAF.Impl
{
    interface IWhere<TOutItem, TInnerEnumerable, TInnerEnumerator> 
        where TInnerEnumerable : struct, IStructEnumerable<TOutItem, TInnerEnumerator>
        where TInnerEnumerator : struct, IStructEnumerator<TOutItem>
    {
        WhereEnumerable<TOutItem, TInnerEnumerable, TInnerEnumerator> Where(Func<TOutItem, bool> predicate);
        WhereIndexedEnumerable<TOutItem, TInnerEnumerable, TInnerEnumerator> Where(Func<TOutItem, int, bool> predicate);
    }

    interface IWhereSpecialized<TOutItem, TOnWhereEnumerable, TOnWhereEnumerator, TOnWhereIndexedEnumerable, TOnWhereIndexedEnumerator>
        where TOnWhereEnumerable: struct, IStructEnumerable<TOutItem, TOnWhereEnumerator>
        where TOnWhereEnumerator: struct, IStructEnumerator<TOutItem>
        where TOnWhereIndexedEnumerable: struct, IStructEnumerable<TOutItem, TOnWhereIndexedEnumerator>
        where TOnWhereIndexedEnumerator: struct, IStructEnumerator<TOutItem>
    {
        TOnWhereEnumerable Where(Func<TOutItem, bool> predicate);
        TOnWhereIndexedEnumerable Where(Func<TOutItem, int, bool> predicate);
    }
}
