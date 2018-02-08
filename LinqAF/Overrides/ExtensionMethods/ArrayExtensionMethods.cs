using LinqAF.Config;
using LinqAF.Impl;
using System.Collections.Generic;

namespace LinqAF
{
    public static class ArrayExtensionMethods
    {
        // Any

        public static bool Any<TItem>(this TItem[] source)
        {
            if (source == null) throw CommonImplementation.ArgumentNull(nameof(source));

            return source.Length > 0;
        }

        // Concat

        public static IdentityEnumerable<TItem, TItem[], ArrayBridger<TItem>, ArrayEnumerator<TItem>> Concat<TItem>(this TItem[] first, EmptyEnumerable<TItem> second)
        {
            var firstBridge = CommonImplementation.Bridge(first, nameof(first));
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            return firstBridge;
        }

        public static IdentityEnumerable<TItem, TItem[], ArrayBridger<TItem>, ArrayEnumerator<TItem>> Concat<TItem>(this TItem[] first, EmptyOrderedEnumerable<TItem> second)
        {
            var firstBridge = CommonImplementation.Bridge(first, nameof(first));
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            return firstBridge;
        }

        // First

        public static TItem First<TItem>(this TItem[] source)
        {
            if (source == null) throw CommonImplementation.ArgumentNull(nameof(source));
            if (source.Length < 1) throw CommonImplementation.SequenceEmpty();

            return source[0];
        }
        
        public static TItem FirstOrDefault<TItem>(this TItem[] source)
        {
            if (source == null) throw CommonImplementation.ArgumentNull(nameof(source));
            if (source.Length < 1) return default(TItem);

            return source[0];
        }

        // Last

        public static TItem Last<TItem>(this TItem[] source)
        {
            if (source == null) throw CommonImplementation.ArgumentNull(nameof(source));
            if (source.Length < 1) throw CommonImplementation.SequenceEmpty();

            return source[source.Length - 1];
        }
        
        public static TItem LastOrDefault<TItem>(this TItem[] source)
        {
            if (source == null) throw CommonImplementation.ArgumentNull(nameof(source));
            if (source.Length < 1) return default(TItem);

            return source[source.Length - 1];
        }

        // Single

        public static TItem Single<TItem>(this TItem[] source)
        {
            if (source == null) throw CommonImplementation.ArgumentNull(nameof(source));
            if (source.Length == 0) throw CommonImplementation.SequenceEmpty();
            if (source.Length > 1) throw CommonImplementation.MultipleElements();

            return source[0];
        }

        public static TItem SingleOrDefault<TItem>(this TItem[] source)
        {
            if (source == null) throw CommonImplementation.ArgumentNull(nameof(source));
            if (source.Length > 1) throw CommonImplementation.MultipleElements();
            if (source.Length == 0) return default(TItem);
            
            return source[0];
        }

        // ToArray

        public static TItem[] ToArray<TItem>(this TItem[] source)
        {
            if (source == null) throw CommonImplementation.ArgumentNull(nameof(source));
            
            var ret = Allocator.Current.GetArray<TItem>(source.Length);
            source.CopyTo(ret, 0);
            return ret;
        }

        // ToList

        public static List<TItem> ToList<TItem>(this TItem[] source)
        {
            if (source == null) throw CommonImplementation.ArgumentNull(nameof(source));

            return Allocator.Current.GetPopulatedList(source);
        }
    }
}
