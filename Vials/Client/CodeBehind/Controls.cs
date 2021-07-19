using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vials.Client.Events;
using Vials.Client.Utilities;
using Vials.Shared.Components;

namespace Vials.Client.CodeBehind
{
    public class Controls : VialComponentBase, IControls
    {
        [Inject] 
        protected ILinkHelper LinkHelper { get; set; }

        [Inject]
        protected IHtmlHelper HtmlHelper { get; set; }

        private bool canUndo = false;
        public bool CanUndo {
            get 
            {
                return canUndo; 
            }
            set 
            {
                canUndo = value;
                StateHasChanged();
            }
        }

        private bool canRedo = false;
        public bool CanRedo {
            get
            {
                return canRedo;
            }
            set
            {
                canRedo = value;
                StateHasChanged();
            }
        }

        public bool CanFindPath { get; set; }

        public int GameNumber { get; set; }

        protected void New(MouseEventArgs e)
        {
            LaunchEvent(EventType.New);
        }

        protected void Undo(MouseEventArgs e)
        {
            LaunchEvent(EventType.Undo);
        }

        protected void Redo(MouseEventArgs e)
        {
            LaunchEvent(EventType.Redo);
        }

        protected void FindPath(MouseEventArgs e)
        {
            LaunchEvent(EventType.PathFindingRequested);
        }

        protected async Task KeyPressed(KeyboardEventArgs e)
        {
            if (e.Key.Equals("Enter"))
            {
                // TODO validation
                LinkHelper.NavigateToSpecificGame(int.Parse(await HtmlHelper.GetElementValue("tbGameNumber")));
            }
        }
    }
}
