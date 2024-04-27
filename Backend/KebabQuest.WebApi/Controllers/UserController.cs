using KebabQuest.Services.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using KebabQuest.Data.Dto;

namespace KebabQuest.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<UserTokenDto>> RegisterUser()
        {
            try
            {
                var token = await _userService.RegisterUserAsync();
                return Ok(new UserTokenDto { Id = token });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
