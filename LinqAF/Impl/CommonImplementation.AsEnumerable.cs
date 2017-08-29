using System;
using System.Collections;
using System.Collections.Generic;

namespace LinqAF.Impl
{
    static partial class CommonImplementation
    {
        [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Auto)]
        struct IEnumeratorBridge<TItem, TEnumerator>:
            IEnumerator<TItem>
            where TEnumerator: struct, IStructEnumerator<TItem>
        {
            public TItem Current { get; private set; }
            object IEnumerator.Current => Current;

            TEnumerator Inner;
            internal IEnumeratorBridge(ref TEnumerator inner)
            {
                Inner = inner;
                Current = default(TItem);
            }
            
            public void Dispose()
            {
                Inner.Dispose();
            }

            public bool MoveNext()
            {
                if (Inner.MoveNext())
                {
                    Current = Inner.Current;
                    return true;
                }

                return false;
            }

            public void Reset()
            {
                Inner.Reset();
                Current = default(TItem);
            }
        }

        [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Auto)]
        struct IEnumerableBridge<TItem, TEnumerable, TEnumerator>: 
            IEnumerable<TItem>
            where TEnumerable: struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator: struct, IStructEnumerator<TItem>
        {
            TEnumerable Inner;

            internal IEnumerableBridge(ref TEnumerable inner)
            {
                Inner = inner;
            }

            public IEnumerator<TItem> GetEnumerator()
            {
                var e = Inner.GetEnumerator();
                return new IEnumeratorBridge<TItem, TEnumerator>(ref e);
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        public static IEnumerable<TItem> AsEnumerable<TItem, TEnumerable, TEnumerator>(ref TEnumerable source)
            where TEnumerable: struct, IStructEnumerable<TItem, TEnumerator>
            where TEnumerator: struct, IStructEnumerator<TItem>
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return new IEnumerableBridge<TItem, TEnumerable, TEnumerator>(ref source);
        }
    }
}
