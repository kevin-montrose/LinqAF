using LinqAF.Impl;
using System;

namespace LinqAF
{
    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Auto)]
    public struct EmptyOrderedEnumerator<TItem> :
        IStructEnumerator<TItem>
    {
        public TItem Current
        {
            get
            {
                throw CommonImplementation.ForbiddenCall(nameof(Current), nameof(EmptyOrderedEnumerator<TItem>));
            }
        }

        byte Sigil;
        internal EmptyOrderedEnumerator(byte sigil)
        {
            Sigil = sigil;
        }

        public bool IsDefaultValue()
        {
            return Sigil == default(byte);
        }

        public void Dispose() { }

        public bool MoveNext() => false;

        public void Reset() { }
    }

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Auto)]
    public partial struct EmptyOrderedEnumerable<TItem> :
        IStructEnumerable<TItem, EmptyOrderedEnumerator<TItem>>,
        IHasComparer<TItem, object, EmptyComparer<TItem>, EmptyEnumerable<TItem>, EmptyEnumerator<TItem>>
    {
        byte Sigil;
        internal EmptyOrderedEnumerable(byte sigil)
        {
            Sigil = sigil;
        }

        public bool IsDefaultValue()
        {
            return Sigil == default(byte);
        }

        public EmptyOrderedEnumerator<TItem> GetEnumerator() => new EmptyOrderedEnumerator<TItem>(Sigil);

        EmptyComparer<TItem> IHasComparer<TItem, object, EmptyComparer<TItem>, EmptyEnumerable<TItem>, EmptyEnumerator<TItem>>.GetComparer() => EmptyCache<TItem>.EmptyComparer;

        EmptyEnumerable<TItem> IHasComparer<TItem, object, EmptyComparer<TItem>, EmptyEnumerable<TItem>, EmptyEnumerator<TItem>>.GetInnerEnumerable() => EmptyCache<TItem>.Empty;
    }
}
