using System;
using System.Collections.Generic;

namespace LinqAF.Impl
{
    static partial class CommonImplementation
    {
        internal static SingleProjection<TOutItem, TInItem> Bridge<TOutItem, TInItem>(Func<TInItem, TOutItem> func, string funcName)
        {
            if (func == null) throw new ArgumentNullException(funcName);

            return new SingleProjection<TOutItem, TInItem>(func);
        }

        internal static ChainedProjection<TOutItem, TInItem, TMiddleItem, SingleProjection<TOutItem, TMiddleItem>, SingleProjection<TMiddleItem, TInItem>> Bridge<TOutItem, TInItem, TMiddleItem>(Func<TInItem, TMiddleItem> left, Func<TMiddleItem, TOutItem> right, string rightName)
        {
            if (right == null) throw new ArgumentNullException(rightName);

            var leftBridge = new SingleProjection<TMiddleItem, TInItem>(left);
            var rightBridge = new SingleProjection<TOutItem, TMiddleItem>(right);

            return new ChainedProjection<TOutItem, TInItem, TMiddleItem, SingleProjection<TOutItem, TMiddleItem>, SingleProjection<TMiddleItem, TInItem>>(ref leftBridge, ref rightBridge);
        }

        internal static ChainedProjection<TOutItem, TInItem, TMiddleItem, SingleProjection<TOutItem, TMiddleItem>, TProjection> Bridge<TOutItem, TInItem, TMiddleItem, TProjection>(ref TProjection left, Func<TMiddleItem, TOutItem> right, string rightName)
            where TProjection: struct, IStructProjection<TMiddleItem, TInItem>
        {
            if (right == null) throw new ArgumentNullException(rightName);
            var rightBridge = new SingleProjection<TOutItem, TMiddleItem>(right);
            
            return new ChainedProjection<TOutItem, TInItem, TMiddleItem, SingleProjection<TOutItem, TMiddleItem>, TProjection>(ref left, ref rightBridge);
        }

        internal static SinglePredicate<TItem> Bridge<TItem>(Func<TItem, bool> func, string name)
        {
            if (func == null) throw new ArgumentNullException(name);

            return new SinglePredicate<TItem>(func);
        }

        internal static ChainedPredicate<TItem, SinglePredicate<TItem>, SinglePredicate<TItem>> Bridge<TItem>(Func<TItem, bool> left, Func<TItem, bool> right, string rightName)
        {
            if (right == null) throw new ArgumentNullException(rightName);

            var leftBridge = new SinglePredicate<TItem>(left);
            var rightBridge = new SinglePredicate<TItem>(right);
            return new ChainedPredicate<TItem, SinglePredicate<TItem>, SinglePredicate<TItem>>(ref leftBridge, ref rightBridge);
        }

        internal static ChainedPredicate<TItem, TPredicate, SinglePredicate<TItem>> Bridge<TItem, TPredicate>(ref TPredicate left, Func<TItem, bool> right, string rightName)
            where TPredicate: struct, IStructPredicate<TItem>
        {
            if (right == null) throw new ArgumentNullException(rightName);
            
            var rightBridge = new SinglePredicate<TItem>(right);
            return new ChainedPredicate<TItem, TPredicate, SinglePredicate<TItem>>(ref left, ref rightBridge);
        }

        internal static ChainedPredicate<TItem, SinglePredicate<TItem>, TTailPredicate> Bridge<TItem, TTailPredicate>(Func<TItem, bool> func, string name, ref TTailPredicate tail)
            where TTailPredicate: struct, IStructPredicate<TItem>
        {
            if (func == null) throw new ArgumentNullException(name);

            var bridge = new SinglePredicate<TItem>(func);
            return new ChainedPredicate<TItem, SinglePredicate<TItem>, TTailPredicate>(ref bridge, ref tail);
        }

        internal static IdentityEnumerable<object, System.Collections.IEnumerable, IdentityEnumerator> Bridge(System.Collections.IEnumerable e, string name)
        {
            if (e == null) throw new ArgumentNullException(name);

            return new IdentityEnumerable<object, System.Collections.IEnumerable, IdentityEnumerator>(e, IdentyMapsNonGeneric.IEnumerable);
        }
        
        internal static IdentityEnumerable<TItem, IEnumerable<TItem>, IdentityEnumerator<TItem>> Bridge<TItem>(IEnumerable<TItem> e, string name)
        {
            if (e == null) throw new ArgumentNullException(name);

            return new IdentityEnumerable<TItem, IEnumerable<TItem>, IdentityEnumerator<TItem>>(e, IdentityMaps<TItem>.IEnumerable);
        }

        internal static IdentityEnumerable<KeyValuePair<TKey, TValue>, Dictionary<TKey, TValue>, DictionaryEnumerator<TKey, TValue>> Bridge<TKey, TValue>(Dictionary<TKey, TValue> e, string name)
        {
            if (e == null) throw new ArgumentNullException(name);

            return new IdentityEnumerable<KeyValuePair<TKey, TValue>, Dictionary<TKey, TValue>, DictionaryEnumerator<TKey, TValue>>(e, IdentityMaps<TKey, TValue>.Dictionary);
        }

