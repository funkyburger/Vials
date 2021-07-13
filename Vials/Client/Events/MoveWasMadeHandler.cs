using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Vials.Shared.Components;

namespace Vials.Client.Events
{
    public class MoveWasMadeHandler : IEventHandler
    {
        private readonly IIndex _index;

        public MoveWasMadeHandler(IIndex index)
        {
            _index = index;
        }

        public Task Handle(object sender, EventType eventType)
        {
            if (eventType != EventType.MoveWasMade)
            {
                return Task.CompletedTask;
            }

            var vialClickedHandler = sender as VialClickedHandler;

            return _index.MoveWasMade(vialClickedHandler.Set.LastAppliedPouring);
        }
    }
}
