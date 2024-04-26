using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using KebabQuest.Services.Dto;
using KebabQuest.Services.Interfaces;
using Newtonsoft.Json.Linq;

namespace KebabQuest.Services.Services.AIModels;

public class BaseChatGptService : IBaseChatGptService
{
    private readonly ChatGptContext _chatGptContext;
    private readonly HttpClient _httpCLient = new();

    public BaseChatGptService(ChatGptContext chatGptContext)
    {
        _chatGptContext = chatGptContext;
    }
    
    public async Task<string> SendRequest(string prompt)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, CreateUrl("chat/completions"));
        SetHeaders(request);

        var content = JsonSerializer.Serialize(new
        {
            model = _chatGptContext.Model,
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
        request.Content = new StringContent(content, Encoding.UTF8, "application/json");
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
}