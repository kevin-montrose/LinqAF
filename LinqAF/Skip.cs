using System;

namespace LinqAF
{
    public struct SkipEnumerator<TItem, TInnerEnumerator> : IStructEnumerator<TItem>
        where TInnerEnumerator : struct, IStructEnumerator<TItem>
    {
        public TItem Current { get; private set; }

        int Count;
        int Index;
        TInnerEnumerator Inner;
        internal SkipEnumerator(ref TInnerEnumerator inner, int count)
        {
            Count = count;
            Index = 0;
            Inner = inner;
            Current = default(TItem);
        }

        public bool IsDefaultValue()
        {
            return 
                Count == default(int) &&
                Index == default(int) && 
                Inner.IsDefaultValue();
        }

        public void Dispose()
        {
            Inner.Dispose();
        }

        public bool MoveNext()
        {
            while(Index < Count && Inner.MoveNext())
            {
                Index++;
            }

            if (!Inner.MoveNext()) return false;

            Current = Inner.Current;
            return true;
        }

        public void Reset()
        {
            Inner.Reset();
            Index = 0;
        }
    }

    public partial struct SkipEnumerable<TItem, TInnerEnumerable, TInnerEnumerator> :
        IStructEnumerable<TItem, SkipEnumerator<TItem, TInnerEnumerator>>
        where TInnerEnumerable: struct, IStructEnumerable<TItem, TInnerEnumerator>
        where TInnerEnumerator: struct, IStructEnumerator<TItem>
    {
        int SkipCount;
        TInnerEnumerable Inner;

        internal SkipEnumerable(ref TInnerEnumerable inner, int count)
        {
            Inner = inner;
            SkipCount = count;
        }

        public bool IsDefaultValue()
        {
            return 
                SkipCount == default(int) &&
                Inner.IsDefaultValue();
        }

        public SkipEnumerator<TItem, TInnerEnumerator> GetEnumerator()
        {
            var inner = Inner.GetEnumerator();
            return new SkipEnumerator<TItem, TInnerEnumerator>(ref inner, SkipCount);
        }
    }

    public struct SkipWhileEnumerator<TItem, TInnerEnumerator>: IStructEnumerator<TItem>
        where TInnerEnumerator: struct, IStructEnumerator<TItem>
    {
        public TItem Current { get; private set; }

        TInnerEnumerator Inner;
        Func<TItem, bool> Predicate;
        bool Finished;
        
        internal SkipWhileEnumerator(ref TInnerEnumerator inner, Func<TItem, bool> predicate)
        {
            Inner = inner;
            Predicate = predicate;
            Current = default(TItem);
            Finished = false;
        }

        public bool IsDefaultValue()
        {
            return
                Predicate == null &&
                Finished == default(bool) &&
                Inner.IsDefaultValue();
        }

        public void Dispose()
        {
            Inner.Dispose();
        }

        public bool MoveNext()
        {
            if (!Finished)
            {
                while (Inner.MoveNext())
                {
                    var item = Inner.Current;
                    if (!Predicate(item))
                    {
                        Current = item;
                        Finished = true;
                        return true;
                    }
                }

                Finished = true;
                return false;
            }

            if (Inner.MoveNext())
            {
                Current = Inner.Current;
                return true;
            }

            return false;
        }

        public void Reset()
        {
            Inner.Reset();
            Finished = false;
        }
    }

    public partial struct SkipWhileEnumerable<TItem, TInnerEnumerable, TInnerEnumerator> :
        IStructEnumerable<TItem, SkipWhileEnumerator<TItem, TInnerEnumerator>>
        where TInnerEnumerable : struct, IStructEnumerable<TItem, TInnerEnumerator>
        where TInnerEnumerator : struct, IStructEnumerator<TItem>
    {
        TInnerEnumerable Inner;
        Func<TItem, bool> Predicate;

        internal SkipWhileEnumerable(ref TInnerEnumerable inner, Func<TItem, bool> predicate)
        {
            Inner = inner;
            Predicate = predicate;
        }

        public bool IsDefaultValue()
        {
            return
                Predicate == null &&
                Inner.IsDefaultValue();
        }

        public SkipWhileEnumerator<TItem, TInnerEnumerator> GetEnumerator()
        {
            var inner = Inner.GetEnumerator();
            return new SkipWhileEnumerator<TItem, TInnerEnumerator>(ref inner, Predicate);
        }
    }

    public struct SkipWhileIndexedEnumerator<TItem, TInnerEnumerator> : IStructEnumerator<TItem>
        where TInnerEnumerator : struct, IStructEnumerator<TItem>
    {
        public TItem Current { get; private set; }

        TInnerEnumerator Inner;
        Func<TItem, int, bool> Predicate;
        bool Finished;
        int Index;

        internal SkipWhileIndexedEnumerator(ref TInnerEnumerator inner, Func<TItem, int, bool> predicate)
        {
            Inner = inner;
            Predicate = predicate;
            Current = default(TItem);
            Finished = false;
            Index = 0;
        }

        public bool IsDefaultValue()
        {
            return
                Predicate == null &&
                Finished == default(bool) &&
                Index == default(int) &&
                Inner.IsDefaultValue();
        }

        public void Dispose()
        {
            Inner.Dispose();
        }

        public bool MoveNext()
        {
            if (!Finished)
            {
                while (Inner.MoveNext())
                {
                    var item = Inner.Current;
                    if (!Predicate(item, Index))
                    {
                        Current = item;
                        Finished = true;
                        return true;
                    }
                    Index++;
                }

                Finished = true;
                return false;
            }

            if (Inner.MoveNext())
            {
                Current = Inner.Current;
                return true;
            }

            return false;
        }

        public void Reset()
        {
            Inner.Reset();
            Finished = false;
            Index = 0;
        }
    }

    public partial struct SkipWhileIndexedEnumerable<TItem, TInnerEnumerable, TInnerEnumerator> :
        IStructEnumerable<TItem, SkipWhileIndexedEnumerator<TItem, TInnerEnumerator>>
        where TInnerEnumerable : struct, IStructEnumerable<TItem, TInnerEnumerator>
        where TInnerEnumerator : struct, IStructEnumerator<TItem>
    {
        TInnerEnumerable Inner;
        Func<TItem, int, bool> Predicate;

        internal SkipWhileIndexedEnumerable(ref TInnerEnumerable inner, Func<TItem, int, bool> predicate)
        {
            Inner = inner;
            Predicate = predicate;
        }

        public bool IsDefaultValue()
        {
            return
                Predicate == null &&
                Inner.IsDefaultValue();
        }

        public SkipWhileIndexedEnumerator<TItem, TInnerEnumerator> GetEnumerator()
        {
            var inner = Inner.GetEnumerator();
            return new SkipWhileIndexedEnumerator<TItem, TInnerEnumerator>(ref inner, Predicate);
        }
    }
}
