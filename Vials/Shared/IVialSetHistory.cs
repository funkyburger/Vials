using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Vials.Shared.Objects;

namespace Vials.Shared
{
    public interface IVialSetHistory
    {
        void RegisterMove(Pouring pourings);
        void SetOngoingPath(IEnumerable<Pouring> path);
        VialSet Undo(VialSet set);
        VialSet Redo(VialSet set);
        bool CanUndo { get; }
        bool CanRedo { get; }
        void Reset();
        HistoryExport Export();
        void Import(HistoryExport history);
    }
}
