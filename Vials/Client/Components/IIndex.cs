using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Vials.Shared.Objects;

namespace Vials.Shared.Components
{
    public interface IIndex
    {
        Task Undo();
        Task Redo();
        Task New();
        Task MoveWasMade(Pouring pouring);
        Task FindPath();
        VialSet VialSet { get; }
        HistoryExport History { get; }
    }
}
