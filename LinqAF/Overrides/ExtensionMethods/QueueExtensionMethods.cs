using System.Collections.Generic;
using LinqAF.Impl;
using LinqAF.Config;

namespace LinqAF
{
    public static class QueueExtensionMethods
    {
        // Any

        public static bool Any<TItem>(this Queue<TItem> source)
        {
            if (source == null) throw CommonImplementation.ArgumentNull(nameof(source));

            return source.Count > 0;
        }

        // Concat

        public static IdentityEnumerable<TItem, Queue<TItem>, QueueBridger<TItem>, QueueEnumerator<TItem>> Concat<TItem>(this Queue<TItem> first, EmptyEnumerable<TItem> second)
        {
            var firstBridge = CommonImplementation.Bridge(first, nameof(first));
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            return firstBridge;
        }

        public static IdentityEnumerable<TItem, Queue<TItem>, QueueBridger<TItem>, QueueEnumerator<TItem>> Concat<TItem>(this Queue<TItem> first, EmptyOrderedEnumerable<TItem> second)
        {
            var firstBridge = CommonImplementation.Bridge(first, nameof(first));
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            return firstBridge;
        }

        // Contains

        public static bool Contains<TItem>(this Queue<TItem> source, TItem value)
        {
            if (source == null) CommonImplementation.ArgumentNull(nameof(source));

            return source.Contains(value);
        }

        // First

        public static TItem First<TItem>(this Queue<TItem> source)
        {
            if (source == null) throw CommonImplementation.ArgumentNull(nameof(source));
            if (source.Count < 1) throw CommonImplementation.SequenceEmpty();

            return source.Peek();
        }

        public static TItem FirstOrDefault<TItem>(this Queue<TItem> source)
        {
            if (source == null) throw CommonImplementation.ArgumentNull(nameof(source));
            if (source.Count < 1) return default(TItem);

            return source.Peek();
        }
        
        // Single

        public static TItem Single<TItem>(this Queue<TItem> source)
        {
            if (source == null) throw CommonImplementation.ArgumentNull(nameof(source));
            if (source.Count == 0) throw CommonImplementation.SequenceEmpty();
            if (source.Count > 1) throw CommonImplementation.MultipleElements();

            return source.Peek();
        }

        public static TItem SingleOrDefault<TItem>(this Queue<TItem> source)
        {
            if (source == null) throw CommonImplementation.ArgumentNull(nameof(source));
            if (source.Count > 1) throw CommonImplementation.MultipleElements();
            if (source.Count == 0) return default(TItem);

            return source.Peek();
        }

        // ToArray

        public static TItem[] ToArray<TItem>(this Queue<TItem> source)
        {
            if (source == null) throw CommonImplementation.ArgumentNull(nameof(source));

            var ret = Allocator.Current.GetArray<TItem>(source.Count);
            source.CopyTo(ret, 0);
            return ret;
        }

        // ToList

        public static List<TItem> ToList<TItem>(this Queue<TItem> source)
        {
            if (source == null) throw CommonImplementation.ArgumentNull(nameof(source));

            return Allocator.Current.GetPopulatedList(source);
        }
    }
}