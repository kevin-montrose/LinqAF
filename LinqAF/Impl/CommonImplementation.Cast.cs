using System;

namespace LinqAF.Impl
{
    static partial class CommonImplementation
    {
        public static CastEnumerable<TInItem, TOutItem, TEnumerable, TEnumerator> Cast<TInItem, TOutItem, TEnumerable, TEnumerator>(ref TEnumerable source)
            where TEnumerable : struct, IStructEnumerable<TInItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TInItem>
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return CastImpl<TInItem, TOutItem, TEnumerable, TEnumerator>(ref source);
        }

        internal static CastEnumerable<TInItem, TOutItem, TEnumerable, TEnumerator> CastImpl<TInItem, TOutItem, TEnumerable, TEnumerator>(ref TEnumerable source)
            where TEnumerable : struct, IStructEnumerable<TInItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TInItem>
        {
            return new CastEnumerable<TInItem, TOutItem, TEnumerable, TEnumerator>(ref source);
        }
    }
}
