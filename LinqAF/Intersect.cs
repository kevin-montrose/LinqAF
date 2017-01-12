using System.Collections.Generic;

namespace LinqAF
{
    public struct IntersectDefaultEnumerator<TItem, TFirstEnumerator, TSecondEnumerator>:
        IStructEnumerator<TItem>
        where TFirstEnumerator: struct, IStructEnumerator<TItem>
        where TSecondEnumerator: struct, IStructEnumerator<TItem>
    {
        public TItem Current { get; private set; }

        TFirstEnumerator FirstInner;
        TSecondEnumerator SecondInner;
        HashSet<TItem> AlreadyYielded;
        HashSet<TItem> UniqueSecondItems;

        internal IntersectDefaultEnumerator(ref TFirstEnumerator first, ref TSecondEnumerator second)
        {
            FirstInner = first;
            SecondInner = second;
            AlreadyYielded = null;
            UniqueSecondItems = null;
            Current = default(TItem);
        }

        public bool IsDefaultValue()
        {
            return UniqueSecondItems == null && AlreadyYielded == null && FirstInner.IsDefaultValue() && SecondInner.IsDefaultValue();
        }

        public void Dispose()
        {
            FirstInner.Dispose();
            SecondInner.Dispose();
            UniqueSecondItems = null;
            AlreadyYielded = null;
        }

        public bool MoveNext()
        {
            if (UniqueSecondItems == null)
            {
                UniqueSecondItems = new HashSet<TItem>(EqualityComparer<TItem>.Default);
                while (SecondInner.MoveNext())
                {
                    UniqueSecondItems.Add(SecondInner.Current);
                }

                AlreadyYielded = new HashSet<TItem>(EqualityComparer<TItem>.Default);
            }

            while (FirstInner.MoveNext())
            {
                var cur = FirstInner.Current;
                if (UniqueSecondItems.Contains(cur))
                {
                    if (AlreadyYielded.Add(cur))
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
            UniqueSecondItems = null;
            AlreadyYielded = null;
        }
    }

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
            return FirstInner.IsDefaultValue() && SecondInner.IsDefaultValue();
        }

        public IntersectDefaultEnumerator<TItem, TFirstEnumerator, TSecondEnumerator> GetEnumerator()
        {
            var firstE = FirstInner.GetEnumerator();
            var secondE = SecondInner.GetEnumerator();

            return new IntersectDefaultEnumerator<TItem, TFirstEnumerator, TSecondEnumerator>(ref firstE, ref secondE);
        }
    }

    public struct IntersectSpecificEnumerator<TItem, TFirstEnumerator, TSecondEnumerator> :
        IStructEnumerator<TItem>
        where TFirstEnumerator : struct, IStructEnumerator<TItem>
        where TSecondEnumerator : struct, IStructEnumerator<TItem>
    {
        public TItem Current { get; private set; }

        TFirstEnumerator FirstInner;
        TSecondEnumerator SecondInner;
        HashSet<TItem> UniqueSecondItems;
        HashSet<TItem> AlreadyYielded;
        IEqualityComparer<TItem> Comparer;

        internal IntersectSpecificEnumerator(ref TFirstEnumerator first, ref TSecondEnumerator second, IEqualityComparer<TItem> comparer)
        {
            FirstInner = first;
            SecondInner = second;
            UniqueSecondItems = null;
            AlreadyYielded = null;
            Current = default(TItem);
            Comparer = comparer;
        }

        public bool IsDefaultValue()
        {
            return UniqueSecondItems == null && AlreadyYielded == null && FirstInner.IsDefaultValue() && SecondInner.IsDefaultValue();
        }

        public void Dispose()
        {
            FirstInner.Dispose();
            SecondInner.Dispose();
            UniqueSecondItems = null;
            AlreadyYielded = null;
        }

        public bool MoveNext()
        {
            if (UniqueSecondItems == null)
            {
                UniqueSecondItems = new HashSet<TItem>(Comparer);
                while (SecondInner.MoveNext())
                {
                    UniqueSecondItems.Add(SecondInner.Current);
                }

                AlreadyYielded = new HashSet<TItem>(Comparer);
            }

            while (FirstInner.MoveNext())
            {
                var cur = FirstInner.Current;
                if (UniqueSecondItems.Contains(cur))
                {
                    if (AlreadyYielded.Add(cur))
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
            UniqueSecondItems = null;
            AlreadyYielded = null;
        }
    }

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
            return FirstInner.IsDefaultValue() && SecondInner.IsDefaultValue();
        }

        public IntersectSpecificEnumerator<TItem, TFirstEnumerator, TSecondEnumerator> GetEnumerator()
        {
            var firstE = FirstInner.GetEnumerator();
            var secondE = SecondInner.GetEnumerator();

            return new IntersectSpecificEnumerator<TItem, TFirstEnumerator, TSecondEnumerator>(ref firstE, ref secondE, Comparer);
        }
    }
}
