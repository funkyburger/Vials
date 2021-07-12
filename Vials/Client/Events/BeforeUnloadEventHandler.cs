using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vials.Client.Utilities;

namespace Vials.Client.Events
{
    public class BeforeUnloadEventHandler : IEventHandler
    {
        private readonly ICookieStore _cookieStore;

        public BeforeUnloadEventHandler(ICookieStore cookieStore)
        {
            _cookieStore = cookieStore;
        }

        public void Handle(object sender, EventType eventType)
        {
            //_cookieStore.Store(new ApplicationCookie());
        }
    }
}
