using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Vials.Shared;

namespace Vials.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            //Http.BaseAddress = new Uri("http://example.com/");
            //Http.DefaultRequestHeaders
            //  .Accept
            //  .Add(new MediaTypeWithQualityHeaderValue("application/json"));//ACCEPT header

            //HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "relativeAddress");
            //request.Content = new StringContent("{\"name\":\"John Doe\",\"age\":33}",
            //                                    Encoding.UTF8,
            //                                    "application/json");//CONTENT-TYPE header

            //Http.SendAsync(request)
            //      .ContinueWith(responseTask =>
            //      {
            //          Console.WriteLine("Response: {0}", responseTask.Result);
            //      });


            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddScoped<IColorToCssMapper, ColorToCssMapper>();
            //builder.Services.AddLogging(builder => builder
            //    .AddBrowserConsole()
            //    .SetMinimumLevel(LogLevel.Trace));

            await builder.Build().RunAsync();
        }
    }
}
