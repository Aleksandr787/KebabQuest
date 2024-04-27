using KebabQuest.Data.Dto;
using KebabQuest.Data.JsonPrompts;
using KebabQuest.Data.Models;
using KebabQuest.Data.Settings;
using KebabQuest.Services.Interfaces;
using KebabQuest.Services.Services.AIModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace KebabQuest.Services.Services;

public class GamePromptService : IGamePromptService
{
    private readonly ChatGptProxyService _chatGptProxyService;
    private readonly ChatGptThebService _chatGptThebService;
    private readonly IKandinskyService _kandinskyService;
    private readonly StringPromptsDto _stringPrompts;
    
    public GamePromptService(
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
        var prompt = NewStoryLine.Json + _stringPrompts.NewStoryLine;
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
        var prompt = NewStoryLine.Json + _stringPrompts.NewStoryLine;
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

    public async Task<NewQuestionJsonDto> GenerateNewQuestion(GameRoom gameRoom)
    {
        var messages = new JArray
        {
            new JObject
            {
                { "role", "system" },
                { "content", $"{gameRoom.Title}\n{gameRoom.Plot}\n{GetMainPlayerInfo(gameRoom.MainPlayer!)}" }
            }
        };

        if (gameRoom.Steps is not null)
        {
            foreach (var step in gameRoom.Steps)
            {
                var questionStep = new JObject {
                    { "role", "system" },
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

        var prompt = NewQuestion.Json + _stringPrompts.NewQuestion;
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

    private string GetMainPlayerInfo(MainPlayer mainPlayer)
    {
        var info = $"Main player: ${mainPlayer.Race}, ${mainPlayer.Gender}";
        return info;
    }
}