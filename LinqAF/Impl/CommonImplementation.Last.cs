using System;
using System.Collections.Generic;

namespace LinqAF.Impl
{
    static partial class CommonImplementation
    {
        // default
        public static TItem Last<TItem, TEnumerable, TEnumerator>(ref TEnumerable source)
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            if (source.IsDefaultValue()) throw new ArgumentException("Argument unintialized", nameof(source));

            return LastImpl<TItem, TEnumerable, TEnumerator>(ref source);
        }

        internal static TItem LastImpl<TItem, TEnumerable, TEnumerator>(ref TEnumerable source)
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            using (var i = source.GetEnumerator())
            {
                if (!i.MoveNext()) throw new InvalidOperationException("Sequence was empty");

                var ret = i.Current;

                while (i.MoveNext())
                {
                    ret = i.Current;
                }

                return ret;
            }
        }

        // predicate
        public static TItem Last<TItem, TEnumerable, TEnumerator>(ref TEnumerable source, Func<TItem, bool> predicate)
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            if (source.IsDefaultValue()) throw new ArgumentException("Argument unintialized", nameof(source));
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));

            return LastImpl<TItem, TEnumerable, TEnumerator>(ref source, predicate);
        }

        internal static TItem LastImpl<TItem, TEnumerable, TEnumerator>(ref TEnumerable source, Func<TItem, bool> predicate)
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            var any = false;
            var ret = default(TItem);

            foreach (var item in source)
            {
                if (predicate(item))
                {
                    any = true;
                    ret = item;
                }
            }

            if (!any) throw new InvalidOperationException($"No items matched {nameof(predicate)}");

            return ret;
        }

        // default
        public static TItem LastOrDefault<TItem, TEnumerable, TEnumerator>(ref TEnumerable source)
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            if (source.IsDefaultValue()) throw new ArgumentException("Argument unintialized", nameof(source));

            return LastOrDefaultImpl<TItem, TEnumerable, TEnumerator>(ref source);
        }

        internal static TItem LastOrDefaultImpl<TItem, TEnumerable, TEnumerator>(ref TEnumerable source)
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            if (source.IsDefaultValue()) throw new ArgumentException("Argument unintialized", nameof(source));

            var ret = default(TItem);
            foreach (var item in source)
            {
                ret = item;
            }

            return ret;
        }

        // default, predicate
        public static TItem LastOrDefault<TItem, TEnumerable, TEnumerator>(ref TEnumerable source, Func<TItem, bool> predicate)
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            if (source.IsDefaultValue()) throw new ArgumentException("Argument unintialized", nameof(source));
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));

            return LastOrDefaultImpl<TItem, TEnumerable, TEnumerator>(ref source, predicate);
        }

        internal static TItem LastOrDefaultImpl<TItem, TEnumerable, TEnumerator>(ref TEnumerable source, Func<TItem, bool> predicate)
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            var ret = default(TItem);
            foreach (var item in source)
            {
                if (predicate(item))
                {
                    ret = item;
                }
            }

            return ret;
        }
    }
}