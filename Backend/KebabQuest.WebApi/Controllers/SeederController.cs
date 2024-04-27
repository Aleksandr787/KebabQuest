using KebabQuest.Data.Dto;
using KebabQuest.Data.Repositories;
using KebabQuest.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace KebabQuest.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeederController : ControllerBase
    {
        private readonly IGameSampleService _newStoryLineDtoService;

        public SeederController(IGameSampleService newStoryLineDtoService)
        {
            _newStoryLineDtoService = newStoryLineDtoService;
        }


        [HttpPost("[action]")]
        public async Task<ActionResult<ICollection<NewGameDto>>> GetGameSamples()
        {
            try
            {
                var NewStoryLineJsonDto = await _newStoryLineDtoService.GetGameSamples();
                return Ok(NewStoryLineJsonDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Generate()
        {
            try
            {
                await _newStoryLineDtoService.Generate();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}
