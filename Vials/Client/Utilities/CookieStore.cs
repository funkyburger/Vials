using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vials.Client.Utilities
{
    public class CookieStore : ICookieStore
    {
        private readonly ICookieAccess _cookieAccess;

        public CookieStore(ICookieAccess cookieAccess)
        {
            _cookieAccess = cookieAccess;
        }

        public async Task<ApplicationCookie> Get()
        {
            var cookieString = await _cookieAccess.Get();
            if (string.IsNullOrEmpty(cookieString))
            {
                return null;
            }

            return RetrieveCookieValueFromString(cookieString);
        }

        public async Task Store(ApplicationCookie cookie)
        {
            await _cookieAccess.Set(GenerateCookieString(cookie));
        }

        private string GenerateCookieString(ApplicationCookie cookie)
        {
            return cookie != null ?
                $"cookie={ JsonConvert.SerializeObject(cookie) };SameSite=Strict" : null;
        }

        private ApplicationCookie RetrieveCookieValueFromString(string str)
        {
            foreach (var pair in str.Split(';'))
            {
                var parts = pair.Split('=');
                if (parts.Length < 2)
                {
                    continue;
                }

                if (parts[0].Equals("cookie"))
                {
                    return JsonConvert.DeserializeObject<ApplicationCookie>(parts[1]);
                }
            }

            return null;
        }
    }
}
