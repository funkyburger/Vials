using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vials.Client.CodeBehind
{
    public class Tooltip : VialComponentBase
    {
        [Parameter] 
        public RenderFragment ChildContent { get; set; }
        [Parameter] 
        public string Text { get; set; }
    }
}
