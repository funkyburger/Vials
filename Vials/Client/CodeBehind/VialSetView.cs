using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vials.Client.Shared;
using Vials.Shared;
using Vials.Shared.Components;
using Vials.Shared.Events;

namespace Vials.Client.CodeBehind
{
    public class VialSetView : VialComponentBase, IVialSetView
    {
        public IEnumerable<VialView> vialViews;

        [Inject]
        protected IVialSetHandler VialSetHandler { get; set; }

        [Parameter]
        public IEventHandler MoveWasMadeHandler { get; set; }

        protected VialSet set;
        public VialSet Set
        {
            get
            {
                return set;
            }
            set
            {
                set = value;
                StateHasChanged();
            }
        }

        public void AddEventHandlerToVials(IEventHandler eventHandler)
        {
            foreach (var view in vialViews)
            {
                view.AddEventHandler(eventHandler);
            }
        }
    }
}
