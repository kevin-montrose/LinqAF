using System;

namespace LinqAF.Impl
{
    static partial class CommonImplementation
    {
        // single
        public static TItem Single<TItem, TEnumerable, TEnumerator>(ref TEnumerable source)
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            if (source.IsDefaultValue()) throw new ArgumentException("Argument unintialized", nameof(source));

            return SingleImpl<TItem, TEnumerable, TEnumerator>(ref source);
        }

        internal static TItem SingleImpl<TItem, TEnumerable, TEnumerator>(ref TEnumerable source)
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            using (var i = source.GetEnumerator())
            {
                if (!i.MoveNext()) throw new InvalidOperationException("Sequence was empty");

                var ret = i.Current;

                if (i.MoveNext()) throw new InvalidOperationException("Sequence contained multiple elements");

                return ret;
            }
        }

        // predicate
        public static TItem Single<TItem, TEnumerable, TEnumerator>(ref TEnumerable source, Func<TItem, bool> predicate)
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            if (source.IsDefaultValue()) throw new ArgumentException("Argument unintialized", nameof(source));
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));

            return SingleImpl<TItem, TEnumerable, TEnumerator>(ref source, predicate);
        }

        internal static TItem SingleImpl<TItem, TEnumerable, TEnumerator>(ref TEnumerable source, Func<TItem, bool> predicate)
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            var ret = default(TItem);
            var found = false;

            foreach (var item in source)
            {
                if (predicate(item))
                {
                    if (found) throw new InvalidOperationException("Sequence contained multiple matching elements");

                    ret = item;
                    found = true;
                }
            }

            if (!found)
            {
                throw new InvalidOperationException($"No items matched {nameof(predicate)}");
            }

            return ret;
        }

        // default
        public static TItem SingleOrDefault<TItem, TEnumerable, TEnumerator>(ref TEnumerable source)
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            if (source.IsDefaultValue()) throw new ArgumentException("Argument unintialized", nameof(source));

            return SingleOrDefaultImpl<TItem, TEnumerable, TEnumerator>(ref source);
        }

        internal static TItem SingleOrDefaultImpl<TItem, TEnumerable, TEnumerator>(ref TEnumerable source)
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            using (var i = source.GetEnumerator())
            {
                if (!i.MoveNext()) return default(TItem);

                var ret = i.Current;

                if (i.MoveNext()) throw new InvalidOperationException("Sequence contained multiple elements");

                return ret;
            }
        }

        // default, predicate
        public static TItem SingleOrDefault<TItem, TEnumerable, TEnumerator>(ref TEnumerable source, Func<TItem, bool> predicate)
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            if (source.IsDefaultValue()) throw new ArgumentException("Argument unintialized", nameof(source));
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));

            return SingleOrDefaultImpl<TItem, TEnumerable, TEnumerator>(ref source, predicate);
        }

        internal static TItem SingleOrDefaultImpl<TItem, TEnumerable, TEnumerator>(ref TEnumerable source, Func<TItem, bool> predicate)
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            var ret = default(TItem);
            var found = false;

            foreach (var item in source)
            {
                if (predicate(item))
                {
                    if (found) throw new InvalidOperationException("Sequence contained multiple matching elements");

                    ret = item;
                    found = true;
                }
            }

            return ret;
        }
    }
}
