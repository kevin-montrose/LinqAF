using System;
using System.Collections.Generic;

namespace LinqAF.Impl
{
    static partial class CommonImplementation
    {
        // Default
        public static DefaultIfEmptyDefaultEnumerable<TItem, TEnumerable, TEnumerator> DefaultIfEmpty<TItem, TEnumerable, TEnumerator>(ref TEnumerable source)
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return DefaultIfEmptyImpl<TItem, TEnumerable, TEnumerator>(ref source);
        }

        internal static DefaultIfEmptyDefaultEnumerable<TItem, TEnumerable, TEnumerator> DefaultIfEmptyImpl<TItem, TEnumerable, TEnumerator>(ref TEnumerable source)
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            return new DefaultIfEmptyDefaultEnumerable<TItem, TEnumerable, TEnumerator>(ref source);
        }

        // Specific
        public static DefaultIfEmptySpecificEnumerable<TItem, TEnumerable, TEnumerator> DefaultIfEmpty<TItem, TEnumerable, TEnumerator>(ref TEnumerable source, TItem defaultValue)
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return DefaultIfEmptyImpl<TItem, TEnumerable, TEnumerator>(ref source, defaultValue);
        }

        internal static DefaultIfEmptySpecificEnumerable<TItem, TEnumerable, TEnumerator> DefaultIfEmptyImpl<TItem, TEnumerable, TEnumerator>(ref TEnumerable source, TItem defaultValue)
           where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
           where TEnumerator : struct, IStructEnumerator<TItem>
        {
            return new DefaultIfEmptySpecificEnumerable<TItem, TEnumerable, TEnumerator>(ref source, defaultValue);
        }
    }
}