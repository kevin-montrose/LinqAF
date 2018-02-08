using LinqAF.Impl;

namespace LinqAF
{
    public static class MinExtensionMethods
    {
        // int
        public static int Min(this EmptyEnumerable<int> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            throw CommonImplementation.SequenceEmpty();
        }

        public static int Min(this EmptyOrderedEnumerable<int> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            throw CommonImplementation.SequenceEmpty();
        }

        public static int Min(this OneItemDefaultEnumerable<int> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return default(int);
        }

        public static int Min(this OneItemDefaultOrderedEnumerable<int> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return default(int);
        }

        public static int Min(this OneItemSpecificEnumerable<int> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return source.Item;
        }

        public static int Min(this OneItemSpecificOrderedEnumerable<int> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return source.Item;
        }

        // float
        public static float Min(this EmptyEnumerable<float> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            throw CommonImplementation.SequenceEmpty();
        }

        public static float Min(this EmptyOrderedEnumerable<float> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            throw CommonImplementation.SequenceEmpty();
        }

        public static float Min(this OneItemDefaultEnumerable<float> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return default(float);
        }

        public static float Min(this OneItemDefaultOrderedEnumerable<float> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return default(float);
        }

        public static float Min(this OneItemSpecificEnumerable<float> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return source.Item;
        }

        public static float Min(this OneItemSpecificOrderedEnumerable<float> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return source.Item;
        }

        // long
        public static long Min(this EmptyEnumerable<long> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            throw CommonImplementation.SequenceEmpty();
        }

        public static long Min(this EmptyOrderedEnumerable<long> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            throw CommonImplementation.SequenceEmpty();
        }

        public static long Min(this OneItemDefaultEnumerable<long> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return default(long);
        }

        public static long Min(this OneItemDefaultOrderedEnumerable<long> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return default(long);
        }

        public static long Min(this OneItemSpecificEnumerable<long> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return source.Item;
        }

        public static long Min(this OneItemSpecificOrderedEnumerable<long> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return source.Item;
        }

        // double
        public static double Min(this EmptyEnumerable<double> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            throw CommonImplementation.SequenceEmpty();
        }

        public static double Min(this EmptyOrderedEnumerable<double> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            throw CommonImplementation.SequenceEmpty();
        }

        public static double Min(this OneItemDefaultEnumerable<double> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return default(double);
        }

        public static double Min(this OneItemDefaultOrderedEnumerable<double> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return default(double);
        }

        public static double Min(this OneItemSpecificEnumerable<double> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return source.Item;
        }

        public static double Min(this OneItemSpecificOrderedEnumerable<double> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return source.Item;
        }

        // decimal
        public static decimal Min(this EmptyEnumerable<decimal> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            throw CommonImplementation.SequenceEmpty();
        }

        public static decimal Min(this EmptyOrderedEnumerable<decimal> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            throw CommonImplementation.SequenceEmpty();
        }

        public static decimal Min(this OneItemDefaultEnumerable<decimal> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return default(decimal);
        }

        public static decimal Min(this OneItemDefaultOrderedEnumerable<decimal> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return default(decimal);
        }

        public static decimal Min(this OneItemSpecificEnumerable<decimal> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return source.Item;
        }

        public static decimal Min(this OneItemSpecificOrderedEnumerable<decimal> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return source.Item;
        }

        // int?
        public static int? Min(this EmptyEnumerable<int?> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return null;
        }

        public static int? Min(this EmptyOrderedEnumerable<int?> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return null;
        }

        public static int? Min(this OneItemDefaultEnumerable<int?> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return default(int?);
        }

        public static int? Min(this OneItemDefaultOrderedEnumerable<int?> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return default(int?);
        }

        public static int? Min(this OneItemSpecificEnumerable<int?> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return source.Item;
        }

        public static int? Min(this OneItemSpecificOrderedEnumerable<int?> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return source.Item;
        }

        // float?
        public static float? Min(this EmptyEnumerable<float?> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return null;
        }

        public static float? Min(this EmptyOrderedEnumerable<float?> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return null;
        }

        public static float? Min(this OneItemDefaultEnumerable<float?> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return default(float?);
        }

        public static float? Min(this OneItemDefaultOrderedEnumerable<float?> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return default(float?);
        }

        public static float? Min(this OneItemSpecificEnumerable<float?> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return source.Item;
        }

        public static float? Min(this OneItemSpecificOrderedEnumerable<float?> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return source.Item;
        }

        // long?
        public static long? Min(this EmptyEnumerable<long?> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return null;
        }

        public static long? Min(this EmptyOrderedEnumerable<long?> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return null;
        }

        public static long? Min(this OneItemDefaultEnumerable<long?> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return default(long?);
        }

        public static long? Min(this OneItemDefaultOrderedEnumerable<long?> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return default(long?);
        }

        public static long? Min(this OneItemSpecificEnumerable<long?> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return source.Item;
        }

        public static long? Min(this OneItemSpecificOrderedEnumerable<long?> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return source.Item;
        }

        // double?
        public static double? Min(this EmptyEnumerable<double?> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return null;
        }

        public static double? Min(this EmptyOrderedEnumerable<double?> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return null;
        }

        public static double? Min(this OneItemDefaultEnumerable<double?> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return default(long?);
        }

        public static double? Min(this OneItemDefaultOrderedEnumerable<double?> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return default(double?);
        }

        public static double? Min(this OneItemSpecificEnumerable<double?> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return source.Item;
        }

        public static double? Min(this OneItemSpecificOrderedEnumerable<double?> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return source.Item;
        }

        // decimal?
        public static decimal? Min(this EmptyEnumerable<decimal?> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return null;
        }

        public static decimal? Min(this EmptyOrderedEnumerable<decimal?> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return null;
        }

        public static decimal? Min(this OneItemDefaultEnumerable<decimal?> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return default(long?);
        }

        public static decimal? Min(this OneItemDefaultOrderedEnumerable<decimal?> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return default(decimal?);
        }

        public static decimal? Min(this OneItemSpecificEnumerable<decimal?> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return source.Item;
        }

        public static decimal? Min(this OneItemSpecificOrderedEnumerable<decimal?> source)
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return source.Item;
        }
    }
}
