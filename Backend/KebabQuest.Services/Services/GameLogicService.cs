using KebabQuest.Data.Dto;
using KebabQuest.Data.JsonPrompts;
using KebabQuest.Data.Models;
using KebabQuest.Data.Settings;
using KebabQuest.Services.Helpers;
using KebabQuest.Services.Interfaces;
using KebabQuest.Services.Services.AIModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace KebabQuest.Services.Services;

public class GameLogicService : IGameLogicService
{
    private readonly ChatGptProxyService _chatGptProxyService;
    private readonly ChatGptThebService _chatGptThebService;
    private readonly IKandinskyService _kandinskyService;
    private readonly StringPromptsDto _stringPrompts;
    
    public GameLogicService(
        ChatGptThebService chatGptThebService,
        ChatGptProxyService chatGptProxyService,
        IKandinskyService kandinskyService,
        StringPromptsDto stringPrompts)
    {
        _chatGptThebService = chatGptThebService;
        _chatGptProxyService = chatGptProxyService;
        _kandinskyService = kandinskyService;
        _stringPrompts = stringPrompts;
    }
    
    public async Task<NewStoryLineJsonDto> GenerateNewStory()
    {
        var prompt = NewStoryLine.JsonPrompt + _stringPrompts.NewStoryLine;
        var messages = new JArray
        {
            new JObject
            {
                { "role", "user" },
                { "content", prompt }
            }
        };
        
        var newStoryLineJson = await _chatGptProxyService.SendRequest(null, messages);
        var newGameDto = JsonConvert.DeserializeObject<NewStoryLineJsonDto>(newStoryLineJson);
        if (newGameDto is null)
        {
            throw new InvalidOperationException("New storyline model was not created correctly");
        }

        return newGameDto;
    }
    
    public async Task<NewStoryLineJsonDto> GenerateNewStoryTheb()
    {
        // just for testing
        var prompt = NewStoryLine.JsonPrompt + _stringPrompts.NewStoryLine;
        var messages = new JArray
        {
            new JObject
            {
                { "role", "user" },
                { "content", prompt }
            }
        };
        
        var newStoryLineJson = await _chatGptThebService.SendRequest(null, messages);
        var newGameDto = JsonConvert.DeserializeObject<NewStoryLineJsonDto>(newStoryLineJson);
        if (newGameDto is null)
        {
            throw new InvalidOperationException("New storyline model was not created correctly");
        }

        return newGameDto;
    }

    public async Task<string> GenerateInitialImage(GameRoom gameRoom)
    {
        var prompt = $"{_stringPrompts.InitialImage} {InfoPromptForInitialImage(gameRoom)}";
        return await _kandinskyService.GenerateImage(prompt);
    }

    public async Task<string> GenerateImagePerStep(GameRoom gameRoom, QuestStep questStep)
    {
        var prompt = $"{_stringPrompts.ImagePerStep} {InfoPromptForImagePerStep(gameRoom, questStep)}";
        return await _kandinskyService.GenerateImage(prompt);
    }

    public async Task<NewQuestionJsonDto> GenerateNewQuestion(GameRoom gameRoom)
    {
        var messages = new JArray
        {
            new JObject
            {
                { "role", "assistant" },
                { "content", $"{gameRoom.Title}\n{gameRoom.Plot}\n{GetMainPlayerInfoForNewQuestion(gameRoom.MainPlayer!)}" }
            }
        };

        if (gameRoom.Steps is not null)
        {
            foreach (var step in gameRoom.Steps)
            {
                var questionStep = new JObject {
                    { "role", "assistant" },
                    { "content", step.Question }
                };
                
                var answerStep = new JObject {
                    { "role", "user" },
                    { "content", step.Answer }
                };

                messages.Add(questionStep);
                messages.Add(answerStep);
            }
        }

        var prompt = NewQuestion.JsonPrompt + _stringPrompts.NewQuestion;
        var promptModel = new JObject {
            { "role", "user" },
            { "content", prompt }
        };
        messages.Add(promptModel);
        var newQuestionJson = await _chatGptProxyService.SendRequest(null, messages);
        var newQuestionDto = JsonConvert.DeserializeObject<NewQuestionJsonDto>(newQuestionJson);
        if (newQuestionDto is null) throw new InvalidOperationException("New question model was not created correctly");
        return newQuestionDto;
    }

    private string InfoPromptForInitialImage(GameRoom gameRoom)
    {
        var info = new JObject
        {
            { "title", gameRoom.Title },
            { "gameColors", gameRoom.GameColors },
            { "mainPlayer", JObject.FromObject(gameRoom.MainPlayer!) }
        };

        return info.GetValidPromptForImage();
    }

    private string InfoPromptForImagePerStep(GameRoom gameRoom, QuestStep questStep)
    {
        var info = new JObject
        {
            { "question", questStep.Question},
            { "answer", questStep.Answer},
            { "gameColors", gameRoom.GameColors },
            { "mainPlayer", JObject.FromObject(gameRoom.MainPlayer!) }
        };

        return info.GetValidPromptForImage();
    }

    private string GetMainPlayerInfoForNewQuestion(MainPlayer mainPlayer)
    {
        var info = $"Main player: ${mainPlayer.Race}, ${mainPlayer.Gender}";
        return info;
    }
}