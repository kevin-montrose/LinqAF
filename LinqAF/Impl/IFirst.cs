using System;

namespace LinqAF.Impl
{
    interface IFirst<TItem>
    {
        TItem First();
        TItem First(Func<TItem, bool> predicate);
        TItem FirstOrDefault();
        TItem FirstOrDefault(Func<TItem, bool> predicate);
    }
}