        internal static IdentityEnumerable<TKey, Dictionary<TKey, TValue>.KeyCollection, DictionaryKeysEnumerator<TKey, TValue>> Bridge<TKey, TValue>(Dictionary<TKey, TValue>.KeyCollection e, string name)
        {
            if (e == null) throw new ArgumentNullException(name);

            return new IdentityEnumerable<TKey, Dictionary<TKey, TValue>.KeyCollection, DictionaryKeysEnumerator<TKey, TValue>>(e, IdentityMaps<TKey, TValue>.DictionaryKeys);
        }

        internal static IdentityEnumerable<TValue, Dictionary<TKey, TValue>.ValueCollection, DictionaryValuesEnumerator<TKey, TValue>> Bridge<TKey, TValue>(Dictionary<TKey, TValue>.ValueCollection e, string name)
        {
            if (e == null) throw new ArgumentNullException(name);

            return new IdentityEnumerable<TValue, Dictionary<TKey, TValue>.ValueCollection, DictionaryValuesEnumerator<TKey, TValue>>(e, IdentityMaps<TKey, TValue>.DictionaryValues);
        }

        internal static IdentityEnumerable<TItem, HashSet<TItem>, HashSetEnumerator<TItem>> Bridge<TItem>(HashSet<TItem> e, string name)
        {
            if (e == null) throw new ArgumentNullException(name);

            return new IdentityEnumerable<TItem, HashSet<TItem>, HashSetEnumerator<TItem>>(e, IdentityMaps<TItem>.HashSet);
        }

        internal static IdentityEnumerable<TItem, LinkedList<TItem>, LinkedListEnumerator<TItem>> Bridge<TItem>(LinkedList<TItem> e, string name)
        {
            if (e == null) throw new ArgumentNullException(name);

            return new IdentityEnumerable<TItem, LinkedList<TItem>, LinkedListEnumerator<TItem>>(e, IdentityMaps<TItem>.LinkedList);
        }

        internal static IdentityEnumerable<TItem, List<TItem>, ListEnumerator<TItem>> Bridge<TItem>(List<TItem> e, string name)
        {
            if (e == null) throw new ArgumentNullException(name);

            return new IdentityEnumerable<TItem, List<TItem>, ListEnumerator<TItem>>(e, IdentityMaps<TItem>.List);
        }

        internal static IdentityEnumerable<TItem, Queue<TItem>, QueueEnumerator<TItem>> Bridge<TItem>(Queue<TItem> e, string name)
        {
            if (e == null) throw new ArgumentNullException(name);

            return new IdentityEnumerable<TItem, Queue<TItem>, QueueEnumerator<TItem>>(e, IdentityMaps<TItem>.Queue);
        }

        internal static IdentityEnumerable<KeyValuePair<TKey, TValue>, SortedDictionary<TKey, TValue>, SortedDictionaryEnumerator<TKey, TValue>> Bridge<TKey, TValue>(SortedDictionary<TKey, TValue> e, string name)
        {
            if (e == null) throw new ArgumentNullException(name);

            return new IdentityEnumerable<KeyValuePair<TKey, TValue>, SortedDictionary<TKey, TValue>, SortedDictionaryEnumerator<TKey, TValue>>(e, IdentityMaps<TKey, TValue>.SortedDictionary);
        }

        internal static IdentityEnumerable<TKey, SortedDictionary<TKey, TValue>.KeyCollection, SortedDictionaryKeysEnumerator<TKey, TValue>> Bridge<TKey, TValue>(SortedDictionary<TKey, TValue>.KeyCollection e, string name)
        {
            if (e == null) throw new ArgumentNullException(name);

            return new IdentityEnumerable<TKey, SortedDictionary<TKey, TValue>.KeyCollection, SortedDictionaryKeysEnumerator<TKey, TValue>>(e, IdentityMaps<TKey, TValue>.SortedDictionaryKeys);
        }

        internal static IdentityEnumerable<TValue, SortedDictionary<TKey, TValue>.ValueCollection, SortedDictionaryValuesEnumerator<TKey, TValue>> Bridge<TKey, TValue>(SortedDictionary<TKey, TValue>.ValueCollection e, string name)
        {
            if (e == null) throw new ArgumentNullException(name);

            return new IdentityEnumerable<TValue, SortedDictionary<TKey, TValue>.ValueCollection, SortedDictionaryValuesEnumerator<TKey, TValue>>(e, IdentityMaps<TKey, TValue>.SortedDictionaryValues);
        }

        internal static IdentityEnumerable<TItem, SortedSet<TItem>, SortedSetEnumerator<TItem>> Bridge<TItem>(SortedSet<TItem> e, string name)
        {
            if (e == null) throw new ArgumentNullException(name);

            return new IdentityEnumerable<TItem, SortedSet<TItem>, SortedSetEnumerator<TItem>>(e, IdentityMaps<TItem>.SortedSet);
        }

        internal static IdentityEnumerable<TItem, Stack<TItem>, StackEnumerator<TItem>> Bridge<TItem>(Stack<TItem> e, string name)
        {
            if (e == null) throw new ArgumentNullException(name);

            return new IdentityEnumerable<TItem, Stack<TItem>, StackEnumerator<TItem>>(e, IdentityMaps<TItem>.Stack);
        }

        internal static IdentityEnumerable<TItem, TItem[], ArrayEnumerator<TItem>> Bridge<TItem>(TItem[] e, string name)
        {
            if (e == null) throw new ArgumentNullException(name);

            return new IdentityEnumerable<TItem, TItem[], ArrayEnumerator<TItem>>(e, IdentityMaps<TItem>.Array);
        }
    }
}
