using LinqAF.Impl;
using System;
using System.Collections.Generic;

namespace LinqAF
{
    public struct IdentityEnumerator : IStructEnumerator<object>
    {
        public object Current { get; private set; }

        System.Collections.IEnumerable InnerEnumerable;
        System.Collections.IEnumerator InnerEnumerator;
        internal IdentityEnumerator(System.Collections.IEnumerable inner)
        {
            InnerEnumerable = inner;
            InnerEnumerator = null;
            Current = null;
        }

        public bool IsDefaultValue()
        {
            return InnerEnumerable == null && InnerEnumerator == null;
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

            if (InnerEnumerator.MoveNext())
            {
                Current = InnerEnumerator.Current;
                return true;
            }

            return false;
        }

        public void Reset()
        {
            if (InnerEnumerator != null)
            {
                InnerEnumerator.Reset();
            }

            Current = null;
        }
    }

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

    public partial struct IdentityEnumerable<TItem, TBridgeType, TEnumerator> :
        IStructEnumerable<TItem, TEnumerator>
        where TEnumerator : struct, IStructEnumerator<TItem>
        where TBridgeType : class
    {
        TBridgeType Inner;
        Func<TBridgeType, TEnumerator> Bridge;
        internal IdentityEnumerable(TBridgeType inner, Func<TBridgeType, TEnumerator> bridge)
        {
            Inner = inner;
            Bridge = bridge;
        }

        public bool IsDefaultValue()
        {
            return
                Inner == null &&
                Bridge == null;
        }

        public TEnumerator GetEnumerator() => Bridge(Inner);
    }
}