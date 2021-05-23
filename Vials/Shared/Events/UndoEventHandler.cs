using System;
using System.Collections.Generic;
using System.Text;
using Vials.Shared.Components;

namespace Vials.Shared.Events
{
    public class UndoEventHandler : IEventHandler
    {
        private readonly IDefault Def;

        public UndoEventHandler(IDefault def)
        {
            Def = def;
        }

        public void Handle(object sender, EventType eventType)
        {
            if(eventType != EventType.Undo)
            {
                return;
            }

            Def.Undo();
        }
    }
}
