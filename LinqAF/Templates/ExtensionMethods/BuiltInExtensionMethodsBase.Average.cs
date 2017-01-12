using LinqAF.Impl;
using System;

namespace LinqAF
{
    abstract partial class BuiltInExtensionMethodsBase
    {

        public double Average<TItem>(BuiltInEnumerable<TItem> source, Func<TItem, long> selector)
        {
            var bridge = Bridge(source, nameof(source));
            if (selector == null) throw new ArgumentNullException(nameof(selector));

            return CommonImplementation.AverageSelectorImpl<TItem, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>>(RefLocal(bridge), selector);
        }

        public float Average<TItem>(BuiltInEnumerable<TItem> source, Func<TItem, float> selector)
        {
            var bridge = Bridge(source, nameof(source));
            if (selector == null) throw new ArgumentNullException(nameof(selector));

            return CommonImplementation.AverageSelectorImpl<TItem, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>>(RefLocal(bridge), selector);
        }

        public double Average<TItem>(BuiltInEnumerable<TItem> source, Func<TItem, double> selector)
        {
            var bridge = Bridge(source, nameof(source));
            if (selector == null) throw new ArgumentNullException(nameof(selector));

            return CommonImplementation.AverageSelectorImpl<TItem, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>>(RefLocal(bridge), selector);
        }

        public decimal Average<TItem>(BuiltInEnumerable<TItem> source, Func<TItem, decimal> selector)
        {
            var bridge = Bridge(source, nameof(source));
            if (selector == null) throw new ArgumentNullException(nameof(selector));

            return CommonImplementation.AverageSelectorImpl<TItem, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>>(RefLocal(bridge), selector);
        }

        public decimal? Average<TItem>(BuiltInEnumerable<TItem> source, Func<TItem, decimal?> selector)
        {
            var bridge = Bridge(source, nameof(source));
            if (selector == null) throw new ArgumentNullException(nameof(selector));

            return CommonImplementation.AverageSelectorImpl<TItem, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>>(RefLocal(bridge), selector);
        }

        public double? Average<TItem>(BuiltInEnumerable<TItem> source, Func<TItem, double?> selector)
        {
            var bridge = Bridge(source, nameof(source));
            if (selector == null) throw new ArgumentNullException(nameof(selector));

            return CommonImplementation.AverageSelectorImpl<TItem, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>>(RefLocal(bridge), selector);
        }

        public float? Average<TItem>(BuiltInEnumerable<TItem> source, Func<TItem, float?> selector)
        {
            var bridge = Bridge(source, nameof(source));
            if (selector == null) throw new ArgumentNullException(nameof(selector));

            return CommonImplementation.AverageSelectorImpl<TItem, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>>(RefLocal(bridge), selector);
        }

        public double? Average<TItem>(BuiltInEnumerable<TItem> source, Func<TItem, long?> selector)
        {
            var bridge = Bridge(source, nameof(source));
            if (selector == null) throw new ArgumentNullException(nameof(selector));

            return CommonImplementation.AverageSelectorImpl<TItem, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>>(RefLocal(bridge), selector);
        }

        public double? Average<TItem>(BuiltInEnumerable<TItem> source, Func<TItem, int?> selector)
        {
            var bridge = Bridge(source, nameof(source));
            if (selector == null) throw new ArgumentNullException(nameof(selector));

            return CommonImplementation.AverageSelectorImpl<TItem, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>>(RefLocal(bridge), selector);
        }

        public double Average<TItem>(BuiltInEnumerable<TItem> source, Func<TItem, int> selector)
        {
            var bridge = Bridge(source, nameof(source));
            if (selector == null) throw new ArgumentNullException(nameof(selector));

            return CommonImplementation.AverageSelectorImpl<TItem, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>>(RefLocal(bridge), selector);
        }

        public double Average(BuiltInEnumerable<int> source)
        {
            var bridge = Bridge(source, nameof(source));

            return CommonImplementation.AverageIntImpl<BuiltInEnumerable<int>, BuiltInEnumerator<int>>(RefLocal(bridge));
        }

        public double? Average(BuiltInEnumerable<int?> source)
        {
            var bridge = Bridge(source, nameof(source));

            return CommonImplementation.AverageNullableIntImpl<BuiltInEnumerable<int?>, BuiltInEnumerator<int?>>(RefLocal(bridge));
        }

        public double Average(BuiltInEnumerable<long> source)
        {
            var bridge = Bridge(source, nameof(source));

            return CommonImplementation.AverageLongImpl<BuiltInEnumerable<long>, BuiltInEnumerator<long>>(RefLocal(bridge));
        }

        public double? Average(BuiltInEnumerable<long?> source)
        {
            var bridge = Bridge(source, nameof(source));

            return CommonImplementation.AverageNullableLongImpl<BuiltInEnumerable<long?>, BuiltInEnumerator<long?>>(RefLocal(bridge));
        }

        public float Average(BuiltInEnumerable<float> source)
        {
            var bridge = Bridge(source, nameof(source));

            return CommonImplementation.AverageFloatImpl<BuiltInEnumerable<float>, BuiltInEnumerator<float>>(RefLocal(bridge));
        }

        public float? Average(BuiltInEnumerable<float?> source)
        {
            var bridge = Bridge(source, nameof(source));

            return CommonImplementation.AverageNullableFloatImpl<BuiltInEnumerable<float?>, BuiltInEnumerator<float?>>(RefLocal(bridge));
        }

        public double Average(BuiltInEnumerable<double> source)
        {
            var bridge = Bridge(source, nameof(source));

            return CommonImplementation.AverageDoubleImpl<BuiltInEnumerable<double>, BuiltInEnumerator<double>>(RefLocal(bridge));
        }

        public double? Average(BuiltInEnumerable<double?> source)
        {
            var bridge = Bridge(source, nameof(source));

            return CommonImplementation.AverageNullableDoubleImpl<BuiltInEnumerable<double?>, BuiltInEnumerator<double?>>(RefLocal(bridge));
        }

        public decimal Average(BuiltInEnumerable<decimal> source)
        {
            var bridge = Bridge(source, nameof(source));

            return CommonImplementation.AverageDecimalImpl<BuiltInEnumerable<decimal>, BuiltInEnumerator<decimal>>(RefLocal(bridge));
        }

        public decimal? Average(BuiltInEnumerable<decimal?> source)
        {
            var bridge = Bridge(source, nameof(source));

            return CommonImplementation.AverageNullableDecimalImpl<BuiltInEnumerable<decimal?>, BuiltInEnumerator<decimal?>>(RefLocal(bridge));
        }
    }
}
