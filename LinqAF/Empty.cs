using System;

namespace LinqAF
{
    public struct EmptyEnumerator<TItem> : IStructEnumerator<TItem>
    {
        public TItem Current
        {
            get
            {
                throw new InvalidOperationException($"Called {nameof(Current)} on {nameof(EmptyEnumerator<TItem>)}");
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
