using System;
using System.Collections.Generic;
using System.Text;
using Vials.Shared.Objects;

namespace Vials.Shared
{
    public interface ICloner
    {
        Vial Clone(Vial vial);
        VialSet Clone(VialSet vialSet);
    }
}
