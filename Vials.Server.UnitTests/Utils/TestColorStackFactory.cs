using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vials.Server.Utilities;
using Vials.Shared.Objects;

namespace Vials.Server.UnitTests.Utils
{
    public class TestColorStackFactory : IColorStackFactory
    {
        public IEnumerable<Color> GenerateStack(int numberOfColors, int seed)
        {
            return Shuffle(GeneratePalette(numberOfColors), seed);
        }

        private IEnumerable<Color> Shuffle(IEnumerable<Color> colors, int seed)
        {
            var random = new Random(seed);
            var list = colors.ToList();
            var index = random.Next(0, list.Count);

            while (list.Any())
            {
                yield return list.ElementAt((int)index);
                list.RemoveAt((int)index);
                index = random.Next(0, list.Count);
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
