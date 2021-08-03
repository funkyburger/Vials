using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Vials.Shared.Objects;

namespace Vials.Client.Service
{
    public class GameService : IGameService
    {
        private readonly HttpClient _http;

        public GameService(HttpClient http)
        {
            _http = http;
        }

        public async Task<VialSet> GetNewGame(int seed)
        {
            _http.DefaultRequestHeaders
              .Accept
              .Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            return await _http.GetFromJsonAsync<VialSet>($"api/vial/new?seed={seed}");
        }

        public Task FinishGame(IEnumerable<long> track, int seed, int footprint)
        {
            throw new NotImplementedException();
        }
    }
}
