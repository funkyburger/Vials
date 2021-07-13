using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Vials.Shared.Components;

namespace Vials.Client.Events
{
    public class PathFindingRequestedHandler : IEventHandler
    {
        private IIndex _default;

        public PathFindingRequestedHandler(IIndex def)
        {
            _default = def;
        }

        public async Task Handle(object sender, EventType eventType)
        {
            if (eventType != EventType.PathFindingRequested)
            {
                return;
            }

            await _default.FindPath();
        }
    }
}
