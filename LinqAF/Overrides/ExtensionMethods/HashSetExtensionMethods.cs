using System.Collections.Generic;
using LinqAF.Impl;
using LinqAF.Config;

namespace LinqAF
{
    public static class HashSetExtensionMethods
    {
        // Any

        public static bool Any<TItem>(this HashSet<TItem> source)
        {
            if (source == null) throw CommonImplementation.ArgumentNull(nameof(source));

            return source.Count > 0;
        }

        // Concat

        public static IdentityEnumerable<TItem, HashSet<TItem>, HashSetBridger<TItem>, HashSetEnumerator<TItem>> Concat<TItem>(this HashSet<TItem> first, EmptyEnumerable<TItem> second)
        {
            var firstBridge = CommonImplementation.Bridge(first, nameof(first));
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            return firstBridge;
        }

        public static IdentityEnumerable<TItem, HashSet<TItem>, HashSetBridger<TItem>, HashSetEnumerator<TItem>> Concat<TItem>(this HashSet<TItem> first, EmptyOrderedEnumerable<TItem> second)
        {
            var firstBridge = CommonImplementation.Bridge(first, nameof(first));
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            return firstBridge;
        }

        // Contains
        
        public static bool Contains<TItem>(this HashSet<TItem> source, TItem value)
        {
            if (source == null) CommonImplementation.ArgumentNull(nameof(source));

            return source.Contains(value);
        }

        public static bool Contains<TItem>(this HashSet<TItem> source, TItem value, IEqualityComparer<TItem> comparer)
        {
            if (source == null) CommonImplementation.ArgumentNull(nameof(source));

            comparer = comparer ?? EqualityComparer<TItem>.Default;
            var sameComparer = object.ReferenceEquals(source.Comparer, comparer);

            if (sameComparer) return source.Contains(value);

            // no need for bridge call, known to be non-null
            var bridge = new IdentityEnumerable<TItem, HashSet<TItem>, HashSetBridger<TItem>, HashSetEnumerator<TItem>>(source);
            if (comparer == null)
            {
                return CommonImplementation.ContainsImpl<TItem, IdentityEnumerable<TItem, HashSet<TItem>, HashSetBridger<TItem>, HashSetEnumerator<TItem>>, HashSetEnumerator<TItem>>(ref bridge, value);
            }

            return CommonImplementation.ContainsImpl<TItem, IdentityEnumerable<TItem, HashSet<TItem>, HashSetBridger<TItem>, HashSetEnumerator<TItem>>, HashSetEnumerator<TItem>>(ref bridge, value, comparer);
        }

        // ToArray

        public static TItem[] ToArray<TItem>(this HashSet<TItem> source)
        {
            if (source == null) throw CommonImplementation.ArgumentNull(nameof(source));

            var ret = Allocator.Current.GetArray<TItem>(source.Count);
            source.CopyTo(ret, 0);
            return ret;
        }

        // ToList

        public static List<TItem> ToList<TItem>(this HashSet<TItem> source)
        {
            if (source == null) throw CommonImplementation.ArgumentNull(nameof(source));

            return Allocator.Current.GetPopulatedList(source);
        }
    }
}