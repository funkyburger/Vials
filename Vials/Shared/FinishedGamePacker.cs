using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vials.Shared.Objects;

namespace Vials.Shared
{
    public class FinishedGamePacker : IFinishedGamePacker
    {
        private readonly IObfuscator _obfuscator;

        public FinishedGamePacker(IObfuscator obfuscator)
        {
            _obfuscator = obfuscator;
        }

        public IEnumerable<long> Pack(FinishedGame game)
        {
            yield return _obfuscator.Obfuscate(game.FootPrint, 0);

            foreach(var track in game.Track)
            {
                yield return _obfuscator.Obfuscate(track, game.FootPrint);
            }

            yield return _obfuscator.Obfuscate(game.Seed, game.FootPrint);
        }

        public FinishedGame Unpack(IEnumerable<long> pack)
        {
            if (pack == null || !pack.Any())
            {
                return null;
            }

            var key = (int)_obfuscator.Unobfuscate(pack.First(), 0);

            return new FinishedGame()
            {
                FootPrint = key,
                Seed = (int)_obfuscator.Unobfuscate(pack.Last(), key),
                Track = pack.Skip(1).SkipLast(1).Select(t => _obfuscator.Unobfuscate(t, key))
            };
        }
    }
}
