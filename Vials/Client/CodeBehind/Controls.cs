using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vials.Client.Events;
using Vials.Shared.Components;

namespace Vials.Client.CodeBehind
{
    public class Controls : VialComponentBase, IControls
    {
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
    }
}
