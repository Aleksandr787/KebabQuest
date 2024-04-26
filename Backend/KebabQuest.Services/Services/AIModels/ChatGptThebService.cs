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
    private readonly ChatGptSettings _configuration;

    public ChatGptThebService(IOptions<ChatGptSettings> settings) : base(new()
    {
        Url = settings.Value.Url,
        ApiKey = settings.Value.ApiKey,
        Model = settings.Value.Model
    })
    {
        _configuration = settings.Value;
    }
}