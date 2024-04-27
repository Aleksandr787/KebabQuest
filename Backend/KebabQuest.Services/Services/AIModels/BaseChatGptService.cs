using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using KebabQuest.Services.Dto;
using KebabQuest.Services.Interfaces;
using Newtonsoft.Json.Linq;

namespace KebabQuest.Services.Services.AIModels;

public abstract class BaseChatGptService
{
    private readonly ChatGptContext _chatGptContext;
    private readonly HttpClient _httpCLient = new();

    public BaseChatGptService(ChatGptContext chatGptContext)
    {
        _chatGptContext = chatGptContext;
    }

    public async Task<string> SendRequest(string? prompt = null, JToken? messageHistory = null)
    {
        // prompt parameter is for testing, should be removed
        var request = new HttpRequestMessage(HttpMethod.Post, CreateUrl("chat/completions"));
        SetHeaders(request);

        request.Content = new StringContent(GetContentData(prompt, messageHistory), Encoding.UTF8, "application/json");
        using var response = await _httpCLient.SendAsync(request);
        response.EnsureSuccessStatusCode();

        var responseContent = JObject.Parse(await response.Content.ReadAsStringAsync());
        var contentString = responseContent.SelectToken("choices")?[0]?.SelectToken("message.content");

        return contentString?.Value<string>() ?? throw new Exception("something went wrong");
    }

    private void SetHeaders(HttpRequestMessage request)
    {
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _chatGptContext.ApiKey);
        request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }

    private string CreateUrl(string url)
    {
        return $"{_chatGptContext.Url}/{url}";
    }

    protected abstract string GetContentData(string? prompt = null, JToken? messagesHistory = null);
}