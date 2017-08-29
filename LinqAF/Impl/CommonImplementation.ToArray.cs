using LinqAF.Config;
using System;

namespace LinqAF.Impl
{
    static partial class CommonImplementation
    {
        const int TO_ARRAY_INITIAL_SIZE = 4;
        
        public static TItem[] ToArray<TItem, TEnumerable, TEnumerator>(ref TEnumerable source)
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return ToArrayImpl<TItem, TEnumerable, TEnumerator>(ref source);
        }

        internal static TItem[] ToArrayImpl<TItem, TEnumerable, TEnumerator>(ref TEnumerable source)
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            var ret = Allocator.Current.GetArray<TItem>(TO_ARRAY_INITIAL_SIZE);
            var ix = 0;
            foreach(var item in source)
            {
                if(ix == ret.Length)
                {
                    Allocator.Current.ResizeArray(ref ret, NextSize(ret.Length));
                }

                ret[ix] = item;
                ix++;
            }

            if(ret.Length != ix)
            {
                Allocator.Current.ResizeArray(ref ret, ix);
            }

            return ret;
        }
    }
}
