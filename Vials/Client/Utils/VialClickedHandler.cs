using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vials.Client.Shared;
using Vials.Shared;
using Vials.Shared.Events;

namespace Vials.Client.Utils
{
    public class VialClickedHandler : IClickHandler
    {
        private readonly VialSetView View;
        private readonly VialSet Set;
        private readonly IVialSetHandler _vialSetHandler;
        private readonly IEventHandler _eventHandler;

        public VialClickedHandler(VialSetView view, VialSet set, IVialSetHandler vialSetHandler, IEventHandler eventHandler)
        {
            View = view;
            Set = set;
            _vialSetHandler = vialSetHandler;
            _eventHandler = eventHandler;
        }

        public void Handle(object sender)
        {
            if (Set.IsComplete)
            {
                return;
            }

            var index = ((VialView)sender).VialIndex;

            View.Set = _vialSetHandler.Select(Set, index);

            if (View.Set.HasChanged)
            {
                _eventHandler.Handle(this, EventType.MoveWasMade);
            }
        }
    }
}
