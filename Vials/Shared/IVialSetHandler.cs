using System;
using System.Collections.Generic;
using System.Text;

namespace Vials.Shared
{
    public interface IVialSetHandler
    {
        VialSet Select(VialSet set, int index);
        //VialSet Pour(VialSet set, int fromIndex, int toIndex);
    }
}
