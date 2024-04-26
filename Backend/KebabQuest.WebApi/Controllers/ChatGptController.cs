using KebabQuest.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace KebabQuest.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ChatGptController : ControllerBase
{
    private readonly IChatGptProxyService _chatGptProxyService;
    private readonly IChatGptThebService _chatGptThebService;
    
    public ChatGptController(IChatGptProxyService chatGptProxyService, IChatGptThebService chatGptThebService)
    {
        _chatGptProxyService = chatGptProxyService;
        _chatGptThebService = chatGptThebService;
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
}