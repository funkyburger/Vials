using System;
using System.Collections.Generic;
using System.Text;
using Vials.Client.Events;

namespace Vials.Shared.Components
{
    public interface IControls
    {
        void AddEventHandler(IEventHandler eventHandler);
        void StopTimer();
        bool CanUndo { get; set; }
        bool CanRedo { get; set; }
        bool CanFindPath { get; set; }
        int GameNumber { get; set; }
    }
}
