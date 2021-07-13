using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Vials.Shared.Objects;

namespace Vials.Shared.Components
{
    public interface IIndex
    {
        void Undo();
        void Redo();
        void New();
        void MoveWasMade(Pouring pouring);
        Task FindPath();
        VialSet VialSet { get; }
        HistoryExport History { get; }
    }
}
