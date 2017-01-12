namespace LinqAF.Impl
{
    interface ICast<TCastInItem, TCastThisEnumerable, TCastThisEnumerator>
        where TCastThisEnumerable: struct, IStructEnumerable<TCastInItem, TCastThisEnumerator>
        where TCastThisEnumerator: struct, IStructEnumerator<TCastInItem>
    {
        CastEnumerable<TCastInItem, TCastOutItem, TCastThisEnumerable, TCastThisEnumerator> Cast<TCastOutItem>();
    }
}
