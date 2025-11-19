using System.Runtime.InteropServices;

namespace SmartAss.Text;

[StructLayout(LayoutKind.Auto)]
public readonly struct Char2 : IEquatable<Char2>, IEquatable<string>
{
    public Char2(string str)
    {
        Guard.NotNull(str, nameof(str));
        if (str.Length != 2) { throw new ArgumentOutOfRangeException(nameof(str), "String should have a length of 2."); }
        else
        {
            _0 = str[0];
            _1 = str[1];
        }
    }

    public Char2(char ch0, char ch1)
    {
        _0 = ch0;
        _1 = ch1;
    }

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly char _0;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly char _1;

    public char this[int index] => index switch
    {
        0 => _0,
        1 => _1,
        _ => throw new IndexOutOfRangeException(),
    };

    /// <inheritdoc />
    [Pure]
    public override string ToString() => new([_0, _1]);

    /// <inheritdoc />
    [Pure]
    public override bool Equals(object? obj)
        => (obj is string str && Equals(str))
        || (obj is Char2 ch2 && Equals(ch2));

    /// <inheritdoc />
    [Pure]
    public bool Equals(Char2 other) => _0 == other._0 && _1 == other._1;

    /// <inheritdoc />
    [Pure]
    public bool Equals(string? other)
        => other is not null
        && other.Length == 2
        && other[0] == _0
        && other[1] == _1;

    /// <inheritdoc />
    [Pure]
    public override int GetHashCode() => _0 ^ (_1 << 16);

    public static implicit operator string(Char2 chars) => chars.ToString();
}
