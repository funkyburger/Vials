using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Vials.Shared.Components;

namespace Vials.Client.Events
{
    public class ControlEventHandler : IEventHandler
    {
        private readonly IIndex _index;

        public ControlEventHandler(IIndex index)
        {
            _index = index;
        }

        public Task Handle(object sender, EventType eventType)
        {
            if (eventType == EventType.Undo)
            {
                return _index.Undo();
            }
            else if(eventType == EventType.Redo)
            {
                return _index.Redo();
            }
            else if (eventType == EventType.New)
            {
                return _index.New();
            }

            return Task.CompletedTask;
        }
    }
}
