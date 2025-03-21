// <copyright file = "SetExtensions.cs">
// Copyright (c) 2018-current, Corniel Nobel.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using SmartAss;

namespace System.Linq;

public static class SetExtensions
{
    [Pure]
    public static IEnumerable<TResult> IntersectMany<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, IEnumerable<TResult>> selector)
    {
        Guard.NotNull(source, nameof(source));
        using var enumerator = source.GetEnumerator();

        if (!enumerator.MoveNext()) { return []; }

        var ret = selector(enumerator.Current);

        while (enumerator.MoveNext())
        {
            ret = ret.Intersect(selector(enumerator.Current));
        }
        return ret;
    }

    [CollectionMutation]
    public static bool AddRange<T>(this ISet<T> set, IEnumerable<T> items)
    {
        Guard.NotNull(set, nameof(set));
        Guard.NotNull(items, nameof(items));

        var added = false;
        foreach (var item in items) { added |= set.Add(item); }
        return added;
    }
}
