using NUnit.Framework;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vials.Server.UnitTests.Utils;
using Vials.Server.Utilities;
using Vials.Shared.Objects;

namespace Vials.Server.UnitTests.Utilities
{
    public class SetGeneratorTests
    {
        [Test]
        public void SetIsGenerated()
        {
            var generator = new SetGenerator(new TestColorStackFactory(), new FakeRandomGenerator());

            var set = generator.Generate(4, 2, 123);

            var colors = set.Vials.Select(v => v.Colors).ToArray();
            set.Vials.Count().ShouldBe(6);

            foreach(var vial in set.Vials.Take(4))
            {
                vial.Colors.Count().ShouldBe(4);
            }

            set.Vials.Last().Colors.ShouldBeEmpty();
            set.Vials.TakeLast(2).First().Colors.ShouldBeEmpty();
        }

        [Test]
        public void GeneratorCanDoUpToTwelveColors()
        {
            var generator = new SetGenerator(new TestColorStackFactory(), new FakeRandomGenerator());

            var set = generator.Generate(12, 2, 123);

            var colors = set.Vials.Select(v => v.Colors).ToArray();
            set.Vials.Count().ShouldBe(14);

            foreach (var vial in set.Vials.Take(12))
            {
                vial.Colors.Count().ShouldBe(4);
            }

            set.Vials.Last().Colors.ShouldBeEmpty();
            set.Vials.TakeLast(2).First().Colors.ShouldBeEmpty();

            var counter = new ColorCounter();
            counter.Add(set);

            counter.Count().ShouldBe(12);
            foreach (var count in counter)
            {
                count.Key.ShouldNotBe(Color.None);
                count.Value.ShouldBe(4);
            }
        }

        [Test]
        public void VialFootPrintsAreUnique()
        {
            var register = new Dictionary<int, int>();
            int dummy;
            var generator = new SetGenerator(new TestColorStackFactory()
                , new FakeRandomGenerator(new int[] { 1, 2, 7, 9, 4, 3, 1, 9, 5, 6, 7, 2, 1, 3, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 }));

            var set = generator.Generate(12, 2, 123);

            foreach (var vial in set.Vials.Take(12))
            {
                register.TryGetValue(vial.FootPrint, out dummy).ShouldBeFalse();

                register.Add(vial.FootPrint, 0);
            }
        }
    }
}
