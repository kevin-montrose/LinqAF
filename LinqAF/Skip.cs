using LinqAF.Impl;
using System;

namespace LinqAF
{
    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Auto)]
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

        public bool IsDefaultValue() => Inner.IsDefaultValue();

        public void Dispose()
        {
            Inner.Dispose();
        }

        public bool MoveNext()
        {
            while (Index < Count && Inner.MoveNext())
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

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Auto)]
    public partial struct SkipEnumerable<TItem, TInnerEnumerable, TInnerEnumerator> :
        IStructEnumerable<TItem, SkipEnumerator<TItem, TInnerEnumerator>>
        where TInnerEnumerable : struct, IStructEnumerable<TItem, TInnerEnumerator>
        where TInnerEnumerator : struct, IStructEnumerator<TItem>
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
            return Inner.IsDefaultValue();
        }

        public SkipEnumerator<TItem, TInnerEnumerator> GetEnumerator()
        {
            var inner = Inner.GetEnumerator();
            return new SkipEnumerator<TItem, TInnerEnumerator>(ref inner, SkipCount);
        }
    }

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Auto)]
    public struct SkipWhileEnumerator<TItem, TInnerEnumerator> : IStructEnumerator<TItem>
        where TInnerEnumerator : struct, IStructEnumerator<TItem>
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
            return Predicate == null;
        }

        public void Dispose() => Inner.Dispose();

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

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Auto)]
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
            return Predicate == null;
        }

        public SkipWhileEnumerator<TItem, TInnerEnumerator> GetEnumerator()
        {
            var inner = Inner.GetEnumerator();
            return new SkipWhileEnumerator<TItem, TInnerEnumerator>(ref inner, Predicate);
        }
    }

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Auto)]
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
            return Predicate == null;
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

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Auto)]
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
            return Predicate == null;
        }

        public SkipWhileIndexedEnumerator<TItem, TInnerEnumerator> GetEnumerator()
        {
            var inner = Inner.GetEnumerator();
            return new SkipWhileIndexedEnumerator<TItem, TInnerEnumerator>(ref inner, Predicate);
        }
    }

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Auto)]
    public struct SkipLastEnumerator<TItem, TInnerEnumerator> :
        IStructEnumerator<TItem>
        where TInnerEnumerator : struct, IStructEnumerator<TItem>
    {
        public TItem Current { get; private set; }

        TInnerEnumerator Inner;
        int InnerCount;
        StructQueue<TItem> Queue;

        internal SkipLastEnumerator(ref TInnerEnumerator inner, int count)
        {
            Current = default(TItem);
            Queue = default(StructQueue<TItem>);
            Inner = inner;
            InnerCount = count;
        }

        public bool IsDefaultValue() => Inner.IsDefaultValue();

        public bool MoveNext()
        {
            // just pass through in this case, don't ever allocate
            //   a StructQueue
            if (InnerCount == 0)
            {
                var ret = Inner.MoveNext();
                if (ret)
                {
                    Current = Inner.Current;
                }

                return ret;
            }

            if (Queue.IsDefaultValue())
            {
                Queue.Initialize();
            }

            // Advance in inner...
            while (Inner.MoveNext())
            {
                Queue.Enqueue(Inner.Current);

                // until we've got Count + 1 Elements....
                if (Queue.Count > InnerCount)
                {
                    // then take the first element out and yield it
                    //   leaving us with Count elements
                    Current = Queue.Dequeue();
                    return true;
                }
            }

            // so when we fall through we've either got Count elements or fewer in the Queue
            return false;
        }

        public void Reset()
        {
            Queue.Dispose();
            Inner.Reset();
        }

        public void Dispose()
        {
            Queue.Dispose();
            Inner.Dispose();
        }
    }

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Auto)]
    public partial struct SkipLastEnumerable<TItem, TInnerEnumerable, TInnerEnumerator> :
        IStructEnumerable<TItem, SkipLastEnumerator<TItem, TInnerEnumerator>>
        where TInnerEnumerable : struct, IStructEnumerable<TItem, TInnerEnumerator>
        where TInnerEnumerator : struct, IStructEnumerator<TItem>
    {
        TInnerEnumerable Inner;
        int InnerCount;

        internal SkipLastEnumerable(ref TInnerEnumerable inner, int count)
        {
            Inner = inner;
            InnerCount = count;
        }

        public bool IsDefaultValue() => Inner.IsDefaultValue();

        public SkipLastEnumerator<TItem, TInnerEnumerator> GetEnumerator()
        {
            var e = Inner.GetEnumerator();
            return new SkipLastEnumerator<TItem, TInnerEnumerator>(ref e, InnerCount);
        }
    }
}