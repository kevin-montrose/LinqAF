using LinqAF.Impl;
using System.Collections.Generic;

namespace LinqAF
{
    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Auto)]
    public struct ListEnumerator<TItem>: IStructEnumerator<TItem>
    {
        public TItem Current { get; private set; }

        bool Initialized;
        List<TItem> InnerEnumerable;
        List<TItem>.Enumerator InnerEnumerator;
        internal ListEnumerator(List<TItem> inner)
        {
            Initialized = false;
            InnerEnumerable = inner;
            InnerEnumerator = default(List<TItem>.Enumerator);
            Current = default(TItem);
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

        public void Dispose()
        {
            if (Initialized)
            {
                InnerEnumerator.Dispose();
            }
            InnerEnumerable = null;
        }

        public void Reset()
        {
            if (Initialized)
            {
                CommonImplementation.Reset<TItem, List<TItem>.Enumerator>(ref InnerEnumerator);
            }
        }
    }
}
