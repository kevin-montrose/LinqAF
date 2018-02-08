using LinqAF.Impl;
using System.Collections.Generic;

namespace LinqAF
{
    abstract class UnionExtensionMethodsBase :
        ExtensionMethodsBase
    {
        // Union, default
        public 
            UnionDefaultEnumerable<
                GroupingEnumerable<TGenKey, TGenElement>,  
                PlaceholderEnumerable<GroupingEnumerable<TGenKey, TGenElement>>,
                PlaceholderEnumerator<GroupingEnumerable<TGenKey, TGenElement>>,
                GroupByDefaultEnumerable<TGenGroupByInItem, TGenKey, TGenElement, TGenGroupByEnumerable, TGenGroupByEnumerator>,
                GroupByDefaultEnumerator<TGenGroupByInItem, TGenKey, TGenElement, TGenGroupByEnumerator>
            > Union<TGenGroupByInItem, TGenKey, TGenElement, TGenGroupByEnumerable, TGenGroupByEnumerator>(
                PlaceholderEnumerable<GroupingEnumerable<TGenKey, TGenElement>> first,
                GroupByDefaultEnumerable<TGenGroupByInItem, TGenKey, TGenElement, TGenGroupByEnumerable, TGenGroupByEnumerator> second
            )
            where TGenGroupByEnumerable: struct, IStructEnumerable<TGenGroupByInItem, TGenGroupByEnumerator>
            where TGenGroupByEnumerator: struct, IStructEnumerator<TGenGroupByInItem>
        {
            return
                CommonImplementation.Union<
                    GroupingEnumerable<TGenKey, TGenElement>,
                    PlaceholderEnumerable<GroupingEnumerable<TGenKey, TGenElement>>,
                    PlaceholderEnumerator<GroupingEnumerable<TGenKey, TGenElement>>,
                    GroupByDefaultEnumerable<TGenGroupByInItem, TGenKey, TGenElement, TGenGroupByEnumerable, TGenGroupByEnumerator>,
                    GroupByDefaultEnumerator<TGenGroupByInItem, TGenKey, TGenElement, TGenGroupByEnumerator>
                >(
                    RefParam(first),
                    ref second
                );
        }

        public
            UnionDefaultEnumerable<
                GroupingEnumerable<TGenKey, TGenElement>,
                PlaceholderEnumerable<GroupingEnumerable<TGenKey, TGenElement>>,
                PlaceholderEnumerator<GroupingEnumerable<TGenKey, TGenElement>>,
                GroupBySpecificEnumerable<TGenGroupByInItem, TGenKey, TGenElement, TGenGroupByEnumerable, TGenGroupByEnumerator>,
                GroupBySpecificEnumerator<TGenGroupByInItem, TGenKey, TGenElement, TGenGroupByEnumerator>
            > Union<TGenGroupByInItem, TGenKey, TGenElement, TGenGroupByEnumerable, TGenGroupByEnumerator>(
                PlaceholderEnumerable<GroupingEnumerable<TGenKey, TGenElement>> first,
                GroupBySpecificEnumerable<TGenGroupByInItem, TGenKey, TGenElement, TGenGroupByEnumerable, TGenGroupByEnumerator> second
            )
            where TGenGroupByEnumerable : struct, IStructEnumerable<TGenGroupByInItem, TGenGroupByEnumerator>
            where TGenGroupByEnumerator : struct, IStructEnumerator<TGenGroupByInItem>
        {
            return
                CommonImplementation.Union<
                    GroupingEnumerable<TGenKey, TGenElement>,
                    PlaceholderEnumerable<GroupingEnumerable<TGenKey, TGenElement>>,
                    PlaceholderEnumerator<GroupingEnumerable<TGenKey, TGenElement>>,
                    GroupBySpecificEnumerable<TGenGroupByInItem, TGenKey, TGenElement, TGenGroupByEnumerable, TGenGroupByEnumerator>,
                    GroupBySpecificEnumerator<TGenGroupByInItem, TGenKey, TGenElement, TGenGroupByEnumerator>
                >(
                    RefParam(first),
                    ref second
                );
        }

        public
            UnionDefaultEnumerable<
                GroupingEnumerable<TGenLookupKey, TGenLookupElement>,
                PlaceholderEnumerable<GroupingEnumerable<TGenLookupKey, TGenLookupElement>>,
                PlaceholderEnumerator<GroupingEnumerable<TGenLookupKey, TGenLookupElement>>,
                LookupDefaultEnumerable<TGenLookupKey, TGenLookupElement>,
                LookupDefaultEnumerator<TGenLookupKey, TGenLookupElement>
            > Union<TGenLookupKey, TGenLookupElement>(
                PlaceholderEnumerable<GroupingEnumerable<TGenLookupKey, TGenLookupElement>> first,
                LookupDefaultEnumerable<TGenLookupKey, TGenLookupElement> second
            )
        {
            return
                CommonImplementation.Union<
                    GroupingEnumerable<TGenLookupKey, TGenLookupElement>,
                    PlaceholderEnumerable<GroupingEnumerable<TGenLookupKey, TGenLookupElement>>,
                    PlaceholderEnumerator<GroupingEnumerable<TGenLookupKey, TGenLookupElement>>,
                    LookupDefaultEnumerable<TGenLookupKey, TGenLookupElement>,
                    LookupDefaultEnumerator<TGenLookupKey, TGenLookupElement>
                >(
                    RefParam(first),
                    ref second
                );
        }

        public
            UnionDefaultEnumerable<
                GroupingEnumerable<TGenLookupKey, TGenLookupElement>,
                PlaceholderEnumerable<GroupingEnumerable<TGenLookupKey, TGenLookupElement>>,
                PlaceholderEnumerator<GroupingEnumerable<TGenLookupKey, TGenLookupElement>>,
                LookupSpecificEnumerable<TGenLookupKey, TGenLookupElement>,
                LookupSpecificEnumerator<TGenLookupKey, TGenLookupElement>
            > Union<TGenLookupKey, TGenLookupElement>(
                PlaceholderEnumerable<GroupingEnumerable<TGenLookupKey, TGenLookupElement>> first,
                LookupSpecificEnumerable<TGenLookupKey, TGenLookupElement> second
            )
        {
            return
                CommonImplementation.Union<
                    GroupingEnumerable<TGenLookupKey, TGenLookupElement>,
                    PlaceholderEnumerable<GroupingEnumerable<TGenLookupKey, TGenLookupElement>>,
                    PlaceholderEnumerator<GroupingEnumerable<TGenLookupKey, TGenLookupElement>>,
                    LookupSpecificEnumerable<TGenLookupKey, TGenLookupElement>,
                    LookupSpecificEnumerator<TGenLookupKey, TGenLookupElement>
                >(
                    RefParam(first),
                    ref second
                );
        }

        // Union, specific
        public
            UnionSpecificEnumerable<
                GroupingEnumerable<TGenKey, TGenElement>,
                PlaceholderEnumerable<GroupingEnumerable<TGenKey, TGenElement>>,
                PlaceholderEnumerator<GroupingEnumerable<TGenKey, TGenElement>>,
                GroupByDefaultEnumerable<TGenGroupByInItem, TGenKey, TGenElement, TGenGroupByEnumerable, TGenGroupByEnumerator>,
                GroupByDefaultEnumerator<TGenGroupByInItem, TGenKey, TGenElement, TGenGroupByEnumerator>
            > Union<TGenGroupByInItem, TGenKey, TGenElement, TGenGroupByEnumerable, TGenGroupByEnumerator>(
                PlaceholderEnumerable<GroupingEnumerable<TGenKey, TGenElement>> first,
                GroupByDefaultEnumerable<TGenGroupByInItem, TGenKey, TGenElement, TGenGroupByEnumerable, TGenGroupByEnumerator> second,
                IEqualityComparer<GroupingEnumerable<TGenKey, TGenElement>> comparer
            )
            where TGenGroupByEnumerable : struct, IStructEnumerable<TGenGroupByInItem, TGenGroupByEnumerator>
            where TGenGroupByEnumerator : struct, IStructEnumerator<TGenGroupByInItem>
        {
            return
                CommonImplementation.Union<
                    GroupingEnumerable<TGenKey, TGenElement>,
                    PlaceholderEnumerable<GroupingEnumerable<TGenKey, TGenElement>>,
                    PlaceholderEnumerator<GroupingEnumerable<TGenKey, TGenElement>>,
                    GroupByDefaultEnumerable<TGenGroupByInItem, TGenKey, TGenElement, TGenGroupByEnumerable, TGenGroupByEnumerator>,
                    GroupByDefaultEnumerator<TGenGroupByInItem, TGenKey, TGenElement, TGenGroupByEnumerator>
                >(
                    RefParam(first),
                    ref second,
                    comparer
                );
        }

        public
            UnionSpecificEnumerable<
                GroupingEnumerable<TGenKey, TGenElement>,
                PlaceholderEnumerable<GroupingEnumerable<TGenKey, TGenElement>>,
                PlaceholderEnumerator<GroupingEnumerable<TGenKey, TGenElement>>,
                GroupBySpecificEnumerable<TGenGroupByInItem, TGenKey, TGenElement, TGenGroupByEnumerable, TGenGroupByEnumerator>,
                GroupBySpecificEnumerator<TGenGroupByInItem, TGenKey, TGenElement, TGenGroupByEnumerator>
            > Union<TGenGroupByInItem, TGenKey, TGenElement, TGenGroupByEnumerable, TGenGroupByEnumerator>(
                PlaceholderEnumerable<GroupingEnumerable<TGenKey, TGenElement>> first,
                GroupBySpecificEnumerable<TGenGroupByInItem, TGenKey, TGenElement, TGenGroupByEnumerable, TGenGroupByEnumerator> second,
                IEqualityComparer<GroupingEnumerable<TGenKey, TGenElement>> comparer
            )
            where TGenGroupByEnumerable : struct, IStructEnumerable<TGenGroupByInItem, TGenGroupByEnumerator>
            where TGenGroupByEnumerator : struct, IStructEnumerator<TGenGroupByInItem>
        {
            return
                CommonImplementation.Union<
                    GroupingEnumerable<TGenKey, TGenElement>,
                    PlaceholderEnumerable<GroupingEnumerable<TGenKey, TGenElement>>,
                    PlaceholderEnumerator<GroupingEnumerable<TGenKey, TGenElement>>,
                    GroupBySpecificEnumerable<TGenGroupByInItem, TGenKey, TGenElement, TGenGroupByEnumerable, TGenGroupByEnumerator>,
                    GroupBySpecificEnumerator<TGenGroupByInItem, TGenKey, TGenElement, TGenGroupByEnumerator>
                >(
                    RefParam(first),
                    ref second,
                    comparer
                );
        }

        public
            UnionSpecificEnumerable<
                GroupingEnumerable<TGenLookupKey, TGenLookupElement>,
                PlaceholderEnumerable<GroupingEnumerable<TGenLookupKey, TGenLookupElement>>,
                PlaceholderEnumerator<GroupingEnumerable<TGenLookupKey, TGenLookupElement>>,
                LookupDefaultEnumerable<TGenLookupKey, TGenLookupElement>,
                LookupDefaultEnumerator<TGenLookupKey, TGenLookupElement>
            > Union<TGenLookupKey, TGenLookupElement>(
                PlaceholderEnumerable<GroupingEnumerable<TGenLookupKey, TGenLookupElement>> first,
                LookupDefaultEnumerable<TGenLookupKey, TGenLookupElement> second,
                IEqualityComparer<GroupingEnumerable<TGenLookupKey, TGenLookupElement>> comparer
            )
        {
            return
                CommonImplementation.Union<
                    GroupingEnumerable<TGenLookupKey, TGenLookupElement>,
                    PlaceholderEnumerable<GroupingEnumerable<TGenLookupKey, TGenLookupElement>>,
                    PlaceholderEnumerator<GroupingEnumerable<TGenLookupKey, TGenLookupElement>>,
                    LookupDefaultEnumerable<TGenLookupKey, TGenLookupElement>,
                    LookupDefaultEnumerator<TGenLookupKey, TGenLookupElement>
                >(
                    RefParam(first),
                    ref second,
                    comparer
                );
        }

        public
            UnionSpecificEnumerable<
                GroupingEnumerable<TGenLookupKey, TGenLookupElement>,
                PlaceholderEnumerable<GroupingEnumerable<TGenLookupKey, TGenLookupElement>>,
                PlaceholderEnumerator<GroupingEnumerable<TGenLookupKey, TGenLookupElement>>,
                LookupSpecificEnumerable<TGenLookupKey, TGenLookupElement>,
                LookupSpecificEnumerator<TGenLookupKey, TGenLookupElement>
            > Union<TGenLookupKey, TGenLookupElement>(
                PlaceholderEnumerable<GroupingEnumerable<TGenLookupKey, TGenLookupElement>> first,
                LookupSpecificEnumerable<TGenLookupKey, TGenLookupElement> second,
                IEqualityComparer<GroupingEnumerable<TGenLookupKey, TGenLookupElement>> comparer
            )
        {
            return
                CommonImplementation.Union<
                    GroupingEnumerable<TGenLookupKey, TGenLookupElement>,
                    PlaceholderEnumerable<GroupingEnumerable<TGenLookupKey, TGenLookupElement>>,
                    PlaceholderEnumerator<GroupingEnumerable<TGenLookupKey, TGenLookupElement>>,
                    LookupSpecificEnumerable<TGenLookupKey, TGenLookupElement>,
                    LookupSpecificEnumerator<TGenLookupKey, TGenLookupElement>
                >(
                    RefParam(first),
                    ref second,
                    comparer
                );
        }

        // Union, default - GroupByDefault

        public
            UnionDefaultEnumerable<
                GroupingEnumerable<TGenLookupKey, TGenLookupElement>,
                GroupByDefaultEnumerable<TGenGroupByInItem, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable, TGenGroupByEnumerator>,
                GroupByDefaultEnumerator<TGenGroupByInItem, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerator>,
                GroupByDefaultEnumerable<TGenGroupByInItem2, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable2, TGenGroupByEnumerator2>,
                GroupByDefaultEnumerator<TGenGroupByInItem2, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerator2>
            > Union<TGenGroupByInItem, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable, TGenGroupByEnumerator, TGenGroupByInItem2, TGenGroupByEnumerable2, TGenGroupByEnumerator2>(
            GroupByDefaultEnumerable<TGenGroupByInItem, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable, TGenGroupByEnumerator> first,
            GroupByDefaultEnumerable<TGenGroupByInItem2, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable2, TGenGroupByEnumerator2> second
        )
            where TGenGroupByEnumerable : struct, IStructEnumerable<TGenGroupByInItem, TGenGroupByEnumerator>
            where TGenGroupByEnumerator : struct, IStructEnumerator<TGenGroupByInItem>
            where TGenGroupByEnumerable2 : struct, IStructEnumerable<TGenGroupByInItem2, TGenGroupByEnumerator2>
            where TGenGroupByEnumerator2 : struct, IStructEnumerator<TGenGroupByInItem2>
        {
            return
                CommonImplementation.Union<
                    GroupingEnumerable<TGenLookupKey, TGenLookupElement>,
                    GroupByDefaultEnumerable<TGenGroupByInItem, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable, TGenGroupByEnumerator>,
                    GroupByDefaultEnumerator<TGenGroupByInItem, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerator>,
                    GroupByDefaultEnumerable<TGenGroupByInItem2, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable2, TGenGroupByEnumerator2>,
                    GroupByDefaultEnumerator<TGenGroupByInItem2, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerator2>
                >(
                    ref first,
                    ref second
                );
        }

        public
            UnionDefaultEnumerable<
                GroupingEnumerable<TGenLookupKey, TGenLookupElement>,
                GroupByDefaultEnumerable<TGenGroupByInItem, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable, TGenGroupByEnumerator>,
                GroupByDefaultEnumerator<TGenGroupByInItem, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerator>,
                GroupBySpecificEnumerable<TGenGroupByInItem2, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable2, TGenGroupByEnumerator2>,
                GroupBySpecificEnumerator<TGenGroupByInItem2, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerator2>
            > Union<TGenGroupByInItem, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable, TGenGroupByEnumerator, TGenGroupByInItem2, TGenGroupByEnumerable2, TGenGroupByEnumerator2>(
                GroupByDefaultEnumerable<TGenGroupByInItem, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable, TGenGroupByEnumerator> first,
                GroupBySpecificEnumerable<TGenGroupByInItem2, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable2, TGenGroupByEnumerator2> second
            )
                where TGenGroupByEnumerable : struct, IStructEnumerable<TGenGroupByInItem, TGenGroupByEnumerator>
                where TGenGroupByEnumerator : struct, IStructEnumerator<TGenGroupByInItem>
                where TGenGroupByEnumerable2: struct, IStructEnumerable<TGenGroupByInItem2,TGenGroupByEnumerator2>
                where TGenGroupByEnumerator2: struct, IStructEnumerator<TGenGroupByInItem2>
        {
            return
                CommonImplementation.Union<
                    GroupingEnumerable<TGenLookupKey, TGenLookupElement>,
                    GroupByDefaultEnumerable<TGenGroupByInItem, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable, TGenGroupByEnumerator>,
                    GroupByDefaultEnumerator<TGenGroupByInItem, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerator>,
                    GroupBySpecificEnumerable<TGenGroupByInItem2, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable2, TGenGroupByEnumerator2>,
                    GroupBySpecificEnumerator<TGenGroupByInItem2, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerator2>
                >(
                    ref first,
                    ref second
                );
        }

        public
            UnionDefaultEnumerable<
                GroupingEnumerable<TGenLookupKey, TGenLookupElement>,
                GroupByDefaultEnumerable<TGenGroupByInItem, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable, TGenGroupByEnumerator>,
                GroupByDefaultEnumerator<TGenGroupByInItem, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerator>,
                LookupDefaultEnumerable<TGenLookupKey, TGenLookupElement>,
                LookupDefaultEnumerator<TGenLookupKey, TGenLookupElement>
            > Union<TGenGroupByInItem, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable, TGenGroupByEnumerator>(
                GroupByDefaultEnumerable<TGenGroupByInItem, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable, TGenGroupByEnumerator> first,
                LookupDefaultEnumerable<TGenLookupKey, TGenLookupElement> second
            )
            where TGenGroupByEnumerable: struct, IStructEnumerable<TGenGroupByInItem, TGenGroupByEnumerator>
            where TGenGroupByEnumerator: struct, IStructEnumerator<TGenGroupByInItem>
        {
            return
                CommonImplementation.Union<
                    GroupingEnumerable<TGenLookupKey, TGenLookupElement>,
                    GroupByDefaultEnumerable<TGenGroupByInItem, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable, TGenGroupByEnumerator>,
                    GroupByDefaultEnumerator<TGenGroupByInItem, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerator>,
                    LookupDefaultEnumerable<TGenLookupKey, TGenLookupElement>,
                    LookupDefaultEnumerator<TGenLookupKey, TGenLookupElement>
                >(
                    ref first,
                    ref second
                );
        }

        public
            UnionDefaultEnumerable<
                GroupingEnumerable<TGenLookupKey, TGenLookupElement>,
                GroupByDefaultEnumerable<TGenGroupByInItem, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable, TGenGroupByEnumerator>,
                GroupByDefaultEnumerator<TGenGroupByInItem, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerator>,
                LookupSpecificEnumerable<TGenLookupKey, TGenLookupElement>,
                LookupSpecificEnumerator<TGenLookupKey, TGenLookupElement>
            > Union<TGenGroupByInItem, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable, TGenGroupByEnumerator>(
                GroupByDefaultEnumerable<TGenGroupByInItem, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable, TGenGroupByEnumerator> first,
                LookupSpecificEnumerable<TGenLookupKey, TGenLookupElement> second
            )
            where TGenGroupByEnumerable : struct, IStructEnumerable<TGenGroupByInItem, TGenGroupByEnumerator>
            where TGenGroupByEnumerator : struct, IStructEnumerator<TGenGroupByInItem>
        {
            return
                CommonImplementation.Union<
                    GroupingEnumerable<TGenLookupKey, TGenLookupElement>,
                    GroupByDefaultEnumerable<TGenGroupByInItem, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable, TGenGroupByEnumerator>,
                    GroupByDefaultEnumerator<TGenGroupByInItem, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerator>,
                    LookupSpecificEnumerable<TGenLookupKey, TGenLookupElement>,
                    LookupSpecificEnumerator<TGenLookupKey, TGenLookupElement>
                >(
                    ref first,
                    ref second
                );
        }

        // Union, specific - GroupByDefault

        public
            UnionSpecificEnumerable<
                GroupingEnumerable<TGenLookupKey, TGenLookupElement>,
                GroupByDefaultEnumerable<TGenGroupByInItem, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable, TGenGroupByEnumerator>,
                GroupByDefaultEnumerator<TGenGroupByInItem, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerator>,
                GroupByDefaultEnumerable<TGenGroupByInItem2, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable2, TGenGroupByEnumerator2>,
                GroupByDefaultEnumerator<TGenGroupByInItem2, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerator2>
            > Union<TGenGroupByInItem, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable, TGenGroupByEnumerator, TGenGroupByInItem2, TGenGroupByEnumerable2, TGenGroupByEnumerator2>(
            GroupByDefaultEnumerable<TGenGroupByInItem, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable, TGenGroupByEnumerator> first,
            GroupByDefaultEnumerable<TGenGroupByInItem2, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable2, TGenGroupByEnumerator2> second,
            IEqualityComparer<GroupingEnumerable<TGenLookupKey, TGenLookupElement>> comparer
        )
            where TGenGroupByEnumerable : struct, IStructEnumerable<TGenGroupByInItem, TGenGroupByEnumerator>
            where TGenGroupByEnumerator : struct, IStructEnumerator<TGenGroupByInItem>
            where TGenGroupByEnumerable2 : struct, IStructEnumerable<TGenGroupByInItem2, TGenGroupByEnumerator2>
            where TGenGroupByEnumerator2 : struct, IStructEnumerator<TGenGroupByInItem2>
        {
            return
                CommonImplementation.Union<
                    GroupingEnumerable<TGenLookupKey, TGenLookupElement>,
                    GroupByDefaultEnumerable<TGenGroupByInItem, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable, TGenGroupByEnumerator>,
                    GroupByDefaultEnumerator<TGenGroupByInItem, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerator>,
                    GroupByDefaultEnumerable<TGenGroupByInItem2, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable2, TGenGroupByEnumerator2>,
                    GroupByDefaultEnumerator<TGenGroupByInItem2, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerator2>
                >(
                    ref first,
                    ref second,
                    comparer
                );
        }

        public
            UnionSpecificEnumerable<
                GroupingEnumerable<TGenLookupKey, TGenLookupElement>,
                GroupByDefaultEnumerable<TGenGroupByInItem, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable, TGenGroupByEnumerator>,
                GroupByDefaultEnumerator<TGenGroupByInItem, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerator>,
                GroupBySpecificEnumerable<TGenGroupByInItem2, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable2, TGenGroupByEnumerator2>,
                GroupBySpecificEnumerator<TGenGroupByInItem2, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerator2>
            > Union<TGenGroupByInItem, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable, TGenGroupByEnumerator, TGenGroupByInItem2, TGenGroupByEnumerable2, TGenGroupByEnumerator2>(
                GroupByDefaultEnumerable<TGenGroupByInItem, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable, TGenGroupByEnumerator> first,
                GroupBySpecificEnumerable<TGenGroupByInItem2, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable2, TGenGroupByEnumerator2> second,
                IEqualityComparer<GroupingEnumerable<TGenLookupKey, TGenLookupElement>> comparer
            )
                where TGenGroupByEnumerable : struct, IStructEnumerable<TGenGroupByInItem, TGenGroupByEnumerator>
                where TGenGroupByEnumerator : struct, IStructEnumerator<TGenGroupByInItem>
                where TGenGroupByEnumerable2 : struct, IStructEnumerable<TGenGroupByInItem2, TGenGroupByEnumerator2>
                where TGenGroupByEnumerator2 : struct, IStructEnumerator<TGenGroupByInItem2>
        {
            return
                CommonImplementation.Union<
                    GroupingEnumerable<TGenLookupKey, TGenLookupElement>,
                    GroupByDefaultEnumerable<TGenGroupByInItem, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable, TGenGroupByEnumerator>,
                    GroupByDefaultEnumerator<TGenGroupByInItem, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerator>,
                    GroupBySpecificEnumerable<TGenGroupByInItem2, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable2, TGenGroupByEnumerator2>,
                    GroupBySpecificEnumerator<TGenGroupByInItem2, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerator2>
                >(
                    ref first,
                    ref second,
                    comparer
                );
        }

        public
            UnionSpecificEnumerable<
                GroupingEnumerable<TGenLookupKey, TGenLookupElement>,
                GroupByDefaultEnumerable<TGenGroupByInItem, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable, TGenGroupByEnumerator>,
                GroupByDefaultEnumerator<TGenGroupByInItem, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerator>,
                LookupDefaultEnumerable<TGenLookupKey, TGenLookupElement>,
                LookupDefaultEnumerator<TGenLookupKey, TGenLookupElement>
            > Union<TGenGroupByInItem, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable, TGenGroupByEnumerator>(
                GroupByDefaultEnumerable<TGenGroupByInItem, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable, TGenGroupByEnumerator> first,
                LookupDefaultEnumerable<TGenLookupKey, TGenLookupElement> second,
                IEqualityComparer<GroupingEnumerable<TGenLookupKey, TGenLookupElement>> comparer
            )
            where TGenGroupByEnumerable : struct, IStructEnumerable<TGenGroupByInItem, TGenGroupByEnumerator>
            where TGenGroupByEnumerator : struct, IStructEnumerator<TGenGroupByInItem>
        {
            return
                CommonImplementation.Union<
                    GroupingEnumerable<TGenLookupKey, TGenLookupElement>,
                    GroupByDefaultEnumerable<TGenGroupByInItem, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable, TGenGroupByEnumerator>,
                    GroupByDefaultEnumerator<TGenGroupByInItem, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerator>,
                    LookupDefaultEnumerable<TGenLookupKey, TGenLookupElement>,
                    LookupDefaultEnumerator<TGenLookupKey, TGenLookupElement>
                >(
                    ref first,
                    ref second,
                    comparer
                );
        }

        public
            UnionSpecificEnumerable<
                GroupingEnumerable<TGenLookupKey, TGenLookupElement>,
                GroupByDefaultEnumerable<TGenGroupByInItem, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable, TGenGroupByEnumerator>,
                GroupByDefaultEnumerator<TGenGroupByInItem, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerator>,
                LookupSpecificEnumerable<TGenLookupKey, TGenLookupElement>,
                LookupSpecificEnumerator<TGenLookupKey, TGenLookupElement>
            > Union<TGenGroupByInItem, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable, TGenGroupByEnumerator>(
                GroupByDefaultEnumerable<TGenGroupByInItem, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable, TGenGroupByEnumerator> first,
                LookupSpecificEnumerable<TGenLookupKey, TGenLookupElement> second,
                IEqualityComparer<GroupingEnumerable<TGenLookupKey, TGenLookupElement>> comparer
            )
            where TGenGroupByEnumerable : struct, IStructEnumerable<TGenGroupByInItem, TGenGroupByEnumerator>
            where TGenGroupByEnumerator : struct, IStructEnumerator<TGenGroupByInItem>
        {
            return
                CommonImplementation.Union<
                    GroupingEnumerable<TGenLookupKey, TGenLookupElement>,
                    GroupByDefaultEnumerable<TGenGroupByInItem, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable, TGenGroupByEnumerator>,
                    GroupByDefaultEnumerator<TGenGroupByInItem, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerator>,
                    LookupSpecificEnumerable<TGenLookupKey, TGenLookupElement>,
                    LookupSpecificEnumerator<TGenLookupKey, TGenLookupElement>
                >(
                    ref first,
                    ref second,
                    comparer
                );
        }

        // Union, default - GroupBySpecific

        public
            UnionDefaultEnumerable<
                GroupingEnumerable<TGenLookupKey, TGenLookupElement>,
                GroupBySpecificEnumerable<TGenGroupByInItem, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable, TGenGroupByEnumerator>,
                GroupBySpecificEnumerator<TGenGroupByInItem, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerator>,
                GroupByDefaultEnumerable<TGenGroupByInItem2, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable2, TGenGroupByEnumerator2>,
                GroupByDefaultEnumerator<TGenGroupByInItem2, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerator2>
            > Union<TGenGroupByInItem, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable, TGenGroupByEnumerator, TGenGroupByInItem2, TGenGroupByEnumerable2, TGenGroupByEnumerator2>(
            GroupBySpecificEnumerable<TGenGroupByInItem, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable, TGenGroupByEnumerator> first,
            GroupByDefaultEnumerable<TGenGroupByInItem2, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable2, TGenGroupByEnumerator2> second
        )
            where TGenGroupByEnumerable : struct, IStructEnumerable<TGenGroupByInItem, TGenGroupByEnumerator>
            where TGenGroupByEnumerator : struct, IStructEnumerator<TGenGroupByInItem>
            where TGenGroupByEnumerable2 : struct, IStructEnumerable<TGenGroupByInItem2, TGenGroupByEnumerator2>
            where TGenGroupByEnumerator2 : struct, IStructEnumerator<TGenGroupByInItem2>
        {
            return
                CommonImplementation.Union<
                    GroupingEnumerable<TGenLookupKey, TGenLookupElement>,
                    GroupBySpecificEnumerable<TGenGroupByInItem, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable, TGenGroupByEnumerator>,
                    GroupBySpecificEnumerator<TGenGroupByInItem, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerator>,
                    GroupByDefaultEnumerable<TGenGroupByInItem2, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable2, TGenGroupByEnumerator2>,
                    GroupByDefaultEnumerator<TGenGroupByInItem2, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerator2>
                >(
                    ref first,
                    ref second
                );
        }

        public
            UnionDefaultEnumerable<
                GroupingEnumerable<TGenLookupKey, TGenLookupElement>,
                GroupBySpecificEnumerable<TGenGroupByInItem, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable, TGenGroupByEnumerator>,
                GroupBySpecificEnumerator<TGenGroupByInItem, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerator>,
                GroupBySpecificEnumerable<TGenGroupByInItem2, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable2, TGenGroupByEnumerator2>,
                GroupBySpecificEnumerator<TGenGroupByInItem2, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerator2>
            > Union<TGenGroupByInItem, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable, TGenGroupByEnumerator, TGenGroupByInItem2, TGenGroupByEnumerable2, TGenGroupByEnumerator2>(
                GroupBySpecificEnumerable<TGenGroupByInItem, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable, TGenGroupByEnumerator> first,
                GroupBySpecificEnumerable<TGenGroupByInItem2, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable2, TGenGroupByEnumerator2> second
            )
                where TGenGroupByEnumerable : struct, IStructEnumerable<TGenGroupByInItem, TGenGroupByEnumerator>
                where TGenGroupByEnumerator : struct, IStructEnumerator<TGenGroupByInItem>
                where TGenGroupByEnumerable2 : struct, IStructEnumerable<TGenGroupByInItem2, TGenGroupByEnumerator2>
                where TGenGroupByEnumerator2 : struct, IStructEnumerator<TGenGroupByInItem2>
        {
            return
                CommonImplementation.Union<
                    GroupingEnumerable<TGenLookupKey, TGenLookupElement>,
                    GroupBySpecificEnumerable<TGenGroupByInItem, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable, TGenGroupByEnumerator>,
                    GroupBySpecificEnumerator<TGenGroupByInItem, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerator>,
                    GroupBySpecificEnumerable<TGenGroupByInItem2, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable2, TGenGroupByEnumerator2>,
                    GroupBySpecificEnumerator<TGenGroupByInItem2, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerator2>
                >(
                    ref first,
                    ref second
                );
        }

        public
            UnionDefaultEnumerable<
                GroupingEnumerable<TGenLookupKey, TGenLookupElement>,
                GroupBySpecificEnumerable<TGenGroupByInItem, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable, TGenGroupByEnumerator>,
                GroupBySpecificEnumerator<TGenGroupByInItem, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerator>,
                LookupDefaultEnumerable<TGenLookupKey, TGenLookupElement>,
                LookupDefaultEnumerator<TGenLookupKey, TGenLookupElement>
            > Union<TGenGroupByInItem, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable, TGenGroupByEnumerator>(
                GroupBySpecificEnumerable<TGenGroupByInItem, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable, TGenGroupByEnumerator> first,
                LookupDefaultEnumerable<TGenLookupKey, TGenLookupElement> second
            )
            where TGenGroupByEnumerable : struct, IStructEnumerable<TGenGroupByInItem, TGenGroupByEnumerator>
            where TGenGroupByEnumerator : struct, IStructEnumerator<TGenGroupByInItem>
        {
            return
                CommonImplementation.Union<
                    GroupingEnumerable<TGenLookupKey, TGenLookupElement>,
                    GroupBySpecificEnumerable<TGenGroupByInItem, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable, TGenGroupByEnumerator>,
                    GroupBySpecificEnumerator<TGenGroupByInItem, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerator>,
                    LookupDefaultEnumerable<TGenLookupKey, TGenLookupElement>,
                    LookupDefaultEnumerator<TGenLookupKey, TGenLookupElement>
                >(
                    ref first,
                    ref second
                );
        }

        public
            UnionDefaultEnumerable<
                GroupingEnumerable<TGenLookupKey, TGenLookupElement>,
                GroupBySpecificEnumerable<TGenGroupByInItem, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable, TGenGroupByEnumerator>,
                GroupBySpecificEnumerator<TGenGroupByInItem, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerator>,
                LookupSpecificEnumerable<TGenLookupKey, TGenLookupElement>,
                LookupSpecificEnumerator<TGenLookupKey, TGenLookupElement>
            > Union<TGenGroupByInItem, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable, TGenGroupByEnumerator>(
                GroupBySpecificEnumerable<TGenGroupByInItem, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable, TGenGroupByEnumerator> first,
                LookupSpecificEnumerable<TGenLookupKey, TGenLookupElement> second
            )
            where TGenGroupByEnumerable : struct, IStructEnumerable<TGenGroupByInItem, TGenGroupByEnumerator>
            where TGenGroupByEnumerator : struct, IStructEnumerator<TGenGroupByInItem>
        {
            return
                CommonImplementation.Union<
                    GroupingEnumerable<TGenLookupKey, TGenLookupElement>,
                    GroupBySpecificEnumerable<TGenGroupByInItem, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable, TGenGroupByEnumerator>,
                    GroupBySpecificEnumerator<TGenGroupByInItem, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerator>,
                    LookupSpecificEnumerable<TGenLookupKey, TGenLookupElement>,
                    LookupSpecificEnumerator<TGenLookupKey, TGenLookupElement>
                >(
                    ref first,
                    ref second
                );
        }

        // Union, specific - GroupBySpecific

        public
            UnionSpecificEnumerable<
                GroupingEnumerable<TGenLookupKey, TGenLookupElement>,
                GroupBySpecificEnumerable<TGenGroupByInItem, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable, TGenGroupByEnumerator>,
                GroupBySpecificEnumerator<TGenGroupByInItem, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerator>,
                GroupByDefaultEnumerable<TGenGroupByInItem2, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable2, TGenGroupByEnumerator2>,
                GroupByDefaultEnumerator<TGenGroupByInItem2, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerator2>
            > Union<TGenGroupByInItem, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable, TGenGroupByEnumerator, TGenGroupByInItem2, TGenGroupByEnumerable2, TGenGroupByEnumerator2>(
            GroupBySpecificEnumerable<TGenGroupByInItem, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable, TGenGroupByEnumerator> first,
            GroupByDefaultEnumerable<TGenGroupByInItem2, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable2, TGenGroupByEnumerator2> second,
            IEqualityComparer<GroupingEnumerable<TGenLookupKey, TGenLookupElement>> comparer
        )
            where TGenGroupByEnumerable : struct, IStructEnumerable<TGenGroupByInItem, TGenGroupByEnumerator>
            where TGenGroupByEnumerator : struct, IStructEnumerator<TGenGroupByInItem>
            where TGenGroupByEnumerable2 : struct, IStructEnumerable<TGenGroupByInItem2, TGenGroupByEnumerator2>
            where TGenGroupByEnumerator2 : struct, IStructEnumerator<TGenGroupByInItem2>
        {
            return
                CommonImplementation.Union<
                    GroupingEnumerable<TGenLookupKey, TGenLookupElement>,
                    GroupBySpecificEnumerable<TGenGroupByInItem, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable, TGenGroupByEnumerator>,
                    GroupBySpecificEnumerator<TGenGroupByInItem, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerator>,
                    GroupByDefaultEnumerable<TGenGroupByInItem2, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable2, TGenGroupByEnumerator2>,
                    GroupByDefaultEnumerator<TGenGroupByInItem2, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerator2>
                >(
                    ref first,
                    ref second,
                    comparer
                );
        }

        public
            UnionSpecificEnumerable<
                GroupingEnumerable<TGenLookupKey, TGenLookupElement>,
                GroupBySpecificEnumerable<TGenGroupByInItem, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable, TGenGroupByEnumerator>,
                GroupBySpecificEnumerator<TGenGroupByInItem, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerator>,
                GroupBySpecificEnumerable<TGenGroupByInItem2, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable2, TGenGroupByEnumerator2>,
                GroupBySpecificEnumerator<TGenGroupByInItem2, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerator2>
            > Union<TGenGroupByInItem, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable, TGenGroupByEnumerator, TGenGroupByInItem2, TGenGroupByEnumerable2, TGenGroupByEnumerator2>(
                GroupBySpecificEnumerable<TGenGroupByInItem, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable, TGenGroupByEnumerator> first,
                GroupBySpecificEnumerable<TGenGroupByInItem2, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable2, TGenGroupByEnumerator2> second,
                IEqualityComparer<GroupingEnumerable<TGenLookupKey, TGenLookupElement>> comparer
            )
                where TGenGroupByEnumerable : struct, IStructEnumerable<TGenGroupByInItem, TGenGroupByEnumerator>
                where TGenGroupByEnumerator : struct, IStructEnumerator<TGenGroupByInItem>
                where TGenGroupByEnumerable2 : struct, IStructEnumerable<TGenGroupByInItem2, TGenGroupByEnumerator2>
                where TGenGroupByEnumerator2 : struct, IStructEnumerator<TGenGroupByInItem2>
        {
            return
                CommonImplementation.Union<
                    GroupingEnumerable<TGenLookupKey, TGenLookupElement>,
                    GroupBySpecificEnumerable<TGenGroupByInItem, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable, TGenGroupByEnumerator>,
                    GroupBySpecificEnumerator<TGenGroupByInItem, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerator>,
                    GroupBySpecificEnumerable<TGenGroupByInItem2, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable2, TGenGroupByEnumerator2>,
                    GroupBySpecificEnumerator<TGenGroupByInItem2, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerator2>
                >(
                    ref first,
                    ref second,
                    comparer
                );
        }

        public
            UnionSpecificEnumerable<
                GroupingEnumerable<TGenLookupKey, TGenLookupElement>,
                GroupBySpecificEnumerable<TGenGroupByInItem, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable, TGenGroupByEnumerator>,
                GroupBySpecificEnumerator<TGenGroupByInItem, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerator>,
                LookupDefaultEnumerable<TGenLookupKey, TGenLookupElement>,
                LookupDefaultEnumerator<TGenLookupKey, TGenLookupElement>
            > Union<TGenGroupByInItem, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable, TGenGroupByEnumerator>(
                GroupBySpecificEnumerable<TGenGroupByInItem, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable, TGenGroupByEnumerator> first,
                LookupDefaultEnumerable<TGenLookupKey, TGenLookupElement> second,
                IEqualityComparer<GroupingEnumerable<TGenLookupKey, TGenLookupElement>> comparer
            )
            where TGenGroupByEnumerable : struct, IStructEnumerable<TGenGroupByInItem, TGenGroupByEnumerator>
            where TGenGroupByEnumerator : struct, IStructEnumerator<TGenGroupByInItem>
        {
            return
                CommonImplementation.Union<
                    GroupingEnumerable<TGenLookupKey, TGenLookupElement>,
                    GroupBySpecificEnumerable<TGenGroupByInItem, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable, TGenGroupByEnumerator>,
                    GroupBySpecificEnumerator<TGenGroupByInItem, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerator>,
                    LookupDefaultEnumerable<TGenLookupKey, TGenLookupElement>,
                    LookupDefaultEnumerator<TGenLookupKey, TGenLookupElement>
                >(
                    ref first,
                    ref second,
                    comparer
                );
        }

        public
            UnionSpecificEnumerable<
                GroupingEnumerable<TGenLookupKey, TGenLookupElement>,
                GroupBySpecificEnumerable<TGenGroupByInItem, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable, TGenGroupByEnumerator>,
                GroupBySpecificEnumerator<TGenGroupByInItem, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerator>,
                LookupSpecificEnumerable<TGenLookupKey, TGenLookupElement>,
                LookupSpecificEnumerator<TGenLookupKey, TGenLookupElement>
            > Union<TGenGroupByInItem, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable, TGenGroupByEnumerator>(
                GroupBySpecificEnumerable<TGenGroupByInItem, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable, TGenGroupByEnumerator> first,
                LookupSpecificEnumerable<TGenLookupKey, TGenLookupElement> second,
                IEqualityComparer<GroupingEnumerable<TGenLookupKey, TGenLookupElement>> comparer
            )
            where TGenGroupByEnumerable : struct, IStructEnumerable<TGenGroupByInItem, TGenGroupByEnumerator>
            where TGenGroupByEnumerator : struct, IStructEnumerator<TGenGroupByInItem>
        {
            return
                CommonImplementation.Union<
                    GroupingEnumerable<TGenLookupKey, TGenLookupElement>,
                    GroupBySpecificEnumerable<TGenGroupByInItem, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable, TGenGroupByEnumerator>,
                    GroupBySpecificEnumerator<TGenGroupByInItem, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerator>,
                    LookupSpecificEnumerable<TGenLookupKey, TGenLookupElement>,
                    LookupSpecificEnumerator<TGenLookupKey, TGenLookupElement>
                >(
                    ref first,
                    ref second,
                    comparer
                );
        }

        // Union, default - Lookup

        public
            UnionDefaultEnumerable<
                GroupingEnumerable<TGenLookupKey, TGenLookupElement>,
                LookupDefaultEnumerable<TGenLookupKey, TGenLookupElement>,
                LookupDefaultEnumerator<TGenLookupKey, TGenLookupElement>,
                GroupByDefaultEnumerable<TGenGroupByInItem2, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable2, TGenGroupByEnumerator2>,
                GroupByDefaultEnumerator<TGenGroupByInItem2, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerator2>
            > Union<TGenLookupKey, TGenLookupElement, TGenGroupByInItem2, TGenGroupByEnumerable2, TGenGroupByEnumerator2>(
            LookupDefaultEnumerable<TGenLookupKey, TGenLookupElement> first,
            GroupByDefaultEnumerable<TGenGroupByInItem2, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable2, TGenGroupByEnumerator2> second
        )
            where TGenGroupByEnumerable2 : struct, IStructEnumerable<TGenGroupByInItem2, TGenGroupByEnumerator2>
            where TGenGroupByEnumerator2 : struct, IStructEnumerator<TGenGroupByInItem2>
        {
            return
                CommonImplementation.Union<
                    GroupingEnumerable<TGenLookupKey, TGenLookupElement>,
                    LookupDefaultEnumerable<TGenLookupKey, TGenLookupElement>,
                    LookupDefaultEnumerator<TGenLookupKey, TGenLookupElement>,
                    GroupByDefaultEnumerable<TGenGroupByInItem2, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable2, TGenGroupByEnumerator2>,
                    GroupByDefaultEnumerator<TGenGroupByInItem2, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerator2>
                >(
                    ref first,
                    ref second
                );
        }

        public
            UnionDefaultEnumerable<
                GroupingEnumerable<TGenLookupKey, TGenLookupElement>,
                LookupSpecificEnumerable<TGenLookupKey, TGenLookupElement>,
                LookupSpecificEnumerator<TGenLookupKey, TGenLookupElement>,
                GroupByDefaultEnumerable<TGenGroupByInItem2, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable2, TGenGroupByEnumerator2>,
                GroupByDefaultEnumerator<TGenGroupByInItem2, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerator2>
            > Union<TGenLookupKey, TGenLookupElement, TGenGroupByInItem2, TGenGroupByEnumerable2, TGenGroupByEnumerator2>(
            LookupSpecificEnumerable<TGenLookupKey, TGenLookupElement> first,
            GroupByDefaultEnumerable<TGenGroupByInItem2, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable2, TGenGroupByEnumerator2> second
        )
            where TGenGroupByEnumerable2 : struct, IStructEnumerable<TGenGroupByInItem2, TGenGroupByEnumerator2>
            where TGenGroupByEnumerator2 : struct, IStructEnumerator<TGenGroupByInItem2>
        {
            return
                CommonImplementation.Union<
                    GroupingEnumerable<TGenLookupKey, TGenLookupElement>,
                    LookupSpecificEnumerable<TGenLookupKey, TGenLookupElement>,
                    LookupSpecificEnumerator<TGenLookupKey, TGenLookupElement>,
                    GroupByDefaultEnumerable<TGenGroupByInItem2, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable2, TGenGroupByEnumerator2>,
                    GroupByDefaultEnumerator<TGenGroupByInItem2, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerator2>
                >(
                    ref first,
                    ref second
                );
        }
        
        public
            UnionDefaultEnumerable<
                GroupingEnumerable<TGenLookupKey, TGenLookupElement>,
                LookupDefaultEnumerable<TGenLookupKey, TGenLookupElement>,
                LookupDefaultEnumerator<TGenLookupKey, TGenLookupElement>,
                GroupBySpecificEnumerable<TGenGroupByInItem2, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable2, TGenGroupByEnumerator2>,
                GroupBySpecificEnumerator<TGenGroupByInItem2, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerator2>
            > Union<TGenLookupKey, TGenLookupElement, TGenGroupByInItem2, TGenGroupByEnumerable2, TGenGroupByEnumerator2>(
                LookupDefaultEnumerable<TGenLookupKey, TGenLookupElement> first,
                GroupBySpecificEnumerable<TGenGroupByInItem2, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable2, TGenGroupByEnumerator2> second
            )
                where TGenGroupByEnumerable2 : struct, IStructEnumerable<TGenGroupByInItem2, TGenGroupByEnumerator2>
                where TGenGroupByEnumerator2 : struct, IStructEnumerator<TGenGroupByInItem2>
        {
            return
                CommonImplementation.Union<
                    GroupingEnumerable<TGenLookupKey, TGenLookupElement>,
                    LookupDefaultEnumerable<TGenLookupKey, TGenLookupElement>,
                    LookupDefaultEnumerator<TGenLookupKey, TGenLookupElement>,
                    GroupBySpecificEnumerable<TGenGroupByInItem2, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable2, TGenGroupByEnumerator2>,
                    GroupBySpecificEnumerator<TGenGroupByInItem2, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerator2>
                >(
                    ref first,
                    ref second
                );
        }

        public
            UnionDefaultEnumerable<
                GroupingEnumerable<TGenLookupKey, TGenLookupElement>,
                LookupSpecificEnumerable<TGenLookupKey, TGenLookupElement>,
                LookupSpecificEnumerator<TGenLookupKey, TGenLookupElement>,
                GroupBySpecificEnumerable<TGenGroupByInItem2, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable2, TGenGroupByEnumerator2>,
                GroupBySpecificEnumerator<TGenGroupByInItem2, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerator2>
            > Union<TGenLookupKey, TGenLookupElement, TGenGroupByInItem2, TGenGroupByEnumerable2, TGenGroupByEnumerator2>(
                LookupSpecificEnumerable<TGenLookupKey, TGenLookupElement> first,
                GroupBySpecificEnumerable<TGenGroupByInItem2, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable2, TGenGroupByEnumerator2> second
            )
                where TGenGroupByEnumerable2 : struct, IStructEnumerable<TGenGroupByInItem2, TGenGroupByEnumerator2>
                where TGenGroupByEnumerator2 : struct, IStructEnumerator<TGenGroupByInItem2>
        {
            return
                CommonImplementation.Union<
                    GroupingEnumerable<TGenLookupKey, TGenLookupElement>,
                    LookupSpecificEnumerable<TGenLookupKey, TGenLookupElement>,
                    LookupSpecificEnumerator<TGenLookupKey, TGenLookupElement>,
                    GroupBySpecificEnumerable<TGenGroupByInItem2, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable2, TGenGroupByEnumerator2>,
                    GroupBySpecificEnumerator<TGenGroupByInItem2, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerator2>
                >(
                    ref first,
                    ref second
                );
        }
        
        public
            UnionDefaultEnumerable<
                GroupingEnumerable<TGenLookupKey, TGenLookupElement>,
                LookupDefaultEnumerable<TGenLookupKey, TGenLookupElement>,
                LookupDefaultEnumerator<TGenLookupKey, TGenLookupElement>,
                LookupDefaultEnumerable<TGenLookupKey, TGenLookupElement>,
                LookupDefaultEnumerator<TGenLookupKey, TGenLookupElement>
            > Union<TGenLookupKey, TGenLookupElement>(
                LookupDefaultEnumerable<TGenLookupKey, TGenLookupElement> first,
                LookupDefaultEnumerable<TGenLookupKey, TGenLookupElement> second
            )
        {
            return
                CommonImplementation.Union<
                    GroupingEnumerable<TGenLookupKey, TGenLookupElement>,
                    LookupDefaultEnumerable<TGenLookupKey, TGenLookupElement>,
                    LookupDefaultEnumerator<TGenLookupKey, TGenLookupElement>,
                    LookupDefaultEnumerable<TGenLookupKey, TGenLookupElement>,
                    LookupDefaultEnumerator<TGenLookupKey, TGenLookupElement>
                >(
                    ref first,
                    ref second
                );
        }

        public
            UnionDefaultEnumerable<
                GroupingEnumerable<TGenLookupKey, TGenLookupElement>,
                LookupDefaultEnumerable<TGenLookupKey, TGenLookupElement>,
                LookupDefaultEnumerator<TGenLookupKey, TGenLookupElement>,
                LookupSpecificEnumerable<TGenLookupKey, TGenLookupElement>,
                LookupSpecificEnumerator<TGenLookupKey, TGenLookupElement>
            > Union<TGenLookupKey, TGenLookupElement>(
                LookupDefaultEnumerable<TGenLookupKey, TGenLookupElement> first,
                LookupSpecificEnumerable<TGenLookupKey, TGenLookupElement> second
            )
        {
            return
                CommonImplementation.Union<
                    GroupingEnumerable<TGenLookupKey, TGenLookupElement>,
                    LookupDefaultEnumerable<TGenLookupKey, TGenLookupElement>,
                    LookupDefaultEnumerator<TGenLookupKey, TGenLookupElement>,
                    LookupSpecificEnumerable<TGenLookupKey, TGenLookupElement>,
                    LookupSpecificEnumerator<TGenLookupKey, TGenLookupElement>
                >(
                    ref first,
                    ref second
                );
        }
        
        public
            UnionDefaultEnumerable<
                GroupingEnumerable<TGenLookupKey, TGenLookupElement>,
                LookupSpecificEnumerable<TGenLookupKey, TGenLookupElement>,
                LookupSpecificEnumerator<TGenLookupKey, TGenLookupElement>,
                LookupDefaultEnumerable<TGenLookupKey, TGenLookupElement>,
                LookupDefaultEnumerator<TGenLookupKey, TGenLookupElement>
            > Union<TGenLookupKey, TGenLookupElement>(
                LookupSpecificEnumerable<TGenLookupKey, TGenLookupElement> first,
                LookupDefaultEnumerable<TGenLookupKey, TGenLookupElement> second
            )
        {
            return
                CommonImplementation.Union<
                    GroupingEnumerable<TGenLookupKey, TGenLookupElement>,
                    LookupSpecificEnumerable<TGenLookupKey, TGenLookupElement>,
                    LookupSpecificEnumerator<TGenLookupKey, TGenLookupElement>,
                    LookupDefaultEnumerable<TGenLookupKey, TGenLookupElement>,
                    LookupDefaultEnumerator<TGenLookupKey, TGenLookupElement>
                >(
                    ref first,
                    ref second
                );
        }

        public
            UnionDefaultEnumerable<
                GroupingEnumerable<TGenLookupKey, TGenLookupElement>,
                LookupSpecificEnumerable<TGenLookupKey, TGenLookupElement>,
                LookupSpecificEnumerator<TGenLookupKey, TGenLookupElement>,
                LookupSpecificEnumerable<TGenLookupKey, TGenLookupElement>,
                LookupSpecificEnumerator<TGenLookupKey, TGenLookupElement>
            > Union<TGenLookupKey, TGenLookupElement>(
                LookupSpecificEnumerable<TGenLookupKey, TGenLookupElement> first,
                LookupSpecificEnumerable<TGenLookupKey, TGenLookupElement> second
            )
        {
            return
                CommonImplementation.Union<
                    GroupingEnumerable<TGenLookupKey, TGenLookupElement>,
                    LookupSpecificEnumerable<TGenLookupKey, TGenLookupElement>,
                    LookupSpecificEnumerator<TGenLookupKey, TGenLookupElement>,
                    LookupSpecificEnumerable<TGenLookupKey, TGenLookupElement>,
                    LookupSpecificEnumerator<TGenLookupKey, TGenLookupElement>
                >(
                    ref first,
                    ref second
                );
        }

        // Union, specific - Lookup

        public
            UnionSpecificEnumerable<
                GroupingEnumerable<TGenLookupKey, TGenLookupElement>,
                LookupDefaultEnumerable<TGenLookupKey, TGenLookupElement>,
                LookupDefaultEnumerator<TGenLookupKey, TGenLookupElement>,
                GroupByDefaultEnumerable<TGenGroupByInItem2, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable2, TGenGroupByEnumerator2>,
                GroupByDefaultEnumerator<TGenGroupByInItem2, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerator2>
            > Union<TGenLookupKey, TGenLookupElement, TGenGroupByInItem2, TGenGroupByEnumerable2, TGenGroupByEnumerator2>(
            LookupDefaultEnumerable<TGenLookupKey, TGenLookupElement> first,
            GroupByDefaultEnumerable<TGenGroupByInItem2, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable2, TGenGroupByEnumerator2> second,
            IEqualityComparer<GroupingEnumerable<TGenLookupKey, TGenLookupElement>> comparer
        )
            where TGenGroupByEnumerable2 : struct, IStructEnumerable<TGenGroupByInItem2, TGenGroupByEnumerator2>
            where TGenGroupByEnumerator2 : struct, IStructEnumerator<TGenGroupByInItem2>
        {
            return
                CommonImplementation.Union<
                    GroupingEnumerable<TGenLookupKey, TGenLookupElement>,
                    LookupDefaultEnumerable<TGenLookupKey, TGenLookupElement>,
                    LookupDefaultEnumerator<TGenLookupKey, TGenLookupElement>,
                    GroupByDefaultEnumerable<TGenGroupByInItem2, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable2, TGenGroupByEnumerator2>,
                    GroupByDefaultEnumerator<TGenGroupByInItem2, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerator2>
                >(
                    ref first,
                    ref second,
                    comparer
                );
        }

        public
            UnionSpecificEnumerable<
                GroupingEnumerable<TGenLookupKey, TGenLookupElement>,
                LookupSpecificEnumerable<TGenLookupKey, TGenLookupElement>,
                LookupSpecificEnumerator<TGenLookupKey, TGenLookupElement>,
                GroupByDefaultEnumerable<TGenGroupByInItem2, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable2, TGenGroupByEnumerator2>,
                GroupByDefaultEnumerator<TGenGroupByInItem2, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerator2>
            > Union<TGenLookupKey, TGenLookupElement, TGenGroupByInItem2, TGenGroupByEnumerable2, TGenGroupByEnumerator2>(
            LookupSpecificEnumerable<TGenLookupKey, TGenLookupElement> first,
            GroupByDefaultEnumerable<TGenGroupByInItem2, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable2, TGenGroupByEnumerator2> second,
            IEqualityComparer<GroupingEnumerable<TGenLookupKey, TGenLookupElement>> comparer
        )
            where TGenGroupByEnumerable2 : struct, IStructEnumerable<TGenGroupByInItem2, TGenGroupByEnumerator2>
            where TGenGroupByEnumerator2 : struct, IStructEnumerator<TGenGroupByInItem2>
        {
            return
                CommonImplementation.Union<
                    GroupingEnumerable<TGenLookupKey, TGenLookupElement>,
                    LookupSpecificEnumerable<TGenLookupKey, TGenLookupElement>,
                    LookupSpecificEnumerator<TGenLookupKey, TGenLookupElement>,
                    GroupByDefaultEnumerable<TGenGroupByInItem2, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable2, TGenGroupByEnumerator2>,
                    GroupByDefaultEnumerator<TGenGroupByInItem2, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerator2>
                >(
                    ref first,
                    ref second,
                    comparer
                );
        }
        
        public
            UnionSpecificEnumerable<
                GroupingEnumerable<TGenLookupKey, TGenLookupElement>,
                LookupDefaultEnumerable<TGenLookupKey, TGenLookupElement>,
                LookupDefaultEnumerator<TGenLookupKey, TGenLookupElement>,
                GroupBySpecificEnumerable<TGenGroupByInItem2, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable2, TGenGroupByEnumerator2>,
                GroupBySpecificEnumerator<TGenGroupByInItem2, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerator2>
            > Union<TGenLookupKey, TGenLookupElement, TGenGroupByInItem2, TGenGroupByEnumerable2, TGenGroupByEnumerator2>(
                LookupDefaultEnumerable<TGenLookupKey, TGenLookupElement> first,
                GroupBySpecificEnumerable<TGenGroupByInItem2, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable2, TGenGroupByEnumerator2> second,
                IEqualityComparer<GroupingEnumerable<TGenLookupKey, TGenLookupElement>> comparer
            )
                where TGenGroupByEnumerable2 : struct, IStructEnumerable<TGenGroupByInItem2, TGenGroupByEnumerator2>
                where TGenGroupByEnumerator2 : struct, IStructEnumerator<TGenGroupByInItem2>
        {
            return
                CommonImplementation.Union<
                    GroupingEnumerable<TGenLookupKey, TGenLookupElement>,
                    LookupDefaultEnumerable<TGenLookupKey, TGenLookupElement>,
                    LookupDefaultEnumerator<TGenLookupKey, TGenLookupElement>,
                    GroupBySpecificEnumerable<TGenGroupByInItem2, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable2, TGenGroupByEnumerator2>,
                    GroupBySpecificEnumerator<TGenGroupByInItem2, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerator2>
                >(
                    ref first,
                    ref second,
                    comparer
                );
        }

        public
            UnionSpecificEnumerable<
                GroupingEnumerable<TGenLookupKey, TGenLookupElement>,
                LookupSpecificEnumerable<TGenLookupKey, TGenLookupElement>,
                LookupSpecificEnumerator<TGenLookupKey, TGenLookupElement>,
                GroupBySpecificEnumerable<TGenGroupByInItem2, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable2, TGenGroupByEnumerator2>,
                GroupBySpecificEnumerator<TGenGroupByInItem2, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerator2>
            > Union<TGenLookupKey, TGenLookupElement, TGenGroupByInItem2, TGenGroupByEnumerable2, TGenGroupByEnumerator2>(
                LookupSpecificEnumerable<TGenLookupKey, TGenLookupElement> first,
                GroupBySpecificEnumerable<TGenGroupByInItem2, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable2, TGenGroupByEnumerator2> second,
                IEqualityComparer<GroupingEnumerable<TGenLookupKey, TGenLookupElement>> comparer
            )
                where TGenGroupByEnumerable2 : struct, IStructEnumerable<TGenGroupByInItem2, TGenGroupByEnumerator2>
                where TGenGroupByEnumerator2 : struct, IStructEnumerator<TGenGroupByInItem2>
        {
            return
                CommonImplementation.Union<
                    GroupingEnumerable<TGenLookupKey, TGenLookupElement>,
                    LookupSpecificEnumerable<TGenLookupKey, TGenLookupElement>,
                    LookupSpecificEnumerator<TGenLookupKey, TGenLookupElement>,
                    GroupBySpecificEnumerable<TGenGroupByInItem2, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable2, TGenGroupByEnumerator2>,
                    GroupBySpecificEnumerator<TGenGroupByInItem2, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerator2>
                >(
                    ref first,
                    ref second,
                    comparer
                );
        }
        
        public
            UnionSpecificEnumerable<
                GroupingEnumerable<TGenLookupKey, TGenLookupElement>,
                LookupDefaultEnumerable<TGenLookupKey, TGenLookupElement>,
                LookupDefaultEnumerator<TGenLookupKey, TGenLookupElement>,
                LookupDefaultEnumerable<TGenLookupKey, TGenLookupElement>,
                LookupDefaultEnumerator<TGenLookupKey, TGenLookupElement>
            > Union<TGenLookupKey, TGenLookupElement>(
                LookupDefaultEnumerable<TGenLookupKey, TGenLookupElement> first,
                LookupDefaultEnumerable<TGenLookupKey, TGenLookupElement> second,
                IEqualityComparer<GroupingEnumerable<TGenLookupKey, TGenLookupElement>> comparer
            )
        {
            return
                CommonImplementation.Union<
                    GroupingEnumerable<TGenLookupKey, TGenLookupElement>,
                    LookupDefaultEnumerable<TGenLookupKey, TGenLookupElement>,
                    LookupDefaultEnumerator<TGenLookupKey, TGenLookupElement>,
                    LookupDefaultEnumerable<TGenLookupKey, TGenLookupElement>,
                    LookupDefaultEnumerator<TGenLookupKey, TGenLookupElement>
                >(
                    ref first,
                    ref second,
                    comparer
                );
        }

        public
            UnionSpecificEnumerable<
                GroupingEnumerable<TGenLookupKey, TGenLookupElement>,
                LookupDefaultEnumerable<TGenLookupKey, TGenLookupElement>,
                LookupDefaultEnumerator<TGenLookupKey, TGenLookupElement>,
                LookupSpecificEnumerable<TGenLookupKey, TGenLookupElement>,
                LookupSpecificEnumerator<TGenLookupKey, TGenLookupElement>
            > Union<TGenLookupKey, TGenLookupElement>(
                LookupDefaultEnumerable<TGenLookupKey, TGenLookupElement> first,
                LookupSpecificEnumerable<TGenLookupKey, TGenLookupElement> second,
                IEqualityComparer<GroupingEnumerable<TGenLookupKey, TGenLookupElement>> comparer
            )
        {
            return
                CommonImplementation.Union<
                    GroupingEnumerable<TGenLookupKey, TGenLookupElement>,
                    LookupDefaultEnumerable<TGenLookupKey, TGenLookupElement>,
                    LookupDefaultEnumerator<TGenLookupKey, TGenLookupElement>,
                    LookupSpecificEnumerable<TGenLookupKey, TGenLookupElement>,
                    LookupSpecificEnumerator<TGenLookupKey, TGenLookupElement>
                >(
                    ref first,
                    ref second,
                    comparer
                );
        }
        
        public
            UnionSpecificEnumerable<
                GroupingEnumerable<TGenLookupKey, TGenLookupElement>,
                LookupSpecificEnumerable<TGenLookupKey, TGenLookupElement>,
                LookupSpecificEnumerator<TGenLookupKey, TGenLookupElement>,
                LookupDefaultEnumerable<TGenLookupKey, TGenLookupElement>,
                LookupDefaultEnumerator<TGenLookupKey, TGenLookupElement>
            > Union<TGenLookupKey, TGenLookupElement>(
                LookupSpecificEnumerable<TGenLookupKey, TGenLookupElement> first,
                LookupDefaultEnumerable<TGenLookupKey, TGenLookupElement> second,
                IEqualityComparer<GroupingEnumerable<TGenLookupKey, TGenLookupElement>> comparer
            )
        {
            return
                CommonImplementation.Union<
                    GroupingEnumerable<TGenLookupKey, TGenLookupElement>,
                    LookupSpecificEnumerable<TGenLookupKey, TGenLookupElement>,
                    LookupSpecificEnumerator<TGenLookupKey, TGenLookupElement>,
                    LookupDefaultEnumerable<TGenLookupKey, TGenLookupElement>,
                    LookupDefaultEnumerator<TGenLookupKey, TGenLookupElement>
                >(
                    ref first,
                    ref second,
                    comparer
                );
        }

        public
            UnionSpecificEnumerable<
                GroupingEnumerable<TGenLookupKey, TGenLookupElement>,
                LookupSpecificEnumerable<TGenLookupKey, TGenLookupElement>,
                LookupSpecificEnumerator<TGenLookupKey, TGenLookupElement>,
                LookupSpecificEnumerable<TGenLookupKey, TGenLookupElement>,
                LookupSpecificEnumerator<TGenLookupKey, TGenLookupElement>
            > Union<TGenLookupKey, TGenLookupElement>(
                LookupSpecificEnumerable<TGenLookupKey, TGenLookupElement> first,
                LookupSpecificEnumerable<TGenLookupKey, TGenLookupElement> second,
                IEqualityComparer<GroupingEnumerable<TGenLookupKey, TGenLookupElement>> comparer
            )
        {
            return
                CommonImplementation.Union<
                    GroupingEnumerable<TGenLookupKey, TGenLookupElement>,
                    LookupSpecificEnumerable<TGenLookupKey, TGenLookupElement>,
                    LookupSpecificEnumerator<TGenLookupKey, TGenLookupElement>,
                    LookupSpecificEnumerable<TGenLookupKey, TGenLookupElement>,
                    LookupSpecificEnumerator<TGenLookupKey, TGenLookupElement>
                >(
                    ref first,
                    ref second,
                    comparer
                );
        }

        // RangeEnumerable

        public UnionDefaultEnumerable<int, PlaceholderEnumerable<int>, PlaceholderEnumerator<int>, RangeEnumerable, RangeEnumerator> Union(
            PlaceholderEnumerable<int> first,
            RangeEnumerable second
        )
        {
            return
                CommonImplementation.Union<
                    int,
                    PlaceholderEnumerable<int>,
                    PlaceholderEnumerator<int>,
                    RangeEnumerable,
                    RangeEnumerator
                >(
                    RefParam(first),
                    ref second
                );
        }

        public UnionSpecificEnumerable<int, PlaceholderEnumerable<int>, PlaceholderEnumerator<int>, RangeEnumerable, RangeEnumerator> Union(
            PlaceholderEnumerable<int> first,
            RangeEnumerable second,
            IEqualityComparer<int> comparer
        )
        {
            return
                CommonImplementation.Union<
                    int,
                    PlaceholderEnumerable<int>,
                    PlaceholderEnumerator<int>,
                    RangeEnumerable,
                    RangeEnumerator
                >(
                    RefParam(first),
                    ref second,
                    comparer
                );
        }

        //public UnionDefaultEnumerable<int, RangeEnumerable, RangeEnumerator, PlaceholderEnumerable<int>, PlaceholderEnumerator<int>> Union(
        //    RangeEnumerable first,
        //    PlaceholderEnumerable<int> second
        //)
        //{
        //    return
        //        CommonImplementation.Union<
        //            int,
        //            RangeEnumerable,
        //            RangeEnumerator,
        //            PlaceholderEnumerable<int>,
        //            PlaceholderEnumerator<int>
        //        >(
        //            ref first,
        //            RefParam(second)
        //        );
        //}

        //public UnionSpecificEnumerable<int, RangeEnumerable, RangeEnumerator, PlaceholderEnumerable<int>, PlaceholderEnumerator<int>> Union(
        //    RangeEnumerable first,
        //    PlaceholderEnumerable<int> second,
        //    IEqualityComparer<int> comparer
        //)
        //{
        //    return
        //        CommonImplementation.Union<
        //            int,
        //            RangeEnumerable,
        //            RangeEnumerator,
        //            PlaceholderEnumerable<int>,
        //            PlaceholderEnumerator<int>
        //        >(
        //            ref first,
        //            RefParam(second),
        //            comparer
        //        );
        //}

        public UnionDefaultEnumerable<int, RangeEnumerable, RangeEnumerator, RangeEnumerable, RangeEnumerator> Union(
            RangeEnumerable first,
            RangeEnumerable second
        )
        {
            return
                CommonImplementation.Union<
                    int,
                    RangeEnumerable,
                    RangeEnumerator,
                    RangeEnumerable,
                    RangeEnumerator
                >(
                    ref first,
                    ref second
                );
        }

        public UnionSpecificEnumerable<int, RangeEnumerable, RangeEnumerator, RangeEnumerable, RangeEnumerator> Union(
            RangeEnumerable first,
            RangeEnumerable second,
            IEqualityComparer<int> comparer
        )
        {
            return
                CommonImplementation.Union<
                    int,
                    RangeEnumerable,
                    RangeEnumerator,
                    RangeEnumerable,
                    RangeEnumerator
                >(
                    ref first,
                    ref second,
                    comparer
                );
        }

        // ReverseRangeEnumerable

        public UnionDefaultEnumerable<int, PlaceholderEnumerable<int>, PlaceholderEnumerator<int>, ReverseRangeEnumerable, ReverseRangeEnumerator> Union(
            PlaceholderEnumerable<int> first,
            ReverseRangeEnumerable second
        )
        {
            return
                CommonImplementation.Union<
                    int,
                    PlaceholderEnumerable<int>,
                    PlaceholderEnumerator<int>,
                    ReverseRangeEnumerable,
                    ReverseRangeEnumerator
                >(
                    RefParam(first),
                    ref second
                );
        }

        public UnionSpecificEnumerable<int, PlaceholderEnumerable<int>, PlaceholderEnumerator<int>, ReverseRangeEnumerable, ReverseRangeEnumerator> Union(
            PlaceholderEnumerable<int> first,
            ReverseRangeEnumerable second,
            IEqualityComparer<int> comparer
        )
        {
            return
                CommonImplementation.Union<
                    int,
                    PlaceholderEnumerable<int>,
                    PlaceholderEnumerator<int>,
                    ReverseRangeEnumerable,
                    ReverseRangeEnumerator
                >(
                    RefParam(first),
                    ref second,
                    comparer
                );
        }

        //public UnionDefaultEnumerable<int, ReverseRangeEnumerable, ReverseRangeEnumerator, PlaceholderEnumerable<int>, PlaceholderEnumerator<int>> Union(
        //    ReverseRangeEnumerable first,
        //    PlaceholderEnumerable<int> second
        //)
        //{
        //    return
        //        CommonImplementation.Union<
        //            int,
        //            ReverseRangeEnumerable,
        //            ReverseRangeEnumerator,
        //            PlaceholderEnumerable<int>,
        //            PlaceholderEnumerator<int>
        //        >(
        //            ref first,
        //            RefParam(second)
        //        );
        //}

        //public UnionSpecificEnumerable<int, ReverseRangeEnumerable, ReverseRangeEnumerator, PlaceholderEnumerable<int>, PlaceholderEnumerator<int>> Union(
        //    ReverseRangeEnumerable first,
        //    PlaceholderEnumerable<int> second,
        //    IEqualityComparer<int> comparer
        //)
        //{
        //    return
        //        CommonImplementation.Union<
        //            int,
        //            ReverseRangeEnumerable,
        //            ReverseRangeEnumerator,
        //            PlaceholderEnumerable<int>,
        //            PlaceholderEnumerator<int>
        //        >(
        //            ref first,
        //            RefParam(second),
        //            comparer
        //        );
        //}

        public UnionDefaultEnumerable<int, ReverseRangeEnumerable, ReverseRangeEnumerator, ReverseRangeEnumerable, ReverseRangeEnumerator> Union(
            ReverseRangeEnumerable first,
            ReverseRangeEnumerable second
        )
        {
            return
                CommonImplementation.Union<
                    int,
                    ReverseRangeEnumerable,
                    ReverseRangeEnumerator,
                    ReverseRangeEnumerable,
                    ReverseRangeEnumerator
                >(
                    ref first,
                    ref second
                );
        }

        public UnionSpecificEnumerable<int, ReverseRangeEnumerable, ReverseRangeEnumerator, ReverseRangeEnumerable, ReverseRangeEnumerator> Union(
            ReverseRangeEnumerable first,
            ReverseRangeEnumerable second,
            IEqualityComparer<int> comparer
        )
        {
            return
                CommonImplementation.Union<
                    int,
                    ReverseRangeEnumerable,
                    ReverseRangeEnumerator,
                    ReverseRangeEnumerable,
                    ReverseRangeEnumerator
                >(
                    ref first,
                    ref second,
                    comparer
                );
        }

        // Range - ReverseRange

        public UnionDefaultEnumerable<int, RangeEnumerable, RangeEnumerator, ReverseRangeEnumerable, ReverseRangeEnumerator> Union(
            RangeEnumerable first,
            ReverseRangeEnumerable second
        )
        {
            return
                CommonImplementation.Union<
                    int,
                    RangeEnumerable,
                    RangeEnumerator,
                    ReverseRangeEnumerable,
                    ReverseRangeEnumerator
                >(
                    ref first,
                    ref second
                );
        }

        public UnionSpecificEnumerable<int, RangeEnumerable, RangeEnumerator, ReverseRangeEnumerable, ReverseRangeEnumerator> Union(
            RangeEnumerable first,
            ReverseRangeEnumerable second,
            IEqualityComparer<int> comparer
        )
        {
            return
                CommonImplementation.Union<
                    int,
                    RangeEnumerable,
                    RangeEnumerator,
                    ReverseRangeEnumerable,
                    ReverseRangeEnumerator
                >(
                    ref first,
                    ref second,
                    comparer
                );
        }

        public UnionDefaultEnumerable<int, ReverseRangeEnumerable, ReverseRangeEnumerator, RangeEnumerable, RangeEnumerator> Union(
            ReverseRangeEnumerable first,
            RangeEnumerable second
        )
        {
            return
                CommonImplementation.Union<
                    int,
                    ReverseRangeEnumerable,
                    ReverseRangeEnumerator,
                    RangeEnumerable,
                    RangeEnumerator
                >(
                    ref first,
                    ref second
                );
        }

        public UnionSpecificEnumerable<int, ReverseRangeEnumerable, ReverseRangeEnumerator, RangeEnumerable, RangeEnumerator> Union(
            ReverseRangeEnumerable first,
            RangeEnumerable second,
            IEqualityComparer<int> comparer
        )
        {
            return
                CommonImplementation.Union<
                    int,
                    ReverseRangeEnumerable,
                    ReverseRangeEnumerator,
                    RangeEnumerable,
                    RangeEnumerator
                >(
                    ref first,
                    ref second,
                    comparer
                );
        }
    }
}