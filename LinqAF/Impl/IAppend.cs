namespace LinqAF.Impl
{
    interface IAppend<TItem, TInnerEnumerable, TInnerEnumerator>
        where TInnerEnumerable: struct, IStructEnumerable<TItem, TInnerEnumerator>
        where TInnerEnumerator: struct, IStructEnumerator<TItem>
    {
        AppendEnumerable<TItem, TInnerEnumerable, TInnerEnumerator> Append(TItem element);
    }
}
