using System;

namespace LinqAF.Impl
{
    interface ICount<TItem>
    {
        int Count();
        int Count(Func<TItem, bool> predicate);

        long LongCount();
        long LongCount(Func<TItem, bool> predicate);
    }
}
