using LinqAF.Config;
using LinqAF.Impl;
using System.Collections.Generic;

namespace LinqAF
{
    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Auto)]
    public struct IntersectDefaultEnumerator<TItem, TFirstEnumerator, TSecondEnumerator>:
        IStructEnumerator<TItem>
        where TFirstEnumerator: struct, IStructEnumerator<TItem>
        where TSecondEnumerator: struct, IStructEnumerator<TItem>
    {
        public TItem Current { get; private set; }

        TFirstEnumerator FirstInner;
        TSecondEnumerator SecondInner;
        IndexedItemContainer<TItem> Container;
        CompactSet<TItem> AlreadyYielded;
        CompactSet<TItem> UniqueSecondItems;

        internal IntersectDefaultEnumerator(ref TFirstEnumerator first, ref TSecondEnumerator second)
        {
            FirstInner = first;
            SecondInner = second;
            AlreadyYielded = new CompactSet<TItem>();
            UniqueSecondItems = new CompactSet<TItem>();
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
            UniqueSecondItems.Dispose();
            AlreadyYielded.Dispose();
            Container.Dispose();
        }

        public bool MoveNext()
        {
            if (UniqueSecondItems.IsDefaultValue())
            {
                Container.Initialize();
                UniqueSecondItems.Initialize();
                while (SecondInner.MoveNext())
                {
                    UniqueSecondItems.Add(SecondInner.Current, ref Container);
                }

                AlreadyYielded.Initialize();
            }

            while (FirstInner.MoveNext())
            {
                var cur = FirstInner.Current;
                int existingValueIndex;
                if (UniqueSecondItems.Contains(cur, ref Container, out existingValueIndex))
                {
                    if (AlreadyYielded.AddByIndex(existingValueIndex, ref Container))
                    {
                        Current = cur;
                        return true;
                    }
                }
            }

            return false;
        }

        public void Reset()
        {
            FirstInner.Reset();
            SecondInner.Reset();
            UniqueSecondItems.Reset();
            AlreadyYielded.Reset();
            Container.Reset();
        }
    }

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Auto)]
    public partial struct IntersectDefaultEnumerable<TItem, TFirstEnumerable, TFirstEnumerator, TSecondEnumerable, TSecondEnumerator>:
        IStructEnumerable<TItem, IntersectDefaultEnumerator<TItem, TFirstEnumerator, TSecondEnumerator>>
        where TFirstEnumerable: struct, IStructEnumerable<TItem, TFirstEnumerator>
        where TFirstEnumerator: struct, IStructEnumerator<TItem>
        where TSecondEnumerable: struct, IStructEnumerable<TItem, TSecondEnumerator>
        where TSecondEnumerator: struct, IStructEnumerator<TItem>
    {
        TFirstEnumerable FirstInner;
        TSecondEnumerable SecondInner;
        internal IntersectDefaultEnumerable(ref TFirstEnumerable first, ref TSecondEnumerable second)
        {
            FirstInner = first;
            SecondInner = second;
        }

        public bool IsDefaultValue()
        {
            return FirstInner.IsDefaultValue();
        }

        public IntersectDefaultEnumerator<TItem, TFirstEnumerator, TSecondEnumerator> GetEnumerator()
        {
            var firstE = FirstInner.GetEnumerator();
            var secondE = SecondInner.GetEnumerator();

            return new IntersectDefaultEnumerator<TItem, TFirstEnumerator, TSecondEnumerator>(ref firstE, ref secondE);
        }
    }

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Auto)]
    public struct IntersectSpecificEnumerator<TItem, TFirstEnumerator, TSecondEnumerator> :
        IStructEnumerator<TItem>
        where TFirstEnumerator : struct, IStructEnumerator<TItem>
        where TSecondEnumerator : struct, IStructEnumerator<TItem>
    {
        public TItem Current { get; private set; }

        TFirstEnumerator FirstInner;
        TSecondEnumerator SecondInner;
        IndexedItemContainer<TItem> Container;
        CompactSet<TItem> AlreadyYielded;
        CompactSet<TItem> UniqueSecondItems;
        IEqualityComparer<TItem> Comparer;

        internal IntersectSpecificEnumerator(ref TFirstEnumerator first, ref TSecondEnumerator second, IEqualityComparer<TItem> comparer)
        {
            FirstInner = first;
            SecondInner = second;
            UniqueSecondItems = new CompactSet<TItem>();
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
            UniqueSecondItems.Dispose();
            AlreadyYielded.Dispose();
            Container.Dispose();
        }

        public bool MoveNext()
        {
            if (UniqueSecondItems.IsDefaultValue())
            {
                UniqueSecondItems.Initialize();
                Container.Initialize();
                while (SecondInner.MoveNext())
                {
                    UniqueSecondItems.Add(SecondInner.Current, ref Container, Comparer);
                }

                AlreadyYielded.Initialize();
            }

            while (FirstInner.MoveNext())
            {
                var cur = FirstInner.Current;
                int existingValueIndex;
                if (UniqueSecondItems.Contains(cur, ref Container, Comparer, out existingValueIndex))
                {
                    if (AlreadyYielded.AddByIndex(existingValueIndex, ref Container, Comparer))
                    {
                        Current = cur;
                        return true;
                    }
                }
            }

            return false;
        }

        public void Reset()
        {
            FirstInner.Reset();
            SecondInner.Reset();
            UniqueSecondItems.Reset();
            AlreadyYielded.Reset();
            Container.Reset();
        }
    }

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Auto)]
    public partial struct IntersectSpecificEnumerable<TItem, TFirstEnumerable, TFirstEnumerator, TSecondEnumerable, TSecondEnumerator> :
        IStructEnumerable<TItem, IntersectSpecificEnumerator<TItem, TFirstEnumerator, TSecondEnumerator>>
        where TFirstEnumerable : struct, IStructEnumerable<TItem, TFirstEnumerator>
        where TFirstEnumerator : struct, IStructEnumerator<TItem>
        where TSecondEnumerable : struct, IStructEnumerable<TItem, TSecondEnumerator>
        where TSecondEnumerator : struct, IStructEnumerator<TItem>
    {
        TFirstEnumerable FirstInner;
        TSecondEnumerable SecondInner;
        IEqualityComparer<TItem> Comparer;
        internal IntersectSpecificEnumerable(ref TFirstEnumerable first, ref TSecondEnumerable second, IEqualityComparer<TItem> comparer)
        {
            FirstInner = first;
            SecondInner = second;
            Comparer = comparer;
        }

        public bool IsDefaultValue()
        {
            return Comparer == null;
        }

        public IntersectSpecificEnumerator<TItem, TFirstEnumerator, TSecondEnumerator> GetEnumerator()
        {
            var firstE = FirstInner.GetEnumerator();
            var secondE = SecondInner.GetEnumerator();

            return new IntersectSpecificEnumerator<TItem, TFirstEnumerator, TSecondEnumerator>(ref firstE, ref secondE, Comparer);
        }
    }
}
