using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vials.Shared.Objects;

namespace Vials.Server.Game.Check
{
    public interface IFinishedGamerChecker
    {
        void Check(FinishedGame game);
    }
}
