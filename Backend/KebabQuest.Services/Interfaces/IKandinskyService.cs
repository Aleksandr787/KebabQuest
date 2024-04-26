namespace KebabQuest.Services.Interfaces;

public interface IKandinskyService
{
    Task<int> GetModelId();
    Task<string> GetImageWhenReady(string requestId);
    Task<string> GenerateImage(string prompt);
}