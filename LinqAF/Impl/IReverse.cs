using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqAF.Impl
{
    interface IReverse<TItem, TEnumerable, TEnumerator>
        where TEnumerable: struct, IStructEnumerable<TItem, TEnumerator>
        where TEnumerator: struct, IStructEnumerator<TItem>
    {
        ReverseEnumerable<TItem, TEnumerable, TEnumerator> Reverse();
    }
}
