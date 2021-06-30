using System;
using System.Collections.Generic;
using System.Text;
using Vials.Shared.Components;

namespace Vials.Client.Events
{
    public class ControlEventHandler : IEventHandler
    {
        private readonly IIndex Def;

        public ControlEventHandler(IIndex def)
        {
            Def = def;
        }

        public void Handle(object sender, EventType eventType)
        {
            if (eventType == EventType.Undo)
            {
                Def.Undo();
            }
            else if(eventType == EventType.Redo)
            {
                Def.Redo();
            }
            else if (eventType == EventType.New)
            {
                Def.New();
            }
        }
    }
}
