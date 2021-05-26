using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vials.Shared.Extensions;

namespace Vials.Shared.UnitTests
{
    [TestClass]
    public class ClonerTests
    {
        [TestMethod]
        public void CanCloneVials() 
        {
            var cloner = new Cloner();

            var vial = new Vial(new Color[] { Color.Blue, Color.Brown, Color.DarkGreen });
            var same = vial;
            var clone = cloner.Clone(vial);
            
            same.Stack(Color.Red);
            clone.Stack(Color.Yellow);

            vial.Colors.IsEqualTo(new Color[] { Color.Blue, Color.Brown, Color.DarkGreen, Color.Red }).ShouldBeTrue();
            clone.Colors.IsEqualTo(new Color[] { Color.Blue, Color.Brown, Color.DarkGreen, Color.Yellow }).ShouldBeTrue();
        }

        [TestMethod]
        public void CanCloneVialsets()
        {
            var cloner = new Cloner();

            var set = new VialSet()
            {
                Vials = new Vial[] {
                    new Vial(new Color[] { Color.Blue, Color.Brown, Color.DarkGreen }),
                    new Vial(new Color[] { Color.Red, Color.Pink, Color.LightBlue }),
                    new Vial(new Color[] { Color.Purple, Color.Orange, Color.DarkGrey })
                }
            };

            var same = set;
            var clone = cloner.Clone(set);

            same.Vials.First().Stack(Color.Red);
            clone.Vials.First().Stack(Color.Yellow);

            set.Vials.First().Colors.IsEqualTo(new Color[] { Color.Blue, Color.Brown, Color.DarkGreen, Color.Red }).ShouldBeTrue();
            clone.Vials.First().Colors.IsEqualTo(new Color[] { Color.Blue, Color.Brown, Color.DarkGreen, Color.Yellow }).ShouldBeTrue();
        }
    }
}
