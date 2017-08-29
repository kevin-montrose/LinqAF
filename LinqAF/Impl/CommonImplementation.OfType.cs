using System;
namespace LinqAF.Impl
{
    static partial class CommonImplementation
    {
        public static OfTypeEnumerable<TInItem, TOutItem, TEnumerable, TEnumerator> OfType<TInItem, TOutItem, TEnumerable, TEnumerator>(ref TEnumerable source)
            where TEnumerable : struct, IStructEnumerable<TInItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TInItem>
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return OfTypeImpl<TInItem, TOutItem, TEnumerable, TEnumerator>(ref source);
        }

        internal static OfTypeEnumerable<TInItem, TOutItem, TEnumerable, TEnumerator> OfTypeImpl<TInItem, TOutItem, TEnumerable, TEnumerator>(ref TEnumerable source)
            where TEnumerable : struct, IStructEnumerable<TInItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TInItem>
        {
            return new OfTypeEnumerable<TInItem, TOutItem, TEnumerable, TEnumerator>(ref source);
        }
    }
}
