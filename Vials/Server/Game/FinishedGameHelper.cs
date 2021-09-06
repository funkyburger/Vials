using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vials.Shared.Exceptions;
using Vials.Shared.Objects;

namespace Vials.Server.Game
{
    public class FinishedGameHelper : IFinishedGameHelper
    {
        public IEnumerable<Tuple<long, long, long>> ExtractMoves(FinishedGame game)
        {
            int cursor = 0; 
            long from = 0, to = 0, ts = 0;
            
            foreach (var track in game.Track)
            {
                switch(cursor)
                {
                    case 0:
                        from = track;
                        cursor++;
                        break;
                    case 1:
                        to = track;
                        cursor++;
                        break;
                    case 2:
                        ts = track;
                        yield return new Tuple<long, long, long>(from, to, ts);
                        cursor = 0;
                        break;
                    default:
                        throw new Exception("This shouldn't happen. But you never know ...");
                }
            }

            if(cursor != 0)
            {
                throw new GameCheckException("Game track length isn't a multiple of 3.");
            }
        }

        public IEnumerable<long> ExtractTimestamps(FinishedGame game)
        {
            foreach(var move in ExtractMoves(game))
            {
                yield return move.Item3;
            }
        }
    }
}
