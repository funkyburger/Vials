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
using Vials.Server.Game.Check;
using Vials.Shared.Exceptions;
using Vials.Server.Game;
using Moq;

namespace Vials.Server.UnitTests.Game.Check
{
    public class ChronologyCheckTests
    {
        [Test]
        public async Task GoodChronologyIsValidated()
        {
            var finishedGameHelper = new Mock<IFinishedGameHelper>();
            finishedGameHelper.Setup(h => h.ExtractTimestamps(It.IsAny<FinishedGame>()))
                .Returns(new long[] { 789, 790, 791 });

            var check = new ChronologyCheck(finishedGameHelper.Object);

            check.Check(new FinishedGame());
        }

        [Test]
        public async Task BadChronologyThrowsAnException()
        {
            var finishedGameHelper = new Mock<IFinishedGameHelper>();
            finishedGameHelper.Setup(h => h.ExtractTimestamps(It.IsAny<FinishedGame>()))
                .Returns(new long[] { 789, 792, 791 });

            var check = new ChronologyCheck(finishedGameHelper.Object);
            
            Assert.Throws<GameCheckException>(() => check.Check(new FinishedGame()));
        }

        [Test]
        public async Task SimulataneousActionsThrowsAnException()
        {
            var finishedGameHelper = new Mock<IFinishedGameHelper>();
            finishedGameHelper.Setup(h => h.ExtractTimestamps(It.IsAny<FinishedGame>()))
                .Returns(new long[] { 789, 790, 790 });

            var check = new ChronologyCheck(finishedGameHelper.Object);
            
            Assert.Throws<GameCheckException>(() => check.Check(new FinishedGame()));
        }
    }
}
