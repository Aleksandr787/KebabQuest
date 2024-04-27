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
        private readonly INewStoryLineDtoService _newStoryLineDtoService;

        public SeederController(INewStoryLineDtoService newStoryLineDtoService)
        {
            _newStoryLineDtoService = newStoryLineDtoService;
        }


        [HttpPost("[action]")]
        public async Task<ActionResult<NewStoryLineJsonDto>> GetById(string id)
        {
            try
            {
                var NewStoryLineJsonDto = await _newStoryLineDtoService.GetById(id);
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
