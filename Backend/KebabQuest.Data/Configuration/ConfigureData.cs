using KebabQuest.Data.Dto;
using KebabQuest.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using KebabQuest;
using System.Xml.Linq;
using KebabQuest.Data.DataContext;
using KebabQuest.Data.Assets;

namespace KebabQuest.Data.Configuration
{
    public static class ConfigureData
    {
        public static void ConfigurePrompts(this IServiceCollection services)
        {
            var promptsDto = JsonConvert.DeserializeObject<PromptsDto>(Prompts.json)!;

            services.AddSingleton(promptsDto!);
        }

        public static void ConfigureRepositories(this IServiceCollection services)
        {
            services.AddScoped<MongoDataContext>();
            services.AddScoped<UserRepository>();
        }
    }
}
