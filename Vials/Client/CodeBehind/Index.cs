using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Vials.Client.Events;
using Vials.Client.Service;
using Vials.Client.Utilities;
using Vials.Shared;
using Vials.Shared.Components;
using Vials.Shared.Objects;

namespace Vials.Client.CodeBehind
{
    public class Index : PageComponentBase, IIndex
    {
        [Inject]
        protected IVialSetHistory VialSetHistory { get; set; }
        [Inject]
        protected IGameService GameService { get; set; }
        [Inject]
        protected IPathService PathService { get; set; }
        [Inject]
        protected ICookieStore CookieStore { get; set; }

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
            controls.CanFindPath = true;
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
            controls.CanFindPath = true;
            RefreshControls();
        }

        public void New()
        {
            NewGame();
        }

        public void MoveWasMade(Pouring pouring)
        {
            VialSetHistory.RegisterMove(pouring);
            controls.CanFindPath = true;
            RefreshControls();
        }

        public async Task NewGame()
        {
            vialSetView.Set = await GameService.GetNewGame();
            VialSetHistory.Reset();
            controls.CanFindPath = true;

            RefreshControls();
        }

        public async Task FindPath()
        {
            controls.CanFindPath = false;
            var path = await PathService.FetchPath(vialSetView.Set);

            VialSetHistory.SetOngoingPath(path);
            controls.CanFindPath = true;

            RefreshControls();
        }

        protected override async void OnAfterRender(bool firstRender)
        {
            controls.AddEventHandler(new ControlEventHandler(this));
            controls.AddEventHandler(new PathFindingRequestedHandler(this));

            await AddBeforeUnloadEvent(new BeforeUnloadEventHandler(CookieStore));

            await NewGame();
        }

        private void RefreshControls()
        {
            controls.CanUndo = VialSetHistory.CanUndo;
            controls.CanRedo = VialSetHistory.CanRedo;
        }
    }
}
