using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqAF.Impl
{
    interface IOfType<TInItem, TThisEnumerable, TThisEnumerator>
        where TThisEnumerable : struct, IStructEnumerable<TInItem, TThisEnumerator>
        where TThisEnumerator : struct, IStructEnumerator<TInItem>
    {
        OfTypeEnumerable<TInItem, TOutItem, TThisEnumerable, TThisEnumerator> OfType<TOutItem>();
    }
}
