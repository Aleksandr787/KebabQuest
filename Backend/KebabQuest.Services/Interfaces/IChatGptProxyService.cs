namespace KebabQuest.Services.Interfaces;

public interface IChatGptProxyService
{
    Task<string> SendRequest(string prompt);
}