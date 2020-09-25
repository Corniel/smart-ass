# Smart Ass

In this library I research some techniques to help build high performant code
(speedwise) in C#. It comes with absolutely no warranty, and you should in some
cases really wonder, if using this code will safe your day.

## Jagged Array
Jagged are often used for multi-dimensional arrays as they are way faster then
.NET's built-in multi-dimensional arrays. In that case, a clear _constructor_
can help:

``` C#
T[] two_dimensional = Jagger.Array<T>(m, n);
T[][] three_dimensional = Jagger.Array<T>(m, n, o);
T[][][] four_dimensional = Jagger.Array<T>(m, n, o, p);
```
