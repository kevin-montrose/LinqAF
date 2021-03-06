﻿using LinqAF.Config;
using LinqAF.Impl;
using System;
using System.Collections.Generic;

namespace LinqAF
{
    abstract partial class BuiltInExtensionMethodsBase :
        ExtensionMethodsBase
    {
        // Aggregate
        public TItem Aggregate<TItem>(BuiltInEnumerable<TItem> source, Func<TItem, TItem, TItem> func)
        {
            var bridge = Bridge(source, nameof(source));
            if (func == null) throw CommonImplementation.ArgumentNull(nameof(func));

            return CommonImplementation.AggregateImpl<TItem, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>>(RefLocal(bridge), func);
        }

        public TItemOut Aggregate<TItem, TItemOut>(BuiltInEnumerable<TItem> source, TItemOut seed, Func<TItemOut, TItem, TItemOut> func)
        {
            var bridge = Bridge(source, nameof(source));
            if (func == null) throw CommonImplementation.ArgumentNull(nameof(func));

            return CommonImplementation.AggregateImpl<TItem, TItemOut, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>>(RefLocal(bridge), seed, func);
        }

        public TItemOut Aggregate<TItem, TItemMid, TItemOut>(BuiltInEnumerable<TItem> source, TItemMid seed, Func<TItemMid, TItem, TItemMid> func, Func<TItemMid, TItemOut> resultSelector)
        {
            var bridge = Bridge(source, nameof(source));
            if (func == null) throw CommonImplementation.ArgumentNull(nameof(func));
            if (resultSelector == null) throw CommonImplementation.ArgumentNull(nameof(resultSelector));

            return CommonImplementation.AggregateImpl<TItem, TItemMid, TItemOut, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>>(RefLocal(bridge), seed, func, resultSelector);
        }

        // All
        public bool All<TItem>(BuiltInEnumerable<TItem> source, Func<TItem, bool> predicate)
        {
            var bridge = Bridge(source, nameof(source));
            if (predicate == null) throw CommonImplementation.ArgumentNull(nameof(predicate));

            return CommonImplementation.AllImpl<TItem, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>>(RefLocal(bridge), predicate);
        }

        // Any
        public bool Any<TItem>(BuiltInEnumerable<TItem> source)
        {
            var bridge = Bridge(source, nameof(source));

            return CommonImplementation.AnyImpl<TItem, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>>(RefLocal(bridge));
        }

        public bool Any<TItem>(BuiltInEnumerable<TItem> source, Func<TItem, bool> predicate)
        {
            var bridge = Bridge(source, nameof(source));
            if (predicate == null) throw CommonImplementation.ArgumentNull(nameof(predicate));

            return CommonImplementation.AnyImpl<TItem, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>>(RefLocal(bridge), predicate);
        }

        // Append
        public AppendEnumerable<TItem, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>>  Append<TItem>(BuiltInEnumerable<TItem> source, TItem element)
        {
            var bridge = Bridge(source, nameof(source));

            return CommonImplementation.AppendImpl<TItem, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>>(RefLocal(bridge), element);
        }

        // AsEnumerable
        public IEnumerable<TItem> AsEnumerable<TItem>(BuiltInEnumerable<TItem> source)
        {
            return (FakeEnumerable<TItem>)source;
        }
        
        // Contains
        public bool Contains<TItem>(BuiltInEnumerable<TItem> source, TItem value)
        {
#pragma warning disable CS0458
            var asCollection = source as ICollection<TItem>;
#pragma warning restore CS0458
            if (asCollection != null)
            {
                return asCollection.Contains(value);
            }

            var bridge = Bridge(source, nameof(source));

            return CommonImplementation.ContainsImpl<TItem, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>>(RefLocal(bridge), value);
        }

        public bool Contains<TItem>(BuiltInEnumerable<TItem> source, TItem value, IEqualityComparer<TItem> comparer)
        {
            var bridge = Bridge(source, nameof(source));

            if (comparer == null)
            {
                return CommonImplementation.ContainsImpl<TItem, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>>(RefLocal(bridge), value);
            }

            return CommonImplementation.ContainsImpl<TItem, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>>(RefLocal(bridge), value, comparer);
        }

        // Count
        public int Count<TItem>(BuiltInEnumerable<TItem> source)
        {
            var bridge = Bridge(source, nameof(source));

            return CommonImplementation.CountImpl<TItem, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>>(RefLocal(bridge));
        }

        public int Count<TItem>(BuiltInEnumerable<TItem> source, Func<TItem, bool> predicate)
        {
            var bridge = Bridge(source, nameof(source));
            if (predicate == null) throw CommonImplementation.ArgumentNull(nameof(predicate));

            return CommonImplementation.CountImpl<TItem, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>>(RefLocal(bridge), predicate);
        }

        public long LongCount<TItem>(BuiltInEnumerable<TItem> source)
        {
            var bridge = Bridge(source, nameof(source));

            return CommonImplementation.LongCountImpl<TItem, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>>(RefLocal(bridge));
        }

        public long LongCount<TItem>(BuiltInEnumerable<TItem> source, Func<TItem, bool> predicate)
        {
            var bridge = Bridge(source, nameof(source));
            if (predicate == null) throw CommonImplementation.ArgumentNull(nameof(predicate));

            return CommonImplementation.LongCountImpl<TItem, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>>(RefLocal(bridge), predicate);
        }

        // DefaultIfEmpty
        public DefaultIfEmptyDefaultEnumerable<TItem, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>> DefaultIfEmpty<TItem>(BuiltInEnumerable<TItem> source)
        {
            var bridge = Bridge(source, nameof(source));

            return CommonImplementation.DefaultIfEmptyImpl<TItem, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>>(RefLocal(bridge));
        }

        public DefaultIfEmptySpecificEnumerable<TItem, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>> DefaultIfEmpty<TItem>(BuiltInEnumerable<TItem> source, TItem item)
        {
            var bridge = Bridge(source, nameof(source));

            return CommonImplementation.DefaultIfEmptyImpl<TItem, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>>(RefLocal(bridge), item);
        }

        // Distinct
        public DistinctDefaultEnumerable<TItem, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>> Distinct<TItem>(BuiltInEnumerable<TItem> source)
        {
            var bridge = Bridge(source, nameof(source));

            return CommonImplementation.DistinctImpl<TItem, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>>(RefLocal(bridge));
        }

        public DistinctSpecificEnumerable<TItem, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>> Distinct<TItem>(BuiltInEnumerable<TItem> source, IEqualityComparer<TItem> comparer)
        {
            var bridge = Bridge(source, nameof(source));

            return CommonImplementation.DistinctImpl<TItem, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>>(RefLocal(bridge), comparer);
        }

        // ElementAt
        public TItem ElementAt<TItem>(BuiltInEnumerable<TItem> source, int index)
        {
            var asIList = source as IList<TItem>;
            if(asIList != null)
            {
                if(index < 0 || index >= asIList.Count) throw CommonImplementation.OutOfRange(nameof(index));

                return asIList[index];
            }

            var bridge = Bridge(source, nameof(source));

            return CommonImplementation.ElementAtImpl<TItem, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>>(RefLocal(bridge), index);
        }

        public TItem ElementAtOrDefault<TItem>(BuiltInEnumerable<TItem> source, int index)
        {
            var asIList = source as IList<TItem>;
            if (asIList != null)
            {
                if (index < 0 || index >= asIList.Count) return default(TItem);

                return asIList[index];
            }

            var bridge = Bridge(source, nameof(source));

            return CommonImplementation.ElementAtOrDefaultImpl<TItem, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>>(RefLocal(bridge), index);
        }

        // First
        public TItem First<TItem>(BuiltInEnumerable<TItem> source)
        {
            var bridge = Bridge(source, nameof(source));

            return CommonImplementation.FirstImpl<TItem, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>>(RefLocal(bridge));
        }

        public TItem First<TItem>(BuiltInEnumerable<TItem> source, Func<TItem, bool> predicate)
        {
            var bridge = Bridge(source, nameof(source));
            if (predicate == null) throw CommonImplementation.ArgumentNull(nameof(predicate));

            return CommonImplementation.FirstImpl<TItem, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>>(RefLocal(bridge), predicate);
        }

        public TItem FirstOrDefault<TItem>(BuiltInEnumerable<TItem> source)
        {
            var bridge = Bridge(source, nameof(source));

            return CommonImplementation.FirstOrDefaultImpl<TItem, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>>(RefLocal(bridge));
        }

        public TItem FirstOrDefault<TItem>(BuiltInEnumerable<TItem> source, Func<TItem, bool> predicate)
        {
            var bridge = Bridge(source, nameof(source));
            if (predicate == null) throw CommonImplementation.ArgumentNull(nameof(predicate));

            return CommonImplementation.FirstOrDefaultImpl<TItem, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>>(RefLocal(bridge), predicate);
        }

        // GroupBy
        public GroupByDefaultEnumerable<TItem, TKey, TItem, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>> GroupBy<TItem, TKey>(BuiltInEnumerable<TItem> source, Func<TItem, TKey> keySelector)
        {
            var bridge = Bridge(source, nameof(source));
            if (keySelector == null) throw CommonImplementation.ArgumentNull(nameof(keySelector));
            Func<TItem, TItem> elementSelector = _ => _;

            return CommonImplementation.GroupByImpl<TItem, TKey, TItem, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>>(RefLocal(bridge), keySelector, elementSelector);
        }

        public GroupBySpecificEnumerable<TItem, TKey, TItem, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>> GroupBy<TItem, TKey>(BuiltInEnumerable<TItem> source, Func<TItem, TKey> keySelector, IEqualityComparer<TKey> comparer)
        {
            var bridge = Bridge(source, nameof(source));
            if (keySelector == null) throw CommonImplementation.ArgumentNull(nameof(keySelector));
            Func<TItem, TItem> elementSelector = _ => _;

            return CommonImplementation.GroupByImpl<TItem, TKey, TItem, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>>(RefLocal(bridge), keySelector, elementSelector, comparer);
        }

        public GroupByDefaultEnumerable<TItem, TKey, TElement, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>> GroupBy<TItem, TKey, TElement>(BuiltInEnumerable<TItem> source, Func<TItem, TKey> keySelector, Func<TItem, TElement> elementSelector)
        {
            var bridge = Bridge(source, nameof(source));
            if (keySelector == null) throw CommonImplementation.ArgumentNull(nameof(keySelector));
            if (elementSelector == null) throw CommonImplementation.ArgumentNull(nameof(elementSelector));

            return CommonImplementation.GroupByImpl<TItem, TKey, TElement, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>>(RefLocal(bridge), keySelector, elementSelector);
        }

        public GroupBySpecificEnumerable<TItem, TKey, TElement, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>> GroupBy<TItem, TKey, TElement>(BuiltInEnumerable<TItem> source, Func<TItem, TKey> keySelector, Func<TItem, TElement> elementSelector, IEqualityComparer<TKey> comparer)
        {
            var bridge = Bridge(source, nameof(source));
            if (keySelector == null) throw CommonImplementation.ArgumentNull(nameof(keySelector));
            if (elementSelector == null) throw CommonImplementation.ArgumentNull(nameof(elementSelector));

            return CommonImplementation.GroupByImpl<TItem, TKey, TElement, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>>(RefLocal(bridge), keySelector, elementSelector, comparer);
        }

        public GroupByCollectionDefaultEnumerable<TItem, TKey, TItem, TResult, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>> GroupBy<TItem, TKey, TResult>(BuiltInEnumerable<TItem> source, Func<TItem, TKey> keySelector, Func<TKey, GroupedEnumerable<TKey, TItem>, TResult> resultSelector)
        {
            var bridge = Bridge(source, nameof(source));
            if (keySelector == null) throw CommonImplementation.ArgumentNull(nameof(keySelector));
            if (resultSelector == null) throw CommonImplementation.ArgumentNull(nameof(resultSelector));
            Func<TItem, TItem> elementSelector = _ => _;


            return CommonImplementation.GroupByImpl<TItem, TKey, TItem, TResult, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>>(RefLocal(bridge), keySelector, elementSelector, resultSelector);
        }

        public GroupByCollectionSpecificEnumerable<TItem, TKey, TItem, TResult, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>> GroupBy<TItem, TKey, TResult>(BuiltInEnumerable<TItem> source, Func<TItem, TKey> keySelector, Func<TKey, GroupedEnumerable<TKey, TItem>, TResult> resultSelector, IEqualityComparer<TKey> comparer)
        {
            var bridge = Bridge(source, nameof(source));
            if (keySelector == null) throw CommonImplementation.ArgumentNull(nameof(keySelector));
            if (resultSelector == null) throw CommonImplementation.ArgumentNull(nameof(resultSelector));
            Func<TItem, TItem> elementSelector = _ => _;


            return CommonImplementation.GroupByImpl<TItem, TKey, TItem, TResult, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>>(RefLocal(bridge), keySelector, elementSelector, resultSelector, comparer);
        }

        public GroupByCollectionDefaultEnumerable<TItem, TKey, TElement, TResult, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>> GroupBy<TItem, TKey, TElement, TResult>(BuiltInEnumerable<TItem> source, Func<TItem, TKey> keySelector, Func<TItem, TElement> elementSelector, Func<TKey, GroupedEnumerable<TKey, TElement>, TResult> resultSelector)
        {
            var bridge = Bridge(source, nameof(source));
            if (keySelector == null) throw CommonImplementation.ArgumentNull(nameof(keySelector));
            if (elementSelector == null) throw CommonImplementation.ArgumentNull(nameof(elementSelector));
            if (resultSelector == null) throw CommonImplementation.ArgumentNull(nameof(resultSelector));

            return CommonImplementation.GroupByImpl<TItem, TKey, TElement, TResult, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>>(RefLocal(bridge), keySelector, elementSelector, resultSelector);
        }

        public GroupByCollectionSpecificEnumerable<TItem, TKey, TElement, TResult, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>> GroupBy<TItem, TKey, TElement, TResult>(BuiltInEnumerable<TItem> source, Func<TItem, TKey> keySelector, Func<TItem, TElement> elementSelector, Func<TKey, GroupedEnumerable<TKey, TElement>, TResult> resultSelector, IEqualityComparer<TKey> comparer)
        {
            var bridge = Bridge(source, nameof(source));
            if (keySelector == null) throw CommonImplementation.ArgumentNull(nameof(keySelector));
            if (elementSelector == null) throw CommonImplementation.ArgumentNull(nameof(elementSelector));
            if (resultSelector == null) throw CommonImplementation.ArgumentNull(nameof(resultSelector));

            return CommonImplementation.GroupByImpl<TItem, TKey, TElement, TResult, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>>(RefLocal(bridge), keySelector, elementSelector, resultSelector, comparer);
        }
        
        // Last
        public TItem Last<TItem>(BuiltInEnumerable<TItem> source)
        {
            var asIList = source as IList<TItem>;
            if (asIList != null)
            {
                if (asIList.Count == 0)
                {
                    throw CommonImplementation.SequenceEmpty();
                }

                return asIList[asIList.Count - 1];
            }

            var bridge = Bridge(source, nameof(source));

            return CommonImplementation.LastImpl<TItem, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>>(RefLocal(bridge));
        }

        public TItem Last<TItem>(BuiltInEnumerable<TItem> source, Func<TItem, bool> predicate)
        {
            var bridge = Bridge(source, nameof(source));
            if (predicate == null) throw CommonImplementation.ArgumentNull(nameof(predicate));

            return CommonImplementation.LastImpl<TItem, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>>(RefLocal(bridge), predicate);
        }

        public TItem LastOrDefault<TItem>(BuiltInEnumerable<TItem> source)
        {
            var asIList = source as IList<TItem>;
            if (asIList != null)
            {
                if (asIList.Count == 0)
                {
                    return default(TItem);
                }

                return asIList[asIList.Count - 1];
            }

            var bridge = Bridge(source, nameof(source));

            return CommonImplementation.LastOrDefaultImpl<TItem, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>>(RefLocal(bridge));
        }

        public TItem LastOrDefault<TItem>(BuiltInEnumerable<TItem> source, Func<TItem, bool> predicate)
        {
            var bridge = Bridge(source, nameof(source));
            if (predicate == null) throw CommonImplementation.ArgumentNull(nameof(predicate));

            return CommonImplementation.LastOrDefaultImpl<TItem, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>>(RefLocal(bridge), predicate);
        }

        // Max
        
        public int Max(BuiltInEnumerable<int> source)
        {
            var bridge = Bridge(source, nameof(source));
            return CommonImplementation.MaxIntImpl<BuiltInEnumerable<int>, BuiltInEnumerator<int>>(RefLocal(bridge));
        }

        public int? Max(BuiltInEnumerable<int?> source)
        {
            var bridge = Bridge(source, nameof(source));
            return CommonImplementation.MaxNullableIntImpl<BuiltInEnumerable<int?>, BuiltInEnumerator<int?>>(RefLocal(bridge));
        }

        public long Max(BuiltInEnumerable<long> source)
        {
            var bridge = Bridge(source, nameof(source));
            return CommonImplementation.MaxLongImpl<BuiltInEnumerable<long>, BuiltInEnumerator<long>>(RefLocal(bridge));
        }

        public long? Max(BuiltInEnumerable<long?> source)
        {
            var bridge = Bridge(source, nameof(source));
            return CommonImplementation.MaxNullableLongImpl<BuiltInEnumerable<long?>, BuiltInEnumerator<long?>>(RefLocal(bridge));
        }

        public float Max(BuiltInEnumerable<float> source)
        {
            var bridge = Bridge(source, nameof(source));
            return CommonImplementation.MaxFloatImpl<BuiltInEnumerable<float>, BuiltInEnumerator<float>>(RefLocal(bridge));
        }

        public float? Max(BuiltInEnumerable<float?> source)
        {
            var bridge = Bridge(source, nameof(source));
            return CommonImplementation.MaxNullableFloatImpl<BuiltInEnumerable<float?>, BuiltInEnumerator<float?>>(RefLocal(bridge));
        }

        public double Max(BuiltInEnumerable<double> source)
        {
            var bridge = Bridge(source, nameof(source));
            return CommonImplementation.MaxDoubleImpl<BuiltInEnumerable<double>, BuiltInEnumerator<double>>(RefLocal(bridge));
        }

        public double? Max(BuiltInEnumerable<double?> source)
        {
            var bridge = Bridge(source, nameof(source));
            return CommonImplementation.MaxNullableDoubleImpl<BuiltInEnumerable<double?>, BuiltInEnumerator<double?>>(RefLocal(bridge));
        }

        public decimal Max(BuiltInEnumerable<decimal> source)
        {
            var bridge = Bridge(source, nameof(source));
            return CommonImplementation.MaxDecimalImpl<BuiltInEnumerable<decimal>, BuiltInEnumerator<decimal>>(RefLocal(bridge));
        }

        public decimal? Max(BuiltInEnumerable<decimal?> source)
        {
            var bridge = Bridge(source, nameof(source));
            return CommonImplementation.MaxNullableDecimalImpl<BuiltInEnumerable<decimal?>, BuiltInEnumerator<decimal?>>(RefLocal(bridge));
        }

        public TItem Max<TItem>(BuiltInEnumerable<TItem> source)
        {
            var bridge = Bridge(source, nameof(source));

            return CommonImplementation.MaxComparableImpl<TItem, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>>(RefLocal(bridge));
        }

        public long Max<TItem>(BuiltInEnumerable<TItem> source, Func<TItem, long> selector)
        {
            var bridge = Bridge(source, nameof(source));
            if (selector == null) throw CommonImplementation.ArgumentNull(nameof(selector));

            return CommonImplementation.MaxSelectorImpl<TItem, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>>(RefLocal(bridge), selector);
        }

        public float Max<TItem>(BuiltInEnumerable<TItem> source, Func<TItem, float> selector)
        {
            var bridge = Bridge(source, nameof(source));
            if (selector == null) throw CommonImplementation.ArgumentNull(nameof(selector));

            return CommonImplementation.MaxSelectorImpl<TItem, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>>(RefLocal(bridge), selector);
        }

        public double Max<TItem>(BuiltInEnumerable<TItem> source, Func<TItem, double> selector)
        {
            var bridge = Bridge(source, nameof(source));
            if (selector == null) throw CommonImplementation.ArgumentNull(nameof(selector));

            return CommonImplementation.MaxSelectorImpl<TItem, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>>(RefLocal(bridge), selector);
        }

        public decimal Max<TItem>(BuiltInEnumerable<TItem> source, Func<TItem, decimal> selector)
        {
            var bridge = Bridge(source, nameof(source));
            if (selector == null) throw CommonImplementation.ArgumentNull(nameof(selector));

            return CommonImplementation.MaxSelectorImpl<TItem, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>>(RefLocal(bridge), selector);
        }

        public decimal? Max<TItem>(BuiltInEnumerable<TItem> source, Func<TItem, decimal?> selector)
        {
            var bridge = Bridge(source, nameof(source));
            if (selector == null) throw CommonImplementation.ArgumentNull(nameof(selector));

            return CommonImplementation.MaxSelectorImpl<TItem, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>>(RefLocal(bridge), selector);
        }

        public double? Max<TItem>(BuiltInEnumerable<TItem> source, Func<TItem, double?> selector)
        {
            var bridge = Bridge(source, nameof(source));
            if (selector == null) throw CommonImplementation.ArgumentNull(nameof(selector));

            return CommonImplementation.MaxSelectorImpl<TItem, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>>(RefLocal(bridge), selector);
        }

        public float? Max<TItem>(BuiltInEnumerable<TItem> source, Func<TItem, float?> selector)
        {
            var bridge = Bridge(source, nameof(source));
            if (selector == null) throw CommonImplementation.ArgumentNull(nameof(selector));

            return CommonImplementation.MaxSelectorImpl<TItem, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>>(RefLocal(bridge), selector);
        }

        public long? Max<TItem>(BuiltInEnumerable<TItem> source, Func<TItem, long?> selector)
        {
            var bridge = Bridge(source, nameof(source));
            if (selector == null) throw CommonImplementation.ArgumentNull(nameof(selector));

            return CommonImplementation.MaxSelectorImpl<TItem, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>>(RefLocal(bridge), selector);
        }

        public int? Max<TItem>(BuiltInEnumerable<TItem> source, Func<TItem, int?> selector)
        {
            var bridge = Bridge(source, nameof(source));
            if (selector == null) throw CommonImplementation.ArgumentNull(nameof(selector));

            return CommonImplementation.MaxSelectorImpl<TItem, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>>(RefLocal(bridge), selector);
        }

        public int Max<TItem>(BuiltInEnumerable<TItem> source, Func<TItem, int> selector)
        {
            var bridge = Bridge(source, nameof(source));
            if (selector == null) throw CommonImplementation.ArgumentNull(nameof(selector));

            return CommonImplementation.MaxSelectorImpl<TItem, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>>(RefLocal(bridge), selector);
        }

        public TResult Max<TItem, TResult>(BuiltInEnumerable<TItem> source, Func<TItem, TResult> selector)
        {
            var bridge = Bridge(source, nameof(source));
            if (selector == null) throw CommonImplementation.ArgumentNull(nameof(selector));

            return CommonImplementation.MaxProjectedComparableImpl<TItem, TResult, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>>(RefLocal(bridge), selector);
        }

        // Min

        public int Min(BuiltInEnumerable<int> source)
        {
            var bridge = Bridge(source, nameof(source));
            return CommonImplementation.MinIntImpl<BuiltInEnumerable<int>, BuiltInEnumerator<int>>(RefLocal(bridge));
        }

        public int? Min(BuiltInEnumerable<int?> source)
        {
            var bridge = Bridge(source, nameof(source));
            return CommonImplementation.MinNullableIntImpl<BuiltInEnumerable<int?>, BuiltInEnumerator<int?>>(RefLocal(bridge));
        }

        public long Min(BuiltInEnumerable<long> source)
        {
            var bridge = Bridge(source, nameof(source));
            return CommonImplementation.MinLongImpl<BuiltInEnumerable<long>, BuiltInEnumerator<long>>(RefLocal(bridge));
        }

        public long? Min(BuiltInEnumerable<long?> source)
        {
            var bridge = Bridge(source, nameof(source));
            return CommonImplementation.MinNullableLongImpl<BuiltInEnumerable<long?>, BuiltInEnumerator<long?>>(RefLocal(bridge));
        }

        public float Min(BuiltInEnumerable<float> source)
        {
            var bridge = Bridge(source, nameof(source));
            return CommonImplementation.MinFloatImpl<BuiltInEnumerable<float>, BuiltInEnumerator<float>>(RefLocal(bridge));
        }

        public float? Min(BuiltInEnumerable<float?> source)
        {
            var bridge = Bridge(source, nameof(source));
            return CommonImplementation.MinNullableFloatImpl<BuiltInEnumerable<float?>, BuiltInEnumerator<float?>>(RefLocal(bridge));
        }

        public double Min(BuiltInEnumerable<double> source)
        {
            var bridge = Bridge(source, nameof(source));
            return CommonImplementation.MinDoubleImpl<BuiltInEnumerable<double>, BuiltInEnumerator<double>>(RefLocal(bridge));
        }

        public double? Min(BuiltInEnumerable<double?> source)
        {
            var bridge = Bridge(source, nameof(source));
            return CommonImplementation.MinNullableDoubleImpl<BuiltInEnumerable<double?>, BuiltInEnumerator<double?>>(RefLocal(bridge));
        }

        public decimal Min(BuiltInEnumerable<decimal> source)
        {
            var bridge = Bridge(source, nameof(source));
            return CommonImplementation.MinDecimalImpl<BuiltInEnumerable<decimal>, BuiltInEnumerator<decimal>>(RefLocal(bridge));
        }

        public decimal? Min(BuiltInEnumerable<decimal?> source)
        {
            var bridge = Bridge(source, nameof(source));
            return CommonImplementation.MinNullableDecimalImpl<BuiltInEnumerable<decimal?>, BuiltInEnumerator<decimal?>>(RefLocal(bridge));
        }

        public TItem Min<TItem>(BuiltInEnumerable<TItem> source)
        {
            var bridge = Bridge(source, nameof(source));

            return CommonImplementation.MinComparableImpl<TItem, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>>(RefLocal(bridge));
        }

        public long Min<TItem>(BuiltInEnumerable<TItem> source, Func<TItem, long> selector)
        {
            var bridge = Bridge(source, nameof(source));
            if (selector == null) throw CommonImplementation.ArgumentNull(nameof(selector));

            return CommonImplementation.MinSelectorImpl<TItem, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>>(RefLocal(bridge), selector);
        }

        public float Min<TItem>(BuiltInEnumerable<TItem> source, Func<TItem, float> selector)
        {
            var bridge = Bridge(source, nameof(source));
            if (selector == null) throw CommonImplementation.ArgumentNull(nameof(selector));

            return CommonImplementation.MinSelectorImpl<TItem, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>>(RefLocal(bridge), selector);
        }

        public double Min<TItem>(BuiltInEnumerable<TItem> source, Func<TItem, double> selector)
        {
            var bridge = Bridge(source, nameof(source));
            if (selector == null) throw CommonImplementation.ArgumentNull(nameof(selector));

            return CommonImplementation.MinSelectorImpl<TItem, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>>(RefLocal(bridge), selector);
        }

        public decimal Min<TItem>(BuiltInEnumerable<TItem> source, Func<TItem, decimal> selector)
        {
            var bridge = Bridge(source, nameof(source));
            if (selector == null) throw CommonImplementation.ArgumentNull(nameof(selector));

            return CommonImplementation.MinSelectorImpl<TItem, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>>(RefLocal(bridge), selector);
        }

        public decimal? Min<TItem>(BuiltInEnumerable<TItem> source, Func<TItem, decimal?> selector)
        {
            var bridge = Bridge(source, nameof(source));
            if (selector == null) throw CommonImplementation.ArgumentNull(nameof(selector));

            return CommonImplementation.MinSelectorImpl<TItem, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>>(RefLocal(bridge), selector);
        }

        public double? Min<TItem>(BuiltInEnumerable<TItem> source, Func<TItem, double?> selector)
        {
            var bridge = Bridge(source, nameof(source));
            if (selector == null) throw CommonImplementation.ArgumentNull(nameof(selector));

            return CommonImplementation.MinSelectorImpl<TItem, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>>(RefLocal(bridge), selector);
        }

        public float? Min<TItem>(BuiltInEnumerable<TItem> source, Func<TItem, float?> selector)
        {
            var bridge = Bridge(source, nameof(source));
            if (selector == null) throw CommonImplementation.ArgumentNull(nameof(selector));

            return CommonImplementation.MinSelectorImpl<TItem, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>>(RefLocal(bridge), selector);
        }

        public long? Min<TItem>(BuiltInEnumerable<TItem> source, Func<TItem, long?> selector)
        {
            var bridge = Bridge(source, nameof(source));
            if (selector == null) throw CommonImplementation.ArgumentNull(nameof(selector));

            return CommonImplementation.MinSelectorImpl<TItem, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>>(RefLocal(bridge), selector);
        }

        public int? Min<TItem>(BuiltInEnumerable<TItem> source, Func<TItem, int?> selector)
        {
            var bridge = Bridge(source, nameof(source));
            if (selector == null) throw CommonImplementation.ArgumentNull(nameof(selector));

            return CommonImplementation.MinSelectorImpl<TItem, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>>(RefLocal(bridge), selector);
        }

        public int Min<TItem>(BuiltInEnumerable<TItem> source, Func<TItem, int> selector)
        {
            var bridge = Bridge(source, nameof(source));
            if (selector == null) throw CommonImplementation.ArgumentNull(nameof(selector));

            return CommonImplementation.MinSelectorImpl<TItem, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>>(RefLocal(bridge), selector);
        }

        public TResult Min<TItem, TResult>(BuiltInEnumerable<TItem> source, Func<TItem, TResult> selector)
        {
            var bridge = Bridge(source, nameof(source));
            if (selector == null) throw CommonImplementation.ArgumentNull(nameof(selector));

            return CommonImplementation.MinProjectedComparableImpl<TItem, TResult, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>>(RefLocal(bridge), selector);
        }

        // OrderBy

        public OrderByEnumerable<TItem, TKey, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>, DefaultAscending<TItem, TKey>> OrderBy<TItem, TKey>(BuiltInEnumerable<TItem> source, Func<TItem, TKey> keySelector)
        {
            var bridge = Bridge(source, nameof(source));
            if (keySelector == null) throw CommonImplementation.ArgumentNull(nameof(keySelector));

            return CommonImplementation.OrderBy<TItem, TKey, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>>(RefLocal(bridge), keySelector);
        }

        public OrderByEnumerable<TItem, TKey, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>, SingleComparerAscending<TItem, TKey>> OrderBy<TItem, TKey>(BuiltInEnumerable<TItem> source, Func<TItem, TKey> keySelector, IComparer<TKey> comparer)
        {
            var bridge = Bridge(source, nameof(source));
            if (keySelector == null) throw CommonImplementation.ArgumentNull(nameof(keySelector));

            return CommonImplementation.OrderBy<TItem, TKey, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>>(RefLocal(bridge), keySelector, comparer);
        }

        public OrderByEnumerable<TItem, TKey, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>, DefaultDescending<TItem, TKey>> OrderByDescending<TItem, TKey>(BuiltInEnumerable<TItem> source, Func<TItem, TKey> keySelector)
        {
            var bridge = Bridge(source, nameof(source));
            if (keySelector == null) throw CommonImplementation.ArgumentNull(nameof(keySelector));

            return CommonImplementation.OrderByDescending<TItem, TKey, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>>(RefLocal(bridge), keySelector);
        }

        public OrderByEnumerable<TItem, TKey, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>, SingleComparerDescending<TItem, TKey>> OrderByDescending<TItem, TKey>(BuiltInEnumerable<TItem> source, Func<TItem, TKey> keySelector, IComparer<TKey> comparer)
        {
            var bridge = Bridge(source, nameof(source));
            if (keySelector == null) throw CommonImplementation.ArgumentNull(nameof(keySelector));

            return CommonImplementation.OrderByDescending<TItem, TKey, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>>(RefLocal(bridge), keySelector, comparer);
        }

        // Prepend
        public PrependEnumerable<TItem, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>> Prepend<TItem>(BuiltInEnumerable<TItem> source, TItem element)
        {
            var bridge = Bridge(source, nameof(source));

            return CommonImplementation.PrependImpl<TItem, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>>(RefLocal(bridge), element);
        }

        // Reverse

        public ReverseEnumerable<TItem, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>> Reverse<TItem>(BuiltInEnumerable<TItem> source)
        {
            var bridge = Bridge(source, nameof(source));

            return CommonImplementation.ReverseImpl<TItem, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>>(RefLocal(bridge));
        }

        // Select

        public SelectIndexedEnumerable<TItem, TOutItem, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>> Select<TItem, TOutItem>(BuiltInEnumerable<TItem> source, Func<TItem, int, TOutItem> selector)
        {
            var bridge = Bridge(source, nameof(source));
            if (selector == null) throw CommonImplementation.ArgumentNull(nameof(selector));

            return CommonImplementation.SelectImpl<TItem, TOutItem, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>>(RefLocal(bridge), selector);
        }

        public SelectEnumerable<TItem, TOutItem, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>> Select<TItem, TOutItem>(BuiltInEnumerable<TItem> source, Func<TItem, TOutItem> selector)
        {
            var bridge = Bridge(source, nameof(source));
            if (selector == null) throw CommonImplementation.ArgumentNull(nameof(selector));

            return CommonImplementation.SelectImpl<TItem, TOutItem, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>>(RefLocal(bridge), selector);
        }
        
        // Single

        public TItem Single<TItem>(BuiltInEnumerable<TItem> source)
        {
            var bridge = Bridge(source, nameof(source));

            return CommonImplementation.SingleImpl<TItem, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>>(RefLocal(bridge));
        }

        public TItem Single<TItem>(BuiltInEnumerable<TItem> source, Func<TItem, bool> predicate)
        {
            var bridge = Bridge(source, nameof(source));
            if (predicate == null) throw CommonImplementation.ArgumentNull(nameof(predicate));

            return CommonImplementation.SingleImpl<TItem, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>>(RefLocal(bridge), predicate);
        }

        public TItem SingleOrDefault<TItem>(BuiltInEnumerable<TItem> source)
        {
            var bridge = Bridge(source, nameof(source));

            return CommonImplementation.SingleOrDefaultImpl<TItem, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>>(RefLocal(bridge));
        }

        public TItem SingleOrDefault<TItem>(BuiltInEnumerable<TItem> source, Func<TItem, bool> predicate)
        {
            var bridge = Bridge(source, nameof(source));
            if (predicate == null) throw CommonImplementation.ArgumentNull(nameof(predicate));

            return CommonImplementation.SingleOrDefaultImpl<TItem, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>>(RefLocal(bridge), predicate);
        }

        // Skip

        public SkipEnumerable<TItem, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>> Skip<TItem>(BuiltInEnumerable<TItem> source, int count)
        {
            var bridge = Bridge(source, nameof(source));

            return CommonImplementation.SkipImpl<TItem, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>>(RefLocal(bridge), count);
        }

        public SkipWhileIndexedEnumerable<TItem, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>> SkipWhile<TItem>(BuiltInEnumerable<TItem> source, Func<TItem, int, bool> predicate)
        {
            var bridge = Bridge(source, nameof(source));
            if (predicate == null) throw CommonImplementation.ArgumentNull(nameof(predicate));

            return CommonImplementation.SkipWhileImpl<TItem, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>>(RefLocal(bridge), predicate);
        }

        public SkipWhileEnumerable<TItem, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>> SkipWhile<TItem>(BuiltInEnumerable<TItem> source, Func<TItem, bool> predicate)
        {
            var bridge = Bridge(source, nameof(source));
            if (predicate == null) throw CommonImplementation.ArgumentNull(nameof(predicate));

            return CommonImplementation.SkipWhileImpl<TItem, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>>(RefLocal(bridge), predicate);
        }

        public SkipLastEnumerable<TItem, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>> SkipLast<TItem>(BuiltInEnumerable<TItem> source, int count)
        {
            var bridge = Bridge(source, nameof(source));

            return CommonImplementation.SkipLastImpl<TItem, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>>(RefLocal(bridge), count);
        }

        // Take

        public TakeEnumerable<TItem, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>> Take<TItem>(BuiltInEnumerable<TItem> source, int count)
        {
            var bridge = Bridge(source, nameof(source));

            return CommonImplementation.TakeImpl<TItem, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>>(RefLocal(bridge), count);
        }

        public TakeWhileIndexedEnumerable<TItem, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>> TakeWhile<TItem>(BuiltInEnumerable<TItem> source, Func<TItem, int, bool> predicate)
        {
            var bridge = Bridge(source, nameof(source));
            if (predicate == null) throw CommonImplementation.ArgumentNull(nameof(predicate));

            return CommonImplementation.TakeWhileImpl<TItem, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>>(RefLocal(bridge), predicate);
        }

        public TakeWhileEnumerable<TItem, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>> TakeWhile<TItem>(BuiltInEnumerable<TItem> source, Func<TItem, bool> predicate)
        {
            var bridge = Bridge(source, nameof(source));
            if (predicate == null) throw CommonImplementation.ArgumentNull(nameof(predicate));

            return CommonImplementation.TakeWhileImpl<TItem, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>>(RefLocal(bridge), predicate);
        }

        public TakeLastEnumerable<TItem, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>> TakeLast<TItem>(BuiltInEnumerable<TItem> source, int count)
        {
            var bridge = Bridge(source, nameof(source));

            return CommonImplementation.TakeLastImpl<TItem, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>>(RefLocal(bridge), count);
        }

        // ToArray
        public TItem[] ToArray<TItem>(BuiltInEnumerable<TItem> source)
        {
            var asICollection = source as ICollection<TItem>;
            if (asICollection != null)
            {
                var ret = Allocator.Current.GetArray<TItem>(asICollection.Count);
                asICollection.CopyTo(ret, 0);
                return ret;
            }

            var bridge = Bridge(source, nameof(source));

            return CommonImplementation.ToArrayImpl<TItem, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>>(RefLocal(bridge));
        }

        // ToDictionary

        public Dictionary<TKey, TItem> ToDictionary<TItem, TKey>(BuiltInEnumerable<TItem> source, Func<TItem, TKey> keySelector)
        {
            var bridge = Bridge(source, nameof(source));
            if (keySelector == null) throw CommonImplementation.ArgumentNull(nameof(keySelector));

            var asList = source as IList<TItem>;
            if (asList != null)
            {
                var size = asList.Count;
                var ret = Allocator.Current.GetEmptyDictionary<TKey, TItem>(size, null);
                CommonImplementation.ToDictionaryLoopImpl<TItem, TKey, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>>(RefLocal(bridge), ret, keySelector);
                return ret;
            }

            return CommonImplementation.ToDictionaryImpl<TItem, TKey, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>>(RefLocal(bridge), keySelector);
        }

        public Dictionary<TKey, TElement> ToDictionary<TItem, TKey, TElement>(BuiltInEnumerable<TItem> source, Func<TItem, TKey> keySelector, Func<TItem, TElement> elementSelector)
        {
            var bridge = Bridge(source, nameof(source));
            if (keySelector == null) throw CommonImplementation.ArgumentNull(nameof(keySelector));
            if (elementSelector == null) throw CommonImplementation.ArgumentNull(nameof(elementSelector));

            var asList = source as IList<TItem>;
            if (asList != null)
            {
                var size = asList.Count;
                var ret = Allocator.Current.GetEmptyDictionary<TKey, TElement>(size, null);
                CommonImplementation.ToDictionaryLoopImpl<TItem, TKey, TElement, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>>(RefLocal(bridge), ret, keySelector, elementSelector);
                return ret;
            }

            return CommonImplementation.ToDictionaryImpl<TItem, TKey, TElement, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>>(RefLocal(bridge), keySelector, elementSelector, null);
        }

        public Dictionary<TKey, TItem> ToDictionary<TItem, TKey>(BuiltInEnumerable<TItem> source, Func<TItem, TKey> keySelector, IEqualityComparer<TKey> comparer)
        {
            var bridge = Bridge(source, nameof(source));
            if (keySelector == null) throw CommonImplementation.ArgumentNull(nameof(keySelector));

            var asList = source as IList<TItem>;
            if (asList != null)
            {
                var size = asList.Count;
                var ret = Allocator.Current.GetEmptyDictionary<TKey, TItem>(size, comparer);
                CommonImplementation.ToDictionaryLoopImpl<TItem, TKey, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>>(RefLocal(bridge), ret, keySelector);
                return ret;
            }

            return CommonImplementation.ToDictionaryImpl<TItem, TKey, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>>(RefLocal(bridge), keySelector, comparer);
        }

        public Dictionary<TKey, TElement> ToDictionary<TItem, TKey, TElement>(BuiltInEnumerable<TItem> source, Func<TItem, TKey> keySelector, Func<TItem, TElement> elementSelector, IEqualityComparer<TKey> comparer)
        {
            var bridge = Bridge(source, nameof(source));
            if (keySelector == null) throw CommonImplementation.ArgumentNull(nameof(keySelector));
            if (elementSelector == null) throw CommonImplementation.ArgumentNull(nameof(elementSelector));

            var asList = source as IList<TItem>;
            if (asList != null)
            {
                var size = asList.Count;
                var ret = Allocator.Current.GetEmptyDictionary<TKey, TElement>(size, comparer);
                CommonImplementation.ToDictionaryLoopImpl<TItem, TKey, TElement, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>>(RefLocal(bridge), ret, keySelector, elementSelector);
                return ret;
            }

            return CommonImplementation.ToDictionaryImpl<TItem, TKey, TElement, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>>(RefLocal(bridge), keySelector, elementSelector, comparer);
        }

        // ToHashSet
        public HashSet<TItem> ToHashSet<TItem>(BuiltInEnumerable<TItem> source)
        {
            var bridge = Bridge(source, nameof(source));
            return CommonImplementation.ToHashSetImpl<TItem, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>>(RefLocal(bridge), null);
        }

        public HashSet<TItem> ToHashSet<TItem>(BuiltInEnumerable<TItem> source, IEqualityComparer<TItem> comparer)
        {
            var bridge = Bridge(source, nameof(source));
            return CommonImplementation.ToHashSetImpl<TItem, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>>(RefLocal(bridge), comparer);
        }

        // ToList

        public List<TItem> ToList<TItem>(BuiltInEnumerable<TItem> source)
        {
            var asIEnumerable = source as IEnumerable<TItem>;
            if (asIEnumerable != null)
            {
                return Allocator.Current.GetPopulatedList(asIEnumerable);
            }

            var bridge = Bridge(source, nameof(source));

            return CommonImplementation.ToListImpl<TItem, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>>(RefLocal(bridge));
        }

        // ToLookup
        public LookupDefaultEnumerable<TKey, TItem> ToLookup<TItem, TKey>(BuiltInEnumerable<TItem> source, Func<TItem, TKey> keySelector)
        {
            var bridge = Bridge(source, nameof(source));
            if (keySelector == null) throw CommonImplementation.ArgumentNull(nameof(keySelector));

            return CommonImplementation.ToLookupImpl<TItem, TKey, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>>(RefLocal(bridge), keySelector);
        }

        public LookupSpecificEnumerable<TKey, TItem> ToLookup<TItem, TKey>(BuiltInEnumerable<TItem> source, Func<TItem, TKey> keySelector, IEqualityComparer<TKey> comparer)
        {
            var bridge = Bridge(source, nameof(source));
            if (keySelector == null) throw CommonImplementation.ArgumentNull(nameof(keySelector));

            return CommonImplementation.ToLookupImpl<TItem, TKey, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>>(RefLocal(bridge), keySelector, comparer);
        }

        public LookupDefaultEnumerable<TKey, TElement> ToLookup<TItem, TKey, TElement>(BuiltInEnumerable<TItem> source, Func<TItem, TKey> keySelector, Func<TItem, TElement> elementSelector)
        {
            var bridge = Bridge(source, nameof(source));
            if (keySelector == null) throw CommonImplementation.ArgumentNull(nameof(keySelector));
            if (elementSelector == null) throw CommonImplementation.ArgumentNull(nameof(elementSelector));

            return CommonImplementation.ToLookupImpl<TItem, TKey, TElement, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>>(RefLocal(bridge), keySelector, elementSelector);
        }

        public LookupSpecificEnumerable<TKey, TElement> ToLookup<TItem, TKey, TElement>(BuiltInEnumerable<TItem> source, Func<TItem, TKey> keySelector, Func<TItem, TElement> elementSelector, IEqualityComparer<TKey> comparer)
        {
            var bridge = Bridge(source, nameof(source));
            if (keySelector == null) throw CommonImplementation.ArgumentNull(nameof(keySelector));
            if (elementSelector == null) throw CommonImplementation.ArgumentNull(nameof(elementSelector));

            return CommonImplementation.ToLookupImpl<TItem, TKey, TElement, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>>(RefLocal(bridge), keySelector, elementSelector, comparer);
        }
        
        // Where

        public WhereIndexedEnumerable<TItem, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>> Where<TItem>(BuiltInEnumerable<TItem> source, Func<TItem, int, bool> predicate)
        {
            var bridge = Bridge(source, nameof(source));
            if (predicate == null) throw CommonImplementation.ArgumentNull(nameof(predicate));

            return CommonImplementation.WhereImpl<TItem, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>>(RefLocal(bridge), predicate);
        }

        public WhereEnumerable<TItem, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>> Where<TItem>(BuiltInEnumerable<TItem> source, Func<TItem, bool> predicate)
        {
            var bridge = Bridge(source, nameof(source));
            if (predicate == null) throw CommonImplementation.ArgumentNull(nameof(predicate));

            return CommonImplementation.WhereImpl<TItem, BuiltInEnumerable<TItem>, BuiltInEnumerator<TItem>>(RefLocal(bridge), predicate);
        }
    }
}