using System;
using System.Collections.Generic;
using System.Text;
using Vials.Shared.Objects;

namespace Vials.Shared
{
    public interface IFinishedGamePacker
    {
        IEnumerable<long> Pack(FinishedGame game);
        FinishedGame Unpack(IEnumerable<long> pack);
    }
}
