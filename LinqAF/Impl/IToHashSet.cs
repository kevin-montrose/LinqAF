using System.Collections.Generic;

namespace LinqAF.Impl
{
    interface IToHashSet<TItem>
    {
        HashSet<TItem> ToHashSet();
        HashSet<TItem> ToHashSet(IEqualityComparer<TItem> comparer);
    }
}
