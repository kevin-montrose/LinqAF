using LinqAF.Impl;
using System;
using System.Collections.Generic;

namespace LinqAF
{
    partial struct EmptyEnumerable<TItem>
    {
        public bool SequenceEqual<TSequenceEqual_DefaultIfEmptyInnerEnumerable, TSequenceEqual_DefaultIfEmptyInnerEnumerator>(DefaultIfEmptyDefaultEnumerable<TItem, TSequenceEqual_DefaultIfEmptyInnerEnumerable, TSequenceEqual_DefaultIfEmptyInnerEnumerator> second)
            where TSequenceEqual_DefaultIfEmptyInnerEnumerable : struct, IStructEnumerable<TItem, TSequenceEqual_DefaultIfEmptyInnerEnumerator>
            where TSequenceEqual_DefaultIfEmptyInnerEnumerator : struct, IStructEnumerator<TItem>
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            return false;
        }

        public bool SequenceEqual<TSequenceEqual_DefaultIfEmptyInnerEnumerable, TSequenceEqual_DefaultIfEmptyInnerEnumerator>(DefaultIfEmptyDefaultEnumerable<TItem, TSequenceEqual_DefaultIfEmptyInnerEnumerable, TSequenceEqual_DefaultIfEmptyInnerEnumerator> second, IEqualityComparer<TItem> comparer)
            where TSequenceEqual_DefaultIfEmptyInnerEnumerable : struct, IStructEnumerable<TItem, TSequenceEqual_DefaultIfEmptyInnerEnumerator>
            where TSequenceEqual_DefaultIfEmptyInnerEnumerator : struct, IStructEnumerator<TItem>
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            return false;
        }

        public bool SequenceEqual<TSequenceEqual_DefaultIfEmptyInnerEnumerable, TSequenceEqual_DefaultIfEmptyInnerEnumerator>(DefaultIfEmptySpecificEnumerable<TItem, TSequenceEqual_DefaultIfEmptyInnerEnumerable, TSequenceEqual_DefaultIfEmptyInnerEnumerator> second)
            where TSequenceEqual_DefaultIfEmptyInnerEnumerable : struct, IStructEnumerable<TItem, TSequenceEqual_DefaultIfEmptyInnerEnumerator>
            where TSequenceEqual_DefaultIfEmptyInnerEnumerator : struct, IStructEnumerator<TItem>
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            return false;
        }

        public bool SequenceEqual<TSequenceEqual_DefaultIfEmptyInnerEnumerable, TSequenceEqual_DefaultIfEmptyInnerEnumerator>(DefaultIfEmptySpecificEnumerable<TItem, TSequenceEqual_DefaultIfEmptyInnerEnumerable, TSequenceEqual_DefaultIfEmptyInnerEnumerator> second, IEqualityComparer<TItem> comparer)
            where TSequenceEqual_DefaultIfEmptyInnerEnumerable : struct, IStructEnumerable<TItem, TSequenceEqual_DefaultIfEmptyInnerEnumerator>
            where TSequenceEqual_DefaultIfEmptyInnerEnumerator : struct, IStructEnumerator<TItem>
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            return false;
        }

        public bool SequenceEqual(EmptyEnumerable<TItem> second)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            return true;
        }

        public bool SequenceEqual(EmptyEnumerable<TItem> second, IEqualityComparer<TItem> comparer)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            return true;
        }

        public bool SequenceEqual(EmptyOrderedEnumerable<TItem> second)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            return true;
        }

        public bool SequenceEqual(EmptyOrderedEnumerable<TItem> second, IEqualityComparer<TItem> comparer)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            return true;
        }

        public bool SequenceEqual(OneItemDefaultEnumerable<TItem> second)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            return false;
        }

        public bool SequenceEqual(OneItemDefaultEnumerable<TItem> second, IEqualityComparer<TItem> comparer)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            return false;
        }

        public bool SequenceEqual(OneItemSpecificEnumerable<TItem> second)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            return false;
        }

        public bool SequenceEqual(OneItemSpecificEnumerable<TItem> second, IEqualityComparer<TItem> comparer)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            return false;
        }

        public bool SequenceEqual(OneItemDefaultOrderedEnumerable<TItem> second)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            return false;
        }

        public bool SequenceEqual(OneItemDefaultOrderedEnumerable<TItem> second, IEqualityComparer<TItem> comparer)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            return false;
        }

        public bool SequenceEqual(OneItemSpecificOrderedEnumerable<TItem> second)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            return false;
        }

        public bool SequenceEqual(OneItemSpecificOrderedEnumerable<TItem> second, IEqualityComparer<TItem> comparer)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            return false;
        }

        public bool SequenceEqual<TSequenceEqual_DictionaryValue>(Dictionary<TItem, TSequenceEqual_DictionaryValue>.KeyCollection second, IEqualityComparer<TItem> comparer)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            if (second == null) throw CommonImplementation.ArgumentNull(nameof(second));

            return second.Count == 0;
        }

        public bool SequenceEqual<TSequenceEqual_DictionaryKey>(Dictionary<TSequenceEqual_DictionaryKey, TItem>.ValueCollection second, IEqualityComparer<TItem> comparer)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            if (second == null) throw CommonImplementation.ArgumentNull(nameof(second));

            return second.Count == 0;
        }

        public bool SequenceEqual(HashSet<TItem> second, IEqualityComparer<TItem> comparer)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            if (second == null) throw CommonImplementation.ArgumentNull(nameof(second));

            return second.Count == 0;
        }

        public bool SequenceEqual(LinkedList<TItem> second, IEqualityComparer<TItem> comparer)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            if (second == null) throw CommonImplementation.ArgumentNull(nameof(second));

            return second.Count == 0;
        }

        public bool SequenceEqual(List<TItem> second, IEqualityComparer<TItem> comparer)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            if (second == null) throw CommonImplementation.ArgumentNull(nameof(second));

            return second.Count == 0;
        }

        public bool SequenceEqual(Queue<TItem> second, IEqualityComparer<TItem> comparer)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            if (second == null) throw CommonImplementation.ArgumentNull(nameof(second));

            return second.Count == 0;
        }

        public bool SequenceEqual<TSequenceEqual_DictionaryValue>(SortedDictionary<TItem, TSequenceEqual_DictionaryValue>.KeyCollection second, IEqualityComparer<TItem> comparer)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            if (second == null) throw CommonImplementation.ArgumentNull(nameof(second));

            return second.Count == 0;
        }

        public bool SequenceEqual<TSequenceEqual_DictionaryKey>(SortedDictionary<TSequenceEqual_DictionaryKey, TItem>.ValueCollection second, IEqualityComparer<TItem> comparer)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            if (second == null) throw CommonImplementation.ArgumentNull(nameof(second));

            return second.Count == 0;
        }

        public bool SequenceEqual(SortedSet<TItem> second, IEqualityComparer<TItem> comparer)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            if (second == null) throw CommonImplementation.ArgumentNull(nameof(second));

            return second.Count == 0;
        }

        public bool SequenceEqual(Stack<TItem> second, IEqualityComparer<TItem> comparer)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            if (second == null) throw CommonImplementation.ArgumentNull(nameof(second));

            return second.Count == 0;
        }

        public bool SequenceEqual<TSequenceEqual_DictionaryValue>(Dictionary<TItem, TSequenceEqual_DictionaryValue>.KeyCollection second)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            if (second == null) throw CommonImplementation.ArgumentNull(nameof(second));

            return second.Count == 0;
        }

        public bool SequenceEqual<TSequenceEqual_DictionaryKey>(Dictionary<TSequenceEqual_DictionaryKey, TItem>.ValueCollection second)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            if (second == null) throw CommonImplementation.ArgumentNull(nameof(second));

            return second.Count == 0;
        }

        public bool SequenceEqual(HashSet<TItem> second)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            if (second == null) throw CommonImplementation.ArgumentNull(nameof(second));

            return second.Count == 0;
        }

        public bool SequenceEqual(LinkedList<TItem> second)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            if (second == null) throw CommonImplementation.ArgumentNull(nameof(second));

            return second.Count == 0;
        }

        public bool SequenceEqual(List<TItem> second)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            if (second == null) throw CommonImplementation.ArgumentNull(nameof(second));

            return second.Count == 0;
        }

        public bool SequenceEqual(Queue<TItem> second)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            if (second == null) throw CommonImplementation.ArgumentNull(nameof(second));

            return second.Count == 0;
        }

        public bool SequenceEqual<TSequenceEqual_DictionaryValue>(SortedDictionary<TItem, TSequenceEqual_DictionaryValue>.KeyCollection second)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            if (second == null) throw CommonImplementation.ArgumentNull(nameof(second));

            return second.Count == 0;
        }

        public bool SequenceEqual<TSequenceEqual_DictionaryKey>(SortedDictionary<TSequenceEqual_DictionaryKey, TItem>.ValueCollection second)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            if (second == null) throw CommonImplementation.ArgumentNull(nameof(second));

            return second.Count == 0;
        }

        public bool SequenceEqual(SortedSet<TItem> second)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            if (second == null) throw CommonImplementation.ArgumentNull(nameof(second));

            return second.Count == 0;
        }

        public bool SequenceEqual(Stack<TItem> second)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            if (second == null) throw CommonImplementation.ArgumentNull(nameof(second));

            return second.Count == 0;
        }

        public bool SequenceEqual(TItem[] second)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            if (second == null) throw CommonImplementation.ArgumentNull(nameof(second));

            return second.Length == 0;
        }

        public bool SequenceEqual(TItem[] second, IEqualityComparer<TItem> comparer)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            if (second == null) throw CommonImplementation.ArgumentNull(nameof(second));

            return second.Length == 0;
        }
        
        public bool SequenceEqual(RepeatEnumerable<TItem> second)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            return second.InnerCount == 0;
        }

        public bool SequenceEqual(RepeatEnumerable<TItem> second, IEqualityComparer<TItem> comparer)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("first");
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            return second.InnerCount == 0;
        }
    }
}
