using LinqAF.Impl;
using System;
using System.Collections.Generic;

namespace LinqAF
{
    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Auto)]
    public struct SortedDictionaryEnumerator<TKey, TValue> : IStructEnumerator<KeyValuePair<TKey, TValue>>
    {
        public KeyValuePair<TKey, TValue> Current { get; private set; }

        bool Initialized;
        SortedDictionary<TKey, TValue> InnerEnumerable;
        SortedDictionary<TKey, TValue>.Enumerator InnerEnumerator;

        internal SortedDictionaryEnumerator(SortedDictionary<TKey, TValue> inner)
        {
            Initialized = false;
            InnerEnumerable = inner;
            InnerEnumerator = default(SortedDictionary<TKey, TValue>.Enumerator);
            Current = default(KeyValuePair<TKey, TValue>);
        }

        public bool IsDefaultValue()
        {
            return InnerEnumerable == null;
        }

        public bool MoveNext()
        {
            if (!Initialized)
            {
                InnerEnumerator = InnerEnumerable.GetEnumerator();
                Initialized = true;
            }

            if (!InnerEnumerator.MoveNext()) return false;

            Current = InnerEnumerator.Current;
            return true;
        }

        public void Reset()
        {
            if (Initialized)
            {
                CommonImplementation.Reset<KeyValuePair<TKey, TValue>, SortedDictionary<TKey, TValue>.Enumerator>(ref InnerEnumerator);
            }
        }

        public void Dispose()
        {
            if (Initialized)
            {
                InnerEnumerator.Dispose();
            }
            InnerEnumerable = null;
        }
    }

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Auto)]
    public struct SortedDictionaryKeysEnumerator<TKey, TValue> : IStructEnumerator<TKey>
    {
        public TKey Current { get; private set; }

        bool Initialized;
        SortedDictionary<TKey, TValue>.KeyCollection InnerEnumerable;
        SortedDictionary<TKey, TValue>.KeyCollection.Enumerator InnerEnumerator;

        internal SortedDictionaryKeysEnumerator(SortedDictionary<TKey, TValue>.KeyCollection inner)
        {
            Initialized = false;
            Current = default(TKey);
            InnerEnumerable = inner;
            InnerEnumerator = default(SortedDictionary<TKey, TValue>.KeyCollection.Enumerator);
        }

        public bool IsDefaultValue()
        {
            return InnerEnumerable == null;
        }

        public bool MoveNext()
        {
            if (!Initialized)
            {
                InnerEnumerator = InnerEnumerable.GetEnumerator();
                Initialized = true;
            }

            if (!InnerEnumerator.MoveNext()) return false;

            Current = InnerEnumerator.Current;
            return true;
        }

        public void Reset()
        {
            if (Initialized)
            {
                CommonImplementation.Reset<TKey, SortedDictionary<TKey, TValue>.KeyCollection.Enumerator>(ref InnerEnumerator);
            }
        }

        public void Dispose()
        {
            if (Initialized)
            {
                InnerEnumerator.Dispose();
            }
            InnerEnumerable = null;
        }
    }

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Auto)]
    public struct SortedDictionaryValuesEnumerator<TKey, TValue> : IStructEnumerator<TValue>
    {
        public TValue Current { get; private set; }

        bool Initialized;
        SortedDictionary<TKey, TValue>.ValueCollection InnerEnumerable;
        SortedDictionary<TKey, TValue>.ValueCollection.Enumerator InnerEnumerator;

        internal SortedDictionaryValuesEnumerator(SortedDictionary<TKey, TValue>.ValueCollection inner)
        {
            Initialized = false;
            Current = default(TValue);
            InnerEnumerable = inner;
            InnerEnumerator = default(SortedDictionary<TKey, TValue>.ValueCollection.Enumerator);
        }

        public bool IsDefaultValue()
        {
            return InnerEnumerable == null;
        }

        public bool MoveNext()
        {
            if (!Initialized)
            {
                InnerEnumerator = InnerEnumerable.GetEnumerator();
                Initialized = true;
            }

            if (!InnerEnumerator.MoveNext()) return false;

            Current = InnerEnumerator.Current;
            return true;
        }

        public void Reset()
        {
            if (Initialized)
            {
                CommonImplementation.Reset<TValue, SortedDictionary<TKey, TValue>.ValueCollection.Enumerator>(ref InnerEnumerator);
            }
        }

        public void Dispose()
        {
            if (Initialized)
            {
                InnerEnumerator.Dispose();
            }
            InnerEnumerable = null;
        }
    }
}
