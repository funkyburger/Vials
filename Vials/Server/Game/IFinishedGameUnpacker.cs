using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vials.Shared.Objects;

namespace Vials.Server.Game
{
    public interface IFinishedGameUnpacker
    {
        FinishedGame Unpack(IEnumerable<long> blah);
    }
}
