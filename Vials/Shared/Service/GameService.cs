using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Vials.Shared.Service
{
    public class GameService : IGameService
    {
        private readonly HttpClient _http;

        public GameService(HttpClient http)
        {
            _http = http;
        }

        public async Task<VialSet> GetNewGame()
        {
            _http.DefaultRequestHeaders
              .Accept
              .Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            return await _http.GetFromJsonAsync<VialSet>("api/vial/new");
        }
    }
}
