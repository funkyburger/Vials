using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Vials.Client.Shared;
using Vials.Shared;
using Vials.Shared.Components;
using Vials.Shared.Events;

namespace Vials.Client.CodeBehind
{
    public class Default : VialComponentBase, IDefault
    {
        [Inject]
        protected HttpClient Http { get; set; }
        [Inject]
        protected IVialSetHistory VialSetHistory { get; set; }

        protected VialSetView vialSetView;

        protected Controls controls;

        public IEventHandler MoveWasMadeHandler => new MoveWasMadeHandler(this);

        public void Undo()
        {
            vialSetView.Set = VialSetHistory.Undo(vialSetView.Set);
        }

        public void MoveWasMade(Pouring pouring)
        {
            VialSetHistory.RegisterMove(pouring);
        }

        public async void NewGame()
        {
            Http.DefaultRequestHeaders
              .Accept
              .Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));//ACCEPT header

            vialSetView.Set = await Http.GetFromJsonAsync<VialSet>("api/vial/new");
        }

        protected override async void OnAfterRender(bool firstRender)
        {
            controls.AddEventHandler(new UndoEventHandler(this));

            NewGame();
        }
    }
}
