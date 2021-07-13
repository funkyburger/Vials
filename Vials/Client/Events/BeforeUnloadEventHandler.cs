using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vials.Client.Service;
using Vials.Client.Utilities;
using Vials.Shared.Components;

namespace Vials.Client.Events
{
    public class BeforeUnloadEventHandler : IEventHandler
    {
        private readonly ICookieService _cookieService;
        private readonly IIndex _index;

        public BeforeUnloadEventHandler(ICookieService cookieService, IIndex index)
        {
            _cookieService = cookieService;
            _index = index;
        }

        public async Task Handle(object sender, EventType eventType)
        {
            if(eventType != EventType.BeforeUnload || !(await _cookieService.DidUserConsent()))
            {
                return;
            }

            var cookie = await _cookieService.GetCookie();
            
            cookie.VialSet = _index.VialSet;
            cookie.History = _index.History;

            await _cookieService.SetCookie(cookie);
        }
    }
}
