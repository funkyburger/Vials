using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Vials.Shared;
using Vials.Shared.Objects;

namespace Vials.Client.Service
{
    public class GameService : IGameService
    {
        private readonly HttpClient _http;
        private readonly IFinishedGamePacker _packer;

        public GameService(HttpClient http, IFinishedGamePacker packer)
        {
            _http = http;
            _packer = packer;
        }

        public async Task<VialSet> GetNewGame(int seed)
        {
            _http.DefaultRequestHeaders
              .Accept
              .Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            return await _http.GetFromJsonAsync<VialSet>($"api/vial/new?seed={seed}");
        }

        public async Task FinishGame(IEnumerable<long> track, int seed, int footprint)
        {
            Console.WriteLine("FinishGame");

            var pack = _packer.Pack(new FinishedGame() { 
                FootPrint = footprint,
                Seed = seed,
                Track = track
            });

            Console.WriteLine("packed");

            _http.DefaultRequestHeaders
              .Accept
              .Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            Console.WriteLine("posting");
            var kaka = await _http.PostAsJsonAsync("api/finish/check", pack);
            Console.WriteLine("posted");
            var str = await kaka.Content.ReadAsStringAsync();
            Console.WriteLine(str);
        }
    }
}
