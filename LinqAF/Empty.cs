using LinqAF.Impl;
using System;

namespace LinqAF
{
    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Auto)]
    public struct EmptyEnumerator<TItem> : IStructEnumerator<TItem>
    {
        public TItem Current
        {
            get
            {
                throw CommonImplementation.ForbiddenCall(nameof(Current), nameof(EmptyEnumerator<TItem>));
            }
        }

        // Sigil is necessary to distinguish instances from default(EmptyEnumerator)
        byte Sigil;
        internal EmptyEnumerator(byte sigil)
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
    public partial struct EmptyEnumerable<TItem> : IStructEnumerable<TItem, EmptyEnumerator<TItem>>
    {
        // Sigil is necessary to distinguish instances from default(EmptyEnumerable)
        byte Sigil;
        internal EmptyEnumerable(byte sigil)
        {
            Sigil = sigil;
        }

        public bool IsDefaultValue()
        {
            return Sigil == default(byte);
        }
        
        public EmptyEnumerator<TItem> GetEnumerator()
        {
            return new EmptyEnumerator<TItem>(Sigil);
        }
    }
}
