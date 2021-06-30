﻿using System;
using System.Collections.Generic;
using System.Text;
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

        public void Handle(object sender, EventType eventType)
        {
            if (eventType != EventType.MoveWasMade)
            {
                return;
            }

            var vialClickedHandler = sender as VialClickedHandler;

            _def.MoveWasMade(vialClickedHandler.Set.LastAppliedPouring);
        }
    }
}
