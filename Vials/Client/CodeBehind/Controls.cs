using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vials.Shared.Events;

namespace Vials.Client.CodeBehind
{
    public class Controls : VialComponentBase
    {
        protected void New(MouseEventArgs e)
        {

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
