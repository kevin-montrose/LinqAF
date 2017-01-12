using LinqAF.Impl;
using System;

namespace LinqAF
{
    public static class LookupExtensionMethods
    {
        public static int Count<TLookupKey, TLookupElement>(this LookupEnumerable<TLookupKey, TLookupElement> source)
        => CommonImplementation.Count<GroupingEnumerable<TLookupKey, TLookupElement>, LookupEnumerable<TLookupKey, TLookupElement>, LookupEnumerator<TLookupKey, TLookupElement>>(ref source);

        public static int Count<TLookupKey, TLookupElement>(this LookupEnumerable<TLookupKey, TLookupElement> source, Func<GroupingEnumerable<TLookupKey, TLookupElement>, bool> predicate)
        => CommonImplementation.Count<GroupingEnumerable<TLookupKey, TLookupElement>, LookupEnumerable<TLookupKey, TLookupElement>, LookupEnumerator<TLookupKey, TLookupElement>>(ref source, predicate);
    }
}
