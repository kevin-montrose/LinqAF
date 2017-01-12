using System;
using System.Collections.Generic;

namespace LinqAF.Impl
{
    static partial class CommonImplementation
    {
        public static void Reset<TItem, TEnumerator>(ref TEnumerator e)
            where TEnumerator: IEnumerator<TItem>
        {
            e.Reset();
        }
    }
}
