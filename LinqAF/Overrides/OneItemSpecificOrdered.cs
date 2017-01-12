using System;
using System.Collections.Generic;

namespace LinqAF
{
    public partial struct OneItemSpecificOrderedEnumerable<TItem>
    {
        public bool All(Func<TItem, bool> predicate)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "source");
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));

            return predicate(Item);
        }

        public bool Any()
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "source");

            return true;
        }

        public bool Contains(TItem value)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "source");

            return EqualityComparer<TItem>.Default.Equals(Item, value);
        }

        public bool Contains(TItem value, IEqualityComparer<TItem> comparer)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "source");

            if (comparer == null)
            {
                // dodge an interface
                return EqualityComparer<TItem>.Default.Equals(Item, value);
            }

            return comparer.Equals(Item, value);
        }

        public int Count()
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "source");

            return 1;
        }

        public int Count(Func<TItem, bool> predicate)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "source");
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));

            return predicate(Item) ? 1 : 0;
        }

        public long LongCount()
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "source");

            return 1;
        }

        public long LongCount(Func<TItem, bool> predicate)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "source");
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));

            return predicate(Item) ? 1 : 0;
        }

        public OneItemSpecificEnumerable<TItem> DefaultIfEmpty()
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "source");

            return new OneItemSpecificEnumerable<TItem>(Enumerable.OneItemSigil, Item);
        }

        public OneItemSpecificEnumerable<TItem> DefaultIfEmpty(TItem item)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "source");

            return new OneItemSpecificEnumerable<TItem>(Enumerable.OneItemSigil, Item);
        }

        public TItem ElementAt(int index)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "source");
            if (index < 0 || index > 1) throw new ArgumentOutOfRangeException(nameof(index));

            return Item;
        }

        public TItem ElementAtOrDefault(int index)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "source");

            if (index == 0) return Item;

            return default(TItem);
        }

        public TItem First()
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "source");

            return Item;
        }

        public TItem First(Func<TItem, bool> predicate)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "source");
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));
            if (!predicate(Item)) throw new InvalidOperationException($"No items matched {nameof(predicate)}");

            return Item;
        }

        public TItem FirstOrDefault()
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "source");

            return Item;
        }

        public TItem FirstOrDefault(Func<TItem, bool> predicate)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "source");
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));

            if (predicate(Item)) return Item;

            return default(TItem);
        }

        public TItem Last()
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "source");

            return Item;
        }

        public TItem Last(Func<TItem, bool> predicate)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "source");
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));
            if (!predicate(Item)) throw new InvalidOperationException($"No items matched {nameof(predicate)}");

            return Item;
        }

        public TItem LastOrDefault()
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "source");

            return Item;
        }

        public TItem LastOrDefault(Func<TItem, bool> predicate)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "source");
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));

            if (predicate(Item)) return Item;

            return default(TItem);
        }

        public OneItemSpecificEnumerable<TItem> Reverse()
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "source");

            return new OneItemSpecificEnumerable<TItem>(Enumerable.OneItemSigil, Item);
        }

        public TItem Single()
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "source");

            return Item;
        }

        public TItem Single(Func<TItem, bool> predicate)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "source");
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));

            if (!predicate(Item)) throw new InvalidOperationException($"No items matched {nameof(predicate)}");

            return Item;
        }

        public TItem SingleOrDefault()
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "source");

            return Item;
        }

        public TItem SingleOrDefault(Func<TItem, bool> predicate)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "source");
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));

            if (predicate(Item)) return Item;

            return default(TItem);
        }

        public TItem[] ToArray()
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "source");

            return new TItem[] { Item };
        }

        public Dictionary<TToDictionary_Key, TItem> ToDictionary<TToDictionary_Key>(Func<TItem, TToDictionary_Key> keySelector)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "source");
            if (keySelector == null) throw new ArgumentNullException(nameof(keySelector));

            return
                new Dictionary<TToDictionary_Key, TItem>(1)
                {
                    [keySelector(Item)] = Item
                };
        }

        public Dictionary<TToDictionary_Key, TItem> ToDictionary<TToDictionary_Key>(Func<TItem, TToDictionary_Key> keySelector, IEqualityComparer<TToDictionary_Key> comparer)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "source");
            if (keySelector == null) throw new ArgumentNullException(nameof(keySelector));

            return
                new Dictionary<TToDictionary_Key, TItem>(1, comparer)
                {
                    [keySelector(Item)] = Item
                };
        }

        public Dictionary<TToDictionary_Key, TToDictionary_Value> ToDictionary<TToDictionary_Key, TToDictionary_Value>(Func<TItem, TToDictionary_Key> keySelector, Func<TItem, TToDictionary_Value> elementSelector)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "source");
            if (keySelector == null) throw new ArgumentNullException(nameof(keySelector));
            if (elementSelector == null) throw new ArgumentNullException(nameof(elementSelector));

            return
                new Dictionary<TToDictionary_Key, TToDictionary_Value>(1)
                {
                    [keySelector(Item)] = elementSelector(Item)
                };
        }

        public Dictionary<TToDictionary_Key, TToDictionary_Value> ToDictionary<TToDictionary_Key, TToDictionary_Value>(Func<TItem, TToDictionary_Key> keySelector, Func<TItem, TToDictionary_Value> elementSelector, IEqualityComparer<TToDictionary_Key> comparer)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "source");
            if (keySelector == null) throw new ArgumentNullException(nameof(keySelector));
            if (elementSelector == null) throw new ArgumentNullException(nameof(elementSelector));

            return
                new Dictionary<TToDictionary_Key, TToDictionary_Value>(1, comparer)
                {
                    [keySelector(Item)] = elementSelector(Item)
                };
        }

        public List<TItem> ToList()
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "source");

            return new List<TItem>(1) { Item };
        }

        public OneItemSpecificOrderedEnumerable<TItem> OrderBy<TOrderBy_Key>(Func<TItem, TOrderBy_Key> keySelector)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "source");
            if (keySelector == null) throw new ArgumentNullException(nameof(keySelector));

            return this;
        }

        public OneItemSpecificOrderedEnumerable<TItem> OrderBy<TOrderBy_Key>(Func<TItem, TOrderBy_Key> keySelector, IComparer<TOrderBy_Key> comparer)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "source");
            if (keySelector == null) throw new ArgumentNullException(nameof(keySelector));

            return this;
        }

        public OneItemSpecificOrderedEnumerable<TItem> OrderByDescending<TOrderBy_Key>(Func<TItem, TOrderBy_Key> keySelector)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "source");
            if (keySelector == null) throw new ArgumentNullException(nameof(keySelector));

            return this;
        }

        public OneItemSpecificOrderedEnumerable<TItem> OrderByDescending<TOrderBy_Key>(Func<TItem, TOrderBy_Key> keySelector, IComparer<TOrderBy_Key> comparer)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "source");
            if (keySelector == null) throw new ArgumentNullException(nameof(keySelector));

            return this;
        }

        public OneItemSpecificOrderedEnumerable<TItem> ThenBy<TThenBy_SecondKey>(Func<TItem, TThenBy_SecondKey> keySelector)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "source");
            if (keySelector == null) throw new ArgumentNullException(nameof(keySelector));

            return this;
        }

        public OneItemSpecificOrderedEnumerable<TItem> ThenBy<TThenBy_SecondKey>(Func<TItem, TThenBy_SecondKey> keySelector, IComparer<TThenBy_SecondKey> comparer)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "source");
            if (keySelector == null) throw new ArgumentNullException(nameof(keySelector));

            return this;
        }

        public OneItemSpecificOrderedEnumerable<TItem> ThenByDescending<TThenBy_SecondKey>(Func<TItem, TThenBy_SecondKey> keySelector)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "source");
            if (keySelector == null) throw new ArgumentNullException(nameof(keySelector));

            return this;
        }

        public OneItemSpecificOrderedEnumerable<TItem> ThenByDescending<TThenBy_SecondKey>(Func<TItem, TThenBy_SecondKey> keySelector, IComparer<TThenBy_SecondKey> comparer)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "source");
            if (keySelector == null) throw new ArgumentNullException(nameof(keySelector));

            return this;
        }
    }
}