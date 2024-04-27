using KebabQuest.Data.Dto;
using KebabQuest.Data.Models;

namespace KebabQuest.Services.Interfaces;

public interface IGameLogicService
{
    Task<NewStoryLineJsonDto> GenerateNewStory();
    Task<NewQuestionJsonDto> GenerateNewQuestion(GameRoom gameRoom);
    Task<NewQuestionJsonDto> GenerateFirstQuestion(NewStoryLineJsonDto newStoryLineDot);
    Task<NewStoryLineJsonDto> GenerateNewStoryTheb();
    Task<string> GenerateInitialImage(NewStoryLineJsonDto newStoryLine);
    Task<string> GenerateImagePerStep(GameRoom gameRoom, QuestStep questStep);

}