namespace SmartAss.Specs.Reddit;

public readonly struct Temperature : IFormattable
{
    private static readonly decimal offset = 273.15m;
    private readonly decimal kelvin;

    private Temperature(decimal k) => kelvin = k;
    public string ToString(string? format, IFormatProvider? formatProvider) => format ?? string.Empty switch
    {
        var f when f.EndsWith('C') => (kelvin - offset).ToString(f[..^1], formatProvider),
        var f when f.EndsWith('F') => ((kelvin - offset) * 9 / 5 + 32).ToString(f[..^1], formatProvider),
        _ => kelvin.ToString(format, formatProvider),
    };

    public static Temperature FromCelcius(string s, IFormatProvider? provider = null)
        => FromCelcius(decimal.Parse(s, provider));

    public static Temperature FromCelcius(decimal c) => new(c + offset);

    public static Temperature FromFahrenheit(string s, IFormatProvider? provider = null)
        => FromFahrenheit(decimal.Parse(s, provider));

    public static Temperature FromFahrenheit(decimal f) => new((f - 32) * 5 / 9 + offset);
}
