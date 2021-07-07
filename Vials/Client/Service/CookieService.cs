using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vials.Client.Utilities;
using Vials.Shared.Objects;

namespace Vials.Client.Service
{
    public class CookieService : ICookieService
    {
        private readonly ICookieStore _cookieStore;

        public CookieService(ICookieStore cookieStore)
        {
            _cookieStore = cookieStore;
        }

        public async Task<bool> DidUserConsent()
        {
            var cookie = await _cookieStore.Get();

            return cookie != null;
        }

        public async Task MarkUserDidConsent()
        {
            await _cookieStore.Store(new ApplicationCookie());
        }
    }
}
