using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqAF.Impl
{
    static partial class CommonImplementation
    {
        public static List<TItem> ToList<TItem, TEnumerable, TEnumerator>(ref TEnumerable source)
            where TEnumerable: struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator: struct, IStructEnumerator<TItem>
        {
            if (source.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(source));

            return ToListImpl<TItem, TEnumerable, TEnumerator>(ref source);
        }

        internal static List<TItem> ToListImpl<TItem, TEnumerable, TEnumerator>(ref TEnumerable source)
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            var ret = new List<TItem>();
            foreach (var item in source)
            {
                ret.Add(item);
            }

            return ret;
        }
    }
}
