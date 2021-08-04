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

            ConfigureServices(builder);

            await builder.Build().RunAsync();
        }

        private static void ConfigureServices(WebAssemblyHostBuilder builder)
        {
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddScoped<Utilities.IColorToCssMapper, Utilities.ColorToCssMapper>();
            builder.Services.AddScoped<Utilities.ICookieAccess, Utilities.CookieAccess>();
            builder.Services.AddScoped<Utilities.ICookieStore, Utilities.CookieStore>();
            builder.Services.AddScoped<Utilities.ILinkHelper, Utilities.LinkHelper>();
            builder.Services.AddScoped<Utilities.IHtmlHelper, Utilities.HtmlHelper>();
            builder.Services.AddScoped<Utilities.IMoveTracker, Utilities.MoveTracker>();
            builder.Services.AddScoped<IVialSetHandler, VialSetHandler>();
            builder.Services.AddScoped<IVialSetHistory, VialSetHistory>();
            builder.Services.AddScoped<IObfuscator, Obfuscator>();
            builder.Services.AddScoped<IFinishedGamePacker, FinishedGamePacker>();
            builder.Services.AddScoped<Service.IGameService, Service.GameService>();
            builder.Services.AddScoped<Service.IPathService, Service.PathService>();
            builder.Services.AddScoped<Service.ICookieService, Service.CookieService>();
        }
    }
}
