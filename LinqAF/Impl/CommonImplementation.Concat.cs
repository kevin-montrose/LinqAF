using System;

namespace LinqAF.Impl
{
    static partial class CommonImplementation
    {
        public static ConcatEnumerable<TItem, TFirstEnumerable, TFirstEnumerator, TSecondEnumerable, TSecondEnumerator> Concat<TItem, TFirstEnumerable, TFirstEnumerator, TSecondEnumerable, TSecondEnumerator>(ref TFirstEnumerable first, ref TSecondEnumerable second)
            where TFirstEnumerable : struct, IStructEnumerable<TItem, TFirstEnumerator>
            where TFirstEnumerator : struct, IStructEnumerator<TItem>
            where TSecondEnumerable : struct, IStructEnumerable<TItem, TSecondEnumerator>
            where TSecondEnumerator : struct, IStructEnumerator<TItem>
        {
            if (first.IsDefaultValue()) throw new ArgumentException("Argument unintialized", nameof(first));
            if (second.IsDefaultValue()) throw new ArgumentException("Argument unintialized", nameof(second));

            return ConcatImpl<TItem, TFirstEnumerable, TFirstEnumerator, TSecondEnumerable, TSecondEnumerator>(ref first, ref second);
        }

        internal static ConcatEnumerable<TItem, TFirstEnumerable, TFirstEnumerator, TSecondEnumerable, TSecondEnumerator> ConcatImpl<TItem, TFirstEnumerable, TFirstEnumerator, TSecondEnumerable, TSecondEnumerator>(ref TFirstEnumerable first, ref TSecondEnumerable second)
            where TFirstEnumerable : struct, IStructEnumerable<TItem, TFirstEnumerator>
            where TFirstEnumerator : struct, IStructEnumerator<TItem>
            where TSecondEnumerable : struct, IStructEnumerable<TItem, TSecondEnumerator>
            where TSecondEnumerator : struct, IStructEnumerator<TItem>
        {
            return new ConcatEnumerable<TItem, TFirstEnumerable, TFirstEnumerator, TSecondEnumerable, TSecondEnumerator>(ref first, ref second);
        }
    }
}
