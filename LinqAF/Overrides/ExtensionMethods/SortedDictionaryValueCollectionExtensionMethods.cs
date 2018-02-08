using System.Collections.Generic;
using LinqAF.Impl;
using LinqAF.Config;

namespace LinqAF
{
    public static class SortedDictionaryValueCollectionExtensionMethods
    {

        // Any

        public static bool Any<TItem, TDictionaryKey>(this SortedDictionary<TDictionaryKey, TItem>.ValueCollection source)
        {
            if (source == null) throw CommonImplementation.ArgumentNull(nameof(source));

            return source.Count > 0;
        }

        // Concat

        public static IdentityEnumerable<TItem, SortedDictionary<TDictionaryKey, TItem>.ValueCollection, SortedDictionaryValuesBridger<TDictionaryKey, TItem>, SortedDictionaryValuesEnumerator<TDictionaryKey, TItem>> Concat<TItem, TDictionaryKey>(this SortedDictionary<TDictionaryKey, TItem>.ValueCollection first, EmptyEnumerable<TItem> second)
        {
            var firstBridge = CommonImplementation.Bridge(first, nameof(first));
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            return firstBridge;
        }

        public static IdentityEnumerable<TItem, SortedDictionary<TDictionaryKey, TItem>.ValueCollection, SortedDictionaryValuesBridger<TDictionaryKey, TItem>, SortedDictionaryValuesEnumerator<TDictionaryKey, TItem>> Concat<TItem, TDictionaryKey>(this SortedDictionary<TDictionaryKey, TItem>.ValueCollection first, EmptyOrderedEnumerable<TItem> second)
        {
            var firstBridge = CommonImplementation.Bridge(first, nameof(first));
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            return firstBridge;
        }
        
        // ToArray

        public static TItem[] ToArray<TItem, TDictionaryKey>(this SortedDictionary<TDictionaryKey, TItem>.ValueCollection source)
        {
            if (source == null) throw CommonImplementation.ArgumentNull(nameof(source));

            var ret = Allocator.Current.GetArray<TItem>(source.Count);
            source.CopyTo(ret, 0);
            return ret;
        }

        // ToList

        public static List<TItem> ToList<TItem, TDictionaryKey>(this SortedDictionary<TDictionaryKey, TItem>.ValueCollection source)
        {
            if (source == null) throw CommonImplementation.ArgumentNull(nameof(source));

            return Allocator.Current.GetPopulatedList(source);
        }
    }
}