using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using KebabQuest.Data.Settings;
using KebabQuest.Services.Interfaces;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;

namespace KebabQuest.Services.Services.AIModels;

public class ChatGptThebService : BaseChatGptService
{
    private readonly ChatGptSettings _settings;

    public ChatGptThebService(IOptions<ChatGptSettings> settings) : base(new()
    {
        Url = settings.Value.Url,
        ApiKey = settings.Value.ApiKey,
        Model = settings.Value.Model
    })
    {
        _settings = settings.Value;
    }
    
    public override Task<string> SendRequest(string prompt, string? content = null)
    {
        var bodyContent = JsonSerializer.Serialize(new
        {
            model = _settings.Model,
            messages = new[]
            {
                new
                {
                    role = "user",
                    content = prompt
                }
            },
            stream = false,
            model_params = new
            {
                temperature = 1.0
            }
        });
        return base.SendRequest(prompt, bodyContent);
    }
}