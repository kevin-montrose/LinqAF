using System;
using System.Collections.Generic;
using LinqAF.Impl;

namespace LinqAF
{
    public struct GroupJoinDefaultEnumerator<TOutItem, TKeyItem, TLeftItem, TLeftEnumerator, TRightItem, TRightEnumerator> :
        IStructEnumerator<TOutItem>
        where TLeftEnumerator : struct, IStructEnumerator<TLeftItem>
        where TRightEnumerator : struct, IStructEnumerator<TRightItem>
    {
        public TOutItem Current { get; private set; }

        TLeftEnumerator Left;
        TRightEnumerator Right;
        
        Dictionary<TKeyItem, GroupingEnumerable<TKeyItem, TRightItem>> InnerLookup;

        Func<TLeftItem, TKeyItem> LeftKeySelector;
        Func<TRightItem, TKeyItem> RightKeySelector;
        Func<TLeftItem, GroupedEnumerable<TKeyItem, TRightItem>, TOutItem> ResultSelector;
        
        internal GroupJoinDefaultEnumerator(
            ref TLeftEnumerator left,
            ref TRightEnumerator right,
            Func<TLeftItem, TKeyItem> leftKeySelector,
            Func<TRightItem, TKeyItem> rightKeySelector,
            Func<TLeftItem, GroupedEnumerable<TKeyItem, TRightItem>, TOutItem> resultSelector
        )
        {
            Left = left;
            Right = right;
            LeftKeySelector = leftKeySelector;
            RightKeySelector = rightKeySelector;
            ResultSelector = resultSelector;
            InnerLookup = null;
            Current = default(TOutItem);

        }

        public bool IsDefaultValue()
        {
            return
                LeftKeySelector == null &&
                RightKeySelector == null &&
                ResultSelector == null &&
                InnerLookup == null &&
                Left.IsDefaultValue() &&
                Right.IsDefaultValue();
        }

        public bool MoveNext()
        {
            if (InnerLookup == null)
            {
                InnerLookup = new Dictionary<TKeyItem, GroupingEnumerable<TKeyItem, TRightItem>>(EqualityComparer<TKeyItem>.Default);
                while (Right.MoveNext())
                {
                    var cur = Right.Current;
                    var curKey = RightKeySelector(cur);

                    if (curKey == null)
                    {
                        continue;
                    }

                    GroupingEnumerable<TKeyItem, TRightItem> forKey;
                    if (!InnerLookup.TryGetValue(curKey, out forKey))
                    {
                        InnerLookup[curKey] = forKey = new GroupingEnumerable<TKeyItem, TRightItem>(curKey, new List<TRightItem>());
                    }
                    // note: DANGER ZONE
                    //   this only works because Inner is a reference type, but any other modification
                    ///  to forKey will be lost
                    forKey.Inner.Add(cur);
                }
            }

            while (Left.MoveNext())
            {
                var cur = Left.Current;
                var curKey = LeftKeySelector(cur);

                if (curKey == null)
                {
                    var nullGroup = EmptyCache<TKeyItem, TRightItem>.EmptyGrouping;
                    Current = ResultSelector(cur, new GroupedEnumerable<TKeyItem, TRightItem>(ref nullGroup));
                    return true;
                }

                GroupingEnumerable<TKeyItem, TRightItem> forKey;
                if (InnerLookup.TryGetValue(curKey, out forKey))
                {
                    var asGrouped = new GroupedEnumerable<TKeyItem, TRightItem>(ref forKey);
                    Current = ResultSelector(cur, asGrouped);
                    return true;
                }
                else
                {
                    var emptyGrouping = new GroupingEnumerable<TKeyItem, TRightItem>(curKey, EmptyCache<TRightItem>.List);
                    Current = ResultSelector(cur, new GroupedEnumerable<TKeyItem, TRightItem>(ref emptyGrouping));
                    return true;
                }
            }

            return false;
        }

        public void Reset()
        {
            Left.Reset();
            Right.Reset();
            InnerLookup = null;
        }

        public void Dispose()
        {
            Left.Dispose();
            Right.Dispose();
            InnerLookup = null;
        }
    }

