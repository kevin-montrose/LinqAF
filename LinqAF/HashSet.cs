using LinqAF.Impl;
using System;
using System.Collections.Generic;

namespace LinqAF
{
    public struct HashSetEnumerator<TItem>: IStructEnumerator<TItem>
    {
        public TItem Current { get; private set; }

        bool Initialized;
        HashSet<TItem> InnerEnumerable;
        HashSet<TItem>.Enumerator InnerEnumerator;

        internal HashSetEnumerator(HashSet<TItem> inner)
        {
            Current = default(TItem);
            Initialized = false;
            InnerEnumerable = inner;
            InnerEnumerator = default(HashSet<TItem>.Enumerator);
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
                CommonImplementation.Reset<TItem, HashSet<TItem>.Enumerator>(ref InnerEnumerator);
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
