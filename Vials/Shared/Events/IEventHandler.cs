using System;
using System.Collections.Generic;
using System.Text;

namespace Vials.Shared.Events
{
    public interface IEventHandler
    {
        void Handle(object sender, EventType eventType);
    }
}
