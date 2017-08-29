using System;
using System.Collections.Generic;

namespace LinqAF.Impl
{
    static partial class CommonImplementation
    {
        internal static SingleProjection<TOutItem, TInItem> Bridge<TOutItem, TInItem>(Func<TInItem, TOutItem> func, string funcName)
        {
            if (func == null) throw CommonImplementation.ArgumentNull(funcName);

            return new SingleProjection<TOutItem, TInItem>(func);
        }

        internal static ChainedProjection<TOutItem, TInItem, TMiddleItem, SingleProjection<TOutItem, TMiddleItem>, SingleProjection<TMiddleItem, TInItem>> Bridge<TOutItem, TInItem, TMiddleItem>(Func<TInItem, TMiddleItem> left, Func<TMiddleItem, TOutItem> right, string rightName)
        {
            if (right == null) throw CommonImplementation.ArgumentNull(rightName);

            var leftBridge = new SingleProjection<TMiddleItem, TInItem>(left);
            var rightBridge = new SingleProjection<TOutItem, TMiddleItem>(right);

            return new ChainedProjection<TOutItem, TInItem, TMiddleItem, SingleProjection<TOutItem, TMiddleItem>, SingleProjection<TMiddleItem, TInItem>>(ref leftBridge, ref rightBridge);
        }

        internal static ChainedProjection<TOutItem, TInItem, TMiddleItem, SingleProjection<TOutItem, TMiddleItem>, TProjection> Bridge<TOutItem, TInItem, TMiddleItem, TProjection>(ref TProjection left, Func<TMiddleItem, TOutItem> right, string rightName)
            where TProjection: struct, IStructProjection<TMiddleItem, TInItem>
        {
            if (right == null) throw CommonImplementation.ArgumentNull(rightName);
            var rightBridge = new SingleProjection<TOutItem, TMiddleItem>(right);
            
            return new ChainedProjection<TOutItem, TInItem, TMiddleItem, SingleProjection<TOutItem, TMiddleItem>, TProjection>(ref left, ref rightBridge);
        }

        internal static SinglePredicate<TItem> Bridge<TItem>(Func<TItem, bool> func, string name)
        {
            if (func == null) throw CommonImplementation.ArgumentNull(name);

            return new SinglePredicate<TItem>(func);
        }

        internal static ChainedPredicate<TItem, SinglePredicate<TItem>, SinglePredicate<TItem>> Bridge<TItem>(Func<TItem, bool> left, Func<TItem, bool> right, string rightName)
        {
            if (right == null) throw CommonImplementation.ArgumentNull(rightName);

            var leftBridge = new SinglePredicate<TItem>(left);
            var rightBridge = new SinglePredicate<TItem>(right);
            return new ChainedPredicate<TItem, SinglePredicate<TItem>, SinglePredicate<TItem>>(ref leftBridge, ref rightBridge);
        }

        internal static ChainedPredicate<TItem, TPredicate, SinglePredicate<TItem>> Bridge<TItem, TPredicate>(ref TPredicate left, Func<TItem, bool> right, string rightName)
            where TPredicate: struct, IStructPredicate<TItem>
        {
            if (right == null) throw CommonImplementation.ArgumentNull(rightName);
            
            var rightBridge = new SinglePredicate<TItem>(right);
            return new ChainedPredicate<TItem, TPredicate, SinglePredicate<TItem>>(ref left, ref rightBridge);
        }

        internal static ChainedPredicate<TItem, SinglePredicate<TItem>, TTailPredicate> Bridge<TItem, TTailPredicate>(Func<TItem, bool> func, string name, ref TTailPredicate tail)
            where TTailPredicate: struct, IStructPredicate<TItem>
        {
            if (func == null) throw CommonImplementation.ArgumentNull(name);

            var bridge = new SinglePredicate<TItem>(func);
            return new ChainedPredicate<TItem, SinglePredicate<TItem>, TTailPredicate>(ref bridge, ref tail);
        }

