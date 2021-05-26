using System;
using System.Collections.Generic;
using System.Text;

namespace Vials.Shared
{
    public interface ICloner
    {
        Vial Clone(Vial vial);
        VialSet Clone(VialSet vialSet);
    }
}
