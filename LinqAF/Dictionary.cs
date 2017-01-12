using LinqAF.Impl;
using System;
using System.Collections.Generic;

namespace LinqAF
{
    public struct DictionaryEnumerator<TKey, TValue>: IStructEnumerator<KeyValuePair<TKey, TValue>>
    {
        public KeyValuePair<TKey, TValue> Current { get; private set; }

        bool Initialized;
        Dictionary<TKey, TValue> InnerEnumerable;
        Dictionary<TKey, TValue>.Enumerator InnerEnumerator;
        
        internal DictionaryEnumerator(Dictionary<TKey, TValue> inner)
        {
            Initialized = false;
            InnerEnumerable = inner;
            InnerEnumerator = default(Dictionary<TKey, TValue>.Enumerator);
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
                CommonImplementation.Reset<KeyValuePair<TKey, TValue>, Dictionary<TKey, TValue>.Enumerator>(ref InnerEnumerator);
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

    public struct DictionaryKeysEnumerator<TKey, TValue> : IStructEnumerator<TKey>
    {
        public TKey Current { get; private set; }

        bool Initialized;
        Dictionary<TKey, TValue>.KeyCollection InnerEnumerable;
        Dictionary<TKey, TValue>.KeyCollection.Enumerator InnerEnumerator;

        internal DictionaryKeysEnumerator(Dictionary<TKey, TValue>.KeyCollection inner)
        {
            Initialized = false;
            Current = default(TKey);
            InnerEnumerable = inner;
            InnerEnumerator = default(Dictionary<TKey, TValue>.KeyCollection.Enumerator);
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
                CommonImplementation.Reset<TKey, Dictionary<TKey, TValue>.KeyCollection.Enumerator>(ref InnerEnumerator);
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

    public struct DictionaryValuesEnumerator<TKey, TValue> : IStructEnumerator<TValue>
    {
        public TValue Current { get; private set; }

        bool Initialized;
        Dictionary<TKey, TValue>.ValueCollection InnerEnumerable;
        Dictionary<TKey, TValue>.ValueCollection.Enumerator InnerEnumerator;

        internal DictionaryValuesEnumerator(Dictionary<TKey, TValue>.ValueCollection inner)
        {
            Initialized = false;
            Current = default(TValue);
            InnerEnumerable = inner;
            InnerEnumerator = default(Dictionary<TKey, TValue>.ValueCollection.Enumerator);
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
                CommonImplementation.Reset<TValue, Dictionary<TKey, TValue>.ValueCollection.Enumerator>(ref InnerEnumerator);
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
