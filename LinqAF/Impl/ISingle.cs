using System;

namespace LinqAF.Impl
{
    interface ISingle<TItem>
    {
        TItem Single();
        TItem Single(Func<TItem, bool> predicate);
        TItem SingleOrDefault();
        TItem SingleOrDefault(Func<TItem, bool> predicate);
    }
}
