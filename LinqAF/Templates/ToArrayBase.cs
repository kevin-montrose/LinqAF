using LinqAF.Impl;

namespace LinqAF
{
    abstract class ToArrayBase<TItem, TEnumerable, TEnumerator> :
        TemplateBase,
        IToArray<TItem>
        where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
        where TEnumerator : struct, IStructEnumerator<TItem>
    {
        public TItem[] ToArray()
        => CommonImplementation.ToArray<TItem, TEnumerable, TEnumerator>(RefThis());
    }
}
