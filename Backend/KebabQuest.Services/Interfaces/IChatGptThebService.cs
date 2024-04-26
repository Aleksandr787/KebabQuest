namespace KebabQuest.Services.Interfaces;

public interface IChatGptThebService
{
    Task<string> GetModels();
    Task<string> SendRequest(string prompt);
}