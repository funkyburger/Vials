using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Linq;

namespace Vials.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddRazorPages();

            services.AddScoped<Utilities.ISetGenerator, Utilities.SetGenerator>();
            services.AddScoped<Utilities.IPathFinder, Utilities.PathFinder>();
            services.AddScoped<Utilities.IColorStackFactory, Utilities.ColorStackFactory>();
            services.AddScoped<Utilities.IRandomGenerator, Utilities.PseudoRandomGenerator>();
            services.AddScoped<Utilities.ISetMoveMaker, Utilities.SetMoveMaker>();

            services.AddScoped<Shared.ICloner, Shared.Cloner>();
            services.AddScoped<Shared.IObfuscator, Shared.Obfuscator>();
            services.AddScoped<Shared.IFinishedGamePacker, Shared.FinishedGamePacker>();

            services.AddScoped<Game.IFinishedGameHelper, Game.FinishedGameHelper>();
            services.AddScoped<Game.Check.IFinishedGamerChecker, Game.Check.FinishedGamerChecker>();
            services.AddScoped<Game.Check.IGameCheck, Game.Check.ChronologyCheck>();
            services.AddScoped<Game.Check.IGameCheck, Game.Check.CompletionCheck>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapFallbackToFile("index.html");
            });
        }
    }
}
