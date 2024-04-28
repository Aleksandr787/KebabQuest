using System.Text.RegularExpressions;
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
        var extractedJsonObject = ExtractJsonObject(newStoryLineJson);
        var newGameDto = JsonConvert.DeserializeObject<NewStoryLineJsonDto>(extractedJsonObject);
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

    public async Task<string> GenerateInitialImage(NewStoryLineJsonDto newStoryLine)
    {
        var prompt = $"{_stringPrompts.InitialImage} {InfoPromptForInitialImage(newStoryLine)}";
        return await _kandinskyService.GenerateImage(prompt);
    }

    public async Task<string> GenerateImagePerStep(GameRoom gameRoom, QuestStep questStep)
    {
        var prompt = $"{_stringPrompts.ImagePerStep} {await InfoPromptForImagePerStep(gameRoom, questStep)}";
        return await _kandinskyService.GenerateImage(prompt);
    }

    public async Task<bool> IsAnswerValid(string answer)
    {
        var prompt = $" Текст:{answer} {_stringPrompts.ValidateAnswer}.";

        var messages = new JArray
        {
            new JObject
            {
                { "role", "user" },
                { "content", prompt }
            }
        };

        var isValid = await _chatGptProxyService.SendRequest(null, messages);
        return isValid == "1";
    }

    public async Task<NewQuestionJsonDto> GenerateFirstQuestion(NewStoryLineJsonDto newStoryLineJsonDto)
    {
        var prompt = $"{_stringPrompts.InitialQuestion} ${NewQuestion.JsonPrompt}";
        var messages = new JArray
        {
            new JObject
            {
                { "role", "assistant" },
                { "content", GetInfoForFirstQuestion(newStoryLineJsonDto) }
            },
            new JObject
            {
                { "role", "user" },
                { "content", prompt }
            }
        };

        var newQuestionJsonString = await _chatGptProxyService.SendRequest(null, messages);
        var extractedJsonObject = ExtractJsonObject(newQuestionJsonString);
        var newQuestionDtoModel = JsonConvert.DeserializeObject<NewQuestionJsonDto>(extractedJsonObject);
        if (newQuestionDtoModel is null) throw new InvalidOperationException("New question model was not created correctly");
        return newQuestionDtoModel;
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

        var prompt = _stringPrompts.NewQuestion + NewQuestion.JsonPrompt;
        var promptModel = new JObject {
            { "role", "user" },
            { "content", prompt }
        };
        messages.Add(promptModel);

        var newQuestionJson = await _chatGptProxyService.SendRequest(null, messages);
        var extractedJsonObject = ExtractJsonObject(newQuestionJson);
        var newQuestionDto = JsonConvert.DeserializeObject<NewQuestionJsonDto>(extractedJsonObject);
        if (newQuestionDto is null) throw new InvalidOperationException("New question model was not created correctly");
        return newQuestionDto;
    }

    private string InfoPromptForInitialImage(NewStoryLineJsonDto newStoryLine)
    {
        var info = new JObject
        {
            { "title", newStoryLine.Title },
            { "gameColors", newStoryLine.GameColors },
            { "mainPlayer", JObject.FromObject(newStoryLine.MainPlayer!) }
        };

        return info.GetValidPromptForImage();
    }

    private async Task<string> InfoPromptForImagePerStep(GameRoom gameRoom, QuestStep questStep)
    {
        var info = new JObject
        {
            { "question", questStep.Question},
            { "gameColors", gameRoom.GameColors },
            { "mainPlayer", JObject.FromObject(gameRoom.MainPlayer!) }
        };

        if (questStep.Answer is null)
        {
            return info.GetValidPromptForImage();
        }

        var isValid = await IsAnswerValid(questStep.Answer);
        if (isValid)
        {
            info.Add(new JProperty("answer", questStep.Answer));
        }

        return info.GetValidPromptForImage();
    }

    private string GetMainPlayerInfoForNewQuestion(MainPlayer mainPlayer)
    {
        var info = $"Main player: ${mainPlayer.Race}, ${mainPlayer.Gender}";
        return info;
    }

    private string GetInfoForFirstQuestion(NewStoryLineJsonDto newStoryLineJsonDto)
    {
        var info = new JObject
        {
            { "title", newStoryLineJsonDto.Title },
            { "plot", newStoryLineJsonDto.Plot },
            { "mainPlayer", JObject.FromObject(newStoryLineJsonDto.MainPlayer) },
        };

        return info.ToString();
    }

    private string ExtractJsonObject(string answerContent)
    {
        var regex = new Regex(@"\{.*\}");
        var match = regex.Match(answerContent.Replace("\n", ""));
        if (match.Success)
        {
            return match.Value;
        }

        throw new InvalidOperationException("Ai model didn't return json object");
    }
}