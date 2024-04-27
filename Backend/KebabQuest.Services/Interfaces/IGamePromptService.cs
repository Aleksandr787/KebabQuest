using KebabQuest.Data.Dto;
using KebabQuest.Data.Models;

namespace KebabQuest.Services.Interfaces;

public interface IGamePromptService
{
    Task<NewStoryLineJsonDto> GenerateNewStory();
    Task<NewQuestionJsonDto> GenerateNewQuestion(GameRoom gameRoom);
    Task<NewStoryLineJsonDto> GenerateNewStoryTheb();

}