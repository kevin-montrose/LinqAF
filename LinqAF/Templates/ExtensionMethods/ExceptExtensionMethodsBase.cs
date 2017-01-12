using LinqAF.Impl;
using System.Collections.Generic;

namespace LinqAF
{
    abstract class ExceptExtensionMethodsBase :
        ExtensionMethodsBase
    {
        // Except, default
        public 
            ExceptDefaultEnumerable<
                GroupingEnumerable<TGenKey, TGenElement>,  
                PlaceholderEnumerable<GroupingEnumerable<TGenKey, TGenElement>>,
                PlaceholderEnumerator<GroupingEnumerable<TGenKey, TGenElement>>,
                GroupByDefaultEnumerable<TGenGroupByInItem, TGenKey, TGenElement, TGenGroupByEnumerable, TGenGroupByEnumerator>,
                GroupByDefaultEnumerator<TGenGroupByInItem, TGenKey, TGenElement, TGenGroupByEnumerator>
            > Except<TGenGroupByInItem, TGenKey, TGenElement, TGenGroupByEnumerable, TGenGroupByEnumerator>(
                PlaceholderEnumerable<GroupingEnumerable<TGenKey, TGenElement>> first,
                GroupByDefaultEnumerable<TGenGroupByInItem, TGenKey, TGenElement, TGenGroupByEnumerable, TGenGroupByEnumerator> second
            )
            where TGenGroupByEnumerable: struct, IStructEnumerable<TGenGroupByInItem, TGenGroupByEnumerator>
            where TGenGroupByEnumerator: struct, IStructEnumerator<TGenGroupByInItem>
        {
            return
                CommonImplementation.Except<
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
            ExceptDefaultEnumerable<
                GroupingEnumerable<TGenKey, TGenElement>,
                PlaceholderEnumerable<GroupingEnumerable<TGenKey, TGenElement>>,
                PlaceholderEnumerator<GroupingEnumerable<TGenKey, TGenElement>>,
                GroupBySpecificEnumerable<TGenGroupByInItem, TGenKey, TGenElement, TGenGroupByEnumerable, TGenGroupByEnumerator>,
                GroupBySpecificEnumerator<TGenGroupByInItem, TGenKey, TGenElement, TGenGroupByEnumerator>
            > Except<TGenGroupByInItem, TGenKey, TGenElement, TGenGroupByEnumerable, TGenGroupByEnumerator>(
                PlaceholderEnumerable<GroupingEnumerable<TGenKey, TGenElement>> first,
                GroupBySpecificEnumerable<TGenGroupByInItem, TGenKey, TGenElement, TGenGroupByEnumerable, TGenGroupByEnumerator> second
            )
            where TGenGroupByEnumerable : struct, IStructEnumerable<TGenGroupByInItem, TGenGroupByEnumerator>
            where TGenGroupByEnumerator : struct, IStructEnumerator<TGenGroupByInItem>
        {
            return
                CommonImplementation.Except<
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
            ExceptDefaultEnumerable<
                GroupingEnumerable<TGenLookupKey, TGenLookupElement>,
                PlaceholderEnumerable<GroupingEnumerable<TGenLookupKey, TGenLookupElement>>,
                PlaceholderEnumerator<GroupingEnumerable<TGenLookupKey, TGenLookupElement>>,
                LookupEnumerable<TGenLookupKey, TGenLookupElement>,
                LookupEnumerator<TGenLookupKey, TGenLookupElement>
            > Except<TGenLookupKey, TGenLookupElement>(
                PlaceholderEnumerable<GroupingEnumerable<TGenLookupKey, TGenLookupElement>> first,
                LookupEnumerable<TGenLookupKey, TGenLookupElement> second
            )
        {
            return
                CommonImplementation.Except<
                    GroupingEnumerable<TGenLookupKey, TGenLookupElement>,
                    PlaceholderEnumerable<GroupingEnumerable<TGenLookupKey, TGenLookupElement>>,
                    PlaceholderEnumerator<GroupingEnumerable<TGenLookupKey, TGenLookupElement>>,
                    LookupEnumerable<TGenLookupKey, TGenLookupElement>,
                    LookupEnumerator<TGenLookupKey, TGenLookupElement>
                >(
                    RefParam(first),
                    ref second
                );
        }

        // Except, specific
        public
            ExceptSpecificEnumerable<
                GroupingEnumerable<TGenKey, TGenElement>,
                PlaceholderEnumerable<GroupingEnumerable<TGenKey, TGenElement>>,
                PlaceholderEnumerator<GroupingEnumerable<TGenKey, TGenElement>>,
                GroupByDefaultEnumerable<TGenGroupByInItem, TGenKey, TGenElement, TGenGroupByEnumerable, TGenGroupByEnumerator>,
                GroupByDefaultEnumerator<TGenGroupByInItem, TGenKey, TGenElement, TGenGroupByEnumerator>
            > Except<TGenGroupByInItem, TGenKey, TGenElement, TGenGroupByEnumerable, TGenGroupByEnumerator>(
                PlaceholderEnumerable<GroupingEnumerable<TGenKey, TGenElement>> first,
                GroupByDefaultEnumerable<TGenGroupByInItem, TGenKey, TGenElement, TGenGroupByEnumerable, TGenGroupByEnumerator> second,
                IEqualityComparer<GroupingEnumerable<TGenKey, TGenElement>> comparer
            )
            where TGenGroupByEnumerable : struct, IStructEnumerable<TGenGroupByInItem, TGenGroupByEnumerator>
            where TGenGroupByEnumerator : struct, IStructEnumerator<TGenGroupByInItem>
        {
            return
                CommonImplementation.Except<
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
            ExceptSpecificEnumerable<
                GroupingEnumerable<TGenKey, TGenElement>,
                PlaceholderEnumerable<GroupingEnumerable<TGenKey, TGenElement>>,
                PlaceholderEnumerator<GroupingEnumerable<TGenKey, TGenElement>>,
                GroupBySpecificEnumerable<TGenGroupByInItem, TGenKey, TGenElement, TGenGroupByEnumerable, TGenGroupByEnumerator>,
                GroupBySpecificEnumerator<TGenGroupByInItem, TGenKey, TGenElement, TGenGroupByEnumerator>
            > Except<TGenGroupByInItem, TGenKey, TGenElement, TGenGroupByEnumerable, TGenGroupByEnumerator>(
                PlaceholderEnumerable<GroupingEnumerable<TGenKey, TGenElement>> first,
                GroupBySpecificEnumerable<TGenGroupByInItem, TGenKey, TGenElement, TGenGroupByEnumerable, TGenGroupByEnumerator> second,
                IEqualityComparer<GroupingEnumerable<TGenKey, TGenElement>> comparer
            )
            where TGenGroupByEnumerable : struct, IStructEnumerable<TGenGroupByInItem, TGenGroupByEnumerator>
            where TGenGroupByEnumerator : struct, IStructEnumerator<TGenGroupByInItem>
        {
            return
                CommonImplementation.Except<
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
            ExceptSpecificEnumerable<
                GroupingEnumerable<TGenLookupKey, TGenLookupElement>,
                PlaceholderEnumerable<GroupingEnumerable<TGenLookupKey, TGenLookupElement>>,
                PlaceholderEnumerator<GroupingEnumerable<TGenLookupKey, TGenLookupElement>>,
                LookupEnumerable<TGenLookupKey, TGenLookupElement>,
                LookupEnumerator<TGenLookupKey, TGenLookupElement>
            > Except<TGenLookupKey, TGenLookupElement>(
                PlaceholderEnumerable<GroupingEnumerable<TGenLookupKey, TGenLookupElement>> first,
                LookupEnumerable<TGenLookupKey, TGenLookupElement> second,
                IEqualityComparer<GroupingEnumerable<TGenLookupKey, TGenLookupElement>> comparer
            )
        {
            return
                CommonImplementation.Except<
                    GroupingEnumerable<TGenLookupKey, TGenLookupElement>,
                    PlaceholderEnumerable<GroupingEnumerable<TGenLookupKey, TGenLookupElement>>,
                    PlaceholderEnumerator<GroupingEnumerable<TGenLookupKey, TGenLookupElement>>,
                    LookupEnumerable<TGenLookupKey, TGenLookupElement>,
                    LookupEnumerator<TGenLookupKey, TGenLookupElement>
                >(
                    RefParam(first),
                    ref second,
                    comparer
                );
        }

        // Except, default - GroupByDefault

        public
            ExceptDefaultEnumerable<
                GroupingEnumerable<TGenLookupKey, TGenLookupElement>,
                GroupByDefaultEnumerable<TGenGroupByInItem, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable, TGenGroupByEnumerator>,
                GroupByDefaultEnumerator<TGenGroupByInItem, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerator>,
                GroupByDefaultEnumerable<TGenGroupByInItem2, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable2, TGenGroupByEnumerator2>,
                GroupByDefaultEnumerator<TGenGroupByInItem2, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerator2>
            > Except<TGenGroupByInItem, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable, TGenGroupByEnumerator, TGenGroupByInItem2, TGenGroupByEnumerable2, TGenGroupByEnumerator2>(
            GroupByDefaultEnumerable<TGenGroupByInItem, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable, TGenGroupByEnumerator> first,
            GroupByDefaultEnumerable<TGenGroupByInItem2, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable2, TGenGroupByEnumerator2> second
        )
            where TGenGroupByEnumerable : struct, IStructEnumerable<TGenGroupByInItem, TGenGroupByEnumerator>
            where TGenGroupByEnumerator : struct, IStructEnumerator<TGenGroupByInItem>
            where TGenGroupByEnumerable2 : struct, IStructEnumerable<TGenGroupByInItem2, TGenGroupByEnumerator2>
            where TGenGroupByEnumerator2 : struct, IStructEnumerator<TGenGroupByInItem2>
        {
            return
                CommonImplementation.Except<
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
            ExceptDefaultEnumerable<
                GroupingEnumerable<TGenLookupKey, TGenLookupElement>,
                GroupByDefaultEnumerable<TGenGroupByInItem, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable, TGenGroupByEnumerator>,
                GroupByDefaultEnumerator<TGenGroupByInItem, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerator>,
                GroupBySpecificEnumerable<TGenGroupByInItem2, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable2, TGenGroupByEnumerator2>,
                GroupBySpecificEnumerator<TGenGroupByInItem2, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerator2>
            > Except<TGenGroupByInItem, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable, TGenGroupByEnumerator, TGenGroupByInItem2, TGenGroupByEnumerable2, TGenGroupByEnumerator2>(
                GroupByDefaultEnumerable<TGenGroupByInItem, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable, TGenGroupByEnumerator> first,
                GroupBySpecificEnumerable<TGenGroupByInItem2, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable2, TGenGroupByEnumerator2> second
            )
                where TGenGroupByEnumerable : struct, IStructEnumerable<TGenGroupByInItem, TGenGroupByEnumerator>
                where TGenGroupByEnumerator : struct, IStructEnumerator<TGenGroupByInItem>
                where TGenGroupByEnumerable2: struct, IStructEnumerable<TGenGroupByInItem2,TGenGroupByEnumerator2>
                where TGenGroupByEnumerator2: struct, IStructEnumerator<TGenGroupByInItem2>
        {
            return
                CommonImplementation.Except<
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
            ExceptDefaultEnumerable<
                GroupingEnumerable<TGenLookupKey, TGenLookupElement>,
                GroupByDefaultEnumerable<TGenGroupByInItem, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable, TGenGroupByEnumerator>,
                GroupByDefaultEnumerator<TGenGroupByInItem, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerator>,
                LookupEnumerable<TGenLookupKey, TGenLookupElement>,
                LookupEnumerator<TGenLookupKey, TGenLookupElement>
            > Except<TGenGroupByInItem, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable, TGenGroupByEnumerator>(
                GroupByDefaultEnumerable<TGenGroupByInItem, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable, TGenGroupByEnumerator> first,
                LookupEnumerable<TGenLookupKey, TGenLookupElement> second
            )
            where TGenGroupByEnumerable: struct, IStructEnumerable<TGenGroupByInItem, TGenGroupByEnumerator>
            where TGenGroupByEnumerator: struct, IStructEnumerator<TGenGroupByInItem>
        {
            return
                CommonImplementation.Except<
                    GroupingEnumerable<TGenLookupKey, TGenLookupElement>,
                    GroupByDefaultEnumerable<TGenGroupByInItem, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable, TGenGroupByEnumerator>,
                    GroupByDefaultEnumerator<TGenGroupByInItem, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerator>,
                    LookupEnumerable<TGenLookupKey, TGenLookupElement>,
                    LookupEnumerator<TGenLookupKey, TGenLookupElement>
                >(
                    ref first,
                    ref second
                );
        }

        // Except, specific - GroupByDefault

        public
            ExceptSpecificEnumerable<
                GroupingEnumerable<TGenLookupKey, TGenLookupElement>,
                GroupByDefaultEnumerable<TGenGroupByInItem, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable, TGenGroupByEnumerator>,
                GroupByDefaultEnumerator<TGenGroupByInItem, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerator>,
                GroupByDefaultEnumerable<TGenGroupByInItem2, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable2, TGenGroupByEnumerator2>,
                GroupByDefaultEnumerator<TGenGroupByInItem2, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerator2>
            > Except<TGenGroupByInItem, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable, TGenGroupByEnumerator, TGenGroupByInItem2, TGenGroupByEnumerable2, TGenGroupByEnumerator2>(
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
                CommonImplementation.Except<
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
            ExceptSpecificEnumerable<
                GroupingEnumerable<TGenLookupKey, TGenLookupElement>,
                GroupByDefaultEnumerable<TGenGroupByInItem, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable, TGenGroupByEnumerator>,
                GroupByDefaultEnumerator<TGenGroupByInItem, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerator>,
                GroupBySpecificEnumerable<TGenGroupByInItem2, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable2, TGenGroupByEnumerator2>,
                GroupBySpecificEnumerator<TGenGroupByInItem2, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerator2>
            > Except<TGenGroupByInItem, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable, TGenGroupByEnumerator, TGenGroupByInItem2, TGenGroupByEnumerable2, TGenGroupByEnumerator2>(
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
                CommonImplementation.Except<
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
            ExceptSpecificEnumerable<
                GroupingEnumerable<TGenLookupKey, TGenLookupElement>,
                GroupByDefaultEnumerable<TGenGroupByInItem, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable, TGenGroupByEnumerator>,
                GroupByDefaultEnumerator<TGenGroupByInItem, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerator>,
                LookupEnumerable<TGenLookupKey, TGenLookupElement>,
                LookupEnumerator<TGenLookupKey, TGenLookupElement>
            > Except<TGenGroupByInItem, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable, TGenGroupByEnumerator>(
                GroupByDefaultEnumerable<TGenGroupByInItem, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable, TGenGroupByEnumerator> first,
                LookupEnumerable<TGenLookupKey, TGenLookupElement> second,
                IEqualityComparer<GroupingEnumerable<TGenLookupKey, TGenLookupElement>> comparer
            )
            where TGenGroupByEnumerable : struct, IStructEnumerable<TGenGroupByInItem, TGenGroupByEnumerator>
            where TGenGroupByEnumerator : struct, IStructEnumerator<TGenGroupByInItem>
        {
            return
                CommonImplementation.Except<
                    GroupingEnumerable<TGenLookupKey, TGenLookupElement>,
                    GroupByDefaultEnumerable<TGenGroupByInItem, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable, TGenGroupByEnumerator>,
                    GroupByDefaultEnumerator<TGenGroupByInItem, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerator>,
                    LookupEnumerable<TGenLookupKey, TGenLookupElement>,
                    LookupEnumerator<TGenLookupKey, TGenLookupElement>
                >(
                    ref first,
                    ref second,
                    comparer
                );
        }

        // Except, default - GroupBySpecific

        public
            ExceptDefaultEnumerable<
                GroupingEnumerable<TGenLookupKey, TGenLookupElement>,
                GroupBySpecificEnumerable<TGenGroupByInItem, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable, TGenGroupByEnumerator>,
                GroupBySpecificEnumerator<TGenGroupByInItem, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerator>,
                GroupByDefaultEnumerable<TGenGroupByInItem2, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable2, TGenGroupByEnumerator2>,
                GroupByDefaultEnumerator<TGenGroupByInItem2, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerator2>
            > Except<TGenGroupByInItem, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable, TGenGroupByEnumerator, TGenGroupByInItem2, TGenGroupByEnumerable2, TGenGroupByEnumerator2>(
            GroupBySpecificEnumerable<TGenGroupByInItem, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable, TGenGroupByEnumerator> first,
            GroupByDefaultEnumerable<TGenGroupByInItem2, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable2, TGenGroupByEnumerator2> second
        )
            where TGenGroupByEnumerable : struct, IStructEnumerable<TGenGroupByInItem, TGenGroupByEnumerator>
            where TGenGroupByEnumerator : struct, IStructEnumerator<TGenGroupByInItem>
            where TGenGroupByEnumerable2 : struct, IStructEnumerable<TGenGroupByInItem2, TGenGroupByEnumerator2>
            where TGenGroupByEnumerator2 : struct, IStructEnumerator<TGenGroupByInItem2>
        {
            return
                CommonImplementation.Except<
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
            ExceptDefaultEnumerable<
                GroupingEnumerable<TGenLookupKey, TGenLookupElement>,
                GroupBySpecificEnumerable<TGenGroupByInItem, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable, TGenGroupByEnumerator>,
                GroupBySpecificEnumerator<TGenGroupByInItem, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerator>,
                GroupBySpecificEnumerable<TGenGroupByInItem2, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable2, TGenGroupByEnumerator2>,
                GroupBySpecificEnumerator<TGenGroupByInItem2, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerator2>
            > Except<TGenGroupByInItem, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable, TGenGroupByEnumerator, TGenGroupByInItem2, TGenGroupByEnumerable2, TGenGroupByEnumerator2>(
                GroupBySpecificEnumerable<TGenGroupByInItem, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable, TGenGroupByEnumerator> first,
                GroupBySpecificEnumerable<TGenGroupByInItem2, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable2, TGenGroupByEnumerator2> second
            )
                where TGenGroupByEnumerable : struct, IStructEnumerable<TGenGroupByInItem, TGenGroupByEnumerator>
                where TGenGroupByEnumerator : struct, IStructEnumerator<TGenGroupByInItem>
                where TGenGroupByEnumerable2 : struct, IStructEnumerable<TGenGroupByInItem2, TGenGroupByEnumerator2>
                where TGenGroupByEnumerator2 : struct, IStructEnumerator<TGenGroupByInItem2>
        {
            return
                CommonImplementation.Except<
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
            ExceptDefaultEnumerable<
                GroupingEnumerable<TGenLookupKey, TGenLookupElement>,
                GroupBySpecificEnumerable<TGenGroupByInItem, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable, TGenGroupByEnumerator>,
                GroupBySpecificEnumerator<TGenGroupByInItem, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerator>,
                LookupEnumerable<TGenLookupKey, TGenLookupElement>,
                LookupEnumerator<TGenLookupKey, TGenLookupElement>
            > Except<TGenGroupByInItem, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable, TGenGroupByEnumerator>(
                GroupBySpecificEnumerable<TGenGroupByInItem, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable, TGenGroupByEnumerator> first,
                LookupEnumerable<TGenLookupKey, TGenLookupElement> second
            )
            where TGenGroupByEnumerable : struct, IStructEnumerable<TGenGroupByInItem, TGenGroupByEnumerator>
            where TGenGroupByEnumerator : struct, IStructEnumerator<TGenGroupByInItem>
        {
            return
                CommonImplementation.Except<
                    GroupingEnumerable<TGenLookupKey, TGenLookupElement>,
                    GroupBySpecificEnumerable<TGenGroupByInItem, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable, TGenGroupByEnumerator>,
                    GroupBySpecificEnumerator<TGenGroupByInItem, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerator>,
                    LookupEnumerable<TGenLookupKey, TGenLookupElement>,
                    LookupEnumerator<TGenLookupKey, TGenLookupElement>
                >(
                    ref first,
                    ref second
                );
        }

        // Except, specific - GroupBySpecific

        public
            ExceptSpecificEnumerable<
                GroupingEnumerable<TGenLookupKey, TGenLookupElement>,
                GroupBySpecificEnumerable<TGenGroupByInItem, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable, TGenGroupByEnumerator>,
                GroupBySpecificEnumerator<TGenGroupByInItem, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerator>,
                GroupByDefaultEnumerable<TGenGroupByInItem2, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable2, TGenGroupByEnumerator2>,
                GroupByDefaultEnumerator<TGenGroupByInItem2, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerator2>
            > Except<TGenGroupByInItem, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable, TGenGroupByEnumerator, TGenGroupByInItem2, TGenGroupByEnumerable2, TGenGroupByEnumerator2>(
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
                CommonImplementation.Except<
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
            ExceptSpecificEnumerable<
                GroupingEnumerable<TGenLookupKey, TGenLookupElement>,
                GroupBySpecificEnumerable<TGenGroupByInItem, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable, TGenGroupByEnumerator>,
                GroupBySpecificEnumerator<TGenGroupByInItem, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerator>,
                GroupBySpecificEnumerable<TGenGroupByInItem2, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable2, TGenGroupByEnumerator2>,
                GroupBySpecificEnumerator<TGenGroupByInItem2, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerator2>
            > Except<TGenGroupByInItem, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable, TGenGroupByEnumerator, TGenGroupByInItem2, TGenGroupByEnumerable2, TGenGroupByEnumerator2>(
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
                CommonImplementation.Except<
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
            ExceptSpecificEnumerable<
                GroupingEnumerable<TGenLookupKey, TGenLookupElement>,
                GroupBySpecificEnumerable<TGenGroupByInItem, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable, TGenGroupByEnumerator>,
                GroupBySpecificEnumerator<TGenGroupByInItem, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerator>,
                LookupEnumerable<TGenLookupKey, TGenLookupElement>,
                LookupEnumerator<TGenLookupKey, TGenLookupElement>
            > Except<TGenGroupByInItem, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable, TGenGroupByEnumerator>(
                GroupBySpecificEnumerable<TGenGroupByInItem, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable, TGenGroupByEnumerator> first,
                LookupEnumerable<TGenLookupKey, TGenLookupElement> second,
                IEqualityComparer<GroupingEnumerable<TGenLookupKey, TGenLookupElement>> comparer
            )
            where TGenGroupByEnumerable : struct, IStructEnumerable<TGenGroupByInItem, TGenGroupByEnumerator>
            where TGenGroupByEnumerator : struct, IStructEnumerator<TGenGroupByInItem>
        {
            return
                CommonImplementation.Except<
                    GroupingEnumerable<TGenLookupKey, TGenLookupElement>,
                    GroupBySpecificEnumerable<TGenGroupByInItem, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable, TGenGroupByEnumerator>,
                    GroupBySpecificEnumerator<TGenGroupByInItem, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerator>,
                    LookupEnumerable<TGenLookupKey, TGenLookupElement>,
                    LookupEnumerator<TGenLookupKey, TGenLookupElement>
                >(
                    ref first,
                    ref second,
                    comparer
                );
        }

        // Except, default - Lookup

        public
            ExceptDefaultEnumerable<
                GroupingEnumerable<TGenLookupKey, TGenLookupElement>,
                LookupEnumerable<TGenLookupKey, TGenLookupElement>,
                LookupEnumerator<TGenLookupKey, TGenLookupElement>,
                GroupByDefaultEnumerable<TGenGroupByInItem2, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable2, TGenGroupByEnumerator2>,
                GroupByDefaultEnumerator<TGenGroupByInItem2, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerator2>
            > Except<TGenLookupKey, TGenLookupElement, TGenGroupByInItem2, TGenGroupByEnumerable2, TGenGroupByEnumerator2>(
            LookupEnumerable<TGenLookupKey, TGenLookupElement> first,
            GroupByDefaultEnumerable<TGenGroupByInItem2, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable2, TGenGroupByEnumerator2> second
        )
            where TGenGroupByEnumerable2 : struct, IStructEnumerable<TGenGroupByInItem2, TGenGroupByEnumerator2>
            where TGenGroupByEnumerator2 : struct, IStructEnumerator<TGenGroupByInItem2>
        {
            return
                CommonImplementation.Except<
                    GroupingEnumerable<TGenLookupKey, TGenLookupElement>,
                    LookupEnumerable<TGenLookupKey, TGenLookupElement>,
                    LookupEnumerator<TGenLookupKey, TGenLookupElement>,
                    GroupByDefaultEnumerable<TGenGroupByInItem2, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable2, TGenGroupByEnumerator2>,
                    GroupByDefaultEnumerator<TGenGroupByInItem2, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerator2>
                >(
                    ref first,
                    ref second
                );
        }

        public
            ExceptDefaultEnumerable<
                GroupingEnumerable<TGenLookupKey, TGenLookupElement>,
                LookupEnumerable<TGenLookupKey, TGenLookupElement>,
                LookupEnumerator<TGenLookupKey, TGenLookupElement>,
                GroupBySpecificEnumerable<TGenGroupByInItem2, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable2, TGenGroupByEnumerator2>,
                GroupBySpecificEnumerator<TGenGroupByInItem2, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerator2>
            > Except<TGenLookupKey, TGenLookupElement, TGenGroupByInItem2, TGenGroupByEnumerable2, TGenGroupByEnumerator2>(
                LookupEnumerable<TGenLookupKey, TGenLookupElement> first,
                GroupBySpecificEnumerable<TGenGroupByInItem2, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable2, TGenGroupByEnumerator2> second
            )
                where TGenGroupByEnumerable2 : struct, IStructEnumerable<TGenGroupByInItem2, TGenGroupByEnumerator2>
                where TGenGroupByEnumerator2 : struct, IStructEnumerator<TGenGroupByInItem2>
        {
            return
                CommonImplementation.Except<
                    GroupingEnumerable<TGenLookupKey, TGenLookupElement>,
                    LookupEnumerable<TGenLookupKey, TGenLookupElement>,
                    LookupEnumerator<TGenLookupKey, TGenLookupElement>,
                    GroupBySpecificEnumerable<TGenGroupByInItem2, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable2, TGenGroupByEnumerator2>,
                    GroupBySpecificEnumerator<TGenGroupByInItem2, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerator2>
                >(
                    ref first,
                    ref second
                );
        }

        public
            ExceptDefaultEnumerable<
                GroupingEnumerable<TGenLookupKey, TGenLookupElement>,
                LookupEnumerable<TGenLookupKey, TGenLookupElement>,
                LookupEnumerator<TGenLookupKey, TGenLookupElement>,
                LookupEnumerable<TGenLookupKey, TGenLookupElement>,
                LookupEnumerator<TGenLookupKey, TGenLookupElement>
            > Except<TGenLookupKey, TGenLookupElement>(
                LookupEnumerable<TGenLookupKey, TGenLookupElement> first,
                LookupEnumerable<TGenLookupKey, TGenLookupElement> second
            )
        {
            return
                CommonImplementation.Except<
                    GroupingEnumerable<TGenLookupKey, TGenLookupElement>,
                    LookupEnumerable<TGenLookupKey, TGenLookupElement>,
                    LookupEnumerator<TGenLookupKey, TGenLookupElement>,
                    LookupEnumerable<TGenLookupKey, TGenLookupElement>,
                    LookupEnumerator<TGenLookupKey, TGenLookupElement>
                >(
                    ref first,
                    ref second
                );
        }

        // Except, specific - Lookup

        public
            ExceptSpecificEnumerable<
                GroupingEnumerable<TGenLookupKey, TGenLookupElement>,
                LookupEnumerable<TGenLookupKey, TGenLookupElement>,
                LookupEnumerator<TGenLookupKey, TGenLookupElement>,
                GroupByDefaultEnumerable<TGenGroupByInItem2, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable2, TGenGroupByEnumerator2>,
                GroupByDefaultEnumerator<TGenGroupByInItem2, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerator2>
            > Except<TGenLookupKey, TGenLookupElement, TGenGroupByInItem2, TGenGroupByEnumerable2, TGenGroupByEnumerator2>(
            LookupEnumerable<TGenLookupKey, TGenLookupElement> first,
            GroupByDefaultEnumerable<TGenGroupByInItem2, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable2, TGenGroupByEnumerator2> second,
            IEqualityComparer<GroupingEnumerable<TGenLookupKey, TGenLookupElement>> comparer
        )
            where TGenGroupByEnumerable2 : struct, IStructEnumerable<TGenGroupByInItem2, TGenGroupByEnumerator2>
            where TGenGroupByEnumerator2 : struct, IStructEnumerator<TGenGroupByInItem2>
        {
            return
                CommonImplementation.Except<
                    GroupingEnumerable<TGenLookupKey, TGenLookupElement>,
                    LookupEnumerable<TGenLookupKey, TGenLookupElement>,
                    LookupEnumerator<TGenLookupKey, TGenLookupElement>,
                    GroupByDefaultEnumerable<TGenGroupByInItem2, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable2, TGenGroupByEnumerator2>,
                    GroupByDefaultEnumerator<TGenGroupByInItem2, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerator2>
                >(
                    ref first,
                    ref second,
                    comparer
                );
        }

        public
            ExceptSpecificEnumerable<
                GroupingEnumerable<TGenLookupKey, TGenLookupElement>,
                LookupEnumerable<TGenLookupKey, TGenLookupElement>,
                LookupEnumerator<TGenLookupKey, TGenLookupElement>,
                GroupBySpecificEnumerable<TGenGroupByInItem2, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable2, TGenGroupByEnumerator2>,
                GroupBySpecificEnumerator<TGenGroupByInItem2, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerator2>
            > Except<TGenLookupKey, TGenLookupElement, TGenGroupByInItem2, TGenGroupByEnumerable2, TGenGroupByEnumerator2>(
                LookupEnumerable<TGenLookupKey, TGenLookupElement> first,
                GroupBySpecificEnumerable<TGenGroupByInItem2, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable2, TGenGroupByEnumerator2> second,
                IEqualityComparer<GroupingEnumerable<TGenLookupKey, TGenLookupElement>> comparer
            )
                where TGenGroupByEnumerable2 : struct, IStructEnumerable<TGenGroupByInItem2, TGenGroupByEnumerator2>
                where TGenGroupByEnumerator2 : struct, IStructEnumerator<TGenGroupByInItem2>
        {
            return
                CommonImplementation.Except<
                    GroupingEnumerable<TGenLookupKey, TGenLookupElement>,
                    LookupEnumerable<TGenLookupKey, TGenLookupElement>,
                    LookupEnumerator<TGenLookupKey, TGenLookupElement>,
                    GroupBySpecificEnumerable<TGenGroupByInItem2, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerable2, TGenGroupByEnumerator2>,
                    GroupBySpecificEnumerator<TGenGroupByInItem2, TGenLookupKey, TGenLookupElement, TGenGroupByEnumerator2>
                >(
                    ref first,
                    ref second,
                    comparer
                );
        }

        public
            ExceptSpecificEnumerable<
                GroupingEnumerable<TGenLookupKey, TGenLookupElement>,
                LookupEnumerable<TGenLookupKey, TGenLookupElement>,
                LookupEnumerator<TGenLookupKey, TGenLookupElement>,
                LookupEnumerable<TGenLookupKey, TGenLookupElement>,
                LookupEnumerator<TGenLookupKey, TGenLookupElement>
            > Except<TGenLookupKey, TGenLookupElement>(
                LookupEnumerable<TGenLookupKey, TGenLookupElement> first,
                LookupEnumerable<TGenLookupKey, TGenLookupElement> second,
                IEqualityComparer<GroupingEnumerable<TGenLookupKey, TGenLookupElement>> comparer
            )
        {
            return
                CommonImplementation.Except<
                    GroupingEnumerable<TGenLookupKey, TGenLookupElement>,
                    LookupEnumerable<TGenLookupKey, TGenLookupElement>,
                    LookupEnumerator<TGenLookupKey, TGenLookupElement>,
                    LookupEnumerable<TGenLookupKey, TGenLookupElement>,
                    LookupEnumerator<TGenLookupKey, TGenLookupElement>
                >(
                    ref first,
                    ref second,
                    comparer
                );
        }
    }
}