using LinqAF.Impl;

namespace LinqAF
{
    abstract class PrependBase<TItem, TEnumerable, TEnumerator> :
        TemplateBase,
        IPrepend<TItem, TEnumerable, TEnumerator>
        where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
        where TEnumerator : struct, IStructEnumerator<TItem>
    {
        public PrependEnumerable<TItem, TEnumerable, TEnumerator> Prepend(TItem element)
        => CommonImplementation.Prepend<TItem, TEnumerable, TEnumerator>(RefThis(), element);
    }
}
