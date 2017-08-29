using LinqAF.Config;
using LinqAF.Impl;
using System.Collections.Generic;

namespace LinqAF
{
    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Auto)]
    public struct DistinctDefaultEnumerator<TItem, TInnerEnumerator>:
        IStructEnumerator<TItem>
        where TInnerEnumerator: struct, IStructEnumerator<TItem>
    {
        public TItem Current { get; private set; }

        TInnerEnumerator Inner;
        IndexedItemContainer<TItem> Container;
        CompactSet<TItem> PreviouslyYielded;

        internal DistinctDefaultEnumerator(ref TInnerEnumerator inner)
        {
            Inner = inner;
            PreviouslyYielded = new CompactSet<TItem>();
            Container = new IndexedItemContainer<TItem>();
            Current = default(TItem);
        }

        public bool IsDefaultValue() => Inner.IsDefaultValue();

        public void Dispose()
        {
            Inner.Dispose();
            PreviouslyYielded.Dispose();
            Container.Dispose();
            Current = default(TItem);
        }

        public bool MoveNext()
        {
            if (PreviouslyYielded.IsDefaultValue())
            {
                PreviouslyYielded.Initialize();
                Container.Initialize();
            }
            
            while (Inner.MoveNext())
            {
                var cur = Inner.Current;
                if (PreviouslyYielded.Add(cur, ref Container))
                {
                    Current = cur;
                    return true;
                }
            }

            return false;
        }

        public void Reset()
        {
            Inner.Reset();
            PreviouslyYielded.Reset();
            Container.Reset();
        }
    }

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Auto)]
    public partial struct DistinctDefaultEnumerable<TItem, TInnerEnumerable, TInnerEnumerator>:
        IStructEnumerable<TItem, DistinctDefaultEnumerator<TItem, TInnerEnumerator>>
        where TInnerEnumerable: struct, IStructEnumerable<TItem, TInnerEnumerator>
        where TInnerEnumerator: struct, IStructEnumerator<TItem>
    {
        TInnerEnumerable Inner;
        internal DistinctDefaultEnumerable(ref TInnerEnumerable inner)
        {
            Inner = inner;
        }

        public bool IsDefaultValue() => Inner.IsDefaultValue();

        public DistinctDefaultEnumerator<TItem, TInnerEnumerator> GetEnumerator()
        {
            var e = Inner.GetEnumerator();
            return new DistinctDefaultEnumerator<TItem, TInnerEnumerator>(ref e);
        }
    }

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Auto)]
    public struct DistinctSpecificEnumerator<TItem, TInnerEnumerator> :
        IStructEnumerator<TItem>
        where TInnerEnumerator: struct, IStructEnumerator<TItem>
    {
        public TItem Current { get; private set; }

        TInnerEnumerator Inner;
        IEqualityComparer<TItem> Comparer;
        IndexedItemContainer<TItem> Container;
        CompactSet<TItem> PreviouslyYielded;

        internal DistinctSpecificEnumerator(ref TInnerEnumerator inner, IEqualityComparer<TItem> comparer)
        {
            Inner = inner;
            Comparer = comparer;
            PreviouslyYielded = new CompactSet<TItem>();
            Container = new IndexedItemContainer<TItem>();
            Current = default(TItem);
        }

        public bool IsDefaultValue()
        {
            return Comparer == null;
        }

        public bool MoveNext()
        {
            if (PreviouslyYielded.IsDefaultValue())
            {
                PreviouslyYielded.Initialize();
                Container.Initialize();
            }

            while (Inner.MoveNext())
            {
                var cur = Inner.Current;
                if (PreviouslyYielded.Add(cur, ref Container, Comparer))
                {
                    Current = cur;
                    return true;
                }
            }

            return false;
        }

        public void Reset()
        {
            Inner.Reset();
            PreviouslyYielded.Reset();
            Container.Reset();
        }

        public void Dispose()
        {
            Inner.Dispose();
            PreviouslyYielded.Dispose();
            Container.Dispose();
            Comparer = null;
        }
    }

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Auto)]
    public partial struct DistinctSpecificEnumerable<TItem, TInnerEnumerable, TInnerEnumerator>:
        IStructEnumerable<TItem, DistinctSpecificEnumerator<TItem, TInnerEnumerator>>
        where TInnerEnumerable: struct, IStructEnumerable<TItem, TInnerEnumerator>
        where TInnerEnumerator: struct, IStructEnumerator<TItem>
    {
        TInnerEnumerable Inner;
        IEqualityComparer<TItem> Comparer;

        internal DistinctSpecificEnumerable(ref TInnerEnumerable inner, IEqualityComparer<TItem> comparer)
        {
            Inner = inner;
            Comparer = comparer;
        }

        public bool IsDefaultValue()
        {
            return Comparer == null;
        }

        public DistinctSpecificEnumerator<TItem, TInnerEnumerator> GetEnumerator()
        {
            var e = Inner.GetEnumerator();
            return new DistinctSpecificEnumerator<TItem, TInnerEnumerator>(ref e, Comparer);
        }
    }
}
