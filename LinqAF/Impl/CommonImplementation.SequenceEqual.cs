using System;
using System.Collections.Generic;

namespace LinqAF.Impl
{
    static partial class CommonImplementation
    {
        public static bool SequenceEqual<TItem, TEnumerable, TEnumerator>(ref TEnumerable first, IEnumerable<TItem> second, IEqualityComparer<TItem> comparer)
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            var ident = Bridge(second, nameof(second));

            return SequenceEqual<TItem, TEnumerable, TEnumerator, IdentityEnumerable<TItem, IEnumerable<TItem>, IEnumerableBridger<TItem>, IdentityEnumerator<TItem>>, IdentityEnumerator<TItem>>(ref first, ref ident, comparer);
        }

        public static bool SequenceEqual<TItem, TDictionaryValue, TEnumerable, TEnumerator>(ref TEnumerable first, Dictionary<TItem, TDictionaryValue>.KeyCollection second, IEqualityComparer<TItem> comparer)
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            var ident = Bridge(second, nameof(second));

            return SequenceEqual<TItem, TEnumerable, TEnumerator, IdentityEnumerable<TItem, Dictionary<TItem, TDictionaryValue>.KeyCollection, DictionaryKeysBridger<TItem, TDictionaryValue>, DictionaryKeysEnumerator<TItem, TDictionaryValue>>, DictionaryKeysEnumerator<TItem, TDictionaryValue>>(ref first, ref ident, comparer);
        }

        public static bool SequenceEqual<TItem, TDictionaryKey, TEnumerable, TEnumerator>(ref TEnumerable first, Dictionary<TDictionaryKey, TItem>.ValueCollection second, IEqualityComparer<TItem> comparer)
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            var ident = Bridge(second, nameof(second));

            return SequenceEqual<TItem, TEnumerable, TEnumerator, IdentityEnumerable<TItem, Dictionary<TDictionaryKey, TItem>.ValueCollection, DictionaryValuesBridger<TDictionaryKey, TItem>, DictionaryValuesEnumerator<TDictionaryKey, TItem>>, DictionaryValuesEnumerator<TDictionaryKey, TItem>>(ref first, ref ident, comparer);
        }

        public static bool SequenceEqual<TItem, TEnumerable, TEnumerator>(ref TEnumerable first, HashSet<TItem> second, IEqualityComparer<TItem> comparer)
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            var ident = Bridge(second, nameof(second));

            return SequenceEqual<TItem, TEnumerable, TEnumerator, IdentityEnumerable<TItem, HashSet<TItem>, HashSetBridger<TItem>, HashSetEnumerator<TItem>>, HashSetEnumerator<TItem>>(ref first, ref ident, comparer);
        }

        public static bool SequenceEqual<TItem, TEnumerable, TEnumerator>(ref TEnumerable first, LinkedList<TItem> second, IEqualityComparer<TItem> comparer)
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            var ident = Bridge(second, nameof(second));

            return SequenceEqual<TItem, TEnumerable, TEnumerator, IdentityEnumerable<TItem, LinkedList<TItem>, LinkedListBridger<TItem>, LinkedListEnumerator<TItem>>, LinkedListEnumerator<TItem>>(ref first, ref ident, comparer);
        }

        public static bool SequenceEqual<TItem, TEnumerable, TEnumerator>(ref TEnumerable first, Queue<TItem> second, IEqualityComparer<TItem> comparer)
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            var ident = Bridge(second, nameof(second));

            return SequenceEqual<TItem, TEnumerable, TEnumerator, IdentityEnumerable<TItem, Queue<TItem>, QueueBridger<TItem>, QueueEnumerator<TItem>>, QueueEnumerator<TItem>>(ref first, ref ident, comparer);
        }

        public static bool SequenceEqual<TItem, TDictionaryValue, TEnumerable, TEnumerator>(ref TEnumerable first, SortedDictionary<TItem, TDictionaryValue>.KeyCollection second, IEqualityComparer<TItem> comparer)
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            var ident = Bridge(second, nameof(second));

            return SequenceEqual<TItem, TEnumerable, TEnumerator, IdentityEnumerable<TItem, SortedDictionary<TItem, TDictionaryValue>.KeyCollection, SortedDictionaryKeysBridger<TItem, TDictionaryValue>, SortedDictionaryKeysEnumerator<TItem, TDictionaryValue>>, SortedDictionaryKeysEnumerator<TItem, TDictionaryValue>>(ref first, ref ident, comparer);
        }

        public static bool SequenceEqual<TItem, TDictionaryKey, TEnumerable, TEnumerator>(ref TEnumerable first, SortedDictionary<TDictionaryKey, TItem>.ValueCollection second, IEqualityComparer<TItem> comparer)
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            var ident = Bridge(second, nameof(second));

            return SequenceEqual<TItem, TEnumerable, TEnumerator, IdentityEnumerable<TItem, SortedDictionary<TDictionaryKey, TItem>.ValueCollection, SortedDictionaryValuesBridger<TDictionaryKey, TItem>, SortedDictionaryValuesEnumerator<TDictionaryKey, TItem>>, SortedDictionaryValuesEnumerator<TDictionaryKey, TItem>>(ref first, ref ident, comparer);
        }

        public static bool SequenceEqual<TItem, TEnumerable, TEnumerator>(ref TEnumerable first, SortedSet<TItem> second, IEqualityComparer<TItem> comparer)
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            var ident = Bridge(second, nameof(second));

            return SequenceEqual<TItem, TEnumerable, TEnumerator, IdentityEnumerable<TItem, SortedSet<TItem>, SortedSetBridger<TItem>, SortedSetEnumerator<TItem>>, SortedSetEnumerator<TItem>>(ref first, ref ident, comparer);
        }

        public static bool SequenceEqual<TItem, TEnumerable, TEnumerator>(ref TEnumerable first, Stack<TItem> second, IEqualityComparer<TItem> comparer)
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            var ident = Bridge(second, nameof(second));

            return SequenceEqual<TItem, TEnumerable, TEnumerator, IdentityEnumerable<TItem, Stack<TItem>, StackBridger<TItem>, StackEnumerator<TItem>>, StackEnumerator<TItem>>(ref first, ref ident, comparer);
        }

        public static bool SequenceEqual<TItem, TFirstEnumerable, TFirstEnumerator, TSecondEnumerable, TSecondEnumerator>(ref TFirstEnumerable first, ref TSecondEnumerable second, IEqualityComparer<TItem> comparer)
            where TFirstEnumerable : struct, IStructEnumerable<TItem, TFirstEnumerator>
            where TFirstEnumerator : struct, IStructEnumerator<TItem>
            where TSecondEnumerable : struct, IStructEnumerable<TItem, TSecondEnumerator>
            where TSecondEnumerator : struct, IStructEnumerator<TItem>
        {
            if (first.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(first));
            if (second.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(second));

            return SequenceEqualImpl<TItem, TFirstEnumerable, TFirstEnumerator, TSecondEnumerable, TSecondEnumerator>(ref first, ref second, comparer);
        }

        internal static bool SequenceEqualImpl<TItem, TFirstEnumerable, TFirstEnumerator, TSecondEnumerable, TSecondEnumerator>(ref TFirstEnumerable first, ref TSecondEnumerable second, IEqualityComparer<TItem> comparer)
            where TFirstEnumerable : struct, IStructEnumerable<TItem, TFirstEnumerator>
            where TFirstEnumerator : struct, IStructEnumerator<TItem>
            where TSecondEnumerable : struct, IStructEnumerable<TItem, TSecondEnumerator>
            where TSecondEnumerator : struct, IStructEnumerator<TItem>
        {
            comparer = comparer ?? EqualityComparer<TItem>.Default;

            using (var fi = first.GetEnumerator())
            using (var si = second.GetEnumerator())
            {
                while (true)
                {
                    var fMoved = fi.MoveNext();
                    var sMoved = si.MoveNext();

                    // one moved, but the other didn't, freak out
                    if (fMoved != sMoved) return false;

                    // we hit the end of the stream, success!
                    if (!fMoved) return true;

                    var fCur = fi.Current;
                    var sCur = si.Current;

                    // not equal, freak out!
                    if (!comparer.Equals(fCur, sCur)) return false;
                }
            }
        }

        internal static bool SequenceEqualImpl<TItem, TFirstEnumerable, TFirstEnumerator, TSecondEnumerable, TSecondEnumerator>(ref TFirstEnumerable first, ref TSecondEnumerable second)
            where TFirstEnumerable : struct, IStructEnumerable<TItem, TFirstEnumerator>
            where TFirstEnumerator : struct, IStructEnumerator<TItem>
            where TSecondEnumerable : struct, IStructEnumerable<TItem, TSecondEnumerator>
            where TSecondEnumerator : struct, IStructEnumerator<TItem>
        {
            var comparer = EqualityComparer<TItem>.Default;

            using (var fi = first.GetEnumerator())
            using (var si = second.GetEnumerator())
            {
                while (true)
                {
                    var fMoved = fi.MoveNext();
                    var sMoved = si.MoveNext();

                    // one moved, but the other didn't, freak out
                    if (fMoved != sMoved) return false;

                    // we hit the end of the stream, success!
                    if (!fMoved) return true;

                    var fCur = fi.Current;
                    var sCur = si.Current;

                    // not equal, freak out!
                    if (!comparer.Equals(fCur, sCur)) return false;
                }
            }
        }
    }
}
