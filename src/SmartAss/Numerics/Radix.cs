namespace SmartAss.Numerics;

public readonly struct Radix
{
    public static readonly Radix Unary = new(1);
    public static readonly Radix Binary = new(2);
    public static readonly Radix Ternary = new(3);
    public static readonly Radix Quaternary = new(4);
    public static readonly Radix Quinary = new(5);
    public static readonly Radix Senary = new(6);
    public static readonly Radix Septimal = new(7);
    public static readonly Radix Octal = new(8);
    public static readonly Radix Nonary = new(9);
    public static readonly Radix Decimal;
    public static readonly Radix Duodecimal = new(12);
    public static readonly Radix Hexadecimal = new(16);
    public static readonly Radix Vigesimal = new(20);
    public static readonly Radix Duotrigesimal = new(32);
    public static readonly Radix Hexatrigesimal = new(36);
    public static readonly Radix Sexagesimal = new(60);


    private readonly int Value;

    public Radix(int value) => Value = value- 10;

    public int Size => Value + 10;

    /// <inheritdoc />
    [Pure]
    public override int GetHashCode() => Value;
}
