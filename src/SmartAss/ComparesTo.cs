namespace SmartAss;

public static class ComparableExtensions
{
    public static int? ComparesTo<T>(this T? comparable, T? other) where T : struct, IComparable<T>
    {
        if (comparable is { } c && other is { } o) return c.ComparesTo(o);
        else if (comparable.HasValue) { return -1; }
        else if (other.HasValue) { return +1; }
        else { return default; }
    }

    public static int? ComparesTo<T>(this T comparable, T other) where T : IComparable<T>
    {
        if (comparable is null) { return other is null ? null : +1; }
        var compare = comparable.CompareTo(other);
        return compare == 0 ? null : compare;
    }

    public static int? ComparesTo(this IComparable comparable, IComparable other)
    {
        if (comparable is null) { return other is null ? null : +1; }
        var compare = comparable.CompareTo(other);
        return compare == 0 ? null : compare;
    }
}
