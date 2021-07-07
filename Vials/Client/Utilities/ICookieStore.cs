using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vials.Client.Utilities
{
    public interface ICookieStore
    {
        Task<ApplicationCookie> Get();
        Task Store(ApplicationCookie cookie);
    }
}
