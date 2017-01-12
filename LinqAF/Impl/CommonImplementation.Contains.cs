using System;
using System.Collections.Generic;

namespace LinqAF.Impl
{
    static partial class CommonImplementation
    {
        // Default comparer
        public static bool Contains<TItem, TEnumerable, TEnumerator>(ref TEnumerable source, TItem value)
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            if (source.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(source));

            return ContainsImpl<TItem, TEnumerable, TEnumerator>(ref source, value);
        }

        internal static bool ContainsImpl<TItem, TEnumerable, TEnumerator>(ref TEnumerable source, TItem value)
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            // non-interface type here
            var comparer = EqualityComparer<TItem>.Default;

            foreach (var item in source)
            {
                if (comparer.Equals(item, value)) return true;
            }

            return false;
        }

        // Specific comparer
        public static bool Contains<TItem, TEnumerable, TEnumerator>(ref TEnumerable source, TItem value, IEqualityComparer<TItem> comparer)
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            if (comparer == null)
            {
                // defer to default, 'cause that'll dodge an interface
                return Contains<TItem, TEnumerable, TEnumerator>(ref source, value);
            }

            if (source.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(source));

            return ContainsImpl<TItem, TEnumerable, TEnumerator>(ref source, value, comparer);
        }

        internal static bool ContainsImpl<TItem, TEnumerable, TEnumerator>(ref TEnumerable source, TItem value, IEqualityComparer<TItem> comparer)
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            foreach (var item in source)
            {
                if (comparer.Equals(item, value)) return true;
            }

            return false;
        }
    }
}
