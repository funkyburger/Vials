using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    }
}
