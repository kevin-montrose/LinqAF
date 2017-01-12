using System.Collections.Generic;

namespace LinqAF
{
    public struct DistinctDefaultEnumerator<TItem, TInnerEnumerator>:
        IStructEnumerator<TItem>
        where TInnerEnumerator: struct, IStructEnumerator<TItem>
    {
        public TItem Current { get; private set; }

        TInnerEnumerator Inner;
        HashSet<TItem> PreviouslyYielded;

        internal DistinctDefaultEnumerator(ref TInnerEnumerator inner)
        {
            Inner = inner;
            PreviouslyYielded = null;
            Current = default(TItem);
        }

        public bool IsDefaultValue()
        {
            return
                PreviouslyYielded == null &&    // if non-null then it's not default, but _could_ be null prior to iteration
                Inner.IsDefaultValue();
        }

        public void Dispose()
        {
            Inner.Dispose();
            PreviouslyYielded = null;
            Current = default(TItem);
        }

        public bool MoveNext()
        {
            var previouslyYield = PreviouslyYielded ?? (PreviouslyYielded = new HashSet<TItem>(EqualityComparer<TItem>.Default));

            while (Inner.MoveNext())
            {
                var cur = Inner.Current;
                if (previouslyYield.Add(cur))
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
            PreviouslyYielded = null;
        }
    }

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

        public bool IsDefaultValue()
        {
            return Inner.IsDefaultValue();
        }

        public DistinctDefaultEnumerator<TItem, TInnerEnumerator> GetEnumerator()
        {
            var e = Inner.GetEnumerator();
            return new DistinctDefaultEnumerator<TItem, TInnerEnumerator>(ref e);
        }
    }

    public struct DistinctSpecificEnumerator<TItem, TInnerEnumerator> :
        IStructEnumerator<TItem>
        where TInnerEnumerator: struct, IStructEnumerator<TItem>
    {
        public TItem Current { get; private set; }

        TInnerEnumerator Inner;
        IEqualityComparer<TItem> Comparer;
        HashSet<TItem> PreviouslyYielded;

        internal DistinctSpecificEnumerator(ref TInnerEnumerator inner, IEqualityComparer<TItem> comparer)
        {
            Inner = inner;
            Comparer = comparer;
            PreviouslyYielded = null;
            Current = default(TItem);
        }

        public bool IsDefaultValue()
        {
            return
                Comparer == null ||
                (
                    PreviouslyYielded == null &&    // if non-null then it's not default, but _could_ be null prior to iteration
                    Inner.IsDefaultValue()
                );
        }

        public bool MoveNext()
        {
            var previouslyYield = PreviouslyYielded ?? (PreviouslyYielded = new HashSet<TItem>(Comparer));

            while (Inner.MoveNext())
            {
                var cur = Inner.Current;
                if (previouslyYield.Add(cur))
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
            PreviouslyYielded = null;
        }

        public void Dispose()
        {
            Inner.Dispose();
            PreviouslyYielded = null;
            Comparer = null;
        }
    }

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
            return
                Comparer == null ||
                Inner.IsDefaultValue();
        }

        public DistinctSpecificEnumerator<TItem, TInnerEnumerator> GetEnumerator()
        {
            var e = Inner.GetEnumerator();
            return new DistinctSpecificEnumerator<TItem, TInnerEnumerator>(ref e, Comparer);
        }
    }
}
