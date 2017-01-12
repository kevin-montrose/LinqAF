using System;
using LinqAF.Impl;

namespace LinqAF
{
    abstract class SkipBase<TItem, TEnumerable, TEnumerator> :
        TemplateBase,
        ISkip<TItem, TEnumerable, TEnumerator>
        where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
        where TEnumerator : struct, IStructEnumerator<TItem>
    {
        public SkipEnumerable<TItem, TEnumerable, TEnumerator> Skip(int count)
        => CommonImplementation.Skip<TItem, TEnumerable, TEnumerator>(RefThis(), count);

        public SkipWhileIndexedEnumerable<TItem, TEnumerable, TEnumerator> SkipWhile(Func<TItem, int, bool> predicate)
        => CommonImplementation.SkipWhile<TItem, TEnumerable, TEnumerator>(RefThis(), predicate);

        public SkipWhileEnumerable<TItem, TEnumerable, TEnumerator> SkipWhile(Func<TItem, bool> predicate)
        => CommonImplementation.SkipWhile<TItem, TEnumerable, TEnumerator>(RefThis(), predicate);
    }
}
