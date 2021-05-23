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
    public class VialSetHistoryTests
    {
        [TestMethod]
        public void HistoryIsStacked()
        {
            var history = new VialSetHistory();
            var set = new VialSet() { 
                Vials = new Vial[] 
                { 
                    new Vial(new Color[] { Color.Red }),
                    new Vial(new Color[] { Color.Green, Color.Red }),
                    new Vial(new Color[] { Color.Green, Color.Green }),
                } 
            };

            set.Vials.ElementAt(0).Stack(set.Vials.ElementAt(1).Pop());
            history.RegisterMove(new Pouring(1, 0));

            set.Vials.ElementAt(1).Stack(set.Vials.ElementAt(2).Pop());
            set.Vials.ElementAt(1).Stack(set.Vials.ElementAt(2).Pop());
            history.RegisterMove(new Pouring(2, 1, 2));

            set.ContentEquals(new VialSet()
            {
                Vials = new Vial[]
                {
                    new Vial(new Color[] { Color.Red, Color.Red }),
                    new Vial(new Color[] { Color.Green, Color.Green, Color.Green }),
                    new Vial(new Color[] { }),
                }
            }).ShouldBeTrue();

            set = history.Undo(set);

            set.ContentEquals(new VialSet()
            {
                Vials = new Vial[]
                {
                    new Vial(new Color[] { Color.Red, Color.Red }),
                    new Vial(new Color[] { Color.Green }),
                    new Vial(new Color[] { Color.Green, Color.Green }),
                }
            }).ShouldBeTrue();

            set = history.Undo(set);

            set.ContentEquals(new VialSet()
            {
                Vials = new Vial[]
                {
                    new Vial(new Color[] { Color.Red }),
                    new Vial(new Color[] { Color.Green, Color.Red }),
                    new Vial(new Color[] { Color.Green, Color.Green }),
                }
            }).ShouldBeTrue();

            set = history.Undo(set);

            set.ContentEquals(new VialSet()
            {
                Vials = new Vial[]
                {
                    new Vial(new Color[] { Color.Red }),
                    new Vial(new Color[] { Color.Green, Color.Red }),
                    new Vial(new Color[] { Color.Green, Color.Green }),
                }
            }).ShouldBeTrue();
        }

        [TestMethod]
        public void NextCanBeFetched()
        {
            var history = new VialSetHistory();
            var set = new VialSet()
            {
                Vials = new Vial[]
                {
                    new Vial(new Color[] { Color.Red }),
                    new Vial(new Color[] { Color.Green, Color.Red }),
                    new Vial(new Color[] { Color.Green, Color.Green }),
                }
            };

            set.Vials.ElementAt(0).Stack(set.Vials.ElementAt(1).Pop());
            history.RegisterMove(new Pouring(1, 0));

            set.Vials.ElementAt(1).Stack(set.Vials.ElementAt(2).Pop());
            set.Vials.ElementAt(1).Stack(set.Vials.ElementAt(2).Pop());
            history.RegisterMove(new Pouring(2, 1, 2));

            set = history.Undo(set);
            set = history.Undo(set);

            set = history.Redo(set);

            set.ContentEquals(new VialSet()
            {
                Vials = new Vial[]
                {
                    new Vial(new Color[] { Color.Red, Color.Red }),
                    new Vial(new Color[] { Color.Green }),
                    new Vial(new Color[] { Color.Green, Color.Green }),
                }
            }).ShouldBeTrue();

            set = history.Redo(set);

            set.ContentEquals(new VialSet()
            {
                Vials = new Vial[]
                {
                    new Vial(new Color[] { Color.Red, Color.Red }),
                    new Vial(new Color[] { Color.Green, Color.Green, Color.Green }),
                    new Vial(new Color[] { }),
                }
            }).ShouldBeTrue();

            set = history.Redo(set);

            set.ContentEquals(new VialSet()
            {
                Vials = new Vial[]
                {
                    new Vial(new Color[] { Color.Red, Color.Red }),
                    new Vial(new Color[] { Color.Green, Color.Green, Color.Green }),
                    new Vial(new Color[] { }),
                }
            }).ShouldBeTrue();
        }

        [TestMethod]
        public void StoringErasesNextOnes()
        {
            var history = new VialSetHistory();
            var set = new VialSet()
            {
                Vials = new Vial[]
                {
                    new Vial(new Color[] { Color.Red }),
                    new Vial(new Color[] { Color.Green, Color.Red }),
                    new Vial(new Color[] { Color.Green, Color.Green }),
                }
            };

            set.Vials.ElementAt(0).Stack(set.Vials.ElementAt(1).Pop());
            history.RegisterMove(new Pouring(1, 0));

            set.Vials.ElementAt(1).Stack(set.Vials.ElementAt(2).Pop());
            set.Vials.ElementAt(1).Stack(set.Vials.ElementAt(2).Pop());
            history.RegisterMove(new Pouring(2, 1, 2));

            set = history.Undo(set);
            set = history.Undo(set);

            set.Vials.ElementAt(1).Stack(set.Vials.ElementAt(0).Pop());
            history.RegisterMove(new Pouring(0, 1));

            set.ContentEquals(new VialSet()
            {
                Vials = new Vial[]
                {
                    new Vial(new Color[] { }),
                    new Vial(new Color[] { Color.Green, Color.Red, Color.Red }),
                    new Vial(new Color[] { Color.Green, Color.Green }),
                }
            }).ShouldBeTrue();

            set = history.Redo(set);

            set.ContentEquals(new VialSet()
            {
                Vials = new Vial[]
                {
                    new Vial(new Color[] { }),
                    new Vial(new Color[] { Color.Green, Color.Red, Color.Red }),
                    new Vial(new Color[] { Color.Green, Color.Green }),
                }
            }).ShouldBeTrue();
        }

        [TestMethod]
        public void StoringErasesNextOnes2()
        {
            var history = new VialSetHistory();
            var set = new VialSet()
            {
                Vials = new Vial[]
                {
                    new Vial(new Color[] { Color.Red }),
                    new Vial(new Color[] { Color.Green, Color.Red }),
                    new Vial(new Color[] { Color.Green, Color.Green }),
                }
            };

            set.Vials.ElementAt(0).Stack(set.Vials.ElementAt(1).Pop());
            history.RegisterMove(new Pouring(1, 0));

            set.Vials.ElementAt(1).Stack(set.Vials.ElementAt(2).Pop());
            set.Vials.ElementAt(1).Stack(set.Vials.ElementAt(2).Pop());
            history.RegisterMove(new Pouring(2, 1, 2));

            set.ContentEquals(new VialSet()
            {
                Vials = new Vial[]
                {
                    new Vial(new Color[] { Color.Red, Color.Red}),
                    new Vial(new Color[] { Color.Green, Color.Green, Color.Green }),
                    new Vial(new Color[] { }),
                }
            }).ShouldBeTrue();

            set = history.Undo(set);

            set.ContentEquals(new VialSet()
            {
                Vials = new Vial[]
                {
                    new Vial(new Color[] { Color.Red, Color.Red}),
                    new Vial(new Color[] { Color.Green }),
                    new Vial(new Color[] { Color.Green, Color.Green }),
                }
            }).ShouldBeTrue();

            set.Vials.ElementAt(2).Stack(set.Vials.ElementAt(1).Pop());
            history.RegisterMove(new Pouring(1, 2));

            set.ContentEquals(new VialSet()
            {
                Vials = new Vial[]
                {
                    new Vial(new Color[] { Color.Red, Color.Red}),
                    new Vial(new Color[] { }),
                    new Vial(new Color[] { Color.Green, Color.Green, Color.Green }),
                }
            }).ShouldBeTrue();

            set = history.Undo(set);

            set.ContentEquals(new VialSet()
            {
                Vials = new Vial[]
                {
                    new Vial(new Color[] { Color.Red, Color.Red}),
                    new Vial(new Color[] { Color.Green }),
                    new Vial(new Color[] { Color.Green, Color.Green }),
                }
            }).ShouldBeTrue();

            set = history.Redo(set);

            set.ContentEquals(new VialSet()
            {
                Vials = new Vial[]
                {
                    new Vial(new Color[] { Color.Red, Color.Red}),
                    new Vial(new Color[] { }),
                    new Vial(new Color[] { Color.Green, Color.Green, Color.Green }),
                }
            }).ShouldBeTrue();
        }

        [TestMethod]
        public void IndicatesIfCanUndo()
        {
            var history = new VialSetHistory();
            var set = new VialSet()
            {
                Vials = new Vial[]
                {
                    new Vial(new Color[] { Color.Red }),
                    new Vial(new Color[] { Color.Green, Color.Red }),
                    new Vial(new Color[] { Color.Green, Color.Green }),
                }
            };

            history.CanUndo.ShouldBeFalse();

            set.Vials.ElementAt(0).Stack(set.Vials.ElementAt(1).Pop());
            history.RegisterMove(new Pouring(1, 0));

            history.CanUndo.ShouldBeTrue();

            set.Vials.ElementAt(1).Stack(set.Vials.ElementAt(2).Pop());
            set.Vials.ElementAt(1).Stack(set.Vials.ElementAt(2).Pop());
            history.RegisterMove(new Pouring(2, 1, 2));

            history.CanUndo.ShouldBeTrue();

            history.Undo(set);
            history.CanUndo.ShouldBeTrue();

            history.Undo(set);
            history.CanUndo.ShouldBeFalse();

            history.Redo(set);
            history.CanUndo.ShouldBeTrue();
        }

        [TestMethod]
        public void IndicatesIfCanRedo()
        {
            var history = new VialSetHistory();
            var set = new VialSet()
            {
                Vials = new Vial[]
                {
                    new Vial(new Color[] { Color.Red }),
                    new Vial(new Color[] { Color.Green, Color.Red }),
                    new Vial(new Color[] { Color.Green, Color.Green }),
                }
            };

            history.CanRedo.ShouldBeFalse();

            set.Vials.ElementAt(0).Stack(set.Vials.ElementAt(1).Pop());
            history.RegisterMove(new Pouring(1, 0));

            history.CanRedo.ShouldBeFalse();

            set.Vials.ElementAt(1).Stack(set.Vials.ElementAt(2).Pop());
            set.Vials.ElementAt(1).Stack(set.Vials.ElementAt(2).Pop());
            history.RegisterMove(new Pouring(2, 1, 2));

            history.CanRedo.ShouldBeFalse();

            history.Undo(set);
            history.CanRedo.ShouldBeTrue();

            history.Undo(set);
            history.CanRedo.ShouldBeTrue();

            history.Redo(set);
            history.CanRedo.ShouldBeTrue();

            history.Redo(set);
            history.CanRedo.ShouldBeFalse();
        }
    }
}
