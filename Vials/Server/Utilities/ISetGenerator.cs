using System;
using System.Collections.Generic;
using System.Text;
using Vials.Shared;
using Vials.Shared.Objects;

namespace Vials.Server.Utilities
{
    public interface ISetGenerator
    {
        VialSet Generate(int numberOfColors, int numberOfEmptyVials, int seed, int setFootPrints);
    }
}
