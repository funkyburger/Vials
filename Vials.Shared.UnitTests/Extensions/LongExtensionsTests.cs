using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vials.Shared.Extensions;

namespace Vials.Shared.UnitTests.Extensions
{
    [TestClass]
    public class LongExtensionsTests
    {
        [TestMethod]
        public void MirrorsWork()
        {
            var number = (long)0b0001_0101_0011_1111_0000_1110_0010_0001; // 356453921
            number.MirrorBytes().ShouldBe(0b0000_0100_0111_0000_1111_1100_1010_1000_0001_0101_0011_1111_0000_1110_0010_0001);
        }

        [TestMethod]
        public void MirrorOfBigOneIsSame()
        {
            var number = 0b0001_0001_0101_0011_1111_0000_1110_0010_0001; // Too big
            number.MirrorBytes().ShouldBe(number);
            number = 0b1001_0101_0011_1111_0000_1110_0010_0001; // Almost
            number.MirrorBytes().ShouldNotBe(number);
        }
    }
}
