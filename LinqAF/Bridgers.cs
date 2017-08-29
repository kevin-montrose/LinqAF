using System.Collections.Generic;

namespace LinqAF
{
    // typeless
    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Auto)]
    public struct IEnumerableBridger : IStructBridger<object, System.Collections.IEnumerable, IdentityEnumerator>
    {
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public IdentityEnumerator Bridge(System.Collections.IEnumerable enumerable) => new IdentityEnumerator(enumerable);
    }

    // single types
    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Auto)]
    public struct IEnumerableBridger<TItem> : IStructBridger<TItem, IEnumerable<TItem>, IdentityEnumerator<TItem>>
    {
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public IdentityEnumerator<TItem> Bridge(IEnumerable<TItem> enumerable) => new IdentityEnumerator<TItem>(enumerable);
    }

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Auto)]
    public struct HashSetBridger<TItem> : IStructBridger<TItem, HashSet<TItem>, HashSetEnumerator<TItem>>
    {
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public HashSetEnumerator<TItem> Bridge(HashSet<TItem> enumerable) => new HashSetEnumerator<TItem>(enumerable);
    }

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Auto)]
    public struct LinkedListBridger<TItem> : IStructBridger<TItem, LinkedList<TItem>, LinkedListEnumerator<TItem>>
    {
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public LinkedListEnumerator<TItem> Bridge(LinkedList<TItem> enumerable) => new LinkedListEnumerator<TItem>(enumerable);
    }

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Auto)]
    public struct ListBridger<TItem> : IStructBridger<TItem, List<TItem>, ListEnumerator<TItem>>
    {
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public ListEnumerator<TItem> Bridge(List<TItem> enumerable) => new ListEnumerator<TItem>(enumerable);
    }

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Auto)]
    public struct SortedSetBridger<TItem> : IStructBridger<TItem, SortedSet<TItem>, SortedSetEnumerator<TItem>>
    {
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public SortedSetEnumerator<TItem> Bridge(SortedSet<TItem> enumerable) => new SortedSetEnumerator<TItem>(enumerable);
    }

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Auto)]
    public struct StackBridger<TItem> : IStructBridger<TItem, Stack<TItem>, StackEnumerator<TItem>>
    {
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public StackEnumerator<TItem> Bridge(Stack<TItem> enumerable) => new StackEnumerator<TItem>(enumerable);
    }

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Auto)]
    public struct QueueBridger<TItem> : IStructBridger<TItem, Queue<TItem>, QueueEnumerator<TItem>>
    {
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public QueueEnumerator<TItem> Bridge(Queue<TItem> enumerable) => new QueueEnumerator<TItem>(enumerable);
    }

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Auto)]
    public struct ArrayBridger<TItem> : IStructBridger<TItem, TItem[], ArrayEnumerator<TItem>>
    {
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public ArrayEnumerator<TItem> Bridge(TItem[] enumerable) => new ArrayEnumerator<TItem>(enumerable);
    }

    // double types
    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Auto)]
    public struct DictionaryBridger<TKey, TValue> : IStructBridger<KeyValuePair<TKey, TValue>, Dictionary<TKey, TValue>, DictionaryEnumerator<TKey, TValue>>
    {
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public DictionaryEnumerator<TKey, TValue> Bridge(Dictionary<TKey, TValue> enumerable) => new DictionaryEnumerator<TKey, TValue>(enumerable);
    }

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Auto)]
    public struct DictionaryKeysBridger<TKey, TValue> : IStructBridger<TKey, Dictionary<TKey, TValue>.KeyCollection, DictionaryKeysEnumerator<TKey, TValue>>
    {
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public DictionaryKeysEnumerator<TKey, TValue> Bridge(Dictionary<TKey, TValue>.KeyCollection enumerable) => new DictionaryKeysEnumerator<TKey, TValue>(enumerable);
    }

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Auto)]
    public struct DictionaryValuesBridger<TKey, TValue> : IStructBridger<TValue, Dictionary<TKey, TValue>.ValueCollection, DictionaryValuesEnumerator<TKey, TValue>>
    {
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public DictionaryValuesEnumerator<TKey, TValue> Bridge(Dictionary<TKey, TValue>.ValueCollection enumerable) => new DictionaryValuesEnumerator<TKey, TValue>(enumerable);
    }

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Auto)]
    public struct SortedDictionaryBridger<TKey, TValue> : IStructBridger<KeyValuePair<TKey, TValue>, SortedDictionary<TKey, TValue>, SortedDictionaryEnumerator<TKey, TValue>>
    {
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public SortedDictionaryEnumerator<TKey, TValue> Bridge(SortedDictionary<TKey, TValue> enumerable) => new SortedDictionaryEnumerator<TKey, TValue>(enumerable);
    }

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Auto)]
    public struct SortedDictionaryKeysBridger<TKey, TValue> : IStructBridger<TKey, SortedDictionary<TKey, TValue>.KeyCollection, SortedDictionaryKeysEnumerator<TKey, TValue>>
    {
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public SortedDictionaryKeysEnumerator<TKey, TValue> Bridge(SortedDictionary<TKey, TValue>.KeyCollection enumerable) => new SortedDictionaryKeysEnumerator<TKey, TValue>(enumerable);
    }

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Auto)]
    public struct SortedDictionaryValuesBridger<TKey, TValue> : IStructBridger<TValue, SortedDictionary<TKey, TValue>.ValueCollection, SortedDictionaryValuesEnumerator<TKey, TValue>>
    {
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public SortedDictionaryValuesEnumerator<TKey, TValue> Bridge(SortedDictionary<TKey, TValue>.ValueCollection enumerable) => new SortedDictionaryValuesEnumerator<TKey, TValue>(enumerable);
    }
}
