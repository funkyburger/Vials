using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vials.Shared.Objects;

namespace Vials.Server.Game
{
    public interface IFinishedGameHelper
    {
        IEnumerable<Tuple<long, long, long>> ExtractMoves(FinishedGame game);
        IEnumerable<long> ExtractTimestamps(FinishedGame game);
    }
}
