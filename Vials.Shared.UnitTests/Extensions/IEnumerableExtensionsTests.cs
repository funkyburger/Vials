using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vials.Core;
using Vials.Shared.Extensions;

namespace Vials.Shared.UnitTests.Extensions
{
    [TestClass]
    public class IEnumerableExtensionsTests
    {
        [TestMethod]
        public void ShufflesKeepsElements()
        {
            var enumerable = new string[] { "toto", "tata", "titi", "tutu" };

            var shuffled = enumerable.Shuffle();

            shuffled.Count().ShouldBe(enumerable.Count());

            var temp = new List<string>();
            foreach (var item in shuffled)
            {
                temp.ShouldNotContain(item);
                enumerable.ShouldContain(item);
                temp.Add(item);
            }
        }
    }
}
