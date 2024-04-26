using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using KebabQuest.Data.Settings;
using KebabQuest.Services.Interfaces;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;

namespace KebabQuest.Services.Services.AIModels;

public class ChatGptThebService : IChatGptThebService
{
    private readonly ChatGptSettings _configuration;
    private readonly HttpClient _httpCLient = new();

    public ChatGptThebService(IOptions<ChatGptSettings> configuration)
    {
        _configuration = configuration.Value;
    }

    public async Task<string> GetModels()
    {
        // this is just a test method to be sure that API works correctly
        var request = new HttpRequestMessage(HttpMethod.Get, CreateUrl("models"));
        SetHeaders(request);

        using var response = await _httpCLient.SendAsync(request);
        response.EnsureSuccessStatusCode();
        return (await response.Content.ReadAsStringAsync()).Substring(0, 200);
    }

    public async Task<string> SendRequest(string prompt)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, CreateUrl("chat/completions"));
        SetHeaders(request);

        var content = JsonSerializer.Serialize(new
        {
            model = "gpt-3.5-turbo",
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
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _configuration.ApiKey);
        request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }

    private string CreateUrl(string url)
    {
        return $"{_configuration.Url}/{url}";
    }
}