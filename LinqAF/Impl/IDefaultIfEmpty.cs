namespace LinqAF.Impl
{
    interface IDefaultIfEmpty<TItem, TInnerEnumerable, TInnerEnumerator>
        where TInnerEnumerable: struct, IStructEnumerable<TItem, TInnerEnumerator>
        where TInnerEnumerator: struct, IStructEnumerator<TItem>
    {
        DefaultIfEmptyDefaultEnumerable<TItem, TInnerEnumerable, TInnerEnumerator> DefaultIfEmpty();
        DefaultIfEmptySpecificEnumerable<TItem, TInnerEnumerable, TInnerEnumerator> DefaultIfEmpty(TItem item);
    }
}
