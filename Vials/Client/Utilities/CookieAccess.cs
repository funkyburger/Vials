using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vials.Client.Utilities
{
    public class CookieAccess : ICookieAccess
    {
        private readonly IJSRuntime _jsRuntime;

        public CookieAccess(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public async Task<string> Get()
        {
            return await _jsRuntime.InvokeAsync<string>("eval", new object[] { $"document.cookie" });
        }

        public async Task Set(string value)
        {
            await _jsRuntime.InvokeAsync<string>("eval", new object[] { $"document.cookie = '{ value }'" });
        }
    }
}
