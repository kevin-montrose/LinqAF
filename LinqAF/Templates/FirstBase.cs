using LinqAF.Impl;
using System;

namespace LinqAF
{
    abstract class FirstBase<TItem, TEnumerable, TEnumerator> :
        TemplateBase,
        IFirst<TItem>
        where TEnumerable: struct, IStructEnumerable<TItem, TEnumerator>
        where TEnumerator: struct, IStructEnumerator<TItem>
    {
        public TItem First()
        => CommonImplementation.First<TItem, TEnumerable, TEnumerator>(RefThis());

        public TItem First(Func<TItem, bool> predicate)
        => CommonImplementation.First<TItem, TEnumerable, TEnumerator>(RefThis(), predicate);

        public TItem FirstOrDefault()
        => CommonImplementation.FirstOrDefault<TItem, TEnumerable, TEnumerator>(RefThis());

        public TItem FirstOrDefault(Func<TItem, bool> predicate)
        => CommonImplementation.FirstOrDefault<TItem, TEnumerable, TEnumerator>(RefThis(), predicate);
    }
}
