using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vials.Shared.Exceptions;
using Vials.Shared.Extensions;
using Vials.Shared.Objects;

namespace Vials.Server.Game.Check
{
    public class ChronologyCheck : IGameCheck
    {
        private readonly IFinishedGameHelper _finishedGameHelper;

        public ChronologyCheck(IFinishedGameHelper finishedGameHelper)
        {
            _finishedGameHelper = finishedGameHelper;
        }

        public void Check(FinishedGame game)
        {
            long ts = 0;

            foreach(var timestamp in _finishedGameHelper.ExtractTimestamps(game))
            {
                if(timestamp <= ts)
                {
                    throw new GameCheckException("Timestamps are not in order.");
                }

                ts = timestamp;
            }
        }
    }
}
