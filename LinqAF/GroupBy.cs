using System;
using System.Collections.Generic;

namespace LinqAF
{
    public struct GroupedEnumerator<TOutItem> :
        IStructEnumerator<TOutItem>
    {
        public TOutItem Current { get; private set; }

        GroupingEnumerator<TOutItem> Inner;
        internal GroupedEnumerator(ref GroupingEnumerator<TOutItem> inner)
        {
            Inner = inner;
            Current = default(TOutItem);
        }

        public bool IsDefaultValue()
        {
            return Inner.IsDefaultValue();
        }

        public void Dispose()
        {
            Inner.Dispose();
        }

        public bool MoveNext()
        {
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
            Current = default(TOutItem);
        }
    }

    public partial struct GroupedEnumerable<TKey, TOutItem>:
        IStructEnumerable<TOutItem, GroupedEnumerator<TOutItem>>
    {
        GroupingEnumerable<TKey, TOutItem> Inner;
        internal GroupedEnumerable(ref GroupingEnumerable<TKey, TOutItem> inner)
        {
            Inner = inner;
        }

        public bool IsDefaultValue()
        {
            return Inner.IsDefaultValue();
        }

        public GroupedEnumerator<TOutItem> GetEnumerator()
        {
            var e = Inner.GetEnumerator();
            return new GroupedEnumerator<TOutItem>(ref e);
        }
    }

    public struct GroupByDefaultEnumerator<TInItem, TKey, TElement, TEnumerator>:
        IStructEnumerator<GroupingEnumerable<TKey, TElement>>
        where TEnumerator: struct, IStructEnumerator<TInItem>
    {
        public GroupingEnumerable<TKey, TElement> Current { get; private set; }

        LookupEnumerator<TKey, TElement> Inner;
        
        internal GroupByDefaultEnumerator(ref LookupEnumerator<TKey, TElement> inner)
        {
            Inner = inner;
            Current = default(GroupingEnumerable<TKey, TElement>);
        }

        public bool IsDefaultValue()
        {
            return Inner.IsDefaultValue();
        }

        public void Dispose()
        {
            Inner.Dispose();
        }

        public bool MoveNext()
        {
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
            Current = default(GroupingEnumerable<TKey, TElement>);
        }
    }

    public partial struct GroupByDefaultEnumerable<TInItem, TKey, TElement, TEnumerable, TEnumerator> :
        IStructEnumerable<GroupingEnumerable<TKey, TElement>, GroupByDefaultEnumerator<TInItem, TKey, TElement, TEnumerator>>
        where TEnumerable : struct, IStructEnumerable<TInItem, TEnumerator>
        where TEnumerator : struct, IStructEnumerator<TInItem>
    {
        TEnumerable Inner;
        Func<TInItem, TKey> KeySelector;
        Func<TInItem, TElement> ElementSelector;
        internal GroupByDefaultEnumerable(ref TEnumerable inner, Func<TInItem, TKey> keySelector, Func<TInItem, TElement> elementSelector)
        {
            Inner = inner;
            KeySelector = keySelector;
            ElementSelector = elementSelector;
        }

        public bool IsDefaultValue()
        {
            return
                KeySelector == null &&
                ElementSelector == null &&
                Inner.IsDefaultValue();
        }

        public GroupByDefaultEnumerator<TInItem, TKey, TElement, TEnumerator> GetEnumerator()
        {
            var lookup = Impl.CommonImplementation.ToLookupImpl<TInItem, TKey, TElement, TEnumerable, TEnumerator>(ref Inner, KeySelector, ElementSelector, null);
            var inner = lookup.GetEnumerator();
            return new GroupByDefaultEnumerator<TInItem, TKey, TElement, TEnumerator>(ref inner);
        }
    }

    public struct GroupBySpecificEnumerator<TInItem, TKey, TElement, TEnumerator> :
        IStructEnumerator<GroupingEnumerable<TKey, TElement>>
        where TEnumerator : struct, IStructEnumerator<TInItem>
    {
        public GroupingEnumerable<TKey, TElement> Current { get; private set; }

        LookupEnumerator<TKey, TElement> Inner;

        internal GroupBySpecificEnumerator(ref LookupEnumerator<TKey, TElement> inner)
        {
            Inner = inner;
            Current = default(GroupingEnumerable<TKey, TElement>);
        }

        public bool IsDefaultValue()
        {
            return Inner.IsDefaultValue();
        }

        public void Dispose()
        {
            Inner.Dispose();
        }

        public bool MoveNext()
        {
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
            Current = default(GroupingEnumerable<TKey, TElement>);
        }
    }

