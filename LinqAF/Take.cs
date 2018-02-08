using LinqAF.Impl;
using System;

namespace LinqAF
{
    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Auto)]
    public struct TakeEnumerator<TItem, TInnerEnumerator> : IStructEnumerator<TItem>
        where TInnerEnumerator : struct, IStructEnumerator<TItem>
    {
        public TItem Current { get; private set; }

        int Count;
        int Index;
        TInnerEnumerator Inner;
        internal TakeEnumerator(ref TInnerEnumerator inner, int count)
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
            if (Index < Count && Inner.MoveNext())
            {
                Current = Inner.Current;
                Index++;

                return true;
            }

            return false;
        }

        public void Reset()
        {
            Inner.Reset();
            Index = 0;
        }
    }

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Auto)]
    public partial struct TakeEnumerable<TItem, TInnerEnumerable, TInnerEnumerator> :
        IStructEnumerable<TItem, TakeEnumerator<TItem, TInnerEnumerator>>
        where TInnerEnumerable : struct, IStructEnumerable<TItem, TInnerEnumerator>
        where TInnerEnumerator : struct, IStructEnumerator<TItem>
    {
        int TakeCount;
        TInnerEnumerable Inner;

        internal TakeEnumerable(ref TInnerEnumerable inner, int count)
        {
            TakeCount = count;
            Inner = inner;
        }

        public bool IsDefaultValue()
        {
            return Inner.IsDefaultValue();
        }

        public TakeEnumerator<TItem, TInnerEnumerator> GetEnumerator()
        {
            var inner = Inner.GetEnumerator();
            return new TakeEnumerator<TItem, TInnerEnumerator>(ref inner, TakeCount);
        }
    }

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Auto)]
    public struct TakeWhileEnumerator<TItem, TInnerEnumerator> :
        IStructEnumerator<TItem>
        where TInnerEnumerator : struct, IStructEnumerator<TItem>
    {
        public TItem Current { get; private set; }

        Func<TItem, bool> Predicate;
        TInnerEnumerator Inner;
        bool Finished;

        internal TakeWhileEnumerator(ref TInnerEnumerator inner, Func<TItem, bool> predicate)
        {
            Inner = inner;
            Predicate = predicate;
            Finished = false;
            Current = default(TItem);
        }

        public bool IsDefaultValue()
        {
            return Predicate == null;
        }

        public bool MoveNext()
        {
            if (Finished) return false;

            if (!Inner.MoveNext())
            {
                Finished = true;
                return false;
            }

            var item = Inner.Current;

            if (!Predicate(item))
            {
                Finished = true;
                return false;
            }

            Current = item;
            return true;
        }

        public void Reset()
        {
            Inner.Reset();
            Finished = false;
        }

        public void Dispose()
        {
            Inner.Dispose();
        }
    }

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Auto)]
    public partial struct TakeWhileEnumerable<TItem, TInnerEnumerable, TInnerEnumerator> :
        IStructEnumerable<TItem, TakeWhileEnumerator<TItem, TInnerEnumerator>>
        where TInnerEnumerable : struct, IStructEnumerable<TItem, TInnerEnumerator>
        where TInnerEnumerator : struct, IStructEnumerator<TItem>
    {
        TInnerEnumerable Inner;
        Func<TItem, bool> Predicate;

        internal TakeWhileEnumerable(ref TInnerEnumerable inner, Func<TItem, bool> predicate)
        {
            Inner = inner;
            Predicate = predicate;
        }

        public bool IsDefaultValue()
        {
            return Predicate == null;
        }

        public TakeWhileEnumerator<TItem, TInnerEnumerator> GetEnumerator()
        {
            var inner = Inner.GetEnumerator();
            return new TakeWhileEnumerator<TItem, TInnerEnumerator>(ref inner, Predicate);
        }
    }

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Auto)]
    public struct TakeWhileIndexedEnumerator<TItem, TInnerEnumerator> :
        IStructEnumerator<TItem>
        where TInnerEnumerator : struct, IStructEnumerator<TItem>
    {
        public TItem Current { get; private set; }

        Func<TItem, int, bool> Predicate;
        TInnerEnumerator Inner;
        bool Finished;
        int Index;

        internal TakeWhileIndexedEnumerator(ref TInnerEnumerator inner, Func<TItem, int, bool> predicate)
        {
            Inner = inner;
            Predicate = predicate;
            Finished = false;
            Current = default(TItem);
            Index = 0;
        }

        public bool IsDefaultValue()
        {
            return Predicate == null;
        }

        public bool MoveNext()
        {
            if (Finished) return false;

            if (!Inner.MoveNext())
            {
                Finished = true;
                return false;
            }

            var item = Inner.Current;

            if (!Predicate(item, Index))
            {
                Finished = true;
                return false;
            }

            Index++;

            Current = item;
            return true;
        }

        public void Reset()
        {
            Inner.Reset();
            Finished = false;
            Index = 0;
        }

        public void Dispose()
        {
            Inner.Dispose();
        }
    }

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Auto)]
    public partial struct TakeWhileIndexedEnumerable<TItem, TInnerEnumerable, TInnerEnumerator> :
        IStructEnumerable<TItem, TakeWhileIndexedEnumerator<TItem, TInnerEnumerator>>
        where TInnerEnumerable : struct, IStructEnumerable<TItem, TInnerEnumerator>
        where TInnerEnumerator : struct, IStructEnumerator<TItem>
    {
        TInnerEnumerable Inner;
        Func<TItem, int, bool> Predicate;

        internal TakeWhileIndexedEnumerable(ref TInnerEnumerable inner, Func<TItem, int, bool> predicate)
        {
            Inner = inner;
            Predicate = predicate;
        }

        public bool IsDefaultValue()
        {
            return Predicate == null;
        }

        public TakeWhileIndexedEnumerator<TItem, TInnerEnumerator> GetEnumerator()
        {
            var inner = Inner.GetEnumerator();
            return new TakeWhileIndexedEnumerator<TItem, TInnerEnumerator>(ref inner, Predicate);
        }
    }

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Auto)]
    public struct TakeLastEnumerator<TItem, TInnerEnumerator> :
        IStructEnumerator<TItem>
        where TInnerEnumerator : struct, IStructEnumerator<TItem>
    {
        public TItem Current { get; private set; }

        TInnerEnumerator Inner;
        int InnerCount;
        StructQueue<TItem> Queue;

        internal TakeLastEnumerator(ref TInnerEnumerator inner, int count)
        {
            Current = default(TItem);
            Queue = default(StructQueue<TItem>);
            InnerCount = count;
            Inner = inner;
        }

        public bool IsDefaultValue() => Inner.IsDefaultValue();

        public bool MoveNext()
        {
            if (InnerCount == 0) return false;

            if (Queue.IsDefaultValue())
            {
                Queue.Initialize(InnerCount);

                // advance until we exhaust the thing...
                while (Inner.MoveNext())
                {
                    // if we've hit size, pop oldest and push this latest
                    if (Queue.Count == InnerCount)
                    {
                        Queue.DequeueAndEnqueue(Inner.Current);
                    }
                    else
                    {
                        // otherwise, just keep pushing
                        Queue.Enqueue(Inner.Current);
                    }
                }
            }

            // yield everything in Queue
            if (Queue.Count > 0)
            {
                Current = Queue.Dequeue();
                return true;
            }

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
    public partial struct TakeLastEnumerable<TItem, TInnerEnumerable, TInnerEnumerator> :
        IStructEnumerable<TItem, TakeLastEnumerator<TItem, TInnerEnumerator>>
        where TInnerEnumerable : struct, IStructEnumerable<TItem, TInnerEnumerator>
        where TInnerEnumerator : struct, IStructEnumerator<TItem>
    {
        TInnerEnumerable Inner;
        int InnerCount;

        internal TakeLastEnumerable(ref TInnerEnumerable inner, int count)
        {
            Inner = inner;
            InnerCount = count;
        }

        public bool IsDefaultValue() => Inner.IsDefaultValue();

        public TakeLastEnumerator<TItem, TInnerEnumerator> GetEnumerator()
        {
            var e = Inner.GetEnumerator();
            return new TakeLastEnumerator<TItem, TInnerEnumerator>(ref e, InnerCount);
        }
    }
}