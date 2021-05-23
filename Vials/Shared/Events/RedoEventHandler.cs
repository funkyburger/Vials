using System;
using System.Collections.Generic;
using System.Text;
using Vials.Shared.Components;

namespace Vials.Shared.Events
{
    public class RedoEventHandler : IEventHandler
    {
        private readonly IDefault Def;

        public RedoEventHandler(IDefault def)
        {
            Def = def;
        }

        public void Handle(object sender, EventType eventType)
        {
            if (eventType != EventType.Redo)
            {
                return;
            }

            Def.Redo();
        }
    }
}
