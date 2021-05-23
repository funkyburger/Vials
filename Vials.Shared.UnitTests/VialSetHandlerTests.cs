using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shouldly;

namespace Vials.Shared.UnitTests
{
    [TestClass]
    public class VialSetHandlerTests
    {
        [TestMethod]
        public void VialSelection()
        {
            var set = GenerateTestSet();

            var handler = new VialSetHandler();

            var resultingSet = handler.Select(set, 2);

            var vials = resultingSet.Vials.ToArray();

            for (int i = 0; i < vials.Length; i++)
            {
                if(i == 2)
                {
                    vials[i].IsSelected.ShouldBeTrue();
                }
                else
                {
                    vials[i].IsSelected.ShouldBeFalse();
                }
            }
        }

        [TestMethod]
        public void VialSelectionAndUnselection()
        {
            var set = GenerateTestSet();

            var handler = new VialSetHandler();

            var resultingSet = handler.Select(set, 3);
            resultingSet.Vials.ElementAt(3).IsSelected.ShouldBeTrue();
            resultingSet = handler.Select(set, 3);

            var vials = resultingSet.Vials.ToArray();

            foreach(var vial in vials)
            {
                vial.IsSelected.ShouldBeFalse();
            }
        }

        [TestMethod]
        public void VialPouring()
        {
            var set = GenerateTestSetWithSomeMovesDone();

            var handler = new VialSetHandler();

            var resultingSet = handler.Select(set, 0);
            resultingSet.Vials.First().IsSelected.ShouldBeTrue();

            resultingSet = handler.Select(set, 5);

            var vials = resultingSet.Vials.ToArray();
            foreach (var vial in vials)
            {
                vial.IsSelected.ShouldBeFalse();
            }

            vials[0].Colors.ToArray().ShouldBe(new Color[] { Color.Red, Color.Yellow });
            vials[5].Colors.ToArray().ShouldBe(new Color[] { Color.Green, Color.Green });
        }

        [TestMethod]
        public void VialMultiplePouring()
        {
            var set = new VialSet()
            {
                Vials = new Vial[] {
                    new Vial(new Color[] { Color.Red, Color.Green, Color.Green }),
                    new Vial(new Color[] { Color.Green })
                }
            };

            var handler = new VialSetHandler();

            var resultingSet = handler.Select(set, 0);
            resultingSet.Vials.First().IsSelected.ShouldBeTrue();

            resultingSet = handler.Select(set, 1);

            var vials = resultingSet.Vials.ToArray();
            foreach (var vial in vials)
            {
                vial.IsSelected.ShouldBeFalse();
            }

            vials[0].Colors.ToArray().ShouldBe(new Color[] { Color.Red, });
            vials[1].Colors.ToArray().ShouldBe(new Color[] { Color.Green, Color.Green, Color.Green });
        }

        [TestMethod]
        public void VialMultiplePouringInEmptyVial()
        {
            var set = new VialSet()
            {
                Vials = new Vial[] {
                    new Vial(new Color[] { Color.Red, Color.Green, Color.Green }),
                    new Vial()
                }
            };

            var handler = new VialSetHandler();

            var resultingSet = handler.Select(set, 0);
            resultingSet.Vials.First().IsSelected.ShouldBeTrue();

            resultingSet = handler.Select(set, 1);

            var vials = resultingSet.Vials.ToArray();
            foreach (var vial in vials)
            {
                vial.IsSelected.ShouldBeFalse();
            }

            vials[0].Colors.ToArray().ShouldBe(new Color[] { Color.Red, });
            vials[1].Colors.ToArray().ShouldBe(new Color[] { Color.Green, Color.Green });
        }

        [TestMethod]
        public void VialMultiplePouringOverflowingVial()
        {
            var set = new VialSet()
            {
                Vials = new Vial[] {
                    new Vial(new Color[] { Color.Red, Color.Green, Color.Green }),
                    new Vial(new Color[] { Color.Red, Color.Yellow, Color.Green })
                }
            };

            var handler = new VialSetHandler();

            var resultingSet = handler.Select(set, 0);
            resultingSet.Vials.First().IsSelected.ShouldBeTrue();

            resultingSet = handler.Select(set, 1);

            var vials = resultingSet.Vials.ToArray();
            foreach (var vial in vials)
            {
                vial.IsSelected.ShouldBeFalse();
            }

            vials[0].Colors.ToArray().ShouldBe(new Color[] { Color.Red, Color.Green });
            vials[1].Colors.ToArray().ShouldBe(new Color[] { Color.Red, Color.Yellow, Color.Green, Color.Green });
        }

