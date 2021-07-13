using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vials.Client.Utilities;
using Vials.Shared.Objects;

namespace Vials.Client.Service
{
    public interface ICookieService
    {
        Task<bool> DidUserConsent();
        Task MarkUserDidConsent();
        Task<ApplicationCookie> GetCookie();
        Task SetCookie(ApplicationCookie cookie);
    }
}
