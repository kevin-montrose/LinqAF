using LinqAF.Impl;

namespace LinqAF
{
    public static class MaxExtensionMethods
    {
        // int
        public static int Max(this EmptyEnumerable<int> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            throw CommonImplementation.SequenceEmpty();
        }

        public static int Max(this EmptyOrderedEnumerable<int> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            throw CommonImplementation.SequenceEmpty();
        }

        public static int Max(this OneItemDefaultEnumerable<int> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return default(int);
        }

        public static int Max(this OneItemDefaultOrderedEnumerable<int> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return default(int);
        }

        public static int Max(this OneItemSpecificEnumerable<int> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return source.Item;
        }

        public static int Max(this OneItemSpecificOrderedEnumerable<int> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return source.Item;
        }

        // float
        public static float Max(this EmptyEnumerable<float> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            throw CommonImplementation.SequenceEmpty();
        }

        public static float Max(this EmptyOrderedEnumerable<float> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            throw CommonImplementation.SequenceEmpty();
        }

        public static float Max(this OneItemDefaultEnumerable<float> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return default(float);
        }

        public static float Max(this OneItemDefaultOrderedEnumerable<float> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return default(float);
        }

        public static float Max(this OneItemSpecificEnumerable<float> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return source.Item;
        }

        public static float Max(this OneItemSpecificOrderedEnumerable<float> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return source.Item;
        }

        // long
        public static long Max(this EmptyEnumerable<long> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            throw CommonImplementation.SequenceEmpty();
        }

        public static long Max(this EmptyOrderedEnumerable<long> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            throw CommonImplementation.SequenceEmpty();
        }

        public static long Max(this OneItemDefaultEnumerable<long> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return default(long);
        }

        public static long Max(this OneItemDefaultOrderedEnumerable<long> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return default(long);
        }

        public static long Max(this OneItemSpecificEnumerable<long> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return source.Item;
        }

        public static long Max(this OneItemSpecificOrderedEnumerable<long> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return source.Item;
        }

        // double
        public static double Max(this EmptyEnumerable<double> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            throw CommonImplementation.SequenceEmpty();
        }

        public static double Max(this EmptyOrderedEnumerable<double> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            throw CommonImplementation.SequenceEmpty();
        }

        public static double Max(this OneItemDefaultEnumerable<double> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return default(double);
        }

        public static double Max(this OneItemDefaultOrderedEnumerable<double> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return default(double);
        }

        public static double Max(this OneItemSpecificEnumerable<double> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return source.Item;
        }

        public static double Max(this OneItemSpecificOrderedEnumerable<double> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return source.Item;
        }

        // decimal
        public static decimal Max(this EmptyEnumerable<decimal> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            throw CommonImplementation.SequenceEmpty();
        }

        public static decimal Max(this EmptyOrderedEnumerable<decimal> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            throw CommonImplementation.SequenceEmpty();
        }

        public static decimal Max(this OneItemDefaultEnumerable<decimal> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return default(decimal);
        }

        public static decimal Max(this OneItemDefaultOrderedEnumerable<decimal> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return default(decimal);
        }

        public static decimal Max(this OneItemSpecificEnumerable<decimal> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return source.Item;
        }

        public static decimal Max(this OneItemSpecificOrderedEnumerable<decimal> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return source.Item;
        }

        // int?
        public static int? Max(this EmptyEnumerable<int?> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return null;
        }

        public static int? Max(this EmptyOrderedEnumerable<int?> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return null;
        }

        public static int? Max(this OneItemDefaultEnumerable<int?> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return default(int?);
        }

        public static int? Max(this OneItemDefaultOrderedEnumerable<int?> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return default(int?);
        }

        public static int? Max(this OneItemSpecificEnumerable<int?> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return source.Item;
        }

        public static int? Max(this OneItemSpecificOrderedEnumerable<int?> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return source.Item;
        }

        // float?
        public static float? Max(this EmptyEnumerable<float?> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return null;
        }

        public static float? Max(this EmptyOrderedEnumerable<float?> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return null;
        }

        public static float? Max(this OneItemDefaultEnumerable<float?> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return default(float?);
        }

        public static float? Max(this OneItemDefaultOrderedEnumerable<float?> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return default(float?);
        }

        public static float? Max(this OneItemSpecificEnumerable<float?> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return source.Item;
        }

        public static float? Max(this OneItemSpecificOrderedEnumerable<float?> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return source.Item;
        }

        // long?
        public static long? Max(this EmptyEnumerable<long?> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return null;
        }

        public static long? Max(this EmptyOrderedEnumerable<long?> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return null;
        }

        public static long? Max(this OneItemDefaultEnumerable<long?> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return default(long?);
        }

        public static long? Max(this OneItemDefaultOrderedEnumerable<long?> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return default(long?);
        }

        public static long? Max(this OneItemSpecificEnumerable<long?> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return source.Item;
        }

        public static long? Max(this OneItemSpecificOrderedEnumerable<long?> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return source.Item;
        }

        // double?
        public static double? Max(this EmptyEnumerable<double?> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return null;
        }

        public static double? Max(this EmptyOrderedEnumerable<double?> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return null;
        }

        public static double? Max(this OneItemDefaultEnumerable<double?> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return default(long?);
        }

        public static double? Max(this OneItemDefaultOrderedEnumerable<double?> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return default(double?);
        }

        public static double? Max(this OneItemSpecificEnumerable<double?> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return source.Item;
        }

        public static double? Max(this OneItemSpecificOrderedEnumerable<double?> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return source.Item;
        }

        // decimal?
        public static decimal? Max(this EmptyEnumerable<decimal?> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return null;
        }

        public static decimal? Max(this EmptyOrderedEnumerable<decimal?> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return null;
        }

        public static decimal? Max(this OneItemDefaultEnumerable<decimal?> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return default(long?);
        }

        public static decimal? Max(this OneItemDefaultOrderedEnumerable<decimal?> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return default(decimal?);
        }

        public static decimal? Max(this OneItemSpecificEnumerable<decimal?> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return source.Item;
        }

        public static decimal? Max(this OneItemSpecificOrderedEnumerable<decimal?> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return source.Item;
        }
    }
}
