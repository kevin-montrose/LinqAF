namespace LinqAF.Impl
{
    interface IOfType<TInItem, TThisEnumerable, TThisEnumerator>
        where TThisEnumerable : struct, IStructEnumerable<TInItem, TThisEnumerator>
        where TThisEnumerator : struct, IStructEnumerator<TInItem>
    {
        OfTypeEnumerable<TInItem, TOutItem, TThisEnumerable, TThisEnumerator> OfType<TOutItem>();
    }
}
