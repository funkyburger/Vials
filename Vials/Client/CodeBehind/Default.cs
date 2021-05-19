using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Vials.Client.Shared;
using Vials.Shared;

namespace Vials.Client.CodeBehind
{
    public class Default : ComponentBase
    {
        [Inject]
        protected HttpClient Http { get; set; }

        protected VialSetView vialSetView;

        protected override async void OnAfterRender(bool firstRender)
        {
            Http.DefaultRequestHeaders
              .Accept
              .Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));//ACCEPT header

            vialSetView.Set = await Http.GetFromJsonAsync<VialSet>("api/vial/new");
        }
    }
}