        internal static IdentityEnumerable<object, System.Collections.IEnumerable, IEnumerableBridger, IdentityEnumerator> Bridge(System.Collections.IEnumerable e, string name)
        {
            if (e == null) throw CommonImplementation.ArgumentNull(name);

            return new IdentityEnumerable<object, System.Collections.IEnumerable, IEnumerableBridger, IdentityEnumerator>(e);
        }
        
        internal static IdentityEnumerable<TItem, IEnumerable<TItem>, IEnumerableBridger<TItem>, IdentityEnumerator<TItem>> Bridge<TItem>(IEnumerable<TItem> e, string name)
        {
            if (e == null) throw CommonImplementation.ArgumentNull(name);

            return new IdentityEnumerable<TItem, IEnumerable<TItem>, IEnumerableBridger<TItem>, IdentityEnumerator<TItem>>(e);
        }

        internal static IdentityEnumerable<KeyValuePair<TKey, TValue>, Dictionary<TKey, TValue>, DictionaryBridger<TKey, TValue>, DictionaryEnumerator<TKey, TValue>> Bridge<TKey, TValue>(Dictionary<TKey, TValue> e, string name)
        {
            if (e == null) throw CommonImplementation.ArgumentNull(name);

            return new IdentityEnumerable<KeyValuePair<TKey, TValue>, Dictionary<TKey, TValue>, DictionaryBridger<TKey, TValue>, DictionaryEnumerator<TKey, TValue>>(e);
        }

        internal static IdentityEnumerable<TKey, Dictionary<TKey, TValue>.KeyCollection, DictionaryKeysBridger<TKey, TValue>, DictionaryKeysEnumerator<TKey, TValue>> Bridge<TKey, TValue>(Dictionary<TKey, TValue>.KeyCollection e, string name)
        {
            if (e == null) throw CommonImplementation.ArgumentNull(name);

            return new IdentityEnumerable<TKey, Dictionary<TKey, TValue>.KeyCollection, DictionaryKeysBridger<TKey, TValue>, DictionaryKeysEnumerator<TKey, TValue>>(e);
        }

        internal static IdentityEnumerable<TValue, Dictionary<TKey, TValue>.ValueCollection, DictionaryValuesBridger<TKey, TValue>, DictionaryValuesEnumerator<TKey, TValue>> Bridge<TKey, TValue>(Dictionary<TKey, TValue>.ValueCollection e, string name)
        {
            if (e == null) throw CommonImplementation.ArgumentNull(name);

            return new IdentityEnumerable<TValue, Dictionary<TKey, TValue>.ValueCollection, DictionaryValuesBridger<TKey, TValue>, DictionaryValuesEnumerator<TKey, TValue>>(e);
        }

        internal static IdentityEnumerable<TItem, HashSet<TItem>, HashSetBridger<TItem>, HashSetEnumerator<TItem>> Bridge<TItem>(HashSet<TItem> e, string name)
        {
            if (e == null) throw CommonImplementation.ArgumentNull(name);

            return new IdentityEnumerable<TItem, HashSet<TItem>, HashSetBridger<TItem>, HashSetEnumerator<TItem>>(e);
        }

        internal static IdentityEnumerable<TItem, LinkedList<TItem>, LinkedListBridger<TItem>, LinkedListEnumerator<TItem>> Bridge<TItem>(LinkedList<TItem> e, string name)
        {
            if (e == null) throw CommonImplementation.ArgumentNull(name);

            return new IdentityEnumerable<TItem, LinkedList<TItem>, LinkedListBridger<TItem>, LinkedListEnumerator<TItem>>(e);
        }

        internal static IdentityEnumerable<TItem, List<TItem>, ListBridger<TItem>, ListEnumerator<TItem>> Bridge<TItem>(List<TItem> e, string name)
        {
            if (e == null) throw CommonImplementation.ArgumentNull(name);

            return new IdentityEnumerable<TItem, List<TItem>, ListBridger<TItem>, ListEnumerator<TItem>>(e);
        }

