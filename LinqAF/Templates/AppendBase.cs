using LinqAF.Impl;

namespace LinqAF
{
    abstract class AppendBase<TItem, TEnumerable, TEnumerator> :
        TemplateBase,
        IAppend<TItem, TEnumerable, TEnumerator>
        where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
        where TEnumerator : struct, IStructEnumerator<TItem>
    {
        public AppendEnumerable<TItem, TEnumerable, TEnumerator> Append(TItem element)
        => CommonImplementation.Append<TItem, TEnumerable, TEnumerator>(RefThis(), element);
    }
}
