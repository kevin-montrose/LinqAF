using LinqAF.Impl;
using System;
using System.Collections.Generic;

namespace LinqAF
{
    public partial struct OneItemDefaultEnumerable<TItem>
    {
        public bool SequenceEqual(EmptyEnumerable<TItem> second)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            return false;
        }

        public bool SequenceEqual(EmptyEnumerable<TItem> second, IEqualityComparer<TItem> comparer)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            return false;
        }

        public bool SequenceEqual(EmptyOrderedEnumerable<TItem> second)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            return false;
        }

        public bool SequenceEqual(EmptyOrderedEnumerable<TItem> second, IEqualityComparer<TItem> comparer)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            return false;
        }

        public bool SequenceEqual(OneItemDefaultEnumerable<TItem> second)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            return EqualityComparer<TItem>.Default.Equals(default(TItem), default(TItem));
        }

        public bool SequenceEqual(OneItemDefaultEnumerable<TItem> second, IEqualityComparer<TItem> comparer)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            if (comparer == null)
            {
                return EqualityComparer<TItem>.Default.Equals(default(TItem), default(TItem));
            }

            return comparer.Equals(default(TItem), default(TItem));
        }

        public bool SequenceEqual(OneItemSpecificEnumerable<TItem> second)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            return EqualityComparer<TItem>.Default.Equals(default(TItem), second.Item);
        }

        public bool SequenceEqual(OneItemSpecificEnumerable<TItem> second, IEqualityComparer<TItem> comparer)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            if (comparer == null)
            {
                return EqualityComparer<TItem>.Default.Equals(default(TItem), second.Item);
            }

            return comparer.Equals(default(TItem), second.Item);
        }

        public bool SequenceEqual(OneItemDefaultOrderedEnumerable<TItem> second)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            return EqualityComparer<TItem>.Default.Equals(default(TItem), default(TItem));
        }

        public bool SequenceEqual(OneItemDefaultOrderedEnumerable<TItem> second, IEqualityComparer<TItem> comparer)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            if (comparer == null)
            {
                return EqualityComparer<TItem>.Default.Equals(default(TItem), default(TItem));
            }

            return comparer.Equals(default(TItem), default(TItem));
        }

        public bool SequenceEqual(OneItemSpecificOrderedEnumerable<TItem> second)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            return EqualityComparer<TItem>.Default.Equals(default(TItem), second.Item);
        }

        public bool SequenceEqual(OneItemSpecificOrderedEnumerable<TItem> second, IEqualityComparer<TItem> comparer)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            if (comparer == null)
            {
                return EqualityComparer<TItem>.Default.Equals(default(TItem), second.Item);
            }

            return comparer.Equals(default(TItem), second.Item);
        }

        public bool SequenceEqual<TSequenceEqual_DictionaryValue>(Dictionary<TItem, TSequenceEqual_DictionaryValue>.KeyCollection second)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            var bridge = CommonImplementation.Bridge(second, nameof(second));

            if (second.Count != 1) return false;

            return
                CommonImplementation.SequenceEqualImpl<
                    TItem,
                    OneItemDefaultEnumerable<TItem>,
                    OneItemDefaultEnumerator<TItem>,
                    IdentityEnumerable<
                        TItem,
                        Dictionary<TItem, TSequenceEqual_DictionaryValue>.KeyCollection,
                        DictionaryKeysBridger<TItem, TSequenceEqual_DictionaryValue>,
                        DictionaryKeysEnumerator<TItem, TSequenceEqual_DictionaryValue>
                    >,
                    DictionaryKeysEnumerator<TItem, TSequenceEqual_DictionaryValue>
                >(ref this, ref bridge);
        }

        public bool SequenceEqual<TSequenceEqual_DictionaryValue>(Dictionary<TItem, TSequenceEqual_DictionaryValue>.KeyCollection second, IEqualityComparer<TItem> comparer)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            var bridge = CommonImplementation.Bridge(second, nameof(second));

            if (second.Count != 1) return false;

            return 
                CommonImplementation.SequenceEqualImpl<
                    TItem, 
                    OneItemDefaultEnumerable<TItem>, 
                    OneItemDefaultEnumerator<TItem>, 
                    IdentityEnumerable<
                        TItem, 
                        Dictionary<TItem, TSequenceEqual_DictionaryValue>.KeyCollection,
                        DictionaryKeysBridger<TItem, TSequenceEqual_DictionaryValue>,
                        DictionaryKeysEnumerator<TItem, TSequenceEqual_DictionaryValue>
                    >,
                    DictionaryKeysEnumerator<TItem, TSequenceEqual_DictionaryValue>
                >(ref this, ref bridge, comparer);
        }

        public bool SequenceEqual<TSequenceEqual_DictionaryKey>(Dictionary<TSequenceEqual_DictionaryKey, TItem>.ValueCollection second)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            var bridge = CommonImplementation.Bridge(second, nameof(second));

            if (second.Count != 1) return false;

            return
                CommonImplementation.SequenceEqualImpl<
                    TItem,
                    OneItemDefaultEnumerable<TItem>,
                    OneItemDefaultEnumerator<TItem>,
                    IdentityEnumerable<
                        TItem,
                        Dictionary<TSequenceEqual_DictionaryKey, TItem>.ValueCollection,
                        DictionaryValuesBridger<TSequenceEqual_DictionaryKey, TItem>,
                        DictionaryValuesEnumerator<TSequenceEqual_DictionaryKey, TItem>
                    >,
                    DictionaryValuesEnumerator<TSequenceEqual_DictionaryKey, TItem>
                >(ref this, ref bridge);
        }

        public bool SequenceEqual<TSequenceEqual_DictionaryKey>(Dictionary<TSequenceEqual_DictionaryKey, TItem>.ValueCollection second, IEqualityComparer<TItem> comparer)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            var bridge = CommonImplementation.Bridge(second, nameof(second));

            if (second.Count != 1) return false;

            return
                CommonImplementation.SequenceEqualImpl<
                    TItem,
                    OneItemDefaultEnumerable<TItem>,
                    OneItemDefaultEnumerator<TItem>,
                    IdentityEnumerable<
                        TItem,
                        Dictionary<TSequenceEqual_DictionaryKey, TItem>.ValueCollection,
                        DictionaryValuesBridger<TSequenceEqual_DictionaryKey, TItem>,
                        DictionaryValuesEnumerator<TSequenceEqual_DictionaryKey, TItem>
                    >,
                    DictionaryValuesEnumerator<TSequenceEqual_DictionaryKey, TItem>
                >(ref this, ref bridge, comparer);
        }

        public bool SequenceEqual(HashSet<TItem> second)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            var bridge = CommonImplementation.Bridge(second, nameof(second));

            if (second.Count != 1) return false;

            return
                CommonImplementation.SequenceEqualImpl<
                    TItem,
                    OneItemDefaultEnumerable<TItem>,
                    OneItemDefaultEnumerator<TItem>,
                    IdentityEnumerable<
                        TItem,
                        HashSet<TItem>,
                        HashSetBridger<TItem>,
                        HashSetEnumerator<TItem>
                    >,
                    HashSetEnumerator<TItem>
                >(ref this, ref bridge);
        }

        public bool SequenceEqual(HashSet<TItem> second, IEqualityComparer<TItem> comparer)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            var bridge = CommonImplementation.Bridge(second, nameof(second));

            if (second.Count != 1) return false;

            return
                CommonImplementation.SequenceEqualImpl<
                    TItem,
                    OneItemDefaultEnumerable<TItem>,
                    OneItemDefaultEnumerator<TItem>,
                    IdentityEnumerable<
                        TItem,
                        HashSet<TItem>,
                        HashSetBridger<TItem>,
                        HashSetEnumerator<TItem>
                    >,
                    HashSetEnumerator<TItem>
                >(ref this, ref bridge, comparer);
        }
        
        public bool SequenceEqual(LinkedList<TItem> second)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            if (second == null) throw CommonImplementation.ArgumentNull(nameof(second));

            if (second.Count != 1) return false;

            var first = second.First.Value;

            return EqualityComparer<TItem>.Default.Equals(default(TItem), first);
        }

        public bool SequenceEqual(LinkedList<TItem> second, IEqualityComparer<TItem> comparer)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            if (second == null) throw CommonImplementation.ArgumentNull(nameof(second));

            if (second.Count != 1) return false;

            var first = second.First.Value;

            if (comparer == null)
            {
                return EqualityComparer<TItem>.Default.Equals(default(TItem), first);
            }

            return comparer.Equals(default(TItem), first);
        }

        public bool SequenceEqual(List<TItem> second)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            if (second == null) throw CommonImplementation.ArgumentNull(nameof(second));

            if (second.Count != 1) return false;

            var first = second[0];

            return EqualityComparer<TItem>.Default.Equals(default(TItem), first);
        }

        public bool SequenceEqual(List<TItem> second, IEqualityComparer<TItem> comparer)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            if (second == null) throw CommonImplementation.ArgumentNull(nameof(second));

            if (second.Count != 1) return false;

            var first = second[0];

            if (comparer == null)
            {
                return EqualityComparer<TItem>.Default.Equals(default(TItem), first);
            }

            return comparer.Equals(default(TItem), first);
        }

        public bool SequenceEqual(Queue<TItem> second)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            if (second == null) throw CommonImplementation.ArgumentNull(nameof(second));

            if (second.Count != 1) return false;

            var first = second.Peek();

            return EqualityComparer<TItem>.Default.Equals(default(TItem), first);
        }

        public bool SequenceEqual(Queue<TItem> second, IEqualityComparer<TItem> comparer)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            if (second == null) throw CommonImplementation.ArgumentNull(nameof(second));

            if (second.Count != 1) return false;

            var first = second.Peek();

            if (comparer == null)
            {
                return EqualityComparer<TItem>.Default.Equals(default(TItem), first);
            }

            return comparer.Equals(default(TItem), first);
        }

        public bool SequenceEqual<TSequenceEqual_DictionaryValue>(SortedDictionary<TItem, TSequenceEqual_DictionaryValue>.KeyCollection second)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            var bridge = CommonImplementation.Bridge(second, nameof(second));

            if (second.Count != 1) return false;

            return
                CommonImplementation.SequenceEqualImpl<
                    TItem,
                    OneItemDefaultEnumerable<TItem>,
                    OneItemDefaultEnumerator<TItem>,
                    IdentityEnumerable<
                        TItem,
                        SortedDictionary<TItem, TSequenceEqual_DictionaryValue>.KeyCollection,
                        SortedDictionaryKeysBridger<TItem, TSequenceEqual_DictionaryValue>,
                        SortedDictionaryKeysEnumerator<TItem, TSequenceEqual_DictionaryValue>
                    >,
                    SortedDictionaryKeysEnumerator<TItem, TSequenceEqual_DictionaryValue>
                >(ref this, ref bridge);
        }

        public bool SequenceEqual<TSequenceEqual_DictionaryValue>(SortedDictionary<TItem, TSequenceEqual_DictionaryValue>.KeyCollection second, IEqualityComparer<TItem> comparer)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            var bridge = CommonImplementation.Bridge(second, nameof(second));

            if (second.Count != 1) return false;

            return
                CommonImplementation.SequenceEqualImpl<
                    TItem,
                    OneItemDefaultEnumerable<TItem>,
                    OneItemDefaultEnumerator<TItem>,
                    IdentityEnumerable<
                        TItem,
                        SortedDictionary<TItem, TSequenceEqual_DictionaryValue>.KeyCollection,
                        SortedDictionaryKeysBridger<TItem, TSequenceEqual_DictionaryValue>,
                        SortedDictionaryKeysEnumerator<TItem, TSequenceEqual_DictionaryValue>
                    >,
                    SortedDictionaryKeysEnumerator<TItem, TSequenceEqual_DictionaryValue>
                >(ref this, ref bridge, comparer);
        }

        public bool SequenceEqual<TSequenceEqual_DictionaryKey>(SortedDictionary<TSequenceEqual_DictionaryKey, TItem>.ValueCollection second)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            var bridge = CommonImplementation.Bridge(second, nameof(second));

            if (second.Count != 1) return false;

            return
                CommonImplementation.SequenceEqualImpl<
                    TItem,
                    OneItemDefaultEnumerable<TItem>,
                    OneItemDefaultEnumerator<TItem>,
                    IdentityEnumerable<
                        TItem,
                        SortedDictionary<TSequenceEqual_DictionaryKey, TItem>.ValueCollection,
                        SortedDictionaryValuesBridger<TSequenceEqual_DictionaryKey, TItem>,
                        SortedDictionaryValuesEnumerator<TSequenceEqual_DictionaryKey, TItem>
                    >,
                    SortedDictionaryValuesEnumerator<TSequenceEqual_DictionaryKey, TItem>
                >(ref this, ref bridge);
        }

        public bool SequenceEqual<TSequenceEqual_DictionaryKey>(SortedDictionary<TSequenceEqual_DictionaryKey, TItem>.ValueCollection second, IEqualityComparer<TItem> comparer)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            var bridge = CommonImplementation.Bridge(second, nameof(second));

            if (second.Count != 1) return false;

            return
                CommonImplementation.SequenceEqualImpl<
                    TItem,
                    OneItemDefaultEnumerable<TItem>,
                    OneItemDefaultEnumerator<TItem>,
                    IdentityEnumerable<
                        TItem,
                        SortedDictionary<TSequenceEqual_DictionaryKey, TItem>.ValueCollection,
                        SortedDictionaryValuesBridger<TSequenceEqual_DictionaryKey, TItem>,
                        SortedDictionaryValuesEnumerator<TSequenceEqual_DictionaryKey, TItem>
                    >,
                    SortedDictionaryValuesEnumerator<TSequenceEqual_DictionaryKey, TItem>
                >(ref this, ref bridge, comparer);
        }

        public bool SequenceEqual(SortedSet<TItem> second)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            var bridge = CommonImplementation.Bridge(second, nameof(second));

            if (second.Count != 1) return false;

            return
                CommonImplementation.SequenceEqualImpl<
                    TItem,
                    OneItemDefaultEnumerable<TItem>,
                    OneItemDefaultEnumerator<TItem>,
                    IdentityEnumerable<
                        TItem,
                        SortedSet<TItem>,
                        SortedSetBridger<TItem>,
                        SortedSetEnumerator<TItem>
                    >,
                    SortedSetEnumerator<TItem>
                >(ref this, ref bridge);
        }

        public bool SequenceEqual(SortedSet<TItem> second, IEqualityComparer<TItem> comparer)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            var bridge = CommonImplementation.Bridge(second, nameof(second));

            if (second.Count != 1) return false;

            return
                CommonImplementation.SequenceEqualImpl<
                    TItem,
                    OneItemDefaultEnumerable<TItem>,
                    OneItemDefaultEnumerator<TItem>,
                    IdentityEnumerable<
                        TItem,
                        SortedSet<TItem>,
                        SortedSetBridger<TItem>,
                        SortedSetEnumerator<TItem>
                    >,
                    SortedSetEnumerator<TItem>
                >(ref this, ref bridge, comparer);
        }

        public bool SequenceEqual(Stack<TItem> second)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            if (second == null) throw CommonImplementation.ArgumentNull(nameof(second));

            if (second.Count != 1) return false;

            var first = second.Peek();

            return EqualityComparer<TItem>.Default.Equals(default(TItem), first);
        }

        public bool SequenceEqual(Stack<TItem> second, IEqualityComparer<TItem> comparer)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            if (second == null) throw CommonImplementation.ArgumentNull(nameof(second));

            if (second.Count != 1) return false;

            var first = second.Peek();

            if (comparer == null)
            {
                return EqualityComparer<TItem>.Default.Equals(default(TItem), first);
            }

            return comparer.Equals(default(TItem), first);
        }
        
        public bool SequenceEqual(TItem[] second)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            if (second == null) throw CommonImplementation.ArgumentNull(nameof(second));

            if (second.Length != 1) return false;

            return EqualityComparer<TItem>.Default.Equals(default(TItem), second[0]);
        }

        public bool SequenceEqual(TItem[] second, IEqualityComparer<TItem> comparer)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            if (second == null) throw CommonImplementation.ArgumentNull(nameof(second));

            if (second.Length != 1) return false;

            if (comparer == null)
            {
                return EqualityComparer<TItem>.Default.Equals(default(TItem), second[0]);
            }

            return comparer.Equals(default(TItem), second[0]);
        }

        public bool SequenceEqual(RangeEnumerable<TItem> second)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            if (second.InnerCount != 1) return false;

            return EqualityComparer<TItem>.Default.Equals(default(TItem), second.Start);
        }

        public bool SequenceEqual(RangeEnumerable<TItem> second, IEqualityComparer<TItem> comparer)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            if (second.InnerCount != 1) return false;

            if (comparer == null)
            {
                return EqualityComparer<TItem>.Default.Equals(default(TItem), second.Start);
            }

            return comparer.Equals(default(TItem), second.Start);
        }

        public bool SequenceEqual(RepeatEnumerable<TItem> second)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            if (second.InnerCount != 1) return false;

            return EqualityComparer<TItem>.Default.Equals(default(TItem), second.Item);
        }

        public bool SequenceEqual(RepeatEnumerable<TItem> second, IEqualityComparer<TItem> comparer)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            if (second.InnerCount != 1) return false;

            if (comparer == null)
            {
                return EqualityComparer<TItem>.Default.Equals(default(TItem), second.Item);
            }

            return comparer.Equals(default(TItem), second.Item);
        }
    }
}
