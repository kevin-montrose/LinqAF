using System;
using System.Collections.Generic;

namespace LinqAF.Impl
{
    static partial class CommonImplementation
    {
        internal static EmptyEnumerable<TOutItem> EmptyZip_Impl<TItem, TOutItem, TSecondItemOut, TSecondEnumerable, TSecondEnumerator>(ref EmptyEnumerable<TItem> first, ref TSecondEnumerable second, Delegate resultSelector)
            where TSecondEnumerable : struct, IStructEnumerable<TSecondItemOut, TSecondEnumerator>
            where TSecondEnumerator : struct, IStructEnumerator<TSecondItemOut>
        {
            if (first.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(first));
            if (second.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(second));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TOutItem>.Empty;
        }

        internal static EmptyEnumerable<TOutItem> EmptyZip_Impl<TItem, TOutItem, TSecondItemOut, TSecondEnumerable, TSecondEnumerator>(ref EmptyOrderedEnumerable<TItem> first, ref TSecondEnumerable second, Delegate resultSelector)
            where TSecondEnumerable : struct, IStructEnumerable<TSecondItemOut, TSecondEnumerator>
            where TSecondEnumerator : struct, IStructEnumerator<TSecondItemOut>
        {
            if (first.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(first));
            if (second.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(second));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TOutItem>.Empty;
        }

        internal static EmptyEnumerable<TOutItem> EmptyZipBridge_Impl<TItem, TOutItem, TSecondItemOut, TBridgeEnumerable>(ref EmptyEnumerable<TItem> first, TBridgeEnumerable second, Delegate resultSelector)
            where TBridgeEnumerable: class, IEnumerable<TSecondItemOut>
        {
            if (first.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(first));
            if (second == null) throw new ArgumentException("Argument uninitialized", nameof(second));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TOutItem>.Empty;
        }

        internal static EmptyEnumerable<TOutItem> EmptyZipBridge_Impl<TItem, TOutItem, TSecondItemOut, TBridgeEnumerable>(ref EmptyOrderedEnumerable<TItem> first, TBridgeEnumerable second, Delegate resultSelector)
            where TBridgeEnumerable : class, IEnumerable<TSecondItemOut>
        {
            if (first.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(first));
            if (second == null) throw new ArgumentException("Argument uninitialized", nameof(second));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TOutItem>.Empty;
        }

        public static ZipEnumerable<TOutItem, TFirstItem, TSecondItem, TFirstEnumerable, TFirstEnumerator, TSecondEnumerable, TSecondEnumerator> Zip<TOutItem, TFirstItem, TSecondItem, TFirstEnumerable, TFirstEnumerator, TSecondEnumerable, TSecondEnumerator>(ref TFirstEnumerable first, ref TSecondEnumerable second, Func<TFirstItem, TSecondItem, TOutItem> resultSelector)
            where TFirstEnumerable : struct, IStructEnumerable<TFirstItem, TFirstEnumerator>
            where TFirstEnumerator : struct, IStructEnumerator<TFirstItem>
            where TSecondEnumerable : struct, IStructEnumerable<TSecondItem, TSecondEnumerator>
            where TSecondEnumerator : struct, IStructEnumerator<TSecondItem>
        {
            if (first.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(first));
            if (second.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(second));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return ZipImpl<TOutItem, TFirstItem, TSecondItem, TFirstEnumerable, TFirstEnumerator, TSecondEnumerable, TSecondEnumerator>(ref first, ref second, resultSelector);
        }

        internal static ZipEnumerable<TOutItem, TFirstItem, TSecondItem, TFirstEnumerable, TFirstEnumerator, TSecondEnumerable, TSecondEnumerator> ZipImpl<TOutItem, TFirstItem, TSecondItem, TFirstEnumerable, TFirstEnumerator, TSecondEnumerable, TSecondEnumerator>(ref TFirstEnumerable first, ref TSecondEnumerable second, Func<TFirstItem, TSecondItem, TOutItem> resultSelector)
            where TFirstEnumerable : struct, IStructEnumerable<TFirstItem, TFirstEnumerator>
            where TFirstEnumerator : struct, IStructEnumerator<TFirstItem>
            where TSecondEnumerable : struct, IStructEnumerable<TSecondItem, TSecondEnumerator>
            where TSecondEnumerator : struct, IStructEnumerator<TSecondItem>
        {
            return new ZipEnumerable<TOutItem, TFirstItem, TSecondItem, TFirstEnumerable, TFirstEnumerator, TSecondEnumerable, TSecondEnumerator>(ref first, ref second, resultSelector);
        }

