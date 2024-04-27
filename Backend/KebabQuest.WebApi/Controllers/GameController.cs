using KebabQuest.Data.Models;
using KebabQuest.Services.Interfaces;
using KebabQuest.Services.Services;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace KebabQuest.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IGameService _gameService;
        private readonly IGameRoomService _gameRoomService;
        
        public GameController(
            IGameService gameService,
            IGameRoomService gameRoomService)
        {
            _gameService = gameService;
            _gameRoomService = gameRoomService;
        }

        [HttpGet("{gameRoomId}")]
        public async Task<IActionResult> GetGameRoomById(string gameRoomId)
        {
            try
            {
                var gameRoom = await _gameRoomService.GetById(gameRoomId);
                return Ok(gameRoom);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            
        }

        [HttpGet("get-all/{userId}")]
        public async Task<IActionResult> GetAllGameRoomByUserId(string userId)
        {
            try
            {
                var gameRooms = await _gameService.GetAllGameRooms(userId);
                return Ok(gameRooms);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("new-game/{userId}")]
        public async Task<IActionResult> GenerateNewGameRoom(string userId)
        {
            try
            {
                var newGameDto = await _gameService.GenerateNewGameRoom(userId);
                return Ok(newGameDto);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("do-step/{roomId}")]
        public async Task<IActionResult> DoStep(string roomId, [FromBody] QuestStep questStep)
        {
            try
            {
                var newGameDto = await _gameService.DoStep(roomId, questStep);
                return Ok(newGameDto);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> RemoveGameRoom(string userId, [FromQuery] string roomId)
        {
            try
            {
                await _gameService.RemoveRoom(userId, roomId);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
    }
}
