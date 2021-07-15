using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vials.Client.Exceptions
{
    // TODO move to shared
    public class CookieConsentExcpetion : Exception
    {
        public CookieConsentExcpetion(string message) : base(message)
        {
        }
    }
}
