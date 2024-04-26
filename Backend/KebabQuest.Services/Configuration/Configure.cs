using KebabQuest.Services.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KebabQuest.Services.Configuration
{
    public static class Configure
    {
        public static void ConfigureService(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
        }
    }
}
