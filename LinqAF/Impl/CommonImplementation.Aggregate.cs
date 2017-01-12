using System;
using System.Collections.Generic;
using LinqAF;

namespace LinqAF.Impl
{
    static partial class CommonImplementation
    {
        // single item

        public static TItem Aggregate<TItem, TEnumerable, TEnumerator>(ref TEnumerable source, Func<TItem, TItem, TItem> func)
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            if (source.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(source));
            if (func == null) throw new ArgumentNullException(nameof(func));

            return AggregateImpl<TItem, TEnumerable, TEnumerator>(ref source, func);
        }

        internal static TItem AggregateImpl<TItem, TEnumerable, TEnumerator>(ref TEnumerable source, Func<TItem, TItem, TItem> func)
            where TEnumerable : struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItem>
        {
            using (var i = source.GetEnumerator())
            {
                if (!i.MoveNext()) throw new InvalidOperationException("Sequence was empty");

                var ret = i.Current;

                while (i.MoveNext())
                {
                    ret = func(ret, i.Current);
                }

                return ret;
            }
        }

        // two items
        
        public static TItemOut Aggregate<TItemIn, TItemOut, TEnumerable, TEnumerator>(ref TEnumerable source, TItemOut seed, Func<TItemOut, TItemIn, TItemOut> func)
            where TEnumerable : struct, IStructEnumerable<TItemIn, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItemIn>
        {
            if (source.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(source));
            if (func == null) throw new ArgumentNullException(nameof(func));

            return AggregateImpl<TItemIn, TItemOut, TEnumerable, TEnumerator>(ref source, seed, func);
        }

        internal static TItemOut AggregateImpl<TItemIn, TItemOut, TEnumerable, TEnumerator>(ref TEnumerable source, TItemOut seed, Func<TItemOut, TItemIn, TItemOut> func)
            where TEnumerable : struct, IStructEnumerable<TItemIn, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItemIn>
        {
            var ret = seed;

            using (var i = source.GetEnumerator())
            {
                while (i.MoveNext())
                {
                    ret = func(ret, i.Current);
                }
            }

            return ret;
        }

        // three items
        public static TItemOut Aggregate<TItemIn, TItemMid, TItemOut, TDictionaryValue>(Dictionary<TItemIn, TDictionaryValue>.KeyCollection source, TItemMid seed, Func<TItemMid, TItemIn, TItemMid> func, Func<TItemMid, TItemOut> resultSelector)
        {
            var bridge = Bridge(source, nameof(source));
            if (func == null) throw new ArgumentNullException(nameof(func));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return AggregateImpl<
                TItemIn,
                TItemMid,
                TItemOut,
                IdentityEnumerable<
                    TItemIn,
                    Dictionary<TItemIn, TDictionaryValue>.KeyCollection,
                    DictionaryKeysEnumerator<TItemIn, TDictionaryValue>
                >,
                DictionaryKeysEnumerator<TItemIn, TDictionaryValue>
            >(ref bridge, seed, func, resultSelector);
        }

        public static TItemOut Aggregate<TItemIn, TItemMid, TItemOut, TDictionaryKey>(Dictionary<TDictionaryKey, TItemIn>.ValueCollection source, TItemMid seed, Func<TItemMid, TItemIn, TItemMid> func, Func<TItemMid, TItemOut> resultSelector)
        {
            var bridge = Bridge(source, nameof(source));
            if (func == null) throw new ArgumentNullException(nameof(func));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return AggregateImpl<
                TItemIn,
                TItemMid,
                TItemOut,
                IdentityEnumerable<
                    TItemIn,
                    Dictionary<TDictionaryKey, TItemIn>.ValueCollection,
                    DictionaryValuesEnumerator<TDictionaryKey, TItemIn>
                >,
                DictionaryValuesEnumerator<TDictionaryKey, TItemIn>
            >(ref bridge, seed, func, resultSelector);
        }

        public static TItemOut Aggregate<TItemIn, TItemMid, TItemOut>(HashSet<TItemIn> source, TItemMid seed, Func<TItemMid, TItemIn, TItemMid> func, Func<TItemMid, TItemOut> resultSelector)
        {
            var bridge = Bridge(source, nameof(source));
            if (func == null) throw new ArgumentNullException(nameof(func));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return AggregateImpl<
                TItemIn,
                TItemMid,
                TItemOut,
                IdentityEnumerable<
                    TItemIn,
                    HashSet<TItemIn>,
                    HashSetEnumerator<TItemIn>
                >,
                HashSetEnumerator<TItemIn>
            >(ref bridge, seed, func, resultSelector);
        }

        public static TItemOut Aggregate<TItemIn, TItemMid, TItemOut>(LinkedList<TItemIn> source, TItemMid seed, Func<TItemMid, TItemIn, TItemMid> func, Func<TItemMid, TItemOut> resultSelector)
        {
            var bridge = Bridge(source, nameof(source));
            if (func == null) throw new ArgumentNullException(nameof(func));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return AggregateImpl<
                TItemIn,
                TItemMid,
                TItemOut,
                IdentityEnumerable<
                    TItemIn,
                    LinkedList<TItemIn>,
                    LinkedListEnumerator<TItemIn>
                >,
                LinkedListEnumerator<TItemIn>
            >(ref bridge, seed, func, resultSelector);
        }

