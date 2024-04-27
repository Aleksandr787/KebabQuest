using KebabQuest.Data.Dto;
using KebabQuest.Data.Models;
using KebabQuest.Data.Repositories;
using KebabQuest.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KebabQuest.Services.Services
{
    public class GameService : IGameService
    {
        private readonly IGameRoomService _gameRoomService;
        private readonly  _gameService;


        public GameService() { }


        public async Task<NewStoryLineJsonDto> GenerateNewGameRoom()
        {

        }

        public async Task<GameRoom> GetGameRoom(string gameRoomId)
        {

        }

        public async Task DeleteRoom(string gameRoomId)
        {

        }

        public async Task<ICollection<string>?> GetAllGameRooms(string userId)
        {

        }

        public async Task<QuestStep> DoStep(QuestStep step, string roomId)
        {

        }
    }
}
