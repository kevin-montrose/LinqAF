using LinqAF.Impl;
using System;

namespace LinqAF
{
    abstract class AllBase<TItem, TEnumerable, TEnumerator> :
        TemplateBase,
        IAll<TItem>
        where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
        where TEnumerator : struct, IStructEnumerator<TItem>
    {
        public bool All(Func<TItem, bool> predicate)
        => CommonImplementation.All<TItem, TEnumerable, TEnumerator>(RefThis(), predicate);
    }
}
