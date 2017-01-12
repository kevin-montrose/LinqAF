using System;
using System.Collections.Generic;

namespace LinqAF
{
    public partial struct OneItemDefaultEnumerable<TItem>
    {
        public bool All(Func<TItem, bool> predicate)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "source");
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));

            return predicate(default(TItem));
        }

        public bool Any()
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "source");

            return true;
        }

        public bool Contains(TItem value)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "source");

            return EqualityComparer<TItem>.Default.Equals(default(TItem), value);
        }

        public bool Contains(TItem value, IEqualityComparer<TItem> comparer)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "source");

            if(comparer == null)
            {
                // dodge an interface
                return EqualityComparer<TItem>.Default.Equals(default(TItem), value);
            }
            
            return comparer.Equals(default(TItem), value);
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

            return predicate(default(TItem)) ? 1 : 0;
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

            return predicate(default(TItem)) ? 1 : 0;
        }

        public OneItemDefaultEnumerable<TItem> DefaultIfEmpty()
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "source");

            return this;
        }

        public OneItemDefaultEnumerable<TItem> DefaultIfEmpty(TItem item)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "source");

            return this;
        }

        public TItem ElementAt(int index)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "source");
            if (index < 0 || index > 1) throw new ArgumentOutOfRangeException(nameof(index));

            return default(TItem);
        }

        public TItem ElementAtOrDefault(int index)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "source");

            return default(TItem);
        }

        public TItem First()
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "source");

            return default(TItem);
        }

        public TItem First(Func<TItem, bool> predicate)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "source");
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));
            if (!predicate(default(TItem))) throw new InvalidOperationException($"No items matched {nameof(predicate)}");

            return default(TItem);
        }

        public TItem FirstOrDefault()
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "source");

            return default(TItem);
        }

        public TItem FirstOrDefault(Func<TItem, bool> predicate)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "source");
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));

            return default(TItem);
        }

        public TItem Last()
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "source");

            return default(TItem);
        }

        public TItem Last(Func<TItem, bool> predicate)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "source");
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));
            if (!predicate(default(TItem))) throw new InvalidOperationException($"No items matched {nameof(predicate)}");

            return default(TItem);
        }

        public TItem LastOrDefault()
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "source");

            return default(TItem);
        }

        public TItem LastOrDefault(Func<TItem, bool> predicate)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "source");
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));

            return default(TItem);
        }

        public OneItemDefaultEnumerable<TItem> Reverse()
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "source");

            return this;
        }

        public TItem Single()
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "source");

            return default(TItem);
        }

        public TItem Single(Func<TItem, bool> predicate)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "source");
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));

            if(!predicate(default(TItem))) throw new InvalidOperationException($"No items matched {nameof(predicate)}");

            return default(TItem);
        }

        public TItem SingleOrDefault()
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "source");

            return default(TItem);
        }

        public TItem SingleOrDefault(Func<TItem, bool> predicate)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "source");
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));

            return default(TItem);
        }

        public TItem[] ToArray()
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "source");

            return new TItem[1];
        }

        public Dictionary<TToDictionary_Key, TItem> ToDictionary<TToDictionary_Key>(Func<TItem, TToDictionary_Key> keySelector)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "source");
            if (keySelector == null) throw new ArgumentNullException(nameof(keySelector));

            return
                new Dictionary<TToDictionary_Key, TItem>(1)
                {
                    [keySelector(default(TItem))] = default(TItem)
                };
        }

        public Dictionary<TToDictionary_Key, TItem> ToDictionary<TToDictionary_Key>(Func<TItem, TToDictionary_Key> keySelector, IEqualityComparer<TToDictionary_Key> comparer)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "source");
            if (keySelector == null) throw new ArgumentNullException(nameof(keySelector));

            return
                new Dictionary<TToDictionary_Key, TItem>(1, comparer)
                {
                    [keySelector(default(TItem))] = default(TItem)
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
                    [keySelector(default(TItem))] = elementSelector(default(TItem))
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
                    [keySelector(default(TItem))] = elementSelector(default(TItem))
                };
        }

        public List<TItem> ToList()
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "source");

            return new List<TItem>(1) { default(TItem) };
        }

        public OneItemDefaultOrderedEnumerable<TItem> OrderBy<TOrderBy_Key>(Func<TItem, TOrderBy_Key> keySelector)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "source");
            if (keySelector == null) throw new ArgumentNullException(nameof(keySelector));

            return OneItemDefaultOrderedEnumerable<TItem>.Instance;
        }

        public OneItemDefaultOrderedEnumerable<TItem> OrderBy<TOrderBy_Key>(Func<TItem, TOrderBy_Key> keySelector, IComparer<TOrderBy_Key> comparer)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "source");
            if (keySelector == null) throw new ArgumentNullException(nameof(keySelector));

            return OneItemDefaultOrderedEnumerable<TItem>.Instance;
        }

        public OneItemDefaultOrderedEnumerable<TItem> OrderByDescending<TOrderBy_Key>(Func<TItem, TOrderBy_Key> keySelector)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "source");
            if (keySelector == null) throw new ArgumentNullException(nameof(keySelector));

            return OneItemDefaultOrderedEnumerable<TItem>.Instance;
        }

        public OneItemDefaultOrderedEnumerable<TItem> OrderByDescending<TOrderBy_Key>(Func<TItem, TOrderBy_Key> keySelector, IComparer<TOrderBy_Key> comparer)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "source");
            if (keySelector == null) throw new ArgumentNullException(nameof(keySelector));

            return OneItemDefaultOrderedEnumerable<TItem>.Instance;
        }
    }
}