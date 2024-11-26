using SmartAss.Numerics;
using System.ComponentModel;

namespace SmartAss.Conversion.Numerics;

public sealed class Point4DTypeConverter : TypeConverter
{
    [Pure]
    public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        => sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);

    [Pure]
    public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
       => value is string str
        ? Point4D.Parse(str)
        : base.ConvertFrom(context, culture, value);
}
