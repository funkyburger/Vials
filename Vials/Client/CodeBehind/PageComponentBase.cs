using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vials.Client.Events;

namespace Vials.Client.CodeBehind
{
    public class PageComponentBase : ComponentBase
    {
        protected readonly IList<IEventHandler> BeforeUnloadHandlers = new List<IEventHandler>();

        [Inject]
        protected IJSRuntime JsRuntime { get; set; }

        public async Task AddBeforeUnloadEvent(IEventHandler eventHandler)
        {
            if (!BeforeUnloadHandlers.Any())
            {
                await JsRuntime.InvokeVoidAsync("BeforeUnloadInterop.addEventListener", DotNetObjectReference.Create(this));
            }

            BeforeUnloadHandlers.Add(eventHandler);
        }

        [JSInvokable]
        public DotNetObjectReference<string> OnBeforeUnload(object e)
        {
            foreach (var handler in BeforeUnloadHandlers)
            {
                handler.Handle(this, EventType.BeforeUnload);
            }
            return null;
        }
    }
}
