using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Text;
using Vials.Shared.Objects;

namespace Vials.Shared.UnitTests
{
    [TestClass]
    public class VialSetTests
    {
        [TestMethod]
        public void CompletedIsComplete()
        {
            var set = new VialSet()
            {
                Vials = new Vial[] {
                    new Vial(new Color[] { Color.Red, Color.Red, Color.Red, Color.Red }),
                    new Vial(new Color[] { Color.Blue, Color.Blue, Color.Blue, Color.Blue }),
                    new Vial(new Color[] { Color.Yellow, Color.Yellow, Color.Yellow, Color.Yellow }),
                    new Vial(new Color[] { Color.Green, Color.Green, Color.Green, Color.Green }),
                    new Vial(),
                    new Vial()
                }
            };

            set.IsComplete.ShouldBeTrue();
        }

        [TestMethod]
        public void NotFullIsntComplete()
        {
            var set = new VialSet()
            {
                Vials = new Vial[] {
                    new Vial(new Color[] { Color.Red, Color.Red, Color.Red, Color.Red }),
                    new Vial(new Color[] { Color.Blue, Color.Blue, Color.Blue }),
                    new Vial(new Color[] { Color.Yellow, Color.Yellow, Color.Yellow, Color.Yellow }),
                    new Vial(new Color[] { Color.Green, Color.Green, Color.Green, Color.Green }),
                    new Vial(),
                    new Vial()
                }
            };

            set.IsComplete.ShouldBeFalse();
        }

        [TestMethod]
        public void NotMatchingIsntComplete()
        {
            var set = new VialSet()
            {
                Vials = new Vial[] {
                    new Vial(new Color[] { Color.Red, Color.Red, Color.Red, Color.Red }),
                    new Vial(new Color[] { Color.Blue, Color.Yellow, Color.Blue, Color.Blue }),
                    new Vial(new Color[] { Color.Yellow, Color.Yellow, Color.Yellow, Color.Yellow }),
                    new Vial(new Color[] { Color.Green, Color.Green, Color.Green, Color.Green }),
                    new Vial(),
                    new Vial()
                }
            };

            set.IsComplete.ShouldBeFalse();
        }

        [TestMethod]
        public void HashShouldBeSameForSameSet()
        {
            var set = new VialSet()
            {
                Vials = new Vial[] {
                    new Vial(new Color[] { Color.Red, Color.Red, Color.Red, Color.Red }),
                    new Vial(new Color[] { Color.Blue, Color.Yellow, Color.Blue, Color.Blue }),
                    new Vial(new Color[] { Color.Yellow, Color.Yellow, Color.Yellow, Color.Yellow }),
                    new Vial(new Color[] { Color.Green, Color.Green, Color.Green, Color.Green }),
                    new Vial(),
                    new Vial()
                }
            };

            var set2 = new VialSet()
            {
                Vials = new Vial[] {
                    new Vial(new Color[] { Color.Red, Color.Red, Color.Red, Color.Red }),
                    new Vial(new Color[] { Color.Blue, Color.Yellow, Color.Blue, Color.Blue }),
                    new Vial(new Color[] { Color.Yellow, Color.Yellow, Color.Yellow, Color.Yellow }),
                    new Vial(new Color[] { Color.Green, Color.Green, Color.Green, Color.Green }),
                    new Vial(),
                    new Vial()
                }
            };

            set.GetHashCode().ShouldBe(set2.GetHashCode());
        }
    }
}
