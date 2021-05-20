using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vials.Shared.UnitTests
{
    [TestClass]
    public class VialSetHistoryTests
    {
        [TestMethod]
        public void HistoryIsStacked()
        {
            var history = new VialSetHistory();

            history.Store(new VialSet() { Vials = new Vial[] { new Vial(new Color[] { Color.Red }) } });
            history.Store(new VialSet() { Vials = new Vial[] { new Vial(new Color[] { Color.Red, Color.Blue }) } });
            history.Store(new VialSet() { Vials = new Vial[] { new Vial(new Color[] { Color.Red, Color.Blue, Color.Green }) } });

            history.Current.Vials.Single().Colors.ToArray().ShouldBe(new Color[] { Color.Red, Color.Blue, Color.Green });

            history.GetPrevious().Vials.Single().Colors.ToArray().ShouldBe(new Color[] { Color.Red, Color.Blue });
            history.Current.Vials.Single().Colors.ToArray().ShouldBe(new Color[] { Color.Red, Color.Blue });

            history.GetPrevious().Vials.Single().Colors.ToArray().ShouldBe(new Color[] { Color.Red });
            history.Current.Vials.Single().Colors.ToArray().ShouldBe(new Color[] { Color.Red });

            history.GetPrevious().ShouldBeNull();
            history.Current.Vials.Single().Colors.ToArray().ShouldBe(new Color[] { Color.Red });
        }

        [TestMethod]
        public void NextCanBeFetched()
        {
            var history = new VialSetHistory();

            history.Store(new VialSet() { Vials = new Vial[] { new Vial(new Color[] { Color.Red }) } });
            history.Store(new VialSet() { Vials = new Vial[] { new Vial(new Color[] { Color.Red, Color.Blue }) } });
            history.Store(new VialSet() { Vials = new Vial[] { new Vial(new Color[] { Color.Red, Color.Blue, Color.Green }) } });

            history.GetPrevious();
            history.GetPrevious();

            history.Current.Vials.Single().Colors.ToArray().ShouldBe(new Color[] { Color.Red });

            history.GetNext().Vials.Single().Colors.ToArray().ShouldBe(new Color[] { Color.Red, Color.Blue });
            history.Current.Vials.Single().Colors.ToArray().ShouldBe(new Color[] { Color.Red, Color.Blue });

            history.GetNext().Vials.Single().Colors.ToArray().ShouldBe(new Color[] { Color.Red, Color.Blue, Color.Green });
            history.Current.Vials.Single().Colors.ToArray().ShouldBe(new Color[] { Color.Red, Color.Blue, Color.Green });

            history.GetNext().ShouldBeNull();
            history.Current.Vials.Single().Colors.ToArray().ShouldBe(new Color[] { Color.Red, Color.Blue, Color.Green });
        }

        [TestMethod]
        public void StoringErasesNextOnes()
        {
            var history = new VialSetHistory();

            history.Store(new VialSet() { Vials = new Vial[] { new Vial(new Color[] { Color.Red }) } });
            history.Store(new VialSet() { Vials = new Vial[] { new Vial(new Color[] { Color.Red, Color.Blue }) } });
            history.Store(new VialSet() { Vials = new Vial[] { new Vial(new Color[] { Color.Red, Color.Blue, Color.Green }) } });

            history.GetPrevious();
            history.GetPrevious();

            history.Current.Vials.Single().Colors.ToArray().ShouldBe(new Color[] { Color.Red });

            history.Store(new VialSet() { Vials = new Vial[] { new Vial(new Color[] { Color.Green, Color.Yellow }) } });

            history.Current.Vials.Single().Colors.ToArray().ShouldBe(new Color[] { Color.Green, Color.Yellow });
            history.GetNext().ShouldBeNull();
        }
    }
}
