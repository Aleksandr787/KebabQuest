using System.Text.Json.Serialization;

namespace KebabQuest.Services.Dto;

public class KandinskyGenResponse
{
    [JsonPropertyName("uuid")]
    public string? Uuid { get; set; }

    [JsonPropertyName("status")]
    public string? Status { get; set; }
}