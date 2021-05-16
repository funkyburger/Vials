using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace Vials.Core
{
    public static class CoreInstaller
    {
        public static void Install(IServiceCollection serviceCollection, IConfiguration configurationRoot)
        {
            serviceCollection.AddScoped<ISetGenerator, SetGenerator>();
        }
    }
}
