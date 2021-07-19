using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vials.Server.Utilities
{
    public interface IRandomGenerator
    {
        int Generate(int seed);
        int Generate(int seed, int maxValue);
        int GenerateNext();
        int GenerateNext(int maxValue);
    }
}