    public partial struct GroupBySpecificEnumerable<TInItem, TKey, TElement, TEnumerable, TEnumerator>:
        IStructEnumerable<GroupingEnumerable<TKey, TElement>, GroupBySpecificEnumerator<TInItem, TKey, TElement, TEnumerator>>
        where TEnumerable: struct, IStructEnumerable<TInItem, TEnumerator>
        where TEnumerator: struct, IStructEnumerator<TInItem>
    {
        TEnumerable Inner;
        Func<TInItem, TKey> KeySelector;
        Func<TInItem, TElement> ElementSelector;
        IEqualityComparer<TKey> Comparer;
        internal GroupBySpecificEnumerable(ref TEnumerable inner, Func<TInItem, TKey> keySelector, Func<TInItem, TElement> elementSelector, IEqualityComparer<TKey> comparer)
        {
            Inner = inner;
            KeySelector = keySelector;
            ElementSelector = elementSelector;
            Comparer = comparer;
        }

        public bool IsDefaultValue()
        {
            return
                KeySelector == null &&
                ElementSelector == null &&
                Comparer == null &&
                Inner.IsDefaultValue();
        }

        public GroupBySpecificEnumerator<TInItem, TKey, TElement, TEnumerator> GetEnumerator()
        {
            var lookup = Impl.CommonImplementation.ToLookupImpl<TInItem, TKey, TElement, TEnumerable, TEnumerator>(ref Inner, KeySelector, ElementSelector, Comparer);
            var inner = lookup.GetEnumerator();
            return new GroupBySpecificEnumerator<TInItem, TKey, TElement, TEnumerator>(ref inner);
        }
    }

    public struct GroupByCollectionDefaultEnumerator<TInItem, TKey, TElement, TResult, TEnumerator> :
        IStructEnumerator<TResult>
        where TEnumerator : struct, IStructEnumerator<TInItem>
    {
        public TResult Current { get; private set; }

        GroupByDefaultEnumerator<TInItem, TKey, TElement, TEnumerator> Inner;
        Func<TKey, GroupedEnumerable<TKey, TElement>, TResult> ResultSelector;
        internal GroupByCollectionDefaultEnumerator(ref GroupByDefaultEnumerator<TInItem, TKey, TElement, TEnumerator> inner, Func<TKey, GroupedEnumerable<TKey, TElement>, TResult> resultSelector)
        {
            Inner = inner;
            ResultSelector = resultSelector;
            Current = default(TResult);
        }

        public bool IsDefaultValue()
        {
            return 
                ResultSelector == null &&
                Inner.IsDefaultValue();
        }

        public void Dispose()
        {
            Inner.Dispose();
        }

        public bool MoveNext()
        {
            if (Inner.MoveNext())
            {
                var cur = Inner.Current;
                Current = ResultSelector(Inner.Current.Key, new GroupedEnumerable<TKey, TElement>(ref cur));
                return true;
            }

            return false;
        }

        public void Reset()
        {
            Inner.Reset();
            Current = default(TResult);
        }
    }

