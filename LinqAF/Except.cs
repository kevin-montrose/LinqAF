using System;
using System.Collections.Generic;

namespace LinqAF
{
    public struct ExceptDefaultEnumerator<TItem, TFirstEnumerator, TSecondEnumerator>:
        IStructEnumerator<TItem>
        where TFirstEnumerator: struct, IStructEnumerator<TItem>
        where TSecondEnumerator: struct, IStructEnumerator<TItem>
    {
        public TItem Current { get; private set; }

        TFirstEnumerator FirstInner;
        TSecondEnumerator SecondInner;
        HashSet<TItem> AlreadyYielded;
        HashSet<TItem> UniqueSecondItems;
        internal ExceptDefaultEnumerator(ref TFirstEnumerator first, ref TSecondEnumerator second)
        {
            FirstInner = first;
            SecondInner = second;
            AlreadyYielded = null;
            UniqueSecondItems = null;
            Current = default(TItem);
        }

        public bool IsDefaultValue()
        {
            return UniqueSecondItems == null && FirstInner.IsDefaultValue() && SecondInner.IsDefaultValue();
        }

        public void Dispose()
        {
            FirstInner.Dispose();
            SecondInner.Dispose();
            AlreadyYielded = null;
            UniqueSecondItems = null;
        }

        public bool MoveNext()
        {
            if(UniqueSecondItems == null)
            {
                AlreadyYielded = new HashSet<TItem>();
                UniqueSecondItems = new HashSet<TItem>(EqualityComparer<TItem>.Default);
                while (SecondInner.MoveNext())
                {
                    UniqueSecondItems.Add(SecondInner.Current);
                }
            }

            while (FirstInner.MoveNext())
            {
                var cur = FirstInner.Current;
                if (!UniqueSecondItems.Contains(cur) && AlreadyYielded.Add(cur))
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
            AlreadyYielded = null;
            UniqueSecondItems = null;
        }
    }

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
            return FirstInner.IsDefaultValue() && SecondInner.IsDefaultValue();
        }

        public ExceptDefaultEnumerator<TItem, TFirstEnumerator, TSecondEnumerator> GetEnumerator()
        {
            var firstE = FirstInner.GetEnumerator();
            var secondE = SecondInner.GetEnumerator();

            return new ExceptDefaultEnumerator<TItem, TFirstEnumerator, TSecondEnumerator>(ref firstE, ref secondE);
        }
    }

    public struct ExceptSpecificEnumerator<TItem, TFirstEnumerator, TSecondEnumerator> :
       IStructEnumerator<TItem>
       where TFirstEnumerator : struct, IStructEnumerator<TItem>
       where TSecondEnumerator : struct, IStructEnumerator<TItem>
    {
        public TItem Current { get; private set; }

        TFirstEnumerator FirstInner;
        TSecondEnumerator SecondInner;
        HashSet<TItem> AlreadyYielded;
        HashSet<TItem> UniqueSecondItems;
        IEqualityComparer<TItem> Comparer;
        internal ExceptSpecificEnumerator(ref TFirstEnumerator first, ref TSecondEnumerator second, IEqualityComparer<TItem> comparer)
        {
            FirstInner = first;
            SecondInner = second;
            AlreadyYielded = null;
            UniqueSecondItems = null;
            Current = default(TItem);
            Comparer = comparer;
        }

        public bool IsDefaultValue()
        {
            return UniqueSecondItems == null && FirstInner.IsDefaultValue() && SecondInner.IsDefaultValue();
        }

        public void Dispose()
        {
            FirstInner.Dispose();
            SecondInner.Dispose();
            AlreadyYielded = null;
            UniqueSecondItems = null;
        }

        public bool MoveNext()
        {
            if (UniqueSecondItems == null)
            {
                AlreadyYielded = new HashSet<TItem>(Comparer);
                UniqueSecondItems = new HashSet<TItem>(Comparer);
                while (SecondInner.MoveNext())
                {
                    UniqueSecondItems.Add(SecondInner.Current);
                }
            }

            while (FirstInner.MoveNext())
            {
                var cur = FirstInner.Current;
                if (!UniqueSecondItems.Contains(cur) && AlreadyYielded.Add(cur))
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
            AlreadyYielded = null;
            UniqueSecondItems = null;
        }
    }

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
            return Comparer == null && FirstInner.IsDefaultValue() && SecondInner.IsDefaultValue();
        }

        public ExceptSpecificEnumerator<TItem, TFirstEnumerator, TSecondEnumerator> GetEnumerator()
        {
            var firstE = FirstInner.GetEnumerator();
            var secondE = SecondInner.GetEnumerator();

            return new ExceptSpecificEnumerator<TItem, TFirstEnumerator, TSecondEnumerator>(ref firstE, ref secondE, Comparer);
        }
    }
}
