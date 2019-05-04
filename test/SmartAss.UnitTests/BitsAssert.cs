using NUnit.Framework;
using System;
using System.Diagnostics;

namespace SmartAss.Tests
{
    public static class BitsAssert
    {
        [DebuggerStepThrough]
        public static void AreEqual(ulong expected, ulong actual)
        {
            if(expected != actual)
            {
                Assert.Fail($"Expected: 0x{expected:X2} ({expected}){Environment.NewLine}Actual:   0x{actual:X2} ({actual})");
            }
        }
    }
}
