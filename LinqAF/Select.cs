using System;

namespace LinqAF
{
    public struct SelectEnumerator<TInItem, TOutItem, TInnerEnumerator> : IStructEnumerator<TOutItem>
        where TInnerEnumerator: struct, IStructEnumerator<TInItem>
    {
        public TOutItem Current { get; private set; }

        TInnerEnumerator Inner;
        Func<TInItem, TOutItem> Mapper;
        internal SelectEnumerator(ref TInnerEnumerator i, Func<TInItem, TOutItem> m)
        {
            Inner = i;
            Mapper = m;
            Current = default(TOutItem);
        }

        public bool IsDefaultValue()
        {
            return
                Mapper == null &&
                Inner.IsDefaultValue();
        }

        public void Dispose() => Inner.Dispose();

        public bool MoveNext()
        {
            if (Inner.MoveNext())
            {
                var item = Inner.Current;
                var mapped = Mapper(item);
                Current = mapped;
                return true;
            }

            return false;
        }

        public void Reset() => Inner.Reset();
    }

    public partial struct SelectEnumerable<TInItem, TOutItem, TInnerEnumerable, TInnerEnumerator> : 
        IStructEnumerable<TOutItem, SelectEnumerator<TInItem, TOutItem, TInnerEnumerator>>
        where TInnerEnumerable: struct, IStructEnumerable<TInItem, TInnerEnumerator>
        where TInnerEnumerator: struct, IStructEnumerator<TInItem>
    {
        TInnerEnumerable Inner;
        Func<TInItem, TOutItem> Mapper;
        internal SelectEnumerable(ref TInnerEnumerable e, Func<TInItem, TOutItem> m)
        {
            Inner = e;
            Mapper = m;
        }

        public bool IsDefaultValue()
        {
            return
                Mapper == null &&
                Inner.IsDefaultValue();
        }

        public SelectEnumerator<TInItem, TOutItem, TInnerEnumerator> GetEnumerator()
        {
            var inner = Inner.GetEnumerator();
            return new SelectEnumerator<TInItem, TOutItem, TInnerEnumerator>(ref inner, Mapper);
        }
    }

    public struct SelectIndexedEnumerator<TInItem, TOutItem, TInnerEnumerator> : IStructEnumerator<TOutItem>
        where TInnerEnumerator : struct, IStructEnumerator<TInItem>
    {
        public TOutItem Current { get; private set; }

        TInnerEnumerator Inner;
        int Index;
        Func<TInItem, int, TOutItem> Mapper;
        internal SelectIndexedEnumerator(ref TInnerEnumerator i, Func<TInItem, int, TOutItem> m)
        {
            Inner = i;
            Mapper = m;
            Index = 0;
            Current = default(TOutItem);
        }

        public bool IsDefaultValue()
        {
            return
                Index == default(int) &&
                Mapper == null &&
                Inner.IsDefaultValue();
        }

        public void Dispose() => Inner.Dispose();

        public bool MoveNext()
        {
            if (Inner.MoveNext())
            {
                var item = Inner.Current;
                var mapped = Mapper(item, Index);

                Index++;
                Current = mapped;

                return true;
            }

            return false;
        }

        public void Reset()
        {
            Index = 0;
            Current = default(TOutItem);
            Inner.Reset();
        }
    }

    public partial struct SelectIndexedEnumerable<TInItem, TOutItem, TInnerEnumerable, TInnerEnumerator>: 
        IStructEnumerable<TOutItem, SelectIndexedEnumerator<TInItem, TOutItem, TInnerEnumerator>>
        where TInnerEnumerable : struct, IStructEnumerable<TInItem, TInnerEnumerator>
        where TInnerEnumerator : struct, IStructEnumerator<TInItem>
    {
        TInnerEnumerable Inner;
        Func<TInItem, int, TOutItem> Mapper;
        internal SelectIndexedEnumerable(ref TInnerEnumerable e, Func<TInItem, int, TOutItem> m)
        {
            Inner = e;
            Mapper = m;
        }

        public bool IsDefaultValue()
        {
            return
                Mapper == null &&
                Inner.IsDefaultValue();
        }

        public SelectIndexedEnumerator<TInItem, TOutItem, TInnerEnumerator> GetEnumerator()
        {
            var inner = Inner.GetEnumerator();
            return new SelectIndexedEnumerator<TInItem, TOutItem, TInnerEnumerator>(ref inner, Mapper);
        }
    }
}
