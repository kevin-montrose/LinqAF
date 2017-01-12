using LinqAF.Impl;

namespace LinqAF
{
    abstract class DefaultIfEmptyBase<TItem, TEnumerable, TEnumerator> :
        TemplateBase,
        IDefaultIfEmpty<TItem, TEnumerable, TEnumerator>
        where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
        where TEnumerator : struct, IStructEnumerator<TItem>
    {
        public DefaultIfEmptyDefaultEnumerable<TItem, TEnumerable, TEnumerator> DefaultIfEmpty()
        => CommonImplementation.DefaultIfEmpty<TItem, TEnumerable, TEnumerator>(RefThis());

        public DefaultIfEmptySpecificEnumerable<TItem, TEnumerable, TEnumerator> DefaultIfEmpty(TItem item)
        => CommonImplementation.DefaultIfEmpty<TItem, TEnumerable, TEnumerator>(RefThis(), item);
    }
}
