using Qowaiv.Conversion;
using SmartAss.Numerics;

namespace SmartAss.Conversion.Numerics;

public sealed class ModuloInt64TypeConverter : SvoTypeConverter<ModuloInt64>
{
    protected override ModuloInt64 FromString(string? str, CultureInfo? culture) => ModuloInt64.Parse(str);
}

