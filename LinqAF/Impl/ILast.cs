using System;

namespace LinqAF.Impl
{
    interface ILast<TItem>
    {
        TItem Last();
        TItem Last(Func<TItem, bool> predicate);
        TItem LastOrDefault();
        TItem LastOrDefault(Func<TItem, bool> predicate);
    }
}
