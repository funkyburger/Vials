using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Shouldly;
using System.Linq;
using Vials.Shared.Objects;

namespace Vials.Shared.UnitTests
{
    [TestClass]
    public class VialTests
    {
        [TestMethod]
        public void TopColor()
        {
            var vial = new Vial();

            vial.TopColor.ShouldBe(Color.None);

            vial.Stack(Color.Red);
            vial.TopColor.ShouldBe(Color.Red);

            vial.Stack(new Color[] { Color.Blue, Color.Green });
            vial.TopColor.ShouldBe(Color.Green);

            vial = new Vial(new Color[] { Color.Blue, Color.Yellow });
            vial.TopColor.ShouldBe(Color.Yellow);
        }

        [TestMethod]
        public void IsFull()
        {
            var vial = new Vial();
            vial.IsFull.ShouldBeFalse();

            vial.Stack(Color.Red);
            vial.IsFull.ShouldBeFalse();

            vial.Stack(Color.Green);
            vial.IsFull.ShouldBeFalse();

            vial.Stack(Color.Red);
            vial.IsFull.ShouldBeFalse();

            vial.Stack(Color.Yellow);
            vial.IsFull.ShouldBeTrue();

            vial = new Vial(new Color[] { Color.Blue, Color.Green, Color.Blue, Color.Green });
            vial.IsFull.ShouldBeTrue();
        }

        [TestMethod]
        public void IsCompleteMeansAllColorAreSame()
        {
            var vial = new Vial(new Color[] { Color.Blue, Color.Green, Color.Blue, Color.Blue });
            vial.IsComplete.ShouldBeFalse();

            vial = new Vial(new Color[] { Color.Blue, Color.Blue, Color.Blue, Color.Blue });
            vial.IsComplete.ShouldBeTrue();
        }

        [TestMethod]
        public void IsCompleteMeansVialIsFull()
        {
            var vial = new Vial(new Color[] { Color.Blue, Color.Blue, Color.Blue });
            vial.IsComplete.ShouldBeFalse();

            vial.Stack(Color.Blue);
            vial.IsComplete.ShouldBeTrue();
        }

        [TestMethod]
        public void Poping()
        {
            var vial = new Vial(new Color[] { Color.Blue, Color.Yellow, Color.Green });

            var color = vial.Pop();

            color.ShouldBe(Color.Green);
            vial.Colors.ToArray().ShouldBe(new Color[] { Color.Blue, Color.Yellow });
        }

        [TestMethod]
        public void PopingFromEmptyThrowsException()
        {
            var vial = new Vial();
            var exceptionThrown = false;

            try
            {
                vial.Pop();
            }
            catch
            {
                exceptionThrown = true;
            }

            exceptionThrown.ShouldBeTrue();
        }

        [TestMethod]
        public void CreatingVialWithTooManyColorsThrowsException()
        {
            var exceptionThrown = false;

            try
            {
                var vial = new Vial(new Color[] { Color.Blue, Color.Yellow, Color.Green, Color.Blue, Color.Red });
            }
            catch
            {
                exceptionThrown = true;
            }

            exceptionThrown.ShouldBeTrue();
        }

        [TestMethod]
        public void StackingTooManyColorsThrowsException()
        {
            var vial = new Vial(new Color[] { Color.Blue, Color.Yellow, Color.Green, Color.Blue });
            var exceptionThrown = false;

            try
            {
                vial.Stack(Color.Red);
            }
            catch
            {
                exceptionThrown = true;
            }

            exceptionThrown.ShouldBeTrue();
            exceptionThrown = false;

            vial = new Vial();

            try
            {
                vial.Stack(new Color[] { Color.Blue, Color.Yellow, Color.Green, Color.Blue, Color.Red });
            }
            catch
            {
                exceptionThrown = true;
            }

            exceptionThrown.ShouldBeTrue();
        }

        [TestMethod]
        public void StackingNoneThrowsException()
        {
            var vial = new Vial();
            var exceptionThrown = false;

            try
            {
                vial.Stack(new Color[] { Color.Blue, Color.Yellow, Color.None, Color.Blue });
            }
            catch
            {
                exceptionThrown = true;
            }

            exceptionThrown.ShouldBeTrue();
        }

        [TestMethod]
        public void HashShouldBeSameForSameSet()
        {
            var vial = new Vial(new Color[] { Color.Red, Color.Yellow, Color.Red, Color.Green });
            var vial2 = new Vial(new Color[] { Color.Red, Color.Yellow, Color.Red, Color.Green });

            vial.GetHashCode().ShouldBe(vial2.GetHashCode());
        }
    }
}
