using LinqAF.Config;
using LinqAF.Impl;
using System;
using System.Collections.Generic;

namespace LinqAF
{
    partial struct EmptyEnumerable<TItem>
    {
        public OneItemDefaultEnumerable<TItem> DefaultIfEmpty()
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");

            return OneItemDefaultEnumerable<TItem>.Instance;
        }

        public OneItemSpecificEnumerable<TItem> DefaultIfEmpty(TItem item)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");

            return new OneItemSpecificEnumerable<TItem>(Enumerable.OneItemSigil, item);
        }

        public EmptyEnumerable<TItem> Where(Func<TItem, bool> predicate)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");
            if (predicate == null) throw CommonImplementation.ArgumentNull(nameof(predicate));

            return EmptyCache<TItem>.Empty;
        }

        public EmptyEnumerable<TItem> Where(Func<TItem, int, bool> predicate)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");
            if (predicate == null) throw CommonImplementation.ArgumentNull(nameof(predicate));

            return EmptyCache<TItem>.Empty;
        }

        public EmptyEnumerable<TSelect_OutItem> Select<TSelect_OutItem>(Func<TItem, TSelect_OutItem> selector)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");
            if (selector == null) throw CommonImplementation.ArgumentNull(nameof(selector));

            return EmptyCache<TSelect_OutItem>.Empty;
        }

        public EmptyEnumerable<TSelect_OutItem> Select<TSelect_OutItem>(Func<TItem, int, TSelect_OutItem> selector)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");
            if (selector == null) throw CommonImplementation.ArgumentNull(nameof(selector));

            return EmptyCache<TSelect_OutItem>.Empty;
        }

        public int Count()
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");

            return 0;
        }

        public int Count(Func<TItem, bool> predicate)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");
            if (predicate == null) throw CommonImplementation.ArgumentNull(nameof(predicate));

            return 0;
        }

        public long LongCount()
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");

            return 0;
        }

        public long LongCount(Func<TItem, bool> predicate)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");
            if (predicate == null) throw CommonImplementation.ArgumentNull(nameof(predicate));

            return 0;
        }

        public bool Any()
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");

            return false;
        }

        public bool Any(Func<TItem, bool> predicate)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");
            if (predicate == null) throw CommonImplementation.ArgumentNull(nameof(predicate));

            return false;
        }

        public bool All(Func<TItem, bool> predicate)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");
            if (predicate == null) throw CommonImplementation.ArgumentNull(nameof(predicate));

            return true;
        }
        
        public TItem First()
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");
            throw CommonImplementation.SequenceEmpty();
        }

        public TItem First(Func<TItem, bool> predicate)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");
            if (predicate == null) throw CommonImplementation.ArgumentNull(nameof(predicate));

            throw CommonImplementation.SequenceEmpty();
        }

        public TItem FirstOrDefault()
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");
            return default(TItem);
        }

        public TItem FirstOrDefault(Func<TItem, bool> predicate)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");
            if (predicate == null) throw CommonImplementation.ArgumentNull(nameof(predicate));

            return default(TItem);
        }

        public TItem Single()
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");
            throw CommonImplementation.SequenceEmpty();
        }

        public TItem Single(Func<TItem, bool> predicate)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");
            if (predicate == null) throw CommonImplementation.ArgumentNull(nameof(predicate));

            throw CommonImplementation.SequenceEmpty();
        }

        public TItem SingleOrDefault()
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");
            return default(TItem);
        }

        public TItem SingleOrDefault(Func<TItem, bool> predicate)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");
            if (predicate == null) throw CommonImplementation.ArgumentNull(nameof(predicate));

            return default(TItem);
        }

        public TItem Last()
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");
            throw CommonImplementation.SequenceEmpty();
        }

        public TItem Last(Func<TItem, bool> predicate)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");
            if (predicate == null) throw CommonImplementation.ArgumentNull(nameof(predicate));

            throw CommonImplementation.SequenceEmpty();
        }

        public TItem LastOrDefault()
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");
            return default(TItem);
        }

        public TItem LastOrDefault(Func<TItem, bool> predicate)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");
            if (predicate == null) throw CommonImplementation.ArgumentNull(nameof(predicate));

            return default(TItem);
        }

        public TItem Aggregate(Func<TItem, TItem, TItem> func)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");
            if (func == null) throw CommonImplementation.ArgumentNull(nameof(func));

            throw CommonImplementation.SequenceEmpty();
        }

        public TAggregate_ItemOut Aggregate<TAggregate_ItemOut>(TAggregate_ItemOut seed, Func<TAggregate_ItemOut, TItem, TAggregate_ItemOut> func)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");
            if (func == null) throw CommonImplementation.ArgumentNull(nameof(func));

            return seed;
        }

        public TAggregate_ItemOut Aggregate<TAggregate_ItemMid, TAggregate_ItemOut>(TAggregate_ItemMid seed, Func<TAggregate_ItemMid, TItem, TAggregate_ItemMid> func, Func<TAggregate_ItemMid, TAggregate_ItemOut> resultSelector)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");
            if (func == null) throw CommonImplementation.ArgumentNull(nameof(func));
            if (resultSelector == null) throw CommonImplementation.ArgumentNull(nameof(resultSelector));

            return resultSelector(seed);
        }

        public EmptyEnumerable<TItem> Skip(int count)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");
            return EmptyCache<TItem>.Empty;
        }

        public EmptyEnumerable<TItem> SkipWhile(Func<TItem, bool> predicate)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");
            if (predicate == null) throw CommonImplementation.ArgumentNull(nameof(predicate));

            return EmptyCache<TItem>.Empty;
        }

        public EmptyEnumerable<TItem> SkipWhile(Func<TItem, int, bool> predicate)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");
            if (predicate == null) throw CommonImplementation.ArgumentNull(nameof(predicate));

            return EmptyCache<TItem>.Empty;
        }

        public TItem ElementAt(int index)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");
            throw CommonImplementation.OutOfRange(nameof(index));
        }

        public TItem ElementAtOrDefault(int index)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");
            return default(TItem);
        }

        public bool Contains(TItem value)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");
            return false;
        }

        public bool Contains(TItem value, IEqualityComparer<TItem> comparer)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");
            return false;
        }

        public EmptyEnumerable<TCast_OutItem> Cast<TCast_OutItem>()
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");
            return EmptyCache<TCast_OutItem>.Empty;
        }

        public EmptyEnumerable<TItem> Take(int count)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");
            return EmptyCache<TItem>.Empty;
        }

        public EmptyEnumerable<TItem> TakeWhile(Func<TItem, bool> predicate)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");
            if (predicate == null) throw CommonImplementation.ArgumentNull(nameof(predicate));

            return EmptyCache<TItem>.Empty;
        }

        public EmptyEnumerable<TItem> TakeWhile(Func<TItem, int, bool> predicate)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");
            if (predicate == null) throw CommonImplementation.ArgumentNull(nameof(predicate));

            return EmptyCache<TItem>.Empty;
        }

        public EmptyEnumerable<TOfType_OutItem> OfType<TOfType_OutItem>()
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");
            return EmptyCache<TOfType_OutItem>.Empty;
        }

        public int Sum(Func<TItem, int> selector)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");
            if (selector == null) throw CommonImplementation.ArgumentNull(nameof(selector));

            return 0;
        }

        public int? Sum(Func<TItem, int?> selector)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");
            if (selector == null) throw CommonImplementation.ArgumentNull(nameof(selector));

            return 0;
        }

        public long Sum(Func<TItem, long> selector)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");
            if (selector == null) throw CommonImplementation.ArgumentNull(nameof(selector));

            return 0;
        }

        public long? Sum(Func<TItem, long?> selector)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");
            if (selector == null) throw CommonImplementation.ArgumentNull(nameof(selector));

            return 0;
        }

        public float Sum(Func<TItem, float> selector)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");
            if (selector == null) throw CommonImplementation.ArgumentNull(nameof(selector));

            return 0;
        }

        public float? Sum(Func<TItem, float?> selector)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");
            if (selector == null) throw CommonImplementation.ArgumentNull(nameof(selector));

            return 0;
        }

        public double Sum(Func<TItem, double> selector)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");
            if (selector == null) throw CommonImplementation.ArgumentNull(nameof(selector));

            return 0;
        }

        public double? Sum(Func<TItem, double?> selector)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");
            if (selector == null) throw CommonImplementation.ArgumentNull(nameof(selector));

            return 0;
        }

        public decimal Sum(Func<TItem, decimal> selector)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");
            if (selector == null) throw CommonImplementation.ArgumentNull(nameof(selector));

            return 0;
        }

        public decimal? Sum(Func<TItem, decimal?> selector)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");
            if (selector == null) throw CommonImplementation.ArgumentNull(nameof(selector));

            return 0;
        }

        public TItem Min()
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");
            if (default(TItem) == null) return default(TItem);

            throw CommonImplementation.SequenceEmpty();
        }

        public TMin_Result Min<TMin_Result>(Func<TItem, TMin_Result> selector)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");
            if (selector == null) throw CommonImplementation.ArgumentNull(nameof(selector));

            if (default(TMin_Result) == null) return default(TMin_Result);

            throw CommonImplementation.SequenceEmpty();
        }

        public int Min(Func<TItem, int> selector)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");
            if (selector == null) throw CommonImplementation.ArgumentNull(nameof(selector));

            throw CommonImplementation.SequenceEmpty();
        }

        public int? Min(Func<TItem, int?> selector)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");
            if (selector == null) throw CommonImplementation.ArgumentNull(nameof(selector));

            return null;
        }

        public long Min(Func<TItem, long> selector)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");
            if (selector == null) throw CommonImplementation.ArgumentNull(nameof(selector));

            throw CommonImplementation.SequenceEmpty();
        }

        public long? Min(Func<TItem, long?> selector)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");
            if (selector == null) throw CommonImplementation.ArgumentNull(nameof(selector));

            return null;
        }

        public float Min(Func<TItem, float> selector)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");
            if (selector == null) throw CommonImplementation.ArgumentNull(nameof(selector));

            throw CommonImplementation.SequenceEmpty();
        }

        public float? Min(Func<TItem, float?> selector)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");
            if (selector == null) throw CommonImplementation.ArgumentNull(nameof(selector));

            return null;
        }

        public double Min(Func<TItem, double> selector)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");
            if (selector == null) throw CommonImplementation.ArgumentNull(nameof(selector));

            throw CommonImplementation.SequenceEmpty();
        }

        public double? Min(Func<TItem, double?> selector)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");
            if (selector == null) throw CommonImplementation.ArgumentNull(nameof(selector));

            return null;
        }

        public decimal Min(Func<TItem, decimal> selector)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");
            if (selector == null) throw CommonImplementation.ArgumentNull(nameof(selector));

            throw CommonImplementation.SequenceEmpty();
        }

        public decimal? Min(Func<TItem, decimal?> selector)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");
            if (selector == null) throw CommonImplementation.ArgumentNull(nameof(selector));

            return null;
        }

        public double Average(Func<TItem, int> selector)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");
            if (selector == null) throw CommonImplementation.ArgumentNull(nameof(selector));

            throw CommonImplementation.SequenceEmpty();
        }

        public double? Average(Func<TItem, int?> selector)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");
            if (selector == null) throw CommonImplementation.ArgumentNull(nameof(selector));

            return null;
        }

        public double Average(Func<TItem, long> selector)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");
            if (selector == null) throw CommonImplementation.ArgumentNull(nameof(selector));

            throw CommonImplementation.SequenceEmpty();
        }

        public double? Average(Func<TItem, long?> selector)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");
            if (selector == null) throw CommonImplementation.ArgumentNull(nameof(selector));

            return null;
        }

        public float Average(Func<TItem, float> selector)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");
            if (selector == null) throw CommonImplementation.ArgumentNull(nameof(selector));

            throw CommonImplementation.SequenceEmpty();
        }

        public float? Average(Func<TItem, float?> selector)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");
            if (selector == null) throw CommonImplementation.ArgumentNull(nameof(selector));

            return null;
        }

        public double Average(Func<TItem, double> selector)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");
            if (selector == null) throw CommonImplementation.ArgumentNull(nameof(selector));

            throw CommonImplementation.SequenceEmpty();
        }

        public double? Average(Func<TItem, double?> selector)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");
            if (selector == null) throw CommonImplementation.ArgumentNull(nameof(selector));

            return null;
        }

        public decimal Average(Func<TItem, decimal> selector)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");
            if (selector == null) throw CommonImplementation.ArgumentNull(nameof(selector));

            throw CommonImplementation.SequenceEmpty();
        }

        public decimal? Average(Func<TItem, decimal?> selector)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");
            if (selector == null) throw CommonImplementation.ArgumentNull(nameof(selector));

            return null;
        }

        public TItem Max()
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");

            if (default(TItem) == null) return default(TItem);

            throw CommonImplementation.SequenceEmpty();
        }

        public TMax_Result Max<TMax_Result>(Func<TItem, TMax_Result> selector)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");
            if (selector == null) throw CommonImplementation.ArgumentNull(nameof(selector));

            if (default(TMax_Result) == null) return default(TMax_Result);

            throw CommonImplementation.SequenceEmpty();

        }

        public int Max(Func<TItem, int> selector)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");
            if (selector == null) throw CommonImplementation.ArgumentNull(nameof(selector));

            throw CommonImplementation.SequenceEmpty();
        }

        public int? Max(Func<TItem, int?> selector)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");
            if (selector == null) throw CommonImplementation.ArgumentNull(nameof(selector));

            return null;
        }

        public long Max(Func<TItem, long> selector)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");
            if (selector == null) throw CommonImplementation.ArgumentNull(nameof(selector));

            throw CommonImplementation.SequenceEmpty();
        }

        public long? Max(Func<TItem, long?> selector)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");
            if (selector == null) throw CommonImplementation.ArgumentNull(nameof(selector));

            return null;
        }

        public float Max(Func<TItem, float> selector)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");
            if (selector == null) throw CommonImplementation.ArgumentNull(nameof(selector));

            throw CommonImplementation.SequenceEmpty();
        }

        public float? Max(Func<TItem, float?> selector)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");
            if (selector == null) throw CommonImplementation.ArgumentNull(nameof(selector));

            return null;
        }

        public double Max(Func<TItem, double> selector)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");
            if (selector == null) throw CommonImplementation.ArgumentNull(nameof(selector));

            throw CommonImplementation.SequenceEmpty();
        }

        public double? Max(Func<TItem, double?> selector)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");
            if (selector == null) throw CommonImplementation.ArgumentNull(nameof(selector));

            return null;
        }

        public decimal Max(Func<TItem, decimal> selector)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");
            if (selector == null) throw CommonImplementation.ArgumentNull(nameof(selector));

            throw CommonImplementation.SequenceEmpty();
        }

        public decimal? Max(Func<TItem, decimal?> selector)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");
            if (selector == null) throw CommonImplementation.ArgumentNull(nameof(selector));

            return null;
        }

        public EmptyEnumerable<TItem> Distinct()
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");

            return EmptyCache<TItem>.Empty;
        }

        public EmptyEnumerable<TItem> Distinct(IEqualityComparer<TItem> comparer)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");

            return EmptyCache<TItem>.Empty;
        }

        public List<TItem> ToList()
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");

            return Allocator.Current.GetEmptyList<TItem>(0);
        }

        public TItem[] ToArray()
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");

            return EmptyCache<TItem>.Array;
        }

        public Dictionary<TToDictionary_Key, TItem> ToDictionary<TToDictionary_Key>(Func<TItem, TToDictionary_Key> keySelector)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");
            if (keySelector == null) throw CommonImplementation.ArgumentNull(nameof(keySelector));

            return Allocator.Current.GetEmptyDictionary<TToDictionary_Key, TItem>(0, null);
        }

        public Dictionary<TToDictionary_Key, TToDictionary_Value> ToDictionary<TToDictionary_Key, TToDictionary_Value>(Func<TItem, TToDictionary_Key> keySelector, Func<TItem, TToDictionary_Value> elementSelector)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");
            if (keySelector == null) throw CommonImplementation.ArgumentNull(nameof(keySelector));
            if (elementSelector == null) throw CommonImplementation.ArgumentNull(nameof(elementSelector));

            return Allocator.Current.GetEmptyDictionary<TToDictionary_Key, TToDictionary_Value>(0, null);
        }

        public Dictionary<TToDictionary_Key, TItem> ToDictionary<TToDictionary_Key>(Func<TItem, TToDictionary_Key> keySelector, IEqualityComparer<TToDictionary_Key> comparer)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");
            if (keySelector == null) throw CommonImplementation.ArgumentNull(nameof(keySelector));

            return Allocator.Current.GetEmptyDictionary<TToDictionary_Key, TItem>(0, comparer);
        }

        public Dictionary<TToDictionary_Key, TToDictionary_Value> ToDictionary<TToDictionary_Key, TToDictionary_Value>(Func<TItem, TToDictionary_Key> keySelector, Func<TItem, TToDictionary_Value> elementSelector, IEqualityComparer<TToDictionary_Key> comparer)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");
            if (keySelector == null) throw CommonImplementation.ArgumentNull(nameof(keySelector));
            if (elementSelector == null) throw CommonImplementation.ArgumentNull(nameof(elementSelector));

            return Allocator.Current.GetEmptyDictionary<TToDictionary_Key, TToDictionary_Value>(0, comparer);
        }

        public EmptyEnumerable<TItem> Reverse()
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");

            return EmptyCache<TItem>.Empty;
        }

        public LookupDefaultEnumerable<TToLookup_Key, TItem> ToLookup<TToLookup_Key>(Func<TItem, TToLookup_Key> keySelector)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");
            if (keySelector == null) throw CommonImplementation.ArgumentNull(nameof(keySelector));

            return EmptyCache<TToLookup_Key, TItem>.EmptyLookupDefault;
        }

        public LookupDefaultEnumerable<TToLookup_Key, TItem> ToLookup<TToLookup_Key>(Func<TItem, TToLookup_Key> keySelector, IEqualityComparer<TToLookup_Key> comparer)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");
            if (keySelector == null) throw CommonImplementation.ArgumentNull(nameof(keySelector));

            return EmptyCache<TToLookup_Key, TItem>.EmptyLookupDefault;
        }

        public LookupDefaultEnumerable<TToLookup_Key, TToLookup_Element> ToLookup<TToLookup_Key, TToLookup_Element>(Func<TItem, TToLookup_Key> keySelector, Func<TItem, TToLookup_Element> elementSelector)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");
            if (keySelector == null) throw CommonImplementation.ArgumentNull(nameof(keySelector));
            if (elementSelector == null) throw CommonImplementation.ArgumentNull(nameof(elementSelector));

            return EmptyCache<TToLookup_Key, TToLookup_Element>.EmptyLookupDefault;
        }

        public LookupDefaultEnumerable<TToLookup_Key, TToLookup_Element> ToLookup<TToLookup_Key, TToLookup_Element>(Func<TItem, TToLookup_Key> keySelector, Func<TItem, TToLookup_Element> elementSelector, IEqualityComparer<TToLookup_Key> comparer)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");
            if (keySelector == null) throw CommonImplementation.ArgumentNull(nameof(keySelector));
            if (elementSelector == null) throw CommonImplementation.ArgumentNull(nameof(elementSelector));

            return EmptyCache<TToLookup_Key, TToLookup_Element>.EmptyLookupDefault;
        }

        public EmptyOrderedEnumerable<TItem> OrderBy<TOrderBy_Key>(Func<TItem, TOrderBy_Key> keySelector)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");
            if (keySelector == null) throw CommonImplementation.ArgumentNull(nameof(keySelector));

            return EmptyCache<TItem>.EmptyOrdered;
        }

        public EmptyOrderedEnumerable<TItem> OrderBy<TOrderBy_Key>(Func<TItem, TOrderBy_Key> keySelector, IComparer<TOrderBy_Key> comparer)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");
            if (keySelector == null) throw CommonImplementation.ArgumentNull(nameof(keySelector));

            return EmptyCache<TItem>.EmptyOrdered;
        }

        public EmptyOrderedEnumerable<TItem> OrderByDescending<TOrderBy_Key>(Func<TItem, TOrderBy_Key> keySelector)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");
            if (keySelector == null) throw CommonImplementation.ArgumentNull(nameof(keySelector));

            return EmptyCache<TItem>.EmptyOrdered;
        }

        public EmptyOrderedEnumerable<TItem> OrderByDescending<TOrderBy_Key>(Func<TItem, TOrderBy_Key> keySelector, IComparer<TOrderBy_Key> comparer)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");
            if (keySelector == null) throw CommonImplementation.ArgumentNull(nameof(keySelector));

            return EmptyCache<TItem>.EmptyOrdered;
        }

        public IEnumerable<TItem> AsEnumerable()
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");

            return System.Linq.Enumerable.Empty<TItem>();
        }
    }
}
