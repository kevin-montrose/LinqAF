using LinqAF.Impl;

namespace LinqAF
{
    abstract class OfTypeBase<TItem, TEnumerable, TEnumerator> :
         TemplateBase,
         IOfType<TItem, TEnumerable, TEnumerator>
         where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
         where TEnumerator : struct, IStructEnumerator<TItem>
    {
        public OfTypeEnumerable<TItem, TOfTypeOutItem, TEnumerable, TEnumerator> OfType<TOfTypeOutItem>()
        => CommonImplementation.OfType<TItem, TOfTypeOutItem, TEnumerable, TEnumerator>(RefThis());
    }
}
