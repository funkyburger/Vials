using System;
using System.Collections.Generic;
using System.Text;
using Vials.Shared;

namespace Vials.Core
{
    public interface ISetGenerator
    {
        VialSet Generate(int numberOfColors, int numberOfEmptyVials);
    }
}
