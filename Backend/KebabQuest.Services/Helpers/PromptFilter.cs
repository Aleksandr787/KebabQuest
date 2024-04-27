using Newtonsoft.Json.Linq;

namespace KebabQuest.Services.Helpers;

public static class PromptFilter
{
    public static string GetValidPromptForImage(this JObject promptObject)
    {
        const int validPromptLength = 1000;
        var promptString = promptObject.ToString();
        return promptString.Length < validPromptLength 
            ? promptString
            : promptString.Substring(0, validPromptLength - 1);
    }
}