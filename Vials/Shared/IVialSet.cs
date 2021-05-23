using System;
using System.Collections.Generic;
using System.Text;

namespace Vials.Shared
{
    public interface IVialSet
    {
        IEnumerable<Vial> Vials { get; set; }
        bool IsComplete { get; }
    }
}
