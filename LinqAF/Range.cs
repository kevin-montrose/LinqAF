using MiscUtil;

namespace LinqAF
{
    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Auto)]
    public struct RangeEnumerator : IStructEnumerator<int>
    {
        public int Current { get; private set; }

        byte Sigil;
        int Index;
        int Start;
        int Count;
        internal RangeEnumerator(byte sigil, int start, int count)
        {
            Sigil = sigil;
            Start = start;
            Count = count;
            Current = 0;
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
                Current = Start + Index;
                Index++;
                return true;
            }

            return false;
        }

        public void Reset()
        {
            Index = 0;
            Current = 0;
        }
    }

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Auto)]
    public partial struct RangeEnumerable : IStructEnumerable<int, RangeEnumerator>
    {
        byte Sigil;
        internal int Start;
        internal int InnerCount;
        internal RangeEnumerable(byte sigil, int start, int count)
        {
            Sigil = sigil;
            Start = start;
            InnerCount = count;
        }

        public bool IsDefaultValue()
        {
            return Sigil == default(byte);
        }

        public RangeEnumerator GetEnumerator() => new RangeEnumerator(Sigil, Start, InnerCount);
    }

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Auto)]
    public struct ReverseRangeEnumerator : IStructEnumerator<int>
    {
        public int Current { get; private set; }

        byte Sigil;
        int Index;
        int Start;
        int Count;
        internal ReverseRangeEnumerator(byte sigil, int start, int count)
        {
            Sigil = sigil;
            Start = start;
            Count = count;
            Current = 0;
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
                Current = Start - Index;
                Index++;
                return true;
            }

            return false;
        }

        public void Reset()
        {
            Index = 0;
            Current = 0;
        }
    }

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Auto)]
    public partial struct ReverseRangeEnumerable : IStructEnumerable<int, ReverseRangeEnumerator>
    {
        byte Sigil;
        internal int Start;
        internal int InnerCount;
        internal ReverseRangeEnumerable(byte sigil, int start, int count)
        {
            Sigil = sigil;
            Start = start;
            InnerCount = count;
        }

        public bool IsDefaultValue()
        {
            return Sigil == default(byte);
        }

        public ReverseRangeEnumerator GetEnumerator() => new ReverseRangeEnumerator(Sigil, Start, InnerCount);
    }
}