using System;

namespace LinqAF.Impl
{
    interface IAny<TAnyItem>
    {
        bool Any();
        bool Any(Func<TAnyItem, bool> predicate);
    }
}
