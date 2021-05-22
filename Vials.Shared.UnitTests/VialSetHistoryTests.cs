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

        [TestMethod]
        public void HistoryCanBeEnumerated()
        {
            var history = new VialSetHistory();

            history.Store(new VialSet() { Vials = new Vial[] { new Vial(new Color[] { Color.Red }) } });
            history.Store(new VialSet() { Vials = new Vial[] { new Vial(new Color[] { Color.Red, Color.Blue }) } });
            history.Store(new VialSet() { Vials = new Vial[] { new Vial(new Color[] { Color.Red, Color.Blue, Color.Green }) } });

            int index = 0;
            foreach (var item in history)
            {
                if(index == 0)
                {
                    item.Vials.Single().Colors.ToArray().ShouldBe(new Color[] { Color.Red });
                }
                else if (index == 1)
                {
                    item.Vials.Single().Colors.ToArray().ShouldBe(new Color[] { Color.Red, Color.Blue });
                }
                else if (index == 2)
                {
                    item.Vials.Single().Colors.ToArray().ShouldBe(new Color[] { Color.Red, Color.Blue, Color.Green });
                }

                index++;
            }
        }

        [TestMethod]
        public void ClearHistory()
        {
            var history = new VialSetHistory();

            history.Store(new VialSet() { Vials = new Vial[] { new Vial(new Color[] { Color.Red }) } });
            history.Store(new VialSet() { Vials = new Vial[] { new Vial(new Color[] { Color.Red, Color.Blue }) } });
            history.Store(new VialSet() { Vials = new Vial[] { new Vial(new Color[] { Color.Red, Color.Blue, Color.Green }) } });

            history.GetPrevious().ShouldNotBeNull();
            history.Any().ShouldBeTrue();

            history.Clear();

            history.Any().ShouldBeFalse();

            history.Current.ShouldBeNull();
            history.GetPrevious().ShouldBeNull();
            history.GetNext().ShouldBeNull();
        }
    }
}
