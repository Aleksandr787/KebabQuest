using KebabQuest.Data.Dto;
using KebabQuest.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using KebabQuest;
using System.Xml.Linq;

namespace KebabQuest.Data.Configuration
{
    public static class ConfigureData
    {
        public static void ConfigurePrompts(this IServiceCollection services)
        {
            string solutiondir = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            var json = File.ReadAllText(solutiondir + "\\Backend\\" + "KebabQuest.Data" + "\\Assets\\prompts.json");
            var promptsDto = JsonConvert.DeserializeObject<PromptsDto>(json)!;

            services.AddSingleton(promptsDto!);
        }

        public static void ConfigureRepositories(this IServiceCollection services)
        {
            services.AddScoped<UserRepository>();
        }
    }
}
