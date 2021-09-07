using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using Vials.Shared.Objects;
using Vials.Server.Utilities;
using Vials.Shared;
using Vials.Shared.Extensions;
using Vials.Server.UnitTests.Utils;

namespace Vials.Server.UnitTests.Utilities
{
    public class SetMoveMakerTests
    {
        [Test]
        public void MoveIsMade()
        {
            var moveMaker = new SetMoveMaker();
            var set = new VialSet()
            {
                Vials = new Vial[] {
                    new Vial() { FootPrint = 123, Colors = new Color[] { Color.Blue, Color.Blue, Color.Blue, Color.Blue } },
                    new Vial() { FootPrint = 456, Colors = new Color[] { Color.Blue, Color.Blue, Color.Blue, Color.Blue } },
                    new Vial() { FootPrint = 789, Colors = new Color[] { Color.Blue, Color.Blue, Color.Blue, Color.Blue } },
                    new Vial() { FootPrint = 258, Colors = new Color[] { Color.Blue, Color.Blue, Color.Blue, } },
                }
            };

            moveMaker.AppliMove(set, 123, 258);

            set.Vials.First().Colors.IsEqualTo(new Color[] { Color.Blue, Color.Blue, Color.Blue });
            set.Vials.Skip(3).First().Colors.IsEqualTo(new Color[] { Color.Blue, Color.Blue, Color.Blue, Color.Blue });
        }

        [Test]
        public void MultipleMoveAreMade()
        {
            var moveMaker = new SetMoveMaker();
            var set = new VialSet()
            {
                Vials = new Vial[] {
                    new Vial() { FootPrint = 123, Colors = new Color[] { Color.Blue, Color.Blue, Color.Blue, Color.Blue } },
                    new Vial() { FootPrint = 456, Colors = new Color[] { Color.Blue, Color.Blue, Color.Blue, Color.Blue } },
                    new Vial() { FootPrint = 789, Colors = new Color[] { Color.Blue, Color.Blue, Color.Blue, Color.Blue } },
                    new Vial() { FootPrint = 258, Colors = new Color[] { Color.Blue, } },
                }
            };

            moveMaker.AppliMove(set, 123, 258);

            set.Vials.First().Colors.IsEqualTo(new Color[] { Color.Blue });
            set.Vials.Skip(3).First().Colors.IsEqualTo(new Color[] { Color.Blue, Color.Blue, Color.Blue, Color.Blue });
        }

        [Test]
        public void MoveIsMadeOnlyOnSameColor()
        {
            var moveMaker = new SetMoveMaker();
            var set = new VialSet()
            {
                Vials = new Vial[] {
                    new Vial() { FootPrint = 123, Colors = new Color[] { Color.Blue, Color.Blue, Color.Blue, Color.Blue } },
                    new Vial() { FootPrint = 456, Colors = new Color[] { Color.Blue, Color.Blue, Color.Blue, Color.Blue } },
                    new Vial() { FootPrint = 789, Colors = new Color[] { Color.Blue, Color.Blue, Color.Blue, Color.Blue } },
                    new Vial() { FootPrint = 258, Colors = new Color[] { Color.Green, } },
                }
            };

            moveMaker.AppliMove(set, 123, 258);

            set.Vials.First().Colors.IsEqualTo(new Color[] { Color.Blue, Color.Blue, Color.Blue, Color.Blue });
            set.Vials.Skip(3).First().Colors.IsEqualTo(new Color[] { Color.Green });
        }

        [Test]
        public void AllColorStackIsMoved()
        {
            var moveMaker = new SetMoveMaker();
            var set = new VialSet()
            {
                Vials = new Vial[] {
                    new Vial() { FootPrint = 123, Colors = new Color[] { Color.Red, Color.Green } },
                    new Vial() { FootPrint = 456, Colors = new Color[] { Color.Blue, Color.Blue, Color.Blue, Color.Blue } },
                    new Vial() { FootPrint = 789, Colors = new Color[] { Color.Blue, Color.Blue, Color.Blue, Color.Blue } },
                    new Vial() { FootPrint = 258, Colors = new Color[] { Color.Green, } },
                }
            };

            moveMaker.AppliMove(set, 123, 258);

            set.Vials.First().Colors.IsEqualTo(new Color[] { Color.Red });
            set.Vials.Skip(3).First().Colors.IsEqualTo(new Color[] { Color.Green, Color.Green });
        }
    }
}
