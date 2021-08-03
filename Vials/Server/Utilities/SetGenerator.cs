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
        private readonly IColorStackFactory _stackFactory;
        private readonly IRandomGenerator _randomGenerator;

        public SetGenerator(IColorStackFactory stackFactory, IRandomGenerator randomGenerator)
        {
            _stackFactory = stackFactory;
            _randomGenerator = randomGenerator;
        }

        public VialSet Generate(int numberOfColors, int numberOfEmptyVials, int seed)
        {
            var vialColors = new List<Color>();
            var vials = new List<Vial>();
            var footPrint = _randomGenerator.Generate(new Random().Next());
            var setFootPrints = new Dictionary<int, int>();
            setFootPrints.Add(0, 0); // Zeros not allowed as footprints.

            foreach (var color in _stackFactory.GenerateStack(numberOfColors, seed))
            {
                vialColors.Add(color);

                if(vialColors.Count >= Vial.Length)
                {
                    while (setFootPrints.ContainsKey(footPrint))
                    {
                        footPrint = _randomGenerator.GenerateNext();
                    }

                    var vial = new Vial() {
                        FootPrint = footPrint
                    };

                    setFootPrints.Add(footPrint, 0);

                    vial.Stack(vialColors);
                    vials.Add(vial);
                    vialColors = new List<Color>();
                }
            }

            for (int i = 0; i< numberOfEmptyVials; i++)
            {
                while (setFootPrints.ContainsKey(footPrint))
                {
                    footPrint = _randomGenerator.GenerateNext();
                }

                vials.Add(new Vial() { 
                    FootPrint = footPrint
                });

                setFootPrints.Add(footPrint, 0);
            }

            return new VialSet() { Vials = vials };
        }
    }
}
