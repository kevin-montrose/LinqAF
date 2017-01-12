using System;
using System.Collections.Generic;

namespace LinqAF.Impl
{
    static partial class CommonImplementation
    {
        public static LookupEnumerable<TKey, TElement> ToLookup<TItem, TKey, TElement, TEnumerable, TEnumerator>(
            ref TEnumerable source, 
            Func<TItem, TKey> keySelector,
            Func<TItem, TElement> elementSelector,
            IEqualityComparer<TKey> comparer
        )
            where TEnumerable: struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator: struct, IStructEnumerator<TItem>
        {
            if (source.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(source));
            if (keySelector == null) throw new ArgumentNullException(nameof(keySelector));
            if (elementSelector == null) throw new ArgumentNullException(nameof(elementSelector));

            return ToLookupImpl<TItem, TKey, TElement, TEnumerable, TEnumerator>(ref source, keySelector, elementSelector, comparer);
        }

        internal static LookupEnumerable<TKey, TElement> ToLookupImpl<TItem, TKey, TElement, TEnumerable, TEnumerator>(
            ref TEnumerable source,
            Func<TItem, TKey> keySelector,
            Func<TItem, TElement> elementSelector,
            IEqualityComparer<TKey> comparer
        )
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            var mapper = new Dictionary<TKey, GroupingEnumerable<TKey, TElement>>(comparer ?? EqualityComparer<TKey>.Default);
            var keys = new List<TKey>();

            GroupingEnumerable<TKey, TElement>? nullValue = null;

            foreach (var item in source)
            {
                var key = keySelector(item);
                var element = elementSelector(item);

                // note: order of operations here is key, otherwise nullValue won't get updated
                //   aren't value types a blast!
                if (key == null)
                {
                    GroupingEnumerable<TKey, TElement> updated;
                    if (nullValue == null)
                    {
                        updated = new GroupingEnumerable<TKey, TElement>(default(TKey), new List<TElement>());
                        keys.Add(key);
                    }
                    else
                    {
                        updated = nullValue.Value;
                    }

                    updated.Inner.Add(element);
                    nullValue = updated;

                    continue;
                }

                // note: playing with fire here, updates to the actual values _in_ group will not be reflected
                //   but since Inner is a _reference_ we're OK to modify through it

                GroupingEnumerable<TKey, TElement> group;
                if (!mapper.TryGetValue(key, out group))
                {
                    mapper[key] = group = new GroupingEnumerable<TKey, TElement>(key, new List<TElement>());
                    keys.Add(key);
                }

                group.Inner.Add(element);
            }

            return new LookupEnumerable<TKey, TElement>(keys, mapper, nullValue);
        }
    }
}
