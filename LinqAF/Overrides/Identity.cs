using LinqAF.Impl;
using System;
using System.Collections.Generic;

namespace LinqAF
{
    public partial struct IdentityEnumerable<TItem, TBridgeType, TEnumerator>
    {
        public bool Any()
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument unintialized", "source");

            var asICollection = this.Inner as ICollection<TItem>;
            if (asICollection != null)
            {
                return asICollection.Count > 0;
            }

            return CommonImplementation.AnyImpl<TItem, IdentityEnumerable<TItem, TBridgeType, TEnumerator>, TEnumerator>(ref this);
        }

        public IEnumerable<TItem> AsEnumerable()
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "source");

            return (IEnumerable<TItem>)this.Inner;
        }
        
        public bool Contains(TItem value)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument unintialized", "source");

            var asHashSet = this.Inner as HashSet<TItem>;
            if (asHashSet != null)
            {
                if (EqualityComparer<TItem>.Default.Equals(asHashSet.Comparer))
                {
                    return asHashSet.Contains(value);
                }
            }

            return CommonImplementation.ContainsImpl<TItem, IdentityEnumerable<TItem, TBridgeType, TEnumerator>, TEnumerator>(ref this, value);
        }

        public bool Contains(TItem value, IEqualityComparer<TItem> comparer)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument unintialized", "source");

            var asHashSet = this.Inner as HashSet<TItem>;
            if (asHashSet != null)
            {
                if ((comparer ?? EqualityComparer<TItem>.Default).Equals(asHashSet.Comparer))
                {
                    return asHashSet.Contains(value);
                }
            }

            return CommonImplementation.ContainsImpl<TItem, IdentityEnumerable<TItem, TBridgeType, TEnumerator>, TEnumerator>(ref this, value, comparer);
        }

        public int Count()
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument unintialized", "source");

            var asICollection = this.Inner as ICollection<TItem>;
            if (asICollection != null)
            {
                return asICollection.Count;
            }

            return CommonImplementation.CountImpl<TItem, IdentityEnumerable<TItem, TBridgeType, TEnumerator>, TEnumerator>(ref this);
        }

        public long LongCount()
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument unintialized", "source");

            var asArr = this.Inner as TItem[];
            if(asArr != null)
            {
                return asArr.LongLength;
            }

            var asICollection = this.Inner as ICollection<TItem>;
            if (asICollection != null)
            {
                return asICollection.Count;
            }

            return CommonImplementation.CountImpl<TItem, IdentityEnumerable<TItem, TBridgeType, TEnumerator>, TEnumerator>(ref this);
        }

        public TItem ElementAt(int index)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument unintialized", "source");

            var asIList = this.Inner as IList<TItem>;
            if (asIList != null)
            {
                if (index < 0 || index >= asIList.Count)
                {
                    throw new ArgumentOutOfRangeException(nameof(index));
                }

                return asIList[index];
            }

            return CommonImplementation.ElementAtImpl<TItem, IdentityEnumerable<TItem, TBridgeType, TEnumerator>, TEnumerator>(ref this, index);
        }

        public TItem ElementAtOrDefault(int index)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument unintialized", "source");

            var asIList = this.Inner as IList<TItem>;
            if (asIList != null)
            {
                if (index < 0 || index >= asIList.Count)
                {
                    return default(TItem);
                }

                return asIList[index];
            }

            return CommonImplementation.ElementAtOrDefaultImpl<TItem, IdentityEnumerable<TItem, TBridgeType, TEnumerator>, TEnumerator>(ref this, index);
        }

        public TItem First()
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument unintialized", "source");

            var asIList = this.Inner as IList<TItem>;
            if (asIList != null)
            {
                if (asIList.Count == 0)
                {
                    throw new InvalidOperationException("Sequence was empty");
                }

                return asIList[0];
            }

            return CommonImplementation.FirstImpl<TItem, IdentityEnumerable<TItem, TBridgeType, TEnumerator>, TEnumerator>(ref this);
        }

        public TItem FirstOrDefault()
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument unintialized", "source");

            var asIList = this.Inner as IList<TItem>;
            if (asIList != null)
            {
                if (asIList.Count == 0)
                {
                    return default(TItem);
                }

                return asIList[0];
            }

            return CommonImplementation.FirstOrDefaultImpl<TItem, IdentityEnumerable<TItem, TBridgeType, TEnumerator>, TEnumerator>(ref this);
        }

        public TItem Single()
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument unintialized", "source");

            var asIList = this.Inner as IList<TItem>;
            if (asIList != null)
            {
                if (asIList.Count == 0)
                {
                    throw new InvalidOperationException("Sequence was empty");
                }

                if (asIList.Count > 1)
                {
                    throw new InvalidOperationException("Sequence contained multiple elements");
                }

                return asIList[0];
            }

            return CommonImplementation.SingleImpl<TItem, IdentityEnumerable<TItem, TBridgeType, TEnumerator>, TEnumerator>(ref this);
        }

        public TItem SingleOrDefault()
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument unintialized", "source");

            var asIList = this.Inner as IList<TItem>;
            if (asIList != null)
            {
                if (asIList.Count == 0)
                {
                    return default(TItem);
                }

                if (asIList.Count > 1)
                {
                    throw new InvalidOperationException("Sequence contained multiple elements");
                }

                return asIList[0];
            }

            return CommonImplementation.SingleOrDefaultImpl<TItem, IdentityEnumerable<TItem, TBridgeType, TEnumerator>, TEnumerator>(ref this);
        }

        public TItem Last()
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument unintialized", "source");

            var asIList = this.Inner as IList<TItem>;
            if (asIList != null)
            {
                if (asIList.Count == 0)
                {
                    throw new InvalidOperationException("Sequence was empty");
                }

                return asIList[asIList.Count - 1];
            }

            return CommonImplementation.LastImpl<TItem, IdentityEnumerable<TItem, TBridgeType, TEnumerator>, TEnumerator>(ref this);
        }

        public TItem LastOrDefault()
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument unintialized", "source");

            var asIList = this.Inner as IList<TItem>;
            if (asIList != null)
            {
                if (asIList.Count == 0)
                {
                    return default(TItem);
                }

                return asIList[asIList.Count - 1];
            }

            return CommonImplementation.LastOrDefaultImpl<TItem, IdentityEnumerable<TItem, TBridgeType, TEnumerator>, TEnumerator>(ref this);
        }

        public bool SequenceEqual(EmptyEnumerable<TItem> second)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument unintialized", "first");
            if (second.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(second));

            var asICollection = this.Inner as ICollection<TItem>;
            if (asICollection != null)
            {
                return asICollection.Count == 0;
            }

            return CommonImplementation.SequenceEqualImpl<TItem, IdentityEnumerable<TItem, TBridgeType, TEnumerator>, TEnumerator, EmptyEnumerable<TItem>, EmptyEnumerator<TItem>>(ref this, ref second, null);
        }


        public bool SequenceEqual(EmptyEnumerable<TItem> second, IEqualityComparer<TItem> comparer)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument unintialized", "source");
            if (second.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(second));

            var asICollection = this.Inner as ICollection<TItem>;
            if (asICollection != null)
            {
                return asICollection.Count == 0;
            }

            return CommonImplementation.SequenceEqualImpl<TItem, IdentityEnumerable<TItem, TBridgeType, TEnumerator>, TEnumerator, EmptyEnumerable<TItem>, EmptyEnumerator<TItem>>(ref this, ref second, comparer);
        }

        public List<TItem> ToList()
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument unintialized", "source");

            var asIEnumerable = this.Inner as IEnumerable<TItem>;
            if(asIEnumerable != null)
            {
                return new List<TItem>(asIEnumerable);
            }

            return CommonImplementation.ToListImpl<TItem, IdentityEnumerable<TItem, TBridgeType, TEnumerator>, TEnumerator>(ref this);
        }

        public TItem[] ToArray()
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument unintialized", "source");

            var asICollection = this.Inner as ICollection<TItem>;
            if(asICollection != null)
            {
                var ret = new TItem[asICollection.Count];
                asICollection.CopyTo(ret, 0);
                return ret;
            }

            return CommonImplementation.ToArrayImpl<TItem, IdentityEnumerable<TItem, TBridgeType, TEnumerator>, TEnumerator>(ref this);
        }
        
        public Dictionary<TToDictionary_Key, TItem> ToDictionary<TToDictionary_Key>(Func<TItem, TToDictionary_Key> keySelector)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "source");
            if (keySelector == null) throw new ArgumentNullException(nameof(keySelector));

            var asICollection = this.Inner as ICollection<TItem>;
            if (asICollection != null)
            {
                var ret = new Dictionary<TToDictionary_Key, TItem>(asICollection.Count);
                CommonImplementation.ToDictionaryLoopImpl<TItem, TToDictionary_Key, TItem, IdentityEnumerable<TItem, TBridgeType, TEnumerator>, TEnumerator>(ref this, ret, keySelector, _ => _);
                return ret;
            }

            return CommonImplementation.ToDictionaryImpl<TItem, TToDictionary_Key, TItem, IdentityEnumerable<TItem, TBridgeType, TEnumerator>, TEnumerator>(ref this, keySelector, _ => _, null);
        }

        public Dictionary<TToDictionary_Key, TToDictionary_Value> ToDictionary<TToDictionary_Key, TToDictionary_Value>(Func<TItem, TToDictionary_Key> keySelector, Func<TItem, TToDictionary_Value> elementSelector)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "source");
            if (keySelector == null) throw new ArgumentNullException(nameof(keySelector));
            if (elementSelector == null) throw new ArgumentNullException(nameof(elementSelector));

            var asICollection = this.Inner as ICollection<TItem>;
            if (asICollection != null)
            {
                var ret = new Dictionary<TToDictionary_Key, TToDictionary_Value>(asICollection.Count);
                CommonImplementation.ToDictionaryLoopImpl<TItem, TToDictionary_Key, TToDictionary_Value, IdentityEnumerable<TItem, TBridgeType, TEnumerator>, TEnumerator>(ref this, ret, keySelector, elementSelector);
                return ret;
            }

            return CommonImplementation.ToDictionaryImpl<TItem, TToDictionary_Key, TToDictionary_Value, IdentityEnumerable<TItem, TBridgeType, TEnumerator>, TEnumerator>(ref this, keySelector, elementSelector, null);
        }

        public Dictionary<TToDictionary_Key, TItem> ToDictionary<TToDictionary_Key>(Func<TItem, TToDictionary_Key> keySelector, IEqualityComparer<TToDictionary_Key> comparer)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "source");
            if (keySelector == null) throw new ArgumentNullException(nameof(keySelector));

            var asICollection = this.Inner as ICollection<TItem>;
            if (asICollection != null)
            {
                var ret = new Dictionary<TToDictionary_Key, TItem>(asICollection.Count, comparer);
                CommonImplementation.ToDictionaryLoopImpl<TItem, TToDictionary_Key, TItem, IdentityEnumerable<TItem, TBridgeType, TEnumerator>, TEnumerator>(ref this, ret, keySelector, _ => _);
                return ret;
            }

            return CommonImplementation.ToDictionaryImpl<TItem, TToDictionary_Key, TItem, IdentityEnumerable<TItem, TBridgeType, TEnumerator>, TEnumerator>(ref this, keySelector, _ => _, comparer);
        }

        public Dictionary<TToDictionary_Key, TToDictionary_Value> ToDictionary<TToDictionary_Key, TToDictionary_Value>(Func<TItem, TToDictionary_Key> keySelector, Func<TItem, TToDictionary_Value> elementSelector, IEqualityComparer<TToDictionary_Key> comparer)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "source");
            if (keySelector == null) throw new ArgumentNullException(nameof(keySelector));
            if (elementSelector == null) throw new ArgumentNullException(nameof(elementSelector));

            var asICollection = this.Inner as ICollection<TItem>;
            if (asICollection != null)
            {
                var ret = new Dictionary<TToDictionary_Key, TToDictionary_Value>(asICollection.Count, comparer);
                CommonImplementation.ToDictionaryLoopImpl<TItem, TToDictionary_Key, TToDictionary_Value, IdentityEnumerable<TItem, TBridgeType, TEnumerator>, TEnumerator>(ref this, ret, keySelector, elementSelector);
                return ret;
            }

            return CommonImplementation.ToDictionaryImpl<TItem, TToDictionary_Key, TToDictionary_Value, IdentityEnumerable<TItem, TBridgeType, TEnumerator>, TEnumerator>(ref this, keySelector, elementSelector, comparer);
        }
    }
}