using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqAF.Impl
{
    internal interface IContains<TContainsItem>
    {
        bool Contains(TContainsItem value);
        bool Contains(TContainsItem value, IEqualityComparer<TContainsItem> comparer);
    }
}
