using LinqAF.Impl;

namespace LinqAF
{
    abstract class CastBase<TItem, TEnumerable, TEnumerator> :
         TemplateBase,
         ICast<TItem, TEnumerable, TEnumerator>
         where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
         where TEnumerator : struct, IStructEnumerator<TItem>
    {
        public CastEnumerable<TItem, TCastOutItem, TEnumerable, TEnumerator> Cast<TCastOutItem>()
        => CommonImplementation.Cast<TItem, TCastOutItem, TEnumerable, TEnumerator>(RefThis());
    }
}
