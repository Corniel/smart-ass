using Qowaiv.Conversion;
using SmartAss.Numerics;

namespace SmartAss.Conversion.Numerics;

public sealed class ModuloInt32TypeConverter : SvoTypeConverter<ModuloInt32>
{
    [Pure]
    protected override ModuloInt32 FromString(string? str, CultureInfo? culture) => ModuloInt32.Parse(str);
}
