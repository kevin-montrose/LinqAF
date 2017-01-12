using LinqAF.Impl;
using System;
using System.Collections.Generic;

namespace LinqAF
{
    public struct LinkedListEnumerator<TItem>: IStructEnumerator<TItem>
    {
        public TItem Current { get; private set; }

        bool Initialized;
        LinkedList<TItem> InnerEnumerable;
        LinkedList<TItem>.Enumerator InnerEnumerator;

        internal LinkedListEnumerator(LinkedList<TItem> inner)
        {
            Initialized = false;
            Current = default(TItem);
            InnerEnumerable = inner;
            InnerEnumerator = default(LinkedList<TItem>.Enumerator);
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
                CommonImplementation.Reset<TItem, LinkedList<TItem>.Enumerator>(ref InnerEnumerator);
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
