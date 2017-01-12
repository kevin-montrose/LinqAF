namespace LinqAF.Impl
{
    interface IHasComparer<TItem, TKey, TComparer, TInnerEnumerable, TInnerEnumerator>
        where TComparer: struct, IStructComparer<TItem, TKey>
        where TInnerEnumerable: struct, IStructEnumerable<TItem, TInnerEnumerator>
        where TInnerEnumerator: struct, IStructEnumerator<TItem>
    {
        TComparer GetComparer();
        TInnerEnumerable GetInnerEnumerable();
    }
}