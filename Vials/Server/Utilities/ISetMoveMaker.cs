using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vials.Shared.Objects;

namespace Vials.Server.Utilities
{
    public interface ISetMoveMaker
    {
        void AppliMove(VialSet set, int fromFootPrint, int toFootPrint);
        VialSet CloneAndAppliMove(VialSet set, Pouring pouring);
    }
}
