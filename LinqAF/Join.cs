using LinqAF.Config;
using LinqAF.Impl;
using System;
using System.Collections.Generic;

namespace LinqAF
{
    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Auto)]
    public struct JoinDefaultEnumerator<TOutItem, TKeyItem, TLeftItem, TLeftEnumerator, TRightItem, TRightEnumerator>:
        IStructEnumerator<TOutItem>
        where TLeftEnumerator: struct, IStructEnumerator<TLeftItem>
        where TRightEnumerator: struct, IStructEnumerator<TRightItem>
    {
        public TOutItem Current { get; private set; }

        TLeftEnumerator Left;
        TRightEnumerator Right;

        LookupDefaultEnumerable<TKeyItem, TRightItem> InnerLookup;

        Func<TLeftItem, TKeyItem> LeftKeySelector;
        Func<TRightItem, TKeyItem> RightKeySelector;
        Func<TLeftItem, TRightItem, TOutItem> ResultSelector;

        TLeftItem CurrentKey;
        GroupingEnumerator<TRightItem> NeedsYield;

        internal JoinDefaultEnumerator(
            ref TLeftEnumerator left, 
            ref TRightEnumerator right, 
            Func<TLeftItem, TKeyItem> leftKeySelector,
            Func<TRightItem, TKeyItem> rightKeySelector,
            Func<TLeftItem, TRightItem, TOutItem> resultSelector
        )
        {
            Left = left;
            Right = right;
            LeftKeySelector = leftKeySelector;
            RightKeySelector = rightKeySelector;
            ResultSelector = resultSelector;
            InnerLookup = default(LookupDefaultEnumerable<TKeyItem, TRightItem>);
            Current = default(TOutItem);
            NeedsYield = default(GroupingEnumerator<TRightItem>);
            CurrentKey = default(TLeftItem);

        }

        public bool IsDefaultValue()
        {
            return LeftKeySelector == null;
        }

        public bool MoveNext()
        {
            if(InnerLookup.IsDefaultValue())
            {
                InnerLookup = CommonImplementation.ToLookupImpl(ref Right, RightKeySelector);
            }

            continueYielding:
            if (!NeedsYield.IsDefaultValue())
            {
                if (NeedsYield.MoveNext())
                {
                    var rightCur = NeedsYield.Current;
                    Current = ResultSelector(CurrentKey, rightCur);
                    return true;
                }

                NeedsYield.Dispose();
                NeedsYield = default(GroupingEnumerator<TRightItem>);
            }

            while (Left.MoveNext())
            {
                var cur = Left.Current;
                var curKey = LeftKeySelector(cur);

                if (curKey == null) continue;

                CurrentKey = cur;
                NeedsYield = InnerLookup[curKey].GetEnumerator();
                goto continueYielding;
            }

            return false;
        }

        public void Reset()
        {
            Left.Reset();
            Right.Reset();
            InnerLookup = default(LookupDefaultEnumerable<TKeyItem, TRightItem>);
            NeedsYield = default(GroupingEnumerator<TRightItem>);
            CurrentKey = default(TLeftItem);
        }

        public void Dispose()
        {
            Left.Dispose();
            Right.Dispose();
            InnerLookup = default(LookupDefaultEnumerable<TKeyItem, TRightItem>);
            NeedsYield = default(GroupingEnumerator<TRightItem>);
            CurrentKey = default(TLeftItem);
        }
    }

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Auto)]
    public partial struct JoinDefaultEnumerable<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, TRightItem, TRightEnumerable, TRightEnumerator>:
        IStructEnumerable<TOutItem, JoinDefaultEnumerator<TOutItem, TKeyItem, TLeftItem, TLeftEnumerator, TRightItem, TRightEnumerator>>
        where TLeftEnumerable: struct, IStructEnumerable<TLeftItem, TLeftEnumerator>
        where TLeftEnumerator: struct, IStructEnumerator<TLeftItem>
        where TRightEnumerable: struct, IStructEnumerable<TRightItem, TRightEnumerator>
        where TRightEnumerator: struct, IStructEnumerator<TRightItem>
    {
        TLeftEnumerable Left;
        TRightEnumerable Right;
        Func<TLeftItem, TKeyItem> LeftKeySelector;
        Func<TRightItem, TKeyItem> RightKeySelector;
        Func<TLeftItem, TRightItem, TOutItem> ResultSelector;
        internal JoinDefaultEnumerable(ref TLeftEnumerable left, ref TRightEnumerable right, Func<TLeftItem, TKeyItem> leftKeySelector, Func<TRightItem, TKeyItem> rightKeySelector, Func<TLeftItem, TRightItem, TOutItem> resultSelector)
        {
            Left = left;
            Right = right;
            LeftKeySelector = leftKeySelector;
            RightKeySelector = rightKeySelector;
            ResultSelector = resultSelector;
        }

        public bool IsDefaultValue()
        {
            return LeftKeySelector == null;
        }

        public JoinDefaultEnumerator<TOutItem, TKeyItem, TLeftItem, TLeftEnumerator, TRightItem, TRightEnumerator> GetEnumerator()
        {
            var leftE = Left.GetEnumerator();
            var rightE = Right.GetEnumerator();
            return new JoinDefaultEnumerator<TOutItem, TKeyItem, TLeftItem, TLeftEnumerator, TRightItem, TRightEnumerator>(ref leftE, ref rightE, LeftKeySelector, RightKeySelector, ResultSelector);
        }
    }

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Auto)]
    public partial struct JoinSpecificEnumerator<TOutItem, TKeyItem, TLeftItem, TLeftEnumerator, TRightItem, TRightEnumerator> :
        IStructEnumerator<TOutItem>
        where TLeftEnumerator : struct, IStructEnumerator<TLeftItem>
        where TRightEnumerator : struct, IStructEnumerator<TRightItem>
    {
        public TOutItem Current { get; private set; }

        TLeftEnumerator Left;
        TRightEnumerator Right;
        //Dictionary<TKeyItem, List<TRightItem>> InnerLookup;

        LookupSpecificEnumerable<TKeyItem, TRightItem> InnerLookup;

        Func<TLeftItem, TKeyItem> LeftKeySelector;
        Func<TRightItem, TKeyItem> RightKeySelector;
        Func<TLeftItem, TRightItem, TOutItem> ResultSelector;
        IEqualityComparer<TKeyItem> Comparer;

        TLeftItem CurrentKey;
        GroupingEnumerator<TRightItem> NeedsYield;

        internal JoinSpecificEnumerator(
            ref TLeftEnumerator left,
            ref TRightEnumerator right,
            Func<TLeftItem, TKeyItem> leftKeySelector,
            Func<TRightItem, TKeyItem> rightKeySelector,
            Func<TLeftItem, TRightItem, TOutItem> resultSelector,
            IEqualityComparer<TKeyItem> comparer
        )
        {
            Left = left;
            Right = right;
            LeftKeySelector = leftKeySelector;
            RightKeySelector = rightKeySelector;
            ResultSelector = resultSelector;
            InnerLookup = default(LookupSpecificEnumerable<TKeyItem, TRightItem>);
            Current = default(TOutItem);
            NeedsYield = default(GroupingEnumerator<TRightItem>);
            CurrentKey = default(TLeftItem);
            Comparer = comparer;

        }

        public bool IsDefaultValue()
        {
            return LeftKeySelector == null;
        }

        public bool MoveNext()
        {
            if (InnerLookup.IsDefaultValue())
            {
                InnerLookup = CommonImplementation.ToLookupImpl(ref Right, RightKeySelector, Comparer);
            }

            continueYielding:
            if (!NeedsYield.IsDefaultValue())
            {
                if (NeedsYield.MoveNext())
                {
                    var rightCur = NeedsYield.Current;
                    Current = ResultSelector(CurrentKey, rightCur);
                    return true;
                }

                NeedsYield.Dispose();
                NeedsYield = default(GroupingEnumerator<TRightItem>);
            }

            while (Left.MoveNext())
            {
                var cur = Left.Current;
                var curKey = LeftKeySelector(cur);

                if (curKey == null) continue;

                CurrentKey = cur;
                NeedsYield = InnerLookup[curKey].GetEnumerator();
                goto continueYielding;
            }

            return false;
        }

        public void Reset()
        {
            Left.Reset();
            Right.Reset();
            InnerLookup = default(LookupSpecificEnumerable<TKeyItem, TRightItem>);
            NeedsYield = default(GroupingEnumerator<TRightItem>);
            CurrentKey = default(TLeftItem);
        }

        public void Dispose()
        {
            Left.Dispose();
            Right.Dispose();
            InnerLookup = default(LookupSpecificEnumerable<TKeyItem, TRightItem>);
            NeedsYield = default(GroupingEnumerator<TRightItem>);
            CurrentKey = default(TLeftItem);
        }
    }

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Auto)]
    public partial struct JoinSpecificEnumerable<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, TRightItem, TRightEnumerable, TRightEnumerator> :
        IStructEnumerable<TOutItem, JoinSpecificEnumerator<TOutItem, TKeyItem, TLeftItem, TLeftEnumerator, TRightItem, TRightEnumerator>>
        where TLeftEnumerable : struct, IStructEnumerable<TLeftItem, TLeftEnumerator>
        where TLeftEnumerator : struct, IStructEnumerator<TLeftItem>
        where TRightEnumerable : struct, IStructEnumerable<TRightItem, TRightEnumerator>
        where TRightEnumerator : struct, IStructEnumerator<TRightItem>
    {
        TLeftEnumerable Left;
        TRightEnumerable Right;
        Func<TLeftItem, TKeyItem> LeftKeySelector;
        Func<TRightItem, TKeyItem> RightKeySelector;
        Func<TLeftItem, TRightItem, TOutItem> ResultSelector;
        IEqualityComparer<TKeyItem> Comparer;
        internal JoinSpecificEnumerable(ref TLeftEnumerable left, ref TRightEnumerable right, Func<TLeftItem, TKeyItem> leftKeySelector, Func<TRightItem, TKeyItem> rightKeySelector, Func<TLeftItem, TRightItem, TOutItem> resultSelector, IEqualityComparer<TKeyItem> comparer)
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
            return LeftKeySelector == null;
        }

        public JoinSpecificEnumerator<TOutItem, TKeyItem, TLeftItem, TLeftEnumerator, TRightItem, TRightEnumerator> GetEnumerator()
        {
            var leftE = Left.GetEnumerator();
            var rightE = Right.GetEnumerator();
            return new JoinSpecificEnumerator<TOutItem, TKeyItem, TLeftItem, TLeftEnumerator, TRightItem, TRightEnumerator>(ref leftE, ref rightE, LeftKeySelector, RightKeySelector, ResultSelector, Comparer);
        }
    }
}
