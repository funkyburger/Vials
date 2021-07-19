using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vials.Client.Utilities
{
    public class HtmlHelper : IHtmlHelper
    {
        private readonly IJSRuntime _jsRuntime;

        public HtmlHelper(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public Task<string> GetElementValue(string id)
        {
            return _jsRuntime.InvokeAsync<string>("eval", $"document.getElementById('{id}').value").AsTask();
        }
    }
}
