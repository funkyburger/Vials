using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vials.Shared.Extensions;
using Vials.Shared.Objects;

namespace Vials.Server.Utilities
{
    public class SetGenerator : ISetGenerator
    {
        public VialSet Generate(int numberOfColors, int numberOfEmptyVials)
        {
            var vialColors = new List<Color>();
            var vials = new List<Vial>();

            foreach(var color in GeneratePalette(numberOfColors).Shuffle())
            {
                vialColors.Add(color);

                if(vialColors.Count >= Vial.Length)
                {
                    var vial = new Vial();
                    vial.Stack(vialColors);
                    vials.Add(vial);
                    vialColors = new List<Color>();
                }
            }

            for (int i = 0; i< numberOfEmptyVials; i++)
            {
                vials.Add(new Vial());
            }

            return new VialSet() { Vials = vials };
        }

        private IEnumerable<Color> GeneratePalette(int numberOfColors)
        {
            var colors = Enum.GetValues(typeof(Color));
            if(numberOfColors > colors.Length)
            {
                throw new Exception("Maximum number of colors exceeded.");
            }

            for(int i = 0; i < numberOfColors; i++)
            {
                for (int j = 0; j < Vial.Length; j++)
                {
                    yield return (Color)colors.GetValue(i + 1);
                }
            }
        }
    }
}
