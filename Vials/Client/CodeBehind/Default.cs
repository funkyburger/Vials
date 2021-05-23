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
using Vials.Shared.Service;

namespace Vials.Client.CodeBehind
{
    public class Default : VialComponentBase, IDefault
    {
        [Inject]
        protected IVialSetHistory VialSetHistory { get; set; }
        [Inject]
        protected IGameService GameService { get; set; }

        protected VialSetView vialSetView;

        protected Controls controls;

        public IEventHandler MoveWasMadeHandler => new MoveWasMadeHandler(this);

        public void Undo()
        {
            vialSetView.Set = VialSetHistory.Undo(vialSetView.Set);
        }

        public void Redo()
        {
            vialSetView.Set = VialSetHistory.Redo(vialSetView.Set);
        }

        public void New()
        {
            NewGame();
        }

        public void MoveWasMade(Pouring pouring)
        {
            VialSetHistory.RegisterMove(pouring);
        }

        public async Task NewGame()
        {
            vialSetView.Set = await GameService.GetNewGame();
        }

        protected override async void OnAfterRender(bool firstRender)
        {
            controls.AddEventHandler(new ControlEventHandler(this));

            await NewGame();
        }
    }
}
