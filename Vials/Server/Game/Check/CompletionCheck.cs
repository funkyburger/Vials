using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vials.Server.Utilities;
using Vials.Shared.Exceptions;
using Vials.Shared.Objects;

namespace Vials.Server.Game.Check
{
    public class CompletionCheck : IGameCheck
    {
        private readonly ISetGenerator _setGenerator;
        private readonly IFinishedGameHelper _finishedGameHelper;
        private readonly ISetMoveMaker _moveMaker;


        public CompletionCheck(ISetGenerator setGenerator, IFinishedGameHelper finishedGameHelper, ISetMoveMaker moveMaker)
        {
            _setGenerator = setGenerator;
            _finishedGameHelper = finishedGameHelper;
            _moveMaker = moveMaker;
        }

        public void Check(FinishedGame game)
        {
            // TODO get size
            var set = _setGenerator.Generate(4, 2, game.Seed, game.FootPrint);

            foreach(var move in _finishedGameHelper.ExtractMoves(game))
            {
                _moveMaker.AppliMove(set, (int)move.Item1, (int)move.Item2);
            }

            if (!set.IsComplete)
            {
                throw new GameCheckException("Tracks did not complete the game.");
            }
        }
    }
}
