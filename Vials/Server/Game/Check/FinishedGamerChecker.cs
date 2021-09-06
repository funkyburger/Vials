using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vials.Shared.Objects;

namespace Vials.Server.Game.Check
{
    public class FinishedGamerChecker : IFinishedGamerChecker
    {
        private readonly IEnumerable<IGameCheck> _checks;

        public FinishedGamerChecker(IEnumerable<IGameCheck> checks)
        {
            _checks = checks;
        }

        public void Check(FinishedGame game)
        {
            foreach(var check in _checks)
            {
                check.Check(game);
            }
        }
    }
}
