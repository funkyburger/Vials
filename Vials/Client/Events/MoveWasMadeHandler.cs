using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Vials.Shared.Components;

namespace Vials.Client.Events
{
    public class MoveWasMadeHandler : IEventHandler
    {
        private readonly IIndex _def;

        public MoveWasMadeHandler(IIndex def)
        {
            _def = def;
        }

        public Task Handle(object sender, EventType eventType)
        {
            if (eventType != EventType.MoveWasMade)
            {
                return Task.CompletedTask;
            }

            var vialClickedHandler = sender as VialClickedHandler;

            _def.MoveWasMade(vialClickedHandler.Set.LastAppliedPouring);
            return Task.CompletedTask;
        }
    }
}
