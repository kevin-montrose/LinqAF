using LinqAF.Impl;
using System;
using System.Collections.Generic;

namespace LinqAF
{
    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Auto)]
    public struct IdentityEnumerator : IStructEnumerator<object>
    {
        public object Current => InnerEnumerator.Current;

        System.Collections.IEnumerable InnerEnumerable;
        System.Collections.IEnumerator InnerEnumerator;
        internal IdentityEnumerator(System.Collections.IEnumerable inner)
        {
            InnerEnumerable = inner;
            InnerEnumerator = null;
        }

        public bool IsDefaultValue()
        {
            return InnerEnumerable == null;
        }

        public void Dispose()
        {
            InnerEnumerable = null;
            InnerEnumerator = null;
        }

        public bool MoveNext()
        {
            if(InnerEnumerator == null)
            {
                InnerEnumerator = InnerEnumerable.GetEnumerator();
            }

            return InnerEnumerator.MoveNext();
        }

        public void Reset()
        {
            if (InnerEnumerator != null)
            {
                InnerEnumerator.Reset();
            }
        }
    }

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Auto)]
    public struct IdentityEnumerator<TItem> : IStructEnumerator<TItem>
    {
        public TItem Current => InnerEnumerator.Current;

        IEnumerable<TItem> InnerEnumerable;
        IEnumerator<TItem> InnerEnumerator;
        internal IdentityEnumerator(IEnumerable<TItem> i)
        {
            InnerEnumerable = i;
            InnerEnumerator = null;
        }

        public bool IsDefaultValue()
        {
            return InnerEnumerable == null;
        }

        public void Dispose()
        {
            if (InnerEnumerator != null)
            {
                InnerEnumerator.Dispose();
            }

            InnerEnumerable = null;
            InnerEnumerator = null;
        }

        public bool MoveNext()
        {
            if(InnerEnumerator == null)
            {
                InnerEnumerator = InnerEnumerable.GetEnumerator();
            }

            return InnerEnumerator.MoveNext();
        }

        public void Reset()
        {
            if (InnerEnumerator != null)
            {
                InnerEnumerator.Reset();
            }
        }
    }

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Auto)]
    public partial struct IdentityEnumerable<TItem, TBridgeType, TBridger, TEnumerator> :
        IStructEnumerable<TItem, TEnumerator>
        where TEnumerator : struct, IStructEnumerator<TItem>
        where TBridger: struct, IStructBridger<TItem, TBridgeType, TEnumerator>
        where TBridgeType : class
    {
        TBridgeType Inner;
        internal IdentityEnumerable(TBridgeType inner)
        {
            Inner = inner;
        }

        public bool IsDefaultValue() => Inner == null;

        public TEnumerator GetEnumerator() => default(TBridger).Bridge(Inner);
    }
}