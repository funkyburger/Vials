using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vials.Shared.Extensions;
using Vials.Shared.Objects;
using Vials.Shared.UnitTests.Utils;

namespace Vials.Shared.UnitTests
{
    [TestClass]
    public class FinishedGamePackerTests
    {
        [TestMethod]
        public void Packs()
        {
            var packer = new FinishedGamePacker(new DummyObfuscator());

            var toPack /*shakur ;)*/ = new FinishedGame() {
                Seed = 123,
                FootPrint = 456,
                Track = new long[] { 123, 456, 789 }
            };

            var pack = packer.Pack(toPack).ToArray();
            pack.IsEqualTo(new long[] { 455, 579, 912, 1245, 579 }).ShouldBeTrue();
        }

        [TestMethod]
        public void Unpacks()
        {
            var packer = new FinishedGamePacker(new DummyObfuscator());

            var toUnpack = new long[] { 455, 579, 912, 1245, 579 };

            var unpacked = packer.Unpack(toUnpack);

            unpacked.Seed.ShouldBe(123);
            unpacked.FootPrint.ShouldBe(456);
            unpacked.Track.IsEqualTo(new long[] { 123, 456, 789 }).ShouldBeTrue();
        }

        [TestMethod]
        public void Roundtrip()
        {
            var packer = new FinishedGamePacker(new DummyObfuscator());

            var toPack = new FinishedGame()
            {
                Seed = 123,
                FootPrint = 456,
                Track = new long[] { 123, 456, 789 }
            };

            var pack = packer.Pack(toPack).ToArray();
            var unpacked = packer.Unpack(pack);

            unpacked.Seed.ShouldBe(toPack.Seed);
            unpacked.FootPrint.ShouldBe(toPack.FootPrint);
            unpacked.Track.IsEqualTo(toPack.Track).ShouldBeTrue();
        }
    }
}
