using LinqAF.Impl;

namespace LinqAF
{
    public static class AverageExtensionMethods
    {
        // int
        public static double Average(this OneItemDefaultEnumerable<int> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return default(int);
        }

        public static double Average(this OneItemSpecificEnumerable<int> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return source.Item;
        }

        public static double Average(this OneItemDefaultOrderedEnumerable<int> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return default(int);
        }

        public static double Average(this OneItemSpecificOrderedEnumerable<int> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return source.Item;
        }

        public static double Average(this EmptyOrderedEnumerable<int> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            throw CommonImplementation.SequenceEmpty();
        }

        public static double Average(this EmptyEnumerable<int> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            throw CommonImplementation.SequenceEmpty();
        }

        // int?
        public static double? Average(this OneItemDefaultEnumerable<int?> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return default(int?);
        }

        public static double? Average(this OneItemSpecificEnumerable<int?> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return source.Item;
        }

        public static double? Average(this OneItemDefaultOrderedEnumerable<int?> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return default(int?);
        }

        public static double? Average(this OneItemSpecificOrderedEnumerable<int?> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return source.Item;
        }

        public static double? Average(this EmptyOrderedEnumerable<int?> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return null;
        }

        public static double? Average(this EmptyEnumerable<int?> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return null;
        }

        // float
        public static float Average(this OneItemDefaultEnumerable<float> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return default(float);
        }

        public static float Average(this OneItemSpecificEnumerable<float> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return source.Item;
        }

        public static float Average(this OneItemDefaultOrderedEnumerable<float> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return default(float);
        }

        public static float Average(this OneItemSpecificOrderedEnumerable<float> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return source.Item;
        }

        public static float Average(this EmptyOrderedEnumerable<float> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            throw CommonImplementation.SequenceEmpty();
        }

        public static float Average(this EmptyEnumerable<float> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            throw CommonImplementation.SequenceEmpty();
        }

        // float?
        public static float? Average(this OneItemDefaultEnumerable<float?> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return default(float?);
        }

        public static float? Average(this OneItemSpecificEnumerable<float?> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return source.Item;
        }

        public static float? Average(this OneItemDefaultOrderedEnumerable<float?> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return default(float?);
        }

        public static float? Average(this OneItemSpecificOrderedEnumerable<float?> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return source.Item;
        }

        public static float? Average(this EmptyOrderedEnumerable<float?> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return null;
        }

        public static float? Average(this EmptyEnumerable<float?> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return null;
        }

        // long
        public static double Average(this OneItemDefaultEnumerable<long> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return default(long);
        }

        public static double Average(this OneItemSpecificEnumerable<long> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return source.Item;
        }

        public static double Average(this OneItemDefaultOrderedEnumerable<long> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return default(long);
        }

        public static double Average(this OneItemSpecificOrderedEnumerable<long> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return source.Item;
        }

        public static double Average(this EmptyOrderedEnumerable<long> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            throw CommonImplementation.SequenceEmpty();
        }

        public static double Average(this EmptyEnumerable<long> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            throw CommonImplementation.SequenceEmpty();
        }

        // long?
        public static double? Average(this OneItemDefaultEnumerable<long?> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return default(long?);
        }

        public static double? Average(this OneItemSpecificEnumerable<long?> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return source.Item;
        }

        public static double? Average(this OneItemDefaultOrderedEnumerable<long?> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return default(long?);
        }

        public static double? Average(this OneItemSpecificOrderedEnumerable<long?> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return source.Item;
        }

        public static double? Average(this EmptyOrderedEnumerable<long?> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return null;
        }

        public static double? Average(this EmptyEnumerable<long?> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return null;
        }

        // double
        public static double Average(this OneItemDefaultEnumerable<double> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return default(double);
        }

        public static double Average(this OneItemSpecificEnumerable<double> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return source.Item;
        }

        public static double Average(this OneItemDefaultOrderedEnumerable<double> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return default(double);
        }

        public static double Average(this OneItemSpecificOrderedEnumerable<double> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return source.Item;
        }

        public static double Average(this EmptyOrderedEnumerable<double> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            throw CommonImplementation.SequenceEmpty();
        }

        public static double Average(this EmptyEnumerable<double> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            throw CommonImplementation.SequenceEmpty();
        }

        // double?
        public static double? Average(this OneItemDefaultEnumerable<double?> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return default(double?);
        }

        public static double? Average(this OneItemSpecificEnumerable<double?> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return source.Item;
        }

        public static double? Average(this OneItemDefaultOrderedEnumerable<double?> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return default(double?);
        }

        public static double? Average(this OneItemSpecificOrderedEnumerable<double?> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return source.Item;
        }

        public static double? Average(this EmptyOrderedEnumerable<double?> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return null;
        }

        public static double? Average(this EmptyEnumerable<double?> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return null;
        }

        // decimal
        public static decimal Average(this OneItemDefaultEnumerable<decimal> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return default(decimal);
        }

        public static decimal Average(this OneItemSpecificEnumerable<decimal> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return source.Item;
        }

        public static decimal Average(this OneItemDefaultOrderedEnumerable<decimal> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return default(decimal);
        }

        public static decimal Average(this OneItemSpecificOrderedEnumerable<decimal> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return source.Item;
        }

        public static decimal Average(this EmptyOrderedEnumerable<decimal> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            throw CommonImplementation.SequenceEmpty();
        }

        public static decimal Average(this EmptyEnumerable<decimal> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            throw CommonImplementation.SequenceEmpty();
        }

        // decimal?
        public static decimal? Average(this OneItemDefaultEnumerable<decimal?> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return default(decimal?);
        }

        public static decimal? Average(this OneItemSpecificEnumerable<decimal?> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return source.Item;
        }

        public static decimal? Average(this OneItemDefaultOrderedEnumerable<decimal?> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return default(decimal?);
        }

        public static decimal? Average(this OneItemSpecificOrderedEnumerable<decimal?> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return source.Item;
        }

        public static decimal? Average(this EmptyOrderedEnumerable<decimal?> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return null;
        }

        public static decimal? Average(this EmptyEnumerable<decimal?> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return null;
        }
    }
}
