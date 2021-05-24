using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vials.Core.UnitTests.Utils;
using Vials.Shared;

namespace Vials.Core.UnitTests
{
    [TestClass]
    public class SetGeneratorTests
    {
        [TestMethod]
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

        [TestMethod]
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
