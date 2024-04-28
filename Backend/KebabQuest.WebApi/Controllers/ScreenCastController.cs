using KebabQuest.Data.Dto;
using KebabQuest.Data.Models;
using KebabQuest.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace KebabQuest.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScreenCastController : ControllerBase
    {
        private readonly IScreenCastService _screenCastService;

        public ScreenCastController(IScreenCastService screenCastService)
        {
            _screenCastService = screenCastService;
        }

        [HttpGet("get-data")]
        public async Task<ActionResult<ICollection<QuestStep>>> GetScreenCastCollection()
        {
            try
            {
                var screenCastCollection = await _screenCastService.GetScreenCastCollection();
                return Ok(screenCastCollection);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
