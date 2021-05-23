using System;
using System.Collections.Generic;
using System.Text;

namespace Vials.Shared
{
    public interface IVialSetHistory
    {
        void RegisterMove(Pouring pourings);
        VialSet Undo(VialSet set);
        VialSet Redo(VialSet set);
    }
}
