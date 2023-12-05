using System.ComponentModel;

namespace SmartAss.Conversion.Numerics;

public sealed class Int64RangeTypeConverter : TypeConverter
{
    [Pure]
    public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType)
        => sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);

    [Pure]
    public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
       => value is string str
        ? SmartAss.Numerics.Int64Range.Parse(str)
        : base.ConvertFrom(context, culture, value);
}
