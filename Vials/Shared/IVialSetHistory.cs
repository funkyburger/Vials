using System;
using System.Collections.Generic;
using System.Text;

namespace Vials.Shared
{
    public interface IVialSetHistory : IEnumerable<VialSet>
    {
        VialSet Current { get; }
        void Store(VialSet set);
        VialSet GetPrevious();
        VialSet GetNext();
        void Clear();
    }
}
