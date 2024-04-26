using KebabQuest.Data.Dto;
using KebabQuest.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using KebabQuest;

namespace KebabQuest.Data.Configuration
{
    public static class ConfigureData
    {
        public static void ConfigurePrompts(this IServiceCollection services)
        {
            var json = File.ReadAllText("Assets/Prompts/ruPrompts.json");
            var promptsDto = JsonConvert.DeserializeObject<PromptsDto>(json)!;

            services.AddSingleton(promptsDto!);
        }

        public static void ConfigureRepositories(this IServiceCollection services)
        {
            services.AddScoped<UserRepository>();
        }
    }
}
