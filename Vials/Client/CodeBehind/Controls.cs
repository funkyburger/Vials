using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vials.Shared.Components;
using Vials.Shared.Events;

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
                Console.WriteLine($"can undo : {value}");
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
    }
}
