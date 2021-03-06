using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vials.Client.Events;
using Vials.Shared;
using Vials.Shared.Components;
using Vials.Shared.Objects;

namespace Vials.Client.Events
{
    public class VialClickedHandler : IEventHandler
    {
        private readonly IVialSetView View;
        public VialSet Set { get; private set; }
        private readonly IVialSetHandler _vialSetHandler;
        private readonly IEventHandler _eventHandler;

        public VialClickedHandler(IVialSetView view, VialSet set, IVialSetHandler vialSetHandler, IEventHandler eventHandler)
        {
            View = view;
            Set = set;
            _vialSetHandler = vialSetHandler;
            _eventHandler = eventHandler;
        }

        public async Task Handle(object sender, EventType eventType)
        {
            if(eventType != EventType.VialWasClicked)
            {
                return;
            }

            if (Set.IsComplete)
            {
                return;
            }

            var index = ((IVialView)sender).VialIndex;

            Set = _vialSetHandler.Select(Set, index);
            View.Set = Set;

            if (View.Set.HasChanged)
            {
                await _eventHandler.Handle(this, EventType.MoveWasMade);
            }
        }
    }
}
