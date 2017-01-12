using System.Collections.Generic;

namespace LinqAF.Impl
{
    interface IAsEnumerable<TItem>
    {
        IEnumerable<TItem> AsEnumerable();
    }
}
