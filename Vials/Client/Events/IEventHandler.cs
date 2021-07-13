using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Vials.Client.Events
{
    public interface IEventHandler
    {
        Task Handle(object sender, EventType eventType);
    }
}
