using System;
using System.Collections.Generic;
using System.Text;

namespace Vials.Shared.Components
{
    public interface IDefault
    {
        void Undo();
        void Redo();
        void MoveWasMade(Pouring pouring);
    }
}
