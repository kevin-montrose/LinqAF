using LinqAF.Impl;
using System;

namespace LinqAF
{
    public static class LookupExtensionMethods
    {
        public static int Count<TLookupKey, TLookupElement>(this LookupDefaultEnumerable<TLookupKey, TLookupElement> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return source.Count;
        }

        public static int Count<TLookupKey, TLookupElement>(this LookupDefaultEnumerable<TLookupKey, TLookupElement> source, Func<GroupingEnumerable<TLookupKey, TLookupElement>, bool> predicate)
        => CommonImplementation.Count<GroupingEnumerable<TLookupKey, TLookupElement>, LookupDefaultEnumerable<TLookupKey, TLookupElement>, LookupDefaultEnumerator<TLookupKey, TLookupElement>>(ref source, predicate);

        public static int Count<TLookupKey, TLookupElement>(this LookupSpecificEnumerable<TLookupKey, TLookupElement> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return source.Count;
        }

        public static int Count<TLookupKey, TLookupElement>(this LookupSpecificEnumerable<TLookupKey, TLookupElement> source, Func<GroupingEnumerable<TLookupKey, TLookupElement>, bool> predicate)
        => CommonImplementation.Count<GroupingEnumerable<TLookupKey, TLookupElement>, LookupSpecificEnumerable<TLookupKey, TLookupElement>, LookupSpecificEnumerator<TLookupKey, TLookupElement>>(ref source, predicate);
    }
}
