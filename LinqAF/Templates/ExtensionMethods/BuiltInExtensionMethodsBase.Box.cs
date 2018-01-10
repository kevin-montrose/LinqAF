using System.Collections.Generic;

namespace LinqAF
{
    abstract partial class BuiltInExtensionMethodsBase
    {
        public BoxedEnumerable<TItem> Box<TItem>(BuiltInEnumerable<TItem> source)
        {
            if (((object)source) == null) return Impl.EmptyCache<TItem>.BadBoxedEmpty;

            var bridge = Bridge(source, nameof(source));

            return (BoxedEnumerable<TItem>)bridge;
        }
    }
}