        public static
            ZipEnumerable<
                TOutItem,
                TFirstItem,
                TSecondItem,
                TFirstEnumerable,
                TFirstEnumerator,
                IdentityEnumerable<TSecondItem, IEnumerable<TSecondItem>, IdentityEnumerator<TSecondItem>>,
                IdentityEnumerator<TSecondItem>
            > Zip<TOutItem, TFirstItem, TSecondItem, TFirstEnumerable, TFirstEnumerator>(
                ref TFirstEnumerable first,
                IEnumerable<TSecondItem> second,
                Func<TFirstItem, TSecondItem, TOutItem> resultSelector
            )
            where TFirstEnumerable : struct, IStructEnumerable<TFirstItem, TFirstEnumerator>
            where TFirstEnumerator : struct, IStructEnumerator<TFirstItem>
        {
            if (first.IsDefaultValue()) throw new ArgumentException("argument uninitialized", nameof(first));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            var secondIdent = Bridge(second, nameof(second));

            return new ZipEnumerable<TOutItem, TFirstItem, TSecondItem, TFirstEnumerable, TFirstEnumerator, IdentityEnumerable<TSecondItem, IEnumerable<TSecondItem>, IdentityEnumerator<TSecondItem>>, IdentityEnumerator<TSecondItem>>(ref first, ref secondIdent, resultSelector);
        }

        public static
            ZipEnumerable<
                TOutItem,
                TFirstItem,
                TSecondItem,
                TFirstEnumerable,
                TFirstEnumerator,
                IdentityEnumerable<TSecondItem, Dictionary<TSecondItem, TDictionaryValue>.KeyCollection, DictionaryKeysEnumerator<TSecondItem, TDictionaryValue>>,
                DictionaryKeysEnumerator<TSecondItem, TDictionaryValue>
            > Zip<TOutItem, TFirstItem, TSecondItem, TFirstEnumerable, TFirstEnumerator, TDictionaryValue>(
                ref TFirstEnumerable first,
                Dictionary<TSecondItem, TDictionaryValue>.KeyCollection second,
                Func<TFirstItem, TSecondItem, TOutItem> resultSelector
            )
            where TFirstEnumerable : struct, IStructEnumerable<TFirstItem, TFirstEnumerator>
            where TFirstEnumerator : struct, IStructEnumerator<TFirstItem>
        {
            if (first.IsDefaultValue()) throw new ArgumentException("argument uninitialized", nameof(first));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            var secondIdent = Bridge(second, nameof(second));

            return new ZipEnumerable<TOutItem, TFirstItem, TSecondItem, TFirstEnumerable, TFirstEnumerator, IdentityEnumerable<TSecondItem, Dictionary<TSecondItem, TDictionaryValue>.KeyCollection, DictionaryKeysEnumerator<TSecondItem, TDictionaryValue>>, DictionaryKeysEnumerator<TSecondItem, TDictionaryValue>>(ref first, ref secondIdent, resultSelector);
        }

        public static
            ZipEnumerable<
                TOutItem,
                TFirstItem,
                TSecondItem,
                TFirstEnumerable,
                TFirstEnumerator,
                IdentityEnumerable<TSecondItem, Dictionary<TDictionaryKey, TSecondItem>.ValueCollection, DictionaryValuesEnumerator<TDictionaryKey, TSecondItem>>,
                DictionaryValuesEnumerator<TDictionaryKey, TSecondItem>
            > Zip<TOutItem, TFirstItem, TSecondItem, TFirstEnumerable, TFirstEnumerator, TDictionaryKey>(
                ref TFirstEnumerable first,
                Dictionary<TDictionaryKey, TSecondItem>.ValueCollection second,
                Func<TFirstItem, TSecondItem, TOutItem> resultSelector
            )
            where TFirstEnumerable : struct, IStructEnumerable<TFirstItem, TFirstEnumerator>
            where TFirstEnumerator : struct, IStructEnumerator<TFirstItem>
        {
            if (first.IsDefaultValue()) throw new ArgumentException("argument uninitialized", nameof(first));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            var secondIdent = Bridge(second, nameof(second));

            return new ZipEnumerable<TOutItem, TFirstItem, TSecondItem, TFirstEnumerable, TFirstEnumerator, IdentityEnumerable<TSecondItem, Dictionary<TDictionaryKey, TSecondItem>.ValueCollection, DictionaryValuesEnumerator<TDictionaryKey, TSecondItem>>, DictionaryValuesEnumerator<TDictionaryKey, TSecondItem>>(ref first, ref secondIdent, resultSelector);
        }

