using System.Collections.Generic;
using LinqAF.Impl;
using LinqAF.Config;

namespace LinqAF
{
    public static class DictionaryKeyCollectionExtensionMethods
    {
        // Any

        public static bool Any<TItem, TDictionaryValue>(this Dictionary<TItem, TDictionaryValue>.KeyCollection source)
        {
            if (source == null) throw CommonImplementation.ArgumentNull(nameof(source));

            return source.Count > 0;
        }

        // Concat

        public static IdentityEnumerable<TItem, Dictionary<TItem, TDictionaryValue>.KeyCollection, DictionaryKeysBridger<TItem, TDictionaryValue>, DictionaryKeysEnumerator<TItem, TDictionaryValue>> Concat<TItem, TDictionaryValue>(this Dictionary<TItem, TDictionaryValue>.KeyCollection first, EmptyEnumerable<TItem> second)
        {
            var firstBridge = CommonImplementation.Bridge(first, nameof(first));
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            return firstBridge;
        }

        public static IdentityEnumerable<TItem, Dictionary<TItem, TDictionaryValue>.KeyCollection, DictionaryKeysBridger<TItem, TDictionaryValue>, DictionaryKeysEnumerator<TItem, TDictionaryValue>> Concat<TItem, TDictionaryValue>(this Dictionary<TItem, TDictionaryValue>.KeyCollection first, EmptyOrderedEnumerable<TItem> second)
        {
            var firstBridge = CommonImplementation.Bridge(first, nameof(first));
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            return firstBridge;
        }
        
        // ToArray

        public static TItem[] ToArray<TItem, TDictionaryValue>(this Dictionary<TItem, TDictionaryValue>.KeyCollection source)
        {
            if (source == null) throw CommonImplementation.ArgumentNull(nameof(source));

            var ret = Allocator.Current.GetArray<TItem>(source.Count);
            source.CopyTo(ret, 0);
            return ret;
        }

        // ToList

        public static List<TItem> ToList<TItem, TDictionaryValue>(this Dictionary<TItem, TDictionaryValue>.KeyCollection source)
        {
            if (source == null) throw CommonImplementation.ArgumentNull(nameof(source));

            return Allocator.Current.GetPopulatedList(source);
        }
    }
}
