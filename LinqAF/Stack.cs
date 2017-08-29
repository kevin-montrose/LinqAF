using LinqAF.Impl;
using System;
using System.Collections.Generic;

namespace LinqAF
{
    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Auto)]
    public struct StackEnumerator<TItem> : IStructEnumerator<TItem>
    {
        public TItem Current { get; private set; }

        bool Initialized;
        Stack<TItem> InnerEnumerable;
        Stack<TItem>.Enumerator InnerEnumerator;

        internal StackEnumerator(Stack<TItem> inner)
        {
            Initialized = false;
            Current = default(TItem);
            InnerEnumerable = inner;
            InnerEnumerator = default(Stack<TItem>.Enumerator);
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
                CommonImplementation.Reset<TItem, Stack<TItem>.Enumerator>(ref InnerEnumerator);
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
