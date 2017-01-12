using LinqAF.Impl;
using System;
using System.Collections.Generic;

namespace LinqAF
{
    public struct LookupEnumerator<TKey, TElement>:
        IStructEnumerator<GroupingEnumerable<TKey, TElement>>
    {
        public GroupingEnumerable<TKey, TElement> Current { get; private set; }

        ListEnumerator<TKey> Keys;
        GroupingEnumerable<TKey, TElement> NullValue;
        Dictionary<TKey, GroupingEnumerable<TKey, TElement>> Lookup;
        
        internal LookupEnumerator(ref ListEnumerator<TKey> keys, Dictionary<TKey, GroupingEnumerable<TKey, TElement>> lookup, ref GroupingEnumerable<TKey, TElement> nullValue)
        {
            Keys = keys;
            Lookup = lookup;
            Current = default(GroupingEnumerable<TKey, TElement>);
            NullValue = nullValue;
        }

        public bool IsDefaultValue() => Lookup == null;

        public void Dispose()
        {
            Keys.Dispose();
            NullValue = default(GroupingEnumerable<TKey, TElement>);
            Lookup = null;
        }

        public bool MoveNext()
        {
            if (!Keys.MoveNext())
            {
                return false;
            }

            var key = Keys.Current;
            if(key == null)
            {
                Current = NullValue;
                return true;
            }

            Current = Lookup[key];
            return true;
        }

        public void Reset()
        {
            Keys.Reset();
            Current = default(GroupingEnumerable<TKey, TElement>);
        }
    }

    public partial struct LookupEnumerable<TKey, TElement>:
        IStructEnumerable<GroupingEnumerable<TKey, TElement>, LookupEnumerator<TKey, TElement>>
    {
        public int Count => InnerLookup.Count + (NullPresent ? 1 : 0);

        bool NullPresent;
        GroupingEnumerable<TKey, TElement> NullValue;
        List<TKey> Keys;
        Dictionary<TKey, GroupingEnumerable<TKey, TElement>> InnerLookup;

        public GroupingEnumerable<TKey, TElement> this[TKey key]
        {
            get
            {
                if (key == null)
                {
                    return NullValue;
                }

                GroupingEnumerable<TKey, TElement> ret;
                if (!InnerLookup.TryGetValue(key, out ret))
                {
                    ret = EmptyCache<TKey, TElement>.EmptyGrouping;
                }

                return ret;
            }
        }

        internal LookupEnumerable(List<TKey> keys, Dictionary<TKey, GroupingEnumerable<TKey, TElement>> lookup, GroupingEnumerable<TKey, TElement>? nullValue)
        {
            Keys = keys;
            NullPresent = nullValue.HasValue;
            InnerLookup = lookup;
            NullValue = nullValue.HasValue ? nullValue.Value : EmptyCache<TKey, TElement>.EmptyGrouping;
        }

        public bool IsDefaultValue()
        {
            return InnerLookup == null;
        }

        public bool Contains(TKey key)
        {
            if (key == null) return NullPresent;

            return InnerLookup.ContainsKey(key);
        }

        public LookupEnumerator<TKey, TElement> GetEnumerator()
        {
            var k = new ListEnumerator<TKey>(Keys);
            return new LookupEnumerator<TKey, TElement>(ref k, InnerLookup, ref NullValue);
        }
    }
}
