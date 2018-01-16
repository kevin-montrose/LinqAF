# LinqAF

A low allocation re-implementation of LINQ-to-Objects using some dubious techniques.

## Compatibility

LinqAF aims to be "type inference compatible" with LINQ-to-Objects, a very weak form of source compatible I made up.  Essentially, if your LINQ code has no type names in it then LinqAF will probably just work - this is common in LINQ code due to extensive use of anonymous delegates and `var`.

As an illustration, the following code will works seamlessly with LINQ-to-Objects and LinqAF.

```csharp
var range  = Enumerable.Range(0, 100);
var expanded = range.SelectMany(x => new [] { x, x * 2 });
var reversed = expanded.Reverse();
var asString = reversed.Select(y => y.ToString());

foreach(var str in asString)
{
  Console.WriteLine(str);
}
```

and could be written more concisely as 

```csharp
var e = 
  Enumerable.Range(0, 100)
  .SelectMany(x => new [] { x, x * 2 })
  .Reverse()
  .Select(y => y.ToString());
 
foreach(var str in e)
{
  Console.WriteLine(str);
}
```

## Dealing with compatibility exceptions

There cases where LinqAF is not a direct replacement for LinqAF are broadly:

 1. Where the `IEnumerable<T>` type appears
 2. When different kinds of enumerables need to be assigned or returned
 
### Dealing with IEnumerable<T> 

There are a few fixes for the first case: use `AsEnumerable()` to create an `IEnumerable<T>` wrapper (this allocates), change the type to `var`, or change the type to the appropriate LinqAF enumerable.

This LINQ-to-Objects code, for example

```csharp
IEnumerable<string> e = Enumerable.Repeat("foo", 3);
IEnumerable<char> f = e.Select((string x, int ix) => x[ix]);
```

could be rewritten as 

```csharp
var e = Enumerable.Repeat("foo", 3);
var f = e.Select((x, ix) => x[ix]);
```

or 

```csharp
RepeatEnumerable<string> e = Enumerable.Repeat("foo", 3);
SelectIndexedEnumerable<string, char, RepeatEnumerable<string>, RepeatEnumerator<string>> f = e.Select((x, ix) => x[ix]);
```

or (if allocations are acceptable)

```csharp
IEnumerable<string> e = Enumerable.Repeat("foo", 3).AsEnumerable();
IEnumerable<char> f = e.Select((string x, int ix) => x[ix]).AsEnumerable();
```

obviously, the first option is the preferable one.

### Coalescing different kinds of enumerables

All LINQ-to-Objects operators return `IEnumerable<T>` or a subtype of it, which means that you can assign the result of almost all operators to the same variables, parameters, or returns (modulo the generic type parameter T).  In LinqAF every operator returns a different type, which breaks code like the following:

```csharp
// this works with LINQ-to-Objects, but not LinqAF
int a = ...;
var e = a > 0 ? Enumerable.Repeat(1, 1) : Enumerable.Empty<int>();
```

There are two ways to fix this, either introduce calls to `Box()` or `AsEnumerable()`.  `AsEnumerable()` returns an IEnumerable<T> (and is thus ideal for interoperating with non-LinqAF code), while `Box()` returns a `BoxedEnumerable<T>` (there is also an explicit cast from all LinqAF enumerables to `BoxedEnumerable<T>`, but using `.Box()` lets you avoid typing out type names).

So the previous code would become either

```csharp
// this works with both LINQ-to-Objects and LinqAF
int a = ...;
var e = a > 0 ? Enumerable.Repeat(1, 1).AsEnumerable() : Enumerable.Empty<int>().AsEnumerable();
```

or

```csharp
// this works with LinqAF, but not LINQ-to-Objects
int a = ...;
var e = a > 0 ? Enumerable.Repeat(1, 1).Box() : Enumerable.Empty<int>().Box();
```

## Optimizations

LinqAF's primary purpose (besides scratching an intellectual itch) is to minimize the number of allocations LINQ-like code involves.  Accordingly almost all operations are zero-allocation, with the exceptions of the `ToXXX`, `Distinct`, `Except`, `GroupJoin`, `Intersect`, `Join`, `OrderBy(Descending)`, `ThenBy(Descending)`, `Reverse`, and `Union` operations (and even then, certain cases will be zero allocation).

LinqAF achieves this, in part, by representing all operations as structs and exploiting C#'s extensive duck-typing around LINQ and `foreach`.  This blatently disregards the [official guidance on struct use](https://docs.microsoft.com/en-us/dotnet/standard/design-guidelines/choosing-between-class-and-struct).

Note that LinqAF does not change any of C#'s LINQ -> method rewrite rules, so allocations introduced as part of those will still occur.  It is for this reason that I encourage the direct use of methods rather than [LINQ query keywords](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/query-keywords) (in all cases, not just with LinqAF) as that syntax makes allocations from intermediate objects and delegates explicit.

LinqAF leverages the much more extensive type information it has available at compile time (compared to LINQ-to-Objects) to optimize numerous cases.  For example, `Enumerable.Range(0, 100).Reverse()` is optimized into a `ReverseRangeEnumerable<int>` that does no allocations.

LinqAF also has a few alternative collection implementations that aim to allocate less than what LINQ-to-Objects would.

LinqAF also enables custom allocation rules (enabling collection reuse, or allocation monitoring) via registering an `IAllocator` implementation with `LinqAF.Config.Allocator`.

More details can be found in an upcoming blog series.

## Using LinqAF

[LinqAF is available on Nuget](https://www.nuget.org/packages/LinqAF/).

Be aware that LinqAF is full of dubious ideas: large structs, mutable structs, lots of generic constraints, lots of generic parameters, and truly gargantuan amounts of generated code.

LinqAF _can_ be useful, but it's not a painless or trivial dependency.  Be very cautious in using it.

Also the LinqAF.dll is ~23MB, so that's a thing.

## Acknowledgements

The initial motivation for LinqAF came from playing around with the [Rust Language](https://www.rust-lang.org/en-US/) and its [Iterators](https://doc.rust-lang.org/std/iter/trait.Iterator.html).

A great deal of my understanding of LINQ-to-Objects (and [600+ test cases](https://github.com/kevin-montrose/LinqAF/blob/master/LinqAF.Tests/EdulinqTests.cs)) came from [Jon Skeet's EduLinq blog series](https://codeblog.jonskeet.uk/category/edulinq/).

The [MiscUtil library](http://www.yoda.arachsys.com/csharp/miscutil/) made my `RangeEnumerable` implementation possible - so thanks to Jon Skeet (again), and Marc Gravell.

Thanks to Benjamin Hodgson for `Box()`, as he pointed out that explicit casting couldn't handle anonymous types and would be quite painful for long type names regardless.