    public partial struct GroupJoinDefaultEnumerable<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, TRightItem, TRightEnumerable, TRightEnumerator>:
        IStructEnumerable<TOutItem, GroupJoinDefaultEnumerator<TOutItem, TKeyItem, TLeftItem, TLeftEnumerator, TRightItem, TRightEnumerator>>
        where TLeftEnumerable : struct, IStructEnumerable<TLeftItem, TLeftEnumerator>
        where TLeftEnumerator : struct, IStructEnumerator<TLeftItem>
        where TRightEnumerable : struct, IStructEnumerable<TRightItem, TRightEnumerator>
        where TRightEnumerator : struct, IStructEnumerator<TRightItem>
    {
        TLeftEnumerable Left;
        TRightEnumerable Right;
        Func<TLeftItem, TKeyItem> LeftKeySelector;
        Func<TRightItem, TKeyItem> RightKeySelector;
        Func<TLeftItem, GroupedEnumerable<TKeyItem, TRightItem>, TOutItem> ResultSelector;
        internal GroupJoinDefaultEnumerable(ref TLeftEnumerable left, ref TRightEnumerable right, Func<TLeftItem, TKeyItem> leftKeySelector, Func<TRightItem, TKeyItem> rightKeySelector, Func<TLeftItem, GroupedEnumerable<TKeyItem, TRightItem>, TOutItem> resultSelector)
        {
            Left = left;
            Right = right;
            LeftKeySelector = leftKeySelector;
            RightKeySelector = rightKeySelector;
            ResultSelector = resultSelector;
        }

        public bool IsDefaultValue()
        {
            return
                LeftKeySelector == null &&
                RightKeySelector == null &&
                ResultSelector == null &&
                Left.IsDefaultValue() &&
                Right.IsDefaultValue();
        }

        public GroupJoinDefaultEnumerator<TOutItem, TKeyItem, TLeftItem, TLeftEnumerator, TRightItem, TRightEnumerator> GetEnumerator()
        {
            var leftE = Left.GetEnumerator();
            var rightE = Right.GetEnumerator();

            return new GroupJoinDefaultEnumerator<TOutItem, TKeyItem, TLeftItem, TLeftEnumerator, TRightItem, TRightEnumerator>(ref leftE, ref rightE, LeftKeySelector, RightKeySelector, ResultSelector);
        }
    }

    public struct GroupJoinSpecificEnumerator<TOutItem, TKeyItem, TLeftItem, TLeftEnumerator, TRightItem, TRightEnumerator> :
        IStructEnumerator<TOutItem>
        where TLeftEnumerator : struct, IStructEnumerator<TLeftItem>
        where TRightEnumerator : struct, IStructEnumerator<TRightItem>
    {
        public TOutItem Current { get; private set; }

        TLeftEnumerator Left;
        TRightEnumerator Right;
        
        Dictionary<TKeyItem, GroupingEnumerable<TKeyItem, TRightItem>> InnerLookup;

        Func<TLeftItem, TKeyItem> LeftKeySelector;
        Func<TRightItem, TKeyItem> RightKeySelector;
        Func<TLeftItem, GroupedEnumerable<TKeyItem, TRightItem>, TOutItem> ResultSelector;
        IEqualityComparer<TKeyItem> Comparer;

        internal GroupJoinSpecificEnumerator(
            ref TLeftEnumerator left,
            ref TRightEnumerator right,
            Func<TLeftItem, TKeyItem> leftKeySelector,
            Func<TRightItem, TKeyItem> rightKeySelector,
            Func<TLeftItem, GroupedEnumerable<TKeyItem, TRightItem>, TOutItem> resultSelector,
            IEqualityComparer<TKeyItem> comparer
        )
        {
            Left = left;
            Right = right;
            LeftKeySelector = leftKeySelector;
            RightKeySelector = rightKeySelector;
            ResultSelector = resultSelector;
            InnerLookup = null;
            Current = default(TOutItem);
            Comparer = comparer;
        }

        public bool IsDefaultValue()
        {
            return
                LeftKeySelector == null &&
                RightKeySelector == null &&
                ResultSelector == null &&
                InnerLookup == null &&
                Comparer == null &&
                Left.IsDefaultValue() &&
                Right.IsDefaultValue();
        }

        public bool MoveNext()
        {
            if (InnerLookup == null)
            {
                InnerLookup = new Dictionary<TKeyItem, GroupingEnumerable<TKeyItem, TRightItem>>(Comparer);
                while (Right.MoveNext())
                {
                    var cur = Right.Current;
                    var curKey = RightKeySelector(cur);

                    if (curKey == null)
                    {
                        continue;
                    }

                    GroupingEnumerable<TKeyItem, TRightItem> forKey;
                    if (!InnerLookup.TryGetValue(curKey, out forKey))
                    {
                        InnerLookup[curKey] = forKey = new GroupingEnumerable<TKeyItem, TRightItem>(curKey, new List<TRightItem>());
                    }
                    // note: DANGER ZONE
                    //   this only works because Inner is a reference type, but any other modification
                    ///  to forKey will be lost
                    forKey.Inner.Add(cur);
                }
            }

            while (Left.MoveNext())
            {
                var cur = Left.Current;
                var curKey = LeftKeySelector(cur);

                if (curKey == null)
                {
                    var nullGroup = EmptyCache<TKeyItem, TRightItem>.EmptyGrouping;
                    Current = ResultSelector(cur, new GroupedEnumerable<TKeyItem, TRightItem>(ref nullGroup));
                    return true;
                }

                GroupingEnumerable<TKeyItem, TRightItem> forKey;
                if (InnerLookup.TryGetValue(curKey, out forKey))
                {
                    var asGrouped = new GroupedEnumerable<TKeyItem, TRightItem>(ref forKey);
                    Current = ResultSelector(cur, asGrouped);
                    return true;
                }
                else
                {
                    var emptyGrouping = new GroupingEnumerable<TKeyItem, TRightItem>(curKey, EmptyCache<TRightItem>.List);
                    Current = ResultSelector(cur, new GroupedEnumerable<TKeyItem, TRightItem>(ref emptyGrouping));
                    return true;
                }
            }

            return false;
        }

        public void Reset()
        {
            Left.Reset();
            Right.Reset();
            InnerLookup = null;
        }

        public void Dispose()
        {
            Left.Dispose();
            Right.Dispose();
            InnerLookup = null;
        }
    }

