using System;
using System.Collections.Generic;
using System.Text;

namespace Vials.Shared
{
    public class VialSet : IVialSet
    {
        public IEnumerable<Vial> Vials { get; set; }
    }
}
