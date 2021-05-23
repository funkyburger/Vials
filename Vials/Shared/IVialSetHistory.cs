using System;
using System.Collections.Generic;
using System.Text;

namespace Vials.Shared
{
    public interface IVialSetHistory : IEnumerable<VialSet>
    {
        [Obsolete]
        VialSet Current { get; }
        [Obsolete]
        void Store(VialSet set);
        //void SetOriginal(VialSet set);
        void RegisterMove(IEnumerable<Pouring> pourings);
        [Obsolete]
        VialSet GetPrevious();
        [Obsolete]
        VialSet GetNext();
        VialSet Undo(VialSet set);
        VialSet Redo(VialSet set);
        void Clear();
    }
}
