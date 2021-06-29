using System;
using System.Collections.Generic;
using System.Text;
using Vials.Shared.Objects;

namespace Vials.Shared
{
    public interface IVialSetHandler
    {
        VialSet Select(VialSet set, int index);
    }
}
