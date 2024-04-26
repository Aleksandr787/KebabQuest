using KebabQuest.Data.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace KebabQuest.Data.Configuration;

public static class ConfigureSettings
{
    public static void ConfigureAllSettings(this IServiceCollection services, IConfiguration configuration, bool isDevelopment)
    {
        services.Configure<DatabaseSettings>(isDevelopment
            ? configuration.GetSection("MongoDb:Dev")
            : configuration.GetSection("MongoDb:Prod"));
        services.Configure<ChatGptSettings>(
            configuration.GetSection("ChatGptApiConfiguration"));
        services.Configure<KandinskySettings>(
            configuration.GetSection("KandinskyApiConfiguration"));
        services.Configure<ChatGptProxySettings>(
            configuration.GetSection("ChatGptProxyApiConfiguration"));
    }
}