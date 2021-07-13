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

        public HistoryExport History => VialSetHistory.Export();

        public Task Undo()
        {
            if (vialSetView.Set.IsComplete)
            {
                controls.CanUndo = false;
                controls.CanRedo = false;
                return Task.CompletedTask;
            }

            vialSetView.Set = VialSetHistory.Undo(vialSetView.Set);
            controls.CanFindPath = true;
            RefreshControls();
            return Task.CompletedTask;
        }

        public Task Redo()
        {
            if (vialSetView.Set.IsComplete)
            {
                controls.CanUndo = false;
                controls.CanRedo = false;
                return Task.CompletedTask;
            }

            vialSetView.Set = VialSetHistory.Redo(vialSetView.Set);
            controls.CanFindPath = true;
            RefreshControls();
            return Task.CompletedTask;
        }

        public async Task New()
        {
            if(await CookieService.DidUserConsent())
            {
                await CookieService.SetCookie(new ApplicationCookie());
            }
            
            await NewGame();
        }

        public async Task MoveWasMade(Pouring pouring)
        {
            if (VialSet.IsComplete)
            {
                if (await CookieService.DidUserConsent())
                {
                    await CookieService.SetCookie(new ApplicationCookie());
                }

                controls.CanUndo = false;
                controls.CanRedo = false;
                controls.CanFindPath = false;

                RefreshControls();
                return;
            }

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

        public Task RestoreGame(VialSet set, HistoryExport history)
        {
            vialSetView.Set = set;
            VialSetHistory.Import(history);
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
                    await RestoreGame(cookie.VialSet, cookie.History);
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
