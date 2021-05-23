using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vials.Shared.Events;

namespace Vials.Client.CodeBehind
{
    public class VialComponentBase : ComponentBase
    {
        protected readonly IList<IEventHandler> EventHandlers = new List<IEventHandler>();

        public void AddEventHandler(IEventHandler eventHandler)
        {
            EventHandlers.Add(eventHandler);
        }

        protected void LaunchEvent(EventType eventType)
        {
            foreach (var handler in EventHandlers)
            {
                handler.Handle(this, eventType);
            }
        }
    }
}
