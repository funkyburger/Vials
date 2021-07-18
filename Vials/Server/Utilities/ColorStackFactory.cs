using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vials.Shared.Objects;

namespace Vials.Server.Utilities
{
    public class ColorStackFactory : IColorStackFactory
    {
        private readonly IRandomGenerator _randomGenerator;

        public ColorStackFactory(IRandomGenerator randomGenerator)
        {
            _randomGenerator = randomGenerator;
        }

        public IEnumerable<Color> GenerateStack(int numberOfColors, int seed)
        {
            return Shuffle(GeneratePalette(numberOfColors), seed);
        }

        private IEnumerable<Color> Shuffle(IEnumerable<Color> colors, int seed)
        {
            var list = colors.ToList();
            var index = _randomGenerator.Generate(seed);

            while (list.Any())
            {
                index = _randomGenerator.GenerateNext(list.Count - 1); ;
                yield return list.ElementAt(index);
                list.RemoveAt(index);
            }
        }

        private IEnumerable<Color> GeneratePalette(int numberOfColors)
        {
            var colors = Enum.GetValues(typeof(Color));
            if (numberOfColors > colors.Length)
            {
                throw new Exception("Maximum number of colors exceeded.");
            }

            for (int i = 0; i < numberOfColors; i++)
            {
                for (int j = 0; j < Vial.Length; j++)
                {
                    yield return (Color)colors.GetValue(i + 1);
                }
            }
        }
    }
}