        public static
            ZipEnumerable<
                TOutItem,
                TFirstItem,
                TSecondItem,
                TFirstEnumerable,
                TFirstEnumerator,
                IdentityEnumerable<TSecondItem, HashSet<TSecondItem>, HashSetEnumerator<TSecondItem>>,
                HashSetEnumerator<TSecondItem>
            > Zip<TOutItem, TFirstItem, TSecondItem, TFirstEnumerable, TFirstEnumerator>(
                ref TFirstEnumerable first,
                HashSet<TSecondItem> second,
                Func<TFirstItem, TSecondItem, TOutItem> resultSelector
            )
            where TFirstEnumerable : struct, IStructEnumerable<TFirstItem, TFirstEnumerator>
            where TFirstEnumerator : struct, IStructEnumerator<TFirstItem>
        {
            if (first.IsDefaultValue()) throw new ArgumentException("argument uninitialized", nameof(first));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            var secondIdent = Bridge(second, nameof(second));

            return new ZipEnumerable<TOutItem, TFirstItem, TSecondItem, TFirstEnumerable, TFirstEnumerator, IdentityEnumerable<TSecondItem, HashSet<TSecondItem>, HashSetEnumerator<TSecondItem>>, HashSetEnumerator<TSecondItem>>(ref first, ref secondIdent, resultSelector);
        }

        public static
            ZipEnumerable<
                TOutItem,
                TFirstItem,
                TSecondItem,
                TFirstEnumerable,
                TFirstEnumerator,
                IdentityEnumerable<TSecondItem, LinkedList<TSecondItem>, LinkedListEnumerator<TSecondItem>>,
                LinkedListEnumerator<TSecondItem>
            > Zip<TOutItem, TFirstItem, TSecondItem, TFirstEnumerable, TFirstEnumerator>(
                ref TFirstEnumerable first,
                LinkedList<TSecondItem> second,
                Func<TFirstItem, TSecondItem, TOutItem> resultSelector
            )
            where TFirstEnumerable : struct, IStructEnumerable<TFirstItem, TFirstEnumerator>
            where TFirstEnumerator : struct, IStructEnumerator<TFirstItem>
        {
            if (first.IsDefaultValue()) throw new ArgumentException("argument uninitialized", nameof(first));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            var secondIdent = Bridge(second, nameof(second));

            return new ZipEnumerable<TOutItem, TFirstItem, TSecondItem, TFirstEnumerable, TFirstEnumerator, IdentityEnumerable<TSecondItem, LinkedList<TSecondItem>, LinkedListEnumerator<TSecondItem>>, LinkedListEnumerator<TSecondItem>>(ref first, ref secondIdent, resultSelector);
        }

        public static
            ZipEnumerable<
                TOutItem,
                TFirstItem,
                TSecondItem,
                TFirstEnumerable,
                TFirstEnumerator,
                IdentityEnumerable<TSecondItem, List<TSecondItem>, ListEnumerator<TSecondItem>>,
                ListEnumerator<TSecondItem>
            > Zip<TOutItem, TFirstItem, TSecondItem, TFirstEnumerable, TFirstEnumerator>(
                ref TFirstEnumerable first,
                List<TSecondItem> second,
                Func<TFirstItem, TSecondItem, TOutItem> resultSelector
            )
            where TFirstEnumerable : struct, IStructEnumerable<TFirstItem, TFirstEnumerator>
            where TFirstEnumerator : struct, IStructEnumerator<TFirstItem>
        {
            if (first.IsDefaultValue()) throw new ArgumentException("argument uninitialized", nameof(first));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            var secondIdent = Bridge(second, nameof(second));

            return new ZipEnumerable<TOutItem, TFirstItem, TSecondItem, TFirstEnumerable, TFirstEnumerator, IdentityEnumerable<TSecondItem, List<TSecondItem>, ListEnumerator<TSecondItem>>, ListEnumerator<TSecondItem>>(ref first, ref secondIdent, resultSelector);
        }

