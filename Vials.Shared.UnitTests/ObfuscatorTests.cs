using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vials.Shared.Extensions;
using Vials.Shared.Objects;

namespace Vials.Shared.UnitTests
{
    [TestClass]
    public class ObfuscatorTests
    {
        [TestMethod]
        public void DoesObfuscate()
        {
            var obfuscator = new Obfuscator();

            var number = 547744215L;

            obfuscator.Obfuscate(number, 123456L).ShouldBe(1342904483443114659L);
        }

        [TestMethod]
        public void Roundtrip()
        {
            var obfuscator = new Obfuscator();

            var number = 547744215L;

            var obfuscated = obfuscator.Obfuscate(number, 123456L);

            obfuscator.Unobfuscate(obfuscated, 123456L).ShouldBe(547744215L);
        }
    }
}
