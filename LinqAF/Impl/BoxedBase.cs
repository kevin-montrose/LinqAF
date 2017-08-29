
using System;

namespace LinqAF.Impl
{
    internal abstract class BoxedBase<TItem, TEnumerable, TEnumerator>:
        IBoxedEnumerable<TItem>
        where TEnumerable: struct, IStructEnumerable<TItem, TEnumerator>
        where TEnumerator: struct, IStructEnumerator<TItem>
    {
        class Enumerator : IBoxedEnumerator<TItem>
        {
            public TItem Current { get; set; }

            TEnumerator Outer;
            internal Enumerator(ref TEnumerator outer)
            {
                Outer = outer;
            }

            public bool IsDefaultValue() => Outer.IsDefaultValue();

            public bool MoveNext()
            {
                if (!Outer.MoveNext()) return false;

                Current = Outer.Current;
                return true;
            }

            public void Dispose() => Outer.Dispose();
            public void Reset()
            {
                Outer.Reset();
                Current = default(TItem);
            }
        }

        TEnumerable Outer;
        internal BoxedBase(ref TEnumerable outer)
        {
            Outer = outer;
        }

        public IBoxedEnumerator<TItem> GetEnumerator()
        {
            if (Outer.IsDefaultValue())
            {
                throw CommonImplementation.InnerUninitialized();
            }

            var e = Outer.GetEnumerator();
            return new Enumerator(ref e);
        }

        public bool IsDefaultValue() => Outer.IsDefaultValue();
    }
}
