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
using Vials.Server.UnitTests.Utils;

namespace Vials.Server.UnitTests.Utilities
{
    public class PathFinderTests
    {
        [Test]
        public async Task EasyFinding()
        {
            var source = new CancellationTokenSource();
            source.CancelAfter(60000);

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

            var path = await pathFinder.FindPath(set, source.Token);
            path.ShouldNotBeNull();
            path.ShouldNotBeEmpty();
        }

        [Ignore("Slows down test process dramatically.")]
        [Test]
        public async Task HarderFinding()
        {
            var source = new CancellationTokenSource();
            source.CancelAfter(120000);

            var set = new VialSet()
            {
                Vials = new Vial[] {
                    new Vial(new Color[] { Color.LightGreen, Color.Orange, Color.Yellow, Color.LightBlue }),
                    new Vial(new Color[] { Color.DarkGreen, Color.Yellow, Color.Green, Color.LightBlue }),
                    new Vial(new Color[] { Color.Blue, Color.Red, Color.LightGreen, Color.Purple }),
                    new Vial(new Color[] { Color.DarkGrey, Color.Red, Color.LightBlue, Color.Yellow }),
                    new Vial(new Color[] { Color.Yellow, Color.Blue, Color.Pink, Color.DarkGreen }),
                    new Vial(new Color[] { Color.Green, Color.Brown, Color.DarkGreen, Color.Brown }),
                    new Vial(new Color[] { Color.Red, Color.Purple, Color.Blue, Color.Brown }),
                    new Vial(new Color[] { Color.Brown, Color.Blue, Color.Pink, Color.Purple }),
                    new Vial(new Color[] { Color.DarkGrey, Color.LightGreen, Color.Pink, Color.DarkGrey }),
                    new Vial(new Color[] { Color.LightBlue, Color.Green, Color.LightGreen, Color.Orange }),
                    new Vial(new Color[] { Color.Red, Color.Purple, Color.DarkGrey, Color.DarkGreen }),
                    new Vial(new Color[] { Color.Orange, Color.Green, Color.Orange, Color.Pink }),
                    new Vial(),
                    new Vial()
                }
            };

            var pathFinder = new PathFinder(new Cloner());

            var path = await pathFinder.FindPath(set, source.Token);
            path.ShouldNotBeNull();
            path.ShouldNotBeEmpty();
        }

        [Test]
        public async Task Finding6()
        {
            var start = DateTime.Now;

            var path = await TryFindPath(6, 60000); // 1 minute

            var elapsedTime = DateTime.Now - DateTime.Now;

            path.ShouldNotBeNull();
            path.ShouldNotBeEmpty();
        }

        [Ignore("Slows down test process dramatically.")]
        [Test]
        public async Task Finding8()
        {
            var start = DateTime.Now;

            var path = await TryFindPath(8, 60000); // 1 minute

            var elapsedTime = DateTime.Now - DateTime.Now;

            path.ShouldNotBeNull();
            path.ShouldNotBeEmpty();
        }

        [Ignore("Slows down test process dramatically.")]
        [Test]
        public async Task Finding10()
        {
            var start = DateTime.Now;

            var path = await TryFindPath(10, 60000); // 1 minute

            var elapsedTime = DateTime.Now - DateTime.Now;

            path.ShouldNotBeNull();
            path.ShouldNotBeEmpty();
        }

        private async Task<IEnumerable<Pouring>> TryFindPath(int size, int timeOutMilliseconds)
        {
            var source = new CancellationTokenSource();
            source.CancelAfter(timeOutMilliseconds);

            var generator = new SetGenerator(new TestColorStackFactory(), new FakeRandomGenerator());
            var pathFinder = new PathFinder(new Cloner());

            var set = generator.Generate(size, 2, 123, 456);

            return await pathFinder.FindPath(set, source.Token);
        }
    }
}
