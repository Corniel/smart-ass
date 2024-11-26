// <copyright file = "HeapPermutations.cs">
// Copyright (c) 2018-current, Corniel Nobel.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using SmartAss;

namespace System;

/// <summary>Gets permutations using the Heap's algorithm.</summary>
public static class HeapPermutations
{
    [Pure]
    public static IEnumerable<T[]> Permutations<T>(this T[] values)
        => values.Permutations(Guard.NotNull(values, nameof(values)).Length, 0);

    /// <remarks>Heap's algorithm.</remarks>
    [Pure]
    private static IEnumerable<T[]> Permutations<T>(this T[] array, int size, int n)
    {
        if (size == 1)
        {
            yield return array.Copy();
        }

        for (int i = 0; i < size; i++)
        {
            foreach (var result in Permutations(array, size - 1, n))
            {
                yield return result;
            }

            array.Swap(size.IsEven() ? i : 0, size - 1);
        }
    }

    private static void Swap<T>(this T[] array, int index0, int index1)
    {
        (array[index1], array[index0]) = (array[index0], array[index1]);
    }

    [Pure]
    private static T[] Copy<T>(this T[] array)
    {
        var copy = new T[array.Length];
        Array.Copy(array, copy, array.Length);
        return copy;
    }
}