        [TestMethod]
        public void PouringOnWrongColor()
        {
            var set = GenerateTestSetWithSomeMovesDone();

            var handler = new VialSetHandler();

            var resultingSet = handler.Select(set, 0);
            resultingSet.Vials.First().IsSelected.ShouldBeTrue();

            resultingSet = handler.Select(set, 2);

            resultingSet = handler.Select(set, 0);
            resultingSet = handler.Select(set, 3);

            var vials = resultingSet.Vials.ToArray();

            vials[0].Colors.ToArray().ShouldBe(new Color[] { Color.Red, Color.Yellow, Color.Green });
            vials[2].Colors.ToArray().ShouldBe(new Color[] { Color.Yellow, Color.Red, Color.Blue });
        }

        [TestMethod]
        public void PouringInFullVial()
        {
            var set = GenerateTestSetWithSomeMovesDone();

            var handler = new VialSetHandler();

            var resultingSet = handler.Select(set, 0);
            resultingSet.Vials.First().IsSelected.ShouldBeTrue();

            resultingSet = handler.Select(set, 5);

            var vials = resultingSet.Vials.ToArray();
            
            vials[0].Colors.ToArray().ShouldBe(new Color[] { Color.Red, Color.Yellow });
            vials[3].Colors.ToArray().ShouldBe(new Color[] { Color.Red, Color.Blue, Color.Green, Color.Yellow });
            vials[5].Colors.ToArray().ShouldBe(new Color[] { Color.Green, Color.Green });
        }

        [TestMethod]
        public void SelectionShowOptions()
        {
            var set = new VialSet()
            {
                Vials = new Vial[] {
                    new Vial(new Color[] { Color.Red, Color.Yellow, Color.Green }),
                    new Vial(new Color[] { Color.Blue, Color.Yellow, Color.Green, Color.Red }),
                    new Vial(new Color[] { Color.Yellow, Color.Red, Color.Green }),
                    new Vial(new Color[] { Color.Red, Color.Blue, Color.Yellow, Color.Green }),
                    new Vial(),
                    new Vial()
                }
            };

            var handler = new VialSetHandler();

            var resultingSet = handler.Select(set, 2);
            
            var vials = resultingSet.Vials.ToArray();

            vials[0].IsOption.ShouldBeTrue();
            vials[1].IsOption.ShouldBeFalse();
            vials[2].IsOption.ShouldBeFalse();
            vials[3].IsOption.ShouldBeFalse();
            vials[4].IsOption.ShouldBeTrue();
            vials[5].IsOption.ShouldBeTrue();
        }

        [TestMethod]
        public void DeselectionClearsOptions()
        {
            var set = new VialSet()
            {
                Vials = new Vial[] {
                    new Vial(new Color[] { Color.Red, Color.Yellow, Color.Green }),
                    new Vial(new Color[] { Color.Blue, Color.Yellow, Color.Green, Color.Red }),
                    new Vial(new Color[] { Color.Yellow, Color.Red, Color.Green }),
                    new Vial(new Color[] { Color.Red, Color.Blue, Color.Yellow, Color.Green }),
                    new Vial(),
                    new Vial()
                }
            };

            var handler = new VialSetHandler();

            var resultingSet = handler.Select(set, 2);
            resultingSet = handler.Select(resultingSet, 2);

            foreach(var vial in resultingSet.Vials)
            {
                vial.IsOption.ShouldBeFalse();
            }
        }

