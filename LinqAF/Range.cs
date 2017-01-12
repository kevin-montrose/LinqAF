using MiscUtil;

namespace LinqAF
{
    public struct RangeEnumerator<TItem>: IStructEnumerator<TItem>
        // TItem is _always_ int, but we need an unconstrained generic param for chaining elsewhere
    {
        static volatile bool _OneSet;
        static TItem _One;
        internal static TItem One
        {
            get
            {
                if (_OneSet)
                {
                    return _One;
                }

                try
                {
                    // we can't prevent someone from going nuts new'ing up something that 1 can't handle
                    //   so just eat it and give them non-sense
                    _One = Operator.Convert<int, TItem>(1);
                }
                catch
                {
                    _One = default(TItem);
                }
                _OneSet = true;
                return _One;
            }
        }

        static volatile bool _ZeroSet;
        static TItem _Zero;
        internal static TItem Zero
        {
            get
            {
                if (_ZeroSet)
                {
                    return _Zero;
                }

                _Zero = Operator<TItem>.Zero;
                _ZeroSet = true;
                return _Zero;
            }
        }
        
        public TItem Current { get; private set; }

        byte Sigil;
        int Index;
        TItem Start;
        int Count;
        internal RangeEnumerator(byte sigil,TItem start, int count)
        {
            Sigil = sigil;
            Start = start;
            Count = count;
            Current = default(TItem);
            Index = 0;
        }

        public bool IsDefaultValue()
        {
            return Sigil == default(byte);
        }

        public void Dispose() { }

        public bool MoveNext()
        {
            var indexLessThanCount = Index < Count;

            if(indexLessThanCount)
            {
                Current = Operator.Add(Start, Operator.Convert<int, TItem>(Index));
                Index++;
                return true;
            }

            return false;
        }

        public void Reset()
        {
            Index = 0;
            Current = default(TItem);
        }
    }

    // soooo, really this should be RangeEnumerator<int> but life is hard
    public partial struct RangeEnumerable<TItem>: IStructEnumerable<TItem, RangeEnumerator<TItem>>
    {
        byte Sigil;
        internal TItem Start;
        internal int InnerCount;
        internal RangeEnumerable(byte sigil, TItem start, int count)
        {
            Sigil = sigil;
            Start = start;
            InnerCount = count;
        }

        public bool IsDefaultValue()
        {
            return Sigil == default(byte);
        }
        
        public RangeEnumerator<TItem> GetEnumerator() => new RangeEnumerator<TItem>(Sigil, Start, InnerCount);
    }

    public struct ReverseRangeEnumerator<TItem>: IStructEnumerator<TItem>
    {
        public TItem Current { get; private set; }

        byte Sigil;
        int Index;
        TItem Start;
        int Count;
        internal ReverseRangeEnumerator(byte sigil, TItem start, int count)
        {
            Sigil = sigil;
            Start = start;
            Count = count;
            Current = default(TItem);
            Index = 0;
        }

        public bool IsDefaultValue()
        {
            return Sigil == default(byte);
        }

        public void Dispose() { }

        public bool MoveNext()
        {
            var indexLessThanCount = Index < Count;

            if (indexLessThanCount)
            {
                Current = Operator.Subtract(Start, Operator.Convert<int, TItem>(Index));
                Index++;
                return true;
            }

            return false;
        }

        public void Reset()
        {
            Index = 0;
            Current = default(TItem);
        }
    }

    public partial struct ReverseRangeEnumerable<TItem>: IStructEnumerable<TItem, ReverseRangeEnumerator<TItem>>
    {
        byte Sigil;
        internal TItem Start;
        internal int InnerCount;
        internal ReverseRangeEnumerable(byte sigil, TItem start, int count)
        {
            Sigil = sigil;
            Start = start;
            InnerCount = count;
        }

        public bool IsDefaultValue()
        {
            return Sigil == default(byte);
        }

        public ReverseRangeEnumerator<TItem> GetEnumerator() => new ReverseRangeEnumerator<TItem>(Sigil, Start, InnerCount);
    }
}
