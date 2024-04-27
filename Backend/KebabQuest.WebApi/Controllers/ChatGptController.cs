using KebabQuest.Services.Interfaces;
using KebabQuest.Services.Services;
using KebabQuest.Services.Services.AIModels;
using Microsoft.AspNetCore.Mvc;

namespace KebabQuest.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ChatGptController : ControllerBase
{
    private readonly ChatGptProxyService _chatGptProxyService;
    private readonly ChatGptThebService _chatGptThebService;
    private readonly IGamePromptService _gamePromptService;
    
    public ChatGptController(
        ChatGptProxyService chatGptProxyService,
        ChatGptThebService chatGptThebService,
        IGamePromptService gamePromtService)
    {
        _chatGptProxyService = chatGptProxyService;
        _chatGptThebService = chatGptThebService;
        _gamePromptService = gamePromtService;
    }
    
    //for testing
    [HttpPost("chat-proxy")]
    public async Task<IActionResult> ChatProxy([FromQuery] string prompt)
    {
        var answer = await _chatGptProxyService.SendRequest(prompt);
        return Ok(answer);
    }

    [HttpPost("chat-theb")]
    public async Task<IActionResult> ChatTheb([FromQuery] string prompt)
    {
        var answer = await _chatGptThebService.SendRequest(prompt);
        return Ok(answer);
    }

    [HttpPost("new-story-line")]
    public async Task<IActionResult> GenerateNewStoryLine()
    {
        var newStoryLine = await _gamePromptService.GenerateNewStory();
        return Ok(newStoryLine);
    }
    
    [HttpPost("new-story-line-theb")]
    public async Task<IActionResult> GenerateNewStoryLineTheb()
    {
        var newStoryLine = await _gamePromptService.GenerateNewStoryTheb();
        return Ok(newStoryLine);
    }
}