using System;
using System.Collections.Generic;
using System.Text;

namespace Vials.Client.Events
{
    public interface IEventHandler
    {
        void Handle(object sender, EventType eventType);
    }
}
