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

        protected IControls controls;

        public IEventHandler MoveWasMadeHandler => new MoveWasMadeHandler(this);

        public void Undo()
        {
            if (vialSetView.Set.IsComplete)
            {
                controls.CanUndo = false;
                controls.CanRedo = false;
                return;
            }

            vialSetView.Set = VialSetHistory.Undo(vialSetView.Set);
            RefreshControls();
        }

        public void Redo()
        {
            if (vialSetView.Set.IsComplete)
            {
                controls.CanUndo = false;
                controls.CanRedo = false;
                return;
            }

            vialSetView.Set = VialSetHistory.Redo(vialSetView.Set);
            RefreshControls();
        }

        public void New()
        {
            NewGame();
        }

        public void MoveWasMade(Pouring pouring)
        {
            VialSetHistory.RegisterMove(pouring);
            RefreshControls();
        }

        public async Task NewGame()
        {
            vialSetView.Set = await GameService.GetNewGame();
            VialSetHistory.Reset();

            RefreshControls();
        }

        protected override async void OnAfterRender(bool firstRender)
        {
            controls.AddEventHandler(new ControlEventHandler(this));

            await NewGame();
        }

        private void RefreshControls()
        {
            Console.WriteLine("RefreshControls()");

            controls.CanUndo = VialSetHistory.CanUndo;
            controls.CanRedo = VialSetHistory.CanRedo;
        }
    }
}