    public partial struct GroupByCollectionDefaultEnumerable<TInItem, TKey, TElement, TResult, TEnumerable, TEnumerator> :
        IStructEnumerable<TResult, GroupByCollectionDefaultEnumerator<TInItem, TKey, TElement, TResult, TEnumerator>>
        where TEnumerable : struct, IStructEnumerable<TInItem, TEnumerator>
        where TEnumerator : struct, IStructEnumerator<TInItem>
    {
        TEnumerable Inner;
        Func<TInItem, TKey> KeySelector;
        Func<TInItem, TElement> ElementSelector;
        Func<TKey, GroupedEnumerable<TKey, TElement>, TResult> ResultSelector;

        internal GroupByCollectionDefaultEnumerable(ref TEnumerable inner, Func<TInItem, TKey> keySelector, Func<TInItem, TElement> elementSelector, Func<TKey, GroupedEnumerable<TKey, TElement>, TResult> resultSelector)
        {
            Inner = inner;
            KeySelector = keySelector;
            ElementSelector = elementSelector;
            ResultSelector = resultSelector;
        }

        public bool IsDefaultValue()
        {
            return
                KeySelector == null &&
                ElementSelector == null &&
                ResultSelector == null &&
                Inner.IsDefaultValue();
        }

        public GroupByCollectionDefaultEnumerator<TInItem, TKey, TElement, TResult, TEnumerator> GetEnumerator()
        {
            var grouped = Impl.CommonImplementation.GroupByImpl<TInItem, TKey, TElement, TEnumerable, TEnumerator>(ref Inner, KeySelector, ElementSelector);

            var e = grouped.GetEnumerator();
            return new GroupByCollectionDefaultEnumerator<TInItem, TKey, TElement, TResult, TEnumerator>(ref e, ResultSelector);
        }
    }

    public struct GroupByCollectionSpecificEnumerator<TInItem, TKey, TElement, TResult, TEnumerator> :
        IStructEnumerator<TResult>
        where TEnumerator : struct, IStructEnumerator<TInItem>
    {
        public TResult Current { get; private set; }

        GroupBySpecificEnumerator<TInItem, TKey, TElement, TEnumerator> Inner;
        Func<TKey, GroupedEnumerable<TKey, TElement>, TResult> ResultSelector;
        internal GroupByCollectionSpecificEnumerator(ref GroupBySpecificEnumerator<TInItem, TKey, TElement, TEnumerator> inner, Func<TKey, GroupedEnumerable<TKey, TElement>, TResult> resultSelector)
        {
            Inner = inner;
            ResultSelector = resultSelector;
            Current = default(TResult);
        }

        public bool IsDefaultValue()
        {
            return
                ResultSelector == null &&
                Inner.IsDefaultValue();
        }

        public void Dispose()
        {
            Inner.Dispose();
        }

        public bool MoveNext()
        {
            if (Inner.MoveNext())
            {
                var cur = Inner.Current;
                Current = ResultSelector(Inner.Current.Key, new GroupedEnumerable<TKey, TElement>(ref cur));
                return true;
            }

            return false;
        }

        public void Reset()
        {
            Inner.Reset();
            Current = default(TResult);
        }
    }

    public partial struct GroupByCollectionSpecificEnumerable<TInItem, TKey, TElement, TResult, TEnumerable, TEnumerator>:
        IStructEnumerable<TResult, GroupByCollectionSpecificEnumerator<TInItem, TKey, TElement, TResult, TEnumerator>>
        where TEnumerable : struct, IStructEnumerable<TInItem, TEnumerator>
        where TEnumerator : struct, IStructEnumerator<TInItem>
    {
        TEnumerable Inner;
        Func<TInItem, TKey> KeySelector;
        Func<TInItem, TElement> ElementSelector;
        Func<TKey, GroupedEnumerable<TKey, TElement>, TResult> ResultSelector;
        IEqualityComparer<TKey> Comparer;

        internal GroupByCollectionSpecificEnumerable(ref TEnumerable inner, Func<TInItem, TKey> keySelector, Func<TInItem, TElement> elementSelector, Func<TKey, GroupedEnumerable<TKey, TElement>, TResult> resultSelector, IEqualityComparer<TKey> comparer)
        {
            Inner = inner;
            KeySelector = keySelector;
            ElementSelector = elementSelector;
            ResultSelector = resultSelector;
            Comparer = comparer;
        }

        public bool IsDefaultValue()
        {
            return
                KeySelector == null &&
                ElementSelector == null &&
                ResultSelector == null &&
                Inner.IsDefaultValue();
        }

        public GroupByCollectionSpecificEnumerator<TInItem, TKey, TElement, TResult, TEnumerator> GetEnumerator()
        {
            var grouped = Impl.CommonImplementation.GroupByImpl<TInItem, TKey, TElement, TEnumerable, TEnumerator>(ref Inner, KeySelector, ElementSelector, Comparer);

            var e = grouped.GetEnumerator();
            return new GroupByCollectionSpecificEnumerator<TInItem, TKey, TElement, TResult, TEnumerator>(ref e, ResultSelector);
        }
    }
}
