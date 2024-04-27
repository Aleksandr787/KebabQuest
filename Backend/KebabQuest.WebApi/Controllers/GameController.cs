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


        
    }
}
