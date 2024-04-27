using System.Net.Http.Headers;
using System.Text;
using KebabQuest.Data.Settings;
using KebabQuest.Services.Interfaces;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using KebabQuest.Services.Dto;
using Newtonsoft.Json.Linq;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace KebabQuest.Services.Services.AIModels;

public class KandinskyService : IKandinskyService
{
    private readonly KandinskySettings _kandinskySettings;
    private readonly HttpClient _httpClient = new();

    public KandinskyService(
        IOptions<KandinskySettings> kandinskyConfig)
    {
        _kandinskySettings = kandinskyConfig.Value;
    }

    public async Task<int> GetModelId()
    {
        var requestMessage = new HttpRequestMessage(HttpMethod.Get, $"{_kandinskySettings.Url}/models");
        SetHeaders(requestMessage);

        var response = await _httpClient.SendAsync(requestMessage);
        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync();
        var json = JArray.Parse(responseContent);
        return json[0].Value<int>("id");
    }

    public async Task<string> GetImageWhenReady(string requestId)
    {
        const int delayInSeconds = 1;

        while (true)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, GetUrl($"text2image/status/{requestId}"));
            SetHeaders(request);

            using var response = await _httpClient.SendAsync(request);

            var responseModel = JsonConvert.DeserializeObject<KandinskyStatusResponse>
                (await response.Content.ReadAsStringAsync());
            if (responseModel?.Status == "DONE")
            {
                var imageString = responseModel.Images!.FirstOrDefault();
                return imageString ?? throw new InvalidOperationException();
            }

            if (responseModel?.Status == "FAIL")
            {
                break;
            }

            await Task.Delay(TimeSpan.FromSeconds(delayInSeconds));
        }

        throw new InvalidOperationException();
    }

    public async Task<string> GenerateImage(string prompt)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, GetUrl("text2image/run"));
        SetHeaders(request);

        var jsonParams = JsonSerializer.Serialize(new
        {
            type = "GENERATE",
            numImages = 1,
            width = 1024,
            height = 1024,
            generateParams = new
            {
                query = prompt
            }
        });

        var modelId = await GetModelId();
        var formContent = new MultipartFormDataContent();
        formContent.Add(new StringContent(jsonParams, Encoding.UTF8, "application/json"), "params");
        formContent.Add(new StringContent(modelId.ToString()), "model_id");
        request.Content = formContent;

        using var response = await _httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();

        var responseString = await response.Content.ReadAsStringAsync();
        var responseModel = JsonConvert.DeserializeObject<KandinskyGenResponse>(responseString);
        if (responseModel?.Uuid is null)
        {
            throw new InvalidOperationException();
        }

        return await GetImageWhenReady(responseModel.Uuid);
    }

    private void SetHeaders(HttpRequestMessage request)
    {
        request.Headers.Add("X-Key", $"Key {_kandinskySettings.ApiKey}");
        request.Headers.Add("X-Secret", $"Secret {_kandinskySettings.SecretKey}");
        request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }

    private string GetUrl(string url)
    {
        return $"{_kandinskySettings.Url}/{url}";
    }
}