        internal static IdentityEnumerable<TItem, Queue<TItem>, QueueBridger<TItem>, QueueEnumerator<TItem>> Bridge<TItem>(Queue<TItem> e, string name)
        {
            if (e == null) throw CommonImplementation.ArgumentNull(name);

            return new IdentityEnumerable<TItem, Queue<TItem>, QueueBridger<TItem>, QueueEnumerator<TItem>>(e);
        }

        internal static IdentityEnumerable<KeyValuePair<TKey, TValue>, SortedDictionary<TKey, TValue>, SortedDictionaryBridger<TKey, TValue>, SortedDictionaryEnumerator<TKey, TValue>> Bridge<TKey, TValue>(SortedDictionary<TKey, TValue> e, string name)
        {
            if (e == null) throw CommonImplementation.ArgumentNull(name);

            return new IdentityEnumerable<KeyValuePair<TKey, TValue>, SortedDictionary<TKey, TValue>, SortedDictionaryBridger<TKey, TValue>, SortedDictionaryEnumerator<TKey, TValue>>(e);
        }

        internal static IdentityEnumerable<TKey, SortedDictionary<TKey, TValue>.KeyCollection, SortedDictionaryKeysBridger<TKey, TValue>, SortedDictionaryKeysEnumerator<TKey, TValue>> Bridge<TKey, TValue>(SortedDictionary<TKey, TValue>.KeyCollection e, string name)
        {
            if (e == null) throw CommonImplementation.ArgumentNull(name);

            return new IdentityEnumerable<TKey, SortedDictionary<TKey, TValue>.KeyCollection, SortedDictionaryKeysBridger<TKey, TValue>, SortedDictionaryKeysEnumerator<TKey, TValue>>(e);
        }

        internal static IdentityEnumerable<TValue, SortedDictionary<TKey, TValue>.ValueCollection, SortedDictionaryValuesBridger<TKey, TValue>, SortedDictionaryValuesEnumerator<TKey, TValue>> Bridge<TKey, TValue>(SortedDictionary<TKey, TValue>.ValueCollection e, string name)
        {
            if (e == null) throw CommonImplementation.ArgumentNull(name);

            return new IdentityEnumerable<TValue, SortedDictionary<TKey, TValue>.ValueCollection, SortedDictionaryValuesBridger<TKey, TValue>, SortedDictionaryValuesEnumerator<TKey, TValue>>(e);
        }

        internal static IdentityEnumerable<TItem, SortedSet<TItem>, SortedSetBridger<TItem>, SortedSetEnumerator<TItem>> Bridge<TItem>(SortedSet<TItem> e, string name)
        {
            if (e == null) throw CommonImplementation.ArgumentNull(name);

            return new IdentityEnumerable<TItem, SortedSet<TItem>, SortedSetBridger<TItem>, SortedSetEnumerator<TItem>>(e);
        }

        internal static IdentityEnumerable<TItem, Stack<TItem>, StackBridger<TItem>, StackEnumerator<TItem>> Bridge<TItem>(Stack<TItem> e, string name)
        {
            if (e == null) throw CommonImplementation.ArgumentNull(name);

            return new IdentityEnumerable<TItem, Stack<TItem>, StackBridger<TItem>, StackEnumerator<TItem>>(e);
        }

        internal static IdentityEnumerable<TItem, TItem[], ArrayBridger<TItem>, ArrayEnumerator<TItem>> Bridge<TItem>(TItem[] e, string name)
        {
            if (e == null) throw CommonImplementation.ArgumentNull(name);

            return new IdentityEnumerable<TItem, TItem[], ArrayBridger<TItem>, ArrayEnumerator<TItem>>(e);
        }
    }
}
