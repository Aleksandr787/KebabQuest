using KebabQuest.Services.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KebabQuest.Data.Settings;
using KebabQuest.Services.Interfaces;
using KebabQuest.Services.Services.AIModels;

namespace KebabQuest.Services.Configuration
{
    public static class Configure
    {
        public static void ConfigureService(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IGameLogicService, GameLogicService>();
            services.AddScoped<IGameRoomService, GameRoomService>();
            services.AddScoped<IGameService, GameService>();
            services.AddScoped<IGameSampleService, GameSampleService>();
            services.AddScoped<IScreenCastService, ScreenCastService>();
            
            services.AddSingleton<ChatGptProxyService>();
            services.AddSingleton<ChatGptThebService>();
            services.AddSingleton<IKandinskyService, KandinskyService>();
        }
    }
}
