namespace LinqAF
{
    public struct DefaultIfEmptyDefaultEnumerator<TItem, TInnerEnumerator>: IStructEnumerator<TItem>
        where TInnerEnumerator: struct, IStructEnumerator<TItem>
    {
        public TItem Current { get; private set; }

        TInnerEnumerator Inner;
        bool? WasEmpty;
        internal DefaultIfEmptyDefaultEnumerator(ref TInnerEnumerator inner)
        {
            Inner = inner;
            Current = default(TItem);
            WasEmpty = null;
        }

        public bool IsDefaultValue()
        {
            return
                WasEmpty == default(bool?) &&
                Inner.IsDefaultValue();
        }

        public void Dispose()
        {
            Inner.Dispose();
        }

        public bool MoveNext()
        {
            if (WasEmpty == null)
            {
                if (Inner.MoveNext())
                {
                    WasEmpty = false;
                    Current = Inner.Current;
                    return true;
                }
                else
                {
                    WasEmpty = true;
                    Current = default(TItem);
                    return true;
                }
            }

            if (!WasEmpty.Value)
            {
                if (Inner.MoveNext())
                {
                    Current = Inner.Current;
                    return true;
                }
            }

            return false;
        }

        public void Reset()
        {
            Inner.Reset();
            Current = default(TItem);
            WasEmpty = null;
        }
    }

    public partial struct DefaultIfEmptyDefaultEnumerable<TItem, TInnerEnumerable, TInnerEnumerator>:
        IStructEnumerable<TItem, DefaultIfEmptyDefaultEnumerator<TItem, TInnerEnumerator>>
        where TInnerEnumerable: struct, IStructEnumerable<TItem, TInnerEnumerator>
        where TInnerEnumerator: struct, IStructEnumerator<TItem>
    {
        TInnerEnumerable Inner;
        internal DefaultIfEmptyDefaultEnumerable(ref TInnerEnumerable inner)
        {
            Inner = inner;
        }

        public bool IsDefaultValue()
        {
            return Inner.IsDefaultValue();
        }

        public DefaultIfEmptyDefaultEnumerator<TItem, TInnerEnumerator> GetEnumerator()
        {
            var inner = Inner.GetEnumerator();
            return new DefaultIfEmptyDefaultEnumerator<TItem, TInnerEnumerator>(ref inner);
        }
    }

    public struct DefaultIfEmptySpecificEnumerator<TItem, TInnerEnumerator> : IStructEnumerator<TItem>
        where TInnerEnumerator : struct, IStructEnumerator<TItem>
    {
        public TItem Current { get; private set; }

        TInnerEnumerator Inner;
        TItem Default;
        bool? WasEmpty;
        internal DefaultIfEmptySpecificEnumerator(ref TInnerEnumerator inner, TItem @default)
        {
            Inner = inner;
            Current = default(TItem);
            WasEmpty = null;
            Default = @default;
        }

        public bool IsDefaultValue()
        {
            return
                WasEmpty == default(bool?) &&
                Inner.IsDefaultValue();
        }

        public void Dispose()
        {
            Inner.Dispose();
        }

        public bool MoveNext()
        {
            if (WasEmpty == null)
            {
                if (Inner.MoveNext())
                {
                    WasEmpty = false;
                    Current = Inner.Current;
                    return true;
                }
                else
                {
                    WasEmpty = true;
                    Current = Default;
                    return true;
                }
            }

            if (!WasEmpty.Value)
            {
                if (Inner.MoveNext())
                {
                    Current = Inner.Current;
                    return true;
                }
            }

            return false;
        }

        public void Reset()
        {
            Inner.Reset();
            Current = default(TItem);
            WasEmpty = null;
        }
    }

    public partial struct DefaultIfEmptySpecificEnumerable<TItem, TInnerEnumerable, TInnerEnumerator>: 
        IStructEnumerable<TItem, DefaultIfEmptySpecificEnumerator<TItem, TInnerEnumerator>>
        where TInnerEnumerable : struct, IStructEnumerable<TItem, TInnerEnumerator>
        where TInnerEnumerator : struct, IStructEnumerator<TItem>
    {
        TInnerEnumerable Inner;
        TItem Default;
        internal DefaultIfEmptySpecificEnumerable(ref TInnerEnumerable inner, TItem @default)
        {
            Inner = inner;
            Default = @default;
        }

        public bool IsDefaultValue()
        {
            return Inner.IsDefaultValue();
        }

        public DefaultIfEmptySpecificEnumerator<TItem, TInnerEnumerator> GetEnumerator()
        {
            var inner = Inner.GetEnumerator();
            return new DefaultIfEmptySpecificEnumerator<TItem, TInnerEnumerator>(ref inner, Default);
        }
    }
}