using System.Text.Json.Serialization;

namespace KebabQuest.Services.Dto;

public class KandinskyStatusResponse
{
    [JsonPropertyName("uuid")]
    public string? Uuid { get; set; }

    [JsonPropertyName("status")]
    public string? Status { get; set; }

    [JsonPropertyName("images")]
    public string[]? Images { get; set; }

    [JsonPropertyName("errorDescription")]
    public string? ErrorDescription { get; set; }

    [JsonPropertyName("censored")]
    public bool Censored { get; set; }
}