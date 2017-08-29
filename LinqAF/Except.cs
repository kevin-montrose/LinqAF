using LinqAF.Impl;
using System.Collections.Generic;

namespace LinqAF
{
    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Auto)]
    public struct ExceptDefaultEnumerator<TItem, TFirstEnumerator, TSecondEnumerator>:
        IStructEnumerator<TItem>
        where TFirstEnumerator: struct, IStructEnumerator<TItem>
        where TSecondEnumerator: struct, IStructEnumerator<TItem>
    {
        public TItem Current { get; private set; }

        TFirstEnumerator FirstInner;
        TSecondEnumerator SecondInner;
        IndexedItemContainer<TItem> Container;
        CompactSet<TItem> AlreadyYielded;
        internal ExceptDefaultEnumerator(ref TFirstEnumerator first, ref TSecondEnumerator second)
        {
            FirstInner = first;
            SecondInner = second;
            Container = new IndexedItemContainer<TItem>();
            AlreadyYielded = new CompactSet<TItem>();
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
            Container.Dispose();
            AlreadyYielded.Dispose();
        }

        public bool MoveNext()
        {
            if(AlreadyYielded.IsDefaultValue())
            {
                AlreadyYielded.Initialize();
                Container.Initialize();
                while (SecondInner.MoveNext())
                {
                    AlreadyYielded.Add(SecondInner.Current, ref Container);
                }
            }

            while (FirstInner.MoveNext())
            {
                var cur = FirstInner.Current;
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
            Container.Reset();
            AlreadyYielded.Reset();
        }
    }

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Auto)]
    public partial struct ExceptDefaultEnumerable<TItem, TFirstEnumerable, TFirstEnumerator, TSecondEnumerable, TSecondEnumerator> :
        IStructEnumerable<TItem, ExceptDefaultEnumerator<TItem, TFirstEnumerator, TSecondEnumerator>>
        where TFirstEnumerable : struct, IStructEnumerable<TItem, TFirstEnumerator>
        where TFirstEnumerator : struct, IStructEnumerator<TItem>
        where TSecondEnumerable : struct, IStructEnumerable<TItem, TSecondEnumerator>
        where TSecondEnumerator : struct, IStructEnumerator<TItem>
    {
        TFirstEnumerable FirstInner;
        TSecondEnumerable SecondInner;
        internal ExceptDefaultEnumerable(ref TFirstEnumerable first, ref TSecondEnumerable second)
        {
            FirstInner = first;
            SecondInner = second;
        }

        public bool IsDefaultValue()
        {
            return FirstInner.IsDefaultValue();
        }

        public ExceptDefaultEnumerator<TItem, TFirstEnumerator, TSecondEnumerator> GetEnumerator()
        {
            var firstE = FirstInner.GetEnumerator();
            var secondE = SecondInner.GetEnumerator();

            return new ExceptDefaultEnumerator<TItem, TFirstEnumerator, TSecondEnumerator>(ref firstE, ref secondE);
        }
    }

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Auto)]
    public struct ExceptSpecificEnumerator<TItem, TFirstEnumerator, TSecondEnumerator> :
       IStructEnumerator<TItem>
       where TFirstEnumerator : struct, IStructEnumerator<TItem>
       where TSecondEnumerator : struct, IStructEnumerator<TItem>
    {
        public TItem Current { get; private set; }

        TFirstEnumerator FirstInner;
        TSecondEnumerator SecondInner;
        IndexedItemContainer<TItem> Container;
        CompactSet<TItem> AlreadyYielded;
        IEqualityComparer<TItem> Comparer;
        internal ExceptSpecificEnumerator(ref TFirstEnumerator first, ref TSecondEnumerator second, IEqualityComparer<TItem> comparer)
        {
            FirstInner = first;
            SecondInner = second;
            Container = new IndexedItemContainer<TItem>();
            AlreadyYielded = new CompactSet<TItem>();
            Current = default(TItem);
            Comparer = comparer;
        }

        public bool IsDefaultValue()
        {
            return FirstInner.IsDefaultValue();
        }

        public void Dispose()
        {
            FirstInner.Dispose();
            SecondInner.Dispose();
            Container.Dispose();
            AlreadyYielded.Dispose();
        }

        public bool MoveNext()
        {
            if (AlreadyYielded.IsDefaultValue())
            {
                AlreadyYielded.Initialize();
                Container.Initialize();
                while (SecondInner.MoveNext())
                {
                    AlreadyYielded.Add(SecondInner.Current, ref Container, Comparer);
                }
            }

            while (FirstInner.MoveNext())
            {
                var cur = FirstInner.Current;
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
            Container.Reset();
            AlreadyYielded.Reset();
        }
    }

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Auto)]
    public partial struct ExceptSpecificEnumerable<TItem, TFirstEnumerable, TFirstEnumerator, TSecondEnumerable, TSecondEnumerator> :
        IStructEnumerable<TItem, ExceptSpecificEnumerator<TItem, TFirstEnumerator, TSecondEnumerator>>
        where TFirstEnumerable : struct, IStructEnumerable<TItem, TFirstEnumerator>
        where TFirstEnumerator : struct, IStructEnumerator<TItem>
        where TSecondEnumerable : struct, IStructEnumerable<TItem, TSecondEnumerator>
        where TSecondEnumerator : struct, IStructEnumerator<TItem>
    {
        TFirstEnumerable FirstInner;
        TSecondEnumerable SecondInner;
        IEqualityComparer<TItem> Comparer;
        internal ExceptSpecificEnumerable(ref TFirstEnumerable first, ref TSecondEnumerable second, IEqualityComparer<TItem> comparer)
        {
            FirstInner = first;
            SecondInner = second;
            Comparer = comparer;
        }

        public bool IsDefaultValue()
        {
            return Comparer == null;
        }

        public ExceptSpecificEnumerator<TItem, TFirstEnumerator, TSecondEnumerator> GetEnumerator()
        {
            var firstE = FirstInner.GetEnumerator();
            var secondE = SecondInner.GetEnumerator();

            return new ExceptSpecificEnumerator<TItem, TFirstEnumerator, TSecondEnumerator>(ref firstE, ref secondE, Comparer);
        }
    }
}