        [TestMethod]
        public void PouringClearsOptions()
        {
            var set = new VialSet()
            {
                Vials = new Vial[] {
                    new Vial(new Color[] { Color.Red, Color.Yellow, Color.Green }),
                    new Vial(new Color[] { Color.Blue, Color.Yellow, Color.Green, Color.Red }),
                    new Vial(new Color[] { Color.Yellow, Color.Red, Color.Green }),
                    new Vial(new Color[] { Color.Red, Color.Blue, Color.Yellow, Color.Green }),
                    new Vial(),
                    new Vial()
                }
            };

            var handler = new VialSetHandler();

            var resultingSet = handler.Select(set, 2);
            resultingSet = handler.Select(resultingSet, 4);

            foreach (var vial in resultingSet.Vials)
            {
                vial.IsOption.ShouldBeFalse();
            }
        }

        [TestMethod]
        public void SetIsMarkedAsChanged()
        {
            var set = new VialSet()
            {
                Vials = new Vial[] {
                    new Vial(new Color[] { Color.Red, Color.Yellow, Color.Green }),
                    new Vial(new Color[] { Color.Blue, Color.Yellow, Color.Green, Color.Red }),
                    new Vial(new Color[] { Color.Yellow, Color.Red, Color.Green }),
                    new Vial(new Color[] { Color.Red, Color.Blue, Color.Yellow, Color.Green }),
                    new Vial(),
                    new Vial()
                }
            };

            var handler = new VialSetHandler();

            var resultingSet = handler.Select(set, 3);
            resultingSet = handler.Select(resultingSet, 2);

            resultingSet.HasChanged.ShouldBeTrue();
        }

        [TestMethod]
        public void SetIsntMarkedAsChangedIfNoMoveIsMade()
        {
            var set = new VialSet()
            {
                Vials = new Vial[] {
                    new Vial(new Color[] { Color.Red, Color.Yellow, Color.Green }),
                    new Vial(new Color[] { Color.Blue, Color.Yellow, Color.Green, Color.Red }),
                    new Vial(new Color[] { Color.Yellow, Color.Red, Color.Green }),
                    new Vial(new Color[] { Color.Red, Color.Blue, Color.Yellow, Color.Green }),
                    new Vial(),
                    new Vial()
                }
            };

            var handler = new VialSetHandler();

            var resultingSet = handler.Select(set, 2);
            resultingSet = handler.Select(resultingSet, 3);

            resultingSet.HasChanged.ShouldBeFalse();
        }

        [TestMethod]
        public void SetIsntMarkedAsChangedOnSelect()
        {
            var set = new VialSet()
            {
                Vials = new Vial[] {
                    new Vial(new Color[] { Color.Red, Color.Yellow, Color.Green }),
                    new Vial(new Color[] { Color.Blue, Color.Yellow, Color.Green, Color.Red }),
                    new Vial(new Color[] { Color.Yellow, Color.Red, Color.Green }),
                    new Vial(new Color[] { Color.Red, Color.Blue, Color.Yellow, Color.Green }),
                    new Vial(),
                    new Vial()
                }
            };

            var handler = new VialSetHandler();

            var resultingSet = handler.Select(set, 2);

            resultingSet.HasChanged.ShouldBeFalse();
        }

        private VialSet GenerateTestSet()
        {
            return new VialSet()
            {
                Vials = new Vial[] {
                    new Vial(new Color[] { Color.Red, Color.Yellow, Color.Green, Color.Blue }),
                    new Vial(new Color[] { Color.Blue, Color.Yellow, Color.Green, Color.Red }),
                    new Vial(new Color[] { Color.Yellow, Color.Red, Color.Blue, Color.Green }),
                    new Vial(new Color[] { Color.Red, Color.Blue, Color.Green, Color.Yellow }),
                    new Vial(),
                    new Vial()
                }
            };
        }

        private VialSet GenerateTestSetWithSomeMovesDone()
        {
            return new VialSet()
            {
                Vials = new Vial[] {
                    new Vial(new Color[] { Color.Red, Color.Yellow, Color.Green }),
                    new Vial(new Color[] { Color.Blue, Color.Yellow, Color.Green, Color.Red }),
                    new Vial(new Color[] { Color.Yellow, Color.Red, Color.Blue }),
                    new Vial(new Color[] { Color.Red, Color.Blue, Color.Green, Color.Yellow }),
                    new Vial(new Color[] { Color.Blue }),
                    new Vial(new Color[] { Color.Green })
                }
            };
        }

    }
}
