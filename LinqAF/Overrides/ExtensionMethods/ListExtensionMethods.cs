using System.Collections.Generic;
using LinqAF.Impl;
using LinqAF.Config;

namespace LinqAF
{
    public static class ListExtensionMethods
    {
        // Any

        public static bool Any<TItem>(this List<TItem> source)
        {
            if (source == null) throw CommonImplementation.ArgumentNull(nameof(source));

            return source.Count > 0;
        }

        // Concat

        public static IdentityEnumerable<TItem, List<TItem>, ListBridger<TItem>, ListEnumerator<TItem>> Concat<TItem>(this List<TItem> first, EmptyEnumerable<TItem> second)
        {
            var firstBridge = CommonImplementation.Bridge(first, nameof(first));
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            return firstBridge;
        }

        public static IdentityEnumerable<TItem, List<TItem>, ListBridger<TItem>, ListEnumerator<TItem>> Concat<TItem>(this List<TItem> first, EmptyOrderedEnumerable<TItem> second)
        {
            var firstBridge = CommonImplementation.Bridge(first, nameof(first));
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            return firstBridge;
        }

        // Contains

        public static bool Contains<TItem>(this List<TItem> source, TItem value)
        {
            if (source == null) CommonImplementation.ArgumentNull(nameof(source));

            return source.Contains(value);
        }

        // First

        public static TItem First<TItem>(this List<TItem> source)
        {
            if (source == null) throw CommonImplementation.ArgumentNull(nameof(source));
            if (source.Count < 1) throw CommonImplementation.SequenceEmpty();

            return source[0];
        }

        public static TItem FirstOrDefault<TItem>(this List<TItem> source)
        {
            if (source == null) throw CommonImplementation.ArgumentNull(nameof(source));
            if (source.Count < 1) return default(TItem);

            return source[0];
        }

        // Last

        public static TItem Last<TItem>(this List<TItem> source)
        {
            if (source == null) throw CommonImplementation.ArgumentNull(nameof(source));
            if (source.Count < 1) throw CommonImplementation.SequenceEmpty();

            return source[source.Count - 1];
        }

        public static TItem LastOrDefault<TItem>(this List<TItem> source)
        {
            if (source == null) throw CommonImplementation.ArgumentNull(nameof(source));
            if (source.Count < 1) return default(TItem);

            return source[source.Count - 1];
        }

        // Single

        public static TItem Single<TItem>(this List<TItem> source)
        {
            if (source == null) throw CommonImplementation.ArgumentNull(nameof(source));
            if (source.Count == 0) throw CommonImplementation.SequenceEmpty();
            if (source.Count > 1) throw CommonImplementation.MultipleElements();

            return source[0];
        }

        public static TItem SingleOrDefault<TItem>(this List<TItem> source)
        {
            if (source == null) throw CommonImplementation.ArgumentNull(nameof(source));
            if (source.Count > 1) throw CommonImplementation.MultipleElements();
            if (source.Count == 0) return default(TItem);

            return source[0];
        }

        // ToArray

        public static TItem[] ToArray<TItem>(this List<TItem> source)
        {
            if (source == null) throw CommonImplementation.ArgumentNull(nameof(source));

            var ret = Allocator.Current.GetArray<TItem>(source.Count);
            source.CopyTo(ret, 0);
            return ret;
        }

        // ToList

        public static List<TItem> ToList<TItem>(this List<TItem> source)
        {
            if (source == null) throw CommonImplementation.ArgumentNull(nameof(source));

            return Allocator.Current.GetPopulatedList(source);
        }
    }
}