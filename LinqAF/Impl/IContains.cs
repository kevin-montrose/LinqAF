using System.Collections.Generic;

namespace LinqAF.Impl
{
    internal interface IContains<TContainsItem>
    {
        bool Contains(TContainsItem value);
        bool Contains(TContainsItem value, IEqualityComparer<TContainsItem> comparer);
    }
}
