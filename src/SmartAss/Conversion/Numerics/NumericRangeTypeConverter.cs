using System.ComponentModel;
using System.Reflection;

namespace SmartAss.Conversion.Numerics;

public sealed class NumericRangeTypeConverter : TypeConverter
{
    private readonly MethodInfo Parse;

    public NumericRangeTypeConverter(Type type)
        => Parse = type?.GetMethod(nameof(Parse), BindingFlags.Static | BindingFlags.Public)
        ?? throw new ArgumentException($"Type {type} does not contain a parse method.");

    [Pure]
    public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType)
        => sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);

    [Pure]
    public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
       => value is string str
        ? Parse.Invoke(null, [str])
        : base.ConvertFrom(context, culture, value);
}
