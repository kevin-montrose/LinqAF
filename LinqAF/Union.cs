using LinqAF.Impl;
using System.Collections.Generic;

namespace LinqAF
{
    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Auto)]
    public struct UnionDefaultEnumerator<TItem, TFirstEnumerator, TSecondEnumerator>:
        IStructEnumerator<TItem>
        where TFirstEnumerator : struct, IStructEnumerator<TItem>
        where TSecondEnumerator: struct, IStructEnumerator<TItem>
    {
        public TItem Current { get; private set; }

        TFirstEnumerator FirstInner;
        bool FirstFinished;
        TSecondEnumerator SecondInner;
        IndexedItemContainer<TItem> Container;
        CompactSet<TItem> AlreadyYielded;
        internal UnionDefaultEnumerator(ref TFirstEnumerator first, ref TSecondEnumerator second)
        {
            FirstInner = first;
            FirstFinished = false;
            SecondInner = second;
            AlreadyYielded = new CompactSet<TItem>();
            Container = new IndexedItemContainer<TItem>();
            Current = default(TItem);
        }

        public bool IsDefaultValue()
        {
            return FirstInner.IsDefaultValue();
        }

        public void Dispose()
        {
            FirstInner.Dispose();
            SecondInner.Dispose();
            AlreadyYielded.Dispose();
            Container.Dispose();
        }

        public bool MoveNext()
        {
            if (AlreadyYielded.IsDefaultValue())
            {
                AlreadyYielded.Initialize();
                Container.Initialize();
            }
            
            if (!FirstFinished)
            {
                while (FirstInner.MoveNext())
                {
                    var cur = FirstInner.Current;

                    if (AlreadyYielded.Add(cur, ref Container))
                    {
                        Current = cur;
                        return true;
                    }
                }

                FirstFinished = true;
            }

            while (SecondInner.MoveNext())
            {
                var cur = SecondInner.Current;

                if (AlreadyYielded.Add(cur, ref Container))
                {
                    Current = cur;
                    return true;
                }
            }

            return false;
        }

        public void Reset()
        {
            FirstInner.Reset();
            SecondInner.Reset();

            FirstFinished = false;
            AlreadyYielded.Reset();
            Container.Reset();
        }
    }

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Auto)]
    public partial struct UnionDefaultEnumerable<TItem, TFirstEnumerable, TFirstEnumerator, TSecondEnumerable, TSecondEnumerator>:
        IStructEnumerable<TItem, UnionDefaultEnumerator<TItem, TFirstEnumerator, TSecondEnumerator>>
        where TFirstEnumerable: struct, IStructEnumerable<TItem, TFirstEnumerator>
        where TFirstEnumerator: struct, IStructEnumerator<TItem>
        where TSecondEnumerable: struct, IStructEnumerable<TItem, TSecondEnumerator>
        where TSecondEnumerator: struct, IStructEnumerator<TItem>
    {
        TFirstEnumerable FirstInner;
        TSecondEnumerable SecondInner;
        internal UnionDefaultEnumerable(ref TFirstEnumerable first, ref TSecondEnumerable second)
        {
            FirstInner = first;
            SecondInner = second;
        }

        public bool IsDefaultValue()
        {
            return FirstInner.IsDefaultValue();
        }

        public UnionDefaultEnumerator<TItem, TFirstEnumerator, TSecondEnumerator> GetEnumerator()
        {
            var firstE = FirstInner.GetEnumerator();
            var secondE = SecondInner.GetEnumerator();

            return new UnionDefaultEnumerator<TItem, TFirstEnumerator, TSecondEnumerator>(ref firstE, ref secondE);
        }
    }

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Auto)]
    public struct UnionSpecificEnumerator<TItem, TFirstEnumerator, TSecondEnumerator> :
        IStructEnumerator<TItem>
        where TFirstEnumerator : struct, IStructEnumerator<TItem>
        where TSecondEnumerator : struct, IStructEnumerator<TItem>
    {
        public TItem Current { get; private set; }

        TFirstEnumerator FirstInner;
        bool FirstFinished;
        TSecondEnumerator SecondInner;
        IndexedItemContainer<TItem> Container;
        CompactSet<TItem> AlreadyYielded;
        IEqualityComparer<TItem> Comparer;
        internal UnionSpecificEnumerator(ref TFirstEnumerator first, ref TSecondEnumerator second, IEqualityComparer<TItem> comparer)
        {
            FirstInner = first;
            FirstFinished = false;
            SecondInner = second;
            AlreadyYielded = new CompactSet<TItem>();
            Container = new IndexedItemContainer<TItem>();
            Current = default(TItem);
            Comparer = comparer;
        }

        public bool IsDefaultValue()
        {
            return Comparer == null;
        }

        public void Dispose()
        {
            FirstInner.Dispose();
            SecondInner.Dispose();
            AlreadyYielded.Dispose();
            Container.Dispose();
        }

        public bool MoveNext()
        {
            if (AlreadyYielded.IsDefaultValue())
            {
                AlreadyYielded.Initialize();
                Container.Initialize();
            }
            
            if (!FirstFinished)
            {
                while (FirstInner.MoveNext())
                {
                    var cur = FirstInner.Current;

                    if (AlreadyYielded.Add(cur, ref Container, Comparer))
                    {
                        Current = cur;
                        return true;
                    }
                }

                FirstFinished = true;
            }

            while (SecondInner.MoveNext())
            {
                var cur = SecondInner.Current;

                if (AlreadyYielded.Add(cur, ref Container, Comparer))
                {
                    Current = cur;
                    return true;
                }
            }

            return false;
        }

        public void Reset()
        {
            FirstInner.Reset();
            SecondInner.Reset();

            FirstFinished = false;
            AlreadyYielded.Reset();
            Container.Reset();
        }
    }

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Auto)]
    public partial struct UnionSpecificEnumerable<TItem, TFirstEnumerable, TFirstEnumerator, TSecondEnumerable, TSecondEnumerator> :
        IStructEnumerable<TItem, UnionSpecificEnumerator<TItem, TFirstEnumerator, TSecondEnumerator>>
        where TFirstEnumerable : struct, IStructEnumerable<TItem, TFirstEnumerator>
        where TFirstEnumerator : struct, IStructEnumerator<TItem>
        where TSecondEnumerable : struct, IStructEnumerable<TItem, TSecondEnumerator>
        where TSecondEnumerator : struct, IStructEnumerator<TItem>
    {
        TFirstEnumerable FirstInner;
        TSecondEnumerable SecondInner;
        IEqualityComparer<TItem> Comparer;
        internal UnionSpecificEnumerable(ref TFirstEnumerable first, ref TSecondEnumerable second, IEqualityComparer<TItem> comparer)
        {
            FirstInner = first;
            SecondInner = second;
            Comparer = comparer;
        }

        public bool IsDefaultValue()
        {
            return Comparer == null;
        }

        public UnionSpecificEnumerator<TItem, TFirstEnumerator, TSecondEnumerator> GetEnumerator()
        {
            var firstE = FirstInner.GetEnumerator();
            var secondE = SecondInner.GetEnumerator();

            return new UnionSpecificEnumerator<TItem, TFirstEnumerator, TSecondEnumerator>(ref firstE, ref secondE, Comparer);
        }
    }
}
