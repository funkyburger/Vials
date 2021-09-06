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
using Vials.Shared.Extensions;
using Vials.Server.Game;
using Vials.Shared.Exceptions;

namespace Vials.Server.UnitTests.Game
{
    public class FinishedGameHelperTests
    {
        [Test]
        public async Task MovesAreExtracted()
        {
            var helper = new FinishedGameHelper();
            var game = new FinishedGame()
            {
                Seed = 123,
                FootPrint = 456,
                Track = new long[] { 123, 456, 789, 147, 258, 790, 369, 789, 791 }
            };

            helper.ExtractMoves(game).IsEqualTo(new Tuple<long, long, long>[] { 
                new Tuple<long, long, long>(123, 456, 789),
                new Tuple<long, long, long>(147, 258, 790),
                new Tuple<long, long, long>(369, 789, 791)
            });
        }

        [Test]
        public async Task MovesLengthShouldBeMultipleOfThree()
        {
            var helper = new FinishedGameHelper();
            var game = new FinishedGame()
            {
                Seed = 123,
                FootPrint = 456,
                Track = new long[] { 123, 456, 789, 147, 258, 790, 369, 789 }
            };

            Assert.Throws<GameCheckException>(() => helper.ExtractMoves(game).ToArray());
        }

        [Test]
        public async Task TimestampsAreExtracted()
        {
            var helper = new FinishedGameHelper();
            var game = new FinishedGame()
            {
                Seed = 123,
                FootPrint = 456,
                Track = new long[] { 123, 456, 789, 147, 258, 790, 369, 789, 791 }
            };

            helper.ExtractTimestamps(game).IsEqualTo(new long[] { 789, 790, 791 });
        }
    }
}
