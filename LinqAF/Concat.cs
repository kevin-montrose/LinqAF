namespace LinqAF
{
    public struct ConcatEnumerator<TItem, TFirstEnumerator, TSecondEnumerator>: IStructEnumerator<TItem>
        where TFirstEnumerator: struct, IStructEnumerator<TItem>
        where TSecondEnumerator: struct, IStructEnumerator<TItem>
    {
        public TItem Current { get; private set; }

        bool UseSecond;
        TFirstEnumerator First;
        TSecondEnumerator Second;
        internal ConcatEnumerator(ref TFirstEnumerator first, ref TSecondEnumerator second)
        {
            First = first;
            Second = second;
            Current = default(TItem);
            UseSecond = false;
        }

        public bool IsDefaultValue()
        {
            return
                UseSecond == default(bool) &&
                First.IsDefaultValue() &&
                Second.IsDefaultValue();
        }

        public void Dispose()
        {
            First.Dispose();
            Second.Dispose();
        }

        public bool MoveNext()
        {
            methodStart:
            if (UseSecond)
            {
                if (Second.MoveNext())
                {
                    Current = Second.Current;
                    return true;
                }

                return false;
            }

            if (First.MoveNext())
            {
                Current = First.Current;
                return true;
            }

            UseSecond = true;
            goto methodStart;
        }

        public void Reset()
        {
            First.Reset();
            Second.Reset();
        }
    }

    public partial struct ConcatEnumerable<TItem, TFirstEnumerable, TFirstEnumerator, TSecondEnumerable, TSecondEnumerator>: 
        IStructEnumerable<TItem, ConcatEnumerator<TItem, TFirstEnumerator, TSecondEnumerator>>
        where TFirstEnumerable: struct, IStructEnumerable<TItem, TFirstEnumerator>
        where TFirstEnumerator: struct, IStructEnumerator<TItem>
        where TSecondEnumerable: struct, IStructEnumerable<TItem, TSecondEnumerator>
        where TSecondEnumerator: struct, IStructEnumerator<TItem>
    {
        TFirstEnumerable Left;
        TSecondEnumerable Right;
        internal ConcatEnumerable(ref TFirstEnumerable first, ref TSecondEnumerable second)
        {
            Left = first;
            Right = second;
        }

        public bool IsDefaultValue()
        {
            return
                Left.IsDefaultValue() &&
                Right.IsDefaultValue();
        }

        public ConcatEnumerator<TItem, TFirstEnumerator, TSecondEnumerator> GetEnumerator()
        {
            var left = Left.GetEnumerator();
            var right = Right.GetEnumerator();
            return new ConcatEnumerator<TItem, TFirstEnumerator, TSecondEnumerator>(ref left, ref right);
        }
    }
}
