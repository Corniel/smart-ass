namespace SmartAss.Numerics;

public static class Numbers
{
    [Pure]
    public static int Sign(this int number) => Math.Sign(number);

    [Pure]
    public static int Abs(this int number) => Math.Abs(number);
}
