namespace LinqAF.Impl
{
    interface IPrepend<TItem, TInnerEnumerable, TInnerEnumerator>
        where TInnerEnumerable: struct, IStructEnumerable<TItem, TInnerEnumerator>
        where TInnerEnumerator: struct, IStructEnumerator<TItem>
    {
        PrependEnumerable<TItem, TInnerEnumerable, TInnerEnumerator> Prepend(TItem element);
    }
}