        public static
            ZipEnumerable<
                TOutItem,
                TFirstItem,
                TSecondItem,
                TFirstEnumerable,
                TFirstEnumerator,
                IdentityEnumerable<TSecondItem, Queue<TSecondItem>, QueueEnumerator<TSecondItem>>,
                QueueEnumerator<TSecondItem>
            > Zip<TOutItem, TFirstItem, TSecondItem, TFirstEnumerable, TFirstEnumerator>(
                ref TFirstEnumerable first,
                Queue<TSecondItem> second,
                Func<TFirstItem, TSecondItem, TOutItem> resultSelector
            )
            where TFirstEnumerable : struct, IStructEnumerable<TFirstItem, TFirstEnumerator>
            where TFirstEnumerator : struct, IStructEnumerator<TFirstItem>
        {
            if (first.IsDefaultValue()) throw new ArgumentException("argument uninitialized", nameof(first));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            var secondIdent = Bridge(second, nameof(second));

            return new ZipEnumerable<TOutItem, TFirstItem, TSecondItem, TFirstEnumerable, TFirstEnumerator, IdentityEnumerable<TSecondItem, Queue<TSecondItem>, QueueEnumerator<TSecondItem>>, QueueEnumerator<TSecondItem>>(ref first, ref secondIdent, resultSelector);
        }

        public static
            ZipEnumerable<
                TOutItem,
                TFirstItem,
                TSecondItem,
                TFirstEnumerable,
                TFirstEnumerator,
                IdentityEnumerable<TSecondItem, SortedDictionary<TSecondItem, TDictionaryValue>.KeyCollection, SortedDictionaryKeysEnumerator<TSecondItem, TDictionaryValue>>,
                SortedDictionaryKeysEnumerator<TSecondItem, TDictionaryValue>
            > Zip<TOutItem, TFirstItem, TSecondItem, TFirstEnumerable, TFirstEnumerator, TDictionaryValue>(
                ref TFirstEnumerable first,
                SortedDictionary<TSecondItem, TDictionaryValue>.KeyCollection second,
                Func<TFirstItem, TSecondItem, TOutItem> resultSelector
            )
            where TFirstEnumerable : struct, IStructEnumerable<TFirstItem, TFirstEnumerator>
            where TFirstEnumerator : struct, IStructEnumerator<TFirstItem>
        {
            if (first.IsDefaultValue()) throw new ArgumentException("argument uninitialized", nameof(first));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            var secondIdent = Bridge(second, nameof(second));

            return new ZipEnumerable<TOutItem, TFirstItem, TSecondItem, TFirstEnumerable, TFirstEnumerator, IdentityEnumerable<TSecondItem, SortedDictionary<TSecondItem, TDictionaryValue>.KeyCollection, SortedDictionaryKeysEnumerator<TSecondItem, TDictionaryValue>>, SortedDictionaryKeysEnumerator<TSecondItem, TDictionaryValue>>(ref first, ref secondIdent, resultSelector);
        }

        public static
            ZipEnumerable<
                TOutItem,
                TFirstItem,
                TSecondItem,
                TFirstEnumerable,
                TFirstEnumerator,
                IdentityEnumerable<TSecondItem, SortedDictionary<TDictionaryKey, TSecondItem>.ValueCollection, SortedDictionaryValuesEnumerator<TDictionaryKey, TSecondItem>>,
                SortedDictionaryValuesEnumerator<TDictionaryKey, TSecondItem>
            > Zip<TOutItem, TFirstItem, TSecondItem, TFirstEnumerable, TFirstEnumerator, TDictionaryKey>(
                ref TFirstEnumerable first,
                SortedDictionary<TDictionaryKey, TSecondItem>.ValueCollection second,
                Func<TFirstItem, TSecondItem, TOutItem> resultSelector
            )
            where TFirstEnumerable : struct, IStructEnumerable<TFirstItem, TFirstEnumerator>
            where TFirstEnumerator : struct, IStructEnumerator<TFirstItem>
        {
            if (first.IsDefaultValue()) throw new ArgumentException("argument uninitialized", nameof(first));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            var secondIdent = Bridge(second, nameof(second));

            return new ZipEnumerable<TOutItem, TFirstItem, TSecondItem, TFirstEnumerable, TFirstEnumerator, IdentityEnumerable<TSecondItem, SortedDictionary<TDictionaryKey, TSecondItem>.ValueCollection, SortedDictionaryValuesEnumerator<TDictionaryKey, TSecondItem>>, SortedDictionaryValuesEnumerator<TDictionaryKey, TSecondItem>>(ref first, ref secondIdent, resultSelector);
        }

