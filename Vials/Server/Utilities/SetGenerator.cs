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

        public SetGenerator(IColorStackFactory stackFactory)
        {
            _stackFactory = stackFactory;
        }

        public VialSet Generate(int numberOfColors, int numberOfEmptyVials, int seed)
        {
            var vialColors = new List<Color>();
            var vials = new List<Vial>();

            foreach(var color in _stackFactory.GenerateStack(numberOfColors, seed))
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
    }
}
