using System;
using System.Collections.Generic;
using System.Text;
using Vials.Shared.Events;

namespace Vials.Shared.Components
{
    public interface IControls
    {
        void AddEventHandler(IEventHandler eventHandler);
        bool CanUndo { get; set; }
        bool CanRedo { get; set; }
        bool CanFindPath { get; set; }
    }
}
