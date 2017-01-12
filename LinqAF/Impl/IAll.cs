using System;

namespace LinqAF.Impl
{
    interface IAll<TAllItem>
    {
        bool All(Func<TAllItem, bool> predicate);
    }
}
