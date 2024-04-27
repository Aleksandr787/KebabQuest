using KebabQuest.Data.Dto;
using KebabQuest.Data.Models;
using KebabQuest.Services.Helpers;
using KebabQuest.Services.Interfaces;
using KebabQuest.Services.Services;
using KebabQuest.WebApi.Dto;
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
            _gameService = gameService;
        }

        [HttpGet("{gameRoomId}")]
        public async Task<ActionResult> GetGameRoomById(string gameRoomId)
        {
            try
            {
                var newGameDto = await _gameService.GetById(gameRoomId);
                return Ok(newGameDto);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [HttpGet("get-all-games/{userId}")]
        public async Task<ActionResult> GetAllGameRoomByUserId(string userId)
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

        [HttpGet("game-from-sample/{sampleId}/{userId}")]
        public async Task<IActionResult> StartGameFromSample(string sampleId, string userId)
        {
            try
            {
                var newGame = await _gameService.StartGameFromGameSample(sampleId, userId);
                return Ok(newGame);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
        [HttpGet("is-valid")]
        public async Task<IActionResult> IsAnswerValid([FromQuery] string answer)
        {
            // just for testing the validation of user answers
            var isValid = await _gameService.IsAnswerValid(answer);
            return Ok(isValid);
        }

        [HttpPost("new-game/{userId}")]
        public async Task<ActionResult<NewGameDto>> GenerateNewGameRoom(string userId)
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
        public async Task<ActionResult<QuestStep>> DoStep(string roomId, [FromBody] AnswerDto answerDto)
        {
            try
            {
                if (answerDto.Answer is null)
                {
                    return BadRequest();
                }
                
                var newStep = await _gameService.DoStep(roomId, answerDto.Answer);
                return Ok(newStep);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{userId}")]
        public async Task<ActionResult> RemoveGameRoom(string userId, [FromQuery] string roomId)
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
