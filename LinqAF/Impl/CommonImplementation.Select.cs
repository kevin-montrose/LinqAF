using System;

namespace LinqAF.Impl
{
    static partial class CommonImplementation
    {
        public static SelectEnumerable<TInItem, TOutItem, TInnerEnumerable, TInnerEnumerator> Select<TInItem, TOutItem, TInnerEnumerable, TInnerEnumerator>(ref TInnerEnumerable source, Func<TInItem, TOutItem> selector)
            where TInnerEnumerable : struct, IStructEnumerable<TInItem, TInnerEnumerator>
            where TInnerEnumerator : struct, IStructEnumerator<TInItem>
        {
            if (source.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(source));
            if (selector == null) throw new ArgumentNullException(nameof(selector));

            return SelectImpl<TInItem, TOutItem, TInnerEnumerable, TInnerEnumerator>(ref source, selector);
        }

        internal static SelectEnumerable<TInItem, TOutItem, TInnerEnumerable, TInnerEnumerator> SelectImpl<TInItem, TOutItem, TInnerEnumerable, TInnerEnumerator>(ref TInnerEnumerable source, Func<TInItem, TOutItem> selector)
            where TInnerEnumerable : struct, IStructEnumerable<TInItem, TInnerEnumerator>
            where TInnerEnumerator : struct, IStructEnumerator<TInItem>
        {
            return new SelectEnumerable<TInItem, TOutItem, TInnerEnumerable, TInnerEnumerator>(ref source, selector);
        }

        public static SelectIndexedEnumerable<TInItem, TOutItem, TInnerEnumerable, TInnerEnumerator> Select<TInItem, TOutItem, TInnerEnumerable, TInnerEnumerator>(ref TInnerEnumerable source, Func<TInItem, int, TOutItem> selector)
            where TInnerEnumerable : struct, IStructEnumerable<TInItem, TInnerEnumerator>
            where TInnerEnumerator : struct, IStructEnumerator<TInItem>
        {
            if (source.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(source));
            if (selector == null) throw new ArgumentNullException(nameof(selector));

            return SelectImpl<TInItem, TOutItem, TInnerEnumerable, TInnerEnumerator>(ref source, selector);
        }

        internal static SelectIndexedEnumerable<TInItem, TOutItem, TInnerEnumerable, TInnerEnumerator> SelectImpl<TInItem, TOutItem, TInnerEnumerable, TInnerEnumerator>(ref TInnerEnumerable source, Func<TInItem, int, TOutItem> selector)
            where TInnerEnumerable : struct, IStructEnumerable<TInItem, TInnerEnumerator>
            where TInnerEnumerator : struct, IStructEnumerator<TInItem>
        {
            return new SelectIndexedEnumerable<TInItem, TOutItem, TInnerEnumerable, TInnerEnumerator>(ref source, selector);
        }
    }
}
