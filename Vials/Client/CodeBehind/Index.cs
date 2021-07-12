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
        protected ICookieService CookieService { get; set; }

        protected VialSetView vialSetView;

        protected IControls controls;

        public IEventHandler MoveWasMadeHandler => new MoveWasMadeHandler(this);

        public VialSet VialSet => vialSetView.Set;

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

        public Task RestoreGame(VialSet set)
        {
            Console.WriteLine("restoring game");
            vialSetView.Set = set;
            // TODO restore that too
            VialSetHistory.Reset();
            controls.CanFindPath = true;

            RefreshControls();
            return Task.CompletedTask;
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

            await AddBeforeUnloadEvent(new BeforeUnloadEventHandler(CookieService, this));

            if (await CookieService.DidUserConsent())
            {
                var cookie = await CookieService.GetCookie();

                if(cookie != null && cookie.VialSet != null)
                {
                    await RestoreGame(cookie.VialSet);
                    return;
                }
            }

            await NewGame();
        }

        private void RefreshControls()
        {
            controls.CanUndo = VialSetHistory.CanUndo;
            controls.CanRedo = VialSetHistory.CanRedo;
        }
    }
}
