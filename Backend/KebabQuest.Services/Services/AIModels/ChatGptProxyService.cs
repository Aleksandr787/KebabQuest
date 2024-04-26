using System.Net.Http.Headers;
using System.Text;
using KebabQuest.Data.Settings;
using KebabQuest.Services.Interfaces;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using System.Text.Json;

namespace KebabQuest.Services.Services.AIModels;

public class ChatGptProxyService : BaseChatGptService
{
    private readonly ChatGptProxySettings _settings;
    public ChatGptProxyService(IOptions<ChatGptProxySettings> settings) : base(new()
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
            }
        });
        return base.SendRequest(prompt, bodyContent);
    }
}