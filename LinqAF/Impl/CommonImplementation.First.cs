using System;
using System.Collections.Generic;

namespace LinqAF.Impl
{
    static partial class CommonImplementation
    {
        // no predicate
        public static TItem First<TItem, TEnumerable, TEnumerator>(ref TEnumerable source)
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return FirstImpl<TItem, TEnumerable, TEnumerator>(ref source);
        }

        internal static TItem FirstImpl<TItem, TEnumerable, TEnumerator>(ref TEnumerable source)
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            using (var i = source.GetEnumerator())
            {
                if (!i.MoveNext()) throw CommonImplementation.SequenceEmpty();

                return i.Current;
            }
        }

        // predicate
        public static TItem First<TItem, TEnumerable, TEnumerator>(ref TEnumerable source, Func<TItem, bool> predicate)
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));
            if (predicate == null) throw CommonImplementation.ArgumentNull(nameof(predicate));

            return FirstImpl<TItem, TEnumerable, TEnumerator>(ref source, predicate);
        }

        internal static TItem FirstImpl<TItem, TEnumerable, TEnumerator>(ref TEnumerable source, Func<TItem, bool> predicate)
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            foreach (var item in source)
            {
                if (predicate(item)) return item;
            }

            throw CommonImplementation.NoItemsMatched(nameof(predicate));
        }

        // default
        public static TItem FirstOrDefault<TItem, TEnumerable, TEnumerator>(ref TEnumerable source)
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return FirstOrDefaultImpl<TItem, TEnumerable, TEnumerator>(ref source);
        }

        internal static TItem FirstOrDefaultImpl<TItem, TEnumerable, TEnumerator>(ref TEnumerable source)
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            using (var i = source.GetEnumerator())
            {
                if (!i.MoveNext()) return default(TItem);

                return i.Current;
            }
        }

        // default, predicate
        public static TItem FirstOrDefault<TItem, TEnumerable, TEnumerator>(ref TEnumerable source, Func<TItem, bool> predicate)
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));
            if (predicate == null) throw CommonImplementation.ArgumentNull(nameof(predicate));

            return FirstOrDefaultImpl<TItem, TEnumerable, TEnumerator>(ref source, predicate);
        }

        internal static TItem FirstOrDefaultImpl<TItem, TEnumerable, TEnumerator>(ref TEnumerable source, Func<TItem, bool> predicate)
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            foreach (var item in source)
            {
                if (predicate(item)) return item;
            }

            return default(TItem);
        }
    }
}