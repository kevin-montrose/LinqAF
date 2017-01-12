using System.Collections.Generic;

namespace LinqAF.Impl
{
    interface IToList<TItem>
    {
        List<TItem> ToList();
    }
}
