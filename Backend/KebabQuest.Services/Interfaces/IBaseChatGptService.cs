namespace KebabQuest.Services.Interfaces;

public interface IBaseChatGptService
{
    Task<string> SendRequest(string prompt);

}