        public static
            ZipEnumerable<
                TOutItem,
                TFirstItem,
                TSecondItem,
                TFirstEnumerable,
                TFirstEnumerator,
                IdentityEnumerable<TSecondItem, SortedSet<TSecondItem>, SortedSetEnumerator<TSecondItem>>,
                SortedSetEnumerator<TSecondItem>
            > Zip<TOutItem, TFirstItem, TSecondItem, TFirstEnumerable, TFirstEnumerator>(
                ref TFirstEnumerable first,
                SortedSet<TSecondItem> second,
                Func<TFirstItem, TSecondItem, TOutItem> resultSelector
            )
            where TFirstEnumerable : struct, IStructEnumerable<TFirstItem, TFirstEnumerator>
            where TFirstEnumerator : struct, IStructEnumerator<TFirstItem>
        {
            if (first.IsDefaultValue()) throw new ArgumentException("argument uninitialized", nameof(first));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            var secondIdent = Bridge(second, nameof(second));

            return new ZipEnumerable<TOutItem, TFirstItem, TSecondItem, TFirstEnumerable, TFirstEnumerator, IdentityEnumerable<TSecondItem, SortedSet<TSecondItem>, SortedSetEnumerator<TSecondItem>>, SortedSetEnumerator<TSecondItem>>(ref first, ref secondIdent, resultSelector);
        }

        public static
            ZipEnumerable<
                TOutItem,
                TFirstItem,
                TSecondItem,
                TFirstEnumerable,
                TFirstEnumerator,
                IdentityEnumerable<TSecondItem, Stack<TSecondItem>, StackEnumerator<TSecondItem>>,
                StackEnumerator<TSecondItem>
            > Zip<TOutItem, TFirstItem, TSecondItem, TFirstEnumerable, TFirstEnumerator>(
                ref TFirstEnumerable first,
                Stack<TSecondItem> second,
                Func<TFirstItem, TSecondItem, TOutItem> resultSelector
            )
            where TFirstEnumerable : struct, IStructEnumerable<TFirstItem, TFirstEnumerator>
            where TFirstEnumerator : struct, IStructEnumerator<TFirstItem>
        {
            if (first.IsDefaultValue()) throw new ArgumentException("argument uninitialized", nameof(first));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            var secondIdent = Bridge(second, nameof(second));

            return new ZipEnumerable<TOutItem, TFirstItem, TSecondItem, TFirstEnumerable, TFirstEnumerator, IdentityEnumerable<TSecondItem, Stack<TSecondItem>, StackEnumerator<TSecondItem>>, StackEnumerator<TSecondItem>>(ref first, ref secondIdent, resultSelector);
        }

        public static
            ZipEnumerable<
                TOutItem,
                TFirstItem,
                TSecondItem,
                TFirstEnumerable,
                TFirstEnumerator,
                IdentityEnumerable<TSecondItem, TSecondItem[], ArrayEnumerator<TSecondItem>>,
                ArrayEnumerator<TSecondItem>
            > Zip<TOutItem, TFirstItem, TSecondItem, TFirstEnumerable, TFirstEnumerator>(
                ref TFirstEnumerable first,
                TSecondItem[] second,
                Func<TFirstItem, TSecondItem, TOutItem> resultSelector
            )
            where TFirstEnumerable : struct, IStructEnumerable<TFirstItem, TFirstEnumerator>
            where TFirstEnumerator : struct, IStructEnumerator<TFirstItem>
        {
            if (first.IsDefaultValue()) throw new ArgumentException("argument uninitialized", nameof(first));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            var secondIdent = Bridge(second, nameof(second));

            return new ZipEnumerable<TOutItem, TFirstItem, TSecondItem, TFirstEnumerable, TFirstEnumerator, IdentityEnumerable<TSecondItem, TSecondItem[], ArrayEnumerator<TSecondItem>>, ArrayEnumerator<TSecondItem>>(ref first, ref secondIdent, resultSelector);
        }
    }
}
