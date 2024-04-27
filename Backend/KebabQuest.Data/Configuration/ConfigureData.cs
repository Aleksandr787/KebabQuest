using KebabQuest.Data.Dto;
using KebabQuest.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using KebabQuest.Data.DataContext;
using KebabQuest.Data.JsonPrompts;

namespace KebabQuest.Data.Configuration
{
    public static class ConfigureData
    {
        public static void ConfigurePrompts(this IServiceCollection services)
        {
            var stringPrompts = JsonConvert.DeserializeObject<StringPromptsDto>(StringPrompts.GetPrompts)!;
            services.AddSingleton(stringPrompts!);
            
        }

        public static void ConfigureRepositories(this IServiceCollection services)
        {
            services.AddScoped<MongoDataContext>();
            services.AddScoped<UserRepository>();
            services.AddScoped<GameRoomRepository>();
        }
    }
}
