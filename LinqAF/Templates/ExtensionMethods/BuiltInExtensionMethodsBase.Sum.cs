using LinqAF.Impl;
using System;

namespace LinqAF
{
    abstract partial class BuiltInExtensionMethodsBase :
        ExtensionMethodsBase
    {
        public int Sum(BuiltInEnumerable<int> source)
        {
            var bridge = Bridge(source, nameof(source));

            return CommonImplementation.SumInt<BuiltInEnumerable<int>, BuiltInEnumerator<int>>(RefLocal(bridge));
        }

        public int? Sum(BuiltInEnumerable<int?> source)
        {
            var bridge = Bridge(source, nameof(source));

            return CommonImplementation.SumNullableInt<BuiltInEnumerable<int?>, BuiltInEnumerator<int?>>(RefLocal(bridge));
        }

        public long Sum(BuiltInEnumerable<long> source)
        {
            var bridge = Bridge(source, nameof(source));

            return CommonImplementation.SumLong<BuiltInEnumerable<long>, BuiltInEnumerator<long>>(RefLocal(bridge));
        }

        public long? Sum(BuiltInEnumerable<long?> source)
        {
            var bridge = Bridge(source, nameof(source));

            return CommonImplementation.SumNullableLong<BuiltInEnumerable<long?>, BuiltInEnumerator<long?>>(RefLocal(bridge));
        }

        public float Sum(BuiltInEnumerable<float> source)
        {
            var bridge = Bridge(source, nameof(source));

            return CommonImplementation.SumFloat<BuiltInEnumerable<float>, BuiltInEnumerator<float>>(RefLocal(bridge));
        }

        public float? Sum(BuiltInEnumerable<float?> source)
        {
            var bridge = Bridge(source, nameof(source));

            return CommonImplementation.SumNullableFloat<BuiltInEnumerable<float?>, BuiltInEnumerator<float?>>(RefLocal(bridge));
        }

        public double Sum(BuiltInEnumerable<double> source)
        {
            var bridge = Bridge(source, nameof(source));

            return CommonImplementation.SumDouble<BuiltInEnumerable<double>, BuiltInEnumerator<double>>(RefLocal(bridge));
        }

        public double? Sum(BuiltInEnumerable<double?> source)
        {
            var bridge = Bridge(source, nameof(source));

            return CommonImplementation.SumNullableDouble<BuiltInEnumerable<double?>, BuiltInEnumerator<double?>>(RefLocal(bridge));
        }

        public decimal Sum(BuiltInEnumerable<decimal> source)
        {
            var bridge = Bridge(source, nameof(source));

            return CommonImplementation.SumDecimal<BuiltInEnumerable<decimal>, BuiltInEnumerator<decimal>>(RefLocal(bridge));
        }

        public decimal? Sum(BuiltInEnumerable<decimal?> source)
        {
            var bridge = Bridge(source, nameof(source));

            return CommonImplementation.SumNullableDecimal<BuiltInEnumerable<decimal?>, BuiltInEnumerator<decimal?>>(RefLocal(bridge));
        }
        
        public long Sum<TItem>(BuiltInEnumerable<TItem> source, Func<TItem, long> selector)
        {
            var bridge = Bridge(source, nameof(source));
            if (selector == null) throw CommonImplementation.ArgumentNull(nameof(selector));

            return CommonImplementation.SumSelectorImpl<TItem, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>>(RefLocal(bridge), selector);
        }

        public float Sum<TItem>(BuiltInEnumerable<TItem> source, Func<TItem, float> selector)
        {
            var bridge = Bridge(source, nameof(source));
            if (selector == null) throw CommonImplementation.ArgumentNull(nameof(selector));

            return CommonImplementation.SumSelectorImpl<TItem, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>>(RefLocal(bridge), selector);
        }

        public double Sum<TItem>(BuiltInEnumerable<TItem> source, Func<TItem, double> selector)
        {
            var bridge = Bridge(source, nameof(source));
            if (selector == null) throw CommonImplementation.ArgumentNull(nameof(selector));

            return CommonImplementation.SumSelectorImpl<TItem, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>>(RefLocal(bridge), selector);
        }

        public decimal Sum<TItem>(BuiltInEnumerable<TItem> source, Func<TItem, decimal> selector)
        {
            var bridge = Bridge(source, nameof(source));
            if (selector == null) throw CommonImplementation.ArgumentNull(nameof(selector));

            return CommonImplementation.SumSelectorImpl<TItem, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>>(RefLocal(bridge), selector);
        }

        public decimal? Sum<TItem>(BuiltInEnumerable<TItem> source, Func<TItem, decimal?> selector)
        {
            var bridge = Bridge(source, nameof(source));
            if (selector == null) throw CommonImplementation.ArgumentNull(nameof(selector));

            return CommonImplementation.SumSelectorImpl<TItem, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>>(RefLocal(bridge), selector);
        }

        public double? Sum<TItem>(BuiltInEnumerable<TItem> source, Func<TItem, double?> selector)
        {
            var bridge = Bridge(source, nameof(source));
            if (selector == null) throw CommonImplementation.ArgumentNull(nameof(selector));

            return CommonImplementation.SumSelectorImpl<TItem, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>>(RefLocal(bridge), selector);
        }

        public float? Sum<TItem>(BuiltInEnumerable<TItem> source, Func<TItem, float?> selector)
        {
            var bridge = Bridge(source, nameof(source));
            if (selector == null) throw CommonImplementation.ArgumentNull(nameof(selector));

            return CommonImplementation.SumSelectorImpl<TItem, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>>(RefLocal(bridge), selector);
        }

        public long? Sum<TItem>(BuiltInEnumerable<TItem> source, Func<TItem, long?> selector)
        {
            var bridge = Bridge(source, nameof(source));
            if (selector == null) throw CommonImplementation.ArgumentNull(nameof(selector));

            return CommonImplementation.SumSelectorImpl<TItem, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>>(RefLocal(bridge), selector);
        }

        public int? Sum<TItem>(BuiltInEnumerable<TItem> source, Func<TItem, int?> selector)
        {
            var bridge = Bridge(source, nameof(source));
            if (selector == null) throw CommonImplementation.ArgumentNull(nameof(selector));

            return CommonImplementation.SumSelectorImpl<TItem, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>>(RefLocal(bridge), selector);
        }

        public int Sum<TItem>(BuiltInEnumerable<TItem> source, Func<TItem, int> selector)
        {
            var bridge = Bridge(source, nameof(source));
            if (selector == null) throw CommonImplementation.ArgumentNull(nameof(selector));

            return CommonImplementation.SumSelectorImpl<TItem, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>>(RefLocal(bridge), selector);
        }
    }
}
