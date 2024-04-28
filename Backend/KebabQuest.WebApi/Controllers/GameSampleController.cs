using KebabQuest.Data.Dto;
using KebabQuest.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace KebabQuest.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameSampleController : ControllerBase
    {
        private readonly IGameSampleService _newStoryLineDtoService;

        public GameSampleController(IGameSampleService newStoryLineDtoService)
        {
            _newStoryLineDtoService = newStoryLineDtoService;
        }


        [HttpPost("get-samples")]
        public async Task<ActionResult<ICollection<NewGameDto>>> GetGameSamples()
        {
            try
            {
                var newStoryLineJsonDto = await _newStoryLineDtoService.GetGameSamples();
                return Ok(newStoryLineJsonDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("seed-data")]
        public async Task<IActionResult> SeedData()
        {
            try
            {
                await _newStoryLineDtoService.SeedData();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}
