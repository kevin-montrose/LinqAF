using System;
using System.Collections.Generic;

namespace LinqAF.Impl
{
    static partial class CommonImplementation
    {
        const int TO_ARRAY_INITIAL_SIZE = 16;
        const int TO_ARRAY_MAX_DOUBLE_SIZE = 4096;

        public static TItem[] ToArray<TItem, TEnumerable, TEnumerator>(ref TEnumerable source)
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            if (source.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(source));

            return ToArrayImpl<TItem, TEnumerable, TEnumerator>(ref source);
        }

        internal static TItem[] ToArrayImpl<TItem, TEnumerable, TEnumerator>(ref TEnumerable source)
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            var ret = new TItem[TO_ARRAY_INITIAL_SIZE];
            var ix = 0;
            foreach(var item in source)
            {
                if(ix == ret.Length)
                {
                    Array.Resize(ref ret, ToArray_NextSize(ret.Length));
                }

                ret[ix] = item;
                ix++;
            }

            if(ret.Length != ix)
            {
                Array.Resize(ref ret, ix);
            }

            return ret;
        }

        static int ToArray_NextSize(int curSize)
        {
            if(curSize < TO_ARRAY_MAX_DOUBLE_SIZE)
            {
                return curSize * 2;
            }

            return curSize + TO_ARRAY_MAX_DOUBLE_SIZE;
        }
    }
}
