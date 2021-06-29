using NUnit.Framework;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vials.Server.UnitTests.Utils;
using Vials.Server.Utilities;
using Vials.Shared.Objects;

namespace Vials.Server.UnitTests
{
    public class SetGeneratorTests
    {
        [Test]
        public void SetIsGenerated()
        {
            var generator = new SetGenerator();

            var set = generator.Generate(4, 2);

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
            var generator = new SetGenerator();

            var set = generator.Generate(12, 2);

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
    }
}
