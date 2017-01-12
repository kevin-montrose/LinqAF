using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqAF.Impl
{
    internal interface IElementAt<TItem>
    {
        TItem ElementAt(int index);
        TItem ElementAtOrDefault(int index);
    }
}
