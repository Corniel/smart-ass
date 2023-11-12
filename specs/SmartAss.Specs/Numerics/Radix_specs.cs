using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Numerics.Radix_specs;

internal class Radix_specs
{
    [Test]
    public void X()
    {
        var s = 12L.ToString("", new CultureInfo("en-US"));
    }
}
