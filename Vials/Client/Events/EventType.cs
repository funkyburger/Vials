using System;
using System.Collections.Generic;
using System.Text;

namespace Vials.Client.Events
{
    public enum EventType
    {
        Undo,
        MoveWasMade,
        VialWasClicked,
        Redo,
        New,
        PathFindingRequested
    }
}