        public static TItemOut Aggregate<TItemIn, TItemMid, TItemOut>(List<TItemIn> source, TItemMid seed, Func<TItemMid, TItemIn, TItemMid> func, Func<TItemMid, TItemOut> resultSelector)
        {
            var bridge = Bridge(source, nameof(source));
            if (func == null) throw new ArgumentNullException(nameof(func));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return AggregateImpl<
                TItemIn,
                TItemMid,
                TItemOut,
                IdentityEnumerable<
                    TItemIn,
                    List<TItemIn>,
                    ListEnumerator<TItemIn>
                >,
                ListEnumerator<TItemIn>
            >(ref bridge, seed, func, resultSelector);
        }

        public static TItemOut Aggregate<TItemIn, TItemMid, TItemOut>(Queue<TItemIn> source, TItemMid seed, Func<TItemMid, TItemIn, TItemMid> func, Func<TItemMid, TItemOut> resultSelector)
        {
            var bridge = Bridge(source, nameof(source));
            if (func == null) throw new ArgumentNullException(nameof(func));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return AggregateImpl<
                TItemIn,
                TItemMid,
                TItemOut,
                IdentityEnumerable<
                    TItemIn,
                    Queue<TItemIn>,
                    QueueEnumerator<TItemIn>
                >,
                QueueEnumerator<TItemIn>
            >(ref bridge, seed, func, resultSelector);
        }

        public static TItemOut Aggregate<TItemIn, TItemMid, TItemOut, TDictionaryValue>(SortedDictionary<TItemIn, TDictionaryValue>.KeyCollection source, TItemMid seed, Func<TItemMid, TItemIn, TItemMid> func, Func<TItemMid, TItemOut> resultSelector)
        {
            var bridge = Bridge(source, nameof(source));
            if (func == null) throw new ArgumentNullException(nameof(func));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return AggregateImpl<
                TItemIn,
                TItemMid,
                TItemOut,
                IdentityEnumerable<
                    TItemIn,
                    SortedDictionary<TItemIn, TDictionaryValue>.KeyCollection,
                    SortedDictionaryKeysEnumerator<TItemIn, TDictionaryValue>
                >,
                SortedDictionaryKeysEnumerator<TItemIn, TDictionaryValue>
            >(ref bridge, seed, func, resultSelector);
        }

        public static TItemOut Aggregate<TItemIn, TItemMid, TItemOut, TDictionaryKey>(SortedDictionary<TDictionaryKey, TItemIn>.ValueCollection source, TItemMid seed, Func<TItemMid, TItemIn, TItemMid> func, Func<TItemMid, TItemOut> resultSelector)
        {
            var bridge = Bridge(source, nameof(source));
            if (func == null) throw new ArgumentNullException(nameof(func));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return AggregateImpl<
                TItemIn,
                TItemMid,
                TItemOut,
                IdentityEnumerable<
                    TItemIn,
                    SortedDictionary<TDictionaryKey, TItemIn>.ValueCollection,
                    SortedDictionaryValuesEnumerator<TDictionaryKey, TItemIn>
                >,
                SortedDictionaryValuesEnumerator<TDictionaryKey, TItemIn>
            >(ref bridge, seed, func, resultSelector);
        }

        public static TItemOut Aggregate<TItemIn, TItemMid, TItemOut>(SortedSet<TItemIn> source, TItemMid seed, Func<TItemMid, TItemIn, TItemMid> func, Func<TItemMid, TItemOut> resultSelector)
        {
            var bridge = Bridge(source, nameof(source));
            if (func == null) throw new ArgumentNullException(nameof(func));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return AggregateImpl<
                TItemIn,
                TItemMid,
                TItemOut,
                IdentityEnumerable<
                    TItemIn,
                    SortedSet<TItemIn>,
                    SortedSetEnumerator<TItemIn>
                >,
                SortedSetEnumerator<TItemIn>
            >(ref bridge, seed, func, resultSelector);
        }

        public static TItemOut Aggregate<TItemIn, TItemMid, TItemOut>(Stack<TItemIn> source, TItemMid seed, Func<TItemMid, TItemIn, TItemMid> func, Func<TItemMid, TItemOut> resultSelector)
        {
            var bridge = Bridge(source, nameof(source));
            if (func == null) throw new ArgumentNullException(nameof(func));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return AggregateImpl<
                TItemIn,
                TItemMid,
                TItemOut,
                IdentityEnumerable<
                    TItemIn,
                    Stack<TItemIn>,
                    StackEnumerator<TItemIn>
                >,
                StackEnumerator<TItemIn>
            >(ref bridge, seed, func, resultSelector);
        }

        public static TItemOut Aggregate<TItemIn, TItemMid, TItemOut, TEnumerable, TEnumerator>(ref TEnumerable source, TItemMid seed, Func<TItemMid, TItemIn, TItemMid> func, Func<TItemMid, TItemOut> resultSelector)
            where TEnumerable : struct, IStructEnumerable<TItemIn, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItemIn>
        {
            if (source.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(source));
            if (func == null) throw new ArgumentNullException(nameof(func));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return AggregateImpl<TItemIn, TItemMid, TItemOut, TEnumerable, TEnumerator>(ref source, seed, func, resultSelector);
        }

        internal static TItemOut AggregateImpl<TItemIn, TItemMid, TItemOut, TEnumerable, TEnumerator>(ref TEnumerable source, TItemMid seed, Func<TItemMid, TItemIn, TItemMid> func, Func<TItemMid, TItemOut> resultSelector)
            where TEnumerable : struct, IStructEnumerable<TItemIn, TEnumerator>
            where TEnumerator : struct, IStructEnumerator<TItemIn>
        {
            var ret = seed;

            using (var i = source.GetEnumerator())
            {
                while (i.MoveNext())
                {
                    ret = func(ret, i.Current);
                }
            }

            return resultSelector(ret);
        }
    }
}
