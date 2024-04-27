using KebabQuest.Data.Models;
using KebabQuest.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace KebabQuest.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        IGameRoomService _gameRoomService { get; set; }

        public GameController(IGameRoomService gameRoomService)
        {
            _gameRoomService = gameRoomService;
        }


        [HttpPost("[action]")]
        public async Task<ActionResult<string>> CreateGameRoom(GameRoom newRoom)
        {
            try
            {
                string id = await _gameRoomService.CreateGameRoom(newRoom);
                return Ok(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> DeleteGameRoom(string id)
        {
            try
            {
                await _gameRoomService.DeleteGameRoom(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<GameRoom>> Get(string id)
        {
            try
            {
                return Ok(await _gameRoomService.GetById(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
