using LinqAF.Config;
using LinqAF.Impl;
using System;
using System.Collections.Generic;

namespace LinqAF
{
    public partial struct OneItemSpecificOrderedEnumerable<TItem>
    {
        public bool All(Func<TItem, bool> predicate)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");
            if (predicate == null) throw CommonImplementation.ArgumentNull(nameof(predicate));

            return predicate(Item);
        }

        public bool Any()
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");

            return true;
        }

        public bool Contains(TItem value)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");

            return EqualityComparer<TItem>.Default.Equals(Item, value);
        }

        public bool Contains(TItem value, IEqualityComparer<TItem> comparer)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");

            if (comparer == null)
            {
                // dodge an interface
                return EqualityComparer<TItem>.Default.Equals(Item, value);
            }

            return comparer.Equals(Item, value);
        }

        public int Count()
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");

            return 1;
        }

        public int Count(Func<TItem, bool> predicate)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");
            if (predicate == null) throw CommonImplementation.ArgumentNull(nameof(predicate));

            return predicate(Item) ? 1 : 0;
        }

        public long LongCount()
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");

            return 1;
        }

        public long LongCount(Func<TItem, bool> predicate)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");
            if (predicate == null) throw CommonImplementation.ArgumentNull(nameof(predicate));

            return predicate(Item) ? 1 : 0;
        }

        public OneItemSpecificEnumerable<TItem> DefaultIfEmpty()
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");

            return new OneItemSpecificEnumerable<TItem>(Enumerable.OneItemSigil, Item);
        }

        public OneItemSpecificEnumerable<TItem> DefaultIfEmpty(TItem item)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");

            return new OneItemSpecificEnumerable<TItem>(Enumerable.OneItemSigil, Item);
        }

        public TItem ElementAt(int index)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");
            if (index < 0 || index > 1) throw CommonImplementation.OutOfRange(nameof(index));

            return Item;
        }

        public TItem ElementAtOrDefault(int index)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");

            if (index == 0) return Item;

            return default(TItem);
        }

        public TItem First()
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");

            return Item;
        }

        public TItem First(Func<TItem, bool> predicate)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");
            if (predicate == null) throw CommonImplementation.ArgumentNull(nameof(predicate));
            if (!predicate(Item)) throw CommonImplementation.NoItemsMatched(nameof(predicate));

            return Item;
        }

        public TItem FirstOrDefault()
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");

            return Item;
        }

        public TItem FirstOrDefault(Func<TItem, bool> predicate)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");
            if (predicate == null) throw CommonImplementation.ArgumentNull(nameof(predicate));

            if (predicate(Item)) return Item;

            return default(TItem);
        }

        public TItem Last()
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");

            return Item;
        }

        public TItem Last(Func<TItem, bool> predicate)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");
            if (predicate == null) throw CommonImplementation.ArgumentNull(nameof(predicate));
            if (!predicate(Item)) throw CommonImplementation.NoItemsMatched(nameof(predicate));

            return Item;
        }

        public TItem LastOrDefault()
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");

            return Item;
        }

        public TItem LastOrDefault(Func<TItem, bool> predicate)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");
            if (predicate == null) throw CommonImplementation.ArgumentNull(nameof(predicate));

            if (predicate(Item)) return Item;

            return default(TItem);
        }

        public OneItemSpecificEnumerable<TItem> Reverse()
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");

            return new OneItemSpecificEnumerable<TItem>(Enumerable.OneItemSigil, Item);
        }

        public TItem Single()
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");

            return Item;
        }

        public TItem Single(Func<TItem, bool> predicate)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");
            if (predicate == null) throw CommonImplementation.ArgumentNull(nameof(predicate));

            if (!predicate(Item)) throw CommonImplementation.NoItemsMatched(nameof(predicate));

            return Item;
        }

        public TItem SingleOrDefault()
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");

            return Item;
        }

        public TItem SingleOrDefault(Func<TItem, bool> predicate)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");
            if (predicate == null) throw CommonImplementation.ArgumentNull(nameof(predicate));

            if (predicate(Item)) return Item;

            return default(TItem);
        }

        public TItem[] ToArray()
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");

            var ret = Allocator.Current.GetArray<TItem>(1);
            ret[0] = Item;
            return ret;
        }

        public Dictionary<TToDictionary_Key, TItem> ToDictionary<TToDictionary_Key>(Func<TItem, TToDictionary_Key> keySelector)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");
            if (keySelector == null) throw CommonImplementation.ArgumentNull(nameof(keySelector));

            var ret = Allocator.Current.GetEmptyDictionary<TToDictionary_Key, TItem>(1, null);
            ret.Add(keySelector(Item), Item);
            return ret;
        }

        public Dictionary<TToDictionary_Key, TItem> ToDictionary<TToDictionary_Key>(Func<TItem, TToDictionary_Key> keySelector, IEqualityComparer<TToDictionary_Key> comparer)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");
            if (keySelector == null) throw CommonImplementation.ArgumentNull(nameof(keySelector));

            var ret = Allocator.Current.GetEmptyDictionary<TToDictionary_Key, TItem>(1, comparer);
            ret.Add(keySelector(Item), Item);
            return ret;
        }

        public Dictionary<TToDictionary_Key, TToDictionary_Value> ToDictionary<TToDictionary_Key, TToDictionary_Value>(Func<TItem, TToDictionary_Key> keySelector, Func<TItem, TToDictionary_Value> elementSelector)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");
            if (keySelector == null) throw CommonImplementation.ArgumentNull(nameof(keySelector));
            if (elementSelector == null) throw CommonImplementation.ArgumentNull(nameof(elementSelector));

            var ret = Allocator.Current.GetEmptyDictionary<TToDictionary_Key, TToDictionary_Value>(1, null);
            ret.Add(keySelector(Item), elementSelector(Item));
            return ret;
        }

        public Dictionary<TToDictionary_Key, TToDictionary_Value> ToDictionary<TToDictionary_Key, TToDictionary_Value>(Func<TItem, TToDictionary_Key> keySelector, Func<TItem, TToDictionary_Value> elementSelector, IEqualityComparer<TToDictionary_Key> comparer)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");
            if (keySelector == null) throw CommonImplementation.ArgumentNull(nameof(keySelector));
            if (elementSelector == null) throw CommonImplementation.ArgumentNull(nameof(elementSelector));

            var ret = Allocator.Current.GetEmptyDictionary<TToDictionary_Key, TToDictionary_Value>(1, comparer);
            ret.Add(keySelector(Item), elementSelector(Item));
            return ret;
        }

        public List<TItem> ToList()
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");

            var ret = Allocator.Current.GetEmptyList<TItem>(1);
            ret.Add(Item);
            return ret;
        }

        public OneItemSpecificOrderedEnumerable<TItem> OrderBy<TOrderBy_Key>(Func<TItem, TOrderBy_Key> keySelector)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");
            if (keySelector == null) throw CommonImplementation.ArgumentNull(nameof(keySelector));

            return this;
        }

        public OneItemSpecificOrderedEnumerable<TItem> OrderBy<TOrderBy_Key>(Func<TItem, TOrderBy_Key> keySelector, IComparer<TOrderBy_Key> comparer)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");
            if (keySelector == null) throw CommonImplementation.ArgumentNull(nameof(keySelector));

            return this;
        }

        public OneItemSpecificOrderedEnumerable<TItem> OrderByDescending<TOrderBy_Key>(Func<TItem, TOrderBy_Key> keySelector)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");
            if (keySelector == null) throw CommonImplementation.ArgumentNull(nameof(keySelector));

            return this;
        }

        public OneItemSpecificOrderedEnumerable<TItem> OrderByDescending<TOrderBy_Key>(Func<TItem, TOrderBy_Key> keySelector, IComparer<TOrderBy_Key> comparer)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");
            if (keySelector == null) throw CommonImplementation.ArgumentNull(nameof(keySelector));

            return this;
        }

        public OneItemSpecificOrderedEnumerable<TItem> ThenBy<TThenBy_SecondKey>(Func<TItem, TThenBy_SecondKey> keySelector)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");
            if (keySelector == null) throw CommonImplementation.ArgumentNull(nameof(keySelector));

            return this;
        }

        public OneItemSpecificOrderedEnumerable<TItem> ThenBy<TThenBy_SecondKey>(Func<TItem, TThenBy_SecondKey> keySelector, IComparer<TThenBy_SecondKey> comparer)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");
            if (keySelector == null) throw CommonImplementation.ArgumentNull(nameof(keySelector));

            return this;
        }

        public OneItemSpecificOrderedEnumerable<TItem> ThenByDescending<TThenBy_SecondKey>(Func<TItem, TThenBy_SecondKey> keySelector)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");
            if (keySelector == null) throw CommonImplementation.ArgumentNull(nameof(keySelector));

            return this;
        }

        public OneItemSpecificOrderedEnumerable<TItem> ThenByDescending<TThenBy_SecondKey>(Func<TItem, TThenBy_SecondKey> keySelector, IComparer<TThenBy_SecondKey> comparer)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");
            if (keySelector == null) throw CommonImplementation.ArgumentNull(nameof(keySelector));

            return this;
        }

        public OneItemSpecificOrderedEnumerable<TItem> CreateOrderedEnumerable<TThenBy_SecondKey>(Func<TItem, TThenBy_SecondKey> keySelector, IComparer<TThenBy_SecondKey> comparer, bool descending)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");
            if (keySelector == null) throw CommonImplementation.ArgumentNull(nameof(keySelector));

            return this;
        }
    }
}