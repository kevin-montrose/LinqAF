using System;
using System.Collections.Generic;

namespace LinqAF
{
    public partial struct TakeEnumerable<TItem, TInnerEnumerable, TInnerEnumerator>
    {
        public TakeEnumerable<TItem, TInnerEnumerable, TInnerEnumerator> Take(int count)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument unintialized", "source");

            if (count < 0)
            {
                count = 0;
            }

            if (count >= TakeCount) return this;

            return new TakeEnumerable<TItem, TInnerEnumerable, TInnerEnumerator>(ref Inner, count);
        }

        public List<TItem> ToList()
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument unintialized", "source");

            var ret = new List<TItem>(this.TakeCount);
            foreach(var item in this)
            {
                ret.Add(item);
            }

            return ret;
        }

        public TItem[] ToArray()
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument unintialized", "source");

            var ret = new TItem[this.TakeCount];
            var ix = 0;
            foreach(var item in this)
            {
                ret[ix] = item;
                ix++;
            }

            if(ix != ret.Length)
            {
                Array.Resize(ref ret, ix);
            }

            return ret;
        }

        public Dictionary<TToDictionary_Key, TItem> ToDictionary<TToDictionary_Key>(Func<TItem, TToDictionary_Key> keySelector)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "source");
            if (keySelector == null) throw new ArgumentNullException(nameof(keySelector));

            var ret = new Dictionary<TToDictionary_Key, TItem>(this.TakeCount);
            foreach (var item in this)
            {
                ret.Add(keySelector(item), item);
            }
            return ret;
        }

        public Dictionary<TToDictionary_Key, TToDictionary_Value> ToDictionary<TToDictionary_Key, TToDictionary_Value>(Func<TItem, TToDictionary_Key> keySelector, Func<TItem, TToDictionary_Value> elementSelector)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "source");
            if (keySelector == null) throw new ArgumentNullException(nameof(keySelector));
            if (elementSelector == null) throw new ArgumentNullException(nameof(elementSelector));

            var ret = new Dictionary<TToDictionary_Key, TToDictionary_Value>(this.TakeCount);
            foreach (var item in this)
            {
                ret.Add(keySelector(item), elementSelector(item));
            }
            return ret;
        }

        public Dictionary<TToDictionary_Key, TItem> ToDictionary<TToDictionary_Key>(Func<TItem, TToDictionary_Key> keySelector, IEqualityComparer<TToDictionary_Key> comparer)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "source");
            if (keySelector == null) throw new ArgumentNullException(nameof(keySelector));

            var ret = new Dictionary<TToDictionary_Key, TItem>(this.TakeCount, comparer);
            foreach (var item in this)
            {
                ret.Add(keySelector(item), item);
            }
            return ret;
        }

        public Dictionary<TToDictionary_Key, TToDictionary_Value> ToDictionary<TToDictionary_Key, TToDictionary_Value>(Func<TItem, TToDictionary_Key> keySelector, Func<TItem, TToDictionary_Value> elementSelector, IEqualityComparer<TToDictionary_Key> comparer)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "source");
            if (keySelector == null) throw new ArgumentNullException(nameof(keySelector));
            if (elementSelector == null) throw new ArgumentNullException(nameof(elementSelector));

            var ret = new Dictionary<TToDictionary_Key, TToDictionary_Value>(this.TakeCount, comparer);
            foreach (var item in this)
            {
                ret.Add(keySelector(item), elementSelector(item));
            }
            return ret;
        }
    }
}