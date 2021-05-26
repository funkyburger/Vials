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
    public class PathFinderTests
    {
        [TestMethod]
        public void EasyFinding()
        {
            var set = new VialSet()
            {
                Vials = new Vial[] {
                    new Vial(new Color[] { Color.Blue, Color.Blue, Color.Red, Color.Yellow }),
                    new Vial(new Color[] { Color.LightBlue, Color.LightBlue, Color.Yellow, Color.LightBlue }),
                    new Vial(new Color[] { Color.LightBlue, Color.Yellow, Color.Yellow, Color.Blue }),
                    new Vial(new Color[] { Color.Red, Color.Red, Color.Red, Color.Blue }),
                    new Vial(),
                    new Vial()
                }
            };

            var pathFinder = new PathFinder(new Cloner());

            var path = pathFinder.FindPath(set);
            path.ShouldNotBeNull();
            path.ShouldNotBeEmpty();
        }
    }
}
