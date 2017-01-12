using LinqAF.Impl;
namespace LinqAF
{
    abstract class ElementAtBase<TItem, TEnumerable, TEnumerator> :
        TemplateBase,
        IElementAt<TItem>
        where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
        where TEnumerator : struct, IStructEnumerator<TItem>
    {
        public TItem ElementAt(int index)
        => CommonImplementation.ElementAt<TItem, TEnumerable, TEnumerator>(RefThis(), index);

        public TItem ElementAtOrDefault(int index)
        => CommonImplementation.ElementAtOrDefault<TItem, TEnumerable, TEnumerator>(RefThis(), index);
    }
}
