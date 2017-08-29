using System;

namespace LinqAF
{
    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Auto)]
    public struct WhereEnumerator<TItem, TInnerEnumerator> : IStructEnumerator<TItem>
        where TInnerEnumerator: struct, IStructEnumerator<TItem>
    {
        TInnerEnumerator Inner;
        Func<TItem, bool> Filter;

        public TItem Current { get; private set; }
        
        internal WhereEnumerator(ref TInnerEnumerator i, Func<TItem, bool> f)
        {
            Current = default(TItem);
            Inner = i;
            Filter = f;
        }

        public bool IsDefaultValue()
        {
            return Filter == null;
        }

        public bool MoveNext()
        {
            while (Inner.MoveNext())
            {
                var item = Inner.Current;
                if (Filter(item))
                {
                    Current = item;
                    return true;
                }
            }

            return false;
        }

        public void Dispose() => Inner.Dispose();

        public void Reset() => Inner.Reset();
    }

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Auto)]
    public partial struct WhereEnumerable<TItem, TInnerEnumerable, TInnerEnumerator>: 
        IStructEnumerable<TItem, WhereEnumerator<TItem, TInnerEnumerator>>
        where TInnerEnumerable : struct, IStructEnumerable<TItem, TInnerEnumerator>
        where TInnerEnumerator : struct, IStructEnumerator<TItem>
    {
        TInnerEnumerable Inner;
        Func<TItem, bool> Filter;
        internal WhereEnumerable(ref TInnerEnumerable e, Func<TItem, bool> f)
        {
            Inner = e;
            Filter = f;
        }

        public bool IsDefaultValue()
        {
            return Filter == null;
        }

        public WhereEnumerator<TItem, TInnerEnumerator> GetEnumerator()
        {
            var inner = Inner.GetEnumerator();
            return new WhereEnumerator<TItem, TInnerEnumerator>(ref inner, Filter);
        }
    }

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Auto)]
    public struct WhereIndexedEnumerator<TItem, TInnerEnumerator> : IStructEnumerator<TItem>
        where TInnerEnumerator : struct, IStructEnumerator<TItem>
    {
        TInnerEnumerator Inner;
        int Index;
        Func<TItem, int, bool> Filter;

        public TItem Current { get; private set; }

        internal WhereIndexedEnumerator(ref TInnerEnumerator i, Func<TItem, int, bool> f)
        {
            Current = default(TItem);
            Inner = i;
            Filter = f;
            Index = 0;
        }

        public bool IsDefaultValue()
        {
            return Filter == null;
        }

        public bool MoveNext()
        {
            while (Inner.MoveNext())
            {
                var item = Inner.Current;
                var keep = Filter(item, Index);
                Index++;

                if (keep)
                {
                    Current = item;
                    return true;
                }
            }

            return false;
        }

        public void Dispose() => Inner.Dispose();

        public void Reset()
        {
            Index = 0;
            Inner.Reset();
        }
    }

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Auto)]
    public partial struct WhereIndexedEnumerable<TItem, TInnerEnumerable, TInnerEnumerator>: 
        IStructEnumerable<TItem, WhereIndexedEnumerator<TItem, TInnerEnumerator>>
        where TInnerEnumerable : struct, IStructEnumerable<TItem, TInnerEnumerator>
        where TInnerEnumerator : struct, IStructEnumerator<TItem>
    {
        TInnerEnumerable Inner;
        Func<TItem, int, bool> Filter;
        internal WhereIndexedEnumerable(ref TInnerEnumerable e, Func<TItem, int, bool> f)
        {
            Inner = e;
            Filter = f;
        }

        public bool IsDefaultValue()
        {
            return Filter == null;
        }

        public WhereIndexedEnumerator<TItem, TInnerEnumerator> GetEnumerator()
        {
            var inner = Inner.GetEnumerator();
            return new WhereIndexedEnumerator<TItem, TInnerEnumerator>(ref inner, Filter);
        }
    }
}
