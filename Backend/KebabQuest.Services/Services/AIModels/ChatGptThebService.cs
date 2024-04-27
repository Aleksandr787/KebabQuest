using System.Net.Http.Headers;
using System.Security.Principal;
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

    protected override string GetContentData(string? prompt = null, JToken? messagesHistory = null)
    {
        var jObject = new JObject
        {
            { "model", _settings.Model },
            { "messages", messagesHistory },
            { "stream", false },
            {
                "model_params", new JObject
                {
                    { "temperature", 1.0 }
                }
            }
        };
        var bodyContent = jObject.ToString();
        return bodyContent;
    }
}