// <copyright file = "Vectors.cs">
// Copyright (c) 2018-current, Corniel Nobel.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace SmartAss.Numerics;

public static class Vectors
{
    /// <summary>Sums all vectors together.</summary>
    public static Vector Sum(this IEnumerable<Vector> vectors)
    {
        Guard.NotNull(vectors, nameof(vectors));

        var sum = Vector.O;
        foreach (var vector in vectors) { sum += vector; }
        return sum;
    }
}
