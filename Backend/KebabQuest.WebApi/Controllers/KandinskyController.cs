using KebabQuest.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace KebabQuest.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class KandinskyController : ControllerBase
{
    private readonly IKandinskyService _kandinskyService;
    
    public KandinskyController(IKandinskyService kandinskyService)
    {
        _kandinskyService = kandinskyService;
    }

    [HttpPost]
    public async Task<IActionResult> GenerateImage([FromQuery] string prompt)
    {
        // for testing
        var imageString = await _kandinskyService.GenerateImage(prompt);
        return Ok(imageString);
    }
}