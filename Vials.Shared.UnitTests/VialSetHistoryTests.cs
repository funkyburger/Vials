using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vials.Shared.Extensions;
using Vials.Shared.Objects;

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
            history.RegisterMove(new Pouring()
            {
                From = 1,
                To = 0
            });

            set.Vials.ElementAt(1).Stack(set.Vials.ElementAt(2).Pop());
            set.Vials.ElementAt(1).Stack(set.Vials.ElementAt(2).Pop());
            history.RegisterMove(new Pouring() { 
                From = 2,
                To = 1,
                Quantity = 2
            });

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
            history.RegisterMove(new Pouring()
            {
                From = 1,
                To = 0
            });

            set.Vials.ElementAt(1).Stack(set.Vials.ElementAt(2).Pop());
            set.Vials.ElementAt(1).Stack(set.Vials.ElementAt(2).Pop());
            history.RegisterMove(new Pouring()
            {
                From = 2,
                To = 1,
                Quantity = 2
            });

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
            history.RegisterMove(new Pouring()
            {
                From = 1,
                To = 0
            });

            set.Vials.ElementAt(1).Stack(set.Vials.ElementAt(2).Pop());
            set.Vials.ElementAt(1).Stack(set.Vials.ElementAt(2).Pop());
            history.RegisterMove(new Pouring()
            {
                From = 2,
                To = 1,
                Quantity = 2
            });

            set = history.Undo(set);
            set = history.Undo(set);

            set.Vials.ElementAt(1).Stack(set.Vials.ElementAt(0).Pop());
            history.RegisterMove(new Pouring()
            {
                From = 0,
                To = 1
            });

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
            history.RegisterMove(new Pouring()
            {
                From = 1,
                To = 0
            });

            set.Vials.ElementAt(1).Stack(set.Vials.ElementAt(2).Pop());
            set.Vials.ElementAt(1).Stack(set.Vials.ElementAt(2).Pop());
            history.RegisterMove(new Pouring()
            {
                From = 2,
                To = 1,
                Quantity = 2
            });

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
            history.RegisterMove(new Pouring()
            {
                From = 1,
                To = 2
            });

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
            history.RegisterMove(new Pouring()
            {
                From = 1,
                To = 0
            });

            history.CanUndo.ShouldBeTrue();

            set.Vials.ElementAt(1).Stack(set.Vials.ElementAt(2).Pop());
            set.Vials.ElementAt(1).Stack(set.Vials.ElementAt(2).Pop());
            history.RegisterMove(new Pouring()
            {
                From = 2,
                To = 1,
                Quantity = 2
            });

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
            history.RegisterMove(new Pouring()
            {
                From = 1,
                To = 0
            });

            history.CanRedo.ShouldBeFalse();

            set.Vials.ElementAt(1).Stack(set.Vials.ElementAt(2).Pop());
            set.Vials.ElementAt(1).Stack(set.Vials.ElementAt(2).Pop());
            history.RegisterMove(new Pouring()
            {
                From = 2,
                To = 1,
                Quantity = 2
            });

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

        [TestMethod]
        public void SetsOngoingPath()
        {
            var history = new VialSetHistory();
            
            history.RegisterMove(new Pouring()
            {
                From = 1,
                To = 2
            });
            history.RegisterMove(new Pouring()
            {
                From = 2,
                To = 0
            });

            history.CanRedo.ShouldBeFalse();

            history.SetOngoingPath(new Pouring[] {
                new Pouring() { From = 0, To = 2, Quantity = 2 },
                new Pouring() { From = 2, To = 1, Quantity = 3 }
            });

            history.CanRedo.ShouldBeTrue();

            var list = history.ToList();

            list.Count.ShouldBe(4);

            list[0].From.ShouldBe(1);
            list[0].To.ShouldBe(2);
            list[0].Quantity.ShouldBe(1);

            list[1].From.ShouldBe(2);
            list[1].To.ShouldBe(0);
            list[1].Quantity.ShouldBe(1);

            list[2].From.ShouldBe(0);
            list[2].To.ShouldBe(2);
            list[2].Quantity.ShouldBe(2);

            list[3].From.ShouldBe(2);
            list[3].To.ShouldBe(1);
            list[3].Quantity.ShouldBe(3);
        }

        [TestMethod]
        public void SetsOngoingPathWillOverwritePreviousPath()
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

            history.RegisterMove(new Pouring()
            {
                From = 1,
                To = 2
            });
            history.RegisterMove(new Pouring()
            {
                From = 2,
                To = 0
            });

            history.RegisterMove(new Pouring()
            {
                From = 1,
                To = 2
            });
            history.RegisterMove(new Pouring()
            {
                From = 2,
                To = 0
            });

            history.Undo(set);
            history.Undo(set);

            history.SetOngoingPath(new Pouring[] {
                new Pouring() { From = 0, To = 2, Quantity = 2 },
                new Pouring() { From = 2, To = 1, Quantity = 3 }
            });

            history.CanRedo.ShouldBeTrue();

            var list = history.ToList();

            list.Count.ShouldBe(4);

            list[0].From.ShouldBe(1);
            list[0].To.ShouldBe(2);
            list[0].Quantity.ShouldBe(1);

            list[1].From.ShouldBe(2);
            list[1].To.ShouldBe(0);
            list[1].Quantity.ShouldBe(1);

            list[2].From.ShouldBe(0);
            list[2].To.ShouldBe(2);
            list[2].Quantity.ShouldBe(2);

            list[3].From.ShouldBe(2);
            list[3].To.ShouldBe(1);
            list[3].Quantity.ShouldBe(3);
        }
    }
}
