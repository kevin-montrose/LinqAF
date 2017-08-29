using LinqAF.Impl;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace LinqAF
{
    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Auto)]
    public struct LookupSpecificEnumerator<TKey, TElement>:
        IStructEnumerator<GroupingEnumerable<TKey, TElement>>
    {
        public GroupingEnumerable<TKey, TElement> Current { get; private set; }
        
        int Index;
        LookupHashtable<TKey, TElement> HashTable;
        
        internal LookupSpecificEnumerator(ref LookupHashtable<TKey, TElement> hashTable)
        {
            Current = default(GroupingEnumerable<TKey, TElement>);
            HashTable = hashTable;
            Index = -1;
        }

        public bool IsDefaultValue() => HashTable.IsDefaultValue();

        public void Dispose()
        {
            HashTable = default(LookupHashtable<TKey, TElement>);
        }

        public bool MoveNext()
        {
            Index++;
            if (Index == HashTable.Count) return false;

            Current = HashTable.GetAtIndex(Index);
            return true;
        }
        
        public void Reset()
        {
            Index = -1;
        }
    }

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Auto)]
    public partial struct LookupSpecificEnumerable<TKey, TElement>:
        IStructEnumerable<GroupingEnumerable<TKey, TElement>, LookupSpecificEnumerator<TKey, TElement>>
    {
        public int Count => HashTable.Count;

        LookupHashtable<TKey, TElement> HashTable;
        IEqualityComparer<TKey> Comparer;

        public GroupingEnumerable<TKey, TElement> this[TKey key] => HashTable.GetGrouping(key, Comparer);

        internal LookupSpecificEnumerable(ref LookupHashtable<TKey, TElement> hashTable, IEqualityComparer<TKey> comparer)
        {
            // Comparer is guaranteed to be non-null
            HashTable = hashTable;
            Comparer = comparer;
        }

        public bool IsDefaultValue() => Comparer == null;

        public bool Contains(TKey key) => HashTable.Contains(key, Comparer);

        public LookupSpecificEnumerator<TKey, TElement> GetEnumerator()
        {
            return new LookupSpecificEnumerator<TKey, TElement>(ref HashTable);
        }
    }

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Auto)]
    public struct LookupDefaultEnumerator<TKey, TElement> :
        IStructEnumerator<GroupingEnumerable<TKey, TElement>>
    {
        public GroupingEnumerable<TKey, TElement> Current { get; private set; }

        int Index;
        LookupHashtable<TKey, TElement> HashTable;

        internal LookupDefaultEnumerator(ref LookupHashtable<TKey, TElement> hashTable)
        {
            Current = default(GroupingEnumerable<TKey, TElement>);
            HashTable = hashTable;
            Index = -1;
        }

        public bool IsDefaultValue() => HashTable.IsDefaultValue();

        public void Dispose()
        {
            HashTable = default(LookupHashtable<TKey, TElement>);
        }

        public bool MoveNext()
        {
            Index++;
            if (Index == HashTable.Count) return false;

            Current = HashTable.GetAtIndex(Index);
            return true;
        }

        public void Reset()
        {
            Index = -1;
        }
    }

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Auto)]
    public partial struct LookupDefaultEnumerable<TKey, TElement> :
        IStructEnumerable<GroupingEnumerable<TKey, TElement>, LookupDefaultEnumerator<TKey, TElement>>
    {
        public int Count => HashTable.Count;

        LookupHashtable<TKey, TElement> HashTable;

        public GroupingEnumerable<TKey, TElement> this[TKey key] => HashTable.GetGrouping(key);

        internal LookupDefaultEnumerable(ref LookupHashtable<TKey, TElement> hashTable)
        {
            HashTable = hashTable;
        }

        public bool IsDefaultValue() => HashTable.IsDefaultValue();

        public bool Contains(TKey key) => HashTable.Contains(key);

        public LookupDefaultEnumerator<TKey, TElement> GetEnumerator()
        {
            return new LookupDefaultEnumerator<TKey, TElement>(ref HashTable);
        }
    }
}
