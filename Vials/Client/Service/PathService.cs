using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Vials.Shared.Objects;

namespace Vials.Client.Service
{
    public class PathService : IPathService
    {
        private readonly HttpClient _http;

        public PathService(HttpClient http)
        {
            _http = http;
        }

        public async Task<IEnumerable<Pouring>> FetchPath(VialSet set)
        {
            _http.DefaultRequestHeaders
              .Accept
              .Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var response = await _http.PostAsJsonAsync("api/path/find", set);
            if (!response.IsSuccessStatusCode)
            {
                // TODO handle exceptions
                return null;
            }

            var json = await response.Content.ReadAsStringAsync();
            var path = JsonConvert.DeserializeObject<IEnumerable<Pouring>>(json);

            return path;
        }
    }
}
