using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vials.Client.Service;
using Vials.Client.Utilities;

namespace Vials.Client.CodeBehind
{
    public class CookieDialog : VialComponentBase
    {
        [Inject]
        protected ICookieService CookieService { get; set; }

        protected bool ShouldBeShown { get; set; }

        protected void ConsentCookies(MouseEventArgs e)
        {
            CookieService.MarkUserDidConsent();
            ShouldBeShown = false;
            StateHasChanged();
        }

        protected override async Task OnInitializedAsync()
        {
            ShouldBeShown = !(await CookieService.DidUserConsent());
            await base.OnInitializedAsync();
        }
    }
}