    public partial struct GroupJoinSpecificEnumerable<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, TRightItem, TRightEnumerable, TRightEnumerator> :
        IStructEnumerable<TOutItem, GroupJoinSpecificEnumerator<TOutItem, TKeyItem, TLeftItem, TLeftEnumerator, TRightItem, TRightEnumerator>>
        where TLeftEnumerable : struct, IStructEnumerable<TLeftItem, TLeftEnumerator>
        where TLeftEnumerator : struct, IStructEnumerator<TLeftItem>
        where TRightEnumerable : struct, IStructEnumerable<TRightItem, TRightEnumerator>
        where TRightEnumerator : struct, IStructEnumerator<TRightItem>
    {
        TLeftEnumerable Left;
        TRightEnumerable Right;
        Func<TLeftItem, TKeyItem> LeftKeySelector;
        Func<TRightItem, TKeyItem> RightKeySelector;
        Func<TLeftItem, GroupedEnumerable<TKeyItem, TRightItem>, TOutItem> ResultSelector;
        IEqualityComparer<TKeyItem> Comparer;
        internal GroupJoinSpecificEnumerable(ref TLeftEnumerable left, ref TRightEnumerable right, Func<TLeftItem, TKeyItem> leftKeySelector, Func<TRightItem, TKeyItem> rightKeySelector, Func<TLeftItem, GroupedEnumerable<TKeyItem, TRightItem>, TOutItem> resultSelector, IEqualityComparer<TKeyItem> comparer)
        {
            Left = left;
            Right = right;
            LeftKeySelector = leftKeySelector;
            RightKeySelector = rightKeySelector;
            ResultSelector = resultSelector;
            Comparer = comparer;
        }

        public bool IsDefaultValue()
        {
            return
                LeftKeySelector == null &&
                RightKeySelector == null &&
                ResultSelector == null &&
                Comparer == null &&
                Left.IsDefaultValue() &&
                Right.IsDefaultValue();
        }

        public GroupJoinSpecificEnumerator<TOutItem, TKeyItem, TLeftItem, TLeftEnumerator, TRightItem, TRightEnumerator> GetEnumerator()
        {
            var leftE = Left.GetEnumerator();
            var rightE = Right.GetEnumerator();

            return new GroupJoinSpecificEnumerator<TOutItem, TKeyItem, TLeftItem, TLeftEnumerator, TRightItem, TRightEnumerator>(ref leftE, ref rightE, LeftKeySelector, RightKeySelector, ResultSelector, Comparer);
        }
    }
}
